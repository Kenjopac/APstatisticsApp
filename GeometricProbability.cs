using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.Distributions;
namespace ApStatToolsLibrary
{

    public class GeometricProbability
    {
        public int NumberOfInterest { get; set; }
        public double Probability { get; set; }
        private int OneBelow;
        private int OneAbove;
        public double GreaterThan { get; set; }
        public double GreaterThanEqualTo { get; set; }
        public double LessThanEqualTo { get; set; }
        public double LessThan { get; set; }
        public string Data { get; set; }
        public string Work { get; set; }
        public GeometricProbability(int n, double p)
        {
            NumberOfInterest = n;
            Probability = p;
            OneBelow = NumberOfInterest - 1;
            OneAbove = NumberOfInterest + 1;
            LessThan = Geometric.CDF(Probability,OneBelow);
            GreaterThan = 1 - Geometric.CDF(Probability,OneAbove);
            GreaterThanEqualTo = Geometric.CDF(Probability, NumberOfInterest);
            LessThanEqualTo = 1 - Geometric.CDF(Probability, NumberOfInterest);
            Data = $"number of interest = {NumberOfInterest}\nProbability = {Probability}\nP(x > {NumberOfInterest}) = {GreaterThan}\nP(x >= {NumberOfInterest}) = {GreaterThanEqualTo}\nP(x < {NumberOfInterest}) = {LessThan}\nP(x <= {NumberOfInterest}) = {LessThanEqualTo}";
            Work = $"GeometricCDF(Probability of success: {Probability}, Number of interest: {NumberOfInterest}) = {LessThanEqualTo}";
        }
    }
}
