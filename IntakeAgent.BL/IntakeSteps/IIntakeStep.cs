using IntakeAgent.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeAgent.BL.IntakeSteps
{
    public interface IIntakeStep
    {
        (bool, Book) RunStep(Book book);
    }
}
