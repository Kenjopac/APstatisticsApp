using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.Distributions;


namespace ApStatToolsLibrary
{
    public class ChiSquareStats
    {
        public string Conclusion { get; set; }
        public string TestType { get; set; }
        public string NullHypothesis { get; set; }
        public string AlternativeHypothesis { get; set; }
        public double SignificanceLevel { get; set; }
        public double DegreesOfFreedom { get; set; }
        public List<double> Observed { get; set; } = new List<double>();
        public List<double> Expected { get; set; } = new List<double>();
        public double ChiSquareStatistic { get; set; }
        public double Pval { get; set; }
        public ChiSquareStats(string testType, string nullHypothesis, string alternativeHypothesis, double significanceLevel, double degreesOfFreedom, List<double> observed, List<double> expected)
        {
            TestType = testType;
            NullHypothesis = nullHypothesis;
            AlternativeHypothesis = alternativeHypothesis;
            SignificanceLevel = significanceLevel;
            DegreesOfFreedom = degreesOfFreedom;
            Observed = observed;
            Expected = expected;
            plugindata();
        }
        private void plugindata() {
            ChiSquareStatistic = CalculateChiSquStat(Observed, Expected);           
            Pval = 1 - ChiSquared.CDF(DegreesOfFreedom, ChiSquareStatistic);
            WriteConclusion(SignificanceLevel, Pval, TestType);
        }
        private void WriteConclusion(double SignificanceLevel, double Pvalue, string TestType)
        {
            string GreaterOrEqualTo, Isisnt, anothervariable;
            if (Pval < SignificanceLevel)
            {
                GreaterOrEqualTo = "less than";
                Isisnt = "";
                anothervariable = "is";
                Conclusion = $"Because the P value of {Pval} is {GreaterOrEqualTo}  the significance level of {SignificanceLevel}, we {Isisnt}reject the null hypothesis that {NullHypothesis}. Therefore, there {anothervariable} sufficient statistical evidence to prove that {AlternativeHypothesis}";
            }
            else if (Pval == SignificanceLevel)
            {
                GreaterOrEqualTo = "equal to";
                Isisnt = "";
                anothervariable = "is";
                Conclusion = $"Because the P value of {Pval} is {GreaterOrEqualTo}  the significance level of {SignificanceLevel}, we {Isisnt}reject the null hypothesis that {NullHypothesis}. Therefore, there {anothervariable} sufficient statistical evidence to prove that {AlternativeHypothesis}";
            }
            else
            {
                GreaterOrEqualTo = "greater than";
                Isisnt = " fail to ";
                anothervariable = "isn't";
                Conclusion = $"Because the P value of {Pval} is {GreaterOrEqualTo}  the significance level of {SignificanceLevel}, we{Isisnt}reject the null hypothesis that {NullHypothesis}. Therefore, there {anothervariable} sufficient statistical evidence to prove that {AlternativeHypothesis}";
            }

        }
        private static double CalculateChiSquStat (List<double> observed, List<double> expected)
        {
            double returnval = 0;
            List<double> stats = new List<double>();

            if (expected.Count == 1)
            {
                for (int i = 0; i < observed.Count; i++)
                {
                    stats.Add(FindOneGridVal(observed[i], expected[0]));
                }
            }
            else
            {
                for (int i = 0; i < observed.Count; i++)
                {
                    stats.Add(FindOneGridVal(observed[i], expected[i]));
                }
            }

            foreach (double stat in stats)
            {
                returnval += stat;
            }
            return returnval;
        }

        
        private static double FindOneGridVal(double input, double expected)
        {
            double returnval = (input - expected) * (input - expected);
            double newval = returnval / expected;
            return newval;
        }
    }
}
