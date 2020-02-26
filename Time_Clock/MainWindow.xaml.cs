using DocumentFormat.OpenXml.Bibliography;
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
using System.Threading;
using Time_TimePeriod;
using System.ComponentModel;
using System.Windows.Threading;

namespace Time_Clock
{

    public partial class MainWindow : Window
    {
        public bool ThreadActive;
        public bool ThreadStopperActive;
        public bool activeStart = true;

        public MainWindow()
        {
            InitializeComponent();  
        }

        public void showTime()
        {
            while(ThreadActive)
            {
                Time now = tick();

                Dispatcher.Invoke(() =>
                {
                    Tdisplay.Content = now.ToString();
                });
                Thread.Sleep(1000);
            }
        }
        public int count = 0;
        public void showStoper()
        {
            while(ThreadStopperActive)
            {
                TimePeriod start = ticking();
                Dispatcher.Invoke(() =>
                {
                    Stopper.Content = start.ToString();
                });
                Thread.Sleep(1000);
            }
        }
        public int other;
        TimePeriod ticking()
        {
            other = 0;
            count++;
            if (count == 254)
                other = count;

            return new TimePeriod(0, 0, Convert.ToByte(count)).Plus(new TimePeriod(0,0,Convert.ToByte(other)));
        }
        Time tick()
        {
            DateTime dt = DateTime.Now;
            byte hour = Convert.ToByte(dt.Hour);
            byte minute = Convert.ToByte(dt.Minute);
            byte seconds = Convert.ToByte(dt.Second);
            return new Time(hour, minute, seconds);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tick();
            ThreadActive = true;
            Thread thread = new Thread(showTime);
            thread.Start();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ThreadActive = false;
            ThreadStopperActive = false;
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            string[] timeAr1 = Tdisplay.Content.ToString().Split(":");
            string[] timeAr2 = TimeInput.Text.Split(":");
            Time t1 = new Time(Convert.ToByte(timeAr1[0]), Convert.ToByte(timeAr1[1]), Convert.ToByte(timeAr1[2]));
            Time t2 = new Time(Convert.ToByte(timeAr2[0]), Convert.ToByte(timeAr2[1]), Convert.ToByte(timeAr2[2]));

            if (t1 > t2) Title.Content = "Result: Input is lower than current Time";
            else if (t1 < t2) Title.Content = "Result: Input is bigger than current Time";
            else Title.Content = "Result: Input equals current Time";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (activeStart)
            {
                ticking();
                ThreadStopperActive = true;
                Thread thr = new Thread(showStoper);
                thr.Start();
                activeStart = false;
                StartButton.Content = "STOP";
            }
            else
            {
                ThreadStopperActive = false;
                activeStart = true;
                StartButton.Content = "START";
            }


        }
    }
}
