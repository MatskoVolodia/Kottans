using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kottans
{
    class CreditCard
    {
        static public bool IsCreditCardNumberValid(string card)
        {        
            card = Clean(card);
            bool resTF =  ((int)char.GetNumericValue(card[card.Length - 1]) == FindCheckDigit(GetTotalSum(card))) ? true : false;
            if (resTF)
            {
                return GetCreditCardVendor(card) == "Unknown" ? false : true;
            }
            return false;
        }
        static public string GetCreditCardVendor(string card)
        {
            card = Clean(card);
            if ((int)char.GetNumericValue(card[card.Length - 1]) != FindCheckDigit(GetTotalSum(card))) return "Unknown";
            int check = Convert.ToInt32(card.Remove(4));
            if ((check >= 3400 && check <= 3499) || (check >= 3700 && check <= 3799))
            {
                return (card.Length == 15) ? "American Express" : "Unknown";
            }
            if ((check >= 5000 && check <= 5099) || (check >= 5600 && check <= 6999))
            {
                return (card.Length >= 12 && card.Length <= 19) ? "Maestro" : "Unknown";
            }
            if (check >= 5100 && check <= 5599)
            {
                return (card.Length == 16) ? "MasterCard" : "Unknown";
            }
            if (check >= 4000 && check <= 4999)
            {
                return (card.Length == 13 || card.Length == 16 || card.Length == 19) ? "VISA" : "Unknown";
            }
            if (check >= 3528 && check <= 3589)
            {
                return (card.Length == 16) ? "JCB" : "Unknown";
            }
            return "Unknown";
        }
        static public string GenerateNextCreditCardNumber(string card)
        {
            if (!IsCreditCardNumberValid(card))
            {
                Console.WriteLine("Incorrect input. Your card returned.");
                return card;
            }
            card = Clean(card);
            ulong number;
            try
            {
                number = Convert.ToUInt64(card);
            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect input. Your card returned.");
                return card;
            }
            string currentVendor = GetCreditCardVendor(card);
            number++;
            while (true)
            {
                int checkDigit = FindCheckDigit(GetTotalSum(GenerateNewWithOldSize(number, card.Length)));
                if ((int)(number % 10) <= checkDigit)
                {
                    string newCard = GenerateNewWithOldSize(number + (ulong)(checkDigit - (int)(number % 10)), card.Length);
                    if (GetCreditCardVendor(newCard) == currentVendor) return newCard; else
                    {
                        Console.WriteLine("No more card numbers available for this vendor. Returned your card.");
                        return card;
                    }
                }
                number += (10 - number % 10);
                if (number >= Math.Pow(10, card.Length))
                {
                    Console.WriteLine("No more card numbers available for this vendor. Returned your card.");
                    return card;
                }
            }
        }

        static private string GenerateNewWithOldSize(ulong x, int size)
        {
            string temp = x.ToString();
            int startLength = temp.Length;
            for (int i = 0; i < size - startLength; i++)
            {
                temp = "0" + temp;
            }
            return temp;
        }
        static private string Clean(string temp)
        {
            while (temp.IndexOf(' ') != -1)
            {
                temp = temp.Remove(temp.IndexOf(' '), 1);
            }
            return temp;
        }
        static private int FindCheckDigit(int totalSum)
        {
            return ((totalSum * 9) % 10);
        }
        static private int GetTotalSum(string card)
        {
            int totalSum = 0;
            for (int i = 2; i < card.Length + 1; i++)
            {
                int temp = (int)char.GetNumericValue(card[card.Length - i]);
                if (i % 2 != 0) totalSum += temp;
                else
                {
                    temp *= 2;
                    totalSum += (temp < 10) ? temp : (temp % 10 + temp / 10);
                }
            }
            return totalSum;
        }
    }
}
