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
    public partial class AnagraficaClienti : AnagraficaBase
    {
        private readonly DBClientiService _db;
        public AnagraficaClienti()
        {
            InitializeComponent();
            txtId.Enabled = false;
            CambiaStatoCampi(DOCUMENT_MODE != _DOC_MODE.BROWSE);

            _db = new DBClientiService();
        }

        private void CambiaStatoCampi(bool setEnabled)
        {
            txtRagioneSociale.Enabled = setEnabled;
            txtIndirizzo.Enabled = setEnabled;
            txtCitta.Enabled = setEnabled;
            txtSettore.Enabled = setEnabled;
        }
        private void PulisciCampi()
        {
            txtId.Clear();
            txtRagioneSociale.Clear();
            txtIndirizzo.Clear();
            txtCitta.Clear();
            txtSettore.Clear();
            lblRagSoc.ForeColor = Color.Black;
        }

        private void RiempiCampi(Clienti cliente)
        {
            txtId.Text = cliente.Id.ToString();
            txtRagioneSociale.Text = cliente.RagioneSociale;
            txtIndirizzo.Text = cliente.Indirizzo;
            txtCitta.Text = cliente.Citta;
            txtSettore.Text = cliente.Settore;
        }

        public override void OnNew()
        {
            CambiaStatoCampi(DOCUMENT_MODE != _DOC_MODE.BROWSE);
            PulisciCampi();
        }
        public override void OnEdit()
        {
            CambiaStatoCampi(DOCUMENT_MODE != _DOC_MODE.BROWSE);
        }
        public override bool OnDelete()
        {
            bool bOk = true;
            if (MessageBox.Show("Confermi eliminazione?",
                "Elimina",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation) == DialogResult.OK)
                bOk = true;
            else
                bOk = false;

            if(bOk)
            {
                var cliente = new Clienti
                {
                    Id = Convert.ToInt32(txtId.Text),
                    RagioneSociale = txtRagioneSociale.Text,
                    Indirizzo = txtIndirizzo.Text,
                    Citta = txtCitta.Text,
                    Settore = txtSettore.Text
                };
                bOk = _db.EliminaCliente(cliente);
            }

            return bOk;
        }
        public override bool OnSave()
        {

            if (txtRagioneSociale.Text == "")
            {
                MessageBox.Show("Ragione sociale obbligatoria");
                txtRagioneSociale.Focus();
                lblRagSoc.ForeColor = Color.Red;
                return false;
            }

            if (MessageBox.Show("Salvare modifiche?",
                "Salva",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.Cancel)
                return false;
            
            var cliente = new Clienti
            {
                Id = DOCUMENT_MODE == _DOC_MODE.NEW ? 0 : Convert.ToInt32(txtId.Text),
                RagioneSociale = txtRagioneSociale.Text,
                Indirizzo = txtIndirizzo.Text,
                Citta = txtCitta.Text,
                Settore = txtSettore.Text
            };

            bool bOk;
            if (DOCUMENT_MODE == _DOC_MODE.NEW)
            {
                Clienti cli = _db.CreaCliente(cliente);
                txtId.Text = cli.Id.ToString();
                bOk = txtId.Text != "";
            }
            else
            {
                bOk = _db.ModificaCliente(cliente);
            }




            if (bOk)
                CambiaStatoCampi(false);
            else
                MessageBox.Show(
                    "Errore in " + (DOCUMENT_MODE == _DOC_MODE.NEW ? "inserimento" : "modifica"),
                    "Errore",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

            return bOk;
        }
        public override bool OnCancel()
        {
            if (MessageBox.Show("Annullare modifiche?",
                "Annulla",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return false;

           
            CambiaStatoCampi(false);
            PulisciCampi();
            
            return true;
        }

        public override void OnFirst()
        {
            RiempiCampi(_db.LeggiCliente(false));
        }
        public override void OnLast()
        {
            RiempiCampi(_db.LeggiCliente(true));
        }




        private void txtRagioneSociale_Leave(object sender, EventArgs e)
        {
            if (txtRagioneSociale.Text == "")
                lblRagSoc.ForeColor = Color.Red;
            else
                lblRagSoc.ForeColor = Color.Black;

        }
    }
}
