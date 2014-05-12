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

        Girl g1;
        Cat c1;
        Player p1;
        KeyboardState kState;
        KeyboardState oldState;
        MouseState mState;

        List<Platform> platforms;
        Platform pl1;
        Button b1;
        Door d1;
        Spike s1, s2;
        Bush bu1, bu2;

        //jumping
        bool jumping;
        int startY;
        int jumpSpeed;

        MainScreen mainScreen;
        PauseScreen pauseScreen;

        Texture2D mainScreenTexture;
        Texture2D startButton;
        Texture2D quitButton;
        Texture2D pauseTexture;
        Texture2D returnToMainMenu;

        // Dictionary to hold the button's cords
        Dictionary<int, int> buttonDict = new Dictionary<int,int>();

        bool GameOver = false;
        InterfaceScreen main;

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
            mState = new MouseState();
            base.Initialize();

            startY = p1.GetPosition().Y;//Starting position
            jumping = false;//Init jumping to false
            jumpSpeed = 0;//Default no speed
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
            g1 = new Girl(new Rectangle(300,172, 96, 128),Content.Load<Texture2D>(GameVariables.girlTexture),1,250,2);
            c1 = new Cat(new Rectangle(600,100, 128, 96), Content.Load<Texture2D>(GameVariables.catTexture), 1, 250, 0);
            s1 = new Spike(new Rectangle(0, 300, 200, 50), Content.Load<Texture2D>(GameVariables.spikeTexture), 1);
            bu1 = new Bush(new Rectangle(200, 300, 200, 50), Content.Load<Texture2D>(GameVariables.bushTexture), 1);
            s2 = new Spike(new Rectangle(400, 300, 200, 50), Content.Load<Texture2D>(GameVariables.spikeTexture), 1);
            bu2 = new Bush(new Rectangle(600, 300, 200, 50), Content.Load<Texture2D>(GameVariables.bushTexture), 1);

            font = Content.Load<SpriteFont>(GameVariables.arialFont);
            mainScreen = new MainScreen(Content.Load<Texture2D>(GameVariables.startButton), Content.Load<Texture2D>(GameVariables.quitButton), Content.Load<Texture2D>(GameVariables.returnToMenu), new Rectangle(200, 200, 97, 33), new Rectangle(100, 200, 97, 33), new Rectangle(300, 200, 100, 100));
            mainScreenTexture = Content.Load<Texture2D>(GameVariables.mainScreen);
            pauseTexture = Content.Load<Texture2D>(GameVariables.pause);
            pauseScreen = new PauseScreen(Content.Load<Texture2D>(GameVariables.startButton), Content.Load<Texture2D>(GameVariables.quitButton), Content.Load<Texture2D>(GameVariables.returnToMenu), new Rectangle(200, 200, 97, 33), new Rectangle(100, 200, 97, 33), new Rectangle(300, 200, 100, 100));
            //returnToMainMenu = Content.Load<Texture2D>(GameVariables.returnToMenu);

            platforms.Add(new Platform(new Rectangle(0, 300, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(200, 300, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(400, 300, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(600, 300, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(0, 100, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(600, 100, 200, 200), Content.Load<Texture2D>(GameVariables.blockTexture)));
            /*
            for (int i = 1; i < (graphics.GraphicsDevice.Viewport.Width/pl1.Image.Width) - 1; i++)
            {
                platforms.Add(new Platform(new Vector2(((pl1.Image.Width)*(i)),300),Content.Load<Texture2D>("GeneralBLock")));
                platforms.Add(new Platform(new Vector2(((pl1.Image.Width) * (i)), 0), Content.Load<Texture2D>("GeneralBLock")));
            }
             */

            
            //platforms.Add(pl1 = new Platform(new Vector2(-50, 100), Content.Load<Texture2D>("GeneralBLock")));

            p1 = new Player(g1, c1, 0);
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

            kState = Keyboard.GetState();

            //Physics.upDateY(p1.GamePiece(), worldClock, spriteBatch);

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

            if (jumping)
            {
                p1.SetPosition(new Rectangle(p1.GetPosition().X,p1.GetPosition().Y + jumpSpeed,p1.GetPosition().Width,p1.GetPosition().Height));//Making it go up
                jumpSpeed += 1;//Some math (explained later)
                if (p1.GetPosition().Y >= startY)
                //If it's farther than ground
                {
                    p1.SetPosition(new Rectangle(p1.GetPosition().X, startY, p1.GetPosition().Width, p1.GetPosition().Height)); ;//Then set it on
                    jumping = false;
                }
            }
            else
            {
                if (kState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                {
                    startY = p1.GetPosition().Y;
                    jumping = true;
                    jumpSpeed = -14;//Give it upward thrust
                }
            }

            if (kState.IsKeyDown(Keys.LeftShift) && !oldState.IsKeyDown(Keys.LeftShift))
            {//switches from girl to cat and vice versa
                p1.Switch();
            }

            if (kState.IsKeyDown(Keys.R) && oldState.IsKeyDown(Keys.R))
            {//reset the original position of the cat and girl
                p1.ResetPosition();
            }

            oldState = kState;

            //checks for any collisions
            CheckCollisions();

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
                
                p1.Draw(spriteBatch);





                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].Draw(spriteBatch);
                }

                s1.Draw(spriteBatch);
                s2.Draw(spriteBatch);
                bu1.Draw(spriteBatch);
                bu2.Draw(spriteBatch);

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
    }
}
