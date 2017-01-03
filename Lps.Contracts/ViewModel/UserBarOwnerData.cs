using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel
{
    public class UserBarOwnerData : UserData
    {
        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }
    }
}
