using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kottans
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter your card: ");
                string yourCard = Console.ReadLine();
                Console.WriteLine("Is Valid: {0} \nVendor: {1} \n", CreditCard.IsCreditCardNumberValid(yourCard), CreditCard.GetCreditCardVendor(yourCard));
                string nextCard = CreditCard.GenerateNextCreditCardNumber(yourCard);
                Console.WriteLine("Next generated: {0} \nIs Valid: {1} \n", nextCard, CreditCard.IsCreditCardNumberValid(nextCard));
                Console.ReadKey();
                Console.Clear();
            }    
        }
    }
}
