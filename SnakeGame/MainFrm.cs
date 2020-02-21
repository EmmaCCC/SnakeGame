using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class MainFrm : Form
    {
        private Timer Timer = new Timer();
        private Game Game = null;
        public MainFrm()
        {
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            this.ClientSize = new Size(500, 500);
            InitializeComponent();
            Timer.Interval = 1000 / 100;
            Timer.Tick += Timer_Tick;
            Timer.Start();
            Game = new Game(0, 0, this.ClientSize.Width+1, this.ClientSize.Height+1);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Game.Check();
            this.Invalidate();
            if (this.Game.IsOver())
            {
                this.Timer.Stop();
                MessageBox.Show("游戏结束");
                this.Game.Start();
                this.Timer.Start();
            }
          
        }

        private void MainFrm_Paint(object sender, PaintEventArgs e)
        {
            Game.Draw(e.Graphics);
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            
        }

        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                this.Game.Snake.Turn(Direction.Down);
            }
            if (e.KeyData == Keys.Up)
            {
                this.Game.Snake.Turn(Direction.Up);
            }
            if (e.KeyData == Keys.Left)
            {
                this.Game.Snake.Turn(Direction.Left);
            }
            if (e.KeyData == Keys.Right)
            {
                this.Game.Snake.Turn(Direction.Right);
            }

            if(e.KeyData == Keys.Z)
            {
                this.Game.Snake.RunFast();
            }
            if (e.KeyData == Keys.X)
            {
                this.Game.Snake.RunSlow();
            }
        }

        private void MainFrm_Resize(object sender, EventArgs e)
        {
            Game.Width = this.ClientSize.Width;
            Game.Height = this.ClientSize.Height;

        }
    }
}
