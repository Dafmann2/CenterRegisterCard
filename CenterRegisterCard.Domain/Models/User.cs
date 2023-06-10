using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class User
    {
        public User()
        {
            ChatTechnicalSupports = new HashSet<ChatTechnicalSupport>();
            SocialCards = new HashSet<SocialCard>();
        }

        public string PassportSeriesNumber { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Inn { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CategoryBeneficiaryId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserStatusId { get; set; }
        public string DocumentImgName { get; set; }
        public DateOnly? BirthdayDate { get; set; }

        public virtual CategoryBeneficiary CategoryBeneficiary { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<ChatTechnicalSupport> ChatTechnicalSupports { get; set; }
        public virtual ICollection<SocialCard> SocialCards { get; set; }
    }
}
