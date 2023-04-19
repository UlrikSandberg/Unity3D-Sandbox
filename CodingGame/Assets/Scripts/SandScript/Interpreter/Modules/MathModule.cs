using System;

namespace SandScript.Interpreter.Modules
{
    public class MathModule
    {
        public double Sin(double a) => Math.Sin(a);

        public double Cos(double a) => Math.Cos(a);

        public double Tan(double a) => Math.Tan(a);

        public double Sqrt(double a) => Math.Sqrt(a);
    }
}