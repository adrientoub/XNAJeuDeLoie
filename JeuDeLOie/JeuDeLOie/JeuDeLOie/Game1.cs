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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Plateau plate;
        int tourActuel, nbjoueurs;
        public static Joueur[] joueurs;
        Dices des;
        Interface interf;

        int screenWidth, screenHeight;
        enum GameState
        {
            Title,
            MainMenu,
            Setting,
            Characters,
            Playing,
        }


        cButton btnPlay, btnQuit, btn2, btn3, btn4;

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
            joueurs = new Joueur[4];
            for (int i = 0; i < joueurs.Length; i++)
            {
                joueurs[i] = new Joueur(i, "");
            }

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
            des = new Dices(new Rectangle(GameData.PreferredBackBufferWidth - 75 * 2 - 60, (int)interf.Position.Y + 347 + 20, 75, 75));
            // TODO: use this.Content to load your game content here

            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;

            //Button
            btnPlay = new cButton(Content.Load<Texture2D>("PlayButton"), graphics.GraphicsDevice, 100, 75);
            btnPlay.setPosition(new Vector2(screenWidth / 2 - btnPlay.size.X / 2, screenHeight / 2 - 150));
            btnQuit = new cButton(Content.Load<Texture2D>("QuitButton"), graphics.GraphicsDevice, 100, 75);
            btnQuit.setPosition(new Vector2(screenWidth / 2 - btnQuit.size.X / 2, screenHeight / 2 - 50));
            btn2 = new cButton(Content.Load<Texture2D>("Button2"), graphics.GraphicsDevice, 100, 75);
            btn2.setPosition(new Vector2(screenWidth / 2 - btn2.size.X / 2, screenHeight / 2 - 50));
            btn3 = new cButton(Content.Load<Texture2D>("Button3"), graphics.GraphicsDevice, 100, 75);
            btn3.setPosition(new Vector2(screenWidth / 2 - btn3.size.X / 2, screenHeight / 2 - 250));
            btn4 = new cButton(Content.Load<Texture2D>("Button4"), graphics.GraphicsDevice, 100, 75);
            btn4.setPosition(new Vector2(screenWidth / 2 - btn4.size.X / 2, screenHeight / 2 + 50));

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
                    if (btnPlay.isClicked == true)
                    {
                        CurrentGameState = GameState.Setting;
                    }
                    if (btnQuit.isClicked == true)
                        Exit();
                    btnPlay.Update(mouse, gameTime);
                    btnQuit.Update(mouse, gameTime);
                    break;

                case GameState.Setting:
                    if (btn2.isClicked)
                    {
                        nbjoueurs = 2;
                        CurrentGameState = GameState.Characters;
                    }
                    if (btn3.isClicked)
                    {
                        nbjoueurs = 3;
                        CurrentGameState = GameState.Characters;
                    }
                    if (btn4.isClicked)
                    {
                        nbjoueurs = 4;
                        CurrentGameState = GameState.Characters;
                    }

                    btn2.Update(mouse, gameTime);
                    btn3.Update(mouse, gameTime);
                    btn4.Update(mouse, gameTime);
                    break;

                case GameState.Characters:
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

                    des.Update();
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
                    spriteBatch.Draw(Content.Load<Texture2D>("Title"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    break;

                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("Menu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    btnQuit.Draw(spriteBatch);

                    break;
                case GameState.Setting:
                    btn2.Draw(spriteBatch);
                    btn3.Draw(spriteBatch);
                    btn4.Draw(spriteBatch);
                    break;

                case GameState.Characters:
                    spriteBatch.Draw(Content.Load<Texture2D>("Moogl"), new Rectangle(900, 300, 150, 300), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Mai"), new Rectangle(650, 200, 200, 400), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Crocodile"), new Rectangle(400, 200, 200, 400), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Conan"), new Rectangle(150, 200, 200, 400), Color.White);
                    break;
                case GameState.Playing:
                    interf.Draw();
                    //   des.Draw();
                    plate.Draw();
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
