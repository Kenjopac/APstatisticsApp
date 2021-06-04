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
namespace ApStatisticsApplicationUI
{
    /// <summary>
    /// Interaction logic for TIntervalAnswerSheet.xaml
    /// </summary>
    public partial class TIntervalAnswerSheet : Window
    {
        public TIntervalAnswerSheet(OnePropTInterval answerSheet)
        {
            InitializeComponent();
            InitializeComponent();
            TitleBox.Text = $"One Sample T interval for population proportion";
            ConditionsBox.Text = $"\n -The data is a simple random sample from the population of interest\n -{answerSheet.SampleSize} < 10% of population of interest for independence\n -Approximately normal, size({answerSheet.SampleSize}) > 30 or we're told normality";
            SampleSizeBox.Text = $"Sample Size: {answerSheet.SampleSize}";
            SampleMeanBox.Text = $"Sample Mean: {answerSheet.SampleMean}";
            SignificanceLevelBox.Text = $"Confidence Level: {answerSheet.SignificanceLevel}";
            TvalueBox.Text = $"T value: {answerSheet.TValue}";
            ConfidenceIntervalBox.Text = $"Statistic +/- (critical value)(standard error of statistic) = {answerSheet.SampleMean} +/- ({answerSheet.TValue}) * ({answerSheet.StandardDeviation} / Sqrt({answerSheet.SampleSize})) \nConfidence Interval: ({answerSheet.MinVal} , {answerSheet.MaxVal})";
            ConclusionBox.Text = $"Conclusion:\n{answerSheet.Conclusion}";
        }
        public TIntervalAnswerSheet(TwoPropTInterval answerSheet)
        {
            InitializeComponent();
            TitleBox.Text = $"Two Sample T interval for {answerSheet.DifforAdd} between population proportions";
            ConditionsBox.Text = $"\n -The data are simple random samples from the populations of interest\n -{answerSheet.SampleSize} < 10% of population of interest \n -{answerSheet.SecondSampleSize} < 10% of population of interest \n -Approximately normal, sizes: ({answerSheet.SampleSize} and {answerSheet.SecondSampleSize}) > 30 or we're told normality ";
            SampleSizeBox.Text = $"First Sample Size: {answerSheet.SampleSize}\nSecond Sample Size: {answerSheet.SecondSampleSize}";
            SampleMeanBox.Text = $"First Sample Mean: {answerSheet.SampleMean}\nSecond Sample Mean {answerSheet.SecondSampleMean}";
            SignificanceLevelBox.Text = $"Confidence Level: {answerSheet.SignificanceLevel}";
            TvalueBox.Text = $"T value: {answerSheet.TValue}";
            ConfidenceIntervalBox.Text = $" Statistic +/- (critical value)(standard error of statistic) = {answerSheet.DAStatistic} +/- ({answerSheet.TValue}) * Sqrt({answerSheet.StandardDeviation}^2 / {answerSheet.SampleSize} + {answerSheet.SecondStandardDeviation}^2 /{answerSheet.SecondSampleSize})\n Confidence Interval: ({answerSheet.MinVal} , {answerSheet.MaxVal})";
            ConclusionBox.Text = $"Conclusion:\n{answerSheet.Conclusion}";
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TIntervalQuestionSheet newz = new TIntervalQuestionSheet();
            newz.Show();
            Close();
        }
    }
}
