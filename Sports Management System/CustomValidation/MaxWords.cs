using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.CustomValidation
{
    public class MaxWords : ValidationAttribute
    {
        private readonly int _maxWords;
        public MaxWords(int maxWords)
        {
            _maxWords = maxWords;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Description is required");
            var textValue = value.ToString();
            return textValue.Split(' ').Length > _maxWords
                ? new ValidationResult("Too long! Only Allowed 100 words.")
                : ValidationResult.Success;
        }
    }
}