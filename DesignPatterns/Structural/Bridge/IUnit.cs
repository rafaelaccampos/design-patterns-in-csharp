namespace DesignPatterns.Structural.Bridge
{
    public interface IUnit
    {
        decimal Minimum { get; set; }

        decimal Maximum { get; set; }

        bool Validate(decimal units);
    }
}
