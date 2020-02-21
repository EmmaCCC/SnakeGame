using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Snake : GameObject
    {
        public static readonly Dictionary<string, Image> HeadImages = new Dictionary<string, Image>()
        {
            {"Left",Resources.headleft },
            {"Right",Resources.headright },
            {"Down",Resources.headdown },
            {"Up",Resources.headup },
        };
        public static readonly Dictionary<string, Image> BodyImages = new Dictionary<string, Image>()
        {

            {"Left",Resources.bodyleft },
            {"Right",Resources.bodyright },
            {"Down",Resources.bodydown },
            {"Up",Resources.bodyup },
        };

        public Direction originDirection = Direction.Right;
        public Direction currentDirection = Direction.Right;

        public int Speed;
        public int BodySize;

        public List<SnakeBlock> SnakeBlocks = new List<SnakeBlock>();

        public Game Game = null;

        private Timer timer = new Timer();

        public Snake(int bodyCount, int bodySize = 35)
        {

            int headTop = 0, headLeft = (bodyCount - 1) * bodySize;
            this.BodySize = bodySize;
            this.Speed = bodySize;

            this.SnakeBlocks = new List<SnakeBlock>(bodyCount);
            for (int i = 0; i < bodyCount; i++)
            {
                this.SnakeBlocks.Add(new SnakeBlock()
                {
                    Top = headTop,
                    Left = headLeft - i * bodySize,
                    Width = bodySize,
                    Height = bodySize,
                    Direction = Direction.Right,
                });
            }

            var head = this.SnakeBlocks[0];
            head.Image = HeadImages[head.Direction.ToString()];

            timer.Tick += Timer_Tick;
            timer.Interval = 500;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Move();
        }

        public void Move()
        {
            var head = this.SnakeBlocks[0];

            var length = this.SnakeBlocks.Count;
            for (int i = length - 1; i > 0; i--)
            {
                this.SnakeBlocks[i].Left = this.SnakeBlocks[i - 1].Left;
                this.SnakeBlocks[i].Top = this.SnakeBlocks[i - 1].Top;
                this.SnakeBlocks[i].Direction = this.SnakeBlocks[i - 1].Direction;
                var item = this.SnakeBlocks[i];
                item.Image = BodyImages[item.Direction.ToString()];
            }
            head.Image = HeadImages[head.Direction.ToString()];

            switch (originDirection)
            {
                case Direction.Down:
                    {
                        if (this.currentDirection == Direction.Down || this.currentDirection == Direction.Up)
                        {
                            head.Top += Speed;
                        }
                        if (this.currentDirection == Direction.Left)
                        {
                            head.Left -= Speed;
                        }
                        if (this.currentDirection == Direction.Right)
                        {
                            head.Left += Speed;
                        }
                    }
                    break;
                case Direction.Up:
                    {
                        if (this.currentDirection == Direction.Down || this.currentDirection == Direction.Up)
                        {
                            head.Top -= Speed;
                        }
                        if (this.currentDirection == Direction.Left)
                        {
                            head.Left -= Speed;
                        }
                        if (this.currentDirection == Direction.Right)
                        {
                            head.Left += Speed;
                        }
                    }
                    break;

                case Direction.Left:
                    {
                        if (this.currentDirection == Direction.Left || this.currentDirection == Direction.Right)
                        {
                            head.Left -= Speed;
                        }

                        if (this.currentDirection == Direction.Down)
                        {
                            head.Top += Speed;
                        }
                        if (this.currentDirection == Direction.Up)
                        {
                            head.Top -= Speed;
                        }


                    }
                    break;
                case Direction.Right:
                    {
                        if (this.currentDirection == Direction.Left || this.currentDirection == Direction.Right)
                        {
                            head.Left += Speed;
                        }

                        if (this.currentDirection == Direction.Down)
                        {
                            head.Top += Speed;
                        }
                        if (this.currentDirection == Direction.Up)
                        {
                            head.Top -= Speed;
                        }


                    }
                    break;
                default:
                    break;

            }



            if (head.Top < 0)
            {
                head.Top = this.Game.Height - this.BodySize;
            }
            if (head.Top > this.Game.Height - this.BodySize)
            {
                head.Top = 0;
            }
            if (head.Left < 0)
            {
                head.Left = this.Game.Width - this.BodySize;
            }
            if (head.Left > this.Game.Width - this.BodySize)
            {
                head.Left = 0;
            }
        }

        public void Turn(Direction direction)
        {
            this.originDirection = this.currentDirection;
            this.currentDirection = direction;
            if ((originDirection == Direction.Left || originDirection == Direction.Right)
                && (direction == Direction.Left || direction == Direction.Right))
            {
                this.currentDirection = direction = this.originDirection;
            }
            if ((originDirection == Direction.Up || originDirection == Direction.Down)
                && (direction == Direction.Up || direction == Direction.Down))
            {
                this.currentDirection = direction = this.originDirection;
            }
            this.timer.Start();

            var head = this.SnakeBlocks[0];
          
            head.Direction = this.currentDirection;
        }

        public void Grown()
        {
            var last = this.SnakeBlocks.Last();
            this.SnakeBlocks.Add(new SnakeBlock()
            {
                Top = last.Top,
                Left = last.Left,
                Width = last.Width,
                Height = last.Height
            });
        }
        public override void Draw(Graphics g)
        {
            foreach (var item in this.SnakeBlocks)
            {
                item.Draw(g);
            }
        }

        public void RunFast()
        {
            this.timer.Interval = this.timer.Interval / 2;
        }

        public void RunSlow()
        {
            this.timer.Interval = this.timer.Interval * 2;
            this.timer.Interval = Math.Min(1000, this.timer.Interval);
        }

        public bool Dead()
        {
            var head = this.SnakeBlocks[0];
            for (int i = 2; i < this.SnakeBlocks.Count; i++)
            {
                var item = this.SnakeBlocks[i];
                if (head.GetRectangle().IntersectsWith(item.GetRectangle()))
                {
                    item.Hit = true;
                    return true;
                }
            }
            return false;
        }

        public bool EatFood(Food food)
        {
            var head = this.SnakeBlocks[0];
            return head.GetRectangle().IntersectsWith(food.GetRectangle());
        }
    }


    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
