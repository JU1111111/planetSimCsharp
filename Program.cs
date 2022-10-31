using System;
using planetSim;
using NumSharp;



var pos1 = np.array(new double[] {1.0, 0.0});
var vel1 = np.array(new double[] { 0.0, 0.1 });

Planet pl1 = new(100000.0, pos1, vel1);


var pos2 = np.array(new double[] { 0.0, 0.0 });
var vel2 = np.array(new double[] { 0.0, 0.0 });

Planet pl2 = new(999999999.0, pos2, vel2);



Planet[] pls = { pl1, pl2 };

Universe universe = new(pls);
universe.AddPlanetIds();

double minT = 0, maxT = 100, dT = 0.0001;

universe.SimThisshit(minT, maxT, dT);


/* foreach (var bruh in universe.planets[0].xPoslist)
{
    Console.WriteLine(bruh);
}*/