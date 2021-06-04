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
    /// Interaction logic for GeometricProbabilityQuestionSheet.xaml
    /// </summary>
    public partial class GeometricProbabilityQuestionSheet : Window
    {
        public GeometricProbabilityQuestionSheet()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartUpWindow newstart = new StartUpWindow();
            newstart.Show();
            Close();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            bool testa = Int32.TryParse(NumberOfInterest.Text, out int num);
            bool testb = Double.TryParse(Probability.Text, out double prop);
            if (testa == true && testb == true)
            {
                GeometricProbability newprop = new GeometricProbability(num, prop);
                GeometricProbabilityAnswerSheet newsheet = new GeometricProbabilityAnswerSheet(newprop);
                newsheet.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Number must be integer \nprob must be a double");
            }
        }
    }
}
