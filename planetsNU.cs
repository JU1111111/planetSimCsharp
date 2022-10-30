using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumSharp;

namespace planetSim
{
    internal class planetsNU
    {
        public double mass;
        public NumSharp.NDArray pos;
        public NumSharp.NDArray vel;
        public NumSharp.NDArray[]? forcelist;
        public NumSharp.NDArray[]? force;

        public planetsNU(double m, NumSharp.NDArray position, NDArray vel)
        {
            this.mass = m;
            this.pos = position;
            this.vel = vel;

        }
    }
}
