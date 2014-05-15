using QuadcopterSimulation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadcopterSimulation.ViewModel
{
    public class ControllerViewModel : ViewModelBase
    {
        public Controller Controller { get; private set; }

        public ControllerViewModel(Controller controller)
        {
            Controller = controller;
        }

        public int RPM1
        {
            get { return Controller.RPM[0]; }
            set
            {
                Controller.RPM[0] = value;
                OnPropertyChanged("RPM1");
            }
        }

        public int RPM2
        {
            get { return Controller.RPM[1]; }
            set
            {
                Controller.RPM[1] = value;
                OnPropertyChanged("RPM2");
            }
        }

        public int RPM3
        {
            get { return Controller.RPM[2]; }
            set
            {
                Controller.RPM[2] = value;
                OnPropertyChanged("RPM3");
            }
        }

        public int RPM4
        {
            get { return Controller.RPM[3]; }
            set
            {
                Controller.RPM[3] = value;
                OnPropertyChanged("RPM4");
            }
        }

        public bool IsControllerEnabled
        {
            get { return Controller.IsControllerEnabled; }
            set
            {
                Controller.IsControllerEnabled = value;
                OnPropertyChanged("IsControllerEnabled");
            }
        }

        #region PID Coefs

        public double ThrustPropCoef
        {
            get { return Controller.ThrustPropCoef; }
            set
            {
                Controller.ThrustPropCoef = value;
                OnPropertyChanged("ThrustPropCoef");
            }
        }

        public double ThrustDifCoef
        {
            get { return Controller.ThrustDifCoef; }
            set
            {
                Controller.ThrustDifCoef = value;
                OnPropertyChanged("ThrustDifCoef");
            }
        }

        public double ThrustIntCoef
        {
            get { return Controller.ThrustIntCoef; }
            set
            {
                Controller.ThrustIntCoef = value;
                OnPropertyChanged("ThrustIntCoef");
            }
        }

        public double RollPropCoef
        {
            get { return Controller.RollPropCoef; }
            set
            {
                Controller.RollPropCoef = value;
                OnPropertyChanged("RollPropCoef");
            }
        }

        public double RollDifCoef
        {
            get { return Controller.RollDifCoef; }
            set
            {
                Controller.RollDifCoef = value;
                OnPropertyChanged("RollDifCoef");
            }
        }

        public double RollIntCoef
        {
            get { return Controller.RollIntCoef; }
            set
            {
                Controller.RollIntCoef = value;
                OnPropertyChanged("RollIntCoef");
            }
        }

        public double PitchPropCoef
        {
            get { return Controller.PitchPropCoef; }
            set
            {
                Controller.PitchPropCoef = value;
                OnPropertyChanged("PitchPropCoef");
            }
        }

        public double PitchDifCoef
        {
            get { return Controller.PitchDifCoef; }
            set
            {
                Controller.PitchDifCoef = value;
                OnPropertyChanged("PitchDifCoef");
            }
        }

        public double PitchIntCoef
        {
            get { return Controller.PitchIntCoef; }
            set
            {
                Controller.PitchIntCoef = value;
                OnPropertyChanged("PitchIntCoef");
            }
        }

        public double YawPropCoef
        {
            get { return Controller.YawPropCoef; }
            set
            {
                Controller.YawPropCoef = value;
                OnPropertyChanged("YawPropCoef");
            }
        }

        public double YawDifCoef
        {
            get { return Controller.YawDifCoef; }
            set
            {
                Controller.YawDifCoef = value;
                OnPropertyChanged("YawDifCoef");
            }
        }

        public double YawIntCoef
        {
            get { return Controller.YawIntCoef; }
            set
            {
                Controller.YawIntCoef = value;
                OnPropertyChanged("YawIntCoef");
            }
        }

        #endregion
    }
}
