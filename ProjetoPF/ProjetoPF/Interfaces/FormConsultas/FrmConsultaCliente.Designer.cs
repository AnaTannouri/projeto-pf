namespace ProjetoPF.Interfaces.FormConsultas
{
    partial class FrmConsultaCliente
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click_1);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPesquisa.Location = new System.Drawing.Point(38, 37);
            // 
            // btnEditar
            // 
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Clientes";
            // 
            // FrmConsultaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label2);
            this.Name = "FrmConsultaCliente";
            this.Text = "Consulta Clientes";
            this.Load += new System.EventHandler(this.FrmConsultaCliente_Load);
            this.Controls.SetChildIndex(this.txtPesquisa, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.btnExcluir, 0);
            this.Controls.SetChildIndex(this.btnEditar, 0);
            this.Controls.SetChildIndex(this.btnAdicionar, 0);
            this.Controls.SetChildIndex(this.listViewFormaPagamento, 0);
            this.Controls.SetChildIndex(this.btnFiltro, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
    }
}
