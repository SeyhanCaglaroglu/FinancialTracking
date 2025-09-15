using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public ICollection<Category>? Categories { get; set; } = new List<Category>();
        [JsonIgnore]
        public ICollection<Goal>? Goals { get; set; } = new List<Goal>();
        [JsonIgnore]
        public Budget? Budget { get; set; }
    }
}
