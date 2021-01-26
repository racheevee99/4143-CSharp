///////////////////////////////////////////////////////////////////////
//
// Name:            Rachel Vetter
// Professor:       Dr. Stringfellow
// Class:           CMPS 4143
// Project:         Program 7 - Inventory Program
// 
// Purpose: This program is an application that uses an MDI to 
// display inventory information from different businesses in 
// separate child form windows. This inventory information
// can be written in manually through the app or read
// directly to a serialized file and is then written to 
// a serialized file.
//
// Input Form: This form allows the user to enter relevant data
// about the current inventory of a business, such as the items
// currently in stock, their cost and the number of total units.
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

namespace Program7App
{
    public partial class InputForm : Form
    {
        //ItemName Property
        public string ItemName { get; set; }
        //Cost Property
        public double Cost { get; set; }
        //Quantity Property
        public int Quantity { get; set; }
        //Valid Property
        public bool Valid { get; set; }
        
        //Default Constructor
        public InputForm()
        {
            InitializeComponent();
        }

        //Parameterized Constructor
        //Parameters: name of item as a string, cost as
        //a double, quantity as an integer
        public InputForm(string name, double cost, int quan)
        {
            InitializeComponent();
            NameTB.Text = name;
            CostTB.Text = cost.ToString();
            QuanTB.Text = quan.ToString();
        }

        //Name: OkButton_Click
        //Purpose: validates the information from the user
        //and sets the properties to the given values.
        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                ItemName = NameTB.Text;
                Cost = Convert.ToDouble(CostTB.Text);
                Quantity = Convert.ToInt32(QuanTB.Text);
                Valid = true;
            }
            catch(FormatException)
            {
                MessageBox.Show("Please enter valid data.", "Data Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Valid = false;
            }
            if(Valid)
            {
                this.Close();
            }
        }

        //Name: CancButton_Click
        //Purpose: Closes the form and does not allow
        //null values to be used
        private void CancButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Valid = false;
        }
    }
}
