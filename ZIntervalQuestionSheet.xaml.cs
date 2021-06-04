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
    /// Interaction logic for ZIntervalQuestionSheet.xaml
    /// </summary>
    public partial class ZIntervalQuestionSheet : Window
    {
        private int changebuttonNum = 1;
        private int SumOrdifferenceB = 1;
        private string SendText = "plus";
        public ZIntervalQuestionSheet()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow newwindows = new StartUpWindow();
            newwindows.Show();
            Close();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            bool SS = Int32.TryParse(SampleSizeBox.Text, out int SampleSize);
            bool SP = Double.TryParse(SampleProportionBox.Text, out double SampleProp);
            bool CL = Double.TryParse(ConfidenceLevelBox.Text, out double ConLev);

            if (changebuttonNum == 1)
            {
                if (SS == true && SP == true && CL == true)
                {
                    if (0 <= ConLev && ConLev <= 1)
                    {
                        OnePropZInterval answersheet = new OnePropZInterval(SampleSize, SampleProp, ConLev);
                        ZIntervalAnswerSheet newanswers = new ZIntervalAnswerSheet(answersheet);
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
                bool SPtwo = Double.TryParse(SecondSampleProportionBox.Text, out double SampleProptwo);
                if (SS == true && SP == true && CL == true && SStwo == true && SPtwo == true)
                {
                    if (0 <= ConLev && ConLev <= 1)
                    {
                        TwoPropZInterval answersheet = new TwoPropZInterval(SampleSize,SampleProp,ConLev,SampleSizetwo,SampleProptwo,SendText);
                        ZIntervalAnswerSheet newanswers = new ZIntervalAnswerSheet(answersheet);
                        newanswers.Show();
                        Close();
                    }
                } else
                {
                    MessageBox.Show("wrong format");
                }
                
            }
            
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
                SecondSampleProportionBox.IsEnabled = true;
                SecondSampleSizeBox.IsEnabled = true;
                ChangeOneOrTwo.Content = "Two";
            }
            else
            {
                sumordifferenceButton.IsEnabled = false;
                SecondSampleProportionBox.IsEnabled = false;
                SecondSampleSizeBox.IsEnabled = false;
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
    }
}
