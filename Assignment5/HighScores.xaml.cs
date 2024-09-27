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

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for HighScores.xaml
    /// </summary>
    public partial class HighScores : Window
    {

        public HighScores()
        {
            InitializeComponent();
        }

        /// <summary>
        /// closing high scores screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCloseHighScores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception)
            { 
                ErrorLabel.Visibility = Visibility.Visible;
            }
            
        }

        /// <summary>
        /// closing window with x button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }

            catch (Exception)
            {
                ErrorLabel.Visibility = Visibility.Visible;
            }


        }

        /// <summary>
        /// Use User class to display Name/Age/#Correct/Seconds
        /// </summary>
        public void DisplayUsersScore()
        {
            try
            {
                ScoresTextbox.AppendText("\n\n" +
                            User.Name + "\t      " + User.Age + "\t\t" + User.iNumberCorrect + "\t\t" + ((Game.iCurrentGameQuestion - 1) - User.iNumberCorrect) + "\t\t" + User.iGameSeconds + "s"
                            );
            }

            catch (Exception )
            {
                ErrorLabel.Visibility= Visibility.Visible;
                ErrorLabel.Content = "Could not display user score!";
            }
            
        }
    }
}

