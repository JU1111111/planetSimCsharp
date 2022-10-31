using NumSharp;

//yeeeeeeeeeeeee
namespace planetSim
{
    internal class Planet
    {
        public double mass;
        public NumSharp.NDArray pos;
        public NumSharp.NDArray vel;
        public NumSharp.NDArray force = 0;
        public List<NumSharp.NDArray> forcelist = new();
        public int? iD;
        public List<Double> xPoslist = new();
        public List<Double> yPoslist = new();

        public Planet(double m, NumSharp.NDArray position, NDArray vel)
        {
            this.mass = m;
            this.pos = position;
            this.vel = vel;
        }

        public void CalcFTo(Planet pl)
        {
            double gamma = 6.673 * Math.Pow(10, -11);
            NumSharp.NDArray rvec = this.pos - pl.pos;
            NumSharp.NDArray rsqt = NumSharp.np.power(rvec, 2);
            NumSharp.NDArray rVal = rsqt[0] + rsqt[1];
            //double rVal = np.linalg.norm(rvec);
            NDArray Fg = gamma * (pl.mass * this.mass / Math.Pow(rVal, 3)) * rvec;
            this.forcelist.Add(Fg);
            pl.forcelist.Add(-1 * Fg);
        }

        public void CalFToListAndAdd(Planet[] plList)
        {
            for (int i = 0; i < plList.Length; i++)
            {
                Planet pl = plList[i];
                if (pl.iD != this.iD)
                {
                    CalcFTo(pl);
                }
            }
            foreach (NumSharp.NDArray f in this.forcelist)
            {
                force += f;
            }
        }

        public void CalcPosAndVel(double dT)
        {
            NumSharp.NDArray a = this.force / this.mass;
            NumSharp.NDArray newPos = 1 / 2 * a * Math.Pow(dT, 2) + this.vel * dT + this.pos;
            NumSharp.NDArray newVel = a * dT + this.vel;

            this.pos = newPos;
            this.vel = newVel;
            this.xPoslist.Add(newPos[0]);
            this.yPoslist.Add(newPos[1]);
        }
    }
    internal class Universe
    {
        public Planet[] planets;

        public Universe(Planet[] planets)
        {
            this.planets = planets;
        }

        public bool CalcFAndSum()
        {
            for (int i = 0; i < this.planets.Length / 2; i++)
            {
                Planet pl = this.planets[i];
                pl.CalFToListAndAdd(this.planets);
            }
            return true;
        }

        public void AddPlanetIds()
        {
            for (int i = 0; i < this.planets.Length; i++)
            {
                Planet pl = planets[i];
                pl.iD = i;
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
