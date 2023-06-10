using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class CategoryBeneficiary
    {
        public CategoryBeneficiary()
        {
            Users = new HashSet<User>();
        }

        public int CategoryBeneficiaryId { get; set; }
        public string CategoryName { get; set; }
        public int BeneficiaryId { get; set; }

        public virtual Beneficiary Beneficiary { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
