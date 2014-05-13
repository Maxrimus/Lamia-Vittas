/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A class for the cat
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

    class Cat:Character
    {
        // constant start position for the cat
        private Rectangle startPosCat;

        // property for the starting Position
        public Rectangle StartPosCat
        {
            get { return startPosCat; }
        }

        /// <summary>
        /// Parameterized Constructor
        /// startPosCat defined here
        /// All other values passed up
        /// </summary>
        /// <param name="picturesize">Rectangle holding all shape info about the cat</param>
        /// <param name="texture">The cat's texture</param>
        /// <param name="dir">The direction of the cat</param>
        /// <param name="hlth">The cat's health</param>
        /// <param name="atk">The cat's attack</param>
        public Cat(Rectangle picturesize, Texture2D texture, int dir, int hlth, int atk)
            : base(picturesize, texture,dir,hlth,atk)
        {
            //assigns startPosCat
            startPosCat = new Rectangle(picturesize.X, picturesize.Y, GameVariables.catWidth, GameVariables.catHeight);
        }

        /// <summary>
        /// Moves the cat
        /// </summary>
        public override void Move()
        {
            if (Direction == 0)
            {//if facing left, moves left
                PictureBox = new Rectangle(PictureBox.X - GameVariables.playerMAXSpeed, PictureBox.Y, GameVariables.catWidth, GameVariables.catHeight);
            }
            else if (Direction == 1)
            {//if facing right, moves right
                PictureBox = new Rectangle(PictureBox.X + GameVariables.playerMAXSpeed, PictureBox.Y, GameVariables.catWidth, GameVariables.catHeight);
            }
        }
    }
}
