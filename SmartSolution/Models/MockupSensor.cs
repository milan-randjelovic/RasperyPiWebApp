using System;
using System.Timers;

namespace WebPortal.Models
{
    public class MockupSensor : Sensor
    {
        private Timer timer;

        public MockupSensor() : base()
        {
            this.timer = new Timer();
            this.timer.Interval = 1000;
            this.timer.Elapsed += TimerRefresh;
            this.timer.Start();
        }

        private void TimerRefresh(object sender, ElapsedEventArgs e)
        {
            Random r = new Random();
            this.Value = r.Next(0, 100).ToString();
        }
    }
}