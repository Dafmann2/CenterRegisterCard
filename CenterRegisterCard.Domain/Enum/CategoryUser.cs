using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Domain.Enum
{
    public enum CategoryUser
    {
        [Display(Name ="Инвалид")]
        Invalid=0,
        [Display(Name = "Пенсионер")]
        Pensioner = 1,
        [Display(Name = "Студент")]
        Student = 2
    }
}
