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
    class Rook: Piece 

    {
        public Rook(Game1 game, int x, int y)
            : base(game, x, y) //constructor
        {
        }
        
        protected override void LoadImage()
        {

            imagefile = game.Content.Load<Texture2D>("Rook"); //load rook image
            
        }

        public override int Type() { //defualt type
            return 1;
    }

        public override bool CanMoveTo(int X, int Y, int x, int y) 
        {


            if (X == x && Y != y) //check if the move is a valid vertical move
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

            else if (Y == y && X != x) //check if the move is a valid horizontal move
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

            return false; //otherwise the move is not a valid rook move
        }

    }
}
