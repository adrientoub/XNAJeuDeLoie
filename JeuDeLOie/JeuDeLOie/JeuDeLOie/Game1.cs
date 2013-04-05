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
        int tourActuel;
        public static Joueur[] joueurs;
        Dices des;
        Interface interf;

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
            des = new Dices(new Rectangle(GameData.PreferredBackBufferWidth - GameData.PreferredBackBufferWidth / 4,
                GameData.PreferredBackBufferHeight - GameData.PreferredBackBufferHeight / 4, 75, 75));
            
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

            // TODO: Add your update logic here
            plate.Update();
            // La boucle de jeu ici :)
            // modifier le tour actuel et tout

            foreach (var j in joueurs)
            {
                j.Update(tourActuel);
            }

            des.Update();

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
            plate.Draw();
            des.Draw();
            interf.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
