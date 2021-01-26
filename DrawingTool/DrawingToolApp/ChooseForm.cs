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
// Choose Form: This form allows the user to choose the 
// color, size, and style of the drawing tools and sends
// them back to the Drawing Tool Form.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolApp
{
    public partial class ChooseForm : Form
    {
        DrawingToolForm drawForm; //instance of a Drawing Tool Form used to reference the original form
        int red, green, blue, size; //integers for red, green and blue for the color and size for the pen
        string style; //string for the brush style

        //Form constructor
        //Parameters: reference to original Drawing Tool Form
        public ChooseForm(DrawingToolForm d)
        {
            InitializeComponent();
            drawForm = d;
            SubmitButton.Enabled = false;
        }

        //Name: SubmitButton_Click
        //Purpose: When this is clicked, the data entered by the 
        //user is checked for validity, and sent to the original
        //Drawing Tool Form
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            bool valid = false;
            try
            {
                red = Convert.ToInt32(RedTB.Text);
                green = Convert.ToInt32(GreenTB.Text);
                blue = Convert.ToInt32(BlueTB.Text);
                size = SizeTBar.Value;
                style = Convert.ToString(StyleCB.SelectedItem);
                valid = true;
            }
            catch(FormatException)
            {
                MessageBox.Show("Please enter integer values for Red, Green and " +
                    "Blue between 0-255 or choose a color from the color palette.");
            }
            if(valid)
            {
                drawForm.SetValues(red, green, blue, size, style);
                drawForm.Show();
                this.Close();
            }
        }

        //Name: PaletteButton_Click
        //Purpose: When this is clicked, a color dialog box
        //pops up, allowing the user to select a color.
        private void PaletteButton_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            RedTB.Text = Convert.ToString(colorDialog1.Color.R);
            GreenTB.Text = Convert.ToString(colorDialog1.Color.G);
            BlueTB.Text = Convert.ToString(colorDialog1.Color.B);
        }


        //Name: StyleCB_SelectedIndexChanged
        //Purpose: When an index is changed, the submit button is
        //enabled, allowing the user to return to the original
        //Drawing Tool Form
        private void StyleCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubmitButton.Enabled = true;
        }
    }
}
