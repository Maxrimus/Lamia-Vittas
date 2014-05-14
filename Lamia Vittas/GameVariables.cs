/*Joseph Tursi
 * Gregory Bednarowicz
 * Sadiki Solomon
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
        public static int playerMAXSpeed = 10;

        //dark textures
        public static string girlTexture = "Girl";
        public static string girlTextureSheet = "GirlWalkSpritesheet";
        public static string fistTexture = "Fist";
        public static string vialTexture = "Vial";
        public static string darkBlockTexture = "DarkBlock";
        public static string spikeTexture = "Spikes";
        public static string bushTexture = "Bush";

        //light textures
        public static string catTexture = "Cat";
        public static string catTextureSheet = "CatWalkSpritesheet";
        public static string yarnTexture = "Yarn";
        public static string lightBlockTexture = "BrightBlock";
        public static string lightSpikesTexture = "CatSpikes";
        public static string lightBushTexture = "CatBush";

        //environment textures
        public static string blockTexture = "GeneralBLock";
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
        public static string mainScreen = "MainUnclicked";
        // Button Textures
        public static string newButton = "NewButton";
        public static string quitButton = "QuitButton";
        //public static string returnToMenu = "Return To Menu";
        // Pause Texture
        public static string pause = "Pause";
        public static string pauseMenu = "pauseMenu";
        public static string returnMenu = "R2Menu";
        public static string returnToGame = "R2Game";
        public static string returnToDesktrop = "R2Desktop";

        // Hud Textures
        public static string hud = "HUD";

        // Win game Textures
        public static string win = "WinScreen";

        // Background Textures
        public static string girlBg = "girlBackground";
        public static string catBg = "catBackground";
      

    }
}
