using IntakeAgent.Common;
using System.Text.RegularExpressions;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal class AuthorBan : IntakeFilter
    {
        public AuthorBan(string name)
        {
            _isPeterRegex = new Regex(@$"(?<=^|\W){name}(?>$|\W)", RegexOptions.IgnoreCase);
        }

        private readonly Regex _isPeterRegex;

        public override bool IsValid(Book book)
        {
            var author = book.Author;

            bool requiredBan = _isPeterRegex.IsMatch(author);

            return requiredBan == false;
        }
    }
}
