/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A class for thorny bush hazards
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
    class Bush:Environmental
    {
        /// <summary>
        /// Parameterized Constuctor, all values passed up
        /// </summary>
        /// <param name="picturesize">The rectangle holding the information of the bush</param>
        /// <param name="texture">The texture of the book</param>
        /// <param name="dir">Direction the bush faces</param>
        public Bush(Rectangle picturesize, Texture2D texture, int dir)
            : base(picturesize, texture, dir)
        {
        }

        /// <summary>
        /// Damages the character if interacted with
        /// </summary>
        /// <param name="chara">Character object to take damage</param>
        public void DamagePlayerBush(Player plyr)
        {
            if (plyr.state == 1)
            {
                plyr.GainLife(1);
                plyr.Canheal = false; // make it so that the player can only heal once
            }
            else if (plyr.state == 0)
            {
                //makes the player take 1 damage
                plyr.TakeHit(1);
            }
            
        }
    }
}
