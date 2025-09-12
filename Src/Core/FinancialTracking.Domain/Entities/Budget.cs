using FinancialTracking.Domain.Entities.Common;
using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities
{
    public class Budget: BaseEntity<int>, IAuditEntity
    {
        public Money TotalAmount { get; set; } = null!;

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        
    }
}
