using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Media;
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
using System.Windows.Threading;
using static Assignment5.clsGame;

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {

        /// <summary>
        /// Variable to hold the high scores form.
        /// </summary>
        private HighScores wndCopyHighScores;

        /// <summary>
        /// Variable to hold our necessary operand (I'm now aware that this is supposed to be operator, leaving unchanged for now)
        /// </summary>
        private String operand;


        /// <summary>
        /// Property to get and set the high scores.
        /// </summary>
        public HighScores CopyHighScores
        {
            get { return wndCopyHighScores; }
            set { wndCopyHighScores = value; }
        }

        /// <summary>
        /// various sounds used throughout the game
        /// </summary>
        public SoundPlayer cheer = new SoundPlayer(@"C:\Users\caleb\source\repos\Assignment5\Assignment5\CHEERING_AND_CLAPPING_cct.wav");
        public SoundPlayer start = new SoundPlayer(@"C:\Users\caleb\source\repos\Assignment5\Assignment5\drum_roll_y.wav");
        public SoundPlayer correct = new SoundPlayer(@"C:\Users\caleb\source\repos\Assignment5\Assignment5\baseball_hit.wav");

        /// <summary>
        /// Initalize components and set up timer
        /// </summary>
        public Game()
        {
            InitializeComponent();
            MyTimer.Interval = TimeSpan.FromSeconds(1);
            MyTimer.Tick += new EventHandler(Timer_Tick);

        }

        /// <summary>
        /// close window and restore to as it was at startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEndGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                label1.Visibility = Visibility.Visible;
                SubmitButton.Visibility = Visibility.Hidden;
                cmdEndGame.Visibility = Visibility.Visible;
                cmdStartGame.Visibility = Visibility.Visible;
                AnswerBox.Visibility = Visibility.Hidden;
                QuestionLabel.Visibility = Visibility.Hidden;
                NumberCorrectLabel.Visibility = Visibility.Hidden;
                NumberQuestionLabel.Visibility = Visibility.Hidden;
                TimeLabel.Visibility = Visibility.Hidden;
                CorrectLabel.Visibility = Visibility.Hidden;
            }
            
            catch (Exception)
            {
                CorrectLabel.Content = "Something went wrong!";
            }
        }

        ///Commented this out because I didn't want players to have the option to view high scores while a game is ongoing
        ///it could be at the initial menu, but I figured it was fine to just force players to end game and go back to menu

       /* private void cmdHighScores_Click(object sender, RoutedEventArgs e)
        {
            //Hide the game form
            this.Hide();
            //Show the high scores
            CopyHighScores.ShowDialog();
        } */

        /// <summary>
        /// close window and restore to as it was at startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                label1.Visibility = Visibility.Visible;
                SubmitButton.Visibility = Visibility.Hidden;
                cmdEndGame.Visibility = Visibility.Visible;
                cmdStartGame.Visibility = Visibility.Visible;
                AnswerBox.Visibility = Visibility.Hidden;
                QuestionLabel.Visibility = Visibility.Hidden;
                NumberCorrectLabel.Visibility = Visibility.Hidden;
                NumberQuestionLabel.Visibility = Visibility.Hidden;
                TimeLabel.Visibility = Visibility.Hidden;
                CorrectLabel.Visibility = Visibility.Hidden;
                e.Cancel = true;
            }

            catch (Exception)
            {
                CorrectLabel.Content = "Something went wrong!";
            }
        }

        public static int iCurrentGameQuestion = 1; //Which question is the user on 1 through 10
        public static int iSeconds;             //How many seconds have elapsed in the game
        DispatcherTimer MyTimer = new DispatcherTimer(); // Track time
                                                       

        /// <summary>
        /// updates iseconds every second, display this updated value on the timer label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {  
            try
            {
                iSeconds++;
                TimeLabel.Content = "Time Elapsed: " + iSeconds;
            }

            catch (Exception)
            {
                CorrectLabel.Content = "Something went wrong!";
                CorrectLabel.Foreground = Brushes.Red;
            }
            

        }




        //StartButton_Click

        /// <summary>
        /// Clicking the "Start Game" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStartGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // set all tracked values back to default
                User.iNumberCorrect = 0;
                User.iGameSeconds = 0;
                iSeconds = 0;
                iCurrentGameQuestion = 1;


                // starting the timer
                MyTimer.Start();

                //play start sound
                start.Load();
                start.Play();




                if (clsGame.egametype == clsGame.GameType.Add)
                {
                    operand = " + ";
                }

                if (clsGame.egametype == clsGame.GameType.Subtract)
                {
                    operand = " - ";
                }

                if (clsGame.egametype == clsGame.GameType.Mult)
                {
                    operand = " x ";
                }

                if (clsGame.egametype == clsGame.GameType.Divide)
                {
                    operand = " / ";
                }

                PlayerLabel.Content = "Current Player: " + User.Name;

                // hide all buttons/labels that need to be hidden, show all buttons/labels that need to be shown

                label1.Visibility = Visibility.Hidden;
                SubmitButton.Visibility = Visibility.Visible;
                cmdEndGame.Visibility = Visibility.Visible;
                cmdStartGame.Visibility = Visibility.Hidden;
                AnswerBox.Visibility = Visibility.Visible;
                QuestionLabel.Visibility = Visibility.Visible;
                NumberCorrectLabel.Visibility = Visibility.Visible;
                NumberQuestionLabel.Visibility = Visibility.Visible;
                TimeLabel.Visibility = Visibility.Visible;
                CorrectLabel.Visibility = Visibility.Visible;
                PlayerLabel.Visibility = Visibility.Visible;

                // setting initial label content after game start
                NumberCorrectLabel.Content = "Number of correct guesses: " + User.iNumberCorrect;
                NumberQuestionLabel.Content = "Question " + iCurrentGameQuestion;
                TimeLabel.Content = "Time Elapsed: " + iSeconds;
                CorrectLabel.Content = "";

                clsGame.GenerateQuestion();

                QuestionLabel.Content = MathGameQuestion.LeftNumber.ToString() + operand + MathGameQuestion.RightNumber.ToString();
            }

            catch (Exception) 
            {
                CorrectLabel.Content = "Something went wrong!";
            }
            

            

        }

        /// <summary>
        /// submit button click if not last question - Use clsGame.GenerateQuestion
        /// if last question - call method DisplayUsersScore() in high score window, then display high score window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int guess = Convert.ToInt32(AnswerBox.Text);

                // Validate user's guess clsGame.ValidateUsersGuess
                clsGame.ValidateUsersGuess(guess);

                // play correct sound
                correct.Load();
                correct.Play();

                //  Tell user if they got it right or wrong. Update static iNumberCorrect in User class
                if (clsGame.ValidateUsersGuess(guess) == true)
                {
                    User.iNumberCorrect++;
                    NumberCorrectLabel.Content = "Number of correct guesses: " + User.iNumberCorrect;
                    CorrectLabel.Foreground = Brushes.Green;
                    CorrectLabel.Content = "Correct!";
                }

                else
                {
                    CorrectLabel.Foreground = Brushes.Red;
                    CorrectLabel.Content = "Not quite right, keep trying!";
                }

                iCurrentGameQuestion++;
                NumberQuestionLabel.Content = "Question " + iCurrentGameQuestion;

                if (iCurrentGameQuestion == 11)
                {
                    // play sound for game ending
                    cheer.Load();
                    cheer.Play();

                    //close window and restore to as it was at startup
                    this.Hide();
                    label1.Visibility = Visibility.Visible;
                    SubmitButton.Visibility = Visibility.Hidden;
                    cmdEndGame.Visibility = Visibility.Visible;
                    cmdStartGame.Visibility = Visibility.Visible;
                    AnswerBox.Visibility = Visibility.Hidden;
                    QuestionLabel.Visibility = Visibility.Hidden;
                    NumberCorrectLabel.Visibility = Visibility.Hidden;
                    NumberQuestionLabel.Visibility = Visibility.Hidden;
                    TimeLabel.Visibility = Visibility.Hidden;
                    CorrectLabel.Visibility = Visibility.Hidden;
                    AnswerBox.Text = "";

                    //stopping timer
                    MyTimer.Stop();

                    // send information to high scores screen
                    User.iGameSeconds = iSeconds;
                    CopyHighScores.DisplayUsersScore();
                    CopyHighScores.ShowDialog();
                    


                }

                else
                {
                    clsGame.GenerateQuestion();
                    QuestionLabel.Content = MathGameQuestion.LeftNumber.ToString() + operand + MathGameQuestion.RightNumber.ToString();
                    AnswerBox.Text = "";
                }

            }

            catch (FormatException)
            {
        
                CorrectLabel.Content = "Please enter a number!";
                CorrectLabel.Foreground = Brushes.Red;
                
            }
            

        }

        /// <summary>
        /// allowing submit button to also be pressed with enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    SubmitButton_Click(sender, e);
                }
            }

            catch (Exception ex)
            {
                CorrectLabel.Content = ex;
                CorrectLabel.Foreground = Brushes.Red;
            }
            
        }



    }
}
