using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadcopterSimulation.Model
{
    public class Controller
    {
        //constants
        private static double g = 9.8;

        //Linked objects
        private Frame _frame;
        private RotorGroup _rotorGroup;

        //Params
        public double ThrustPropCoef, ThrustDifCoef, ThrustIntCoef;
        public double RollPropCoef, RollDifCoef, RollIntCoef;
        public double PitchPropCoef, PitchDifCoef, PitchIntCoef;
        public double YawPropCoef, YawDifCoef, YawIntCoef;

        public bool IsControllerEnabled;

        //Speeds when controller disabled
        public int[] RPM;

        //Desired state to acheive
        public double RollDesired, PitchDesired, YawDesired;
        public double RollSpeedDesired, PitchSpeedDesired, YawSpeedDesired;
        public double YDesired, YSpeedDesired;

        public Controller(Frame frame, RotorGroup rotorGroup)
        {
            _frame = frame;
            _rotorGroup = rotorGroup;

            IsControllerEnabled = false;

            RPM = new int[4];

            RollDesired = 0; PitchDesired = 0; YawDesired = 0;
            RollSpeedDesired = 0; PitchSpeedDesired = 0; YawSpeedDesired = 0;
            YDesired = 10; YSpeedDesired = 0;

            ThrustPropCoef = 1; ThrustDifCoef = 1; ThrustIntCoef = 0;
            RollPropCoef = 1; RollDifCoef = 1; RollIntCoef = 0;
            PitchPropCoef = 1; PitchDifCoef = 1; PitchIntCoef = 0;
            YawPropCoef = 1; YawDifCoef = 1; YawIntCoef = 0;
        }

        public void CalculateControl()
        {
            if (IsControllerEnabled)
            {
                double thrust = (g + ThrustDifCoef * (YSpeedDesired - _frame.YSpeed) + ThrustPropCoef * (YDesired - _frame.Y)) * _frame.Mass / Math.Cos(_frame.Roll) / Math.Cos(_frame.Pitch);
                double torqueX = (RollDifCoef * (RollSpeedDesired - _frame.RollSpeed) + RollPropCoef * (RollDesired - _frame.Roll)) * _frame.Ixx;
                double torqueZ = (PitchDifCoef * (PitchSpeedDesired - _frame.PitchSpeed) + PitchPropCoef * (PitchDesired - _frame.Pitch)) * _frame.Izz;
                double torqueY = (YawDifCoef * (YawSpeedDesired - _frame.YawSpeed) + YawPropCoef * (YawDesired - _frame.Yaw)) * _frame.Iyy;

                _rotorGroup.Thrust = thrust;
                _rotorGroup.TorqueX = torqueX;
                _rotorGroup.TorqueY = torqueY;
                _rotorGroup.TorqueZ = torqueZ;
            }
            else
            {
                _rotorGroup.CurrentRPM = RPM;
            }
        }
    }
}
