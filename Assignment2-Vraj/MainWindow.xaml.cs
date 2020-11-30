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

namespace Assignment2_Vraj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        //assign the first pic and write "normal temp" in status lable
        private void Initialize()
        {
            string imagePath = "/Images/healthy.png";
            imgResult.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            lblStatus.Content = "Normal Temp";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //turn text from the textbox into string
            string height_in_feet = txtHeightFeet.Text;
            string height_in_inch = txtHeightInches.Text;

            //initialize feet and inches to 0 and set background to white for those textbox
            double feet_num = 0, inch_num = 0;
            txtHeightFeet.Background = new SolidColorBrush(Colors.White);
            txtHeightInches.Background = new SolidColorBrush(Colors.White);
          
            //not successful so we use not (!) operator 
            //check to see if the text in textbox is valid 
            if (!double.TryParse(height_in_feet, out feet_num))
            {
                MessageBox.Show("First value is not a valid number");
                txtHeightFeet.Background = new SolidColorBrush(Colors.Red);
                return;
            }

            //check to see if the text in textbox is valid 
            if (!double.TryParse(height_in_inch, out inch_num))
            {
                MessageBox.Show("Second value is not a valid number");
                txtHeightInches.Background = new SolidColorBrush(Colors.Red);
                return;
            }

            //check to see if the height is < 0 inches in total. If not change textbox color to red
            if (feet_num + inch_num <= 0)
            {
                MessageBox.Show("Height total must be greater than 0 inches.");
                txtHeightFeet.Background = new SolidColorBrush(Colors.Red);
                txtHeightInches.Background = new SolidColorBrush(Colors.Red);
                return;
            }

            //change content in status lable if temp is less than 97
            if(sldTemp.Value < 97)
            {
                lblStatus.Content = "Low Temp";
            }

            //change content in status lable if temp is >= 97 and < 99
            if ((sldTemp.Value >= 97) && (sldTemp.Value < 99))
            {
                lblStatus.Content = "Normal Temp";
            }

            //change content in status lable if temp is >= 99 and < 100
            if ((sldTemp.Value >= 99) && (sldTemp.Value < 100))
            {
                lblStatus.Content = "Above Normal Temp";
            }

            //change content in status lable if temp is more than 101
            if (sldTemp.Value >= 101)
            {
                lblStatus.Content = "High Temp";
            }

            //if weight = 0 based on slider position then send out a warning message and chage weight label color to red
            /*if( sldWeight.Value == 0)
            {
                MessageBox.Show("Invalid Temprature. Try again.");
                lblWeightNum.Background = new SolidColorBrush(Colors.Red);
            }*/

            //converting feet --> inches and adding all inches up into a new variable 
            double new_height = (feet_num * 12) + inch_num;
            double bmi = ((sldWeight.Value / (new_height * new_height)) * 703);

            //change BMI pic if BMI is < 18.5 and formate BMI to output with only 2 decimal places
            if (bmi < 18.5)
            {
                lblBmi.Content = ($"BMI:{bmi: 0.00}");
                imgResult.Source = new BitmapImage(new Uri("/Images/Underweight.png", UriKind.Relative));
            }

            //change BMI pic if BMI is > 18.5 and <= 24.9. Formate BMI to output with only 2 decimal places
            if ((bmi > 18.5) && (bmi <= 24.9))
            {
                //normal
                lblBmi.Content = ($"BMI:{bmi: 0.00}");
                imgResult.Source = new BitmapImage(new Uri("/Images/healthy.png", UriKind.Relative));
            }

            //change BMI pic if BMI is >= 25 and <= 29.9. Formate BMI to output with only 2 decimal places
            if ((bmi >= 25) && (bmi <= 29.9))
            {
                //overweight 
                lblBmi.Content = ($"BMI:{bmi: 0.00}");
                imgResult.Source = new BitmapImage(new Uri("/Images/overweight.png", UriKind.Relative));
            }

            //change BMI pic if BMI is >= 30 and formate BMI to output with only 2 decimal places
            if (bmi >= 30)
            {
                //obese 
                lblBmi.Content = ($"BMI:{bmi: 0.00}");
                imgResult.Source = new BitmapImage(new Uri("/Images/obese.png", UriKind.Relative));
            }
        }

        private void txtHeightInches_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //change the temp label next to slider based on slider position
        private void temp_slider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblTempNum != null)
            {
                lblTempNum.Content = sldTemp.Value;
            }
        }

        //change the weight label next to slider based on slider position
        private void weight_slider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblWeightNum != null)
            {
                lblWeightNum.Content = sldWeight.Value;
            }
        }
    }
}
