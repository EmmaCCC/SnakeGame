using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Block : GameObject
    {
        public Color Color = Color.Black;
        public Block()
        {

        }
        public Block(Color color)
        {
            this.Color = color;
        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(this.Color), this.GetRectangle());
        }
    }
}
