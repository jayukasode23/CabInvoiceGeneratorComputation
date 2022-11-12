using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGeneratorProgram
{
    public class InvoiceSummary
    {
        private int numberofRides;
        private double totalFare;
        private double averageFare;

        public InvoiceSummary(int numberofRides, double totalFare)
        {
            this.numberofRides = numberofRides;
            this.totalFare = totalFare;
            this.averageFare = this.totalFare / this.numberofRides;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is InvoiceSummary)) return false;
            InvoiceSummary inputobject = (InvoiceSummary)obj;
            return this.numberofRides == inputobject.numberofRides && this.totalFare == inputobject.totalFare && this.averageFare == inputobject.averageFare;

        }
        public override int GetHashCode()
        {
            return this.numberofRides.GetHashCode();
        }
    }
}