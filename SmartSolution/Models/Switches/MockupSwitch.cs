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
            this.timer = new Timer
            {
                Interval = 60 * 1000
            };
            this.timer.Elapsed += TimerRefresh;
            this.timer.Start();    
        }

        /// <summary>
        /// Timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerRefresh(object sender, ElapsedEventArgs e)
        {
            this.State = !this.State;
        }
        
        /// <summary>
        /// Turn on switch
        /// </summary>
        public override void TurnON()
        {
            if(this.InverseLogic == false)  this.State = true;
            else this.State = false;
            
        }

        /// <summary>
        /// Turn off switch
        /// </summary>
        public override void TurnOFF()
        {
            if (this.InverseLogic == false) this.State = false;
            else this.State = true;
        }
    }
}
