/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A generic, abstract gamepiece class
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
    class GamePiece
    {
        /*Attributes and Properties
         * pictureBox: A rectangle holding the location of the image
         * image: The texture 2D of the piece
         */
        private Rectangle pictureBox;

        public Rectangle PictureBox
        {
            get { return pictureBox; }
            set { pictureBox = value; }
        }

        private Texture2D image;

        public Texture2D Image
        {
            get { return image; }
        }

        /// <summary>
        /// Parameterized Constructor
        /// All values passed in
        /// </summary>
        /// <param name="picturesize">The piece's location</param>
        /// <param name="texture">The image of the piece</param>
        public GamePiece(Rectangle picturesize, Texture2D texture)
        {
            pictureBox = picturesize;
            image = texture;
        }

        /// <summary>
        /// Draws the figure
        /// </summary>
        /// <param name="batch">The spritebatch this image will be drawn in</param>
        virtual public void Draw(SpriteBatch batch)
        {
            batch.Draw(image, pictureBox, Color.White);
        }

        /// <summary>
        /// Determines if character is colliding
        /// </summary>
        /// <param name="gp2">The gamepiece to compare to</param>
        /// <returns>true for colliding, false for not</returns>
        public bool IsColliding(GamePiece gp2)
        {
            if (this.pictureBox.Intersects(gp2.pictureBox))
            {//uses intersects to determine if colliding
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
