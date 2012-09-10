using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    
    public abstract class Piece
    {
        protected Game1 game;
        //protected int movetype;
        protected Texture2D imagefile;
        private bool taken = false;
        protected Rectangle position;
        int x, y;
        public int X { get { return x; } set { x = value; } } //determine a pieces X and Y coordinates
        public int Y { get { return y; } set { y = value; } }


        public bool Clicked { get; set; } //determine if the piece has been selected


        public bool Taken //determine if a piece has been taken
        
        {
            get
            {
                return taken;
            }
            set
            {
                taken = value;
            }
        }

        public abstract int Type(); //type of piece default 1 unless special exceptions are needed

        public abstract bool CanMoveTo(int X, int Y, int x, int y); //check if a piece can move to a given square from a given square

        public Piece(Game1 game, int x, int y) //contructor
        {
            this.game = game;
            this.x = x;
            this.y = y;
            this.LoadImage();
        }

        public void DrawPiece (SpriteBatch spriteBatch, int colour, int width, bool clicked) //draw piece
        {
            position = new Rectangle(0 + width / 8 * x, width / 8* y, width / 8, width / 8); //set to the size of a single square

            if (colour == 2)
            {
                spriteBatch.Draw(imagefile, position, Color.Transparent); //make invisible if taken
            }

            else if (colour == 0)
            {
                if (clicked == true)
                {
                    spriteBatch.Draw(imagefile, position, Color.Yellow); //highlight if selected

                }
                else
                {
                    spriteBatch.Draw(imagefile, position, Color.White); //draw as a white piece
                }
            }


            else
            {
                if (clicked == true)
                {
                    spriteBatch.Draw(imagefile, position, Color.Yellow); //highlight if selected
                }
                else
                {
                    spriteBatch.Draw(imagefile, position, Color.DarkGray); //draw as a black piece
                }
            }
        }

        protected abstract void LoadImage(); //abstract function to load the pieces image

        

        //public abstract String Piecetype();

    }
}
