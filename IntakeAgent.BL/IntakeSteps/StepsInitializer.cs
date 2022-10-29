using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntakeAgent.BL.IntakeSteps
{
    internal class StepsInitializer
    {
        public static IEnumerable<IIntakeStep> InitializeSteps(IEnumerable<IntakeStepConfig> stepConfigs)
        {
            foreach (var config in stepConfigs)
            {
                yield return InitializeStep(config);
            }
        }

        public static IIntakeStep InitializeStep(IntakeStepConfig stepConfig)
        {
            var typeNS = "IntakeAgent.BL.IntakeSteps.";
            try
            {
                typeNS += stepConfig.StepType;

                Assembly currentAssem = Assembly.GetExecutingAssembly();
                var type = currentAssem.GetType(typeNS);
                var ctor = type.GetConstructors().First();
                var ctorParams = ctor.GetParameters();
                object[] instanceParams = new object[ctorParams.Length];
                if (ctorParams.Length > 0)
                {
                    var configProps = stepConfig.GetType().GetProperties();
                    for (int i = 0; i < ctorParams.Length; i++)
                    {
                        var ctorP = ctorParams[i];
                        var matchingConf = configProps.FirstOrDefault(confP => string.Compare(confP.Name, ctorP.Name, true) == 0);
                        if (matchingConf != null)
                        {
                            instanceParams[i] = matchingConf.GetValue(stepConfig);
                        }
                    }
                }

                var instance = (IIntakeStep)ctor.Invoke(instanceParams);
                return instance;
            }
            catch
            {
                throw new Exception($"Failed to instanciate an {nameof(IIntakeStep)} of type {typeNS}");
            }
        }
    }
}
