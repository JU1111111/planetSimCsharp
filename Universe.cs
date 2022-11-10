
using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

using System.Text;
using System.Threading.Tasks;

namespace planetSim
{
    public class Universe
    {
        public List<CelestialBody> CelestialBodies;
        public bool error = false;
        public Universe()
        {
            CelestialBodies = new List<CelestialBody>();
        }

        public void AddBody(CelestialBody c)
        {
            CelestialBodies.Add(c);
        }

        private Vector<double> CalcGravForce(CelestialBody c1, CelestialBody c2)
        {
            if (c1.iD != c2.iD)
            {


                var r = (c1.Pos - c2.Pos);
                double rMag = r.L2Norm();
                if (Math.Abs(rMag) < 0.1)
                {
                    error = true;
                    return Vector<double>.Build.Dense(new double[2] { 0, 0 });
                }
                r /= rMag;
                return ((-Constants.GravConst) * c1.Mass * c2.Mass) / (rMag * rMag) * r;
            }
            return Vector<double>.Build.Dense(new double[2] { 0, 0 }); ;
        }

        private void makeIDs()
        {
            int id = 0;
            foreach (CelestialBody CB in CelestialBodies)
            {
                CB.iD = id++;
            }
        }
        public List<List<double[]>> Simulate(double maxT, double dT, int skip= 100)
        {
            List<List<double[]>> PosList = new List<List<double[]>>();
            Console.WriteLine("Making IDs");
            this.makeIDs();



            DateTime start = DateTime.UtcNow;
            long steps = Convert.ToInt64(maxT / dT);
            for(int i = 0; i < CelestialBodies.Count; i++)
            {
                // xList: 0, yList:1
                PosList.Add(new List<double[]>()
                {
                    new double[steps / skip ],
                    new double[steps / skip ]
                });
            }

            Console.WriteLine(steps);

            for (int i = 0; i < steps; i++)
            {
                // update forces for all planets
                foreach (CelestialBody c in CelestialBodies)
                {
                    c.Force = Vector<double>.Build.Dense(new double[2] {0,0});
                    foreach(CelestialBody c2 in CelestialBodies)
                    {
                        c.Force += CalcGravForce(c, c2);
                        
                    }
                }


                int idx = 0;
                // calc acc, vel, pos
                foreach (CelestialBody c in CelestialBodies)
                {
                    // f=ma
                    c.Acc = c.Force / c.Mass;
                    c.Pos += c.Acc * dT * dT * 0.5 + c.Vel * dT;
                    c.Vel += c.Acc * dT;


                    if (i % skip == 0)
                    {
                        PosList[idx][0][i/skip] = c.Pos[0];
                        PosList[idx][1][i/skip] = c.Pos[1];
                        // only plot every 100000 steps and dont plot twice:
                        if (i % 100000 == 0 && idx == 0)
                        {
                            if (this.error)
                            {
                                Console.WriteLine($"currently at step {i}/{steps} ({(float)i / (float)steps * 100.0} %) ERROR OCCURRED");
                            }
                            else
                            {
                                Console.WriteLine($"currently at step {i}/{steps} ({(float)i / (float)steps * 100.0} %)");
                            }
                        }
                    }
                    idx++;
                }

            }
            DateTime end = DateTime.UtcNow;
            TimeSpan timeDiff = end - start;
            Console.WriteLine($"Finished in {Convert.ToInt32(timeDiff.TotalSeconds)} Second(s)");

            return PosList;
        }
    }
}
