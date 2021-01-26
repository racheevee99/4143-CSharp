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
// Life Game Form: This form generates the game board and displays the 
// current generation(or day) and number of each surviving type 
// of organism. The game can progress a day at a time or skip
// to the end of the total number of days and display the result
// in the grid. At the end of the game the user can choose to 
// play again with new inputs or simply end the game.
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
using Critters;

namespace LifeGameApp
{
    public partial class LifeGameForm : Form
    {
        //Form variable declarations
        StartupForm start;//instance of start up form
        Random rand = new Random();//variable for random number generation
        PictureBox[,] actors;//2D array of picture boxes
        Actor[] critters;//array of critters from dll
        //size of grid, number of majestics, deadly mimics and flies, 
        //total generations, and variable to keep track of the current generation
        static int size, maj, dead, fly, gen, Cgen;
        //variable to set the size of the image
        static int imageSize;

        //Form constructor
        //Parameters: reference to original start up form, size from user
        //number of majestics, deadly mimics and flies from user, and
        //number of total generations from user
        public LifeGameForm(StartupForm sf, int s, int m, int d, int f, int g)
        {
            InitializeComponent();
            PlayButton.Hide();
            EndButton.Hide();
            start = sf;
            size = s;
            maj = m;
            dead = d;
            fly = f;
            gen = g;
            NumMajestics.Text = Convert.ToString(maj);
            NumDeadlyMimics.Text = Convert.ToString(dead);
            NumFlies.Text = Convert.ToString(fly);
            imageSize = 120 - (8 * size);
            Cgen = 1;

            //Initialize array of critters
            critters = new Actor[maj + dead + fly];
            int index = 0;

            actors = new PictureBox[size, size];
            //keep track of how many pictures have been placed of each type
            int act = 4, zero = 0, one = 0, two = 0, three = 0;
            //Key:
            //zero(0): no picture
            //one(1): majestic picture
            //two(2): deadly mimic picture
            //three(3): fly picture


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    //while loop to get a valid random actor that hasn't been used up yet
                    bool valid = false;
                    while (!valid)
                    {
                        act = rand.Next(4); //randomly chooses an actor to be placed
                        if (act == 0 && zero < (size * size) - (maj + dead + fly))
                        {
                            valid = true;
                            zero++;
                        }
                        else if (act == 1 && one < maj)
                        {
                            valid = true;
                            one++;
                        }
                        else if (act == 2 && two < dead)
                        {
                            valid = true;
                            two++;
                        }
                        else if (act == 3 && three < fly)
                        {
                            valid = true;
                            three++;
                        }
                        else
                            valid = false;
                    }

                    //switch to initialize actors and picture boxes
                    switch (act)
                    {
                    case 0:
                    {
                        //Initialize transparent picture box
                        actors[i, j] = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.Transparent,
                            Size = new Size(imageSize, imageSize),
                            Anchor = AnchorStyles.Left,
                            Image = null,
                            Location = new Point(150 + (i * (imageSize + 5)), 100 + (j * (imageSize + 5))),
                            Visible = true
                        };
                    break;
                    }
                    case 1:
                    {
                        //Initialize Majestic
                        critters[index] = new Majestic(i, j);
                        index++;

                        //Initialize picture box
                        actors[i, j] = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.Aqua,
                            Size = new Size(imageSize, imageSize),
                            Anchor = AnchorStyles.Left,
                            Image = global::LifeGameApp.Properties.Resources.Mseed,
                            Location = new Point(150 + (i * (imageSize + 5)), 100 + (j * (imageSize + 5))),
                            Visible = true
                        };
                    break;
                    }
                    case 2:
                    {
                        //Initialize DeadlyMimic
                        critters[index] = new DeadlyMimic(i, j);
                        index++;

                        //Initialize picture box
                        actors[i, j] = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.Aqua,
                            Size = new Size(imageSize, imageSize),
                            Anchor = AnchorStyles.Left,
                            Image = global::LifeGameApp.Properties.Resources.DMseed,
                            Location = new Point(150 + (i * (imageSize + 5)), 100 + (j * (imageSize + 5))),
                            Visible = true
                        };
                    break;
                    }
                    case 3:
                    {
                        //Initialize Fly
                        critters[index] = new Fly(i, j);
                        index++;

                        //Initialize picture box
                        actors[i, j] = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.Aqua,
                            Size = new Size(imageSize, imageSize),
                            Anchor = AnchorStyles.Left,
                            Image = global::LifeGameApp.Properties.Resources.Fly,
                            Location = new Point(150 + (i * (imageSize + 5)), 100 + (j * (imageSize + 5))),
                            Visible = true
                        };
                    break;
                    }
                    case 4:
                    {
                        MessageBox.Show("Picture not loaded.");
                        break;
                    }
                    }//end switch
                    this.Controls.Add(actors[i, j]);
                }//end inner for loop
            }//end outer for loop

            CurrentGenLabel.Text = Convert.ToString(Cgen);
            if(Cgen == gen)
            {
                NextButton.Enabled = false;
                SkipButton.Enabled = false;
                PlayButton.Show();
                EndButton.Show();
            }
        }//end constructor

        //Name: SkipButton_Click
        //Purpose: When this is clicked, the NextButton_Click method
        //is called until generations have passed
        private void SkipButton_Click(object sender, EventArgs e)
        {
            while(Cgen < gen)
            {
                NextButton_Click(sender, e);
            }
        }

        //Name: NextButton_Click
        //Purpose: When this is clicked, one generation passes and the
        //gameplay for one day is simulated
        private void NextButton_Click(object sender, EventArgs e)
        {
            bool ate = false;
            //Display current generation
            Cgen++;
            CurrentGenLabel.Text = Convert.ToString(Cgen);
            if (Cgen <= gen)
            {
                //Loop through and move flies
                foreach (Actor a in critters)
                {
                    //Flies move and eat
                    if (a is Fly)
                    {
                        //Set current position's picture to null
                        actors[a.X, a.Y].Image = null;
                        actors[a.X, a.Y].BackColor = Color.Transparent;

                        Fly f = (Fly)a;
                        if (f.getEat() > 0)//if dead, skip
                        {
                            bool moved = false;
                            int x = f.X, y = f.Y;
                            int stuck = 0;
                            while (!moved)
                            {
                                //randomly choose to change x or y, (axis of movement)
                                int axis = rand.Next(2);//0 = x, 1 = y
                                //randomly choose to go up or down(right or left) (direction of movement)
                                int dir = rand.Next(2);//0 = down/left(--), 1 = up/right(++)
                                //temporary variables to hold current position until actually moved
                                x = f.X; 
                                y = f.Y;
                                //increment stuck, if all positions surrounding fly are other flies, stay there
                                stuck++;

                                switch (axis)
                                {
                                    case 0://move along X axis
                                        {
                                            if (f.X < size - 1 && f.X > 0)
                                            {
                                                if (dir == 1)
                                                    x += 1;
                                                else
                                                    x -= 1;
                                            }
                                            else if (f.X == size - 1)
                                                x -= 1;
                                            else
                                                x += 1;
                                            break;
                                        }
                                    case 1://move along Y axis
                                        {
                                            if (f.Y < size - 1 && f.Y > 0)
                                            {
                                                if (dir == 1)
                                                    y += 1;
                                                else
                                                    y -= 1;
                                            }
                                            else if (f.Y == size - 1)
                                                y -= 1;
                                            else
                                                y += 1;
                                            break;
                                        }
                                }

                                //Check that new position isnt already occupied by another fly
                                int count = 0;
                                foreach (Actor b in critters)
                                {
                                    if (b.X == x && b.Y == y && b is Fly)
                                        count++;
                                }

                                if (count < 1)
                                    moved = true;
                                if (stuck > 8)//to make sure its stuck haha
                                {
                                    moved = true;
                                    x = f.X;
                                    y = f.Y;
                                }
                            }//end while

                            //reset positions
                            f.X = x;
                            f.Y = y;
                            //If fly eats, live another day, else die a little
                            ate = false;
                            foreach (Actor b in critters)
                            {
                                if (b.X == f.X && b.Y == f.Y && b is Majestic)
                                    ate = true;
                            }
                            f.meal(ate);

                            //Change Picture
                            if (f.getEat() > 0)
                                actors[f.X, f.Y].Image = global::LifeGameApp.Properties.Resources.Fly;
                            else//dead now, set picture to null
                            {
                                actors[a.X, a.Y].Image = null;
                                actors[a.X, a.Y].BackColor = Color.Transparent;
                            }
                        }
                    }//end fly if statement
                }

                //Loop through all plants after flies have been moved
                foreach (Actor a in critters)
                {
                    //Majestics Grow and their pictures are set
                    if (a is Majestic)
                    {
                        Majestic m = (Majestic)a;
                        m.grow();

                        //check if flower is being eaten
                        ate = false;
                        foreach (Actor b in critters)
                        {
                            if (b.X == m.X && b.Y == m.Y && b is Fly)
                                ate = true;
                        }

                        //Change Picture
                        if (ate)//if flower is currently being eaten, set picture to fly eating
                            actors[a.X, a.Y].Image = global::LifeGameApp.Properties.Resources.FlyEat;
                        else
                        {
                            if (a.Age == 1)
                            {
                                actors[a.X, a.Y].Image = global::LifeGameApp.Properties.Resources.Mbaby;
                            }
                            else
                            {
                                actors[a.X, a.Y].Image = global::LifeGameApp.Properties.Resources.MFull;
                            }
                        }
                    }

                    if (a is DeadlyMimic)
                    {
                        DeadlyMimic d = (DeadlyMimic)a;
                        d.grow();

                        //check if deadly mimic is eating
                        ate = false;
                        foreach (Actor b in critters)
                        {
                            if (b.X == d.X && b.Y == d.Y && b is Fly)
                            {
                                Fly f = (Fly)b;
                                if(f.getEat() > 0)
                                    ate = true;
                                //Kill fly
                                while(f.getEat()>0)
                                {
                                    f.meal(false);
                                }
                            }
                        }
                        d.meal(ate);

                        if (d.getEat() > 0)//if dead, skip
                        {
                            //Change Picture
                            if (ate)
                                actors[d.X, d.Y].Image = global::LifeGameApp.Properties.Resources.DMEat;
                            else
                            {
                                if (a.Age == 1)
                                {
                                    actors[a.X, a.Y].Image = global::LifeGameApp.Properties.Resources.DMbaby;
                                }
                                else
                                {
                                    actors[a.X, a.Y].Image = global::LifeGameApp.Properties.Resources.DMFull;
                                }
                            }
                        }
                        else
                        {
                            actors[a.X, a.Y].Image = null;
                            actors[a.X, a.Y].BackColor = Color.Transparent;
                        }
                    }
                }//end outer foreach loop

                //Loop to count how many flies and deadly mimics are left
                int fcount = 0, dcount = 0;
                foreach (Actor a in critters)
                {
                    if (a is Fly)
                    {
                        Fly f = (Fly)a;
                        if (f.getEat() == 0)
                        {
                            fcount++;
                        }
                    }
                    if (a is DeadlyMimic)
                    {
                        DeadlyMimic d = (DeadlyMimic)a;
                        if (d.getEat() == 0)
                        {
                            dcount++;
                        }
                    }
                }
                //Display current counts of Deadly Mimics and flies
                NumDeadlyMimics.Text = Convert.ToString(dead - dcount);
                NumFlies.Text = Convert.ToString(fly - fcount);
            }
            if(Cgen == gen)//generations completed, game is over
            {
                NextButton.Enabled = false;
                SkipButton.Enabled = false;
                PlayButton.Show();
                EndButton.Show();
            }
        }//end NextButton_Click

        //Name: EndButton_Click
        //Purpose: When this is clicked, game is ended and original
        //start up form is closed, which closes this form, as well.
        private void EndButton_Click(object sender, EventArgs e)
        {
            start.Close();
        }

        //Name: PlayButton_Click
        //Purpose: When this is clicked, game is reloaded. This
        //form is closed and the original start up form is shown
        //again so the user can simulate the same game again, or 
        //change the values and start a new game.
        private void PlayButton_Click(object sender, EventArgs e)
        {
            this.Close();
            start.Refresh();
            start.Show();
        }
    }
}
