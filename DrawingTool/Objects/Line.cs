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
// Line: This is the base class.
// This class includes two variables: a pen and a List of
// Points that all shapes inherit from. Methods include: a 
// default constructor, and getters and setters for the
// class variables.
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
    public class Line 
    {
        Pen p; //Pen for line
        public List<Point> cl = new List<Point>(); //List of points for a line

        //Default constructor
        public Line() { }

        //Getter for Clicks property
        public List<Point> Clicks{get => cl;}

        //Setter for Clicks property
        //Parameters: a location from the form
        public virtual void setClicks(Point p)
        { 
            //only record two locations for the line
            if(cl.Count < 2)
                cl.Add(p);
        }

        //Getter for Pen property
        public Pen Pen { get => p; }

        //Setter for Pen property
        //Parameters: integers for the color and size of the pen
        public void setPen(int r, int g, int b, int s)
        {
            p = new Pen(Color.FromArgb(r, g, b), s);
        }
    }
}
