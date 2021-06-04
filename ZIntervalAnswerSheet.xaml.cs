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
    /// Interaction logic for ZIntervalAnswerSheet.xaml
    /// </summary>
    public partial class ZIntervalAnswerSheet : Window
    {
        public ZIntervalAnswerSheet(OnePropZInterval answerSheet)
        {
            InitializeComponent();
            TitleBox.Text = $"One Sample Z interval for population proportion";
            ConditionsBox.Text = $"\n -The data is a simple random sample from the population of interest\n -{answerSheet.SampleSize} < 10% of population of interest \n -{answerSheet.SampleSize} * {answerSheet.SampleProportion} ≥ 10 and {answerSheet.SampleSize} * (1 − {answerSheet.SampleProportion}) ≥ 10 ";
            SampleSizeBox.Text = $"Sample Size: {answerSheet.SampleSize}";
            SampleProportionBox.Text = $"Sample Proportion: {answerSheet.SampleProportion}";
            SignificanceLevelBox.Text = $"Confidence Level: {answerSheet.SignificanceLevel}";
            ZvalueBox.Text = $"Z value: {answerSheet.ZValue}";
            ConfidenceIntervalBox.Text = $"Statistic +- (critical value)(standard error of statistic) = {answerSheet.SampleProportion} +- ({answerSheet.ZValue}) * Sqrt(({answerSheet.SampleProportion})(1 - {answerSheet.SampleProportion}) / {answerSheet.SampleSize}) \nConfidence Interval: ({answerSheet.MinVal} , {answerSheet.MaxVal})";
            ConclusionBox.Text = $"Conclusion:\n{answerSheet.Conclusion}";

        }
        public ZIntervalAnswerSheet(TwoPropZInterval answerSheet)
        {
            InitializeComponent();
            TitleBox.Text = $"Two Sample Z interval for {answerSheet.DifforAdd} between population proportions";
            ConditionsBox.Text = $"\n -The data are simple random samples from the populations of interest\n -{answerSheet.SampleSize} < 10% of population of interest \n -{answerSheet.SecondSampleSize} < 10% of population of interest \n -{answerSheet.SampleSize} * {answerSheet.SampleProportion} ≥ 10 and {answerSheet.SampleSize} * (1 − {answerSheet.SampleProportion}) ≥ 10 \n -{answerSheet.SecondSampleSize} * {answerSheet.SecondSampleProportion} ≥ 10 and {answerSheet.SecondSampleSize} * (1 − {answerSheet.SecondSampleProportion}) ≥ 10 ";
            SampleSizeBox.Text = $"First Sample Size: {answerSheet.SampleSize}\nSecond Sample Size: {answerSheet.SecondSampleSize}";
            SampleProportionBox.Text = $"First Sample Proportion: {answerSheet.SampleProportion}\nSecond Sample Proportion {answerSheet.SecondSampleProportion}";
            SignificanceLevelBox.Text = $"Confidence Level: {answerSheet.SignificanceLevel}";
            ZvalueBox.Text = $"Z value: {answerSheet.ZValue}";
            ConfidenceIntervalBox.Text = $" Statistic +- (critical value)(standard error of statistic) = {answerSheet.DAStatistic} +/- ({answerSheet.ZValue}) * Sqrt( ({answerSheet.SampleProportion})(1 - {answerSheet.SampleProportion}) / {answerSheet.SampleSize}  +  ({answerSheet.SecondSampleProportion})(1 - {answerSheet.SecondSampleProportion}) / {answerSheet.SecondSampleSize}) \nConfidence Interval: ({answerSheet.MinVal} , {answerSheet.MaxVal})";
            ConclusionBox.Text = $"Conclusion:\n{answerSheet.Conclusion}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ZIntervalQuestionSheet newz = new ZIntervalQuestionSheet();
            newz.Show();
            Close();
        }
    }
}
