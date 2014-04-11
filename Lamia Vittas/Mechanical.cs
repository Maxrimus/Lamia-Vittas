/*Joseph Tursi
 * Date: 4/2/2014
 * Purpose: A class for generic mechanical objects
 * Exceptions:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Text;

namespace Lamia_Vittas
{
    class Mechanical:GamePiece
    {
        public Mechanical(Rectangle picturesize, Texture2D texture)
            : base(picturesize, texture)
        {
        }
    }
}
