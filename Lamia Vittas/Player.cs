/*Joseph Tursi
 * Gregory Bednarowicz
 * Date: 4/2/2014
 * Purpose: A class for the player, houses a girl and a cat, handles switching
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
    class Player
    {
        /*Attributes
         * girl: The girl associated with this player
         * cat: The cat associated with this player
         * state: what state the player is in; 0 for girl, 1 for cat
         */
        Girl girl;
        Cat cat;
        int state;
        int health;
        int lives;
        const int MAXHEALTH = 250;

        public int Health
        {
            get { return health; }
            set { health = value; }

        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int MAXHEALTH_P
        {
            get { return MAXHEALTH; }
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="g1">Girl object associated with this player</param>
        /// <param name="c1">Cat object associated with this player</param>
        /// <param name="st">State the player is in</param>
        public Player(Girl g1, Cat c1, int st)
        {
            girl = g1;
            cat = c1;
            state = st;
            health = g1.HealthFriendly;
            lives = 10;
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="batch">The spriteBatch to be drawn in</param>
        public void Draw(SpriteBatch batch)
        {
            switch (state)
            {//decides which method to call; 0 for girl, 1 for cat
                case 0:
                    girl.Draw(batch);
                    break;
                case 1:
                    cat.Draw(batch);
                    break;
            }
        }

        /// <summary>
        /// Moves the player
        /// </summary>
        public void Move()
        {
            switch (state)
            {//decides which method to call; 0 for girl, 1 for cat
                case 0:
                    girl.Move();
                    break;
                case 1:
                    cat.Move();
                    break;
            }
        }

        /// <summary>
        /// Jumps the player
        /// </summary>
        /// <param name="batch">The spriteBatch to be drawn in</param>
        public void Jump(SpriteBatch batch)
        {
            switch (state)
            {//decides which method to call; 0 for girl, 1 for cat
                case 0:
                    girl.Jump(batch);
                    break;
                case 1:
                    cat.Jump(batch);
                    break;
            }
        }

        /// <summary>
        /// Makes the player fall
        /// </summary>
        /// <param name="batch">The spriteBatch to be drawn in</param>
        public void Fall(SpriteBatch batch)
        {
            switch (state)
            {//decides which method to call; 0 for girl, 1 for cat
                case 0:
                    girl.Fall(batch);
                    break;
                case 1:
                    cat.Fall(batch);
                    break;
            }
        }

        /// <summary>
        /// The player attacks
        /// </summary>
        public void Attack()
        {
            switch (state)
            {//decides which method to call; 0 for girl, 1 for cat
                case 0:
                    girl.Attacks();
                    break;
                case 1:
                    cat.Attacks();
                    break;
            }
        }

        /// <summary>
        /// The player takes damage
        /// </summary>
        /// <param name="dmgTaken">the amount of damage taken</param>
        public void TakeHit(int dmgTaken)
        {
            //takes damage
            health= health - dmgTaken;

            if (health <= 0)
            {//checks if dead
                health = 0;
                Die();

            }
        }

        /// <summary>
        /// The player Dies
        /// </summary>
        public void Die()
        {
            // reset the postition
            ResetPosition();

            // reset the health
            health = MAXHEALTH;

            // lose a life
            Lives -= 1;
            /*
            switch (state)
            {//decides which method to call; 0 for girl, 1 for cat
                case 0:
                    girl.Die();
                    break;
                case 1:
                    cat.Die();
                    break;
            }
             */
        }

        /// <summary>
        /// Returns the Direction of the player
        /// </summary>
        /// <returns>The direction of the player</returns>
        public int GetDirection()
        {
            switch (state)
            {//decides which direction to return; 0 for girl, 1 for cat
                case 0:
                    return girl.Direction;
                case 1:
                    return cat.Direction;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Sets the Direction of the player
        /// </summary>
        /// <param name="num">The directions to be set</param>
        public void SetDirection(int num)
        {
            switch (state)
            {//decides which direction to set; 0 for girl, 1 for cat
                case 0:
                    girl.Direction = num;
                    break;
                case 1:
                    cat.Direction = num;
                    break;
            }
        }

        /// <summary>
        /// Returns the position
        /// </summary>
        /// <returns>Position of the player</returns>
        public Rectangle GetPosition()
        {
            switch (state)
            {//decides which position to return; 0 for girl, 1 for cat
                case 0:
                    return girl.PictureBox;
                case 1:
                    return cat.PictureBox;
                default:
                    return new Rectangle();
            }
        }

        public void SetPosition(Rectangle r1)
        {
            switch (state)
            {//decides which position to set; 0 for girl, 1 for cat
                case 0:
                    girl.PictureBox = r1;
                    break;
                case 1:
                    cat.PictureBox = r1;
                    break;
            }
        }

        public string Side(GamePiece gp2)
        {
            switch (state)
            {//decides which method to call; 0 for girl, 1 for cat
                case 0:
                    return girl.Side(gp2);
                case 1:
                    return cat.Side(gp2);
                default:
                    return "";
            }
        }

        public Texture2D Image()
        {
            switch (state)
            {//decides which texture to return; 0 for girl, 1 for cat
                case 0:
                    return girl.Image;
                case 1:
                    return cat.Image;
                default:
                    return girl.Image;
            }
        }

        public int JumpHeight()
        {
            switch (state)
            {//decides which texture to return; 0 for girl, 1 for cat
                case 0:
                    return GameVariables.girlJump;
                case 1:
                    return GameVariables.catJump;
                default:
                    return GameVariables.girlJump;
            }
        }

        public int Acc()
        {
            switch (state)
            {//decides which texture to return; 0 for girl, 1 for cat
                case 0:
                    return GameVariables.girlAcc;
                case 1:
                    return GameVariables.catAcc;
                default:
                    return GameVariables.girlAcc;
            }
        }

        public GamePiece GamePiece()
        {
            switch (state)
            {//decides which texture to return; 0 for girl, 1 for cat
                case 0:
                    return girl;
                case 1:
                    return cat;
                default:
                    return girl;
            }
        }

        public bool IsColliding(GamePiece gp2)
        {
            switch (state)
            {//decides which IsColliding method to call to return; 0 for girl, 1 for cat
                case 0:
                    return girl.IsColliding(gp2);
                case 1:
                    return cat.IsColliding(gp2);
                default:
                    return girl.IsColliding(gp2);
            }
        }

        /// <summary>
        /// Switches the state of the player
        /// </summary>
        public void Switch()
        {
            if (state == 0)
            {//switches to cat state
                state = 1;
                cat.PictureBox = new Rectangle(girl.PictureBox.X, girl.PictureBox.Y + 32, GameVariables.catWidth, GameVariables.catHeight);
                cat.Direction = girl.Direction;
            }
            else if (state == 1)
            {//switches to girl state
                state = 0;
                girl.PictureBox = new Rectangle(cat.PictureBox.X,cat.PictureBox.Y -32, GameVariables.girlWidth, GameVariables.girlHeight);
                girl.Direction = cat.Direction;
            }
        }

        /// <summary>
        /// When a button is pressed, reset the original position of the girl and the cat
        /// </summary>
        public void ResetPosition()
        {
            girl.PictureBox = girl.StartPosGirl;
            cat.PictureBox = cat.StartPosCat;
        }
    }
}
