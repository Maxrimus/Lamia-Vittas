/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A class for the fist
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
    class Fist:MovableGamePiece
    {
        bool visible;

        public Fist(Rectangle picturesize, Texture2D texture, int dir)
            : base(picturesize, texture, dir)
        {
            visible = false;
        }

        public void Punch()
        {
            visible = true;
        }

        public void UnPunch()
        {
            visible = false;
        }
    }
}
