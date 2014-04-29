/*Joseph Tursi
 * Greg Bednarowicz
 * Sadiki Solomon
 * Date: 4/2/2014
 * Purpose: The main game class, holding most logic and drawing code
 * Exceptions:
 */
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

enum InterfaceScreen { MainScreen, PauseScreen, GameScreen }
namespace Lamia_Vittas
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        //Instantiations of all needed objects
        Girl g1;
        Fist f1;
        Cat c1;
        Player p1;
        Button b1;
        Door d1;
        Door d2;
        Spike s1, s2;
        Bush bu1, bu2;

        // list to hold the collectibles
        List<Collectible> coll;

        //states for keyboard and mouse
        KeyboardState kState;
        KeyboardState oldState;
        MouseState mState;

        //List to hold platforms
        List<Platform> platforms;

        //list to hold all objects
        List<GamePiece> allObjects;

        //list to hold all Bounding Boxes
        List<GamePiece> bBs;

        //Platform pl1;

        //Screens for MainScreen and PauseScreen
        MainScreen mainScreen;
        PauseScreen pauseScreen;

        //Textures for the Screens
        Texture2D mainScreenTexture;
        Texture2D startButton;
        Texture2D quitButton;
        Texture2D pauseTexture;
        Texture2D returnToMainMenu;

        // Dictionary to hold the button's cords
        Dictionary<int, int> buttonDict = new Dictionary<int,int>();

        bool GameOver = false;
        InterfaceScreen main;
        bool enableSpace = true;

        int jumpStart;
        int vialsLeft = 0;
        int yarnLeft = 0;

        public Game1()
            : base()
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
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            kState = new KeyboardState();
            oldState = new KeyboardState();
            platforms = new List<Platform>();
            allObjects = new List<GamePiece>();
            coll = new List<Collectible>();
            bBs = new List<GamePiece>();
            mState = new MouseState();
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

            main = InterfaceScreen.MainScreen;

            // TODO: use this.Content to load your game content here

            //Instantiates objects and adds to allObjects List
            g1 = new Girl(new Rectangle(300,200, 96, 128),Content.Load<Texture2D>(GameVariables.girlTexture),1,250,2);
            allObjects.Add(g1);
            f1 = new Fist(new Rectangle(300, 200, 60, 20), Content.Load<Texture2D>(GameVariables.fistTexture), 1);
            allObjects.Add(f1);
            c1 = new Cat(new Rectangle(600,100, 128, 96), Content.Load<Texture2D>(GameVariables.catTexture), 1, 250, 0);
            allObjects.Add(c1);
            s1 = new Spike(new Rectangle(0, 300, 200, 50), Content.Load<Texture2D>(GameVariables.spikeTexture), 1);
            allObjects.Add(s1);
            bu1 = new Bush(new Rectangle(200, 300, 200, 50), Content.Load<Texture2D>(GameVariables.bushTexture), 1);
            allObjects.Add(bu1);
            s2 = new Spike(new Rectangle(775, 250, 75, 25), Content.Load<Texture2D>(GameVariables.spikeTexture), 1);
            allObjects.Add(s2);
            bu2 = new Bush(new Rectangle(400, 300, 200, 50), Content.Load<Texture2D>(GameVariables.bushTexture), 1);
            allObjects.Add(bu2);
            b1 = new Button(new Rectangle(25, 200, 25, 75), Content.Load<Texture2D>(GameVariables.buttonOriginalTexture));
            allObjects.Add(b1);
            d1 = new Door(new Rectangle(650, 150, 25, 150), Content.Load<Texture2D>(GameVariables.doorTexture), b1);
            allObjects.Add(d1);

            // populate the list with collectibles, set the value to false
            // when they get collected (picked up) the value becomes true
            coll.Add(new Collectible((new Rectangle(225, 50, 48, 48)), Content.Load<Texture2D>(GameVariables.vialTexture), false, "vial"));
            coll.Add(new Collectible((new Rectangle(250, 100, 48, 48)), Content.Load<Texture2D>(GameVariables.yarnTexture), false, "yarn"));
            coll.Add(new Collectible((new Rectangle(400, 50, 48, 48)), Content.Load<Texture2D>(GameVariables.vialTexture), false, "vial"));
            coll.Add(new Collectible((new Rectangle(500, 100, 48, 48)), Content.Load<Texture2D>(GameVariables.yarnTexture), false, "yarn"));
            coll.Add(new Collectible((new Rectangle(575, 50, 48, 48)), Content.Load<Texture2D>(GameVariables.vialTexture), false, "vial"));
            coll.Add(new Collectible((new Rectangle(750, 100, 48, 48)), Content.Load<Texture2D>(GameVariables.yarnTexture), false, "yarn"));
            coll.Add(new Collectible((new Rectangle(750, 50, 48, 48)), Content.Load<Texture2D>(GameVariables.vialTexture), false, "vial"));

            foreach (Collectible i in coll)
            {
                if (i.Style == "vial")
                {
                    vialsLeft++;
                }
                else
                {
                    yarnLeft++;
                }
            }

            //creates fonts
            font = Content.Load<SpriteFont>(GameVariables.arialFont);
            
            //creates screens
            mainScreen = new MainScreen(Content.Load<Texture2D>(GameVariables.startButton), Content.Load<Texture2D>(GameVariables.quitButton), Content.Load<Texture2D>(GameVariables.returnToMenu), new Rectangle(200, 200, 97, 33), new Rectangle(100, 200, 97, 33), new Rectangle(300, 200, 100, 100));
            mainScreenTexture = Content.Load<Texture2D>(GameVariables.mainScreen);
            pauseTexture = Content.Load<Texture2D>(GameVariables.pause);
            pauseScreen = new PauseScreen(Content.Load<Texture2D>(GameVariables.startButton), Content.Load<Texture2D>(GameVariables.quitButton), Content.Load<Texture2D>(GameVariables.returnToMenu), new Rectangle(200, 200, 97, 33), new Rectangle(100, 200, 97, 33), new Rectangle(300, 200, 100, 100));
            //returnToMainMenu = Content.Load<Texture2D>(GameVariables.returnToMenu);

            //creates platforms and adds to platforms List
            platforms.Add(new Platform(new Rectangle(0, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(0, 200, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(0, 225, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(0, 250, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(0, 275, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(25, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(50, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(75, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(100, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(125, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(175, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(200, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(225, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(250, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(275, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(300, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(325, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(350, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(375, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(400, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(425, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(450, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(475, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(500, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(525, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(575, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(600, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(625, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(650, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(675, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(700, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(725, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(750, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(775, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(650, 0, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(650, 25, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(650, 50, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(650, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(650, 100, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(650, 125, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            //platforms.Add(new Platform(new Rectangle(0, 100, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));
            //platforms.Add(new Platform(new Rectangle(600, 100, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));

            foreach (Platform i in platforms)
            {//adds all platforms to allObjects List
                allObjects.Add(i);
            }

            foreach (GamePiece i in allObjects)
            {//creates Bounding Boxes for all objects
                bBs.Add(new GamePiece(i.PictureBox,Content.Load<Texture2D>(GameVariables.bBTexture)));
            }

            /*
            for (int i = 1; i < (graphics.GraphicsDevice.Viewport.Width/pl1.Image.Width) - 1; i++)
            {
                platforms.Add(new Platform(new Vector2(((pl1.Image.Width)*(i)),300),Content.Load<Texture2D>("GeneralBLock")));
                platforms.Add(new Platform(new Vector2(((pl1.Image.Width) * (i)), 0), Content.Load<Texture2D>("GeneralBLock")));
            }
             */

            
            //platforms.Add(pl1 = new Platform(new Vector2(-50, 100), Content.Load<Texture2D>("GeneralBLock")));

            p1 = new Player(g1, c1, 0, f1);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //current keyboard state
            kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.Left) && (p1.GetPosition().X >= 0))
            {//moves the player left
                p1.SetDirection(0);
                p1.Move();
            }

            if (kState.IsKeyDown(Keys.Right) && ((p1.GetPosition().X + (p1.GetPosition().Width)) < graphics.GraphicsDevice.Viewport.Width))
            {//moves the player right
                p1.SetDirection(1);
                p1.Move();
            }

            if (kState.IsKeyDown(Keys.Space) /*&& !oldState.IsKeyDown(Keys.Space)*/ && (p1.GetPosition().Y >= 0))
            {//makes the player jump
                if (p1.GetPosition().Y + p1.Image().Height >= jumpStart - p1.JumpHeight())
                {
                p1.Jump(spriteBatch);
                
                }
                    /*
                else
                { 
                    enableSpace = false;
                }
                     */
            }

            /*
            if ((kState.IsKeyDown(Keys.Space) && oldState.IsKeyDown(Keys.Space)) && (enableSpace == false) && (p1.GetPosition().Y + p1.Image().Height <= jumpStart))
            {
                for (int i = 0; i < p1.JumpHeight()/4; i++)
                {
                    p1.Fall(spriteBatch);
                }
            }
             */


            if (kState.IsKeyUp(Keys.Space) && p1.GetPosition().Y < 250)
            {//brings the player down from their jump
                p1.Fall(spriteBatch);
                //enableSpace = true;
            }


            if (kState.IsKeyDown(Keys.LeftShift) && !oldState.IsKeyDown(Keys.LeftShift))
            {//switches from girl to cat and vice versa
                p1.Switch();

            }

            if (kState.IsKeyDown(Keys.R) && oldState.IsKeyDown(Keys.R))
            {//reset the original position of the cat and girl, reset the collectibles
                p1.ResetPosition();
                foreach (Collectible z in coll)
                {
                    z.Collected = false;
                }
                b1.on = false;
            }

            if (kState.IsKeyDown(Keys.P) && p1.state == 0)
            {//punches

                //sets the direction of the fist to the direction of the player
                p1.fist.Direction = p1.GetDirection();

                if (p1.fist.Direction == 0)
                {//sets the fist relative to the location of the player
                    p1.fist.PictureBox = new Rectangle(p1.GetPosition().X - ((15*p1.fist.PictureBox.Width)/32), p1.GetPosition().Y + ((15 * p1.GetPosition().Height) / 32), p1.fist.PictureBox.Width, p1.fist.PictureBox.Height);
                }
                else
                {
                    p1.fist.PictureBox = new Rectangle(p1.GetPosition().X + ((2*p1.GetPosition().Width) / 3), p1.GetPosition().Y + ((15 * p1.GetPosition().Height) / 32), p1.fist.PictureBox.Width, p1.fist.PictureBox.Height);
                }

                //sets the fist to visible
                p1.fist.Punch();
            }

            if (kState.IsKeyUp(Keys.P))
            {//unpunches

                //sets the fist to invisible
                p1.fist.UnPunch();
            }

            //clears all Bounding Boxes
            bBs.Clear();

            foreach (GamePiece i in allObjects)
            {//recreates Bounding Boxes
                bBs.Add(new GamePiece(i.PictureBox, Content.Load<Texture2D>(GameVariables.bBTexture)));
            }

            //sets old state to the current one
            oldState = kState;

            //checks for any collisions
            CheckCollisions();

            //checks for any collections
            CheckCollections();

            //opens door if button is pressed
            d1.Open();

            if (!d1.on)
            {
                if (d1.PictureBox.Y - 5 >= d1.maxHeight)
                {
                    d1.PictureBox = new Rectangle(d1.PictureBox.X,d1.PictureBox.Y - 5,d1.PictureBox.Width,d1.PictureBox.Height);
                    //d1.Draw(spriteBatch);
                }
            }

            
            if (b1.on == true)
            {
                b1 = new Button(b1.PictureBox,Content.Load<Texture2D>(GameVariables.buttonPressedTexture));
                b1.on = true;
                d1.on = false;
            }
            else
            {
                b1 = new Button(b1.PictureBox, Content.Load<Texture2D>(GameVariables.buttonOriginalTexture));
            }

            // checks if the game is over
            if (p1.Lives <= 0)
            {
                GameOver = true;
                System.Threading.Thread.Sleep(1000);
                Exit();
            }

           
                //Gets the mouse's state.
                mState = Mouse.GetState();
                for (int r = 200; r <= 297; r++)
                {
                    for (int c = 200; c <= 233; c++)
                    {
                        if ((mState.X == r) && (mState.Y == c) && (mState.LeftButton == ButtonState.Pressed))
                        {
                            // Changes the Enum to GameScreen
                            main = InterfaceScreen.GameScreen;
                            // Calls the draw method
                            Draw(gameTime);
                            r = 297;
                            c = 233;
                        }
                    }
                }
            

            if (main == InterfaceScreen.GameScreen)
            {
                for (int r = 770; r <= 870; r++)
                {
                    for (int c = 450; c <= 550; c++)
                    {
                        if ((mState.X == r) && (mState.Y == c) && (mState.LeftButton == ButtonState.Pressed))
                        {
                            main = InterfaceScreen.PauseScreen;

                            // Calls the draw method
                            Draw(gameTime);

                            r = 870;
                            c = 550;
                        }

                    }
                }
            }

            if (main == InterfaceScreen.MainScreen)
            {
                for (int r = 100; r <= 197; r++)
                {
                    for (int c = 200; c <= 233; c++)
                    {
                        if ((mState.X == r) && (mState.Y == c) && (mState.LeftButton == ButtonState.Pressed))
                        {
                            // Closes the Window
                            Exit();
                        }
                    }
                }
            }


            if (main == InterfaceScreen.PauseScreen)
            {
                for (int r = 300; r <= 400; r++)
                {
                    for (int c = 200; c <= 300; c++)
                    {
                        if ((mState.X == r) && (mState.Y == c) && (mState.LeftButton == ButtonState.Pressed))
                        {
                            main = InterfaceScreen.MainScreen;

                            // Calls the draw method
                            Draw(gameTime);
                            r = 400;
                            c = 500;
                        }

                    }
                }
            }

          
            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            /*
            if (p1.GetDirection() == 0)
            {//flips her to the left if she is moving left
                p1.Draw(spriteBatch);
            }
            else if(p1.GetDirection() == 1)
            {//draws her normally if going right
                p1.Draw(spriteBatch);
            }
             */

            if (main == InterfaceScreen.MainScreen)
            {
                spriteBatch.Draw(mainScreenTexture, new Vector2(0, 0), Color.White);
                mainScreen.Draw(spriteBatch);
                
            }

            if (main == InterfaceScreen.GameScreen)
            {
                //draws the player
                p1.Draw(spriteBatch);

                if (p1.fist.Visible)
                {//draws the fist if the fist is visible
                    p1.fist.Draw(spriteBatch);
                }

                for (int i = 0; i < platforms.Count; i++)
                {//draws all platforms
                    platforms[i].Draw(spriteBatch);
                }

                //draws obstacles
                s1.Draw(spriteBatch);
                s2.Draw(spriteBatch);
                bu1.Draw(spriteBatch);
                bu2.Draw(spriteBatch);
                b1.Draw(spriteBatch);
                d1.Draw(spriteBatch);

                foreach (GamePiece i in bBs)
                {//draws all Bounding Boxes
                    i.Draw(spriteBatch);
                }

                // draw collectibles
                for (int y = 0; y < coll.Count; y++)
                {
                    if (coll[y].Collected == false)
                    {
                        coll[y].Draw(spriteBatch);
                    }
                }
                // draws all bounding boxes
                foreach (GamePiece i in bBs)
                {//draws all Bounding Boxes
                    i.Draw(spriteBatch);
                }
                // draw the Debug info
                spriteBatch.DrawString(font,
                    p1.GetPosition()
                    + "\nP1 Top:       " + p1.GetPosition().Top
                    + "\nP1 Bottom:  " + p1.GetPosition().Bottom                   
                    + "\nP1 Left:       " + p1.GetPosition().Left
                    + "\nP1 Right:     " + p1.GetPosition().Right
                    + "\nHealth:        " + p1.Health
                    + "\nLives:         " + p1.Lives
                    + "\nGame Over: " + GameOver
                    + "\n\nLeft arrow - move left     Right arrom - more right     Shift - switch states     Space - jump/hover     R - respawn", new Vector2(0, 300), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);

                spriteBatch.DrawString(font, "Vials left: " + vialsLeft + " Yarn Left: " + yarnLeft, new Vector2(10, 10), Color.Black);

                // Draws the pause texture
                spriteBatch.Draw(pauseTexture, new Vector2(770, 450), Color.White);
            }
            if (main == InterfaceScreen.PauseScreen)
            {
                spriteBatch.Draw(mainScreenTexture, new Vector2(0, 0), Color.White);
                pauseScreen.Draw(spriteBatch);

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        protected void CheckCollisions()
        {
            foreach (Platform plat in platforms)
            {
                if (p1.GetPosition().Intersects(plat.PictureBox))
                {
                    // determines if player is on top of a block
                    if ((p1.GetPosition().Top < plat.PictureBox.Top) && 
                        (p1.GetPosition().Center.Y < plat.PictureBox.Top) && 
                        (p1.GetPosition().Bottom > plat.PictureBox.Top) && 
                        (p1.GetPosition().Center.X >= plat.PictureBox.Left && p1.GetPosition().Center.X <= plat.PictureBox.Right))
                    {
                        p1.SetPosition(new Rectangle(p1.GetPosition().X, plat.PictureBox.Top - (p1.GetPosition().Height), p1.GetPosition().Width, p1.GetPosition().Height));
                    }

                    // determines if player is to the left of a block
                    if ((p1.GetPosition().Top < plat.PictureBox.Bottom) &&
                        (p1.GetPosition().Center.Y < plat.PictureBox.Bottom && p1.GetPosition().Y > plat.PictureBox.Top) &&
                        (p1.GetPosition().Bottom <= plat.PictureBox.Bottom && p1.GetPosition().Bottom >= plat.PictureBox.Top) &&
                        (p1.GetPosition().Left < plat.PictureBox.Left) &&
                        (p1.GetPosition().Center.X < plat.PictureBox.Left) &&
                        (p1.GetPosition().Right > plat.PictureBox.Left))
                    {
                        p1.SetPosition(new Rectangle(plat.PictureBox.Left - p1.GetPosition().Width, p1.GetPosition().Y, p1.GetPosition().Width, p1.GetPosition().Height));
                    }

                    // determines if player is to the right of a block
                    if ((p1.GetPosition().Top < plat.PictureBox.Bottom) &&
                        (p1.GetPosition().Center.Y < plat.PictureBox.Bottom && p1.GetPosition().Y > plat.PictureBox.Top) &&
                        (p1.GetPosition().Bottom <= plat.PictureBox.Bottom && p1.GetPosition().Bottom >= plat.PictureBox.Top) &&
                        (p1.GetPosition().Left < plat.PictureBox.Right) &&
                        (p1.GetPosition().Center.X > plat.PictureBox.Right) &&
                        (p1.GetPosition().Right > plat.PictureBox.Right))
                    {
                        p1.SetPosition(new Rectangle(plat.PictureBox.Right, p1.GetPosition().Y, p1.GetPosition().Width, p1.GetPosition().Height));
                    }
                }

                if (p1.fist.IsColliding(b1))
                {//determines if button it pushed
                    b1.on = true;
                }

                if ((p1.GetPosition().X < d1.PictureBox.Right && p1.GetPosition().X > d1.PictureBox.Left) && p1.IsColliding(d1))
                {//determines if player colliding with door from left
                    p1.SetPosition(new Rectangle(p1.GetPosition().X + 1, p1.GetPosition().Y, p1.GetPosition().Width,p1.GetPosition().Height));
                }

                if ((p1.GetPosition().X + p1.GetPosition().Width > d1.PictureBox.Left && p1.GetPosition().X + p1.GetPosition().Width < d1.PictureBox.Right) && p1.IsColliding(d1))
                {//determines if player colliding with door from right
                    p1.SetPosition(new Rectangle(p1.GetPosition().X - 1, p1.GetPosition().Y, p1.GetPosition().Width, p1.GetPosition().Height));
                }
            }

            /*
            for (int i = 0; i < platforms.Count; i++)
            {
                if (p1.GetPosition().Intersects(platforms[i].PictureBox))
                {    
                    (
                    // top of the block
                    //if ((p1.GetPosition().Y + p1.GetPosition().Height > platforms[i].PictureBox.Y));/* && ((p1.GetPosition().X + p1.GetPosition().Width < platforms[i].PictureBox.X + platforms[i].PictureBox.Width) && (p1.GetPosition().X > platforms[i].PictureBox.X)))*/
                    //{
                        //p1.SetPosition(new Rectangle(p1.GetPosition().X, platforms[i].PictureBox.Y - p1.GetPosition().Height, p1.GetPosition().Width, p1.GetPosition().Height));
                    //}
                    
                    /* If all else fails USE THIS
                     * 
                     * 
                     * 
                     * 
                    //  top of a block
                    if ((p1.GetPosition().Bottom > platforms[i].PictureBox.Top) && (p1.GetPosition().Left > platforms[i].PictureBox.Left - platforms[i].PictureBox.Width / 2 && p1.GetPosition().Right < platforms[i].PictureBox.Right + platforms[i].PictureBox.Width / 2))
                    {
                        p1.SetPosition(new Rectangle(p1.GetPosition().X, platforms[i].PictureBox.Top - p1.GetPosition().Height, p1.GetPosition().Width, p1.GetPosition().Height));
                    }

                    // right of a block
                    if ((p1.GetPosition().Left < platforms[i].PictureBox.Right) && !(p1.GetPosition().Bottom >= platforms[i].PictureBox.Top))
                    {
                        p1.SetPosition(new Rectangle(platforms[i].PictureBox.Right, p1.GetPosition().Bottom, p1.GetPosition().Width, p1.GetPosition().Height));
                    }
                     */
                    
                        
                        /*
                    // top failure
                    if (p1.GetPosition().Y - GameVariables.girlJump < 0)
                    {
                        p1.SetPosition(new Vector2(p1.GetPosition().X, 0 + p1.Image().Height));
                        
                    }

                    // collision detection for top of blocks
                    if ((p1.GetPosition().Y + p1.Image().Bounds.Width > platforms[i].Position.Y) && ((p1.GetPosition().X + p1.Image().Width < platforms[i].Position.X + platforms[i].BoundingBox.Width) && (p1.GetPosition().X > platforms[i].Position.X)))
                    {
                        p1.SetPosition(new Vector2(p1.GetPosition().X, (platforms[i].Position.Y) - p1.Image().Height));
                    }                                    

                    // right of a block
                    if ((p1.GetPosition().X < platforms[i].Position.X + platforms[i].BoundingBox.Width) && (p1.GetPosition().Y + p1.Image().Height > platforms[i].Position.Y))
                    {
                        p1.SetPosition(new Vector2(platforms[i].Position.X + platforms[i].BoundingBox.Width, p1.GetPosition().Y));
                    }

                    
                    // left of a block
                    if ((p1.GetPosition().X + p1.Image().Bounds.Width > platforms[i].Position.X) && (p1.GetPosition().Y + p1.Image().Height > platforms[i].Position.Y))
                    {
                        p1.SetPosition(new Vector2(platforms[i].Position.X, p1.GetPosition().Y));
                    }
                    
                    /*
                    string side = p1.Side(platforms[i]);
                    
                    switch (side)
                    {
                        case "bottom":
                            p1.SetPosition(new Vector2(p1.GetPosition().X, platforms[i].Position.Y + platforms[i].Image.Height));
                            break;
                        case "top":
                            p1.SetPosition(new Vector2(p1.GetPosition().X, platforms[i].Position.Y - p1.Image().Height));
                            break;
                        case "right":
                            p1.SetPosition(new Vector2(platforms[i].Position.X - p1.Image().Width, p1.GetPosition().Y));
                            break;
                        case "left":
                            p1.SetPosition(new Vector2(platforms[i].Position.X, p1.GetPosition().Y));
                            break;
                        default:
                            break;
                    }
                     */

                    //jumpStart = (int)p1.GetPosition().Y + p1.Image().Height;
                //}
            //}

            
            if (p1.GetPosition().Intersects(s2.PictureBox))
            {// check for collisions between spikes and character

                // damage the player
                s2.DamagePlayerSpike(p1);
            }

            if (p1.GetPosition().Intersects(bu1.PictureBox))
            {// check for collisions between spikes and character

                // damage the player
                bu1.DamagePlayerBush(p1);
            }
             
             
            
                // check for collisions between bush and character
        }

        /// <summary>
        /// Check if the character collected a collectible
        /// </summary>
        public void CheckCollections()
        {
            foreach (Collectible z in coll)
            {
                if (p1.state == 0 && p1.IsColliding(z) && z.Collected==false && z.Style == "vial")
                {
                    z.Collected = true;
                    vialsLeft--;
                }

                if (p1.state == 1 && p1.IsColliding(z) && z.Collected == false && z.Style == "yarn")
                {
                    z.Collected = true;
                    yarnLeft--;
                }
            }
        }
    }
}
