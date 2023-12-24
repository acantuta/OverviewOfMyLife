namespace OverviewOfMyLife.Models.Charts.ProgressBar
{
    public class DoneSquare : IEvent
    {
        public string Color { get => "green"; }
        public DateTime DateTime { get; set; }
    }
}
