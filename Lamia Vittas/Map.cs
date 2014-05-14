using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Lamia_Vittas
{
    class Map
    {
        StreamReader input = null;

        public void ReadMap(string fileName,Game1 meow)
        {
            try
            {
                input = new StreamReader("map.txt");
                string text = "";
                while ((text = input.ReadLine()) != null)
                {
                    Console.WriteLine(text);
                    string[] ls = text.Split(',');

                    

                    if (ls[0].Contains("Block"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Platforms.Add(new Platform(new Rectangle(x, y, 25, 25), meow.Platforms[0].Image));
                    }
                    if (ls[0].Contains("Bush"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Bushes.Add(new Bush(new Rectangle(x, y, 40, 25), meow.Bushes[0].Image,0));
                    }
                    if (ls[0].Contains("Spike"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Spikes.Add(new Spike(new Rectangle(x, y, 60, 25), meow.Spikes[0].Image, 0));
                    }
                    if (ls[0].Contains("Yarn"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Yarns.Add(new Yarn(new Rectangle(x, y, 60, 25), meow.Yarns[0].Image));
                    }
                    if (ls[0].Contains("Vial"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Vials.Add(new Vial(new Rectangle(x, y, 60, 25), meow.Vials[0].Image));
                    }
                    if (ls[0].Contains("Button"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Buttons.Add(new Button(new Rectangle(x, y, 60, 25), meow.Buttons[0].Image));
                    }
                    if (ls[0].Contains("Door"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Doors.Add(new Door(new Rectangle(x, y, 60, 25), meow.Doors[0].Image,meow.Buttons[0]));
                    }
                    if (ls[0].Contains("Girl"))
                    {
                        int x;
                        Boolean parsed = int.TryParse(ls[2], out x);
                        int y;
                        parsed = int.TryParse(ls[4], out y);
                        meow.Girls.Add(new Girl(new Rectangle(x, y, 60, 25), meow.Girls[0].Image,0,250,0));
                    }
                }

                input.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Input Message: " + ioe.Message);
                Console.WriteLine("Input Stack Trace: " + ioe.StackTrace);
            }
        }
    }
}
