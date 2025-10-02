using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArqaCharity.Core.Models.DTOs.Needys
{
    public class CreateNeedyDto
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal RequiredAmount { get; set; }
    }
}
