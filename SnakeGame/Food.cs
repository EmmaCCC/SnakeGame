using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Food : Block
    {
        public bool Eat { get; set; } = true;

        public Image Image { get; set; }
        public Food()
        {
            this.Image = Resources.apple;
        }

        public override void Draw(Graphics g)
        {
            if (!this.Eat)
            {
                g.DrawImage(Image, Left, Top, Width, Height);
            }
        }
    }
}
