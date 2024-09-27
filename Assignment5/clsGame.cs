using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class clsGame
    {
        
        /// <summary>
        /// game types
        /// </summary>
        public enum GameType { Add, Subtract, Mult, Divide }

        /// <summary>
        /// stores gametype selected
        /// </summary>
        public static GameType egametype;
        

        /// <summary>
        /// random number to generate during generatequestions method
        /// </summary>
        static Random rnd = new Random();

        /// <summary>
        /// use the eGametype enum to generate a question of the MathGameQuestion type and return it
        /// </summary>
        /// <returns></returns>
        public static MathGameQuestion GenerateQuestion()
        { 
            // intial randomization of both numbers
            int i = rnd.Next(0, 11);
            int j = rnd.Next(0, 11);

            // addition question
            if (egametype == GameType.Add)
            {
                MathGameQuestion.LeftNumber = i;
                MathGameQuestion.RightNumber = j;
                MathGameQuestion.Answer = MathGameQuestion.LeftNumber + MathGameQuestion.RightNumber;

            }

            // subtraction question
            if (egametype == GameType.Subtract)
            {
                
                if (j > i)
                {
                    i = j;
                    j = i;
                    
                }

                MathGameQuestion.LeftNumber = i;
                MathGameQuestion.RightNumber = j;
                MathGameQuestion.Answer = MathGameQuestion.LeftNumber - MathGameQuestion.RightNumber;
            }

            // multiplication question
            if (egametype == GameType.Mult)
            {
                MathGameQuestion.LeftNumber = i;
                MathGameQuestion.RightNumber = j;
                MathGameQuestion.Answer = MathGameQuestion.LeftNumber * MathGameQuestion.RightNumber;
            }

            // division question
            if (egametype == GameType.Divide)
            {
                //ensuring right number is never 0
                if (j == 0)
                {
                    j = rnd.Next(1, 11);
                }

                // while loop to ensure the remainder will always be 0
                while (i % j != 0) 
                {
                    i = rnd.Next(1, 11);
                    j = rnd.Next(1, 11);


                    if (i % j == 0)
                    {
                        break;
                    }
                }

                MathGameQuestion.LeftNumber = i;
                MathGameQuestion.RightNumber = j;

                MathGameQuestion.Answer = MathGameQuestion.LeftNumber / MathGameQuestion.RightNumber;
            }

            return new MathGameQuestion();
        }

        /// <summary>
        /// check if user's guess is correct
        /// </summary>
        /// <param name="iUsersGuess"></param>
        /// <returns></returns>
        public static bool ValidateUsersGuess(int iUsersGuess)
        {
            if (iUsersGuess == MathGameQuestion.Answer)
            {
                return true;
            }
            return false;
        }


    }

    /// <summary>
    /// Storing components of a math question
    /// </summary>
    public class MathGameQuestion
    {
        public static int LeftNumber;
        public static int RightNumber;
        public static int Answer;
    }
}
