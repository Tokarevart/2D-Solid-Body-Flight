using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace _2D_Solid_Body_Flight
{
    public partial class MainWindow : Window
    {
        public static double scale = 1, startTime, Vel0X, Vel0Y, Y0, stopTime;
        public static bool timerFlag = false, progStarted = false;
        DispatcherTimer tmr = new DispatcherTimer();
        double[] TrajPointsX = new double[0];
        double[] TrajPointsY = new double[0];

        public MainWindow()
        {
            InitializeComponent();
            Vel0X = Convert.ToDouble(Vel0Xtb.Text);
            Vel0Y = Convert.ToDouble(Vel0Ytb.Text);
            Y0 = Convert.ToDouble(Y0tb.Text);
            tmr.Interval = new TimeSpan(0, 0, 0, 0, 5);
            body.Margin = new Thickness(67, 497 - Y0 * 100 * scale, 0, 0);
            trajpl.Points.Clear();
            progStarted = true;
        }

        private void scalePlusLabelsAndVariable()
        {
            scale *= 2;
            Ylabel1.Content = Convert.ToDouble(Ylabel1.Content) / 2;
            Ylabel2.Content = Convert.ToDouble(Ylabel2.Content) / 2;
            Ylabel3.Content = Convert.ToDouble(Ylabel3.Content) / 2;
            Ylabel4.Content = Convert.ToDouble(Ylabel4.Content) / 2;
            Xlabel1.Content = Convert.ToDouble(Xlabel1.Content) / 2;
            Xlabel2.Content = Convert.ToDouble(Xlabel2.Content) / 2;
            Xlabel3.Content = Convert.ToDouble(Xlabel3.Content) / 2;
            Xlabel4.Content = Convert.ToDouble(Xlabel4.Content) / 2;
            Xlabel5.Content = Convert.ToDouble(Xlabel5.Content) / 2;
            Xlabel6.Content = Convert.ToDouble(Xlabel6.Content) / 2;
            Xlabel7.Content = Convert.ToDouble(Xlabel7.Content) / 2;
        }

        private void scaleMinusLabelsAndVariable()
        {
            scale /= 2;
            Ylabel1.Content = Convert.ToDouble(Ylabel1.Content) * 2;
            Ylabel2.Content = Convert.ToDouble(Ylabel2.Content) * 2;
            Ylabel3.Content = Convert.ToDouble(Ylabel3.Content) * 2;
            Ylabel4.Content = Convert.ToDouble(Ylabel4.Content) * 2;
            Xlabel1.Content = Convert.ToDouble(Xlabel1.Content) * 2;
            Xlabel2.Content = Convert.ToDouble(Xlabel2.Content) * 2;
            Xlabel3.Content = Convert.ToDouble(Xlabel3.Content) * 2;
            Xlabel4.Content = Convert.ToDouble(Xlabel4.Content) * 2;
            Xlabel5.Content = Convert.ToDouble(Xlabel5.Content) * 2;
            Xlabel6.Content = Convert.ToDouble(Xlabel6.Content) * 2;
            Xlabel7.Content = Convert.ToDouble(Xlabel7.Content) * 2;
        }

        private void scalePlusbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!timerFlag)
            {
                scalePlusLabelsAndVariable();

                if (Y0tb.Text.Length != 0) Y0 = Convert.ToDouble(Y0tb.Text);
                try { body.Margin = new Thickness(67, 497 - Y0 * 100 * scale, 0, 0); }
                catch (Exception) { }
                trajpl.Points.Clear();
            }
        }

        private void scaleMinusbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!timerFlag)
            {
                scaleMinusLabelsAndVariable();

                if (Y0tb.Text.Length != 0) Y0 = Convert.ToDouble(Y0tb.Text);
                try { body.Margin = new Thickness(67, 497 - Y0 * 100 * scale, 0, 0); }
                catch (Exception) { }
                trajpl.Points.Clear();
            }
        }

        private void Y0tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!timerFlag)
            {
                if (Y0tb.Text.Length != 0) Y0 = Convert.ToDouble(Y0tb.Text);
                try
                {
                    while (4 / scale < Convert.ToDouble(Y0tb.Text) + Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2) / 19.62 || 8 / scale <
                        (Convert.ToDouble(Vel0Ytb.Text) + Math.Sqrt(Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2)
                        + 19.62 * Convert.ToDouble(Y0tb.Text))) * Convert.ToDouble(Vel0Xtb.Text) / 9.81)
                    {
                        scaleMinusLabelsAndVariable();
                    }
                    while (4 / scale > Convert.ToDouble(Y0tb.Text) + Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2) / 19.62 && 8 / scale >
                        (Convert.ToDouble(Vel0Ytb.Text) + Math.Sqrt(Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2)
                        + 19.62 * Convert.ToDouble(Y0tb.Text))) * Convert.ToDouble(Vel0Xtb.Text) / 9.81)
                    {
                        scalePlusLabelsAndVariable();
                    }
                    scaleMinusLabelsAndVariable();

                    if (Y0tb.Text.Length != 0) Y0 = Convert.ToDouble(Y0tb.Text);
                    try { body.Margin = new Thickness(67, 497 - Y0 * 100 * scale, 0, 0); }
                    catch (Exception) { }
                    trajpl.Points.Clear();
                }
                catch (Exception) { }
            }
        }

        private void Vel0Xtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!timerFlag && progStarted)
            {
                VelXtb.Text = Vel0Xtb.Text;
                try
                {
                    while (4 / scale < Convert.ToDouble(Y0tb.Text) + Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2) / 19.62 || 8 / scale <
                        (Convert.ToDouble(Vel0Ytb.Text) + Math.Sqrt(Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2)
                        + 19.62 * Convert.ToDouble(Y0tb.Text))) * Convert.ToDouble(Vel0Xtb.Text) / 9.81)
                    {
                        scaleMinusLabelsAndVariable();
                    }
                    while (4 / scale > Convert.ToDouble(Y0tb.Text) + Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2) / 19.62 && 8 / scale >
                        (Convert.ToDouble(Vel0Ytb.Text) + Math.Sqrt(Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2)
                        + 19.62 * Convert.ToDouble(Y0tb.Text))) * Convert.ToDouble(Vel0Xtb.Text) / 9.81)
                    {
                        scalePlusLabelsAndVariable();
                    }
                    scaleMinusLabelsAndVariable();

                    if (Y0tb.Text.Length != 0) Y0 = Convert.ToDouble(Y0tb.Text);
                    try { body.Margin = new Thickness(67, 497 - Y0 * 100 * scale, 0, 0); }
                    catch (Exception) { }
                    trajpl.Points.Clear();
                }
                catch (Exception) { }
            }
        }

        private void Vel0Ytb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!timerFlag)
            {
                VelYtb.Text = Vel0Ytb.Text;
                try
                {
                    while (4 / scale < Convert.ToDouble(Y0tb.Text) + Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2) / 19.62 || 8 / scale <
                        (Convert.ToDouble(Vel0Ytb.Text) + Math.Sqrt(Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2)
                        + 19.62 * Convert.ToDouble(Y0tb.Text))) * Convert.ToDouble(Vel0Xtb.Text) / 9.81)
                    {
                        scaleMinusLabelsAndVariable();
                    }
                    while (4 / scale > Convert.ToDouble(Y0tb.Text) + Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2) / 19.62 && 8 / scale > 
                        (Convert.ToDouble(Vel0Ytb.Text) + Math.Sqrt(Math.Pow(Convert.ToDouble(Vel0Ytb.Text), 2) 
                        + 19.62 * Convert.ToDouble(Y0tb.Text))) * Convert.ToDouble(Vel0Xtb.Text) / 9.81)
                    {
                        scalePlusLabelsAndVariable();
                    }
                    scaleMinusLabelsAndVariable();

                    if (Y0tb.Text.Length != 0) Y0 = Convert.ToDouble(Y0tb.Text);
                    try { body.Margin = new Thickness(67, 497 - Y0 * 100 * scale, 0, 0); }
                    catch (Exception) { }
                    trajpl.Points.Clear();
                }
                catch (Exception) { }
            }
        }
        
        private void trajCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (trajCheck.IsChecked.Value) trajpl.Visibility = Visibility.Visible;
                else trajpl.Visibility = Visibility.Hidden;
            }
            catch (Exception) { }
        }

        public static double t() //Время с начала движения
        {
            return (DateTime.Now.Millisecond + DateTime.Now.Second * 1000
                + DateTime.Now.Minute * 60000 + DateTime.Now.Hour * 3600000 - startTime) / 1000;
        }
        
        public static double MatXY(string s)
        {
            if (s == "x") return Vel0X * t();
            else return Vel0Y * t() - 4.905 * t() * t();
        }

        public static double PixelXYRelTo0(string s)
        {
            if (s == "x") return MatXY("x") * 100 * scale;
            else return MatXY("y") * 100 * scale;
        }

        public static double VelY()
        {
            return Vel0Y - 9.81 * t();
        }

        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!timerFlag)
            {
                moveTimetb.Text = "0";
                Ytb.Text = Y0tb.Text;
                Vel0X = Convert.ToDouble(Vel0Xtb.Text);
                Vel0Y = Convert.ToDouble(Vel0Ytb.Text);
                Y0 = Convert.ToDouble(Y0tb.Text);
                body.Visibility = Visibility.Visible;
                if (trajCheck.IsChecked.Value) trajpl.Visibility = Visibility.Visible;
                else trajpl.Visibility = Visibility.Hidden;
                trajpl.Points.Clear();
                trajpl.Points.Add(new Point(80, 510 - Y0 * 100 * scale));
                timerFlag = true;
                startTime = DateTime.Now.Millisecond + DateTime.Now.Second * 1000 
                    + DateTime.Now.Minute * 60000 + DateTime.Now.Hour * 3600000;
                stopTime = (Vel0Y + Math.Sqrt(Vel0Y * Vel0Y + 19.62 * Y0)) / 9.81;
                tmr.Tick += new EventHandler((o, ev) =>
                            {
                                body.Margin = new Thickness(67 + PixelXYRelTo0("x"), 497 - Y0 * 100 * scale - PixelXYRelTo0("y"), 0, 0);
                                if (body.Margin.Left + body.Width > coorGrid.Width || body.Margin.Top < 0) body.Visibility = Visibility.Hidden;
                                else body.Visibility = Visibility.Visible;
                                moveTimetb.Text = Convert.ToString(t());
                                Xtb.Text = Convert.ToString(MatXY("x"));
                                Ytb.Text = Convert.ToString(MatXY("y"));
                                VelYtb.Text = Convert.ToString(VelY());
                                if (t() < stopTime) trajpl.Points.Add(new Point(80 + PixelXYRelTo0("x"), 510 - Y0 * 100 * scale - PixelXYRelTo0("y")));
                                if (t() >= stopTime)
                                {
                                    body.Margin = new Thickness(67 + Vel0X * stopTime * 100 * scale, 497, 0, 0);
                                    trajpl.Points.Add(new Point(80 + Vel0X * stopTime * 100 * scale, 510));
                                    moveTimetb.Text = Convert.ToString(stopTime);
                                    Ytb.Text = "0";
                                    VelYtb.Text = Convert.ToString(Vel0Y - 9.81 * stopTime);
                                    timerFlag = false;
                                    tmr.Stop();
                                }
                            });
                tmr.Start();
            }
        }

        private void stopbtn_Click(object sender, RoutedEventArgs e)
        {
            tmr.Stop();
            timerFlag = false;
            body.Margin = new Thickness(67, 497 - Y0 * 100 * scale, 0, 0);
            body.Visibility = Visibility.Visible;
            moveTimetb.Text = "0";
            Ytb.Text = Y0tb.Text;
            Xtb.Text = "0";
            VelXtb.Text = Vel0Xtb.Text;
            VelYtb.Text = Vel0Ytb.Text;
            trajpl.Points.Clear();
        }
    }
}
