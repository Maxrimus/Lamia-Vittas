/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A static class housing multiple variables needed throughout the game
 * Exceptions:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Lamia_Vittas
{
    public static class GameVariables
    {
        // number of collectibles
        public const int NUM_COLLECTIBLES = 500;
        
        //font
        public static string arialFont = "Arial";

        //movement
        public static int girlMAXSpeed = 7;
        public static int catMAXSpeed = 20;
        public static int girlJump = 40;
        public static int catJump = 80;
        public static int enemySpeed = 10;

        // physics
        public static int gravity = 1;
        public static int girlAcc = 9;
        public static int catAcc = 12;
        public static int jumpAngle = 45;

        //dark textures
        public static string girlTexture = "Girl";
        public static string girlTextureSheet = "GirlWalkSpritesheet";
        public static string fistTexture = "Fist";
        public static string vialTexture = "Vial";

        //light textures
        public static string catTexture = "Cat";
        public static string yarnTexture = "Yarn";

        //environment textures
        public static string bushTexture = "Bush";
        public static string blockTexture = "GeneralBLock";
        public static string spikeTexture = "Spikes";
        public static string buttonPressedTexture = "ButtonPressed";
        public static string buttonOriginalTexture = "ButtonOriginal";
        public static string doorTexture = "Door";

        //texture for Bounding Box debug
        public static string bBTexture = "BoundingBox";

        // widths and heights for the images
        public static int girlWidth = 96;
        public static int girlHeight = 128;
        public static int catWidth = 128;
        public static int catHeight = 96;


        // Interface Textures
        //Screen Textures
        public static string mainScreen = "MainScreen";
        // Button Textures
        public static string startButton = "TestStart";
        public static string quitButton = "TestQuit";
        public static string returnToMenu = "Return To Menu";
        // Pause Texture
        public static string pause = "Pause";
    }
}
