
namespace ErpDemo
{
    partial class Ricerca
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ricercaGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ricercaGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ricercaGridView
            // 
            this.ricercaGridView.AllowUserToAddRows = false;
            this.ricercaGridView.AllowUserToDeleteRows = false;
            this.ricercaGridView.AllowUserToOrderColumns = true;
            this.ricercaGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ricercaGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ricercaGridView.Location = new System.Drawing.Point(0, 0);
            this.ricercaGridView.Name = "ricercaGridView";
            this.ricercaGridView.ReadOnly = true;
            this.ricercaGridView.RowTemplate.Height = 25;
            this.ricercaGridView.Size = new System.Drawing.Size(800, 450);
            this.ricercaGridView.TabIndex = 0;
            this.ricercaGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ricercaGridView_CellMouseDoubleClick);
            // 
            // Ricerca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ricercaGridView);
            this.Name = "Ricerca";
            this.Text = "Ricerca";
            ((System.ComponentModel.ISupportInitialize)(this.ricercaGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView ricercaGridView;
    }
}