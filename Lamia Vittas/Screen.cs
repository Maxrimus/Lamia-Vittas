/*Joseph Tursi
 * Sadiki Solomon
 * Date: 4/2/2014
 * Purpose: Generic Screen class
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
    class Screen
    {
        // Attributes
        //   Texture2D mainImage;
        Texture2D startBttnImage;
        Texture2D quitBttnImage;
        Texture2D mainScreenImage;
        Rectangle startButtonPosition;
        Rectangle quitButtonPosition;
   

        
        public Screen(Texture2D startBttn, Texture2D quitBttn, Texture2D returnToMain, Rectangle sBttnPosition, Rectangle qBttnPosition, Rectangle returnToMainPosition)
        {
            //  mainImage = mainTexture;
            startBttnImage = startBttn;
            quitBttnImage = quitBttn;
            startButtonPosition = sBttnPosition;
            quitButtonPosition = qBttnPosition;


        }

        /// <summary>
        /// Draws the figure
        /// </summary>
        /// <param name="batch">The spritebatch this image will be drawn in</param>
        virtual public void Draw(SpriteBatch batch)
        {
            batch.Draw(startBttnImage, startButtonPosition, Color.White);
            batch.Draw(quitBttnImage, quitButtonPosition, Color.White);
           
        }
    }
}
