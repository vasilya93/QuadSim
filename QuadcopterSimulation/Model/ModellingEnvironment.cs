using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadcopterSimulation.Model
{
    public class ModellingEnvironment
    {
        //Linked objects
        public RotorGroup RotorGroup { get; private set; }
        public Frame Frame { get; private set; }
        public Controller Controller { get; private set; }

        //Modelling params
        public double SimulationTime;
        public double SamplingPeriod;

        //Variables in time
        public double[] TimeValues { get; private set; }

        public double[] RPM1Values { get; private set; }
        public double[] RPM2Values { get; private set; }
        public double[] RPM3Values { get; private set; }
        public double[] RPM4Values { get; private set; }

        public double[] RollValues { get; private set; }
        public double[] PitchValues { get; private set; }
        public double[] YawValues { get; private set; }

        public double[] XValues { get; private set; }
        public double[] YValues { get; private set; }
        public double[] ZValues { get; private set; }


        public ModellingEnvironment()
        {
            RotorGroup = new RotorGroup(0.04, 0.12, 0.254);
            Frame = new Frame(RotorGroup);
            Controller = new Controller(Frame, RotorGroup);

            SimulationTime = 10;
            SamplingPeriod = 0.01;
        }

        public void RunSimulation()
        {
            Frame.Reset();
            double currentTime = 0;
            int arraySize = (int)(SimulationTime / SamplingPeriod) + 1;

            TimeValues = new double[arraySize];

            RPM1Values = new double[arraySize];
            RPM2Values = new double[arraySize];
            RPM3Values = new double[arraySize];
            RPM4Values = new double[arraySize];

            RollValues = new double[arraySize];
            PitchValues = new double[arraySize];
            YawValues = new double[arraySize];

            XValues = new double[arraySize];
            YValues = new double[arraySize];
            ZValues = new double[arraySize];

            for (int i = 0; currentTime <= SimulationTime; currentTime += SamplingPeriod, i++)
            {
                TimeValues[i] = currentTime;

                Controller.CalculateControl();
                Frame.CalculateNewState(SamplingPeriod);

                RPM1Values[i] = RotorGroup.CurrentRPM[0];
                RPM2Values[i] = RotorGroup.CurrentRPM[1];
                RPM3Values[i] = RotorGroup.CurrentRPM[2];
                RPM4Values[i] = RotorGroup.CurrentRPM[3];

                RollValues[i] = Frame.Roll;
                PitchValues[i] = Frame.Pitch;
                YawValues[i] = Frame.Yaw;

                XValues[i] = Frame.X;
                YValues[i] = Frame.Y;
                ZValues[i] = Frame.Z;
            }
        }
    }
}
