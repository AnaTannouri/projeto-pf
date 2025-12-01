namespace ProjetoPF.Interfaces.FormConsultas
{
    partial class FrmConsultaContasAReceber
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(845, 654);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Size = new System.Drawing.Size(1298, 20);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(973, 61);
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnFiltro
            // 
            this.btnFiltro.Location = new System.Drawing.Point(1092, 60);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(969, 655);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(1093, 655);
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1211, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FrmConsultaContasAReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.comboBox1);
            this.Name = "FrmConsultaContasAReceber";
            this.Text = "Consulta Contas a Receber";
            this.Load += new System.EventHandler(this.FrmConsultaContasAReceber_Load);
            this.Controls.SetChildIndex(this.txtPesquisa, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.btnExcluir, 0);
            this.Controls.SetChildIndex(this.btnEditar, 0);
            this.Controls.SetChildIndex(this.btnAdicionar, 0);
            this.Controls.SetChildIndex(this.listViewFormaPagamento, 0);
            this.Controls.SetChildIndex(this.btnFiltro, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
