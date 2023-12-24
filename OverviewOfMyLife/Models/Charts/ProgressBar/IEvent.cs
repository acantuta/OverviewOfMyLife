namespace OverviewOfMyLife.Models.Charts.ProgressBar
{
    public interface IEvent
    {
        string Color { get; }
        DateTime DateTime { get; set; }
    }
}
