using planetSim;
using NumSharp;
using ScottPlot;
using System.Drawing;

double minT = 0, maxT = 60*15e6, dT = 10;

Universe u = new Universe();

CelestialBody sun = new CelestialBody(
    vel: new double[2] { 0, 0 },
    pos: new double[2] { 0, 0 },
    Constants.massOfSun
    );
CelestialBody earth = new CelestialBody(
    vel: new double[2] { 0, Constants.tanVelEarthSun },
    pos: new double[2] { Constants.disEarthSun, 0 },
    Constants.massOfEarth
    );

u.AddBody(sun);
u.AddBody(earth);

var BosList = u.Simulate(maxT, dT);

var plt = new ScottPlot.Plot(600, 400);
plt.Benchmark(enable: true);
plt.Style(Style.Black);
plt.Title("Style.Black");
plt.XLabel("t");
plt.YLabel("Cordinate");

double[] xCordpl1 = BosList[0][0];
double[] yCordpl1 = BosList[0][1];

double[] xCordpl2 = BosList[1][0];
double[] yCordpl2 = BosList[1][1];




plt.AddScatter(xCordpl1, yCordpl1, color: Color.Blue, label: "Planet1");
plt.AddScatter(xCordpl2, yCordpl2, color: Color.Green, label: "planet2");

new ScottPlot.FormsPlotViewer(plt).ShowDialog();



return;
