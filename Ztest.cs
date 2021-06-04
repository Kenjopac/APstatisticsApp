using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.Distributions;
namespace ApStatToolsLibrary
{
    public class Ztest
    {
        public string Conclusion { get; set; }
        public int SampleSize { get; set; }
        public double SampleProportion { get; set; }
        public double SignificanceLevel { get; set; }
        public double ZValue { get; set; }
        public static double StandErr(int n, double phat)
        {
            return Math.Sqrt(phat * (1 - phat) / n); 
        }
    }        
    public class OnePropZInterval : Ztest
    {
        public double MinVal { get; set; }
        public double MaxVal { get; set; }
        public OnePropZInterval(int SS, double SP, double SL)
        {
            SampleSize = SS;
            SampleProportion = SP;
            SignificanceLevel = SL;

            double Zstat = Normal.InvCDF(0, 1, (1 - SL) / 2);
            ZValue = Math.Abs(Zstat);
            MinVal = SampleProportion - ZTimesSE(SampleSize, SampleProportion, ZValue);
            MaxVal = SampleProportion + ZTimesSE(SampleSize, SampleProportion, ZValue);
            WriteConc();
        }
        private void WriteConc()
        {
            string ConfidenceLevel = SignificanceLevel.ToString().Remove(0,SignificanceLevel.ToString().IndexOf('.') + 1);
            Conclusion = $"we are {ConfidenceLevel} percent confident that the true population proportion ______ lies within the interval ({MinVal} , {MaxVal})";
        }        
        private static double ZTimesSE(int n, double phat, double Z)
        {
            return Z * StandErr(n , phat);
        }
    }
    public class TwoPropZInterval : Ztest
    {
        public int SecondSampleSize { get; set; }
        public double SecondSampleProportion { get; set; }
        public double MinVal { get; set; }
        public double MaxVal { get; set; }
        public string DifforAdd { get; set; }
        public double DAStatistic { get; set; }
        public TwoPropZInterval(int SSone, double SPone, double SL, int SStwo, double SPtwo, string AdditionOrSubstraction)
        {
            SampleSize = SSone;
            SampleProportion = SPone;
            SignificanceLevel = SL;
            SecondSampleSize = SStwo;
            SecondSampleProportion = SPtwo;
            double Zstat = Normal.InvCDF(0, 1, (1 - SL) / 2);
            ZValue = Math.Abs(Zstat);
            double Statistic;
            string ConfidenceLevel = SignificanceLevel.ToString().Remove(0, SignificanceLevel.ToString().IndexOf('.') + 1);
            if (AdditionOrSubstraction == "plus")
            {
                Statistic = SampleProportion + SecondSampleProportion;
                DAStatistic = Statistic;
                MinVal = Statistic - StandardErrorCalc(SampleSize, SecondSampleSize, SampleProportion, SecondSampleProportion, ZValue);
                MaxVal = Statistic + StandardErrorCalc(SampleSize, SecondSampleSize, SampleProportion, SecondSampleProportion, ZValue);
                Conclusion = $"we are {ConfidenceLevel} percent confident that the sum of the population proportions ______ lies within the interval ({MinVal} , {MaxVal})";
                DifforAdd = "Sum";
            } else
            {
                Statistic = SampleProportion - SecondSampleProportion;
                DAStatistic = Statistic;
                MinVal = Statistic - StandardErrorCalc(SampleSize, SecondSampleSize, SampleProportion, SecondSampleProportion, ZValue);
                MaxVal = Statistic + StandardErrorCalc(SampleSize, SecondSampleSize, SampleProportion, SecondSampleProportion, ZValue);
                Conclusion = $"we are {ConfidenceLevel} percent confident that the difference of the population proportions ______ lies within the interval ({MinVal} , {MaxVal})";
                DifforAdd = "Difference";
            }

        }
        private static double StandardErrorCalc(int none, int ntwo, double Phatone, double Phattwo, double Z)
        {
        double firstnum = Phatone * (1 - Phatone) / none;
        double secondnum = Phattwo * (1 - Phattwo) / ntwo;

        double StandardError = Math.Sqrt(firstnum + secondnum);
        return Z * StandardError;
        }
        
    }
    public class OneSampleZTest
    {
        public int SampleSize { get; set; }
        public double ObservedValue { get; set; }
        public double Alpha { get; set; }
        public double PopulationParameter { get; set; }
        public string Sign { get; set; }
        public string Work { get; set; }
        public double TestStatistic { get; set; }
        public double Pvalue { get; set; }
        public string Conclusion { get; set; }
        public bool MeanOrnoMean { get; set; }
        public double StandardDeviation { get; set; }
        public OneSampleZTest(string sign, double PopulationProportion, double ObservedSampleSuccesses, int SampleSize, double AlphaLevel)
        {
            this.SampleSize = SampleSize;
            ObservedValue = ObservedSampleSuccesses / SampleSize;
            Alpha = AlphaLevel;
            PopulationParameter = PopulationProportion;
            Sign = sign;
            MeanOrnoMean = false;
            DoworkProp(ObservedSampleSuccesses);
        }

        private void DoworkProp(double HardNum)
        {
            
            TestStatistic = (ObservedValue - PopulationParameter) / StandardErrorProp(SampleSize, PopulationParameter);
            Work = $"z = ( p-hat - p_0 ) / Sqrt(p_0(1 - p_0) / n) = \np-hat = {HardNum} / {SampleSize} = {ObservedValue} \n( {ObservedValue} - {PopulationParameter} ) / Sqrt({PopulationParameter}(1 - {PopulationParameter}) / {SampleSize}) = {TestStatistic}";
            Pvalue = PvalueChecking(Sign, TestStatistic);
            WriteConclusion();
        }
        private void WriteConclusion()
        {
            if (Pvalue <= Alpha)
            {
                Conclusion = $"Because the P value of {Pvalue} is less than the ALpha level of {Alpha}, we have sufficient statistical evidence to reject the null hypothesis";
            }
            else
            {
                Conclusion = $"Because the P value of {Pvalue} is Greater than the ALpha level of {Alpha}, we do not have sufficient statistical evidence to reject the null hypothesis";
            }
        }
        private static double PvalueChecking(string Sign, double TestStat)
        {
            switch (Sign)
            {
                case "!=":
                    if (TestStat < 0)
                    {
                        return Normal.CDF(0 , 1 , TestStat) * 2;
                    }
                    else
                    {
                        return (1 - Normal.CDF(0, 1, TestStat)) * 2;
                    }                   
                case ">":
                    return 1 - Normal.CDF(0,1,TestStat);
                    
                case "<":
                    return Normal.CDF(0, 1, TestStat);
                
                default:
                    return 0;
            }
        }
        private static double StandardErrorProp(int n, double p)
        {
            return Math.Sqrt(p * (1 - p) / n);
        }
        private static double StandardErrorMean(int n, double x)
        {
            return x / Math.Sqrt(n);
        }
        public OneSampleZTest(string sign, double PopulationMean, double ObservedMean, double StandardDeviation, int SampleSize, double AlphaLevel)
        {
            this.SampleSize = SampleSize;
            ObservedValue = ObservedMean;
            Alpha = AlphaLevel;
            PopulationParameter = PopulationMean;
            this.StandardDeviation = StandardDeviation;
            Sign = sign;
            MeanOrnoMean = true;
            DoworkMean();
        }
        private void DoworkMean()
        {
            
            TestStatistic = (ObservedValue - PopulationParameter) / StandardErrorMean(SampleSize, StandardDeviation);
            Work = $"z = ( x-bar - p_0 ) / (sigma / Sqrt(n)) = ( {ObservedValue} - {PopulationParameter} ) / ({StandardDeviation} / Sqrt({SampleSize})) = {TestStatistic} ";
            Pvalue = PvalueChecking(Sign, TestStatistic);
            WriteConclusion();
        }
    }
    public class TwoSampleZTest
    {
        public int SampleSizeOne { get; set; }
        public int SampleSizeTwo { get; set; }
        public double ObservedPropOne { get; set; }
        public double ObservedPropTwo { get; set; }
        public double PooledPHat { get; set; }
        public double Alpha { get; set; }
        public string Sign { get; set; }
        public string Work { get; set; }
        public double TestStatistic { get; set; }
        public double Pvalue { get; set; }
        public string Conclusion { get; set; }
        public double xOne { get; set; }
        public double xTwo { get; set; }
        public double SigmaOne { get; set; }
        public double SigmaTwo { get; set; }
        public bool MeanOrnoMean { get; set; }
        public TwoSampleZTest(string sign, double ObservedOne, double ObservedTwo, int SizeOne, int SizeTwo, double AlphaLevel, bool PooledYN)
        {
            Sign = sign;
            xOne = ObservedOne;
            xTwo = ObservedTwo;
            SampleSizeOne = SizeOne;
            SampleSizeTwo = SizeTwo;
            Alpha = AlphaLevel;
            MeanOrnoMean = false;
            DoWorkProp(PooledYN);
        }
        public TwoSampleZTest(string sign, double XbarOne, double XBarTwo, double StandDevOne, double StandDevTwo, int SizeOne , int SizeTwo, double AlphaLevel)
        {
            Sign = sign;
            xOne = XbarOne;
            xTwo = XBarTwo;
            SigmaOne = StandDevOne;
            SigmaTwo = StandDevTwo;
            SampleSizeOne = SizeOne;
            SampleSizeTwo = SizeTwo;
            Alpha = AlphaLevel;
            MeanOrnoMean = true;
            DoWorkMean();
        }
        private void DoWorkProp(bool Pooled)
        {
            ObservedPropOne = xOne / SampleSizeOne;
            ObservedPropTwo = xTwo / SampleSizeTwo;

            if (Pooled == true)
            {
                PooledPHat = (xOne + xTwo) / (SampleSizeOne + SampleSizeTwo);
                TestStatistic = (ObservedPropOne - ObservedPropTwo) / CalcSEProp(PooledPHat,PooledPHat,SampleSizeOne,SampleSizeTwo);
                Work = $"z = ((p_1 - p_2) - 0) / Sqrt(p_0(1 - p_0) / n_1  +  p_0(1 - p_0) / n_2) \np_0 = ({xOne} + {xTwo}) / ({SampleSizeOne} + {SampleSizeTwo}) \np_1 = {ObservedPropOne} = {xOne} / {SampleSizeOne}\np_2 = {ObservedPropTwo} = {xTwo} / {SampleSizeTwo}\n = (({ObservedPropOne} - {ObservedPropTwo}) - 0) / Sqrt({PooledPHat}(1 - {PooledPHat}) / {SampleSizeOne}  +  {PooledPHat}(1 - {PooledPHat}) / {SampleSizeTwo}) = {TestStatistic}";
                Pvalue = PvalueChecking(Sign, TestStatistic);
                WriteConclusion();
            }
            else
            {
                TestStatistic = (ObservedPropOne - ObservedPropTwo) / CalcSEProp(ObservedPropOne, ObservedPropTwo, SampleSizeOne, SampleSizeTwo);
                Work = $"z = ((p_1 - p_2) - 0) / Sqrt(p_1(1 - p_1) / n_1  +  p_2(1 - p_2) / n_2)\np_1 = {ObservedPropOne} = {xOne} / {SampleSizeOne}\np_2 = {ObservedPropTwo} = {xTwo} / {SampleSizeTwo}\n = (({ObservedPropOne} - {ObservedPropTwo}) - 0) / Sqrt({ObservedPropOne}(1 - {ObservedPropOne}) / {SampleSizeOne}  +  {ObservedPropTwo}(1 - {ObservedPropTwo}) / {SampleSizeTwo}) = {TestStatistic}";
                Pvalue = PvalueChecking(Sign, TestStatistic);
                WriteConclusion();
            }
        }
        private void DoWorkMean()
        {
            TestStatistic = xOne - xTwo / CalcSEMean(SigmaOne, SigmaTwo, SampleSizeOne, SampleSizeTwo);
            Work = $"z = ((xbar_1 - xbar_2) - 0) / Sqrt(sigma_1^2 / n_1  +  sigma_2^2 / n_2) = {xOne} - {xTwo} / Sqrt( {SigmaOne}^2 / {SampleSizeOne} + {SigmaTwo}^2 / {SampleSizeTwo}) = {TestStatistic}";
            Pvalue = PvalueChecking(Sign, TestStatistic);
            WriteConclusion();
        }
        private static double CalcSEMean(double Sa, double Sb, double Na, double Nb)
        {
            double a = Sa * Sa / Na;
            double b = Sb * Sb / Nb;
            return Math.Sqrt(a + b);
        }
        private static double CalcSEProp(double PHa,double PHb, int SampleSizeOne, int SampleSizeTwo)
        {
            double a = PHa * (1 - PHa) / SampleSizeOne;
            double b = PHb * (1 - PHb) / SampleSizeTwo;
            return Math.Sqrt(a + b);
        }
        private static double PvalueChecking(string Sign, double TestStat)
        {
            switch (Sign)
            {
                case "!=":
                    if (TestStat < 0)
                    {
                        return Normal.CDF(0, 1, TestStat) * 2;
                    }
                    else
                    {
                        return (1 - Normal.CDF(0, 1, TestStat)) * 2;
                    }
                case ">":
                    return 1 - Normal.CDF(0, 1, TestStat);

                case "<":
                    return Normal.CDF(0, 1, TestStat);

                default:
                    return 0;
            }
        }
        private void WriteConclusion()
        {
            if (Pvalue <= Alpha)
            {
                Conclusion = $"Because the P value of {Pvalue} is less than the ALpha level of {Alpha}, we have sufficient statistical evidence to reject the null hypothesis";
            }
            else
            {
                Conclusion = $"Because the P value of {Pvalue} is Greater than the ALpha level of {Alpha}, we do not have sufficient statistical evidence to reject the null hypothesis";
            }
        }
    }
}
