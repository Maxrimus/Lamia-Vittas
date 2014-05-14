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
        #region Attributes

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        //Spritesheets
        Texture2D gWSheet;
        Texture2D cWSheet;
        Texture2D cJSheet;
        Texture2D gJSheet;

        //needed for girl's walking animation
        Point gframeSize = new Point(70, 100);
        Point gcurrentFrame = new Point(0, 0);
        Point gframes = new Point(4, 1);
        TimeSpan gnextFrameInterval = TimeSpan.FromSeconds((float)1 / 16);

        //needed for cat's walking animation
        Point cframeSize = new Point(100, 50);
        Point ccurrentFrame = new Point(0, 0);
        Point cframes = new Point(4, 1);
        TimeSpan cnextFrameInterval = TimeSpan.FromSeconds((float)1 / 16);

        //light textures
        Texture2D lightBlock;
        Texture2D lightSpikes;
        Texture2D lightBush;

        //dark textures
        Texture2D darkBlock;
        Texture2D darkSpikes;
        Texture2D darkBush;

        //Instantiations of all needed objects
        Girl g1;
        Fist f1;
        Cat c1;
        Player p1;
        //Button b1;
        //Door d1;
        //Spike s1, s2;
        //Bush bu1, bu2;

        // list to hold the collectibles
        List<Collectible> coll;

        //states for keyboard and mouse
        KeyboardState kState;
        KeyboardState oldState;
        MouseState mState;

        //List to hold platforms
        List<Platform> platforms;
        List<Bush> bushes;
        List<Spike> spikes;
        List<Yarn> yarns;
        List<Vial> vials;
        List<Door> doors;
        List<Button> buttons;
        List<Girl> girls;

        internal List<Girl> Girls
        {
            get { return girls; }
            set { girls = value; }
        }

        internal List<Button> Buttons
        {
            get { return buttons; }
            set { buttons = value; }
        }

        internal List<Door> Doors
        {
            get { return doors; }
            set { doors = value; }
        }

        internal List<Vial> Vials
        {
            get { return vials; }
            set { vials = value; }
        }

        internal List<Yarn> Yarns
        {
            get { return yarns; }
            set { yarns = value; }
        }

        internal List<Spike> Spikes
        {
            get { return spikes; }
            set { spikes = value; }
        }

        internal List<Bush> Bushes
        {
            get { return bushes; }
            set { bushes = value; }
        }

        internal List<Platform> Platforms
        {
            get { return platforms; }
            set { platforms = value; }
        }


        //list to hold all objects
        List<GamePiece> allObjects;

        //jumping
        bool jumping;
        int startY;
        int jumpSpeed;
        bool colliding;

        //list to hold all Bounding Boxes
        List<GamePiece> bBs;

        //Screens for MainScreen and PauseScreen
        MainScreen mainScreen;
        PauseScreen pauseScreen;

        //Textures for the Screens
        Texture2D mainScreenTexture;
        Texture2D pauseTexture;
        Texture2D returnToMainMenu;
        Texture2D pauseScreenTexture;

        // Rectangles for position
        Rectangle newButton = new Rectangle();
        Rectangle quitButton = new Rectangle();
        Rectangle loadButton = new Rectangle();
        Rectangle pauseRect = new Rectangle();
        Rectangle menuRect = new Rectangle();
        Rectangle r2Menu = new Rectangle();
        Rectangle r2Desktop = new Rectangle();
        Rectangle r2Game = new Rectangle();
        Rectangle mouseRect = new Rectangle();

        // Dictionary to hold the button's cords
        Dictionary<int, int> buttonDict = new Dictionary<int,int>();

        //Whether or not the game is over
        bool GameOver = false;


        InterfaceScreen main;

        //number of vials and yarn left, starts at 0
        int vialsLeft = 0;
        int yarnLeft = 0;

        #endregion

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100);
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

            //lists of objects
            platforms = new List<Platform>();
            allObjects = new List<GamePiece>();
            coll = new List<Collectible>();
            bBs = new List<GamePiece>();

            
            mState = new MouseState();
            bushes = new List<Bush>();
            spikes = new List<Spike>();
            yarns = new List<Yarn>();
            vials = new List<Vial>();
            doors = new List<Door>();
            buttons = new List<Button>();
            girls = new List<Girl>();
            this.Window.Title = "Lamia Vittas";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            base.Initialize();

            //Initialize jumping data
            startY = p1.GetPosition().Y + p1.GetPosition().Height;
            jumping = false;
            jumpSpeed = 0;
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
            g1 = new Girl(new Rectangle(300,172, 96, 128),Content.Load<Texture2D>(GameVariables.girlTexture),1,250,2);
            allObjects.Add(g1);
            f1 = new Fist(new Rectangle(300, 172, 60, 20), Content.Load<Texture2D>(GameVariables.fistTexture), 1);
            allObjects.Add(f1);
            c1 = new Cat(new Rectangle(600,100, 128, 96), Content.Load<Texture2D>(GameVariables.catTexture), 1, 250, 0);
            allObjects.Add(c1);
            /*
            s1 = new Spike(new Rectangle(0, 300, 200, 50), Content.Load<Texture2D>(GameVariables.spikeTexture), 1);
            allObjects.Add(s1);
            bu1 = new Bush(new Rectangle(200, 300, 200, 50), Content.Load<Texture2D>(GameVariables.bushTexture), 1);
            allObjects.Add(bu1);
            s2 = new Spike(new Rectangle(775, 250, 200, 50), Content.Load<Texture2D>(GameVariables.spikeTexture), 1);
            allObjects.Add(s2);
            bu2 = new Bush(new Rectangle(400, 300, 200, 50), Content.Load<Texture2D>(GameVariables.bushTexture), 1);
            allObjects.Add(bu2);
            b1 = new Button(new Rectangle(25, 200, 25, 75), Content.Load<Texture2D>(GameVariables.buttonOriginalTexture));
            allObjects.Add(b1);
            d1 = new Door(new Rectangle(650, 150, 25, 150), Content.Load<Texture2D>(GameVariables.doorTexture), b1);
            allObjects.Add(d1);
             */

            //Animation Sheets
            gWSheet = Content.Load<Texture2D>(GameVariables.girlTextureSheet);
            cWSheet = Content.Load<Texture2D>(GameVariables.catTextureSheet);
            gJSheet = Content.Load<Texture2D>(GameVariables.girlJumpSheet);
            cJSheet = Content.Load<Texture2D>(GameVariables.catJumpSheet);

            //loads dark and light textures
            lightBlock = Content.Load<Texture2D>(GameVariables.lightBlockTexture);
            lightBush = Content.Load<Texture2D>(GameVariables.lightBushTexture);
            lightSpikes = Content.Load<Texture2D>(GameVariables.lightSpikesTexture);

            darkBlock = Content.Load<Texture2D>(GameVariables.darkBlockTexture);
            darkBush = Content.Load<Texture2D>(GameVariables.bushTexture);
            darkSpikes = Content.Load<Texture2D>(GameVariables.spikeTexture);

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
            {//determines how many vials and yarn there are when the game starts
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
            
            // Store the data into the rectangles
            newButton.X = 43;
            newButton.Y = 215;
            newButton.Width = 700;
            newButton.Height = 84;

            quitButton.X = 43;
            quitButton.Y = 464;
            quitButton.Width = 706;
            quitButton.Height = 109;

            pauseRect.X = 750;
            pauseRect.Y = 550;
            pauseRect.Width = 33;
            pauseRect.Height = 33;

            menuRect.Y = 100;
            menuRect.X = 140;
            menuRect.Width = 510;
            menuRect.Height = 430;

            r2Game.X = 120;
            r2Game.Y = 170;
            r2Game.Width = 379;
            r2Game.Height = 43;

            r2Menu.X = 154;
            r2Menu.Y = 360;
            r2Menu.Width = 481;
            r2Menu.Height = 72;

            //creates screens
            mainScreen = new MainScreen(Content.Load<Texture2D>(GameVariables.newButton), Content.Load<Texture2D>(GameVariables.quitButton), Content.Load<Texture2D>(GameVariables.returnMenu), newButton, quitButton, r2Menu);
            mainScreenTexture = Content.Load<Texture2D>(GameVariables.mainScreen);
            pauseTexture = Content.Load<Texture2D>(GameVariables.pause);
            pauseScreenTexture = Content.Load<Texture2D>(GameVariables.pauseMenu);
            //pauseScreen = new PauseScreen(Content.Load<Texture2D>(GameVariables.newButton), Content.Load<Texture2D>(GameVariables.quitButton), Content.Load<Texture2D>(GameVariables.pauseMenu), new Rectangle(160, 200, 97, 33), new Rectangle(100, 200, 97, 33), new Rectangle(150, 80, 510, 430));
         

            returnToMainMenu = Content.Load<Texture2D>(GameVariables.returnMenu);


            //creates platforms and adds to platforms List
            // floor
            platforms.Add(new Platform(new Rectangle(0, 300, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
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

            // end floor


            // box around girl
            //left
            /*
            platforms.Add(new Platform(new Rectangle(150, 275, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 250, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 225, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 200, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 175, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 150, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 125, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));           
            platforms.Add(new Platform(new Rectangle(150, 100, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));


            //top
            platforms.Add(new Platform(new Rectangle(175, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(200, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(225, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(250, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(275, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(300, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(325, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(350, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(375, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(400, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(425, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(450, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(475, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(500, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(525, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));

            // right
            platforms.Add(new Platform(new Rectangle(550, 275, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 250, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 225, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 200, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 175, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 150, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 125, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 100, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 75, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            //end box around girl
             */


            // pillar on left
            platforms.Add(new Platform(new Rectangle(150, 275, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(150, 250, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(125, 275, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(125, 250, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));

            //end left pillar

            //pillar on right
            platforms.Add(new Platform(new Rectangle(550, 125, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(550, 150, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(525, 125, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            platforms.Add(new Platform(new Rectangle(525, 150, 25, 25), Content.Load<Texture2D>(GameVariables.blockTexture)));
            //end right pillar           

            foreach (Platform i in platforms)
            {//adds all platforms to allObjects List
                allObjects.Add(i);
            }

            foreach (GamePiece i in allObjects)
            {//creates Bounding Boxes for all objects
                bBs.Add(new GamePiece(i.PictureBox,Content.Load<Texture2D>(GameVariables.bBTexture)));
            }

            p1 = new Player(g1, c1, 0, f1);
            Map m1 = new Map();
            m1.ReadMap("map.txt",this);
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

                //sets the direction to the left
                p1.SetDirection(0);

                if (p1.state == 0)
                {//if the player is the girl

                    //moves along the spritesheet one frame
                    gcurrentFrame.X++;
                    if (gcurrentFrame.X >= 4)
                    {//resets the animation sheet if it has gone off the end
                        gcurrentFrame.X = 0;
                    }
                }
                else
                {//if the player is a cat
                    ccurrentFrame.X++;
                    if (ccurrentFrame.X >= 4)
                    {//resets the animation sheet if it has gone off the end
                        ccurrentFrame.X = 0;
                    }
                }

                //moves the player
                p1.Move();
            }

            if (kState.IsKeyDown(Keys.Right) && ((p1.GetPosition().X + (p1.GetPosition().Width)) < graphics.GraphicsDevice.Viewport.Width))
            {//moves the player right

                //sets the direction to the right
                p1.SetDirection(1);

                if (p1.state == 0)
                {//if the player is the girl

                    //moves along the spritesheet one frame
                    gcurrentFrame.X++;
                    if (gcurrentFrame.X >= 4)
                    {//resets the animation sheet if it has gone off the end
                        gcurrentFrame.X = 0;
                    }
                }
                else
                {//if the player is a cat
                    ccurrentFrame.X++;
                    if (ccurrentFrame.X >= 4)
                    {//resets the animation sheet if it has gone off the end
                        ccurrentFrame.X = 0;
                    }
                }

                //moves the player
                p1.Move();
            }

            //checks for any collisions
            CheckCollisions();

            if (jumping)
            {//if already jumping

                //sets the position, adding the current jumpSpeed to the Y
                if (!colliding)
                {
                    p1.SetPosition(new Rectangle(p1.GetPosition().X, p1.GetPosition().Y + jumpSpeed, p1.GetPosition().Width, p1.GetPosition().Height));
                    if (colliding)
                    {
                        p1.SetPosition(new Rectangle(p1.GetPosition().X, p1.GetPosition().Y - (jumpSpeed/2), p1.GetPosition().Width, p1.GetPosition().Height));
                    }
                }

                jumpSpeed += 1;//increments jumpspeed to slow down or speed up
                if (jumpSpeed > 16)
                {
                    jumpSpeed = 16;
                }

                if (((p1.GetPosition().Y + p1.GetPosition().Height) > startY) || colliding)
                {//if the player has landed
                    if (colliding)
                    {
                        p1.SetPosition(new Rectangle(p1.GetPosition().X, p1.GetPosition().Y - jumpSpeed, p1.GetPosition().Width, p1.GetPosition().Height));
                        jumping = false;
                        return;
                    }
                    //makes sure the character is set where it started
                    p1.SetPosition(new Rectangle(p1.GetPosition().X, startY - p1.GetPosition().Height, p1.GetPosition().Width, p1.GetPosition().Height));
                    jumping = false;//sets jumping to false
                }

/*
                if (((p1.GetPosition().Y + p1.GetPosition().Height) >= startY) || (colliding))
                {//if the player has landed

                    if (!colliding)
                    {
                        //makes sure the character is set where it started
                        p1.SetPosition(new Rectangle(p1.GetPosition().X, startY - p1.GetPosition().Height, p1.GetPosition().Width, p1.GetPosition().Height));
                    }
                    jumping = false;//sets jumping to false


                }
 */
                
            }
            else
            {//not currently jumping
                if (kState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                {//determines if space was pressed and was not previously pressed
                    jumping = true;//sets jumping to true

                    if (p1.state == 0)
                    {//if girl, sets upwards velocity to 14
                        jumpSpeed = -14;
                    }
                    else
                    {//if cat, sets upwards velocity to 20, will jump twice as high as girl
                        jumpSpeed = -20;
                    }
                }

            }

            if (!jumping && !colliding)
            {
                p1.SetPosition(new Rectangle(p1.GetPosition().X, p1.GetPosition().Y + 10, p1.GetPosition().Width, p1.GetPosition().Height));
            }      
            else if (!jumping && colliding)
            {
                p1.SetPosition(new Rectangle(p1.GetPosition().X, p1.GetPosition().Y + 1, p1.GetPosition().Width, p1.GetPosition().Height));
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
                //b1.on = false;
            }

            if (kState.IsKeyDown(Keys.Z) && p1.state == 0)
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

            if (kState.IsKeyUp(Keys.Z))
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

            //checks for any collections
            CheckCollections();

            /*
            //opens door if button is pressed
            d1.Open();

            if (!d1.on)
            {//if the door is open
                if (d1.PictureBox.Y - 5 >= d1.maxHeight)
                {//determines if the next move would move it above it's maximum height
                    d1.PictureBox = new Rectangle(d1.PictureBox.X,d1.PictureBox.Y - 5,d1.PictureBox.Width,d1.PictureBox.Height);
                }
            }

            
            if (b1.on == true)
            {//determines if the button has been pressed

                //draws the button as pressed
                b1 = new Button(b1.PictureBox,Content.Load<Texture2D>(GameVariables.buttonPressedTexture));
                
                //makes sure the button stays on and sets the door to open
                b1.on = true;
                d1.on = false;
            }
            else
            {//if not pressed, draws unpressed texture
                b1 = new Button(b1.PictureBox, Content.Load<Texture2D>(GameVariables.buttonOriginalTexture));
            }

            // checks if the game is over
            if (p1.Lives <= 0)
            {
                GameOver = true;
                Exit();
            }
             */

           
                //Gets the mouse's state.
                mState = Mouse.GetState();
            /*    if (main == InterfaceScreen.MainScreen)
                {
                    mouseRect.X = mState.X;
                    mouseRect.Y = mState.Y;
                    if ((newButton.Intersects(mouseRect)) && (mState.LeftButton == ButtonState.Pressed))
                    {
                        // Changes the Enum to GameScreen
                        main = InterfaceScreen.GameScreen;
                        // Calls the draw method
                        Draw(gameTime);
                    }
                    // Outdated Code!
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

                }*/
           
                if (main == InterfaceScreen.GameScreen)
                {

                    mouseRect.X = mState.X;
                    mouseRect.Y = mState.Y;
                    if ((pauseRect.Intersects(mouseRect)) && (mState.LeftButton == ButtonState.Pressed))
                    {
                        // Changes Enum to PauseScreen
                        main = InterfaceScreen.PauseScreen;

                        // Calls the draw method
                        Draw(gameTime);
                    }
                     
                }
             

            if (main == InterfaceScreen.MainScreen)
            {

                mouseRect.X = mState.X;
                mouseRect.Y = mState.Y;

                // Checks if the mouse is in the newbutton's rectangle
                if ((newButton.Intersects(mouseRect)) && (mState.LeftButton == ButtonState.Pressed))
                {
                    // Changes the Enum to GameScreen
                    main = InterfaceScreen.GameScreen;
                    // Calls the draw method
                    Draw(gameTime);
                }

                // Checks if the mouse is in the quitbutton's rectangle
                if ((quitButton.Intersects(mouseRect)) && (mState.LeftButton == ButtonState.Pressed))
                {
                    // Closes the Window
                    Exit();
                }

                // Not Implemented as of yet
                if ((loadButton.Intersects(mouseRect)) && (mState.LeftButton == ButtonState.Pressed))
                {
                    
                }

               
                /*
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
                 */
            }


            if (main == InterfaceScreen.PauseScreen)
            {
                if ((r2Menu.Intersects(mouseRect)) && (mState.LeftButton == ButtonState.Pressed))
                {
                    main = InterfaceScreen.MainScreen;
                    Draw(gameTime);
                }
            }

            colliding = false;
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

            if (main == InterfaceScreen.MainScreen)
            {
                spriteBatch.Draw(mainScreenTexture, new Vector2(0, 0), Color.White);
                mainScreen.Draw(spriteBatch);
                
            }

            if (main == InterfaceScreen.GameScreen)
            {
                //draws the player
                if (!jumping)
                {//draws the characters normally if they aren't jumping
                    if (p1.state == 0)
                    {//if girl, draws the girl
                        if (p1.GetDirection() == 1)
                        {//if right, draws to the right
                            spriteBatch.Draw(gWSheet, p1.GetPosition(), new Rectangle(gframeSize.X * gcurrentFrame.X, gframeSize.Y * gcurrentFrame.Y, gframeSize.X, gframeSize.Y), Color.White);
                        }
                        else if (p1.GetDirection() == 0)
                        {//if left, draws to the left
                            spriteBatch.Draw(gWSheet, p1.GetPosition(), new Rectangle(gframeSize.X * gcurrentFrame.X, gframeSize.Y * gcurrentFrame.Y, gframeSize.X, gframeSize.Y), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                        }
                    }
                    else
                    {//if cat, draws the cat
                        if (p1.GetDirection() == 1)
                        {//if right, draws to the right
                            spriteBatch.Draw(cWSheet, p1.GetPosition(), new Rectangle(cframeSize.X * ccurrentFrame.X, cframeSize.Y * ccurrentFrame.Y, cframeSize.X, cframeSize.Y), Color.White);
                        }
                        else if (p1.GetDirection() == 0)
                        {//if left, draws to the left
                            spriteBatch.Draw(cWSheet, p1.GetPosition(), new Rectangle(cframeSize.X * ccurrentFrame.X, cframeSize.Y * ccurrentFrame.Y, cframeSize.X, cframeSize.Y), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                        }
                    }
                }
                else
                {//if they are jumping draws them depending on their current jumpspeed
                    if (p1.GetDirection() == 0)
                    {//facing left
                        if (p1.state == 0)
                        {//girl
                            if (jumpSpeed == -14)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(0, 0, 70, 100), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                            else if (jumpSpeed > -14 && jumpSpeed <= 0)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(70, 0, 70, 100), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                            else if (jumpSpeed > 0 && jumpSpeed < 14)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(140, 0, 70, 100), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                            else if (jumpSpeed == 14)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(210, 0, 70, 100), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                        }
                        else if (p1.state == 1)
                        {//cat
                            if (jumpSpeed == -20)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(0, 0, 100, 70), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                            else if (jumpSpeed > -20 && jumpSpeed <= 0)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(100, 0, 100, 70), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                            else if (jumpSpeed > 0 && jumpSpeed < 20)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(200, 0, 100, 70), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                            else if (jumpSpeed == 20)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(300, 0, 100, 70), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                            }
                        }
                    }
                    else
                    {//facing right
                        if (p1.state == 0)
                        {//girl
                            if (jumpSpeed == -14)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(0, 0, 70, 100), Color.White);
                            }
                            else if (jumpSpeed > -14 && jumpSpeed <= 0)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(70, 0, 70, 100), Color.White);
                            }
                            else if (jumpSpeed > 0 && jumpSpeed < 14)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(140, 0, 70, 100), Color.White);
                            }
                            else if (jumpSpeed == 14)
                            {
                                spriteBatch.Draw(gJSheet, p1.GetPosition(), new Rectangle(210, 0, 70, 100), Color.White);
                            }
                        }
                        else if (p1.state == 1)
                        {//cat
                            if (jumpSpeed == -20)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(0, 0, 100, 70), Color.White);
                            }
                            else if (jumpSpeed > -20 && jumpSpeed <= 0)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(100, 0, 100, 70), Color.White);
                            }
                            else if (jumpSpeed > 0 && jumpSpeed < 20)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(200, 0, 100, 70), Color.White);
                            }
                            else if (jumpSpeed == 20)
                            {
                                spriteBatch.Draw(cJSheet, p1.GetPosition(), new Rectangle(300, 0, 100, 70), Color.White);
                            }
                        }
                    }
                }

                if (p1.fist.Visible)
                {//draws the fist if the fist is visible
                    p1.fist.Draw(spriteBatch);
                }

                for (int i = 0; i < platforms.Count; i++)
                {//draws all platforms
                    if (p1.state == 0)
                    {
                        platforms[i].Draw(spriteBatch,darkBlock);
                    }
                    else
                    {
                        platforms[i].Draw(spriteBatch, lightBlock);
                    }
                }

                /*
                //draws obstacles
                if (p1.state == 0)
                {
                    s1.Draw(spriteBatch,darkSpikes);
                    s2.Draw(spriteBatch, darkSpikes);
                    bu1.Draw(spriteBatch, darkBush);
                    bu2.Draw(spriteBatch, darkBush);
                }
                else
                {
                    s1.Draw(spriteBatch, lightSpikes);
                    s2.Draw(spriteBatch, lightSpikes);
                    bu1.Draw(spriteBatch, lightBush);
                    bu2.Draw(spriteBatch, lightBush);
                }
                b1.Draw(spriteBatch);
                d1.Draw(spriteBatch);
                 */

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

                // draw the Debug info
                spriteBatch.DrawString(font,
                    p1.GetPosition()
                    + "\nP1 Top:       " + p1.GetPosition().Top
                    + "\nP1 Bottom:  " + p1.GetPosition().Bottom                   
                    + "\nP1 Left:       " + p1.GetPosition().Left
                    + "\nP1 Right:     " + p1.GetPosition().Right
                    + "\nJump Speed:   " + jumpSpeed
                    + "\nHealth:        " + p1.Health
                    + "\nLives:         " + p1.Lives
                    + "\nGame Over: " + GameOver
                    + "\n\nLeft arrow - move left     Right arrom - more right     Shift - switch states     Space - jump/hover     R - respawn", new Vector2(0, 400), Color.Red, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);

                spriteBatch.DrawString(font, "Vials left: " + vialsLeft + " Yarn Left: " + yarnLeft, new Vector2(10, 10), Color.Black);

                // Draws the pause texture
                spriteBatch.Draw(pauseTexture, pauseRect, Color.White);
            }
            if (main == InterfaceScreen.PauseScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(pauseScreenTexture, menuRect, Color.White);
                spriteBatch.Draw(returnToMainMenu, r2Menu, Color.White);
                

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for any collisions
        /// </summary>
        protected void CheckCollisions()
        {
            foreach (Platform plat in platforms)
            {
                if (p1.GetPosition().Intersects(plat.PictureBox))
                {
                    // for when on top of a block or hitting the bottom
                    if (p1.GetPosition().Bottom > plat.PictureBox.Top
                        && p1.GetPosition().Bottom < plat.PictureBox.Center.Y)
                    {//top
                        colliding = true;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("TOP");
                        p1.SetPosition(new Rectangle(p1.GetPosition().X, plat.PictureBox.Top - p1.GetPosition().Height, p1.GetPosition().Width, p1.GetPosition().Height));
                        
                    }
                    else if (p1.GetPosition().Top < plat.PictureBox.Bottom
                        && p1.GetPosition().Top > plat.PictureBox.Center.Y)
                    { //bottom     
                        colliding = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("BOTTOM");
                        p1.SetPosition(new Rectangle(p1.GetPosition().X, plat.PictureBox.Bottom, p1.GetPosition().Width, p1.GetPosition().Height));
                    }
                    else // left or right collision
                    {
                        if(p1.GetPosition().Right > plat.PictureBox.Left
                            && p1.GetPosition().Right < plat.PictureBox.Center.X)
                        {//left of a block
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("LEFT");
                            p1.SetPosition(new Rectangle(plat.PictureBox.Left - p1.GetPosition().Width, p1.GetPosition().Y, p1.GetPosition().Width, p1.GetPosition().Height));
                        }
                        else if(p1.GetPosition().Left < plat.PictureBox.Right
                            && p1.GetPosition().Left > plat.PictureBox.Center.X)
                        {// right of a block
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("RIGHT");
                            p1.SetPosition(new Rectangle(plat.PictureBox.Right, p1.GetPosition().Y, p1.GetPosition().Width, p1.GetPosition().Height));
                        }

                    }

                    /*
                    // determines if player is on top of a block
                    if ((p1.GetPosition().Top < plat.PictureBox.Top) && 
                        (p1.GetPosition().Center.Y < plat.PictureBox.Top) && 
                        (p1.GetPosition().Bottom > plat.PictureBox.Top) && 
                        (p1.GetPosition().Center.X >= plat.PictureBox.Left && p1.GetPosition().Center.X <= plat.PictureBox.Right))
                    {
                        colliding = true;
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

                /*
                if ((p1.GetPosition().X < d1.PictureBox.Right && p1.GetPosition().X > d1.PictureBox.Left) && p1.IsColliding(d1))
                {//determines if player colliding with door from left
                    p1.SetPosition(new Rectangle(p1.GetPosition().X + 1, p1.GetPosition().Y, p1.GetPosition().Width, p1.GetPosition().Height));
                }

                if ((p1.GetPosition().X + p1.GetPosition().Width > d1.PictureBox.Left && p1.GetPosition().X + p1.GetPosition().Width < d1.PictureBox.Right) && p1.IsColliding(d1))
                {//determines if player colliding with door from right
                    p1.SetPosition(new Rectangle(p1.GetPosition().X - 1, p1.GetPosition().Y, p1.GetPosition().Width, p1.GetPosition().Height));
                }
                 */
                }
                /*
                if (p1.fist.IsColliding(b1))
                {//determines if button it pushed
                    b1.on = true;
                }

                //spike checks

                if (p1.GetPosition().Intersects(s1.PictureBox))
                {// check for collisions between spikes and character

                    // damage the player
                    s1.DamagePlayerSpike(p1);
                }

                if (p1.GetPosition().Intersects(s2.PictureBox))
                {// check for collisions between spikes and character

                    // damage the player
                    s2.DamagePlayerSpike(p1);
                }

                //bush checks

                if (p1.GetPosition().Intersects(bu1.PictureBox))
                {// check for collisions between spikes and character

                    // damage the player
                    bu1.DamagePlayerBush(p1);
                }

                if (p1.GetPosition().Intersects(bu2.PictureBox))
                {// check for collisions between spikes and character

                    // damage the player
                    bu2.DamagePlayerBush(p1);
                }
                 */
            }
        }

        /// <summary>
        /// Check if the character collected a collectible
        /// </summary>
        public void CheckCollections()
        {
            foreach (Collectible z in coll)
            {//checks each collectible
                if (p1.state == 0 && p1.IsColliding(z) && z.Collected==false && z.Style == "vial")
                {//checks if it is a vial, if it has not been collected, and if the player is colliding with it
                    z.Collected = true;
                    vialsLeft--;
                }

                if (p1.state == 1 && p1.IsColliding(z) && z.Collected == false && z.Style == "yarn")
                {//checks if it is a yarn, if it has not been collected, and if the player is colliding with it
                    z.Collected = true;
                    yarnLeft--;
                }
            }
        }
    }
}
