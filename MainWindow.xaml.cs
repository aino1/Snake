using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace TheTrueSnakes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        string D ="RIGHT";
        bool IsChange = false;
        class Base
        {
            public string Diraction;
            public List<Point> SnakesPoints = new List<Point>();
            public void MoveBase()
            {
                for(int i = 0; i < SnakesPoints.Count; i++)
                {

                    if (Diraction == "RIGHT")
                    {
                        Point test = new Point(SnakesPoints[i].X+20, SnakesPoints[i].Y);
                        SnakesPoints[i] = test;
                    }
                    if (Diraction == "LEFT")
                    {
                        Point test = new Point(SnakesPoints[i].X - 20, SnakesPoints[i].Y);
                        SnakesPoints[i] = test;
                    }
                    if (Diraction == "UP")
                    {
                        Point test = new Point(SnakesPoints[i].X , SnakesPoints[i].Y-20);
                        SnakesPoints[i] = test;
                    }
                    if (Diraction == "DOWN")
                    {
                        Point test = new Point(SnakesPoints[i].X , SnakesPoints[i].Y+20);
                        SnakesPoints[i]= test;
                    }
                }
            }
        }
        class Snake
        {
            public
                List<Base> SnakesBases = new List<Base>();
            bool IsChange = false;
            bool IsDelete = false;
            public Snake()
            {
                Base S = new Base();
                Point Coordinates = new Point(10, 0);
                Point Coordinates2 = new Point(30, 0);
                Point Coordinates3 = new Point(50, 0);
                Point Coordinates4 = new Point(70, 0);
                Point Coordinates5 = new Point(90, 0);
                Point Coordinates6 = new Point(110, 0);
                Point Coordinates7 = new Point(130, 0);
                Point Coordinates8 = new Point(150, 0);
                S.Diraction = "RIGHT";

                S.SnakesPoints.Add(Coordinates);
                S.SnakesPoints.Add(Coordinates2);           ////Конструктор создания двойной змейки
                S.SnakesPoints.Add(Coordinates3);
                S.SnakesPoints.Add(Coordinates4);
                S.SnakesPoints.Add(Coordinates5);
                S.SnakesPoints.Add(Coordinates6);
                S.SnakesPoints.Add(Coordinates7);
                S.SnakesPoints.Add(Coordinates8);

                SnakesBases.Add(S);
            }
            public  string Info()
            {
                string Intro="";
                for(int i = 0; i < SnakesBases.Count; i++)
                {
                    Intro = Convert.ToString(i) + "Звено " + '\n';
                    
                    for(int j = 0; j < SnakesBases[i].SnakesPoints.Count; j++)
                    {
                        Intro += Convert.ToString(SnakesBases[i].SnakesPoints[j].X)+"  ";
                        Intro += Convert.ToString(SnakesBases[i].SnakesPoints[j].Y) + "  " + '\n';
                    }
                    
                }
                return Intro;
            }
            public void MoveToDiraction()
            {
                CheckOnVoid();
                SnakesBases[SnakesBases.Count-1].MoveBase();
                if (SnakesBases.Count > 1)
                {
                    for(int i = SnakesBases.Count - 2; i >= 0; i--)
                    {
                        SnakesBases[i].MoveBase();
                        SnakesBases[i+1].SnakesPoints.Insert(0,SnakesBases[i].SnakesPoints[SnakesBases[i].SnakesPoints.Count-1]);
                        SnakesBases[i].SnakesPoints.RemoveAt(SnakesBases[i].SnakesPoints.Count - 1);
                    }
                }
            }
            public void ChangeDirection(string Dir)
            {
                CheckOnVoid();
                if (IsChange == true)
                {
                        ///SnakesBases[SnakesBases.Count - 1].SnakesPoints.Reverse();
                        ///if(SnakesBases.Count == 3)
                        ///SnakesBases[SnakesBases.Count - 2].SnakesPoints.Reverse();
                }
                else
                    IsChange = true;
                Base s = new Base();
                s.Diraction = Dir;
                int t = SnakesBases[SnakesBases.Count - 1].SnakesPoints.Count;
                s.SnakesPoints.Add(SnakesBases[SnakesBases.Count - 1].SnakesPoints[t-1]);
                SnakesBases[SnakesBases.Count - 1].SnakesPoints.RemoveAt(SnakesBases[SnakesBases.Count - 1].SnakesPoints.Count-1 );
                SnakesBases.Add(s);
                //if(IsChange)
                ///    SnakesBases[SnakesBases.Count - 2].SnakesPoints.Reverse();
            }
            public void CheckOnVoid()
            {
                if (SnakesBases[SnakesBases.Count - 1].SnakesPoints.Count == 0)
                {
                    SnakesBases.Remove(SnakesBases[SnakesBases.Count - 1]);
                    IsDelete = true;
                }

                if (SnakesBases[0].SnakesPoints.Count == 0)
                {
                    IsDelete = true;
                    SnakesBases.Remove(SnakesBases[0]);
                }
            }

        }
        DispatcherTimer timer = new DispatcherTimer();
        ///timer.Interval = new Timespan(0, 0, 0, 1, 0);
        int x = 10;
        int y = 10;
        int xchange = 10;
        int ychange = 0;
        Point FirstSnakePoint = new Point(1, 1);
        Point SecondSnakePoint = new Point(20, 20);
        Snake FirstSnake = new Snake();
        public MainWindow()
        {  
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.IsEnabled = true;

            InitializeComponent();
            
            timer.Tick += DrawPlain;

        }


        void DrawPlain(object sender, object e)
        {
            if (IsChange)
            {
                FirstSnake.ChangeDirection(D);
                IsChange = false;
            }
            FirstSnake.MoveToDiraction();
            Test.Text = FirstSnake.Info();
            GamePlain.Children.Clear();
            ///Создаём новую змейку
            for (int i = 0; i < FirstSnake.SnakesBases.Count; i++)
            {
                for(int j=0;j < FirstSnake.SnakesBases[i].SnakesPoints.Count; j++)
                {
                    {
                        Rectangle Ro = new Rectangle();
                        Ro.StrokeThickness = 1;
                        Ro.Stroke = new SolidColorBrush(Colors.Blue);
                        Ro.Fill = new SolidColorBrush(Colors.Cyan);
                        Ro.Height = 20;
                        Ro.Width = 20;
                        Canvas.SetLeft(Ro, FirstSnake.SnakesBases[i].SnakesPoints[j].X);
                        Canvas.SetTop(Ro, FirstSnake.SnakesBases[i].SnakesPoints[j].Y);
                        GamePlain.Children.Add(Ro);
                    }
                }

            }
            ///Конец создания змейки
            Rectangle R = new Rectangle();
            R.StrokeThickness = 1;
            R.Stroke = new SolidColorBrush(Colors.Blue);
            R.Fill = new SolidColorBrush(Colors.Cyan);
            R.Height = 20;
            R.Width = 20;
            
            Canvas.SetLeft(R, 100);
            Canvas.SetTop(R, 100);
            GamePlain.Children.Add(R);
            x+=xchange;
            y +=ychange;
            ///
        }
        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                ychange += 10;
            }
            if (e.Key == Key.D)
                xchange += 10;
            if (e.Key == Key.A)
                ychange += -10;
            if (e.Key == Key.S)
                xchange += -10;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.Key == Key.W)
                {
                    ychange = -10;
                    xchange = 0;
                    D = "UP";
                    IsChange = true;
                    
                }
                if (e.Key == Key.D)
                {
                    ychange = 0;
                    xchange = 10;
                    D = "RIGHT";
                    IsChange = true;

                }
                if (e.Key == Key.A)
                {
                    xchange = -10;
                    ychange = 0;
                    D = "LEFT";
                    IsChange = true;
                }
                if (e.Key == Key.S)
                {
                    ychange = 10;
                    xchange = 0;
                    D = "DOWN";
                    IsChange = true;
                }
            }
        }

        private void Reserve_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
    }

}
