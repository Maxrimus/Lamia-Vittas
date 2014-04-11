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
            plyr.TakeHit(10);
        }
    }
}
