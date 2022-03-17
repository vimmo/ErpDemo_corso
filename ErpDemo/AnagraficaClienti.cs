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
using static ErpDemo.RicercaBase;

namespace ErpDemo
{
    public partial class AnagraficaClienti : AnagraficaBase
    {
        private readonly DBClientiService _db;
        private readonly DBUtentiService _dbUtenti;
        public Utenti UserAuthenticated { get; set; }
        public AnagraficaClienti()
        {
            InitializeComponent();
            txtId.Enabled = false;
            CambiaStatoCampi(DOCUMENT_MODE != _DOC_MODE.BROWSE);

            _db = new DBClientiService();
            _dbUtenti = new DBUtentiService();
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
            CURRENT_ID = cliente.Id;
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
        public override bool OnEdit()
        {
            string utente = _dbUtenti.DocumentoBloccato("CLI", CURRENT_ID);
            if(utente != "")
            {
                MessageBox.Show("Documento impegnato dall'utente: " + utente, 
                    "Documento bloccato", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);
                return false;

            }
            else
            {
                _dbUtenti.BloccaDocumento(UserAuthenticated.username, "CLI", CURRENT_ID);
                CambiaStatoCampi(DOCUMENT_MODE != _DOC_MODE.BROWSE);
                return true;
            }
        }
        public override bool OnDelete()
        {

            if (MessageBox.Show("Confermi eliminazione?",
                "Elimina",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return false;
  
            var cliente = new Clienti
            {
                Id = Convert.ToInt32(txtId.Text),
                RagioneSociale = txtRagioneSociale.Text,
                Indirizzo = txtIndirizzo.Text,
                Citta = txtCitta.Text,
                Settore = txtSettore.Text
            };
            bool bOk = _db.EliminaCliente(cliente);
            
            if(!bOk)
            {
                MessageBox.Show(
                      "Errore in salvataggio",
                      "Errore",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
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
            {
                CambiaStatoCampi(false);
                _dbUtenti.SbloccaDocumento(UserAuthenticated.username,
                    "CLI", CURRENT_ID);
            }
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
            _dbUtenti.SbloccaDocumento(UserAuthenticated.username,
            "CLI", CURRENT_ID);

            return true;
        }

        public override void OnFirst()
        {
            RiempiCampi(_db.LeggiCliente(0, false));
        }
        public override void OnLast()
        {
            RiempiCampi(_db.LeggiCliente(0, true));
        }
        public override void OnBack()
        {
            RiempiCampi(_db.LeggiCliente(CURRENT_ID, false));
        }
        public override void OnNext()
        {
            RiempiCampi(_db.LeggiCliente(CURRENT_ID, true));
        }
        public override void OnFind()
        {
            Form[] lista = ParentForm.MdiChildren;
            bool canOpen = true;
            foreach (Form child in lista)
            {
                if (child.Name == "RicercaBase")
                    canOpen = false;
            }
            if (canOpen)
            {
                DataTable BODY_RADAR = new DataTable();
                BODY_RADAR.Columns.Add("ID", typeof(int));
                BODY_RADAR.Columns.Add("Ragione sociale", typeof(string));
                BODY_RADAR.Columns.Add("Indirizzo", typeof(string));
                BODY_RADAR.Columns.Add("Città", typeof(string));
                BODY_RADAR.Columns.Add("Settore", typeof(string));

                foreach (Clienti cliente in _db.LeggiListaClienti().ToList())
                {
                    BODY_RADAR.Rows.Add(
                        cliente.Id,
                        cliente.RagioneSociale,
                        cliente.Indirizzo,
                        cliente.Citta,
                        cliente.Settore);
                }

                RicercaBase DOCUMENT_RADAR = new RicercaBase(BODY_RADAR);
                DOCUMENT_RADAR.MdiParent = ParentForm;

                DOCUMENT_RADAR.RigaSelezionata += new EventHandler<RicercaBase.RicercaEventArgs>(OnSelezionata);
                DOCUMENT_RADAR.Text = "Ricerca Clienti";
                DOCUMENT_RADAR.Show();
            }
        }


        public void OnSelezionata(object sender, RicercaEventArgs e)
        {
            RiempiCampi(_db.LeggiCliente(e.IdSelezionato));
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
