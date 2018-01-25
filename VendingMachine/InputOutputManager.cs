using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class InputOutputManager
    {
        /// <summary>
        /// This is the stock of the vending machine. This holds all of the can objects.
        /// </summary>
        private Can[] stock;

        /// <summary>
        /// This is the stock of coins in the vending machine. This holds all of the coin objects.
        /// </summary>
        private Coin[] change;

        /// <summary>
        /// This is the light that comes on when there is no change left to give. (Or it can't give any, I'm not sure yet)
        /// </summary>
        private TimerLight noChangeLight;

        /// <summary>
        /// This is the light that shows the value of all of the coins that have been added.
        /// </summary>
        private AmountDisplay displayAmount;

        /// <summary>
        /// This is the value of the all of the coins added.
        /// </summary>
        private int moneyInserted;

        public InputOutputManager(Can[] s, Coin[] c, TimerLight nCL, AmountDisplay dA)
        {
            stock = s;
            change = c;
            noChangeLight = nCL;
            displayAmount = dA;
            moneyInserted = 0;
        }

        /// <summary>
        /// This handles the coin inserted. It will run the coinInserted() method of the coin, and then take the returned value of the coin and add it to totalValue
        /// </summary>
        /// <param name="index">This is the index of the coin.</param>
        public void InsertCoin(int index)
        {
            moneyInserted += change[index].coinInserted();
            displayAmount.DisplayAmount(moneyInserted);

            for(int i = 0; i < 4; i++)
            {
                stock[i].CheckPurchasability(moneyInserted);
            }
        }

        /// <summary>
        /// This starts the can being bought process. Tells the can to run the removal process, and then calculates the change that is needed.
        /// </summary>
        /// <param name="index"></param>
        public void CanBought(int index)
        {
            int price = stock[index].CheckPrice(); 
            if (moneyInserted < price)
            {
                return;
            }
            int changeNeeded = moneyInserted - price;
            bool allChange = CalculateChangeByType(changeNeeded);
            if (allChange == true)
            {
                SubtractChangeByType(changeNeeded);
                stock[index].CanBought(moneyInserted);
                moneyInserted = 0;
                displayAmount.DisplayAmount(0);
                for (int i = 0; i < 4; i++)
                {
                    stock[i].CheckPurchasability(moneyInserted);
                }
            }
            else
            {
                noChangeLight.TurnOn3Sec();
            }
        }


        /// <summary>
        /// This handles when the return coin button is pressed. It determines the change needed and then gives it out. Similarly to how the change works.
        /// </summary>
        public void ReturnCoin()
        {
            int changeNeeded = moneyInserted;
            SubtractChangeByType(changeNeeded);
            moneyInserted = 0;
            displayAmount.DisplayAmount(0);
        }

        /// <summary>
        /// This resets the object to its original form
        /// </summary>
        public void Reset()
        {
            moneyInserted = 0;
            displayAmount.DisplayAmount(moneyInserted);
        }

        /// <summary>
        /// This is goes through, determines how much change is needed for each coin and then determines if it is possible to give change. This does NOT remove change, just checks if it's possible
        /// </summary>
        /// <param name="changeNeeded">The total amount of change needed</param>
        /// <returns>Returns a bool if it is possible to give change</returns>
        private bool CalculateChangeByType(int changeNeeded)
        {
            for (int i = 3; i > - 1; i--)
            {
                if (i == 0)
                {
                    int sender = changeNeeded / 10;
                    changeNeeded -= sender * 10;
                    changeNeeded += change[i].CheckChange(sender);
                }
                else if (i == 1)
                {
                    int sender = changeNeeded / 50;
                    changeNeeded -= sender * 50;
                    changeNeeded += change[i].CheckChange(sender);
                }
                else if (i == 2)
                {
                    int sender = changeNeeded / 100;
                    changeNeeded -= sender * 100;
                    changeNeeded += change[i].CheckChange(sender);
                }
                else
                {
                    int sender = changeNeeded / 500;
                    changeNeeded -= sender * 500;
                    changeNeeded += change[i].CheckChange(sender);
                }
            }
            if (changeNeeded == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// This subtracts the change by type rather than just calculating it
        /// </summary>
        /// <param name="changeNeeded">he total amount of change needed.</param>
        private void SubtractChangeByType(int changeNeeded)
        {
            for (int i = 3; i > -1; i--)
            {
                if (i == 0)
                {
                    int sender = changeNeeded / 10;
                    changeNeeded -= sender * 10;
                    changeNeeded += change[i].SubtractChange(sender);
                }
                else if (i == 1)
                {
                    int sender = changeNeeded / 50;
                    changeNeeded -= sender * 50;
                    changeNeeded += change[i].SubtractChange(sender);
                }
                else if (i == 2)
                {
                    int sender = changeNeeded / 100;
                    changeNeeded -= sender * 100;
                    changeNeeded += change[i].SubtractChange(sender);
                }
                else
                {
                    int sender = changeNeeded / 500;
                    changeNeeded -= sender * 500;
                    changeNeeded += change[i].SubtractChange(sender);
                }
            }
        }
    }
}
