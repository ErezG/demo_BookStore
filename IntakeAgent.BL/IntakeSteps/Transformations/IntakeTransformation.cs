using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal abstract class IntakeTransformation : IIntakeStep
    {
        public IntakeTransformation()
        {
            _typeName = this.GetType().Name;
        }

        private readonly string _typeName;

        public (bool, Book) RunStep(Book book)
        {
            book = Transform(book);
            return (true, book);
        }

        public abstract Book Transform(Book book);

        protected string CreateLog(string title, string change)
        {
            return $"\"{title}\" through {_typeName} - change {change}";
        }
    }
}
