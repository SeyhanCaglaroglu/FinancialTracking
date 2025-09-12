
namespace FinancialTracking.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";

        private Money() { }

        public Money(decimal amount,string currency)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("Tutar negatif olamaz !");
            this.Amount = amount;
            this.Currency = currency;
        }

        // Eşitlik kontrolü
        public override bool Equals(object? obj)
        {
            if (obj is not Money other) return false;
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    }
}
