/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A class for all characters; cat, girl, monsters, etc
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
    abstract class Character:MovableGamePiece
    {
        /*Attributes
         * health: how many hits the character can take
         * attack: how many health untis the character takes off
         */

        // health of a cat/girl
        private static int healthFriendly;

        // health of a character
        private int health;

        // property for the health of the cat/ girl
        public int HealthFriendly
        {
            get { return healthFriendly; }
            set { healthFriendly = value; }
        }

        // property for the health of a character
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        //the attack value of the character
        private int attack;

        public int Attack
        {
            get { return attack; }
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="vector">Location of the image</param>
        /// <param name="texture">Texture of the image</param>
        /// <param name="dir">Direction the image is facing, 0 for left, 1 for right</param>
        /// <param name="hlth">The hits the character can take</param>
        /// <param name="atk">The damage the character can deal</param>
        public Character(Rectangle picturesize, Texture2D texture, int dir, int hlth, int atk)
            : base(picturesize, texture,dir)
        {
            health = hlth;
            healthFriendly = health;
            attack = atk;
        }

        /// <summary>
        /// Method stub for Move method
        /// </summary>
        abstract public override void Move();
    }
}
