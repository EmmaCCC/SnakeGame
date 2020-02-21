using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public abstract class GameObject
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle GetRectangle()
        {
            return new Rectangle(Left, Top, Width, Height); ;
        }
        public abstract void Draw(Graphics g);
    }
}
