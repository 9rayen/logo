namespace Logo_loading.Models
{
    /// <summary>
    /// Represents a single letter in the loading text.
    /// Font size is now fixed, so this just holds the character.
    /// </summary>
    public class LetterModel
    {
        /// <summary>
        /// Gets or sets the character to display.
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// Initializes a new instance of the LetterModel class.
        /// </summary>
        /// <param name="character">The character to display</param>
        public LetterModel(string character)
        {
            Character = character;
        }

        /// <summary>
        /// Initializes a new instance of the LetterModel class with default values.
        /// </summary>
        public LetterModel() { }
    }
}