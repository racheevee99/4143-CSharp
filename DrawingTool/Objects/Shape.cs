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
// Shape: This class inherits from Line but is parent to
// the Quad and Polygon classes.
// This class includes two variables: a brush and a boolean
// variable that determines whether the shape is being
// drawn or filled. Methods include: getters and setters for the
// class variables.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolApp
{
    public class Shape : Line
    {
        Brush br; //brush for filling shape
        bool fill; //boolean to determine whether the shape is drawn or filled

        //Getter for Fill property
        public bool Fill { get => fill; }

        //Setter for Fill Property
        //Parameters: string from form(either "Draw" or "Fill")
        public void setFill(string f)
        {
            if(f == "Fill")
                fill = true;
            else
                fill = false;
        }

        //Getter for Brush Property
        public Brush Brush { get => br; }

        //Setter for Brush Property
        //Parameters: integers for color of brush and string for style
        //(either "Solid", "Texture", or "Hatch")
        public void setBrush(int r, int g, int b, string st)
        {
            if (st == "Texture")
                br = new TextureBrush(Properties.Resources.bubbles);
            else if (st == "Hatch")
                br = new HatchBrush(HatchStyle.BackwardDiagonal, Color.FromArgb(r, g, b));
            else
                br = new SolidBrush(Color.FromArgb(r, g, b));
        }
    }
}
