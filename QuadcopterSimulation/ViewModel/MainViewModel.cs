using Microsoft.Research.DynamicDataDisplay.DataSources;
using QuadcopterSimulation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace QuadcopterSimulation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ModellingEnvironmentViewModel Environment { get; set; }

        public FrameViewModel Frame {get; set;}

        public RotorGroupViewModel RotorGroup { get; set; }

        public ControllerViewModel Controller { get; set; }

        public MainViewModel()
        {
            Environment = new ModellingEnvironmentViewModel();
            Frame = Environment.Frame;
            RotorGroup = Environment.RotorGroup;
            Controller = Environment.Controller;
        }

        #region Запустить симуляцию

        private DelegateCommand runSimulationCommand;

        public ICommand RunSimulationCommand
        {
            get
            {
                if (runSimulationCommand == null)
                {
                    runSimulationCommand = new DelegateCommand(RunSimulation);
                }
                return runSimulationCommand;
            }
        }

        public void RunSimulation()
        {
            Environment.RunSimulation();
        }

        #endregion
    }
}
