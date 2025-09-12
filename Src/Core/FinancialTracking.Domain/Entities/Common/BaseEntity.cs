using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}
