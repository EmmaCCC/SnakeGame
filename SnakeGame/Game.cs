using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Game : GameObject
    {
        public Snake Snake { get; private set; }

        public Food Food { get; private set; }
        public int Score { get; private set; }

        Random r = new Random();

        public Game(int top, int left, int width, int height)
        {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
            Init();
        }

        private void Init()
        {
            this.Score = 0;
            this.Snake = new Snake(5);
            this.Snake.Game = this;
            this.Food = new Food();
            Food.Width = Food.Height = this.Snake.BodySize;
            this.GenerateFood();
        }
        public override void Draw(Graphics g)
        {
            this.Snake.Draw(g);
            this.Food.Draw(g);
            g.DrawString($"分数：{Score}（'Z'加速，'X'减速）", new Font("微软雅黑", 15), Brushes.Black, new Point(this.Left + 5, this.Height - 30));
        }

        public void GenerateFood()
        {
            if (Food.Eat)
            {
                Food.Left = r.Next(0, this.Width - this.Snake.BodySize);
                Food.Top = r.Next(0, this.Height - this.Snake.BodySize);
                Food.Eat = false;
            }
        }

        public void Check()
        {

            if (this.Snake.EatFood(Food))
            {
                this.Score += 10;
                Food.Eat = true;
                this.GenerateFood();
                this.Snake.Grown();
            }

        }

        public bool IsOver()
        {
            return this.Snake.Dead();
        }
        public void Start()
        {
            this.Init();
        }
    }
}
