﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Domain.ViewModels.Account
{
    public class ActivateUserEmployeeViewModel
    {
        public string PassportSeriesNumber { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string INN { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CategoryBeneficiaryID { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string DocumentImgName { get; set; }
    }
}
