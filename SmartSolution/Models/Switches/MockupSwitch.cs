namespace WebPortal.Models.Switches
{
    public class MockupSwitch : Switch, ISwitch
    {
        public MockupSwitch() : base()
        {
            this.SwitchType = SwitchType.Mockup;
        }

        public override void TurnOFF()
        {
            this.State = false;
        }

        public override void TurnON()
        {
            this.State = true;
        }
    }
}
