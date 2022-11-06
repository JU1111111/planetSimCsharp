using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace planetSim
{
    internal class Class1
    {
        public Class1(string yeeete)
        {
            Console.WriteLine(yeeete);
        }
        public void bruh()
        {
            double[] testar = { 1, 2 };
            double[] testar2 = { 1, 2 };
            var V = Vector<double>.Build;
            Vector<double> v = V.DenseOfArray(testar);
            Vector<double> b = V.DenseOfArray(testar2);

            double c = v.L2Norm();

            Console.WriteLine(v);
            Console.WriteLine(b);
            Console.WriteLine(c);
        }
    }
}
