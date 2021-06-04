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
namespace ApStatisticsApplicationUI.AllWindows
{
    /// <summary>
    /// Interaction logic for BinomialProbabilityQuestionSheet.xaml
    /// </summary>
    public partial class BinomialProbabilityQuestionSheet : Window
    {
        public BinomialProbabilityQuestionSheet()
        {
            InitializeComponent();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder newstr = new StringBuilder();
            newstr.Append("");
            try {
                Convert.ToInt32(TotalTrialBox.Text);
            } catch
            {
                newstr.Append("Total Trial Box must have integer value");
            }
            try
            {
                Convert.ToInt32(SuccessfulTrialBox.Text);
            }
            catch
            {
                newstr.Append("\nSuccessful Trial Box must have integer value");
            }
            try
            {
                Convert.ToDouble(SuccessfulProbabilityBox.Text);
            }
            catch
            {
                newstr.Append("\nSuccessful Probability box must have a double value");
            }
            if (newstr.Length == 0)
            {
                BinomialProbability newBinomValues = new BinomialProbability(Convert.ToInt32(TotalTrialBox.Text), Convert.ToInt32(SuccessfulTrialBox.Text), Convert.ToDouble(SuccessfulProbabilityBox.Text));
                BinomialProbabilityAnswerSheet newanswer = new BinomialProbabilityAnswerSheet(newBinomValues);
                newanswer.Show();
                Close();
                
            } else
            {
                MessageBox.Show(newstr.ToString());
                newstr.Clear();
            }
           
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow newstart = new StartUpWindow();
            newstart.Show();
            Close();
            
        }
    }
}
