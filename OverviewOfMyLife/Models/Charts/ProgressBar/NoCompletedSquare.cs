namespace OverviewOfMyLife.Models.Charts.ProgressBar
{
    public class NoCompletedSquare : IEvent
    {
        public string Color { get => "gray"; }
        public DateTime DateTime { get; set; }
    }
}
