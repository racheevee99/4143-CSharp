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
// Parent Form: This form allows the user to create new child forms
// with or without existing data from a serialized file. It also
// allows the user to save the data, insert new data, and delete 
// or update existing data.
//
///////////////////////////////////////////////////////////////////////

using System;
using Microsoft.VisualBasic;
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
    public partial class ParentForm : Form
    {
        //Default Constructor
        public ParentForm()
        {
            InitializeComponent();
            MenuMS.Hide();
        }

        //Name: LoginButton_Click
        //Purpose: Shows menustrip if the user has entered
        //some value into both the Login and Password
        //text boxes from the user control
        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (LPUC.Login != "" && LPUC.Password != "")
            {
                LPUC.Hide();
                LoginButton.Hide();
                MenuMS.Show();
            }
        }

        //Name: newToolStripMenuItem_Click
        //Purpose: Creates a new child form with default
        //values
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm cf = new ChildForm();
            cf.MdiParent = this;
            cf.Show();
        }

        //Name: openToolStripMenuItem_Click
        //Purpose: Creates a new child form with parameterized
        //constructor that takes the path of a file that
        //the user chooses from OpenFileDialog
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog OFD = new OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open text file"
            };
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = OFD.FileName;
                ChildForm cf = new ChildForm(path);
                cf.MdiParent = this;
                cf.Show();
            }
            else
            {
                MessageBox.Show("Could not read file.");
            }
        }

        //Name: saveToolStripMenuItem_Click
        //Purpose: Saves the active child form in a
        //serializable file. If one does not exist for 
        //this form, the user may create one using the
        //SaveFileDialog
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm cf = (ChildForm)this.ActiveMdiChild;
            if (cf.Path == "")
            {
                SaveFileDialog SFD = new SaveFileDialog()
                {
                    Filter = "Text files (*.txt)|*.txt",
                    Title = "Create text file"
                };
                if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    cf.Path = SFD.FileName;
                    cf.Save();
                    cf = new ChildForm(SFD.FileName);
                    cf.MdiParent = this;
                    cf.Show();
                }
            }
            else
            {
                cf.Save();
                cf = new ChildForm(cf.Path);
                cf.MdiParent = this;
                cf.Show();
            }
        }

        //Name: exitToolStripMenuItem_Click
        //Purpose: Closes the active child form
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
        }

        //Name: insertToolStripMenuItem_Click
        //Purpose: Inserts a new item into the inventory list
        //of the active child form
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm inf = new InputForm();
            inf.ShowDialog();
            if (inf.Valid)
            {
                ChildForm cf = (ChildForm)this.ActiveMdiChild;
                cf.Insert(inf.ItemName, inf.Cost, inf.Quantity);
            }
        }

        //Name: deleteToolStripMenuItem_Click
        //Purpose: Deletes an item from the inventory list
        //of the active child form
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm cf = (ChildForm)this.ActiveMdiChild;
            cf.Delete();
        }

        //Name: updateToolStripMenuItem_Click
        //Purpose: Updates an item from the inventory list
        //of the active child form
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm cf = (ChildForm)this.ActiveMdiChild;
            cf.UpdateItem();
        }

        //Name: viewAboutToolStripMenuItem_Click
        //Purpose: Displays a message that states information
        //about this program.
        private void viewAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program was written by Rachel Vetter at MSU Texas.",
                "About this program", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
