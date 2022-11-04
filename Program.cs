using planetSim;
using NumSharp;
using ScottPlot;

/*Class1 yee = new("yeet");

yee.bruh();*/

double[] pos1 = new double[] {1.0, 0.0};
double[] vel1 = new double[] { 0.0, 0.1 };

Planet pl1 = new(10000.0, pos1, vel1);


double[] pos2 = new double[] { 0.0, 0.0 };
double[] vel2 = new double[] { 0.0, 0.0 };

Planet pl2 = new(999999999.0, pos2, vel2);



Planet[] pls = { pl1, pl2 };

Universe universe = new(pls);
universe.AddPlanetIds();

double minT = 0, maxT = 100, dT = 0.0001;

universe.SimThisshit(minT, maxT, dT);

var plt = new ScottPlot.Plot(600, 400);
plt.Benchmark(enable: true);
plt.Style(Style.Black);
plt.Title("Style.Black");
plt.XLabel("t");
plt.YLabel("Cordinate");

List <Double> pl1xcordL = universe.planets[0].xPoslist;
List<Double> pl1ycord = universe.planets[0].yPoslist;
double[] xCordpl1 = pl1xcordL.ToArray();
double[] yCordpl1 = pl1ycord.ToArray();

int sampleRate = 10000;

plt.AddSignal(xCordpl1, sampleRate, label:"Xcord");
plt.AddSignal(yCordpl1, sampleRate, label: "Ycord");
new ScottPlot.FormsPlotViewer(plt).ShowDialog();


/* foreach (var bruh in universe.planets[0].xPoslist)
{
    Console.WriteLine(bruh);
}*/