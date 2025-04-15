using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.AuthDots
{
    public class ChangePasswordDto
    {
        public string OldPass { get; set; }
        public string NewPass { get; set; }

    }
}
