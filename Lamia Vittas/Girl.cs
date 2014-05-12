/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A class for the girl
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
using System.Threading;
using System.Timers;

namespace Lamia_Vittas
{
    class Girl:Character
    {
        // constant start position for the girl
        private Rectangle startPosGirl;

        //List to hold platforms
        List<Platform> platforms;

        // property for the starting Position
        public Rectangle StartPosGirl
        {
            get { return startPosGirl; }
        }

        /// <summary>
        /// Parameterzied Constructor
        /// </summary>
        /// <param name="vector">Location of the image</param>
        /// <param name="texture">Texture of the image</param>
        /// <param name="dir">Direction the image is facing, 0 for left, 1 for right</param>
        /// <param name="hlth">The hits the character can take</param>
        /// <param name="atk">The damage the character can deal</param>
        public Girl(Rectangle picturesize, Texture2D texture,int dir,int hlth, int atk)
            : base(picturesize, texture,dir,hlth,atk)
        {
            startPosGirl = new Rectangle(picturesize.X, picturesize.Y, GameVariables.girlWidth, GameVariables.girlHeight);
        }

        /// <summary>
        /// Moves the girl
        /// </summary>
        public override void Move()
        {
            if (Direction == 0)
            {//if facing left, moves left
//<<<<<<< .mine
                PictureBox = new Rectangle(PictureBox.X - GameVariables.girlMAXSpeed, PictureBox.Y, GameVariables.girlWidth, GameVariables.girlHeight);
//=======
//=======
                //PictureBox = new Rectangle(PictureBox.X - GameVariables.girlMaxSpeed, PictureBox.Y, GameVariables.girlWidth, GameVariables.girlHeight);
//>>>>>>> .r27
//>>>>>>> .r28
            }
            else if (Direction == 1)
            {//if facing right, moves right
//<<<<<<< .mine
                PictureBox = new Rectangle(PictureBox.X + GameVariables.girlMAXSpeed, PictureBox.Y, GameVariables.girlWidth, GameVariables.girlHeight);
//=======
//=======
                //PictureBox = new Rectangle(PictureBox.X + GameVariables.girlMaxSpeed, PictureBox.Y, GameVariables.girlWidth, GameVariables.girlHeight);
//>>>>>>> .r27
//>>>>>>> .r28
            }
        }

        /*
        /// <summary>
        /// Makes the girl jump
        /// </summary>
        /// <param name="batch">The spritebatch to draw from</param>
        public override void Jump(SpriteBatch batch)
        {
            batch.Begin();
            int upVelocity = 40;
            int i = 1;
            int totHeight = 0;
            int jumpStart = (int)(Position.Y + Image.Height);
            while ((upVelocity >= 0) && (Position.Y >= 0))
            {
<<<<<<< .mine
                {
                    Position = new Vector2(Position.X, (float)(Position.Y - upVelocity));
                    batch.Draw(Image, Position, Color.White);
                    totHeight += (int)upVelocity;
                    upVelocity = upVelocity - (GameVariables.gravity * i);
                    i++;
                }
=======
                PictureBox = new Rectangle(PictureBox.X, (PictureBox.Y - upVelocity), GameVariables.girlWidth, GameVariables.girlHeight);
                batch.Draw(Image, PictureBox, Color.White);
                upVelocity = upVelocity - (GameVariables.gravity * i);
                i++;
>>>>>>> .r27
            }

            i = 1;
            while (((upVelocity) < 40) && ((Position.Y+Image.Height) < jumpStart))
            {
                PictureBox = new Rectangle(PictureBox.X, (PictureBox.Y + upVelocity), GameVariables.girlWidth, GameVariables.girlHeight);
                batch.Draw(Image, PictureBox, Color.White);
                upVelocity = upVelocity + (GameVariables.gravity * i);
                i++;
            }
            batch.End();
        }
         */

        public override void Jump(SpriteBatch batch)
        {
            batch.Begin();
            PictureBox = new Rectangle(PictureBox.X, PictureBox.Y - 4, GameVariables.girlWidth, GameVariables.girlHeight);
            batch.Draw(Image, PictureBox, Color.White);
            batch.End();
        }


        /// <summary>
        /// Makes the girl fall
        /// </summary>
        /// <param name="batch">The spritebatch to draw from</param>
        public void Fall(SpriteBatch batch, bool check)
        {
           // bool touchCheck = check;
           // if (touchCheck == false)
            //{
                batch.Begin();
                PictureBox = new Rectangle(PictureBox.X, PictureBox.Y + 4, GameVariables.girlWidth, GameVariables.girlHeight);
                batch.Draw(Image, PictureBox, Color.White);
                batch.End();
           //}
        }

        /// <summary>
        /// Makes the girl attack
        /// </summary>
        public override void Attacks()
        {
        }

        /// <summary>
        /// called when the girl dies
        /// </summary>
        public override void Die()
        {
            
        }
    }
}
