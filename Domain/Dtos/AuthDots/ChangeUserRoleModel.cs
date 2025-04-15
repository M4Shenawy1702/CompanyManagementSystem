using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.AuthDots
{
    public class ChangeUserRoleModel
    {
        public string UserId { get; set; }
        public List<string> roles { get; set; }
    }
}
