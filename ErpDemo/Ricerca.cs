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
    public partial class Ricerca : Form
    { 
        // creo le classi di evento personalizzate
        public class ItemRadarSelectedEventArgs : EventArgs
        {
            public int SelectedID;

            public ItemRadarSelectedEventArgs(int value)
                : base()
            {
                this.SelectedID = value;
            }
        }
        // creo le proprietà eventi
        public event EventHandler<ItemRadarSelectedEventArgs> ItemRadarSel;
        protected virtual void OnItemSelected(ItemRadarSelectedEventArgs e)
        {
            //make sure we have someone subscribed to our event before we try to raise it
            if (this.ItemRadarSel != null)
            {
                this.ItemRadarSel(this, e);
            }
        }

        public Ricerca()
        {
            InitializeComponent();
        }
        private void ricercaGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OnItemSelected(new ItemRadarSelectedEventArgs(Convert.ToInt32(ricercaGridView.Rows[e.RowIndex].Cells[0].Value)));
            this.Close();

        }
    }
}
