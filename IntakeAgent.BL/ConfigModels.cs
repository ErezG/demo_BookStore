namespace IntakeAgent.BL
{
    public class IntakeConfigs
    {
        public string ExportPath { get; set; }
    }

    public class BooksProcessorConfigs
    {
        public bool KeepLog { get; set; }
        public bool IsFullLog { get; set; }
        public IntakeStepConfig[] IntakeSteps { get; set; }
    }

    public class IntakeStepConfig
    {
        public string StepType { get; set; }
        public string Name { get; set; }
    }
}
