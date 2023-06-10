using System;
using System.Collections.Generic;

namespace CenterRegisterCard.Domain.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        public int IduserStatus { get; set; }
        public string NameStatus { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
