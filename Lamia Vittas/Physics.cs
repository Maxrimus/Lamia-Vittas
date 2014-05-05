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
        public static double yVelocity = 0;
        public static bool up = true;
        public static bool jump = false;
        public static int jumpAngle = (int)(Math.PI / 4);
        public static int walkAngle = 0;
        public static int initVel = 10;
        public static float prevTimeVel = 0;
        public static float curTimeVel = 0;
        public static float prevTimeDist = 0;
        public static float curTimeDist = 0;

        public static void upDateVelocity(int tm)
        {
            curTimeVel = tm;

            double timeToUse = curTimeVel - prevTimeVel;

            if (up)
            {
                yVelocity = yVelocity + GameVariables.gravity * timeToUse;
            }
            else
            {
                yVelocity = yVelocity - GameVariables.gravity * timeToUse;
            }

            yVelocity = yVelocity/1000;

            if (yVelocity >= 0)
            {
                up = false;
            }

            prevTimeVel = curTimeVel;
        }

        public static void upDateY(GamePiece gp, int time, SpriteBatch batch)
        {
            batch.Begin();
            curTimeDist = time;
            double timeToUse = curTimeDist - prevTimeDist;

            if (jump == true)
            {
                int toChange = (int)((yVelocity * timeToUse) + (.5 * GameVariables.gravity * Math.Pow(timeToUse, 2)));
                gp.PictureBox = new Rectangle(gp.PictureBox.X, gp.PictureBox.Y + toChange, gp.PictureBox.Width, gp.PictureBox.Height);
                gp.Draw(batch);
            }
            else
            {
                gp.PictureBox = new Rectangle(gp.PictureBox.X, gp.PictureBox.Y + 10, gp.PictureBox.Width, gp.PictureBox.Height);
                gp.Draw(batch);
            }

            yVelocity = 0;

            batch.End();
            prevTimeDist = curTimeDist;
        }
    }
}
