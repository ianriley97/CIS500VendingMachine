using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Can
    {
        /// <summary>
        /// The price of the can
        /// </summary>
        private int price;

        /// <summary>
        /// The name of the soda.
        /// </summary>
        private string name;

        /// <summary>
        /// The number of this type of can remaining.
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
        /// This is the light that turns on if there are no more cans remaining to dispense.
        /// </summary>
        private Light soldOutLight;

        /// <summary>
        /// This is the light that turns on if there is enough money inputted to allow the purchase.
        /// </summary>
        private Light purchaseableLight;

        /// <summary>
        /// This is the can dispenser. I'm not exactly sure how this is supposed to work yet but I will eventually.
        /// </summary>
        private CanDispenser dispenser;


        public Can(int p, string n, int c, Light sOL, Light pL, CanDispenser d, DebugDisplay nS)
        {
            price = p;
            name = n;
            count = c;
            soldOutLight = sOL;
            purchaseableLight = pL;
            dispenser = d;
        }

        /// <summary>
        /// This is to check and make sure that the customer has enough money to purchase the can.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CheckPurchasability(int amount)
        {
            if (amount >= price && count != 0)
            {
                purchaseableLight.TurnOn();
                return true;
            }
            else
            {
                purchaseableLight.TurnOff();
                return false;
            }
        }

        
        /// <summary>
        /// This just returns the value of the can for use to check if it can be bought.
        /// </summary>
        /// <returns></returns>
        public int CheckPrice()
        {
            return price;
        }


        /// <summary>
        /// This handles when a can is bought. It removes the can from the count, actuates the dispenser, and then changes the distplay
        /// </summary>
        /// <param name="totalValue">This is the total amount of money the customer has put into the machine</param>
        /// <returns>Returns an int with the price of the can </returns>
        public void CanBought(int totalValue)
        {
            if (count == 0)
            {
                return;
            }
            bool haveEnough = CheckPurchasability(totalValue);
            if  (haveEnough != true) {
                return;
            }
            count--;
            dispenser.Actuate();
            if (count == 0)
            {
                soldOutLight.TurnOn();
                purchaseableLight.TurnOff();
            }
        }



        /// <summary>
        /// This resets the Can to its original form
        /// </summary>
        /// <param name="p">The original price</param>
        /// <param name="n">The original name</param>
        /// <param name="c">The original count</param>

        public void Reset(int c)
        {
            count = c;
            soldOutLight.TurnOff();
            purchaseableLight.TurnOff();
        }
    }
}
