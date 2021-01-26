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
// Quad(as in quadrilateral): This class inherits from Shape, 
// which inherits from Line.
// This class includes two variables: a Rectangle and a boolean
// variable that determines whether the shape is an oval or a
// rectangle. Methods include: a default constructor, and 
// getters and setters for the class variables.
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
    public class Quad : Shape
    {
        Rectangle r; //Rectangle for a rectangle or oval
        bool oval; //boolean to determine if its a rectangle or oval

        //Default Constructor
        public Quad() { }

        //Getter for Oval Property
        public bool Oval { get => oval; }

        //Setter for Oval Property
        //Parameters: string from form(either "Oval" or "Rectangle")
        public void setOval(string o)
        {
            if (o == "Oval")
                oval = true;
            else
                oval = false;
        }

        //Getter for Rect Property
        public Rectangle Rect {get => r;}

        //Setter for Rect Property
        //Parameters: none
        public void setRect()
        {
            if (Clicks.Count > 1)
            {
                //Sets points to first to points int the Clicks List
                Point xy1 = Clicks[0];
                Point xy2 = Clicks[1];

                //Finds width and height by calculating the difference
                //between the points and taking their absolute value
                int width = Math.Abs(xy1.X - xy2.X);
                int height = Math.Abs(xy1.Y - xy2.Y);

                //Sets initial position to top left point
                if (xy1.X > xy2.X)
                {
                    xy1.X = xy2.X;
                }
                if (xy1.Y > xy2.Y)
                {
                    xy1.Y = xy2.Y;
                }

                //Sets r to new rectangle with previous dimensions and coordinates
                r = new Rectangle(xy1.X, xy1.Y, width, height);
            }
        }
    }
}
