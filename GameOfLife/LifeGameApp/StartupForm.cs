///////////////////////////////////////////////////////////////////////
//
// Name:            Rachel Vetter
// Professor:       Dr. Stringfellow
// Class:           CMPS 4143
// Project:         Program 5 - Game of Life!
// 
// Purpose: This program uses a Dynamically Linked Library
// called Critters to simulate the Game of Life with 
// 3 different organisms referred to as Majestics,
// Deadly Mimics and Flies.
//
// Start Up Form: This form gets the number of each organism(or actor) 
// from the user in text boxes and performs error checking to 
// ensure valid data is entered, then sends this data to a 
// newly constructed LifeGameForm to simulate gameplay.
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

namespace LifeGameApp
{
    public partial class StartupForm : Form
    {
        //Form constructor
        public StartupForm()
        {
            InitializeComponent();
        }

        //Name: StartButton_Click
        //Purpose: When this is clicked, data inputed from user is checked for
        //validity using a try/catch block. If input is valid, data is passed
        //to newly declared instance of Life Game Form to simulate gameplay.
        private void StartButton_Click(object sender, EventArgs e)
        {
            int size = 0, maj = 0, dead = 0, fly = 0, gen = 0;
            bool valid = false;

            try
            {
                size = Convert.ToInt32(GridWidthTB.Text);
                maj = Convert.ToInt32(MajesticTB.Text);
                dead = Convert.ToInt32(DeadlyMimicTB.Text);
                fly = Convert.ToInt32(FlyTB.Text);
                gen = Convert.ToInt32(GenerationTB.Text);

                if (size < 1 || size > 10)
                    throw new Exception();
                if (gen < 1)
                    throw new Exception();
                if (maj + dead + fly > size * size)
                    throw new Exception();
                valid = true;
            }
            catch
            {
                MessageBox.Show("Please enter numbers within the following ranges:\n" +
                    "Grid size: 1-10\nTotal number of actors: less than " 
                    + (size < 11 ? Convert.ToString(size*size) : "grid size squared") 
                    + "\nGenerations: greater than 0");
                valid = false;
            }
            if (valid)
            {
                this.Hide();
                LifeGameForm form = new LifeGameForm(this, size, maj, dead, fly, gen);
                form.Show();
            }
        }
    }
}
