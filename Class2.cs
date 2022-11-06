using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace planetSim
{
    internal class Planet2
    {
        public static readonly double[] bruh = new double[] { 0, 0 };
        public Vector<double> vel;
        public Vector<double> pos;
        public Vector<double> force = Vector<double>.Build.DenseOfArray(bruh);

        public double mass;
        public int? iD;

        public Planet2(double[] inPos, double[] inVel, double mass)
        {
            this.pos = Vector<double>.Build.DenseOfArray(inPos);
            this.vel = Vector<double>.Build.DenseOfArray(inVel);
            this.mass = mass;

        }

        public void calcFtoList(Planet2[] planets)
        {
            foreach (Planet2 pl in planets)
            {
                if (pl.iD != this.iD)
                {
                    Vector<double> rvec = this.pos - pl.pos;
                    double r = rvec.L2Norm();
                }
            }
        }
    }
}
