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
// Drawing Tool Form: This form allows the user to choose the 
// shape, to draw or fill, and between default or customizable 
// values for the drawing tools. It then allows the user to 
// "draw" various shapes and lines by clicking on the panel
// located on the left side of the form. A user can also
// clear their drawing or save it when they are finished.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolApp
{
    public partial class DrawingToolForm : Form
    {
        static int red, green, blue, size;//integers for color and size of pen
        static string style, shape, draw;//strings for style of brush, shape and draw type
        List<Line> drawings = new List<Line>();//List of drawn objects for DrawPanel_Paint
        Graphics g;//Graphics object for DrawPanel_Paint
        int clickCount;//integer to store how many times the user has clicked the panel

        //Form Constructor
        public DrawingToolForm()
        {
            //Set all initial values
            InitializeComponent();
            shape = draw = "";
            clickCount =  0;
            red = green = blue = 0; //black
            size = 2;
            style = "Solid";
            DrawPanel.Paint += new PaintEventHandler(DrawPanel_Paint);
        }

        //Name: SetValues
        //Purpose: Sets values in this form from the user chosen
        //values passed by the Choose Form
        //Parameters: integers for color and size of pen and
        //string for style of brush
        public void SetValues(int r, int g, int b, int bs, string st)
        {
            red = r;
            green = g;
            blue = b;
            size = bs;
            style = st;
        }

        //Name: DrawPanel_MouseDown
        //Purpose: When the user clicks on the panel, an instance
        //of the previously selected shape is created with the 
        //chosen specifications.
        private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //increments clicks
            clickCount++;

            //Depending on the shape selected, create a new instance of that 
            //shape and make a new one if enough points have been clicked
            if (shape == "Polygon")
            {
                //if drawings is not empty and the most recent object
                //that was created is a polygon, add all subsequent clicks 
                //to that polygon
                if (drawings.Count > 0 && drawings[drawings.Count - 1] is Polygon)
                {
                    drawings[drawings.Count - 1].setClicks(e.Location);
                }
                else
                {
                    //Create new polygon
                    Polygon p = new Polygon();
                    p.setPen(red, green, blue, size);
                    p.setBrush(red, green, blue, style);
                    p.setFill(draw);
                    p.setClicks(e.Location);
                    drawings.Add(p);
                }
            }
            else //If shape is a line, rectangle, or oval
            {
                //if number of clicks is odd
                if (clickCount % 2 != 0)
                {
                    if (shape == "Line")
                    {
                        //Create new line
                        Line l = new Line();
                        l.setPen(red, green, blue, size);
                        l.setClicks(e.Location);
                        drawings.Add(l);
                    }
                    else if (shape == "Rectangle" || shape == "Oval")
                    {
                        //Create new Quad
                        Quad q = new Quad();
                        q.setOval(shape);//determines if quad is 
                        q.setPen(red, green, blue, size);
                        q.setBrush(red, green, blue, style);
                        q.setFill(draw);
                        q.setClicks(e.Location);
                        drawings.Add(q);
                    }
                    else;
                }
                else //if number of clicks is even, add click to most recent shape
                {
                    if (drawings.Count > 0)
                        drawings[drawings.Count - 1].setClicks(e.Location);
                }
            }

            //Invalidate panel to be redrawn in DrawPanel_Paint
            DrawPanel.Invalidate();
        }


        //Name: DrawPanel_Paint
        //Purpose: This method paints the drawings onto the panel
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            //Set graphics object to this Panel
            g = e.Graphics;

            //Loop through all drawing and paint them on the panel
            foreach(Line d in drawings)
            {
                if (d.Clicks.Count > 1)//only draw if 2 points are recorded
                {
                    if (d is Quad)
                    {
                        Quad q = (Quad)d;
                        q.setRect();//create rectangle in Quad object
                        if (q.Oval)
                        {
                            if (q.Fill)
                                g.FillEllipse(q.Brush, q.Rect);
                            else
                                g.DrawEllipse(q.Pen, q.Rect);
                        }
                        else
                        {
                            if (q.Fill)
                                g.FillRectangle(q.Brush, q.Rect);
                            else
                                g.DrawRectangle(q.Pen, q.Rect);
                        }
                    }
                    else if (d is Polygon)
                    {
                        if (d.Clicks.Count > 2)
                        {
                            Polygon p = (Polygon)d;
                            p.setArr();//set array of points in Polygon object
                            if (p.Fill)
                                g.FillPolygon(p.Brush, p.Arr);
                            else
                                g.DrawPolygon(p.Pen, p.Arr);
                        }
                    }
                    else //if it's not the other two, its just a line
                    {
                        g.DrawLine(d.Pen, d.Clicks[0], d.Clicks[1]);
                    }
                }//end if clickCount > 1
            }//end foreach
        }

        //Name: ClearButton_Click
        //Purpose: When this button is clicked, the DrawPanel is 
        //invalidated and the drawings List is cleared
        private void ClearButton_Click(object sender, EventArgs e)
        {
            DrawPanel.Invalidate();
            drawings.Clear();
        }

        //Name: SaveButton_Click
        //Purpose: When this button is clicked, whatever has
        //been drawn on the panel is saved to the current 
        //directory as a png file
        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool worked;
            try
            { 
                //Create new image of whatever is drawn on the panel
                string ImagePath = string.Format("DrawingToolSS_{0}.png", DateTime.Now.Ticks);
                Bitmap Image = new Bitmap(640, 480);
                DrawPanel.DrawToBitmap(Image, new Rectangle(0, 0, 640, 480));
                
                //Save image to the current directory
                Image.Save(Directory.GetCurrentDirectory() + 
                    "\\" + ImagePath, System.Drawing.Imaging.ImageFormat.Png);
                worked = true;
            }
            catch
            {
                MessageBox.Show("Save image failed.", "Save Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                worked = false;
            }

            //If image was saved successfully, display to user where the image was saved
            if (worked)
            {
                MessageBox.Show("Image saved in current directory!\n" + Directory.GetCurrentDirectory(), 
                    "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Name: ColorBrush_CheckedChanged
        //Purpose: When one of these radio buttons is selected,
        //Brush and pen are either set to default values or
        //Choose Form is called and they are set to customizable
        //values
        private void ColorBrush_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == ChooseOwnRB && ChooseOwnRB.Checked == true)
            {
                //Create new instance of Choose Form
                ChooseForm cform = new ChooseForm(this);
                
                //Show choose form and hide this one
                cform.Show();
                this.Hide();
            }
            else
            {
                //Set default values
                red = green = blue = 0; //black
                size = 2;
                style = "Solid";
            }
        }

        //Name: ShapeCB_SelectedIndexChanged
        //Purpose: When this ComboBox's selected index is changed,
        //the chosen shape is stored in a string
        private void ShapeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            shape = Convert.ToString(ShapeCB.SelectedItem);

            //If shape is a line, disable Draw or Fill ComboBox
            if(shape == "Line")
            {
                DrawTypeCB.Enabled = false;
            }
            else
            {
                DrawTypeCB.Enabled = true;
            }
        }

        //Name: DrawTypeCB_SelectedIndexChanged
        //Purpose: When this ComboBox's selected index is changed,
        //the chosen draw type is stored in a string
        private void DrawTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            draw = Convert.ToString(DrawTypeCB.SelectedItem);
        }
    }
}
