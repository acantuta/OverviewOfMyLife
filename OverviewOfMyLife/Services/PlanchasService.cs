using OverviewOfMyLife.Models.Charts.ProgressBar;

namespace OverviewOfMyLife.Services
{
    public class PlanchasService
    {
        private readonly NotionService _notionService;

        public PlanchasService(NotionService notionService)
        {
            _notionService = notionService;
        }
        public async Task<ProgressBar> GetProgressBar()
        { 
            //var notionService
            var items = await _notionService.GetPlanchasDataAsync();
            var progressBar = new ProgressBar();
            var events = new List<IEvent>();
            progressBar.Events = events;
            var today = DateTime.Now;
            for (var i = 2; i >= 0; i--)
            {
                var month = today.Month - i;
                var year = today.Year;
                var currentDate = new DateTime(year, month, 1);

                while(currentDate.Month == month && currentDate.Year == year)
                {
                    IEvent item = new PendingSquare();

                    if (items.Any(o => o.Date.Date == currentDate))
                    {
                        item = new DoneSquare();
                    }
                     
                    item.DateTime = currentDate;
                    events.Add(item);
                    currentDate = currentDate.AddDays(1);
                }
            }

            return progressBar;
        }
    }
}
