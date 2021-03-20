using System;

namespace QRBuddy
{
    public class PayloadModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }

        public string DarkColor { get; set; }
        public string LightColor { get; set; }
        public string Icon { get; set; }
    }
}