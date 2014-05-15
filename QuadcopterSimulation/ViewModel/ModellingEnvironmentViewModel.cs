using Microsoft.Research.DynamicDataDisplay.DataSources;
using QuadcopterSimulation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadcopterSimulation.ViewModel
{
    public class ModellingEnvironmentViewModel : ViewModelBase
    {
        private ModellingEnvironment _environment;

        public ControllerViewModel Controller { get; private set; }

        public FrameViewModel Frame { get; private set; }

        public RotorGroupViewModel RotorGroup { get; private set; }

        private CompositeDataSource _firstMotorSpeed;

        public CompositeDataSource FirstMotorSpeed
        {
            get { return _firstMotorSpeed; }
            set
            {
                _firstMotorSpeed = value;
                OnPropertyChanged("FirstMotorSpeed");
            }
        }

        private CompositeDataSource _secondMotorSpeed;

        public CompositeDataSource SecondMotorSpeed
        {
            get { return _secondMotorSpeed; }
            set
            {
                _secondMotorSpeed = value;
                OnPropertyChanged("SecondMotorSpeed");
            }
        }

        private CompositeDataSource _thirdMotorSpeed;

        public CompositeDataSource ThirdMotorSpeed
        {
            get { return _thirdMotorSpeed; }
            set
            {
                _thirdMotorSpeed = value;
                OnPropertyChanged("ThirdMotorSpeed");
            }
        }

        private CompositeDataSource _fourthMotorSpeed;

        public CompositeDataSource FourthMotorSpeed
        {
            get { return _fourthMotorSpeed; }
            set
            {
                _fourthMotorSpeed = value;
                OnPropertyChanged("FourthMotorSpeed");
            }
        }

        private CompositeDataSource _roll;

        public CompositeDataSource Roll
        {
            get { return _roll; }
            set
            {
                _roll = value;
                OnPropertyChanged("Roll");
            }
        }

        private CompositeDataSource _pitch;

        public CompositeDataSource Pitch
        {
            get { return _pitch; }
            set
            {
                _pitch = value;
                OnPropertyChanged("Pitch");
            }
        }

        private CompositeDataSource _yaw;

        public CompositeDataSource Yaw
        {
            get { return _yaw; }
            set
            {
                _yaw = value;
                OnPropertyChanged("Yaw");
            }
        }

        private CompositeDataSource _xCoordinate;

        public CompositeDataSource XCoordinate
        {
            get { return _xCoordinate; }
            set
            {
                _xCoordinate = value;
                OnPropertyChanged("XCoordinate");
            }
        }

        private CompositeDataSource _yCoordinate;

        public CompositeDataSource YCoordinate
        {
            get { return _yCoordinate; }
            set
            {
                _yCoordinate = value;
                OnPropertyChanged("YCoordinate");
            }
        }

        private CompositeDataSource _zCoordinate;

        public CompositeDataSource ZCoordinate
        {
            get { return _zCoordinate; }
            set
            {
                _zCoordinate = value;
                OnPropertyChanged("ZCoordinate");
            }
        }
                

        public double SimulationTime
        {
            get
            {
                return _environment.SimulationTime;
            }
            set
            {
                _environment.SimulationTime = value;
                OnPropertyChanged("SimulationTime");
            }
        }

        public double SamplingPeriod
        {
            get
            {
                return _environment.SamplingPeriod;
            }
            set
            {
                _environment.SamplingPeriod = value;
                OnPropertyChanged("SamplingPeriod");
            }
        }

        public ModellingEnvironmentViewModel()
        {
            _environment = new ModellingEnvironment();
            Controller = new ControllerViewModel(_environment.Controller);
            RotorGroup = new RotorGroupViewModel(_environment.RotorGroup);
            Frame = new FrameViewModel(_environment.Frame);
        }

        public void RunSimulation()
        {
            _environment.RunSimulation();

            int arraySize = _environment.TimeValues.GetLength(0);
            int graphSize = 100;

            double remain = ((double)(arraySize - 1) % (double)(graphSize - 1)) / (double)arraySize;

            while ((remain > 0.1) && (graphSize > 50))
            {
                graphSize--;
                remain = ((double)(arraySize - 1) % (double)(graphSize - 1)) / (double)arraySize;
            }

            int step = (arraySize - 1) / (graphSize - 1);

            double[] timeValues = new double[graphSize];
            double[] w1_Values = new double[graphSize];
            double[] w2_Values = new double[graphSize];
            double[] w3_Values = new double[graphSize];
            double[] w4_Values = new double[graphSize];
            double[] rollValues = new double[graphSize];
            double[] pitchValues = new double[graphSize];
            double[] yawValues = new double[graphSize];
            double[] xValues = new double[graphSize];
            double[] yValues = new double[graphSize];
            double[] zValues = new double[graphSize];

            for (int i = 0, j = 0; i < graphSize; i++, j += step)
            {
                timeValues[i] = _environment.TimeValues[j];
                w1_Values[i] = _environment.RPM1Values[j];
                w2_Values[i] = _environment.RPM2Values[j];
                w3_Values[i] = _environment.RPM3Values[j];
                w4_Values[i] = _environment.RPM4Values[j];
                if (double.IsNaN(_environment.RollValues[j]))
                {
                    rollValues[i] = 0;
                }
                else
                {
                    rollValues[i] = _environment.RollValues[j];
                }

                if (double.IsNaN(_environment.PitchValues[j]))
                {
                    pitchValues[i] = 0;
                }
                else
                {
                    pitchValues[i] = _environment.PitchValues[j];
                }

                if (double.IsNaN(_environment.YawValues[j]))
                {
                    yawValues[i] = 0;
                }
                else
                {
                    yawValues[i] = _environment.YawValues[j];
                }

                if (double.IsNaN(_environment.XValues[j]))
                {
                    xValues[i] = 0;
                }
                else
                {
                    xValues[i] = _environment.XValues[j];
                }

                if (double.IsNaN(_environment.YValues[j]))
                {
                    yValues[i] = 0;
                }
                else
                {
                    yValues[i] = _environment.YValues[j];
                }

                if (double.IsNaN(_environment.ZValues[j]))
                {
                    zValues[i] = 0;
                }
                else
                {
                    zValues[i] = _environment.ZValues[j];
                }
            }
            EnumerableDataSource<double> timeDS = new EnumerableDataSource<double>(timeValues);
            timeDS.SetXMapping(e => e);
            EnumerableDataSource<double> w1_DS = new EnumerableDataSource<double>(w1_Values);
            w1_DS.SetYMapping(e => e);
            EnumerableDataSource<double> w2_DS = new EnumerableDataSource<double>(w2_Values);
            w2_DS.SetYMapping(e => e);
            EnumerableDataSource<double> w3_DS = new EnumerableDataSource<double>(w3_Values);
            w3_DS.SetYMapping(e => e);
            EnumerableDataSource<double> w4_DS = new EnumerableDataSource<double>(w4_Values);
            w4_DS.SetYMapping(e => e);
            EnumerableDataSource<double> rollDS = new EnumerableDataSource<double>(rollValues);
            rollDS.SetYMapping(e => e);
            EnumerableDataSource<double> pitchDS = new EnumerableDataSource<double>(pitchValues);
            pitchDS.SetYMapping(e => e);
            EnumerableDataSource<double> yawDS = new EnumerableDataSource<double>(yawValues);
            yawDS.SetYMapping(e => e);
            EnumerableDataSource<double> xDS = new EnumerableDataSource<double>(xValues);
            xDS.SetYMapping(e => e);
            EnumerableDataSource<double> yDS = new EnumerableDataSource<double>(yValues);
            yDS.SetYMapping(e => e);
            EnumerableDataSource<double> zDS = new EnumerableDataSource<double>(zValues);
            zDS.SetYMapping(e => e);

            _firstMotorSpeed = new CompositeDataSource(timeDS, w1_DS);
            _secondMotorSpeed = new CompositeDataSource(timeDS, w2_DS);
            _thirdMotorSpeed = new CompositeDataSource(timeDS, w3_DS);
            _fourthMotorSpeed = new CompositeDataSource(timeDS, w4_DS);
            _roll = new CompositeDataSource(timeDS, rollDS);
            _pitch = new CompositeDataSource(timeDS, pitchDS);
            _yaw = new CompositeDataSource(timeDS, yawDS);
            _xCoordinate = new CompositeDataSource(timeDS, xDS);
            _yCoordinate = new CompositeDataSource(timeDS, yDS);
            _zCoordinate = new CompositeDataSource(timeDS, zDS);
            OnPropertyChanged("FirstMotorSpeed");
            OnPropertyChanged("SecondMotorSpeed");
            OnPropertyChanged("ThirdMotorSpeed");
            OnPropertyChanged("FourthMotorSpeed");
            OnPropertyChanged("XCoordinate");
            OnPropertyChanged("YCoordinate");
            OnPropertyChanged("ZCoordinate");
            OnPropertyChanged("Roll");
            OnPropertyChanged("Pitch");
            OnPropertyChanged("Yaw");
        }
    }
}
