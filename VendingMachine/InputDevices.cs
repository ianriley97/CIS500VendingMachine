//////////////////////////////////////////////////////////////////////
//      Vending Machine (Actuators.cs)                              //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010, 2011 //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    // For each class, you can (must) add fields and overriding constructors

    public class CoinInserter
    {
        // add a field to specify an object that CoinInserted() will firstvisit
        int positionInArray;     //I Think I will need this so when a coin is inserted, it can add one to the proper placce in the IOMan class
        InputOutputManager total;     //This is where everything will be going down.
        // rewrite the following constructor with a constructor that takes an object
        // to be set to the above field
        public CoinInserter(int p, InputOutputManager t)
        {
            positionInArray = p;
            total = t;
        }
        public CoinInserter()
        {
        }
        public void CoinInserted()
        {
            // You can add only one line here
            total.InsertCoin(positionInArray);

        }

    }

    public class PurchaseButton
    {
        // add a field to specify an object that ButtonPressed() will first visit
        int positionInArray;    //Again, I think this needs a field to point towards the proper field in the array.
        InputOutputManager total;      //This is the main connection to the controller. This is obcjously where everything is going down.

        public PurchaseButton(int p, InputOutputManager t)
        {
            positionInArray = p;
            total = t;
        }
        public PurchaseButton()
        { 
        }
        public void ButtonPressed()
        {
            // You can add only one line here
            total.CanBought(positionInArray);
        }
    }

    public class CoinReturnButton
    {
        // add a field to specify an object that Button Pressed will visit
        // replace the following default constructor with a constructor that takes
        // an object to be set to the above field
        InputOutputManager total;   //This is needed to connect this all together. The actual logic of this I will have to decide how it works later.

        public CoinReturnButton(InputOutputManager t)
        {
            total = t;
        }
        public CoinReturnButton()
        {
        }
        public void ButtonPressed()
        {
            // You can add only one lines here
            total.ReturnCoin();
        }
    }
}
