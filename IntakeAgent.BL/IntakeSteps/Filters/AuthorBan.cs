using IntakeAgent.Common;
using System.Text.RegularExpressions;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal class AuthorBan : IntakeFilter
    {
        public AuthorBan(string name)
        {
            _bannedName = name;
            _isPeterRegex = new Regex(@$"(?<=^|\W){name}(?=$|\W)", RegexOptions.IgnoreCase);
        }

        private readonly string _bannedName;
        private readonly Regex _isPeterRegex;

        public override bool IsValid(Book book)
        {
            var author = book.Author;
            bool requiredBan = _isPeterRegex.IsMatch(author);
            bool isValid = requiredBan == false;
            var log = CreateLog(book.Title, isValid, $"{nameof(Book.Author)} {author} contains banned name: {_bannedName}");
            return isValid;
        }
    }
}
