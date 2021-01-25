using System;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.CustomValidation
{
    public class ValidGameCode : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string game_Code = Convert.ToString(value);
            if (game_Code.Length > 7)
                return false;
            int uppercaseCharCounter = 0;
            int numericCharCounter = 0;
            for (int i = 0; i < game_Code.Length; i++)
            {
                //if character is upper add +1 to counter
                if (char.IsUpper(game_Code[i]))
                {
                    uppercaseCharCounter++;
                }
                if (char.IsNumber(game_Code[i]))
                {
                    numericCharCounter++;
                }
            }

            if (uppercaseCharCounter != 4 || numericCharCounter != 3)
                return false;

            return true;
        }
    }
}