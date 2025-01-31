using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static TrafficLightProject.ctrlTrafficLight;


namespace TrafficLightProject
{
    public partial class ctrlTrafficLight : UserControl
    {
        public ctrlTrafficLight()
        {
            InitializeComponent();

            GreenTime = 5;
            RedTime = 10;
            OrangeTime = 3;
            CurrentLight = enCurrentLight.Red;

            if (CurrentLight == enCurrentLight.Red)
            {
                CountTimer = RedTime;
            }
            else if (CurrentLight == enCurrentLight.Green)
            {
                CountTimer = GreenTime;
            }
            else
            {
                CountTimer = OrangeTime;
            }
        }

        private int CountTimer = 0;

        public int GreenTime { get; set; }
        public int OrangeTime { get; set; }
        public int RedTime { get; set; }

        public enum enCurrentLight {Red = 1, Green = 2, Orange = 3};
        public enCurrentLight CurrentLight {get; set; }

        public class clsTrafficLights : EventArgs
        {
            public string ColorCurrentLight { get; set; }

            public clsTrafficLights(string currentlight)
            {
                this.ColorCurrentLight = currentlight;
            }
        }

        public event EventHandler<clsTrafficLights> GreenLightOn;
        public event EventHandler<clsTrafficLights> OrangeLightOn;
        public event EventHandler<clsTrafficLights> RedLightOn;
 
        private void RaiseEvents(enCurrentLight currentLight)
        {
            string colorcurrentlight = CurrentLight.ToString();
            clsTrafficLights e = new clsTrafficLights(colorcurrentlight);

            switch (currentLight)
            {
                case enCurrentLight.Green:
                    GreenLightOn?.Invoke(this, e);
                    break;

                case enCurrentLight.Red:
                    RedLightOn?.Invoke(this, e);
                    break;

                case enCurrentLight.Orange:
                    OrangeLightOn?.Invoke(this, e);
                    break;
            }

        }

        public void Start()
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CountTimer == 0 && CurrentLight == enCurrentLight.Red)
            {
                CountTimer = OrangeTime;
                CurrentLight = enCurrentLight.Orange;
                pictureBox1.Image = Properties.Resources.Orange;
                RaiseEvents(CurrentLight);
            }
            
            if (CountTimer == 0 && CurrentLight == enCurrentLight.Orange)
            {
                CountTimer = GreenTime;
                CurrentLight = enCurrentLight.Green;
                pictureBox1.Image = Properties.Resources.Green;
                RaiseEvents(CurrentLight);
            }

            if (CountTimer == 0 && CurrentLight == enCurrentLight.Green)
            {
                CountTimer = RedTime;
                CurrentLight = enCurrentLight.Red;
                pictureBox1.Image = Properties.Resources.Red;
                RaiseEvents(CurrentLight);
            }

            lbTimer.Text = CountTimer.ToString();
            CountTimer--;
        }
    }
}
