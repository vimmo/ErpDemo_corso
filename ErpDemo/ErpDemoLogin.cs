using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErpDemoEF.Models;
using ErpDemoEF.Services;

namespace ErpDemo
{
    public partial class ErpDemoLogin : Form
    {
        private readonly DBUtentiService _db;
        public ErpDemoLogin()
        {
            InitializeComponent();
            _db = new DBUtentiService();
            lblLoginError.Visible = false;
        }

        public Utenti UserAuthenticated { get; internal set; }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Utenti logged = _db.Login(new Utenti { username = txtUser.Text, password = txtPassword.Text });

            if (logged != null)
            {
                UserAuthenticated = logged;
                Close();
            }
            else
            {
                lblLoginError.Visible = true;
                txtUser.Text = "";
                txtPassword.Text = "";
            }
        }

    }
}
