///////////////////////////////////////////////////////////////////////
//
// Name:            Rachel Vetter
// Professor:       Dr. Stringfellow
// Class:           CMPS 4143
// Project:         Program 6 - Drawing Tool
// 
// Purpose: This program is an application that uses draw and
// fill methods on various shapes, defined in classes, to 
// simulate a painting program.
//
// Polygon: This class inherits from Shape, 
// which inherits from Line.
// This class includes one variable: a Point array. Methods 
// include: a default constructor, a getter and setter 
// for the class variable, and overrides the setter for the
// List of Points data structure in the Line base class.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolApp
{
    public class Polygon : Shape
    {
        Point[] clArr; //array of points

        //Default constructor
        public Polygon() { }

        //Override for setter for Clicks Property
        public override void setClicks(Point p)
        {
            cl.Add(p);
        }

        //Getter for Arr Property
        public Point[] Arr{get => clArr;}

        //Setter for Arr Property
        //Parameters: none
        public void setArr()
        {
            //set array to the Clicks List
            clArr = Clicks.ToArray();
        }
    }
}
