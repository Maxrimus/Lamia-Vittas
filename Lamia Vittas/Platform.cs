/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A class for platforms
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
    class Platform:GamePiece
    {
        // Attributes
        private bool isTouching = false;

        /// <summary>
        /// Parameterized Constructor
        /// All values passed up
        /// </summary>
        /// <param name="picturesize">The rectangle holding the image</param>
        /// <param name="texture">The image of the platform</param>
        public Platform(Rectangle picturesize, Texture2D texture)
            : base(picturesize, texture)
        {

        }

        public bool IsTouching
        {
            get { return isTouching; }
            set { isTouching = value; }
        }

        public void Draw(SpriteBatch batch, Texture2D texture)
        {
            batch.Draw(texture, base.PictureBox, Color.White);
        }
    }
}
