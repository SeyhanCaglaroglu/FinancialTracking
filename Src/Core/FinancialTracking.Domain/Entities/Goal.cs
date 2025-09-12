using FinancialTracking.Domain.Entities.Common;
using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities
{
    public class Goal : BaseEntity<int>, IAuditEntity
    {
        public string Title { get; set; } = null!;
        public Money TargetAmount { get; set; } = null!;
        public Money CurrentAmount { get; set;} = null!;
        public DateTime Deadline { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
