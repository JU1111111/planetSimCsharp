using NumSharp;
using MathNet.Numerics.LinearAlgebra;


//yeeeeeeeeeeeee
namespace planetSim
{
    internal class Planet
    {
        static readonly double[] f = new double[] { 0, 0 };
        public double mass;
        public Vector<double> pos;
        public Vector<double> vel;
        public Vector<double> force = Vector<double>.Build.DenseOfArray(f);
        public List<Vector<double>> forcelist = new();
        public int? iD;
        public List<Double> xPoslist = new();
        public List<Double> yPoslist = new();

        public Planet(double m, double[] position, double[] vel)
        {
            this.mass = m;
            this.pos = Vector<double>.Build.DenseOfArray(position);
            this.vel = Vector<double>.Build.DenseOfArray(vel);
        }

        public void CalcFTo(Planet pl)
        {
            double gamma = 6.673 * Math.Pow(10, -11);
            Vector<double> rvec = this.pos - pl.pos;
            double rVal = rvec.L2Norm();
            //double rVal = np.linalg.norm(rvec);
            //Vector<double> Fg = (-1) * gamma * (pl.mass * this.mass / Math.Pow(rVal, 3)) * rvec;
            Vector<double> Fg = ((-gamma) * this.mass * pl.mass / Math.Pow(rVal, 2)) * 1 / rVal * rvec;
            this.forcelist.Add(Fg);
            //pl.forcelist.Add(-1 * Fg);
        }

        public void CalFToList(Planet[] plList)
        {
            foreach (Planet pl in plList)
            {
                if (pl.iD != this.iD)
                {
                    CalcFTo(pl);
                }
            }

        }
        public void sumOfF()
        {
            foreach (Vector<double> f in this.forcelist)
            {
                force += f;
            }

        }

        public void CalcPosAndVel(double dT)
        {
            Vector<double> a = this.force / this.mass;
            Vector<double> newPos = 1 / 2 * a * Math.Pow(dT, 2) + this.vel * dT + this.pos;
            Vector<double> newVel = a * dT + this.vel;

            this.pos = newPos;
            this.vel = newVel;
            this.xPoslist.Add(newPos[0]);
            this.yPoslist.Add(newPos[1]);
        }
    }
    internal class Universe
    {
        public Planet[] planets;
        public int iD = 0;
        public Universe(Planet[] planets)
        {
            this.planets = planets;
        }

        public bool CalcFAndSum()
        {
            /*
            for (int i = 0; i < this.planets.Length / 2; i++)
            {
                Planet pl = this.planets[i];
            }*/
            foreach (Planet pl in this.planets)
            {
                pl.CalFToList(this.planets);

            }
            foreach(Planet pl in this.planets)
            {
                pl.sumOfF();
                //Console.WriteLine(pl.iD);
            }
            return true;
        }

        public void AddPlanetIds()
        {
            foreach (Planet pl in this.planets)
            {
                pl.iD = this.iD;
                this.iD++;
            }
        }

        public void SimThisshit(double minT, double maxT, double dT)
        {
            DateTime start = DateTime.UtcNow;
            int steps = Convert.ToInt32(maxT / dT);
            for (int i = 0; i < steps; i++)
            {
                bool fs = this.CalcFAndSum();

                if (fs)
                {
                    foreach (var pl in this.planets)
                    {
                        pl.CalcPosAndVel(dT);
                        pl.forcelist.Clear();
                    }
                }
                /*if(i%1000 == 0)
                {
                    Console.WriteLine(i);
                }*/
            }
            DateTime end = DateTime.UtcNow;
            TimeSpan timeDiff = end - start;
            Console.WriteLine(Convert.ToInt32(timeDiff.TotalSeconds));
        }

    }
}
