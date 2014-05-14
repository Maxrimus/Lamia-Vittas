/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A class for environmental hazards
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
    class Environmental:MovableGamePiece
    {
        /// <summary>
        /// Parameterized Constructor, all values passed up
        /// </summary>
        /// <param name="picturesize">Rectangle for the object</param>
        /// <param name="texture">Image for the object</param>
        /// <param name="dir">Direction the object is facing</param>
        public Environmental(Rectangle picturesize, Texture2D texture, int dir)
            : base(picturesize, texture,dir)
        {
        }

        public void Draw(SpriteBatch batch, Texture2D texture)
        {
            batch.Draw(texture, base.PictureBox, Color.White);
        }

        public override void Move()
        {
        }
    }
}
