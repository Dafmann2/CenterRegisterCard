using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CenterRegisterCard.Domain.ViewModels.Account
{
    public class ImageUploadViewModel
    {
        [Display(Name = "Фотография")]
        public IFormFile ImageFile { get; set; }
    }
}
