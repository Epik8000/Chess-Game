using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        MovementHandler movementHandler;
        int width;
        public int gameover;
        public bool turn;
        public int clicked;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public Piece[] pieces = new Piece[32];
        public int[,] board = new int[8, 8];

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int width = Math.Min(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            for (int i = 0; i < 8; i++) //define pieces and initial positions
            {
                //Rectangle position = new Rectangle(0 + width / 8 * i, width / 8, width / 8, width / 8); 
                //Rectangle position2 = new Rectangle(0 + width / 8 * i, width * 6/ 8, width/8, width/ 8);
                pieces[i + 16] = new Pawn(this, i ,1);
                pieces[i] = new Pawn(this, i, 6);

            }

            pieces[8] = new King(this, 4, 7);
            pieces[9] = new Queen(this, 3, 7);
            pieces[10] = new Rook(this, 0, 7);
            pieces[11] = new Rook(this, 7, 7);
            pieces[12] = new Knight(this, 1, 7);
            pieces[13] = new Knight(this, 6, 7);
            pieces[14] = new Bishop(this, 2, 7);
            pieces[15] = new Bishop(this, 5, 7);

            pieces[24] = new King(this, 4, 0);
            pieces[25] = new Queen(this, 3, 0);
            pieces[26] = new Rook(this, 0, 0);
            pieces[27] = new Rook(this, 7, 0);
            pieces[28] = new Knight(this, 1, 0);
            pieces[29] = new Knight(this, 6, 0);
            pieces[30] = new Bishop(this, 2, 0);
            pieces[31] = new Bishop(this, 5, 0);

            this.IsMouseVisible = true;
            board = new int[8, 8];
            turn = false;
            gameover = 0;
            movementHandler = new MovementHandler(this);
            //Components.Add(movementHandler);
            base.Initialize();
        }

        Texture2D background;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Chess.board.fabric"); 


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        
        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here
            movementHandler.Update(gameTime, width, ref turn); //calls movementhandler on every update

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param> 
        /// 

        public void newgame() //call to start a new game
        {
            Initialize(); 

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            width = Math.Min(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            Rectangle pos = new Rectangle(0,0,width,width);
            spriteBatch.Draw(background, pos, Color.White); //draw background
            // TODO: Add your drawing code here 
            for (int i = 0; i < 16; i++) //draw white piece
            {
                if (pieces[i].Taken == true) //if taken don't draw
                {
                    pieces[i].DrawPiece(spriteBatch, 2, width, false);
                }


                else if (pieces[i].Clicked == true) //highlight yellow if clicked
                {
                    pieces[i].DrawPiece(spriteBatch, 0, width,true);
                    board[pieces[i].X, pieces[i].Y] = i + 1;
                }
                else
                {
                    pieces[i].DrawPiece(spriteBatch, 0, width,false);
                    board[pieces[i].X, pieces[i].Y] = i + 1;
                }

            }

            for (int i = 16; i < 32; i++) //draw black pieces
            {
                if (pieces[i].Taken == true) //if taken don't draw
                {
                    pieces[i].DrawPiece(spriteBatch, 2, width, false);
                }


                else if (pieces[i].Clicked == true) //highlight yellow if clicked
                {
                    pieces[i].DrawPiece(spriteBatch, 3, width, true);
                    board[pieces[i].X, pieces[i].Y] = i + 1;
                }
                else
                {
                    pieces[i].DrawPiece(spriteBatch, 3, width, false);
                    board[pieces[i].X, pieces[i].Y] = i + 1;
                }

            }
            //whitePieces[8].DrawPiece(spriteBatch, 0, width, false);
            //blackPieces[8].DrawPiece(spriteBatch, 1, width, false);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
