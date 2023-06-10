using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class EventUpdate
    {
        public int EventUpdateId { get; set; }
        public int EventId { get; set; }
        public string EmployeePassport { get; set; }
        public DateOnly DateUpdate { get; set; }
        public TimeOnly TimeUpdate { get; set; }

        public virtual Employee EmployeePassportNavigation { get; set; }
        public virtual Event Event { get; set; }
    }
}
