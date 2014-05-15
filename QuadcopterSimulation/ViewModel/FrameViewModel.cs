using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuadcopterSimulation.Model;

namespace QuadcopterSimulation.ViewModel
{
    public class FrameViewModel : ViewModelBase
    {
        public Frame Frame { get; private set; }

        public FrameViewModel(Frame frame)
        {
            Frame = frame;
        }

        public double Ixx
        {
            get
            {
                return Frame.Ixx; 
            }
            set
            {
                Frame.Ixx = value;
                OnPropertyChanged("Ixx");
            }
        }

        public double Iyy
        {
            get
            {
                return Frame.Iyy;
            }
            set
            {
                Frame.Iyy = value;
                OnPropertyChanged("Iyy");
            }
        }

        public double Izz
        {
            get { return Frame.Izz; }
            set
            {
                Frame.Izz = value;
                OnPropertyChanged("Izz");
            }
        }

        public double Mass
        {
            get { return Frame.Mass; }
            set
            {
                Frame.Mass = value;
                OnPropertyChanged("Mass");
            }
        }

        public double ArmLength
        {
            get { return Frame.ArmLength; }
            set
            {
                Frame.ArmLength = value;
                OnPropertyChanged("ArmLength");
            }
        }

        #region InitialPosition

        public double RollInitial
        {
            get
            {
                return Frame.RollInitial / Math.PI * 180;
            }
            set
            {
                Frame.RollInitial = ((value % 180) / 180) * Math.PI;
                OnPropertyChanged("RollInitial");
            }
        }

        public double PitchInitial
        {
            get
            {
                return Frame.PitchInitial / Math.PI * 180;
            }
            set
            {
                Frame.PitchInitial = ((value % 180) / 180) * Math.PI;
                OnPropertyChanged("PitchInitial");
            }
        }

        public double YawInitial
        {
            get
            {
                return Frame.YawInitial / Math.PI * 180;
            }
            set
            {
                Frame.YawInitial = ((value % 180) / 180) * Math.PI;
                OnPropertyChanged("YawInitial");
            }
        }

        public double XInitial
        {
            get
            {
                return Frame.XInitial;
            }
            set
            {
                Frame.XInitial = value;
                OnPropertyChanged("XInitial");
            }
        }

        public double YInitial
        {
            get
            {
                return Frame.YInitial;
            }
            set
            {
                Frame.YInitial = value;
                OnPropertyChanged("YInitial");
            }
        }

        public double ZInitial
        {
            get
            {
                return Frame.ZInitial;
            }
            set
            {
                Frame.ZInitial = value;
                OnPropertyChanged("ZInitial");
            }
        }


        #endregion
    }
}
