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
    public enum _DOC_MODE { NEW, EDIT, BROWSE };
    public enum _DOC_TYPE { CLIENTI, ARTICOLI, MOVIMENTI };
    
    public partial class ErpDemo : Form
    {
        public Utenti CURR_USR;
        public ErpDemo(Utenti utente)
        {
            InitializeComponent();
            CURR_USR = utente;
            toolStripStatusLabelUtente.Text = "Utente: " + CURR_USR.username;
        }

        private void OnLoad(object sender, EventArgs e)
        {
        }

        private void clientiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanOpenForm("AnagraficaClienti"))
            {
                AnagraficaClienti cli = new AnagraficaClienti();
                cli.MdiParent = this;
                cli.UserAuthenticated = CURR_USR;
                cli.Show();
            }
        }

        private void articoliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanOpenForm("AnagraficaArticoli"))
            {
                AnagraficaArticoli art = new AnagraficaArticoli();
                art.MdiParent = this;
                art.Show();
            }
        }
        private bool CanOpenForm(string formName)
        {
            bool canOpen = true;
            Form[] schede = this.MdiChildren;
            foreach (Form elem in schede)
            {
                if (elem.Name == formName)
                    canOpen = false;
            }
            return canOpen;
        }

        private void ErpDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form[] schede = this.MdiChildren;
            foreach (AnagraficaBase elem in schede)
            {
                if (elem.DOCUMENT_MODE != _DOC_MODE.BROWSE)
                {
                    MessageBox.Show("Documento aperto impossibile chiudere.");
                    e.Cancel = true;
                    break;
                }
            }

            if(e.Cancel == false)
            {
                DBUtentiService _dbService = new DBUtentiService();
                _dbService.EliminaSessione(CURR_USR.username);
            }
        }
    }
}
