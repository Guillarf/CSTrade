using CSTrades.Enum;
using System;

namespace CSTrades.Model
{
    internal class Trade : Itrade
    {
        public double Value { get; }
        public string ClientSector { get; }
        public DateTime NextPaymentDate { get; }
        public ECategory Category { get; }

        public Trade(DateTime referenceDate, double value, string clientSector, DateTime nextPaymentDate)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
            Category = SetCategory(referenceDate);
        }

        private ECategory SetCategory(DateTime referenceDate)
        {
            if ((referenceDate.Subtract(NextPaymentDate)).Days > 30)
            {
                return ECategory.Expired;
            }
            if (Value > 1000000 && ClientSector == "Private")
            {
                return ECategory.HighRisk;
            }
            if (Value > 1000000 && ClientSector == "Public")
            {
                return ECategory.MediumRisk;
            }
            //TODO: Confirm if there is a Low risk category
            throw new Exception("There is no Category for this trade yet.");
        }
    }
}