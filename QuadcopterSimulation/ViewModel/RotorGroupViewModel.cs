using QuadcopterSimulation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadcopterSimulation.ViewModel
{
    public class RotorGroupViewModel : ViewModelBase
    {
        public RotorGroup RotorGroup { get; private set; }

        public RotorGroupViewModel(RotorGroup rotorGroup)
        {
            RotorGroup = rotorGroup;
        }

        public double PropDiam
        {
            get
            {
                return RotorGroup.PropDiam;
            }
            set
            {
                RotorGroup.PropDiam = value;
                OnPropertyChanged("PropDiam");
            }
        }

        public double DragCoef
        {
            get
            {
                return RotorGroup.DragCoef;
            }
            set
            {
                RotorGroup.DragCoef = value;
                OnPropertyChanged("DragCoef");
            }
        }

        public double LiftCoef
        {
            get
            {
                return RotorGroup.LiftCoef;
            }
            set
            {
                RotorGroup.LiftCoef = value;
                OnPropertyChanged("LiftCoef");
            }
        }

    }
}
