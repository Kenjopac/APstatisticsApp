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
    /// Interaction logic for TIntervalQuestionSheet.xaml
    /// </summary>
    public partial class TIntervalQuestionSheet : Window
    {
        private int changebuttonNum = 1;
        private int SumOrdifferenceB = 1;
        private string SendText = "plus";
        public TIntervalQuestionSheet()
        {
            InitializeComponent();
        }
        private void ChangeOneOrTwo_Click(object sender, RoutedEventArgs e)
        {
            changebuttonNum++;
            if (changebuttonNum > 2)
            {
                changebuttonNum -= 2;
            }

            if (changebuttonNum == 2)
            {
                sumordifferenceButton.IsEnabled = true;
                SecondSampleMeanBox.IsEnabled = true;
                SecondSampleSizeBox.IsEnabled = true;
                SecondStandardDevBox.IsEnabled = true;
                ChangeOneOrTwo.Content = "Two";
            }
            else
            {
                sumordifferenceButton.IsEnabled = false;
                SecondSampleMeanBox.IsEnabled = false;
                SecondSampleSizeBox.IsEnabled = false;
                SecondStandardDevBox.IsEnabled = false;
                ChangeOneOrTwo.Content = "One";
            }

        }

        private void sumordifferenceButton_Click(object sender, RoutedEventArgs e)
        {
            SumOrdifferenceB++;
            if (SumOrdifferenceB > 2)
            {
                SumOrdifferenceB -= 2;
            }
            if (SumOrdifferenceB == 2)
            {
                SendText = "minus";
                sumordifferenceButton.Content = "Difference";
            }
            else
            {
                SendText = "plus";
                sumordifferenceButton.Content = "Sum";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow newstart = new StartUpWindow();
            newstart.Show();
            Close();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            bool SS = Int32.TryParse(SampleSizeBox.Text, out int SampleSize);
            bool SP = Double.TryParse(SampleMeanBox.Text, out double SampleProp);
            bool CL = Double.TryParse(ConfidenceLevelBox.Text, out double ConLev);
            bool SD = Double.TryParse(SampleStandardDevBox.Text, out double StandardDev);
            if (changebuttonNum == 1)
            {
                if (SS == true && SP == true && CL == true && SD == true)
                {
                    if (0 <= ConLev && ConLev <= 1)
                    {
                        OnePropTInterval answersheet = new OnePropTInterval(SampleSize, SampleProp,StandardDev,  ConLev);
                        TIntervalAnswerSheet newanswers = new TIntervalAnswerSheet(answersheet);
                        newanswers.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Confidence Level bad input");
                    }
                }
                else
                {
                    MessageBox.Show("wrong format");
                }
            }
            else
            {
                bool SStwo = Int32.TryParse(SecondSampleSizeBox.Text, out int SampleSizetwo);
                bool SPtwo = Double.TryParse(SecondSampleMeanBox.Text, out double SampleProptwo);
                bool SDtwo = Double.TryParse(SecondStandardDevBox.Text, out double StandardDevtwo);
                if (SS == true && SP == true && CL == true && SStwo == true && SPtwo == true && SDtwo == true)
                {
                    if (0 <= ConLev && ConLev <= 1)
                    {
                        TwoPropTInterval answersheet = new TwoPropTInterval(SampleSize, SampleProp,StandardDev, ConLev, SampleSizetwo, SampleProptwo,StandardDevtwo, SendText);
                        TIntervalAnswerSheet newanswers = new TIntervalAnswerSheet(answersheet);
                        newanswers.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("wrong format");
                }

            }
        }
    }
}
