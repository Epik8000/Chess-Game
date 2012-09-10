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
using System.Runtime.InteropServices; 
using System.Windows.Forms;



namespace WindowsGame1
{
    class MovementHandler 
    {
        Game1 game;
        int x;
        int y;
        int checkerx;
        int checkery;
        int type;
        int xtemp;
        int ytemp;
        int a;
        int b;
        int d;
        int e;
        int kingIndex;
        
        public MovementHandler(Game1 game)
        {
            this.game = game;
        }

        private int Check(int x, int y) //function to return number of pieces that check the king on a given space
        {
            int counter = 0;
            if (game.turn == false)
            {
                game.turn = true; //temporarily switch turn as pawns move differently depending on turn 
                a = 16; // set counters for opposing side
                b = 32;
                d = 0; //set counters for own side
                e = 16;
            }

            else
            {
                game.turn = false; //temporarily switch turn as pawns move differently depending on turn (for potential enemy pawn move checking)
                a = 0; //set counters for opposing side
                b = 16;
                d = 16; //set counters for own side
                e = 32;
            }
            for (int i = a; i < b; i++) //check all enemy pieces to see if they can reach the given position
            {
                if (game.pieces[i].CanMoveTo(game.pieces[i].X, game.pieces[i].Y, x, y) == true && game.pieces[i].Taken == false)
                {
                    if (!(game.pieces[i].X == x && game.pieces[i].Type() == 8))
                    {
                        counter++; //count number of checks
                        if (counter == 1)
                        {
                            checkerx = game.pieces[i].X; //save the first checker in case of there being only one
                            checkery = game.pieces[i].Y;
                            type = game.pieces[i].Type();
                        }
                    }
                }



            }
            game.turn = !game.turn; //change back to current turn
            return counter;
        }

        private bool Checkmate() //function to check if checkmate
        {

            if (game.turn == false) //assign the current players king
            {
                kingIndex = 8;
            }
            else{
                kingIndex = 24; 
            }

            if (Check(game.pieces[kingIndex].X, game.pieces[kingIndex].Y) == 0) //return false if not in check
            {
                return false;
            }

            if (Check(game.pieces[kingIndex].X, game.pieces[kingIndex].Y) == 1) //if there is only one piece checking the king see if any friendly pieces can block the check first
            {
                int checkerx = 0;
                int checkery = 0;
                int type = 0;



                if (type == 8 || type == 3) //if piece is a knight or pawn check is impossible to block so skip to the next step
                {
                   
                }

                //otherwise depending on the type of check see if any friendly pieces can move in between them to block the check

                else if (checkerx == game.pieces[kingIndex].X) //check for friendly pieces that can block vertically
                {
                    if (checkery > game.pieces[kingIndex].Y) 
                    {
                        for (int i = game.pieces[kingIndex].Y + 1; i < checkery; i++)
                        {
                            for (int j = d; j < e; j++) //loop over friendly pieces
                            {
                                if (game.pieces[j].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, checkerx, i) && game.pieces[j].Type() != 7) //if any can block that aren't the king it is not checkmate
                                {
                                    Console.WriteLine("1");
                                    return false;
                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = checkery; i < game.pieces[kingIndex].Y; i++)
                        {
                            for (int j = d; j < e; j++) //loop over friendly pieces
                            {
                                if (game.pieces[j].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, checkerx, i) && game.pieces[j].Type() != 7) //if any can block that aren't the king it is not checkmate
                                {
                                    Console.WriteLine(j);
                                    Console.WriteLine("2");
                                    return false;
                                }
                            }
                        }

                    }


                }

                else if (checkery == game.pieces[kingIndex].Y) //check for friendly pieces that can block horizontally
                {
                    if (checkerx > game.pieces[kingIndex].X)
                    {
                        for (int i = game.pieces[kingIndex].X + 1; i < checkery; i++)
                        {
                            for (int j = d; j < e; j++) //loop over friendly pieces
                            {
                                if (game.pieces[j].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, i, checkery) && game.pieces[j].Type() != 7) //if any can block that aren't the king it is not checkmate
                                {

                                    return false;

                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = checkery; i < game.pieces[kingIndex].X; i++)
                        {
                            for (int j = d; j < e; j++) //loop over friendly pieces
                            {
                                if (game.pieces[j].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, i, checkery) && game.pieces[j].Type() != 7) //if any can block that aren't the king it is not checkmate
                                {

                                    return false;
                                }
                            }
                        }

                    }


                }

                
                else 
                {
                    if (checkerx > game.pieces[kingIndex].X) //check diagonally in all 4 directions for possible blocking pieces
                    {
                        if (checkery > game.pieces[kingIndex].Y)
                        {
                            for (int i = game.pieces[kingIndex].X + 1; i <= checkerx; i++)
                            {
                                for (int j = game.pieces[kingIndex].Y + 1; i <= checkery; j++)
                                {
                                    for (int k = d; k < e; k++)
                                    {
                                        if (game.pieces[j].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, i, j) && game.pieces[j].Type() != 8 && game.pieces[j].Type() != 7)
                                        {
                                            Console.WriteLine("3");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = game.pieces[kingIndex].X + 1; i <= checkerx; i++) //diagonal 2
                            {
                                for (int j = checkery; i < game.pieces[kingIndex].Y; j++)
                                {
                                    for (int k = 0; k < 16; k++)
                                    {
                                        if (game.pieces[j].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, i, j) && game.pieces[j].Type() != 8 && game.pieces[j].Type() != 7)
                                        {
                                            Console.WriteLine("4");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        if (checkery > game.pieces[kingIndex].Y) //diagonal 3
                        {
                            for (int i = checkerx + 1; i < game.pieces[kingIndex].X; i++)
                            {
                                for (int j = game.pieces[kingIndex].Y + 1; i <= checkery; j++)
                                {
                                    for (int k = 0; k < 16; k++)
                                    {
                                        if (game.pieces[j].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, i, j) && game.pieces[j].Type() != 8 && game.pieces[j].Type() != 7)
                                        {
                                            Console.WriteLine("5");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = checkerx; i < game.pieces[kingIndex].X; i++) //diagonal 4
                            {
                                for (int j = checkery + 1; i < game.pieces[kingIndex].Y; j++)
                                {
                                    for (int k = d; k < e; k++)
                                    {
                                        if (game.pieces[k].CanMoveTo(game.pieces[j].X, game.pieces[j].Y, i, j) && game.pieces[j].Type() != 7)
                                        {
                                            Console.WriteLine("7");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                Console.WriteLine("Passed");

                //if no pieces can block or more than one piece is checking the king, see if the king can make a move out of check
                //loop over all possible spaces the king could move to and check if they can be reached by an enemy piece

                for (int i = game.pieces[kingIndex].X - 1; i <= game.pieces[kingIndex].X + 1; i++)
                {
                    if (i < 0 || i > 7)
                    {
                        continue;
                    }
                    for (int j = game.pieces[kingIndex].Y - 1; j <= game.pieces[kingIndex].Y + 1; j++)
                    {
                        if (j < 0 || j > 7)
                        {
                            continue;
                        }
                        if (game.pieces[kingIndex].CanMoveTo(game.pieces[kingIndex].X, game.pieces[kingIndex].Y, x, y) == true && (!((game.board[i, j] > a) && (game.board[i, j] <= b + 1)) || game.board[i, j] == 0))
                        {
                            bool check = false;
                            for (int k = a; k < b; k++) //loop over enemy pieces
                            {
                                if (game.pieces[k].CanMoveTo(game.pieces[k].X, game.pieces[k].Y, i, j)) //check if they can reach the kings desired location
                                {
                                    check = true;
                                    break;
                                }

                            }
                            if (check == false) //if move is valid it is not checkmate
                            {
                                Console.WriteLine("8");
                                return false;
                                
                            }


                        }
                    }

                }
                
            }
            return true;
        }




        public void Update(GameTime gameTime, int width, ref bool turn)
        {
            MouseState ms = Mouse.GetState();

            if (ms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) //get the current mouse state
            {
                
                x = (ms.X * 8) / width; //save mouse click coordinates
                y = (ms.Y * 8) / width;


               if (Checkmate() == true) // if checkmate announce winner
               {
                   if (turn == false){ //black win white is checkmated
                        DialogResult choice = MessageBox.Show("Black Wins, New game?", "Results", MessageBoxButtons.YesNo);
                        if (choice == DialogResult.Yes) //allow the user to start a new game
                        {
                            game.newgame();
                        }
                        else if (choice == DialogResult.No) //allow the user to quit
                        {
                            game.Exit();
                        }


                        Console.WriteLine("Black Wins");
                        game.gameover = 2;
                    }  

                   else { //white win black is checkmated
                        DialogResult choice = MessageBox.Show("White Wins, New game?", "Results", MessageBoxButtons.YesNo);
                        if (choice == DialogResult.Yes) //allow the user to start a new game
                        {
                            game.newgame();
                        }
                        else if (choice == DialogResult.No) //allow the user to quit
                        {
                            game.Exit();
                        }


                        Console.WriteLine("White Wins");
                        game.gameover = 2;
                   }
               } 

               
                    //if not checkmate make normal moves
                
                    for (int i = d; i < e; i++) //loop over friendly pieces
                    {
                        if (game.pieces[i].Clicked == true && game.pieces[i].Taken == false)
                        {
                            if (game.pieces[i].CanMoveTo(game.pieces[i].X, game.pieces[i].Y, x, y)) //check if the current clicked piece can move to the selected space
                            {
                                if (game.board[x, y] == 0) //if the square to move to is blank
                                {
                                    game.board[game.pieces[i].X, game.pieces[i].Y] = 0; //update tentatively
                                    game.board[x, y] = i + 1;
                                    xtemp = game.pieces[i].X;
                                    ytemp = game.pieces[i].Y;
                                    game.pieces[i].X = x;
                                    game.pieces[i].Y = y;



                                    if (Check(game.pieces[kingIndex].X, game.pieces[kingIndex].Y) > 0) //if the result would be a check on your own king reset
                                    {
                                        game.pieces[i].X = xtemp;
                                        game.pieces[i].Y = ytemp;
                                        game.board[game.pieces[i].X, game.pieces[i].Y] = i + 1;
                                        game.board[x, y] = 0;

                                        break;
                                    }

                                    else
                                    {


                                        game.pieces[i].Clicked = false;
                                        if (y == 0 || y == 8)
                                        {
                                            if (game.pieces[i].Type() == 8) // if a pawn reaches the other side promote it to a queen
                                            {
                                                game.pieces[i] = new Queen(game, x, y);
                                            }
                                        }
                                        turn = !turn; //relinquish turn to the opponent
                                        break;
                                    }
                                }

                                else if (!(game.board[x, y] > d && game.board[x, y] < e + 1)  && !((game.pieces[i].Type() == 8) && (game.pieces[i].X == x)))
                                {
                                    game.board[game.pieces[i].X, game.pieces[i].Y] = 0; //if the current square is occupied by an enemy tentatively take
                                    game.pieces[game.board[x, y] - 1].Taken = true;
                                    int g = game.board[x, y];
                                    game.board[x, y] = i + 1;
                                    xtemp = game.pieces[i].X;
                                    ytemp = game.pieces[i].Y;
                                    game.pieces[i].X = x;
                                    game.pieces[i].Y = y;



                                    if (Check(game.pieces[kingIndex].X, game.pieces[kingIndex].Y) > 0)
                                    {
                                        game.pieces[i].X = xtemp; //if the result would be a check on your own king reset
                                        game.pieces[i].Y = ytemp;
                                        game.board[game.pieces[i].X, game.pieces[i].Y] = i + 1;
                                        game.board[x, y] = g;
                                        game.pieces[g - 1].Taken = false;
                                        break;
                                    }

                                    else
                                    {
                                        //game.whitePieces[i].X = x;
                                        //game.whitePieces[i].Y = y;
                                        //game.blackPieces[game.board[x, y] - 17].Taken = true;

                                        game.pieces[i].Clicked = false;
                                        if (y == 0)
                                        {
                                            if (game.pieces[i].Type() == 8) //promote any pawn that reaches the other side with a take
                                            {
                                                game.pieces[i] = new Queen(game, x, y);
                                            }
                                        }
                                        turn = !turn; //relinquish turn to the opponent
                                        break;
                                    }
                                }
                                else
                                {
                                    break; //move not valid the user will have to try something else
                                }

                            }

                        }
                    }
                }
            







                for (int i = 0; i < 32; i++) // update current clicked piece
                {
                    if (game.pieces[i].X == x && game.pieces[i].Y == y) //check if curent clicked coordinated match that of a piece
                    {
                        game.pieces[i].Clicked = true;

                    }
                    else
                    {
                        game.pieces[i].Clicked = false; //set all others to false so only a single piece can be selected at once
                    }
                }
            }
        }
    }

