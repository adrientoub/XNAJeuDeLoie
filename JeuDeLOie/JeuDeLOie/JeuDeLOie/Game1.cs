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

namespace JeuDeLOie
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Plateau plate;
        int tourActuel, nbjoueurs;
        public static Joueur[] joueurs;
        public bool isFirstTimeCharacter;
        Interface interf;

        enum GameState
        {
            Title,
            MainMenu,
            Setting,
            Characters,
            Playing,
        }


        GameState CurrentGameState = GameState.Title;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = GameData.PreferredBackBufferHeight;
            graphics.PreferredBackBufferWidth = GameData.PreferredBackBufferWidth;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            tourActuel = 0;


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameData.Content = Content;
            GameData.SpriteBatch = spriteBatch;
            ContentLoad.Load();
            plate = new Plateau();
            interf = new Interface();
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
            GameData.MouseState = Mouse.GetState();
            GameData.GameTime = gameTime;
            MouseState mouse = Mouse.GetState();
            switch (CurrentGameState)
            {
                case GameState.Title:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        CurrentGameState = GameState.MainMenu;
                    break;

                case GameState.MainMenu:
                    if (ContentLoad.btnPlay.isClicked == true)
                    {
                        CurrentGameState = GameState.Setting;
                    }
                    if (ContentLoad.btnQuit.isClicked == true)
                        Exit();
                    ContentLoad.btnPlay.Update(mouse, gameTime);
                    ContentLoad.btnQuit.Update(mouse, gameTime);
                    break;

                case GameState.Setting:
                    if (ContentLoad.btn2.isClicked)
                    {
                        nbjoueurs = 2;
                        CurrentGameState = GameState.Characters;
                    }
                    if (ContentLoad.btn3.isClicked)
                    {
                        nbjoueurs = 3;
                        CurrentGameState = GameState.Characters;
                    }
                    if (ContentLoad.btn4.isClicked)
                    {
                        nbjoueurs = 4;
                        CurrentGameState = GameState.Characters;
                    }
                    isFirstTimeCharacter = true;

                    ContentLoad.btn2.Update(mouse, gameTime);
                    ContentLoad.btn3.Update(mouse, gameTime);
                    ContentLoad.btn4.Update(mouse, gameTime);
                    break;

                case GameState.Characters:
                    if (isFirstTimeCharacter)
                    {
                        joueurs = new Joueur[nbjoueurs];
                        isFirstTimeCharacter = false;
                        // A supprimer quand on aura fini la selection.
                        for (int i = 0; i < joueurs.Length; i++)
            			{
                            joueurs[i] = new Joueur(i, "conan");
			            } 
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        CurrentGameState = GameState.Playing;
                    break;
                case GameState.Playing:
                    // TODO: Add your update logic here
                    plate.Update();
                    // La boucle de jeu ici :)
                    // modifier le tour actuel et tout

                    foreach (var j in joueurs)
                    {
                        j.Update(tourActuel);
                    }

                    interf.Update();
                    break;
            }
            GameData.PreviousMouseState = GameData.MouseState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Indigo);
            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.Title:
                    spriteBatch.Draw(Content.Load<Texture2D>("Title"), new Rectangle(0, 0, ContentLoad.screenWidth, ContentLoad.screenHeight), Color.White);
                    break;

                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("Menu"), new Rectangle(0, 0, ContentLoad.screenWidth, ContentLoad.screenHeight), Color.White);
                    ContentLoad.btnPlay.Draw(spriteBatch);
                    ContentLoad.btnQuit.Draw(spriteBatch);

                    break;
                case GameState.Setting:
                    ContentLoad.btn2.Draw(spriteBatch);
                    ContentLoad.btn3.Draw(spriteBatch);
                    ContentLoad.btn4.Draw(spriteBatch);
                    break;

                case GameState.Characters:
                    spriteBatch.Draw(Content.Load<Texture2D>("Moogl"), new Rectangle(900, 300, 150, 300), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Mai"), new Rectangle(650, 200, 200, 400), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Crocodile"), new Rectangle(400, 200, 200, 400), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Conan"), new Rectangle(150, 200, 200, 400), Color.White);
                    break;
                case GameState.Playing:
                    interf.Draw();
                    plate.Draw();
                    foreach (var ply in joueurs)
                    {
                        ply.Draw();
                    }
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
