/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A class for a generic enemy
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
    class Enemy:Character
    {
        public Enemy(Rectangle picturesize, Texture2D texture, int dir, int hlth, int atk)
            : base(picturesize, texture,dir,hlth,atk)
        {
        }

        /// <summary>
        /// Moves the enemy
        /// </summary>
        public override void Move()
        {
            if (Direction == 0)
            {//if enemy facing left, moves left
                Position = new Vector2(Position.X - GameVariables.enemySpeed, Position.Y);
            }
            else if (Direction == 1)
            {//if facing right, moves right
                Position = new Vector2(Position.X + GameVariables.enemySpeed, Position.Y);
            }
        }

        /// <summary>
        /// Makes the enemy jump
        /// </summary>
        public override void Jump(SpriteBatch batch)
        {
        }

        /// <summary>
        /// Makes the enemy attack
        /// </summary>
        public override void Attacks()
        {
        }

        /// <summary>
        /// called when the enemy dies
        /// </summary>
        public override void Die()
        {
        }
    }
}
