using System.Collections.Generic;
using System.Text;

namespace Layer_2_Common.Type
{
    public class EntryControls
    {
        public static bool IsValidDecimal(string input)
        {

            if (decimal.TryParse(input, out decimal result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string FixTurkishCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Dictionary of incorrect and correct characters
            var replacements = new Dictionary<char, char>
            {
                { 'Ð', 'Ğ' },  // Replace Ð with Ğ
                { 'Ý', 'İ' },  // Replace Ý with İ
                { 'Þ', 'Ş' },  // Replace Þ with Ş
                { 'ð', 'ğ' },  // Replace ð with ğ (lowercase)
                { 'ý', 'ı' },  // Replace ý with ı
                { 'þ', 'ş' }   // Replace þ with ş (lowercase)
            };

            var sb = new StringBuilder(input);

            // Iterate through the string and replace incorrect characters
            for (int i = 0; i < sb.Length; i++)
            {
                if (replacements.ContainsKey(sb[i]))
                {
                    sb[i] = replacements[sb[i]];
                }
            }

            return sb.ToString();
        }
    }
}
