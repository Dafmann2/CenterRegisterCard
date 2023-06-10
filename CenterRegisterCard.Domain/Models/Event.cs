using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class Event
    {
        public Event()
        {
            EventUpdates = new HashSet<EventUpdate>();
        }

        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DateEvent { get; set; }
        public TimeOnly TimeEvent { get; set; }
        public string Location { get; set; }
        public string ImageEvent { get; set; }
        public string DescriptionTitle { get; set; }

        public virtual ICollection<EventUpdate> EventUpdates { get; set; }
    }
}
