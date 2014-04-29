/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A class for doors
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
    class Door:Mechanical
    {
        Button b1;
        public int maxHeight;

        public Door(Rectangle picturesize, Texture2D texture, Button b)
            : base(picturesize, texture)
        {
            on = true;
            b1 = b;
            maxHeight = picturesize.Y - picturesize.Height;
        }

        public void Open()
        {
            if (b1.on)
            {
                this.on = false;
            }
        }
    }
}
