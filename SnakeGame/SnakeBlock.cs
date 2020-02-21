using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class SnakeBlock : Block
    {
        public bool Hit { get; set; }
        public Image Image { get; set; }

        public Direction Direction { get; set; }
        public SnakeBlock()
        {
            this.Image = Resources.bodyright;
        }
       

        public override void Draw(Graphics g)
        {
            g.DrawImage(Image, Left, Top, Width, Height);
        }
    }
}
