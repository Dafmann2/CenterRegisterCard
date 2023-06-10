using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Domain.ViewModels.Account
{
    public class EventAddViewModel
    {
        public int EventId { get; set; }
        [Required(ErrorMessage = "Укажите заголовок")]
        [MinLength(4, ErrorMessage = "Заголовок должно иметь длину больше 4 символов")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Укажите описание")]
        [MinLength(30, ErrorMessage = "Описание должно иметь длину больше 30 символов")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Укажите дату")]
        public DateOnly DateEvent { get; set; }
        [Required(ErrorMessage = "Укажите время")]
        public TimeOnly TimeEvent { get; set; }
        [Required(ErrorMessage = "Укажите локацию")]
        [MinLength(3, ErrorMessage = "Локацию должно иметь длину больше 3 символов")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Укажите картинку")]
        public string ImageEvent { get; set; }
    }
}
