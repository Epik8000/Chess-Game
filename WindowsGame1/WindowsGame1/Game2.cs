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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public Piece[] blackPieces = new Piece[16];
        public Piece[] whitePieces = new Piece[16];
        public int[,] board = new int[8, 8];

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int width = Math.Min(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            for (int i = 0; i < 8; i++)
            {
                //Rectangle position = new Rectangle(0 + width / 8 * i, width / 8, width / 8, width / 8); 
                //Rectangle position2 = new Rectangle(0 + width / 8 * i, width * 6/ 8, width/8, width/ 8);
                blackPieces[i] = new Pawn(this, i ,1);
                whitePieces[i] = new Pawn(this, i, 6);
                whitePieces[8] = new King(this, 4, 7);
                whitePieces[9] = new Queen(this, 3, 7);
                blackPieces[8] = new King(this, 4, 0);
                blackPieces[9] = new Queen(this, 3, 0);
                whitePieces[10] = new Rook(this, 0, 7);
                whitePieces[11] = new Rook(this, 7, 7);
                blackPieces[10] = new Rook(this, 0, 0);
                blackPieces[11] = new Rook(this, 7, 0);
                whitePieces[12] = new Knight(this, 1, 7);
                whitePieces[13] = new Knight(this, 6, 7);
                blackPieces[12] = new Knight(this, 1, 0);
                blackPieces[13] = new Knight(this, 6, 0);
                whitePieces[14] = new Bishop(this, 2, 7);
                whitePieces[15] = new Bishop(this, 5, 7);
                blackPieces[14] = new Bishop(this, 2, 0);
                blackPieces[15] = new Bishop(this, 5, 0);
            }
            this.IsMouseVisible = true;
            board = new int[8, 8];
            turn = false;
            gameover = 0;
            movementHandler = new MovementHandler(this);
            //Components.Add(movementHandler);
            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        Texture2D background;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Chess.board.fabric"); 

            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            movementHandler.Update(gameTime, width, ref turn);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param> 
        /// 

        public void newgame()
        {
            Initialize();

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            width = Math.Min(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            Rectangle pos = new Rectangle(0,0,width,width);
            spriteBatch.Draw(background, pos, Color.White);
            // TODO: Add your drawing code here 
            for (int i = 0; i < 16; i++)
            {
                if (whitePieces[i].Taken == true)
                {
                    whitePieces[i].DrawPiece(spriteBatch, 2, width, false);
                }


                else if (whitePieces[i].Clicked == true)
                {
                    whitePieces[i].DrawPiece(spriteBatch, 0, width,true);
                    board[whitePieces[i].X, whitePieces[i].Y] = i + 1;
                }
                else
                {
                    whitePieces[i].DrawPiece(spriteBatch, 0, width,false);
                    board[whitePieces[i].X, whitePieces[i].Y] = i + 1;
                }

                if (blackPieces[i].Taken == true)
                {
                    blackPieces[i].DrawPiece(spriteBatch, 2, width, false);
                }
                else if (blackPieces[i].Clicked == true)
                {
                    blackPieces[i].DrawPiece(spriteBatch, 1, width, true);
                    board[blackPieces[i].X, blackPieces[i].Y] = i + 17;
                }
                else
                {
                    blackPieces[i].DrawPiece(spriteBatch, 1, width,false);
                    board[blackPieces[i].X, blackPieces[i].Y] = i + 17;
                }
            }
            //whitePieces[8].DrawPiece(spriteBatch, 0, width, false);
            //blackPieces[8].DrawPiece(spriteBatch, 1, width, false);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
