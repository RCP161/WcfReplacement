namespace Prototype.Publisher.Contract
{
    internal interface IScenarioService
    {
        void EvaluatePresentStandard();
        void EvaluateRequestPerformance();
        void EvaluateSerialisationPerformance();


        event Events.ScenarioFinishedEventHandler ScenarioFinishedEvent;
    }
}
