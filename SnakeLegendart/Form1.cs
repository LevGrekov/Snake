namespace SnakeLegendart
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelscore;
        private int dirX, dirY;
        private int _width = 700;
        private int _height = 600;
        private int _sizeOfSides = 40;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            this.Width = _width + _sizeOfSides;
            this.Height = _height + _sizeOfSides * 2;
            this.Text = "Snake";
            dirX = 1;
            dirY = 0;
            labelscore = new Label();
            labelscore.Text = "Score: 0";
            labelscore.Location = new Point(_width - 90, 10);
            this.Controls.Add(labelscore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);
            snake[0].BackColor = Color.Blue;
            this.Controls.Add(snake[0]);
            _generatemap();
            fruit = new PictureBox();
            fruit.BackColor = Color.IndianRed;
            fruit.Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);
            timer.Tick += new EventHandler(_update);
            timer.Interval = 200;
            timer.Start();
            _generatefruit();
            this.KeyDown += new KeyEventHandler(Interact);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool _eatitself()
        {
            for (int i = 1; i < score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    return true;
                }
            }
            return false;
        }
        private void _getinborder()
        {
            if (snake[0].Location.X < 0)
            {
                snake[0].Location = new Point(_width - 100 - _sizeOfSides + 1, snake[0].Location.Y);
            }
            if (snake[0].Location.Y < 0)
            {
                snake[0].Location = new Point(snake[0].Location.X, _height - _sizeOfSides + 1);
            }
            if (snake[0].Location.X > _width - 100)
            {
                snake[0].Location = new Point(1, snake[0].Location.Y);
            }
            if (snake[0].Location.Y > _height)
            {
                snake[0].Location = new Point(snake[0].Location.X, 1);
            }

        }
        private Point _generatePoint()
        {
            Random r = new Random();
            rI = r.Next(0, _height - _sizeOfSides);
            int tempI = rI % _sizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, _height - _sizeOfSides);
            int tempJ = rJ % _sizeOfSides;
            rJ -= tempJ;
            rI++;
            rJ++;
            return new Point(rI, rJ);
        }
        private void _generatefruit()
        {
            var pos = _generatePoint();
            bool flag = false;
            for (int i = 0; i < score; i++)
            {
                if (snake[i].Location == pos)
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                fruit.Location = pos;
                fruit.BackColor = Color.YellowGreen;
                this.Controls.Add(fruit);
            }
            else
            {
                _generatefruit();
            }
        }
        private void _eatfruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelscore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + _sizeOfSides * dirX, snake[score - 1].Location.Y - _sizeOfSides * dirY);
                snake[score].Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                _generatefruit();
            }
        }
        private void _moveSnake()
        {
            for (int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * _sizeOfSides, snake[0].Location.Y + dirY * _sizeOfSides);

        }
        private void _update(Object myObject, EventArgs eventsArgs)
        {
            _eatfruit();
            _moveSnake();
            _getinborder();
            if (_eatitself())
            {
                timer.Stop();
                dirX = 0;
                dirY = 0;
                var end = new Button();
                _endGame();
            }
        }
        private void _endGame()
        {
            if (MessageBox.Show($"Выш рекорд: {score}", "Вы Проиграли") == DialogResult.OK)
            {
                this.Close();
            }

        }
        private void _generatemap()
        {
            for (int i = 0; i < (_width / _sizeOfSides); i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * i);
                pic.Size = new Size(_width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _height / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * i, 0);
                pic.Size = new Size(1, _height);
                this.Controls.Add(pic);
            }

        }
        private void Interact(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }

    }
}