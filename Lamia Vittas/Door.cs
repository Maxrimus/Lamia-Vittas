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
        /*Attributes
         * b1: the button the door is tied to
         * maxHeight: the maxHeight the door can go
         */
        Button b1;
        public int maxHeight;

        /// <summary>
        /// Parameterized Constructor
        /// Some are passed in, some are assigned
        /// </summary>
        /// <param name="picturesize">Rectangle for the door</param>
        /// <param name="texture">The image of the door</param>
        /// <param name="b">the button</param>
        public Door(Rectangle picturesize, Texture2D texture, Button b)
            : base(picturesize, texture)
        {
            //sets on to true, which means the door is closed
            on = true;

            //assigns button and max height
            b1 = b;
            maxHeight = picturesize.Y - picturesize.Height;
        }

        /// <summary>
        /// Determines if the door should be open
        /// </summary>
        public void Open()
        {
            if (b1.on)
            {//if the button is pressed, sets the door to open
                this.on = false;
            }
        }
    }
}
