using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class SocialCardStatus
    {
        public SocialCardStatus()
        {
            SocialCards = new HashSet<SocialCard>();
        }

        public int SocialCardStatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<SocialCard> SocialCards { get; set; }
    }
}
