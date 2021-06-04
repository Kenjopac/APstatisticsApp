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
    /// Interaction logic for ZTestsQuestionSheet.xaml
    /// </summary>
    public partial class ZTestsQuestionSheet : Window
    {
        public ZTestsQuestionSheet()
        {
            InitializeComponent();
            OSAltHypothesisTextBox.Text = "0.5";
            OSNullHypothesisTextBox.Text = "0.5";
        }
        private int SampleAmountNum = 1;
        private int MeanOrPropNum = 1;
        private int ChangeSignNum = 1;
        private bool PooledYN = true;
        private void SampleAmountButton_Click(object sender, RoutedEventArgs e)
        {
            SampleAmountNum++;
            if (SampleAmountNum > 2)
            {
                SampleAmountNum -= 2;
            }

            if (SampleAmountNum == 2)
            {
                SampleAmountButton.Content = "Two";
                TwoSample.Visibility = Visibility.Visible;
                TwoSample.IsEnabled = true;
                OneSample.Visibility = Visibility.Hidden;
                OneSample.IsEnabled = false;
                
            } else
            {
                SampleAmountButton.Content = "One";
                TwoSample.Visibility = Visibility.Hidden;
                TwoSample.IsEnabled = false;
                OneSample.Visibility = Visibility.Visible;
                OneSample.IsEnabled = true;
            }
        }

        private void MeanOrProportionButton_Click(object sender, RoutedEventArgs e)
        {
            MeanOrPropNum++;
            if (MeanOrPropNum > 2)
            {
                MeanOrPropNum -= 2;
            }

            if (MeanOrPropNum == 2)
            {
                MeanOrProportionButton.Content = "mean";
                OSStandDevTextBlock.Visibility = Visibility.Visible;
                OSStandardDevTextBox.Visibility = Visibility.Visible;
                OSStandardDevTextBox.IsEnabled = true;
                TSStandardDevTextBoxOne.Visibility = Visibility.Visible;
                TSStandardDevTextBoxTwo.Visibility = Visibility.Visible;
                TSStandDevTextBlockOne.Visibility = Visibility.Visible;
                TSStandDevTextBlockTwo.Visibility = Visibility.Visible;
                TSStandardDevTextBoxOne.IsEnabled = true;
                TSStandardDevTextBoxTwo.IsEnabled = true;
                OSNullPopProp.Text = "<--- Population Mean";
                OSAltPopProp.Text = "<--- Population Mean";
                TextBlocka.Text = "H_0: mu = ";
                TextBlockb.Text = "H_a: mu";
                TextBlockc.Text = "H_0: mu_1 - mu_2";
                TextBlockd.Text = "H_a: mu_1 - mu_2";
                truepooled.IsEnabled = false;
                falsepooled.IsEnabled = false;
                PlaceholderTextBlock.Opacity = 0.5;
            } else
            {
                MeanOrProportionButton.Content = "proportion";
                OSStandDevTextBlock.Visibility = Visibility.Hidden;
                OSStandardDevTextBox.Visibility = Visibility.Hidden;
                OSStandardDevTextBox.IsEnabled = false;
                TSStandardDevTextBoxOne.Visibility = Visibility.Hidden;
                TSStandardDevTextBoxTwo.Visibility = Visibility.Hidden;
                TSStandDevTextBlockOne.Visibility = Visibility.Hidden;
                TSStandDevTextBlockTwo.Visibility = Visibility.Hidden;
                TSStandardDevTextBoxOne.IsEnabled = false;
                TSStandardDevTextBoxTwo.IsEnabled = false;
                OSNullPopProp.Text = "<--- Population Proportion";
                OSAltPopProp.Text = "<--- Population Proportion";
                TextBlocka.Text = "H_0: p = ";
                TextBlockb.Text = "H_a: p";
                TextBlockc.Text = "H_0: p_1 - p_2";
                TextBlockd.Text = "H_a: p_1 - p_2";
                truepooled.IsEnabled = true;
                falsepooled.IsEnabled = true;
                PlaceholderTextBlock.Opacity = 1;
            }

        }
        private void TSChangeSignButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSignNum++;
            if (ChangeSignNum > 3)
            {
                ChangeSignNum -= 3;
            }
            CheckSign();
        }

        private void OSChangeSignButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSignNum++;
            if (ChangeSignNum > 3)
            {
                ChangeSignNum -= 3;
            }
            CheckSign();
        }
        private void CheckSign()
        {
            switch (ChangeSignNum)
            {
                case 1:
                    TSChangeSignButton.Content = "!=";
                    OSChangeSignButton.Content = "!=";
                    break;
                case 2:
                    TSChangeSignButton.Content = "<";
                    OSChangeSignButton.Content = "<";
                    break;
                case 3:
                    TSChangeSignButton.Content = ">";
                    OSChangeSignButton.Content = ">";
                    break;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow startup = new StartUpWindow();
            startup.Show();
            Close();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (SampleAmountNum == 1)
            {
                OneSampleZTest newtest;
                bool testa = Double.TryParse(OSAltHypothesisTextBox.Text, out double PopParameter);
                bool testb = Double.TryParse(OSSuccessTextBox.Text, out double SuccessNum);
                bool testc = Int32.TryParse(OSSampleSizeTextBox.Text, out int SampleN);
                bool testd = Double.TryParse(OSAlphaTextBox.Text, out double Alpha);
                
                if (MeanOrPropNum == 1)
                {
                    if (testa == true && testb == true && testc == true && testd == true)
                    {
                        newtest = new OneSampleZTest(OSChangeSignButton.Content.ToString(),PopParameter,SuccessNum,SampleN,Alpha);
                        ZTestsAnswerSheet answerSheet = new ZTestsAnswerSheet(newtest);
                        answerSheet.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("input error");
                    }

                }
                else
                {
                    bool teste = Double.TryParse(OSStandardDevTextBox.Text, out double Standarddev);
                    if (testa == true && testb == true && testc == true && testd == true && teste == true)
                    {
                        newtest = new OneSampleZTest(OSChangeSignButton.Content.ToString(), PopParameter, SuccessNum, Standarddev, SampleN, Alpha);
                        ZTestsAnswerSheet answerSheet = new ZTestsAnswerSheet(newtest);
                        answerSheet.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("input error");
                    }
                }
            }
            else
            {
                TwoSampleZTest newtest;
                bool testa = Double.TryParse(TSSuccessTextBoxOne.Text, out double SuccOne);
                bool testb = Double.TryParse(TSSuccessTextBoxTwo.Text, out double SuccTwo);
                bool testc = Int32.TryParse(TSSampleSizeTextBoxOne.Text, out int SampleSizeOne);
                bool testd = Int32.TryParse(TSSampleSizeTextBoxTwo.Text, out int SampleSizeTwo);
                bool teste = Double.TryParse(TSAlphaTextBox.Text, out double Alpha);
                if (MeanOrPropNum == 1)
                {
                    if (testa == true && testb == true && testc == true && testd == true && teste == true)
                    {
                        
                        newtest = new TwoSampleZTest(TSChangeSignButton.Content.ToString(), SuccOne, SuccTwo, SampleSizeOne, SampleSizeTwo, Alpha, PooledYN);
                        ZTestsAnswerSheet answerSheet = new ZTestsAnswerSheet(newtest);
                        answerSheet.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("input error");
                    }
                }
                else
                {
                    bool testf = Double.TryParse(TSStandardDevTextBoxOne.Text, out double SDVone);
                    bool testg = Double.TryParse(TSStandardDevTextBoxTwo.Text, out double SDVtwo);
                    if (testa == true && testb == true && testc == true && testd == true && teste == true && testf == true && testg == true)
                    {
                        newtest = new TwoSampleZTest(TSChangeSignButton.Content.ToString(), SuccOne, SuccTwo, SDVone,SDVtwo,SampleSizeOne, SampleSizeTwo, Alpha);
                        ZTestsAnswerSheet answerSheet = new ZTestsAnswerSheet(newtest);
                        answerSheet.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("input error");
                    }
                }
            }
        }

        private void OSAltHypothesisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OSNullHypothesisTextBox.Text = OSAltHypothesisTextBox.Text;
        }

        private void truepooled_Checked(object sender, RoutedEventArgs e)
        {
            PooledYN = true;
        }

        private void falsepooled_Checked(object sender, RoutedEventArgs e)
        {
            PooledYN = false;
        }
    }
}
