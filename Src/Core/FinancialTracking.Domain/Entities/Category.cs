using FinancialTracking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities
{
    public class Category : BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = null!;

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        [JsonIgnore]
        public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
        [JsonIgnore]
        public ICollection<RecurringTransaction>? RecurringTransactions { get; set; } = new List<RecurringTransaction>();
    }
}
