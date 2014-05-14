/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A class for the spike hazards
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
    class Spike:Environmental
    {
        /// <summary>
        /// Parameterized Constructor
        /// All values passed up
        /// </summary>
        /// <param name="picturesize">The rectangle of the image</param>
        /// <param name="texture">The image of the spike</param>
        /// <param name="dir">The direction of the spike</param>
        public Spike(Rectangle picturesize, Texture2D texture, int dir)
            : base(picturesize, texture,dir)
        {
        }

        /// <summary>
        /// Damages the character if interacted with
        /// </summary>
        /// <param name="chara">Character object that takes damage</param>
        public void DamagePlayerSpike(Player plyr)
        {
            plyr.TakeHit(1);
        }
    }
}
