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
    class Bishop: Piece 

    {
        public Bishop(Game1 game, int x, int y)
            : base(game, x, y) //constructor
        {
        }
        
        protected override void LoadImage()
        {

            imagefile = game.Content.Load<Texture2D>("Bishop"); //load bishop image
            
        }

        public override int Type() {
            return 0; //default type
    }
        public override bool CanMoveTo(int X, int Y, int x, int y)
        {

            if ((X - x) == (Y - y) || (X - x) == (y - Y)) //check if the move is possible diagonally in any direction
            {
                if (X > x)
                {

                    if (Y > y)
                    {

                        for (int i = 1; i < (X - x); i++)
                        {
                            if (game.board[x + i, y + i] != 0) //make sure nothing is blocking the move
                            {
                                return false;
                            }
                        }
                        return true;

                    }

                    else
                    {
                        for (int i = 1; i < (X - x); i++)
                        {
                            if (game.board[x + i, y - i] != 0) //make sure nothing is blocking the move
                            {
                                return false;
                            }
                        }
                        return true;

                    }


                }

                else
                {

                    if (Y < y)
                    {

                        for (int i = 1; i < (x - X); i++)
                        {
                            if (game.board[x - i, y - i] != 0) //make sure nothing is blocking the move
                            {
                                return false;
                            }
                        }
                        return true;

                    }

                    else
                    {
                        for (int i = 1; i < (x - X); i++)
                        {
                            if (game.board[x - i, y + i] != 0) //make sure nothing is blocking the move
                            {
                                return false;
                            }
                        }
                        return true;

                    }



                }
            }
            return false; //return false if the move is impossible for the bishop
        }

    }
}
