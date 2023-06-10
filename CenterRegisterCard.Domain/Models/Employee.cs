using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ChatTechnicalSupports = new HashSet<ChatTechnicalSupport>();
            EventUpdates = new HashSet<EventUpdate>();
        }

        public string PassportSeriesNumber { get; set; }
        public string Surename { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<ChatTechnicalSupport> ChatTechnicalSupports { get; set; }
        public virtual ICollection<EventUpdate> EventUpdates { get; set; }
    }
}
