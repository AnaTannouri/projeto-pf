namespace ProjetoPF.Interfaces.FormConsultas
{
    partial class FrmConsultaContasAPagar
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
            this.btnCancelarConta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(721, 653);
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click_1);
            // 
            // listViewFormaPagamento
            // 
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Size = new System.Drawing.Size(1290, 20);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(961, 60);
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnFiltro
            // 
            this.btnFiltro.Location = new System.Drawing.Point(1082, 61);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(845, 653);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(969, 653);
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1203, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnCancelarConta
            // 
            this.btnCancelarConta.Location = new System.Drawing.Point(1093, 654);
            this.btnCancelarConta.Name = "btnCancelarConta";
            this.btnCancelarConta.Size = new System.Drawing.Size(118, 22);
            this.btnCancelarConta.TabIndex = 9;
            this.btnCancelarConta.Text = "Cancelar Conta";
            this.btnCancelarConta.UseVisualStyleBackColor = true;
            this.btnCancelarConta.Click += new System.EventHandler(this.btnCancelarConta_Click);
            // 
            // FrmConsultaContasAPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.btnCancelarConta);
            this.Controls.Add(this.comboBox1);
            this.Name = "FrmConsultaContasAPagar";
            this.Text = "Consulta Contas a Pagar";
            this.Load += new System.EventHandler(this.FrmConsultaContasAPagar_Load);
            this.Controls.SetChildIndex(this.txtPesquisa, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.btnExcluir, 0);
            this.Controls.SetChildIndex(this.btnEditar, 0);
            this.Controls.SetChildIndex(this.btnAdicionar, 0);
            this.Controls.SetChildIndex(this.listViewFormaPagamento, 0);
            this.Controls.SetChildIndex(this.btnFiltro, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.btnCancelarConta, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnCancelarConta;
    }
}
