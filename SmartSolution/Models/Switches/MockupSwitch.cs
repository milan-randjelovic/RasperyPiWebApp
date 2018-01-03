using System.Timers;

namespace WebPortal.Models.Switches
{
    public class MockupSwitch : Switch, ISwitch
    {
        private Timer timer;

        public MockupSwitch() : base()
        {
            this.SwitchType = SwitchType.Mockup;
            this.State = false;
            this.timer = new Timer();
            this.timer.Interval = 60 * 1000;
            this.timer.Elapsed += TimerRefresh;
            this.timer.Start();
        }

        private void TimerRefresh(object sender, ElapsedEventArgs e)
        {
            this.State = !this.State;
        }

        public override void TurnON()
        {
            this.State = true;
        }

        public override void TurnOFF()
        {
            this.State = false;
        }
    }
}
