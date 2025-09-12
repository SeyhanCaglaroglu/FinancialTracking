using FinancialTracking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities
{
    public class Category : BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = null!;

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
        public ICollection<RecurringTransaction>? RecurringTransactions { get; set; } = new List<RecurringTransaction>();
    }
}
