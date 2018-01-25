using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Coin
    {
        /// <summary>
        /// The value of the coin.
        /// </summary>
        private int value;

        /// <summary>
        /// The count of this type of coin.
        /// </summary>
        private int count;

        public int Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// This is the coin dispenser object for this coin.
        /// </summary>
        private CoinDispenser dispenser;


        public Coin(int v, int c, CoinDispenser d, DebugDisplay cC)
        {
            value = v;
            count = c;
            dispenser = d;
        }

        /// <summary>
        /// This handles when a coin is inserted. It ups the count of that coin, changes the debug display to display the proper amount.
        /// </summary>
        /// <returns>Returns the value of this coin for use elsewhere.</returns>
        public int coinInserted()
        {
            count++;
            return value;
        }

        /// <summary>
        /// This takes the needed change away from the count, actuates the dispenser, and changes the text box
        /// </summary>
        /// <param name="countNeeded">This is the number of coins of this type needed</param>
        /// <returns>If there aren't enough coins, return the value that wasn't covered</returns>
        public int SubtractChange(int countNeeded)
        {
            if (count < countNeeded)
            {
                dispenser.Actuate(count);
                int returner = (countNeeded * value) - (count * value);
                count = 0;
                return returner;
                
            }
            count -= countNeeded;
            dispenser.Actuate(countNeeded);
            return 0;
        }



        /// <summary>
        /// this method will check the change to see if it possible to give change without actually handing it out.
        /// </summary>
        /// <param name="countNeeded">This is the number of coins needed of this type to give proper change</param>
        /// <returns>Returns an int with the amount of money that still needs to be given as change.</returns>
        public int CheckChange(int countNeeded)
        {
            if (count < countNeeded)
            {
                int returner = (countNeeded * value) - (count * value);
                return returner;

            }
            return 0;
        }


        /// <summary>
        /// This resets the object to its original form
        /// </summary>
        /// <param name="v">The original value of the coin</param>
        /// <param name="c">The original count of the coin</param>
        public void Reset(int c)
        {
            count = c;
        }
    }
}
