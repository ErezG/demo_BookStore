﻿using IntakeAgent.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal interface IIntakeTransformation : IIntakeStep
    {
        Book Transform(Book book);
    }
}
