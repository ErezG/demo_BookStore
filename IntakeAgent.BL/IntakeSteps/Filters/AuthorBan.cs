using IntakeAgent.Common;
using Microsoft.Extensions.Logging;
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

        public override bool IsValid(Book book, ILogger? logger)
        {
            var author = book.Author;
            bool requiredBan = _isPeterRegex.IsMatch(author);
            bool isValid = requiredBan == false;

            if (logger != null)
            {
                var isBannedContained = isValid ? "doesn't contain" : "contains";
                var log = CreateLog(book.Title, isValid, $"{nameof(Book.Author)} \"{author}\" {isBannedContained} banned name: \"{_bannedName}\"");
                logger.LogDebug(log);
            }

            return isValid;
        }
    }
}
