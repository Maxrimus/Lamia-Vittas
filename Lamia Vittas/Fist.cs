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
        //visible: whether or not the fist is visible
        private bool visible;

        public bool Visible
        {//attribute for visibility
            get { return visible; }
            set { visible = value; }
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="vector">Location of the image</param>
        /// <param name="texture">Texture of the image</param>
        /// <param name="dir">Direction the image is facing, 0 for left, 1 for right</param>
        public Fist(Rectangle picturesize, Texture2D texture, int dir)
            : base(picturesize, texture, dir)
        {
            //sets visible
            visible = false;
        }

        /// <summary>
        /// Fist Punches
        /// </summary>
        public void Punch()
        {
            visible = true;
        }

        /// <summary>
        /// Fist UnPunches
        /// </summary>
        public void UnPunch()
        {
            visible = false;
        }

        /// <summary>
        /// Moves the Fist
        /// </summary>
        public override void Move()
        {
        }
    }
}
