﻿/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A class for buttons
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
    class Button:Mechanical
    {

        /// <summary>
        /// Parameterized Constructor, all values passed up
        /// </summary>
        /// <param name="picturesize">Rectangle that holds button information</param>
        /// <param name="texture">Button texture</param>
        public Button(Rectangle picturesize, Texture2D texture)
            : base(picturesize, texture)
        {
        }
    }
}
