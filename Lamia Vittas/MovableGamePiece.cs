/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A class for moving gamepieces
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
    abstract class MovableGamePiece:GamePiece
    {
        /*Attributes and Properties
         * direction: the direction the character is facing. 0 for left, 1 for right
         */
        private int direction;

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="vector">Location of the image</param>
        /// <param name="texture">Texture of the image</param>
        /// <param name="dir">Direction the image is facing, 0 for left, 1 for right</param>
        public MovableGamePiece(Rectangle picturesize, Texture2D texture,int dir)
            : base(picturesize, texture)
        {
            direction = dir;
        }

        /// <summary>
        /// Method stub for Move method
        /// </summary>
        abstract public void Move();

        /// <summary>
        /// Draws the gamepiece depending upon direction
        /// </summary>
        /// <param name="batch">The spritebatch to draw in</param>
        public override void Draw(SpriteBatch batch)
        {
            if (Direction == 0)
            {//flips her to the left if she is moving left
                batch.Draw(Image, PictureBox, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
            else if (Direction == 1)
            {//draws her normally if going right
                batch.Draw(Image, PictureBox, Color.White);
            }
        }
    }
}
