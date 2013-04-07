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
        public static int tourActuel, nbjoueurs;
        public static Joueur[] joueurs;
        public bool isFirstTimeCharacter;
        Interface interf;
        // Sons
        Song bruitdefond;
        Song ambiancejeu;
        SoundEffect mai;
        SoundEffect conan1;
        SoundEffect croco;
        SoundEffect moogle;
        Song ambiancejeuentiere;

        Rectangle crocorec;
        Rectangle conanrec;
        Rectangle mairec;
        Rectangle mooglerec;
        int compteperso;
        string choixjoueur;

        public enum GameState
        {
            Title,
            MainMenu,
            Setting,
            Characters,
            Playing,
            Victory,
            Recette,
        }


        public static GameState CurrentGameState;

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
            CurrentGameState = GameState.Victory;
            // TODO: Add your initialization logic here

            tourActuel = 0;
            crocorec = new Rectangle(400, 300, 200, 400);
            conanrec = new Rectangle(150, 300, 200, 400);
            mairec = new Rectangle(700, 300, 200, 400);
            mooglerec = new Rectangle(1000, 400, 150, 300);
            choixjoueur = "";

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

            // Sons
            bruitdefond = Content.Load<Song>("Sons/debut");
            ambiancejeu = Content.Load<Song>("Sons/jeu");
            mai = Content.Load<SoundEffect>("Sons/mai1");
            conan1 = Content.Load<SoundEffect>("Sons/conan1");
            croco = Content.Load<SoundEffect>("Sons/croco1");
            moogle = Content.Load<SoundEffect>("Sons/moogle1");
            ambiancejeuentiere = Content.Load<Song>("Sons/jeu1");
            MediaPlayer.Play(bruitdefond);
            MediaPlayer.IsRepeating = true;

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
            GameData.presentKey = Keyboard.GetState();
            switch (CurrentGameState)
            {
                case GameState.Title:
                    if ((GameData.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && GameData.PreviousMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                        CurrentGameState = GameState.MainMenu;
                    break;

                case GameState.MainMenu:
                    if (ContentLoad.btnPlay.isClicked == true)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(ambiancejeu);
                        CurrentGameState = GameState.Setting;
                    }
                    if (ContentLoad.btnQuit.isClicked == true)
                        Exit();
                    ContentLoad.btnPlay.Update(gameTime);
                    ContentLoad.btnQuit.Update(gameTime);
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

                    ContentLoad.btn2.Update(gameTime);
                    ContentLoad.btn3.Update(gameTime);
                    ContentLoad.btn4.Update(gameTime);
                    break;

                case GameState.Characters:
                    if (isFirstTimeCharacter)
                    {
                        joueurs = new Joueur[nbjoueurs];
                        isFirstTimeCharacter = false; 
                        compteperso = 0;
                    }


                    if (conanrec.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)) && (GameData.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && GameData.PreviousMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed))
                    {
                        conan1.Play();
                        joueurs[compteperso] = new Joueur(compteperso, "Conan");
                        compteperso++;
                        conanrec = new Rectangle(0, 0, 0, 0);
                    }
                    else if (crocorec.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)) && (GameData.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && GameData.PreviousMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed))
                    {
                        croco.Play();
                        joueurs[compteperso] = new Joueur(compteperso, "Crocodile");
                        compteperso++;
                        crocorec = new Rectangle(0, 0, 0, 0);
                    }

                    else if (mairec.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)) && (GameData.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && GameData.PreviousMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed))
                    {
                        mai.Play();
                        joueurs[compteperso] = new Joueur(compteperso, "Mai");
                        compteperso++;
                        mairec = new Rectangle(0, 0, 0, 0);
                    }
                    else if (mooglerec.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)) && (GameData.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && GameData.PreviousMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed))
                    {
                        moogle.Play();
                        joueurs[compteperso] = new Joueur(compteperso, "Moogle");
                        compteperso++;
                        mooglerec = new Rectangle(0, 0, 0, 0);
                    }
                    if (compteperso == nbjoueurs)
                    {
                        CurrentGameState = GameState.Playing;

                        MediaPlayer.Stop();
                        MediaPlayer.Play(ambiancejeuentiere);
                        MediaPlayer.IsRepeating = true;
                    }
                    else if (compteperso == 0)
                        choixjoueur = "Joueur1, veuillez sélectonner votre personnage";
                    else if (compteperso == 1)
                        choixjoueur = "C'est au Joueur2 de choisir son personnage";
                    else if (compteperso == 2)
                        choixjoueur = "C'est au Joueur3 de choisir son personnage";
                    else if (compteperso == 3)
                        choixjoueur = "C'est au Joueur4 de choisir son personnage";



                    interf.InitDonneesGen(); // après avoir sélectionné les joueurs, on initialise les données de l'interface de jeu
                    break;
                case GameState.Playing:
                    // TODO: Add your update logic here
                    plate.Update();
                    // La boucle de jeu ici :)
                    // modifier le tour actuel et tout

                    foreach (var j in joueurs)
                    {
                        if (j.Update(tourActuel))
                        {
                            // Ajouter un message et changer le tour
                            tourActuel++;
                        }
                    }

                    interf.Update();
                    break;
                case GameState.Victory:
                    if (GameData.presentKey.IsKeyDown(Keys.Enter) && GameData.pastKey.IsKeyUp(Keys.Enter))
                        CurrentGameState = GameState.Recette;
                    break;
                case GameState.Recette:
                    if (GameData.presentKey.IsKeyDown(Keys.Enter) && GameData.pastKey.IsKeyUp(Keys.Enter))
                        this.Exit();
                    break;
            }
            GameData.pastKey = GameData.presentKey;
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
                    spriteBatch.Draw(Content.Load<Texture2D>("ChoixNbJoueurs"), new Rectangle(0, 0, ContentLoad.screenWidth, ContentLoad.screenHeight), Color.White);
                    ContentLoad.btn2.Draw(spriteBatch);
                    ContentLoad.btn3.Draw(spriteBatch);
                    ContentLoad.btn4.Draw(spriteBatch);
                    break;

                case GameState.Characters:
                    spriteBatch.Draw(Content.Load<Texture2D>("SelectionPerso"), new Rectangle(0, 0, ContentLoad.screenWidth, ContentLoad.screenHeight), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Personnages/Moogle"), mooglerec, Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Personnages/Mai"), mairec, Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Personnages/Crocodile"), crocorec, Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Personnages/Conan"), conanrec, Color.White);
                    GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, choixjoueur, new Vector2(GameData.PreferredBackBufferWidth / 2 - 125, 100), Color.Black);
                    break;
                case GameState.Playing:
                    spriteBatch.Draw(Content.Load<Texture2D>("Ciel"), new Rectangle(0, 0, ContentLoad.screenWidth, ContentLoad.screenHeight), Color.White);
                    interf.Draw();
                    plate.Draw();
                    foreach (var ply in joueurs)
                    {
                        ply.Draw();
                    }
                    break;
                case GameState.Victory:
                    spriteBatch.Draw(Content.Load<Texture2D>("Victoire"), new Rectangle(0, 0, ContentLoad.screenWidth, ContentLoad.screenHeight), Color.White);
                    break; 
                case GameState.Recette:
                    spriteBatch.Draw(Content.Load<Texture2D>("Recette"), new Rectangle(0, 0, ContentLoad.screenWidth, ContentLoad.screenHeight), Color.White);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
