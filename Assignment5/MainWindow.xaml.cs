using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for Main Window
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Class that holds the high scores.
        /// </summary>
        HighScores HighScoresForm;

        /// <summary>
        /// Class where the game is played.
        /// </summary>
        Game GameForm;

        public MainWindow()
        {
            InitializeComponent();

            //MAKE SURE TO INCLUDE THIS LINE OR THE APPLICATION WILL NOT CLOSE
            //BECAUSE THE WINDOWS ARE STILL IN MEMORY
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;///////////////////////////////////////////////////////////

            HighScoresForm = new HighScores();
            GameForm = new Game();

            //Pass the high scores form to the game form.  This way the high scores form may be displayed via the game form.
            GameForm.CopyHighScores = HighScoresForm;
        }

        /// <summary>
        /// Validate User's name and age
        /// Set user's info using the static variables in the class User
        /// Set game type using the static variable in the class clsGame
        /// If valid show game window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlayGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(AgeTextbox.Text) < 3 || Convert.ToInt32(AgeTextbox.Text) > 10)
                {
                    throw new ArithmeticException();
                }

                if (NameTextbox.Text == "")
                {
                    throw new ArithmeticException();
                }

                // applying checked value to game type
                if (AddRadioButton.IsChecked == true)
                {
                    clsGame.egametype = clsGame.GameType.Add;
                }

                if (SubtractRadioButton.IsChecked == true)
                {
                    clsGame.egametype = clsGame.GameType.Subtract;
                }

                if (MultRadioButton.IsChecked == true)
                {
                    clsGame.egametype = clsGame.GameType.Mult;
                }

                if (DivideRadioButton.IsChecked == true)
                {
                    clsGame.egametype = clsGame.GameType.Divide;
                }

                // applying user input to static user values, clear error label if errors were displayed
                ErrorLabel.Content = "";
                User.Age = Convert.ToInt32(AgeTextbox.Text);
                User.Name = NameTextbox.Text;


                //Hide the menu
                this.Hide();
                //Show the game form
                GameForm.ShowDialog();
                //Show the main form
                this.Show();
            }
            catch (ArithmeticException) 
            {
                if  (Convert.ToInt32(AgeTextbox.Text) < 3 || Convert.ToInt32(AgeTextbox.Text) > 10)
                {
                    ErrorLabel.Content = "You must be between 3 and 10 years old to play this game!";
                }

                else if (NameTextbox.Text == "")
                {
                    ErrorLabel.Content = "Please input a name!";
                }
                
            } 

            catch (FormatException)
            {
                ErrorLabel.Content = "Please input an age!";
            }
            
        }
        
        /// <summary>
        /// clicking high scores button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHighScores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide the menu
                this.Hide();
                //Show the high scores screen
                HighScoresForm.ShowDialog();
                //Show the main form
                this.Show();
            }

            catch (Exception)
            {
                ErrorLabel.Content = "Could not open high scores!";
            }
            
        }
    }
}

