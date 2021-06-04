using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.Distributions;
namespace ApStatToolsLibrary
{
    public class Ttest
    {
        public string work { get; set; }
        public string data { get; set; }
        public string Conclusion { get; set; }
        public static double FindMean(List<double> list)
        {
            double MorphIntoMean = 0;
            foreach (double dub in list)
            {
                MorphIntoMean += dub;
            }
            return MorphIntoMean / list.Count;
        }
        public int SampleSize { get; set; }
        public double SampleMean { get; set; }
        public double SignificanceLevel { get; set; }
        public double StandardDeviation { get; set; }
        public int DegreesOfFreedom { get; set; }       
        public double TValue { get; set; }
        public static double FindStandardDev(List<double> SampleVals, double Mean)
        {
            List<double> newvals = new List<double>();
            double placeholder;
            foreach (double val in SampleVals)
            {
                placeholder = val - Mean;
                newvals.Add(placeholder * placeholder);
            }
            placeholder = 0;
            foreach (double val in newvals)
            {
                placeholder += val;
            }
            return Math.Sqrt(placeholder / (newvals.Count - 1));
        }
        public double PValue { get; set; }
        public double ExpectedMean { get; set; }
        public double TestStatistic { get; set; }
        public static double StandardErrorCalc(int none, int ntwo, double T, double SDone, double SDtwo)
        {
            double first = SDone * SDone / none;
            double second = SDtwo * SDtwo / ntwo;
            double StandardError = Math.Sqrt(first + second);
            return T * StandardError;
        }
        public static double TestStatCalc(double xbar, double mu, double estimatedSD)
        {
            double numerator = xbar - mu;
            return numerator / estimatedSD;
        }
        public static double PvalSolve( int DF, double t, string LessOrGreater)
        {
            double Value;
            if (LessOrGreater == ">")
            {
                Value = 1 - StudentT.CDF(0, 1, DF,t);
            }
            else if (LessOrGreater == "<")
            {
                Value = StudentT.CDF(0, 1, DF, t);
            }
            else
            {
                if (t > 0)
                {
                    Value = (1 - StudentT.CDF(0, 1, DF, t)) * 2;
                }
                else
                {
                    Value = StudentT.CDF(0, 1, DF, t) * 2;
                }

            }
            return Value;
        }
        public static string WriteConclusions(double PValue, double Alpha)
        {
            string Conclusion;
            if (PValue < Alpha)
            {
                Conclusion = $"Because the p value of {PValue} is less than the Alpha level of {Alpha}, there is sufficient statistical evidence to reject the null hypothesis";
            }
            else
            {
                Conclusion = $"Because the p value of {PValue} is greater than the Alpha level of {Alpha}, there is not sufficient statistical evidence to reject the null hypothesis";
            }
            return Conclusion;
        }
    }
    public class OnePropTInterval : Ttest
    {
        public double MinVal { get; set; }
        public double MaxVal { get; set; }
        
        public OnePropTInterval(int SS, double SM, double SD, double SL)
        {
            SampleSize = SS;
            SampleMean = SM;
            SignificanceLevel = SL;
            StandardDeviation = SD;
            DegreesOfFreedom = SampleSize - 1;

            double Tstat = StudentT.InvCDF(0,1,DegreesOfFreedom, (1 - SignificanceLevel) / 2);
            TValue = Math.Abs(Tstat);
            MinVal = SampleMean - TTimesSE(SampleSize, StandardDeviation, TValue);
            MaxVal = SampleMean + TTimesSE(SampleSize, StandardDeviation, TValue);
            WriteConc();
        }
        private void WriteConc()
        {
            string ConfidenceLevel = SignificanceLevel.ToString().Remove(0, SignificanceLevel.ToString().IndexOf('.') + 1);
            Conclusion = $"we are {ConfidenceLevel} percent confident that the true population proportion ______ lies within the interval ({MinVal} , {MaxVal})";
        }



        private static double TTimesSE(int n, double StandardDeviation, double T)
        {
            double StandardError = StandardDeviation / Math.Sqrt(n);
            return T * StandardError;
            
        }
    }
    public class TwoPropTInterval : Ttest
    {
        public int SecondSampleSize { get; set; }
        public double SecondSampleMean { get; set; }
        public double SecondStandardDeviation { get; set; }
        public double MinVal { get; set; }
        public double MaxVal { get; set; }
       
        public string DifforAdd { get; set; }
        public double DAStatistic { get; set; }
        public TwoPropTInterval(int SSone, double SPone, double SDone, double SL, int SStwo, double SPtwo, double SDtwo, string AdditionOrSubstraction)
        {
            SampleSize = SSone;
            SampleMean = SPone;
            SignificanceLevel = SL;
            SecondSampleSize = SStwo;
            SecondSampleMean = SPtwo;
            StandardDeviation = SDone;
            SecondStandardDeviation = SDtwo;
            DegreesOfFreedom = SampleSize - 1 +  SecondSampleSize - 1;
            
            double Tstat = StudentT.InvCDF(0, 1, DegreesOfFreedom, (1 - SignificanceLevel) / 2);
            TValue = Math.Abs(Tstat);
            double Statistic;
            string ConfidenceLevel = SignificanceLevel.ToString().Remove(0, SignificanceLevel.ToString().IndexOf('.') + 1);
            
            if (AdditionOrSubstraction == "plus")
            {
                Statistic = SampleMean + SecondSampleMean;
                DAStatistic = Statistic;
                MinVal = Statistic - StandardErrorCalc(SampleSize, SecondSampleSize, TValue, StandardDeviation,SecondStandardDeviation);
                MaxVal = Statistic + StandardErrorCalc(SampleSize, SecondSampleSize, TValue, StandardDeviation, SecondStandardDeviation);
                Conclusion = $"we are {ConfidenceLevel} percent confident that the sum of the population proportions ______ lies within the interval ({MinVal} , {MaxVal})";
                DifforAdd = "Sum";
            }
            else
            {
                Statistic = SampleMean - SecondSampleMean;
                DAStatistic = Statistic;
                MinVal = Statistic - StandardErrorCalc(SampleSize, SecondSampleSize, TValue, StandardDeviation, SecondStandardDeviation);
                MaxVal = Statistic + StandardErrorCalc(SampleSize, SecondSampleSize, TValue, StandardDeviation, SecondStandardDeviation);
                Conclusion = $"we are {ConfidenceLevel} percent confident that the difference of the population proportions ______ lies within the interval ({MinVal} , {MaxVal})";
                DifforAdd = "Difference";
            }

        }
        

    }
    public class OnePropTTest : Ttest
    {
        public string LessOrGreater { get; set; }
        public double Alpha { get; set; }
        public OnePropTTest(double ExpMVal, double SampleMVal, double SampleSDVal, int SampleNVal, double Alpha, string LorG) 
        {
            ExpectedMean = ExpMVal;
            SampleMean = SampleMVal;
            SampleSize = SampleNVal;
            StandardDeviation = SampleSDVal;
            TestStatistic = TestStatCalc(SampleMean, ExpectedMean, OnePopStandardError(StandardDeviation,SampleSize));
            DegreesOfFreedom = SampleSize - 1;
            LessOrGreater = LorG;
            this.Alpha = Alpha;

            PValue = PvalSolve(DegreesOfFreedom, TestStatistic, LessOrGreater);
            Conclusion = WriteConclusions(PValue,Alpha);
            work = writework(SampleMean, ExpectedMean, StandardDeviation, SampleSize, TestStatistic,PValue);
            WriteData();
        }
        private static string writework (double SampleMean, double ExpectedMean, double StandardDeviation, int SampleSize, double TestStatistic, double Pval)
        {
            return $"t = ( xbar - mu ) / ( s / Sqrt(n) ) = ( {SampleMean} - {ExpectedMean} ) / ( {StandardDeviation} / Sqrt({SampleSize})  = {TestStatistic} \n P-value = StudentT.cdf(Mean: 0, Standard Deviation: 1, Degrees of Freedom: {SampleSize - 1}, Test Statistic: {TestStatistic} ) = {Pval}";
            
        }
        private void WriteData()
        {
            data = $" null mean: {ExpectedMean}\n Xbar: {SampleMean}\n Standard dev: {StandardDeviation}\n Sample size: {SampleSize}\n Alpha: {Alpha}\n Pvalue: {PValue} ";
        }
        public OnePropTTest(double ExpectedMean, List<double> SampleVals, double Alpha, string Sign)
        {
            this.ExpectedMean = ExpectedMean;
            SampleMean = FindMean(SampleVals);
            SampleSize = SampleVals.Count;
            DegreesOfFreedom = SampleSize - 1;
            StandardDeviation = FindStandardDev(SampleVals, SampleMean);
            this.Alpha = Alpha;
            LessOrGreater = Sign;
            TestStatistic = TestStatCalc(SampleMean, ExpectedMean, OnePopStandardError(StandardDeviation, SampleSize));
            PValue = PvalSolve(DegreesOfFreedom, TestStatistic, LessOrGreater);
            Conclusion = WriteConclusions(PValue, Alpha);

            work = writework(SampleMean, ExpectedMean, StandardDeviation, SampleSize, TestStatistic,PValue);
            WriteData();
        }
        public bool MatchedPairs { get; set; }
        public OnePropTTest(List<double> SampleValsOne, List<double> SampleValsTwo, double Alpha, string Sign, double ExpectedMean)
        {
            List<double> Differences = new List<double>();
            for (int i = 0; i < SampleValsOne.Count; i++)
            {
                Differences.Add(SampleValsOne[i] - SampleValsTwo[i]);
            }
            SampleMean = FindMean(Differences);
            this.ExpectedMean = ExpectedMean;
            SampleSize = Differences.Count;
            DegreesOfFreedom = SampleSize - 1;
            StandardDeviation = FindStandardDev(Differences, SampleMean);
            this.Alpha = Alpha;
            LessOrGreater = Sign;
            TestStatistic = TestStatCalc(SampleMean, ExpectedMean, OnePopStandardError(StandardDeviation, SampleSize));
            PValue = PvalSolve(DegreesOfFreedom, TestStatistic, LessOrGreater);
            Conclusion = WriteConclusions(PValue, Alpha);
            writework(SampleMean, ExpectedMean, StandardDeviation, SampleSize, TestStatistic, PValue);
            WriteData();
            MatchedPairs = true;
        }
        private static double OnePopStandardError(double sd, double n)
        {
           
            return sd / Math.Sqrt(n);
        }

    }
    public class TwoPropTTest : Ttest
    {
        public string LessOrGreater { get; set; }
        public double Alpha { get; set; }
        public double SecondSampleMean { get; set; }
        public double SecondSampleStandardDeviation { get; set; }
        public int SecondN { get; set; }
        public TwoPropTTest(double ExpMVal, double SampleMVal, double SampleSDVal, double secondMV, double secondSD, int secondn, int SampleNVal, double Alpha, string LorG)
        {
            ExpectedMean = ExpMVal;
            SampleMean = SampleMVal;
            SampleSize = SampleNVal;
            StandardDeviation = SampleSDVal;
            SecondSampleMean = secondMV;
            SecondSampleStandardDeviation = secondSD;
            SecondN = secondn;
            TestStatistic = TestStatCalc(SampleMean - SecondSampleMean, ExpectedMean, TwoPopStandardError(StandardDeviation, SecondSampleStandardDeviation, SampleSize,SecondN));
            DegreesOfFreedom = Math.Min(SampleSize - 1, SecondN - 1);
            LessOrGreater = LorG;
            this.Alpha = Alpha;

            PValue = PvalSolve(DegreesOfFreedom, TestStatistic, LessOrGreater);
            Conclusion = WriteConclusions(PValue, Alpha);
            Writework();
        }
        public TwoPropTTest(List<double> SampleValsOne, List<double> SampleValsTwo, double Alpha, string Sign)
        {
            
            SampleMean = FindMean(SampleValsOne);
            SampleSize = SampleValsOne.Count;
            
            StandardDeviation = FindStandardDev(SampleValsOne, SampleMean);
          
            SecondSampleMean = FindMean(SampleValsTwo);
            SecondN = SampleValsOne.Count;
            DegreesOfFreedom = Math.Min(SampleSize - 1, SecondN - 1);

            SecondSampleStandardDeviation = FindStandardDev(SampleValsTwo, SecondSampleMean);
            TestStatistic = TestStatCalc(SampleMean - SecondSampleMean, ExpectedMean, TwoPopStandardError(StandardDeviation, SecondSampleStandardDeviation, SampleSize, SecondN));

            this.Alpha = Alpha;
            LessOrGreater = Sign;
            PValue = PvalSolve(DegreesOfFreedom, TestStatistic, LessOrGreater);
            Conclusion = WriteConclusions(PValue, Alpha);
            Writework();
        }
        
        private void Writework()
        {
            work = $"t = ( (xbar1 - xbar2) - 0 ) / Sqrt(s1^2 / n1  +  s2^2 / n2)  = ( ({SampleMean} - {SecondSampleMean}) - 0 ) / Sqrt({StandardDeviation}^2 / {SampleSize}  +  {SecondSampleStandardDeviation}^2 / {SecondN}) = {TestStatistic}";
            data = $"Mean1:{SampleMean} \nStandard deviation1:{StandardDeviation} \nSample Size1:{SampleSize} \n \nMean2:{SecondSampleMean} \nStandard deviation2:{SecondSampleStandardDeviation} \nSample Size2:{SecondN} \nAlpha:{Alpha} \nPvalue: {PValue}";
        }
        private static double TwoPopStandardError(double sdone, double sdtwo, int none, int ntwo)
        {
            double first = sdtwo * sdtwo / ntwo;
            double second = sdone * sdone / none;
            return Math.Sqrt(first + second);
        }
    }
}
