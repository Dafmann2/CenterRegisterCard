using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Domain.ViewModels.Account
{
    public class ChangeDataViewModel
    {
        [Required(ErrorMessage = "Укажите паспорт")]
        [MaxLength(20, ErrorMessage = "Отчество должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Отчество должно иметь длину больше 3 символов")]
        public string PassportSeriesNumber { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "Укажите отчество")]
        [MaxLength(20, ErrorMessage = "Отчество должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Отчество должно иметь длину больше 3 символов")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Укажите ИНН")]
        [MaxLength(12, ErrorMessage = "Логин должно иметь длину  12 символов")]
        [MinLength(12, ErrorMessage = "Логин должно иметь длину  12 символов")]
        public string INN { get; set; }
        public string Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "Укажите номер телефона")]
        [MaxLength(11, ErrorMessage = "Логин должно иметь длину  11 символов")]
        [MinLength(11, ErrorMessage = "Логин должно иметь длину  11 символов")]
        public string PhoneNumber { get; set; }
        public int CategoryBeneficiaryID { get; set; }
        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(20, ErrorMessage = "Логин должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должно иметь длину больше 3 символов")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        public DateOnly BirthdayDate { get; set; }

        [Display(Name = "Фотография")]
        public IFormFile ImageFile { get; set; }
    }
}
