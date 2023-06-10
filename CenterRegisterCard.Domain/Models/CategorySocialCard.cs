using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class CategorySocialCard
    {
        public CategorySocialCard()
        {
            SocialCards = new HashSet<SocialCard>();
        }

        public int IdCategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SocialCard> SocialCards { get; set; }
    }
}
