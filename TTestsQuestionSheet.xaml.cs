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
    /// Interaction logic for TTestsQuestionSheet.xaml
    /// </summary>
    public partial class TTestsQuestionSheet : Window
    {
        public TTestsQuestionSheet()
        {
            InitializeComponent();
        }
        private int SampleAmountNum = 1;
       
        private int ChangeSignNum = 1;
        private bool ListUsedOrNot = false;
        private void numberOfSampleButtons_Click(object sender, RoutedEventArgs e)
        {
            SampleAmountNum++;
            if (SampleAmountNum > 2)
            {
                SampleAmountNum -= 2;
            }

            if (SampleAmountNum == 2)
            {
                numberOfSampleButtons.Content = "Two";
                TwoSample.Visibility = Visibility.Visible;
                TwoSample.IsEnabled = true;
                OneSample.Visibility = Visibility.Hidden;
                OneSample.IsEnabled = false;

            }
            else if(SampleAmountNum == 1)
            {
                numberOfSampleButtons.Content = "One";
                TwoSample.Visibility = Visibility.Hidden;
                TwoSample.IsEnabled = false;
                OneSample.Visibility = Visibility.Visible;
                OneSample.IsEnabled = true;
            } 
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
        private void TSChangeSignButton_Click(object sender, RoutedEventArgs e)
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
                    TVChangeSignButton.Content = "!=";
                    OVChangeSignButton.Content = "!=";
                    break;
                case 2:
                    TSChangeSignButton.Content = "<";
                    OSChangeSignButton.Content = "<";
                    TVChangeSignButton.Content = "<";
                    OVChangeSignButton.Content = "<";
                    break;
                case 3:
                    TSChangeSignButton.Content = ">";
                    OSChangeSignButton.Content = ">";
                    TVChangeSignButton.Content = ">";
                    OVChangeSignButton.Content = ">";
                    break;
            }
        }

        private void OSAltHypothesisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OSNullHypothesisTextBox.Text = OSAltHypothesisTextBox.Text;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow Star = new StartUpWindow();
            Star.Show();
            Close();
        }
        private void ListRadio_Click(object sender, RoutedEventArgs e)
        {
            OneSampleListGrid.Visibility = Visibility.Visible;
            OneSampleListGrid.IsEnabled = true;
            OneSampleValuesGrid.Visibility = Visibility.Hidden;
            OneSampleValuesGrid.IsEnabled = false;
            TwoSampleListGrid.Visibility = Visibility.Visible;
            TwoSampleListGrid.IsEnabled = true;
            TwoSampleValuesGrid.Visibility = Visibility.Hidden;
            TwoSampleValuesGrid.IsEnabled = false;
            ListUsedOrNot = true;

        }
        private void ValuesRadio_Click(object sender, RoutedEventArgs e)
        {
            OneSampleListGrid.Visibility = Visibility.Hidden;
            OneSampleListGrid.IsEnabled = false;
            OneSampleValuesGrid.Visibility = Visibility.Visible;
            OneSampleValuesGrid.IsEnabled = true;
            TwoSampleListGrid.Visibility = Visibility.Hidden;
            TwoSampleListGrid.IsEnabled = false;
            TwoSampleValuesGrid.Visibility = Visibility.Visible;
            TwoSampleValuesGrid.IsEnabled = true;
            ListUsedOrNot = false;
        }

    private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            TTestsAnswerSheet answerSheet;
            if (ListUsedOrNot == true)
            {
                if (SampleAmountNum == 1)
                {
                    OnePropTTest newtest;
                    try { 
                    List<double> sample = OVValuesList.Text.Trim().Split(' ').ToList().ConvertAll(x => Convert.ToDouble(x));
                        bool testa = Double.TryParse(OVAlphaTextBox.Text, out double Alpha);
                        bool testb = Double.TryParse(OVAltHypothesisTextBox.Text, out double ExpectedMean);
                        if (testa == true && testb == true)
                        {
                            newtest = new OnePropTTest(ExpectedMean, sample, Alpha, OSChangeSignButton.Content.ToString());
                            answerSheet = new TTestsAnswerSheet(newtest);
                            answerSheet.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("list is correct, Alpha or null mean wrong");
                        }
                        
                    }
                    catch 
                    {
                        MessageBox.Show("make sure each number \nonly has one space in between");
                    }

                }
                else
                {
                    TwoPropTTest newtest;
                    try
                    {
                        List<double> sampleone = TVValuesListOne.Text.Trim().Split(' ').ToList().ConvertAll(x => Convert.ToDouble(x));
                        List<double> sampletwo = TVValuesListTwo.Text.Trim().Split(' ').ToList().ConvertAll(x => Convert.ToDouble(x));
                        bool testa = Double.TryParse(TVAlphaTextBox.Text, out double Alpha);
                        if (testa == true)
                        {
                            newtest = new TwoPropTTest(sampleone, sampletwo, Alpha, OSChangeSignButton.Content.ToString());
                            answerSheet = new TTestsAnswerSheet(newtest);
                            answerSheet.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("list is correct, Alpha is wrong");
                        }                       

                    }
                    catch
                    {
                        MessageBox.Show("make sure each number \nonly has one space in between");
                    }
                }
            }
            if (SampleAmountNum == 1)
            {
                OnePropTTest newtest;
                bool testa = Double.TryParse(OSAltHypothesisTextBox.Text, out double PopParameter);
                bool testb = Double.TryParse(OSSuccessTextBox.Text, out double SuccessNum);
                bool testc = Int32.TryParse(OSSampleSizeTextBox.Text, out int SampleN);
                bool testd = Double.TryParse(OSAlphaTextBox.Text, out double Alpha);
                bool teste = Double.TryParse(OSStandardDevTextBox.Text, out double StandardDev);
               
               if (testa == true && testb == true && testc == true && testd == true && teste == true)
               {
                    newtest = new OnePropTTest(PopParameter,SuccessNum,StandardDev,SampleN,Alpha,OSChangeSignButton.Content.ToString());
                    answerSheet = new TTestsAnswerSheet(newtest);
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
                TwoPropTTest newtest;
                bool testa = Double.TryParse(TSSuccessTextBoxOne.Text, out double SuccOne);
                bool testb = Double.TryParse(TSSuccessTextBoxTwo.Text, out double SuccTwo);
                bool testc = Int32.TryParse(TSSampleSizeTextBoxOne.Text, out int SampleSizeOne);
                bool testd = Int32.TryParse(TSSampleSizeTextBoxTwo.Text, out int SampleSizeTwo);
                bool teste = Double.TryParse(TSAlphaTextBox.Text, out double Alpha);
                bool testf = Double.TryParse(TSStandardDevTextBoxOne.Text, out double SDVone);
                bool testg = Double.TryParse(TSStandardDevTextBoxTwo.Text, out double SDVtwo);
                
                
                if (testa == true && testb == true && testc == true && testd == true && teste == true && testf == true && testg == true)
                {

                    newtest = new TwoPropTTest(0,SuccOne,SDVone,SuccTwo,SDVtwo,SampleSizeTwo,SampleSizeOne,Alpha, OSChangeSignButton.Content.ToString());
                    answerSheet = new TTestsAnswerSheet(newtest);
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
}
