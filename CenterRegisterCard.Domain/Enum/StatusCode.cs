using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,

        SocialCardNotFound = 10,

        //NotFound = 20,

        OK = 200,
        InternalServerError = 500
    }
}
