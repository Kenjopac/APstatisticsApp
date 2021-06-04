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
    /// Interaction logic for ZTestsAnswerSheet.xaml
    /// </summary>
    public partial class ZTestsAnswerSheet : Window
    {
        public ZTestsAnswerSheet(OneSampleZTest AnswerSheet)
        {
            InitializeComponent();
            WorkTextDisplay.Text = $"{AnswerSheet.Work}\n{AnswerSheet.Conclusion}";
            if (AnswerSheet.MeanOrnoMean == true)
            {
                DataDisplay.Text = $"Xbar: {AnswerSheet.ObservedValue}\nNull Value: {AnswerSheet.PopulationParameter}\nSample Size: {AnswerSheet.SampleSize} \nStandard Dev: {AnswerSheet.StandardDeviation} \nAlpha: {AnswerSheet.Alpha} \nP-Value: {AnswerSheet.Pvalue}";
                
            }
            else
            {
                DataDisplay.Text = $"Sample Prop: {AnswerSheet.ObservedValue}\nNull Value:{AnswerSheet.PopulationParameter}\nSample Size: {AnswerSheet.SampleSize}\nAlpha: {AnswerSheet.Alpha}";
            }
        }
        public ZTestsAnswerSheet(TwoSampleZTest AnswerSheet)
        {
            InitializeComponent();
            WorkTextDisplay.Text = $"{AnswerSheet.Work}\n{AnswerSheet.Conclusion}";
            if (AnswerSheet.MeanOrnoMean == true)
            {
                DataDisplay.Text = $"Xbar1: {AnswerSheet.xOne}\nXbar2: {AnswerSheet.xTwo} \nSample Size1: {AnswerSheet.SampleSizeOne}\nSample Size2: {AnswerSheet.SampleSizeTwo}\nStandard Dev1: {AnswerSheet.SigmaOne}\n Standard Dev2: {AnswerSheet.SigmaTwo}\nAlpha: {AnswerSheet.Alpha} \nP-Value: {AnswerSheet.Pvalue}";
            }
            else
            {
                DataDisplay.Text = $"prop1: {AnswerSheet.ObservedPropOne}\nprop2: {AnswerSheet.ObservedPropTwo} \nSample Size1: {AnswerSheet.SampleSizeOne}\nSample Size2: {AnswerSheet.SampleSizeTwo}\nAlpha: {AnswerSheet.Alpha} \nP-Value: {AnswerSheet.Pvalue}";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ZTestsQuestionSheet newz = new ZTestsQuestionSheet();
            newz.Show();
            Close();
        }

        private void BackButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
