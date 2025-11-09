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
            this.btnCancelarConta = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(722, 654);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Size = new System.Drawing.Size(1298, 20);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(973, 61);
            // 
            // btnFiltro
            // 
            this.btnFiltro.Location = new System.Drawing.Point(1092, 60);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(846, 654);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(970, 655);
            // 
            // btnCancelarConta
            // 
            this.btnCancelarConta.Location = new System.Drawing.Point(1094, 655);
            this.btnCancelarConta.Name = "btnCancelarConta";
            this.btnCancelarConta.Size = new System.Drawing.Size(118, 23);
            this.btnCancelarConta.TabIndex = 8;
            this.btnCancelarConta.Text = "Cancelar Conta";
            this.btnCancelarConta.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1211, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 9;
            // 
            // FrmConsultaContasAReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnCancelarConta);
            this.Name = "FrmConsultaContasAReceber";
            this.Text = "Consulta Contas a Receber";
            this.Controls.SetChildIndex(this.txtPesquisa, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.btnExcluir, 0);
            this.Controls.SetChildIndex(this.btnEditar, 0);
            this.Controls.SetChildIndex(this.btnAdicionar, 0);
            this.Controls.SetChildIndex(this.listViewFormaPagamento, 0);
            this.Controls.SetChildIndex(this.btnFiltro, 0);
            this.Controls.SetChildIndex(this.btnCancelarConta, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelarConta;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
