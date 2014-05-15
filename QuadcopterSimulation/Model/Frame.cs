using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadcopterSimulation.Model
{
    public class Frame
    {
        //constants
        private static double g = 9.8;

        //Linked objects
        private RotorGroup _rotorGroup;

        //Params
        public double Ixx;
        public double Iyy;
        public double Izz;
        public double Mass;
        public double _armLength; //covered by property

        //State variables
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public double XSpeed { get; private set; }
        public double YSpeed { get; private set; }
        public double ZSpeed { get; private set; }

        public Angle Roll { get; private set; }
        public Angle Pitch { get; private set; }
        public Angle Yaw { get; private set; }

        public double RollSpeed { get; private set; }
        public double PitchSpeed { get; private set; }
        public double YawSpeed { get; private set; }

        public double XAngSpeed { get; private set; }
        public double YAngSpeed { get; private set; }
        public double ZAngSpeed { get; private set; }

        //Initial position
        public double XInitial, YInitial, ZInitial;
        public Angle RollInitial, PitchInitial, YawInitial;

        public Frame(RotorGroup rotorGroup)
        {
            _rotorGroup = rotorGroup;

            Ixx = 0.02;
            Iyy = 0.04;
            Izz = 0.02;
            Mass = 1.5;
            ArmLength = 0.25;

            XInitial = 0;
            YInitial = 0;
            ZInitial = 0;
            RollInitial = new Angle(0);
            PitchInitial = new Angle(0);
            YawInitial = new Angle(0);

            X = 0;
            Y = 0;
            Z = 0;

            XSpeed = 0;
            YSpeed = 0;
            ZSpeed = 0;

            Roll = new Angle(0);
            Pitch = new Angle(0);
            Yaw = new Angle(0);

            RollSpeed = 0;
            PitchSpeed = 0;
            YawSpeed = 0;

            XAngSpeed = 0;
            YAngSpeed = 0;
            ZAngSpeed = 0;
        }

        public void Reset()
        {
            X = XInitial;
            Y = YInitial;
            Z = ZInitial;

            XSpeed = 0;
            YSpeed = 0;
            ZSpeed = 0;

            Roll = RollInitial;
            Pitch = PitchInitial;
            Yaw = YawInitial;

            RollSpeed = 0;
            PitchSpeed = 0;
            YawSpeed = 0;

            XAngSpeed = 0;
            YAngSpeed = 0;
            ZAngSpeed = 0;
        }

        public void CalculateNewState(double period)
        {
            double XAcc = _rotorGroup.RealThrust / Mass *  (Math.Sin(Yaw) * Math.Sin(Roll) - Math.Cos(Yaw) * Math.Sin(Roll) * Math.Cos(Roll));
            double YAcc = -g + _rotorGroup.RealThrust / Mass * Math.Cos(Roll) * Math.Cos(Pitch);
            double ZAcc = _rotorGroup.RealThrust / Mass * (Math.Cos(Yaw) * Math.Sin(Roll) + Math.Sin(Yaw) * Math.Sin(Pitch) * Math.Cos(Roll));

            RollSpeed = XAngSpeed - Math.Sin(Pitch) / Math.Cos(Pitch) * Math.Cos(Roll) * YAngSpeed - Math.Sin(Pitch) / Math.Cos(Pitch) * Math.Sin(Roll) * ZAngSpeed;
            PitchSpeed = Math.Sin(Roll) * YAngSpeed + Math.Cos(Roll) * ZAngSpeed;
            YawSpeed = Math.Cos(Roll) / Math.Cos(Pitch) * YAngSpeed - Math.Sin(Roll) / Math.Cos(Pitch) * ZAngSpeed;

            double xAngAcc = _rotorGroup.RealTorqueX / Ixx + (Iyy - Izz) / Ixx * YAngSpeed * ZAngSpeed;
            double yAngAcc = _rotorGroup.RealTorqueY / Iyy + (Izz - Ixx) / Iyy * XAngSpeed * ZAngSpeed;
            double zAngAcc = _rotorGroup.RealTorqueZ / Izz + (Ixx - Iyy) / Izz * XAngSpeed * YAngSpeed;

            XSpeed += XAcc * period;
            YSpeed += YAcc * period;
            ZSpeed += ZAcc * period;

            X += XSpeed * period;
            Y += YSpeed * period;
            Z += ZSpeed * period;

            Roll += RollSpeed * period;
            Pitch += PitchSpeed * period;
            Yaw += YawSpeed * period;

            XAngSpeed += xAngAcc * period;
            YAngSpeed += yAngAcc * period;
            ZAngSpeed += zAngAcc * period;
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
                _rotorGroup.ArmLength = value;
            }
        }

        #endregion
    }
}
