using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class ChatTechnicalSupport
    {
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public string Message { get; set; }
        public DateOnly DateMessage { get; set; }
        public TimeOnly TimeMessage { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual User User { get; set; }
    }
}
