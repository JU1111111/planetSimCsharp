using planetSim;
using ScottPlot;
using System.Drawing;


double minT = 0, maxT = 15e6, dT = 10;

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

CelestialBody moon = new CelestialBody(
    vel: new double[2] { Constants.tanVelMoonEarth, 0},
    pos: new double[2] { Constants.disEarthSun ,Constants.disMoonEarth },
    Constants.massOfMoon
    );



u.AddBody(sun);
u.AddBody(earth);
u.AddBody(moon);

var BosList = u.Simulate(maxT, dT);


/*
double[] xCordpl1 = BosList[0][0];
double[] yCordpl1 = BosList[0][1];

double[] xCordpl2 = BosList[1][0];
double[] yCordpl2 = BosList[1][1];

double[] xCordpl2 = BosList[2][0];
double[] yCordpl2 = BosList[2][1];




plt.AddScatter(xCordpl1, yCordpl1, color: Color.Blue, label: "Planet1");
plt.AddScatter(xCordpl2, yCordpl2, color: Color.Green, label: "planet2");
new ScottPlot.FormsPlotViewer(plt).ShowDialog();*/

Color[] colors = new Color[] { Color.Blue, Color.Green, Color.Red };



void plotStuff(List<List<double[]>> BosList)
{
    var plt = new ScottPlot.Plot(600, 400);
    plt.Benchmark(enable: true);
    plt.Style(Style.Black);
    plt.Title("Style.Black");
    plt.XLabel("t");
    plt.YLabel("Cordinate");

    int i = 0;
    foreach (List<double[]> plcords in BosList)
    {
        double[] xCord = plcords[0];
        double[] yCord = plcords[1];
        plt.AddScatter(xCord, yCord, color: colors[i], label: $"Planet{i}");
        i++;
    }
    new ScottPlot.FormsPlotViewer(plt).ShowDialog();
}

plotStuff(BosList);
