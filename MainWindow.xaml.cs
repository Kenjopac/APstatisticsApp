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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApStatToolsLibrary;

namespace ApStatisticsApplicationUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private static List<string> TESTTYPE = new List<string>() { "Goodness of Fit", "Independence", "Homogeneity" };
        private int placeholder = 0;
        ChiSquareStats NewCalculation;
        public MainWindow()
        {
            InitializeComponent();
            TestType.Text = TESTTYPE[placeholder % 3];
        }
        
        private void NextChiTestButton_Click(object sender, RoutedEventArgs e)
        {
            placeholder = (placeholder + 1) % 3;
            TestType.Text = TESTTYPE[Math.Abs(placeholder)];
        }

        private void PreviousChiTestButton_Click(object sender, RoutedEventArgs e)
        {
            placeholder--;
            TestType.Text = TESTTYPE[Math.Abs(placeholder % 3)];
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder str = new StringBuilder();
            List<double> ObservedList = new List<double>();
            List<double> ExpectedList = new List<double>();
            try
            {
                ObservedList = ObservedTextBox.Text.TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToDouble(sTemp)).ToList();
            } catch
            {
                str.Append("ObservedList input error");
            }
            try {
                ExpectedList = ExpectedTextBox.Text.TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToDouble(sTemp)).ToList();
            } catch
            {
                str.Append("\nExpectedList input error");
            }

            try { 
            NewCalculation = new ChiSquareStats(TestType.Text, NullHypothesisTextBox.Text, AlternativeHypothesisTextBox.Text, Convert.ToDouble(SignificanceLevelTextBox.Text), Convert.ToDouble(DegreesOfFreedomTextBox.Text), ObservedList, ExpectedList);
            } catch (OverflowException)
            {
                str.Append("\nOverFlow exception error");
            }
            catch
            {
                str.Append("\nunexpected exception error");
            }
            if (str.ToString() == "")
            {
                ChiSquareAnswerSheet AnswerWindow = new ChiSquareAnswerSheet(NewCalculation);
                str.Clear();
                AnswerWindow.Show();
                Close();
            } else
            {
                MessageBox.Show(str.ToString());
                str.Clear();
            }
            
            
        }
        private void WorkInProgress_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow newstart = new StartUpWindow();
            
            HelloKittyGif.Visibility = Visibility.Visible;
            HelloKittyGif.BeginInit();           
            BackButton.Visibility = Visibility.Hidden;
            newstart.Show();
            Close();
        }
    }
}
