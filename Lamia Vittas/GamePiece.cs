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
    public abstract class GamePiece
    {
        /*Attributes
         * position: A Vector2 holding the location of the image
         * image: The texture 2D of the piece
         */
        private Vector2 position;
        private Rectangle pictureBox;

        public Vector2 Position
        {
            get { return new Vector2(pictureBox.X, pictureBox.Y); }
            set { position = value; }
        }
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

        /*
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X,(int)Position.Y,image.Width,image.Height);
            }
        }
         */


        /// <summary>
        /// Parameterized Constructor
        /// All values passed in
        /// </summary>
        /// <param name="vector">The piece's location</param>
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

        public string Side(GamePiece gp2)
        {
            Vector2 center = new Vector2(Position.X + (Image.Width / 2), Position.Y + (Image.Height / 2));
            if ((Position.Y + Image.Height > gp2.Position.Y) && !(Position.Y < gp2.Position.Y) && (center.X > gp2.Position.X) && (center.X + Image.Width < gp2.Position.X + gp2.Image.Width))
            {
                //Position = new Vector2(Position.X, gp2.Position.Y - Image.Height);
                return "bottom";
                //return;
            }
            else if ((center.X < gp2.Position.X + gp2.Image.Width) && !(center.X + Image.Width > gp2.Position.X) && (center.Y /*+ Image.Height*/ > gp2.Position.Y) && (center.Y < gp2.Position.Y))
            {
                //Position = new Vector2(gp2.Position.X + gp2.Image.Width, Position.Y);
                return "left";
                //return;
            }
            else if ((center.X/* + Image.Width */> gp2.Position.X) && !(center.X < gp2.Position.X) && (center.Y /*+ Image.Height*/ > gp2.Position.Y + gp2.Image.Height) && (center.Y < gp2.Position.Y))
            {
                //Position = new Vector2(gp2.Position.X - Image.Width, Position.Y);
                return "right";
                //return;
            }
            else if ((Position.Y < gp2.Position.Y + gp2.Image.Height) && !(Position.Y + Image.Height > gp2.Position.Y + gp2.Image.Height) && (center.X > gp2.Position.X) && (center.X/* + Image.Width*/ < gp2.Position.X + gp2.Image.Width))
            {
                //Position = new Vector2(this.Position.X, gp2.Position.Y + gp2.Image.Height);
                return "top";
                //return;
            }
            else
            {
                return "";
            }
        }
    }
}
