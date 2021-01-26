///////////////////////////////////////////////////////////////////////
//
// Name:            Rachel Vetter
// Professor:       Dr. Stringfellow
// Class:           CMPS 4143
// Project:         Program 5 - Game of Life!
// 
// Purpose: This program is a Dynamically Linked Library
// called Critters that can be used to simulate the Game of  
// Life with 3 different organisms(or actors) referred to as 
// Majestics, Deadly Mimics and Flies.
//
// Deadly Mimic: This class inherits from the Actor class.
// It overrides its parent's Age, X, and Y properties, and
// implements the grow, getEat, and meal methods.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Critters
{
    public class DeadlyMimic : Actor
    {
        int age, x, y, eat;

        //Parameterized constructor
        public DeadlyMimic(int a, int b)
        {
            x = a;
            y = b;
            age = 0;
            eat = 5;
        }

        //Age Property - get & set
        public override int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }

        }

        //X Property - get & set
        public override int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        //Y Property - get & set
        public override int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        //Name: grow
        //Returns: none
        //Parameters: none
        public void grow()
        {
                age++;
        }

        //Name: getEat
        //Returns: eat
        //Parameters: none
        public int getEat()
        {
            return eat;
        }

        //Name: meal
        //Returns: none
        //Parameters: boolean value indicating whether this has eaten
        public void meal(bool ate)
        {
            if (!ate && eat > 0)
                eat--;
        }
    }
}