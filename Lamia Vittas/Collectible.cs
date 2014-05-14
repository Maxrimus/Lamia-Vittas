/* Greg Bednarowicz
 * Date: 4/25/2014
 * Purpose: Hold all the collectible items in the game
 * Exceptions:
 */

#region Using Statements
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
#endregion

namespace Lamia_Vittas
{
    class Collectible:GamePiece
    {
        // attribute to determine if a collectible has been collected
        bool collected;
        string style;

        // property for the collected
        public bool Collected
        {
            get { return collected; }
            set { collected = value; }
        }

        // property for the style, either yarn or vial
        public string Style
        {
            get {return style;}
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="picturesize">size and location of the collectible</param>
        /// <param name="texture">image of the collectible</param>
        public Collectible(Rectangle picturesize, Texture2D texture, bool col,string st)
            : base(picturesize, texture)
        {
            collected = col;
            style = st;
        }
    }
}
