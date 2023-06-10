using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class Beneficiary
    {
        public Beneficiary()
        {
            CategoryBeneficiaries = new HashSet<CategoryBeneficiary>();
        }

        public int BeneficiaryId { get; set; }
        public string BeneficiaryServices { get; set; }

        public virtual ICollection<CategoryBeneficiary> CategoryBeneficiaries { get; set; }
    }
}
