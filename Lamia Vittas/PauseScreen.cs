/*Joseph Tursi
 * Sadiki Solomon
 * Date: 4/2/2014
 * Purpose: A class for the pause screen
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
    class PauseScreen:Screen
    {
        Rectangle mainMenuPosition;
        Texture2D mainMenu;

        public PauseScreen(Texture2D startBttn, Texture2D quitBttn, Texture2D returnToMain, Rectangle sBttnPosition, Rectangle qBttnPosition, Rectangle returnToMainPosition)
            : base(startBttn, quitBttn, returnToMain, sBttnPosition, qBttnPosition, returnToMainPosition)
        {
            mainMenu = returnToMain;
            mainMenuPosition = returnToMainPosition;
        }

        /// <summary>
        /// Draws the figure
        /// </summary>
        /// <param name="batch">The spritebatch this image will be drawn in</param>
        public override void Draw(SpriteBatch batch)
        {
             batch.Draw(mainMenu, mainMenuPosition, Color.White);
        }
     

           
      
    }
}
