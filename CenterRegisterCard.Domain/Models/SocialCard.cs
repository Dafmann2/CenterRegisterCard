using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class SocialCard
    {
        public string NumberCard { get; set; }
        public int Cvc { get; set; }
        public string PassportSeriesNumberUser { get; set; }
        public DateOnly? DateEndActive { get; set; }
        public double Balance { get; set; }
        public int StatusCardId { get; set; }
        public DateOnly? DateFormalization { get; set; }
        public int? CategorySocialCardId { get; set; }

        public virtual CategorySocialCard CategorySocialCard { get; set; }
        public virtual User PassportSeriesNumberUserNavigation { get; set; }
        public virtual SocialCardStatus StatusCard { get; set; }
    }
}
