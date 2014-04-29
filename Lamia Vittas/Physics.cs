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
    public static class Physics
    {
        public static double xVelocity;
        public static double yVelocity;
        public static bool up = true;
        public static bool jump = false;
        public static int jumpAngle = (int)(Math.PI / 4);
        public static int walkAngle = 0;
        public static int initVel = 10;
        public static double time = 0;

        public static void upDateVelocity(double tm)
        {
            time += tm;

            if (up)
            {
                if (jump)
                {
                    yVelocity = (Math.Sin(jumpAngle) * initVel * time) - (.5 * GameVariables.gravity * Math.Pow(time, 2));
                }
                else
                {
                    yVelocity = (Math.Sin(jumpAngle) * initVel * time) - (.5 * GameVariables.gravity * Math.Pow(time, 2));
                }
            }
            else
            {
                if (jump)
                {
                    yVelocity = (Math.Sin(jumpAngle) * initVel * time) - (.5 * -GameVariables.gravity * Math.Pow(time, 2));
                }
                else
                {
                    yVelocity = (Math.Sin(jumpAngle) * initVel * time) - (.5 * -GameVariables.gravity * Math.Pow(time, 2));
                }
            }

            if (yVelocity <= 0)
            {
                up = false;
            }

            xVelocity = Math.Cos(jumpAngle) * initVel * time;
        }

        public static void upDateY(GamePiece gp)
        {
            gp.PictureBox = new Rectangle((int)(gp.PictureBox.X + xVelocity),(int)(gp.PictureBox.Y + yVelocity),gp.PictureBox.Width,gp.PictureBox.Height);
        }
    }
}
