using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.ViewModels
{
    public class AdminInvocation
    {
        [Key]
        public string id { get; set; }

        public string Input { get; set; }
    }
}
