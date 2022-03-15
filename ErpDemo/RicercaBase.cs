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
    public partial class RicercaBase : Form
    {
        public RicercaBase(DataTable body)
        {
            InitializeComponent();
            ricercaGridView.DataSource = body;
        }

        public class RicercaEventArgs : EventArgs
        {
            public int IdSelezionato;
            public RicercaEventArgs(int value) 
                : base()
            { 
                IdSelezionato = value;
            }
        }

        public event EventHandler<RicercaEventArgs> RigaSelezionata;

        protected virtual void OnRigaSelezionata(RicercaEventArgs e)
        {
            if (RigaSelezionata != null)
                RigaSelezionata(this, e);

        }
        private void ricercaGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int val = Convert.ToInt32(ricercaGridView.Rows[e.RowIndex].Cells[0].Value);

            OnRigaSelezionata(new RicercaEventArgs(val));

            Close();
        }
    }
}
