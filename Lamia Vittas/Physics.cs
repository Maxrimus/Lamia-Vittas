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
    class Physics
    {
        public double velocity;
        public bool up = true;

        public void upDateVelocity(int acc)
        {
            if (velocity > 0 && up)
            {
                velocity -= acc;
            }
            else if (velocity == 0)
            {
                up = false;
            }
            else
            {
                velocity += acc;
            }
        }

        public void upDateY(GamePiece gp)
        {
            gp.PictureBox = new Rectangle(gp.PictureBox.X,(int)(gp.PictureBox.Y + velocity),gp.PictureBox.Width,gp.PictureBox.Height);
        }
    }
}
