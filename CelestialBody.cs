using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planetSim
{
    public class CelestialBody
    {
        public Vector<double> Force = Vector<double>.Build.Dense(new double[2]);
        public Vector<double> Acc = Vector<double>.Build.Dense(new double[2]);
        public Vector<double> Vel= Vector<double>.Build.Dense(new double[2]);
        public Vector<double> Pos = Vector<double>.Build.Dense(new double[2]);
        public double Mass;
        public int? iD;
        public CelestialBody(double[] vel, double[] pos, double mass)
        {
            Vel = Vector<double>.Build.Dense(vel);
            Pos = Vector<double>.Build.Dense(pos);
            Mass = mass;
        }
    }
}
