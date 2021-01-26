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
// Login Form: This user control dll allows the user to login 
// to any app with their login name and password, which is 
// hidden with astericks as they type.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class LoginPswdUserControl : UserControl
    {
        //Login Property
        public string Login { get; set; }
        //Password Property
        public string Password { get; set; }

        //Default constructor
        public LoginPswdUserControl()
        {
            InitializeComponent();
        }

        //Name: LoginTB_TextChanged
        //Purpose: Sets Login property to the Login Text Box
        //text
        private void LoginTB_TextChanged(object sender, EventArgs e)
        {
            Login = LoginTB.Text;
        }

        //Name: PasswordTB_TextChanged
        //Purpose: Sets Password property to the Password Text Box
        //text
        private void PasswordTB_TextChanged(object sender, EventArgs e)
        {
            Password = PasswordTB.Text;
        }
    }
}
