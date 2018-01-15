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
            this.InverseLogic = false;
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
            if(this.InverseLogic == false)  this.State = true;
            else this.State = false;
            
        }

        public override void TurnOFF()
        {
            if (this.InverseLogic == false) this.State = false;
            else this.State = true;
        }
    }
}
