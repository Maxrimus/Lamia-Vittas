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
                PictureBox = new Rectangle(PictureBox.X - GameVariables.playerMAXSpeed, PictureBox.Y, GameVariables.girlWidth, GameVariables.girlHeight);
            }
            else if (Direction == 1)
            {//if facing right, moves right
                PictureBox = new Rectangle(PictureBox.X + GameVariables.playerMAXSpeed, PictureBox.Y, GameVariables.girlWidth, GameVariables.girlHeight);
            }
        }
    }
}
