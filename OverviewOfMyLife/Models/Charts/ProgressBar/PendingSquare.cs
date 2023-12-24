namespace OverviewOfMyLife.Models.Charts.ProgressBar
{
    public class PendingSquare : IEvent
    {
        public string Color { get => "black"; }
        public DateTime DateTime { get; set; }
    }
}
