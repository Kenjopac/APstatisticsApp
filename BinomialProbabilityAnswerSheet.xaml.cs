using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ApStatToolsLibrary;
using ApStatisticsApplicationUI.AllWindows;
namespace ApStatisticsApplicationUI
{
    /// <summary>
    /// Interaction logic for BinomialProbabilityAnswerSheet.xaml
    /// </summary>
    public partial class BinomialProbabilityAnswerSheet : Window
    {
        public BinomialProbabilityAnswerSheet(BinomialProbability newAnswerKey)
        {
            InitializeComponent();
            TotalTrialsDisplay.Text = $"{newAnswerKey.Totalnum} total trials";
            TotalSuccessDisplay.Text = $"We want {newAnswerKey.NumberOfSuccesses} successful trials";
            TotalFailsDisplay.Text = $"with exactly {newAnswerKey.FailNum} failures";
            SuccessProbabilityDisplay.Text = $"With a {newAnswerKey.ProbabilityOfSuccess} probability of success";
            FailProbabilityDisplay.Text = $"and a {newAnswerKey.FailProbability} probability of failing";
            EqualsToTextBlock.Text = $"P(x = {newAnswerKey.NumberOfSuccesses}) = C({newAnswerKey.Totalnum} , {newAnswerKey.NumberOfSuccesses})*({newAnswerKey.ProbabilityOfSuccess}^{newAnswerKey.NumberOfSuccesses})*({newAnswerKey.FailProbability}^{newAnswerKey.FailNum}) = {newAnswerKey.ProbabilityOfEqualToSuccess}";
            GreaterThanTextBlock.Text = GreaterThanText(newAnswerKey);
            GreaterThanEqualToTextBlock.Text = GreaterThanOrEqualToTextwriter(newAnswerKey);
            LessThanEqualsToTextBlock.Text = lessThanOrEqualToTextwriter(newAnswerKey);
            LessThanTextBlock.Text = lessThanTextwriter(newAnswerKey);
        }
        private static string GreaterThanText(BinomialProbability newAnswerKey)
        {
            StringBuilder newstr = new StringBuilder();
            newstr.Append($"P(x > {newAnswerKey.NumberOfSuccesses}) = ");
            int f = newAnswerKey.FailNum;
            for (int i = newAnswerKey.NumberOfSuccesses + 1; i <= newAnswerKey.Totalnum; i++)
            {
                f--;
                newstr.Append($" C({newAnswerKey.Totalnum} , {i})*({newAnswerKey.ProbabilityOfSuccess}^{i})*({newAnswerKey.FailProbability}^{f}) +");                
            }
            newstr.Remove(newstr.Length - 1, 1);
            newstr.Append($"= {newAnswerKey.ProbabilityOfGreaterThanSuccess}");
            return newstr.ToString();
        }
        private static string GreaterThanOrEqualToTextwriter(BinomialProbability newAnswerKey)
        {
            StringBuilder newstr = new StringBuilder();
            newstr.Append($"P(x >= {newAnswerKey.NumberOfSuccesses}) = ");
            int f = newAnswerKey.FailNum;
            for (int i = newAnswerKey.NumberOfSuccesses; i <= newAnswerKey.Totalnum; i++)
            {
                
                newstr.Append($" C({newAnswerKey.Totalnum} , {i})*({newAnswerKey.ProbabilityOfSuccess}^{i})*({newAnswerKey.FailProbability}^{f}) +");
                f--;
            }
            newstr.Remove(newstr.Length - 1, 1);
            newstr.Append($"= {newAnswerKey.ProbabilityOfGreaterEqualToSuccess}");
            return newstr.ToString();
        }
        private static string lessThanOrEqualToTextwriter(BinomialProbability newAnswerKey)
        {
            StringBuilder newstr = new StringBuilder();
            newstr.Append($"P(x <= {newAnswerKey.NumberOfSuccesses}) = ");
            int f = newAnswerKey.Totalnum;
            for (int i = 0; i <= newAnswerKey.NumberOfSuccesses; i++)
            {
                f -= i;
                newstr.Append($" C({newAnswerKey.Totalnum} , {i})*({newAnswerKey.ProbabilityOfSuccess}^{i})*({newAnswerKey.FailProbability}^{f}) +");
            }
            newstr.Remove(newstr.Length - 1, 1);
            newstr.Append($"= {newAnswerKey.ProbabilityOfLessEqualToSuccess}");
            return newstr.ToString();
        }
        private static string lessThanTextwriter(BinomialProbability newAnswerKey)
        {
            StringBuilder newstr = new StringBuilder();
            newstr.Append($"P(x < {newAnswerKey.NumberOfSuccesses}) = ");
            int f = newAnswerKey.Totalnum;
            for (int i = 0; i < newAnswerKey.NumberOfSuccesses; i++)
            {
                f -= i;
                newstr.Append($" C({newAnswerKey.Totalnum} , {i})*({newAnswerKey.ProbabilityOfSuccess}^{i})*({newAnswerKey.FailProbability}^{f}) +");
            }
            newstr.Remove(newstr.Length - 1, 1);
            newstr.Append($"= {newAnswerKey.ProbabilityOfLessThanSuccess}");
            return newstr.ToString();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BinomialProbabilityQuestionSheet newqSheet = new BinomialProbabilityQuestionSheet();
            newqSheet.Show();
            Close();
        }
    }
}
