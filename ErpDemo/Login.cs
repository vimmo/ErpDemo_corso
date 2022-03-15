using ErpDemoEF.Models;
using ErpDemoEF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErpDemo
{
    public partial class Login : Form
    {
        public Utenti UTENTE_LOGGATO;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DBUtentiService _dbService = new DBUtentiService();

            UTENTE_LOGGATO = _dbService.Login(txtUtente.Text, txtPassword.Text);     
            
            if (UTENTE_LOGGATO != null)
            {
                if (_dbService.SessioneAttiva(UTENTE_LOGGATO.username) != null)
                {
                    lblMessage.Text = "UTENTE GIA' LOGGATO";
                    lblMessage.Visible = true;
                }
                else
                {
                    _dbService.CreaSessione(UTENTE_LOGGATO.username);
                    Close();
                }
            }
            else
                lblMessage.Visible = true;
            
        }
    }
}
