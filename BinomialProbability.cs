using System;
using System.Collections.Generic;
using System.Text;

namespace ApStatToolsLibrary
{
     public class BinomialProbability
    {       
            private static int TotalPermutations;
            private static decimal SuccessProbExpSuccessNum;
            private static decimal FailProbExpFailNum;
        public int Totalnum { get; set; }
        public int NumberOfSuccesses { get; set; }
        public double ProbabilityOfSuccess { get; set; }
        public int FailNum { get; set; }
        public double FailProbability { get; set; }
        public decimal ProbabilityOfGreaterThanSuccess { get; set; }
        public decimal ProbabilityOfGreaterEqualToSuccess { get; set; }
        public decimal ProbabilityOfEqualToSuccess { get; set; }
        public decimal ProbabilityOfLessEqualToSuccess { get; set; }
        public decimal ProbabilityOfLessThanSuccess { get; set; }
        
        public BinomialProbability(int Tot, int succnum, double propnum)
        {
            Totalnum = Tot;
            NumberOfSuccesses = succnum;
            ProbabilityOfSuccess = propnum;
            ProbabilityOfGreaterThanSuccess = GreaterThan(Totalnum, NumberOfSuccesses, ProbabilityOfSuccess);
            ProbabilityOfGreaterEqualToSuccess = GreaterThan(Totalnum, NumberOfSuccesses, ProbabilityOfSuccess) + GetOneBinomNum(Totalnum, NumberOfSuccesses, ProbabilityOfSuccess);
            ProbabilityOfEqualToSuccess = GetOneBinomNum(Totalnum, NumberOfSuccesses, ProbabilityOfSuccess);
            ProbabilityOfLessEqualToSuccess = LessThan(Totalnum, NumberOfSuccesses, ProbabilityOfSuccess) + GetOneBinomNum(Totalnum, NumberOfSuccesses, ProbabilityOfSuccess);
            ProbabilityOfLessThanSuccess = LessThan(Totalnum, NumberOfSuccesses, ProbabilityOfSuccess);
            FailNum = Totalnum - NumberOfSuccesses;
            FailProbability = 1 - ProbabilityOfSuccess;
        }
        private static decimal GreaterThan(int total, int wins, double probOfSucc)
        {
            return 1 - (LessThan(total, wins, probOfSucc) + EqualTo(total, wins, probOfSucc));
        }
        private static decimal LessThan(int total, int wins, double probOfSucc)
        {
            decimal retnum = 0;
            for (int i = 0 ; i < wins ; i++)
            {
                retnum += GetOneBinomNum(total, i, probOfSucc);
            }
            return retnum;
        }
        private static decimal EqualTo(int total, int wins, double probOfSucc)
        {
            return GetOneBinomNum(total, wins, probOfSucc);
        }
        
        private static decimal GetOneBinomNum(int total, int wins, double probOfSucc)
            {
                int fails = total - wins;
                
                double probOfFail = 1 - probOfSucc;
                totalPermutations(total, wins);
                successProbExpSuccessNum(probOfSucc, wins);
                failProbExpFailNum(probOfFail, fails);
                return TotalPermutations * SuccessProbExpSuccessNum * FailProbExpFailNum;
            }

            private static void totalPermutations(int gTotal, int gWins)
            {
                int totalOutcomes = Factorial(gTotal);
                int tMinusW = Factorial(gTotal - gWins);
                int Wfact = Factorial(gWins);

                TotalPermutations = totalOutcomes / (tMinusW * Wfact);
            }
            private static void successProbExpSuccessNum(double SuccProb, int GWins)
            {
                SuccessProbExpSuccessNum = (decimal)Math.Pow(SuccProb, GWins);
            }
            private static void failProbExpFailNum(double FailProb, int GFails)
            {
                FailProbExpFailNum = (decimal)Math.Pow(FailProb, GFails);
            }
            private static int Factorial(int anum)
            {
                if (anum <= 1)
                {
                    return 1;
                }
                return anum * Factorial(anum - 1);
            }
        
    }
}
