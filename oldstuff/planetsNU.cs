using NumSharp;
using MathNet.Numerics.LinearAlgebra;


//yeeeeeeeeeeeee
namespace planetSim.oldstuff
{
    internal class Planet
    {
        public double mass;
        public Vector<double> pos;
        public Vector<double> vel;
        public Vector<double> force = Vector<double>.Build.DenseOfArray(new double[] { 0, 0 });
        public List<Vector<double>> forcelist = new();
        public int? iD;
        public List<double> xPoslist = new();
        public List<double> yPoslist = new();
        public List<Vector<double>> vlist = new();

        public Planet(double m, double[] position, double[] vel)
        {
            mass = m;
            pos = Vector<double>.Build.DenseOfArray(position);
            this.vel = Vector<double>.Build.DenseOfArray(vel);
            Console.WriteLine($"Created Planet with mass of {mass} at position {pos[0]} {pos[1]} with veolocity of {this.vel[0]} {this.vel[1]}");
        }

        public void CalcFTo(Planet pl)
        {
            double gamma = 6.6743 * 1e-11;
            Vector<double> rvec = pos - pl.pos;
            double rVal = rvec.L2Norm();
            //Console.WriteLine(rVal);
            //double rVal = np.linalg.norm(rvec);
            Vector<double> Fg = -1 * gamma * (pl.mass * mass / Math.Pow(rVal, 3)) * rvec;
            //Vector<double> Fg = ((-gamma) * this.mass * pl.mass / Math.Pow(rVal, 2)) * 1 / rVal * rvec;
            forcelist.Add(Fg);
            //pl.forcelist.Add(-1 * Fg);

        }

        public void CalFToList(Planet[] plList)
        {
            foreach (Planet pl in plList)
            {
                if (pl.iD != iD)
                {
                    CalcFTo(pl);
                }
            }

        }
        public void sumOfF()
        {
            foreach (Vector<double> f in forcelist)
            {
                force += f;
            }

        }

        public void CalcPosAndVel(double dT)
        {
            //this.vlist.Add(vel);
            Vector<double> a = force / mass;
            Vector<double> newVel = a * dT + vel;
            Vector<double> newPos = 1 / 2 * a * Math.Pow(dT, 2) + vel * dT + pos;


            pos = newPos;
            vel = newVel;
        }
    }
    internal class UniverseOld
    {
        public Planet[] planets;
        public int iD = 0;
        public UniverseOld(Planet[] planets)
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
            foreach (Planet pl in planets)
            {
                pl.forcelist.Clear();
                pl.CalFToList(planets);
            }
            foreach (Planet pl in planets)
            {
                pl.sumOfF();
                //Console.WriteLine(pl.iD);
            }
            return true;
        }

        public void AddPlanetIds()
        {
            foreach (Planet pl in planets)
            {
                pl.iD = iD;
                iD++;
            }
        }

        public void SimThisshit(double minT, double maxT, double dT)
        {
            DateTime start = DateTime.UtcNow;
            long steps = Convert.ToInt64(maxT / dT);
            Console.WriteLine(steps);

            for (int i = 0; i < steps; i++)
            {
                bool fs = CalcFAndSum();

                foreach (var pl in planets)
                {
                    pl.CalcPosAndVel(dT);

                    if (i % 100 == 0)
                    {
                        pl.xPoslist.Add(pl.pos[0]);
                        pl.yPoslist.Add(pl.pos[1]);
                        if (i % 100000 == 0)
                        {
                            Console.WriteLine($"currently at step {i}/{steps} ({i / (float)steps * 100.0}%)");
                        }

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
