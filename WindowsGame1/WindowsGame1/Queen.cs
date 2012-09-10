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
    class Queen: Piece

    {
        public Queen(Game1 game, int x, int y)
            : base(game, x, y)
        {
        }
        
        protected override void LoadImage()
        {

            imagefile = game.Content.Load<Texture2D>("Queen"); 
            
        }

        public override int Type() //default type 
        {
            return 1;
        }

        public override bool CanMoveTo(int X, int Y, int x, int y) //check if queen can move to a given space
        {

            if (X == x && Y != y) //handle vertical movements
            {
                if (Y > y)
                {
                    for (int i = y + 1; i < Y; i++)
                    {
                        if (game.board[x, i] != 0)
                        {
                            return false;
                        }
                    }
                    return true;

                }

                else
                {
                    for (int i = Y + 1; i < y; i++)
                    {
                        if (game.board[x, i] != 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            else if (Y == y && X != x) //handle horizontal movements
            {
                if (X > x)
                {
                    for (int i = x + 1; i < X; i++)
                    {
                        if (game.board[i, y] != 0)
                        {
                            return false;
                        }
                    }
                    return true;

                }

                else
                {
                    for (int i = X + 1; i < x; i++)
                    {
                        if (game.board[i, y] != 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }


            if ((X - x) == (Y - y) || (X - x) == (y - Y)) //handles diagonal movements in all 4 directions
            {
                if (X > x)
                {

                    if (Y > y)
                    {

                        for (int i = 1; i < (X - x); i++)
                        {
                            if (game.board[x + i, y + i] != 0)
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
                            if (game.board[x + i, y - i] != 0)
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
                            if (game.board[x - i, y - i] != 0)
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
                            if (game.board[x - i, y + i] != 0)
                            {
                                return false;
                            }
                        }
                        return true;

                    }



                }
            }
            return false; //if none of these pass the queen is unable to move to the given square


        }

    }
}
