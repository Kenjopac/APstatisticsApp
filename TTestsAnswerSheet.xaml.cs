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
    /// Interaction logic for TTestsAnswerSheet.xaml
    /// </summary>
    public partial class TTestsAnswerSheet : Window
    {
        public TTestsAnswerSheet(OnePropTTest AnswerSheet)
        {
            InitializeComponent();
            WorkTextDisplay.Text = $"{AnswerSheet.work}\n{AnswerSheet.Conclusion}";
            DataDisplay.Text = $"{AnswerSheet.data}";
            TitleDisplay.Text = "One sample T test for Population Mean";
            if (AnswerSheet.MatchedPairs == true)
            {
                TitleDisplay.Text = "Paired Difference T test";
            }
        }
        public TTestsAnswerSheet(TwoPropTTest AnswerSheet)
        {
            InitializeComponent();
            
            WorkTextDisplay.Text = $"{AnswerSheet.work}\n{AnswerSheet.Conclusion}";
            DataDisplay.Text = $"{AnswerSheet.data}";
            TitleDisplay.Text = "Two sample T test for Population Mean";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TTestsQuestionSheet newsheet = new TTestsQuestionSheet();
            newsheet.Show();
            Close();
        }
    }
}
