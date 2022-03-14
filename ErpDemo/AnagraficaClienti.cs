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
using static ErpDemo.Ricerca;

namespace ErpDemo
{
    public partial class AnagraficaClienti : AnagraficaBase
    {
        private readonly DBClientiService _db;
        public int CURR_ID = 0;
        public AnagraficaClienti()
        {
            InitializeComponent();
            txtId.Enabled = false;
            CambiaStatoCampi(DOCUMENT_MODE != _DOC_MODE.BROWSE);

            _db = new DBClientiService();

            BODY_RADAR = new DataTable();
            BODY_RADAR.Columns.Clear();
            BODY_RADAR.Columns.Add("ID", typeof(int));
            BODY_RADAR.Columns.Add("Ragione sociale", typeof(string));
            BODY_RADAR.Columns.Add("Indirizzo", typeof(string));
            BODY_RADAR.Columns.Add("Città", typeof(string));
            BODY_RADAR.Columns.Add("Settore", typeof(string));
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
            CURR_ID = cliente.Id;
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
            bool  bOk = _db.EliminaCliente(cliente);
            if (!bOk)
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
            RiempiCampi(_db.LeggiCliente(0,false));
        }
        public override void OnLast()
        {
            RiempiCampi(_db.LeggiCliente(0,true));
        }

        public override void OnNext()
        {
            RiempiCampi(_db.LeggiCliente(CURR_ID, true));
        }
        public override void OnBack()
        {
            RiempiCampi(_db.LeggiCliente(CURR_ID, false));
        }

        public override void OnFind()
        {
            Form[] lista = ParentForm.MdiChildren;
            bool canOpen = true;
            foreach (Form child in lista)
            {
                if (child.Name == "Ricerca")
                    canOpen = false;
            }
            if (canOpen)
            {
                BODY_RADAR.Rows.Clear();

                foreach (var item in _db.LeggiListaClienti().ToList())
                {
                    BODY_RADAR.Rows.Add(item.Id, item.RagioneSociale, item.Indirizzo, item.Citta, item.Settore);
                }
                
                DOCUMENT_RADAR = new Ricerca();
                DOCUMENT_RADAR.ricercaGridView.DataSource = BODY_RADAR;
                DOCUMENT_RADAR.ItemRadarSel += new EventHandler<ItemRadarSelectedEventArgs>(OnItemSelectedEvent);

                DOCUMENT_RADAR.MdiParent = this.ParentForm;
                DOCUMENT_RADAR.Text = "Ricerca Clienti";
                DOCUMENT_RADAR.Show();
            }
        }

        private void OnItemSelectedEvent(object sender, ItemRadarSelectedEventArgs e)
        {
            RiempiCampi(_db.LeggiCliente(e.SelectedID));
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
