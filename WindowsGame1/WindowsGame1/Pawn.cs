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
    class Pawn : Piece 

    {
        public Pawn(Game1 game, int x, int y)
            : base(game, x, y) //constructor
        {
        }
        
        protected override void LoadImage()
        {

            imagefile = game.Content.Load<Texture2D>("pawn"); //load pawn image
            
        }

        public override int Type() {
            return 8; //special type for pawn moves and promotion
        }

        public override bool CanMoveTo(int X, int Y, int x, int y)
        {

            if (game.turn == false) //if a white pawn
            {


                if (Y - y == 2 && Y == 6 && game.board[x, y] == 0 && game.board[x, y + 1] == 0) //check if the pawn is able to make an intial move of 2 squares
                {

                    if (X == x)
                    {
                        return true;
                    }


                }
                else if (Y - y == 1) 
                {
                    if (X == x && game.board[x, y] == 0) //check if it can move forward
                    {
                        return true;
                    }
                    else if (X - x == 1 || X - x == -1) //check if a piece is available to capture
                    {
                        if (game.board[x, y] > 0)
                        {
                            return true;
                        }
                    }
                }

            }

            else //black pawn
            {
                if (Y - y == -2 && Y == 1 && game.board[x, y] == 0 && game.board[x, y -1] == 0) //check if initial move of 2 squares is possible
                {

                    if (X == x)
                    {
                        return true;
                    }


                }
                else if (Y - y == -1) 
                {
                    if (X == x && game.board[x, y] == 0) //check if moving forward 1
                    {
                        return true;
                    }
                    else if (X - x == 1 || X - x == -1) //check if a capture is possible
                    {
                        if (game.board[x, y] > 0)
                        {
                            return true;
                       }
                    }
                }
            }

            return false; //if these all fail the pawn cannot move to the square given
        }

    }
}
