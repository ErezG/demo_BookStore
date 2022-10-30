using IntakeAgent.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeAgent.BL.IntakeSteps
{
    public interface IIntakeStep
    {
        (bool, Book) RunStep(Book book) => RunStep(book, null);
        (bool, Book) RunStep(Book book, ILogger? logger);
    }
}
