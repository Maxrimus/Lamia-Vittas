/* Sadiki Solomon
 * Date: 4/2/2014
 * Purpose: A class for the main Game Screen
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
    class GameScreen:Screen
    {
        public GameScreen (Texture2D startBttn, Texture2D quitBttn, Texture2D returnToMain, Rectangle sBttnPosition, Rectangle qBttnPosition, Rectangle returnToMainPosition)
            : base(startBttn, quitBttn, returnToMain, sBttnPosition, qBttnPosition, returnToMainPosition)
        {

        }

    }
}
