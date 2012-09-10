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
    class Knight: Piece 

    {
        public Knight(Game1 game, int x, int y)
            : base(game, x, y) //constructor
        {
        }
        
        protected override void LoadImage()
        {

            imagefile = game.Content.Load<Texture2D>("Knight"); //load knight image
            
        }

        public override int Type() {
            return 3; //special type for checking for checkmate
    }

        public override bool CanMoveTo(int X, int Y, int x, int y) //check every direction in a knight "L" shape
        {
            if ((((X - x) == 2) || ((X - x) == -2)) && (((Y - y) == 1) || ((Y - y) == -1)))
            {
                return true;
            }

            else if ((((Y - y) == 2) || ((Y - y) == -2)) && (((X - x) == 1) || ((X - x) == -1)))
            {
                return true; 
            }

            return false;
        }

    }
}
