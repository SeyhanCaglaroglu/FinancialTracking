using FinancialTracking.Domain.Entities.Common;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Domain.Entities
{
    public class Transaction : BaseEntity<int>, IAuditEntity
    {
        public Money Amount { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TransactionType Type { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
