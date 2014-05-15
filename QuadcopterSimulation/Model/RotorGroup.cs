using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadcopterSimulation.Model
{
    public class RotorGroup
    {
        //constants
        public const int MaxRPM = 7000;
        public const int MaxRPS = 117;

        public const double AirDensity = 1.225; // kg/m^3

        //disposition param
        private double _armLength; //covered by property

        //propeller params
        private double _dragCoef; // covered by property
        private double _liftCoef;// covered by property
        private double _diam; //meters, covered by property
        private double _diamDeg4; //changes when Diam changes
        private double _diamDeg5;

        //derived propeller params
        private double __k; //changes when _lifCoef or Diam changes 
        private double _kMult4; //changes when _k changes
        private double _momentsDenom; //changes when _k or _armLength changes

        private double __b; //changes when _dragCoef or Diam changes
        private double _bMult4; //changes when _b changes

        //independent vars
        private bool _torqueXChanged; //set vars but not ones which are really created
        private double _torqueX;

        private bool _torqueYChanged;
        private double _torqueY;

        private bool _torqueZChanged;
        private double _torqueZ;

        private bool _thrustChanged;
        private double _thrust;

        //dependent var
        public int[] _currentRPM; //covered by property
        private int[] _currentRPS; //covered by property

        public double RealTorqueX { get; private set; }
        public double RealTorqueY { get; private set; }
        public double RealTorqueZ { get; private set; }
        public double RealThrust { get; private set; }

        public RotorGroup(double dragCoef, double liftCoef, double propDiam)
        {
            DragCoef = dragCoef;
            LiftCoef = liftCoef;
            PropDiam = propDiam;

            _currentRPM = new int[4];
            _currentRPS = new int[4];

            _torqueXChanged = false;
            _torqueYChanged = false;
            _torqueZChanged = false;
            _thrustChanged = false;
        }

        private void _recalculateSpeeds()
        {
            double[] rpsSq = new double[4];
            rpsSq[0] = _thrust / _kMult4 + (_torqueZ - _torqueX) / _momentsDenom + _torqueY / _bMult4;
            rpsSq[1] = _thrust / _kMult4 + (_torqueZ + _torqueX) / _momentsDenom - _torqueY / _bMult4;
            rpsSq[2] = _thrust / _kMult4 + (_torqueX - _torqueZ) / _momentsDenom + _torqueY / _bMult4;
            rpsSq[3] = _thrust / _kMult4 - (_torqueX + _torqueZ) / _momentsDenom - _torqueY / _bMult4;

            int[] rpsSqRound = new int[4];
            rpsSqRound[0] = (int)Math.Sqrt(rpsSq[0]);
            rpsSqRound[1] = (int)Math.Sqrt(rpsSq[1]);
            rpsSqRound[2] = (int)Math.Sqrt(rpsSq[2]);
            rpsSqRound[3] = (int)Math.Sqrt(rpsSq[3]);

            CurrentRPS = rpsSqRound;
        }
        private void _recalculateForces()
        {
            double[] currentRPSSq = new double[4];
            currentRPSSq[0] = (double)(_currentRPS[0] * _currentRPS[0]);
            currentRPSSq[1] = (double)(_currentRPS[1] * _currentRPS[1]);
            currentRPSSq[2] = (double)(_currentRPS[2] * _currentRPS[2]);
            currentRPSSq[3] = (double)(_currentRPS[3] * _currentRPS[3]);

            RealTorqueX = _momentsDenom / 4 * (-currentRPSSq[0] + currentRPSSq[1] + currentRPSSq[2] - currentRPSSq[3]);
            RealTorqueZ = _momentsDenom / 4 * (currentRPSSq[0] + currentRPSSq[1] - currentRPSSq[2] - currentRPSSq[3]);
            RealTorqueY = _b * (currentRPSSq[0] - currentRPSSq[1] + currentRPSSq[2] - currentRPSSq[3]);
            RealThrust = _k * (currentRPSSq[0] + currentRPSSq[1] + currentRPSSq[2] + currentRPSSq[3]);
        }

        #region Properties
        public double ArmLength
        {
            get
            {
                return _armLength;
            }
            set
            {
                _armLength = value;
                _momentsDenom = _kMult4 * _armLength * Math.Cos(Math.PI / 4);
            }
        }

        public double PropDiam
        {
            get
            {
                return _diam;
            }
            set
            {
                _diam = value;
                _diamDeg4 = Math.Pow(_diam, 4);
                _diamDeg5 = Math.Pow(_diam, 5);

                _k = _liftCoef * AirDensity * _diamDeg4;
                _b = _dragCoef * AirDensity * _diamDeg5 / 2 / Math.PI;
            }
        }

        public double LiftCoef
        {
            get
            {
                return _liftCoef;
            }
            set
            {
                _liftCoef = value;
                _k = _liftCoef * AirDensity * _diamDeg4;
            }
        }

        public double DragCoef
        {
            get
            {
                return _dragCoef;
            }
            set
            {
                _dragCoef = value;
                _b = _dragCoef * AirDensity * _diamDeg5 / 2 / Math.PI;
            }
        }

        private double _k
        {
            get
            {
                return __k;
            }
            set
            {
                __k = value;
                _kMult4 = 4 * __k;
                _momentsDenom = _kMult4 * ArmLength * Math.Cos(Math.PI / 4);
            }
        }

        private double _b
        {
            get
            {
                return __b;
            }
            set
            {
                __b = value;
                _bMult4 = 4 * _b;
            }
        }

        public double TorqueX
        {
            get
            {
                return _torqueX;
            }
            set
            {
                _torqueX = value;
                _torqueXChanged = true;
                if (_torqueXChanged & _torqueYChanged & _torqueZChanged & _thrustChanged)
                {
                    _torqueXChanged = false;
                    _torqueYChanged = false;
                    _torqueZChanged = false;
                    _thrustChanged = false;

                    _recalculateSpeeds();
                }
            }
        }

        public double TorqueY
        {
            get
            {
                return _torqueY;
            }
            set
            {
                _torqueY = value;
                _torqueYChanged = true;
                if (_torqueXChanged & _torqueYChanged & _torqueZChanged & _thrustChanged)
                {
                    _torqueXChanged = false;
                    _torqueYChanged = false;
                    _torqueZChanged = false;
                    _thrustChanged = false;

                    _recalculateSpeeds();
                }
            }
        }

        public double TorqueZ
        {
            get
            {
                return _torqueZ;
            }
            set
            {
                _torqueZ = value;
                _torqueZChanged = true;
                if (_torqueXChanged & _torqueYChanged & _torqueZChanged & _thrustChanged)
                {
                    _torqueXChanged = false;
                    _torqueYChanged = false;
                    _torqueZChanged = false;
                    _thrustChanged = false;

                    _recalculateSpeeds();
                }
            }
        }

        public double Thrust
        {
            get
            {
                return _thrust;
            }
            set
            {
                _thrust = value;
                _thrustChanged = true;
                if (_torqueXChanged & _torqueYChanged & _torqueZChanged & _thrustChanged)
                {
                    _torqueXChanged = false;
                    _torqueYChanged = false;
                    _torqueZChanged = false;
                    _thrustChanged = false;

                    _recalculateSpeeds();
                }
            }
        }

        public int[] CurrentRPM
        {
            get { return _currentRPM; }
            set
            {
                _currentRPM = value;
                for (int i = 0; i < 4; i++)
                {
                    if (_currentRPM[i] > MaxRPM)
                    {
                        _currentRPM[i] = MaxRPM;
                    }
                    else if (_currentRPM[i] < 0)
                    {
                        _currentRPM[i] = 0;
                    }

                    if (_currentRPS != null)
                    {
                        _currentRPS[i] = _currentRPM[i] / 60;
                    }

                }
                _recalculateForces();
            }
        }

        private int[] CurrentRPS
        {
            get { return _currentRPS; }
            set
            {
                _currentRPS = value;
                for (int i = 0; i < 4; i++)
                {
                    if (_currentRPS[i] > MaxRPS)
                    {
                        _currentRPS[i] = MaxRPS;
                    }
                    else if (_currentRPS[i] < 0)
                    {
                        _currentRPS[i] = 0;
                    }

                    if (_currentRPM != null)
                    {
                        _currentRPM[i] = _currentRPS[i] * 60;
                    }
                }
                _recalculateForces();
            }
        }
        #endregion

    }
}
