using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Domain.ViewModels.Account
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DateEvent { get; set; }
        public TimeOnly TimeEvent { get; set; }
        public string Location { get; set; }
        public string ImageEvent { get; set; }
        public string DescriptionTitle { get; set; }
    }
}
