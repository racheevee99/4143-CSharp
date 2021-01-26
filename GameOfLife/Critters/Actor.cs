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
// Actor: This is an abstract parent class.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Critters
{
    public abstract class Actor
    {
        //Abstract Properties for Age and X and Y coordinates
        public abstract int Age{ get; set;}
        public abstract int X{ get; set;}
        public abstract int Y{ get; set;}
    }
}
