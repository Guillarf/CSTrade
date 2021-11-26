using CSTrades.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CSTrades
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region parameters

            string[] completeTrade = new string[3];
            List<Trade> Trades = new List<Trade>();
            double value = 0;
            string clientSector = "";
            DateTime nextPaymentDate = DateTime.MinValue;
            DateTime referenceDate;
            int tradesQuantity;

            #endregion parameters

            Console.WriteLine("Wellcome to CS Trade");
            Console.WriteLine("Input the Reference Date:");

            //Inputing Reference Date
            while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out referenceDate))
            {
                Console.WriteLine("Reference Date is Incorrect. Please, input it again.");
            }

            //Inputing number of trades
            Console.WriteLine("Input how many trades you are doing:");
            while (!int.TryParse(Console.ReadLine(), out tradesQuantity))
            {
                Console.WriteLine("Please, input a right number of trades.");
            }

            //Including Trades
            for (int i = 1; i < tradesQuantity + 1; i++)
            {
                Console.WriteLine($"Include Trade number #{i}");
                completeTrade = Console.ReadLine().Split(" ");

                if (validateIput(completeTrade, ref value, ref clientSector, ref nextPaymentDate))
                {
                    Trades.Add(new Trade(referenceDate, value, clientSector, nextPaymentDate));
                }
                else
                {
                    //Console.WriteLine($"Please, input again Trade number #{i}");
                    i -= 1;
                }
            }

            //Printing output
            foreach (Trade trade in Trades)
            {
                Console.WriteLine(trade.Category.ToString());
            }
            Console.ReadLine();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="c">Array of strings containing the trade values - example "2000000 Private 12/29/2021"</param>
        /// <param name="value">value of the trade</param>
        /// <param name="clientSector">Sector can only be Public or Private</param>
        /// <param name="nextPaymentDate">Next payment date of the trade</param>
        /// <returns></returns>
        public static bool validateIput(string[] c, ref double value, ref string clientSector, ref DateTime nextPaymentDate)
        {
            //Value Validation
            if (!double.TryParse(c[0], out value))
            {
                Console.WriteLine("Error: Value is invalid");
                return false;
            }
            //Client Sector Validation
            if (c[1] != "Public" && c[1] != "Private")
            {
                Console.WriteLine("Error: Client Sector is invalid");
                return false;
            }
            else
            {
                clientSector = c[1];
            }
            //Next Payment Validation
            if (!DateTime.TryParseExact(c[2], "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out nextPaymentDate))
            {
                Console.WriteLine("Error: Next Payment Date is invalid");
                return false;
            }
            return true;
        }
    }
}