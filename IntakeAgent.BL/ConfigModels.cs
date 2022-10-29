using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeAgent.BL
{
    public class IntakeConfigs
    {
        public IntakeStepConfig[] IntakeSteps { get; set; }
        public string ExportPath { get; set; }
    }

    public class IntakeStepConfig
    {
        public string StepType { get; set; }
        public string Name { get; set; }
    }
}
