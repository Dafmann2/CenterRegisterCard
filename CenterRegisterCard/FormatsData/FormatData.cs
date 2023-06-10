using CenterRegisterCard.DAL;
using CenterRegisterCard.DAL.Repositorias;
using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace CenterRegisterCard.FormatsData
{
    public class FormatData
    {
        public static CenterRegisterCardContext _context;
        public static UserStatusRepository _userStatusRepository;
        public static CategoryBeneficiaryRepository _categoryBeneficiaryRepository;

        public FormatData(CenterRegisterCardContext context,UserStatusRepository userStatusRepository,CategoryBeneficiaryRepository categoryBeneficiaryRepository) 
        {
        _context = context;
            _userStatusRepository= userStatusRepository;
            _categoryBeneficiaryRepository= categoryBeneficiaryRepository;
        }
        public static string SocialCardViewFormatNumber(string numbercard)
        {
            var sb = new StringBuilder();
            var counter = 0;
            foreach (var element in numbercard)
            {
                if (counter == 4)
                {
                    sb.Append(" ");
                    counter = 0;
                }

                sb.Append(element);
                counter++;
            }
            return sb.ToString();
        }

        public static string CategoryBeneficiaryIDToString(CategoryBeneficiary category) {
            return category.CategoryName;
        }
        public static string SocialCardViewFormatDateEnd(DateTime date)
        {
            return date.ToString("MM" + "/" + "yy").Replace(".", "/");
        }

        public static string UserStatusIDToString(int status)
        {
            UserStatus userStatus = _context.UserStatuses.FirstOrDefault(x=>x.IduserStatus == status);
            return userStatus.NameStatus;
        }
    }
}
