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
    class King : Piece 

    {

        public King(Game1 game, int x, int y)
            : base(game, x, y) //constuctor
        {
        }
        
        protected override void LoadImage()
        {

            imagefile = game.Content.Load<Texture2D>("chess_piece_white_king"); //load king image file
            
        }

        public override int Type() //set type
        {
            return 7;
        }


        public override bool CanMoveTo(int X, int Y, int x, int y)
        {
            

            if ((X - x) < 2 && (X - x) > -2 && (Y - y) < 2 && (Y - y) > -2) //check that the dirence in any direction is no more than 1
            {
                return true;
            }
            return false;
        }

    }
}
