﻿/*Joseph Tursi
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
        /*Attributes and Properties
         * girl: The girl associated with this player
         * cat: The cat associated with this player
         * state: what state the player is in; 0 for girl, 1 for cat
         * fist: the fist associated with the girl
         * health: the girl/cat's combined health
         * lives: how many lives the girl/cat has
         * MAXHEALTH: the maximum health the characters can have
         */
        Girl girl;
        Cat cat;
        public Fist fist;
        public int state;
        int lives;
        bool canheal;

        public bool Canheal
        {
            get { return canheal; }
            set { canheal = value; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="g1">Girl object associated with this player</param>
        /// <param name="c1">Cat object associated with this player</param>
        /// <param name="st">State the player is in</param>
        public Player(Girl g1, Cat c1, int st,Fist f1)
        {
            girl = g1;
            cat = c1;
            fist = f1;
            state = st;
            lives = girl.Lives;
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
        /// The player takes damage
        /// </summary>
        /// <param name="ll">the amount of lives lost</param>
        public void TakeHit(int ll)
        {
            //takes damage
            lives = lives - ll;
            Die();            
        }

        /// <summary>
        /// The player gains lives
        /// </summary>
        /// <param name="lg">amount of lives gained</param>
        public void GainLife(int lg)
        {
            if (canheal == true)
            {
                lives = lives + 1;
            }
        }

        /// <summary>
        /// The player Dies
        /// </summary>
        public void Die()
        {
            // reset the postition
            ResetPosition();
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

        /// <summary>
        /// Sets the position to a passed in rectangle
        /// </summary>
        /// <param name="r1">The new position</param>
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

        /// <summary>
        /// Returns the current image displayed
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the girl or the cat
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns whether or not the two gamepieces are colliding
        /// </summary>
        /// <param name="gp2">The gamepiece to compare to</param>
        /// <returns></returns>
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
                fist.UnPunch();
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
