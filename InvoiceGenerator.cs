using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGeneratorProgram
{
    public class InvoiceGenerator
    {
        /// The cab service is a subscription based cab service,where the customer books a cab ,and pays the bill at end of the month.
        /// Step 1- Calculate fare Given distance and time ,the invoice generator should return the total fare for the journey.
        /// cost-Rs.10 per kilometer + Rs.1 per minute.
        /// Minimum Fare-Rs.5
        /// Step 2- Multiple Rides.
        /// The invoice generator should now take in multiple rides,and calculate the aggregate total for all.
        /// step 3- Enhanced Invoice- The invoice generator should now return the following as a part of the invoice-Total number of rides,Total Fare,Average Fare Per Ride.
        /// step 4-Invoice services- Given a user id,the invoice services gets the list of rides from the riderepository,and returns the invoice.
        /// step 5 -Premium Rides(Bonous)-The cab agenecy now supports 2 categories of rides:Normal Rides:Rs. 10 per km,Rs.1 per minute,Minimum fare of Rs 5,Premium rides:Rs 15 per km,rs.2 per minute,Minimum fare of Rs.20
        /// </summary>


        RideType rideType;
        private RideRepository rideRepository;

        private double MINIMUM_COST_PER_KM;
        private int COST_PER_TIME;
        private double MINIMUM_FARE;


        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                if (rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride");
            }
        }

        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabInvoiceException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride");
                }
                if (distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");

                }
                if (time <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Tme,Time is not less than 0");
                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);

        }

        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");

                }
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }



    }
}