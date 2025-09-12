using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Category>? Categories { get; set; } = new List<Category>();
        public ICollection<Goal>? Goals { get; set; } = new List<Goal>();
        public Budget? Budget { get; set; }
    }
}
