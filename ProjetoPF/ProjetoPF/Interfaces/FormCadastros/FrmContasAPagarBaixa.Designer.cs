namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmContasAPagarBaixa
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
            this.datePagamento = new System.Windows.Forms.DateTimePicker();
            this.txtValorPago = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateEmissao
            // 
            this.dateEmissao.CustomFormat = " ";
            this.dateEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            // 
            // dateVencimento
            // 
            this.dateVencimento.CustomFormat = " ";
            this.dateVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            // 
            // btnSalvar
            // 
            this.btnSalvar.TabIndex = 16;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.Text = "Modelo:";
            // 
            // datePagamento
            // 
            this.datePagamento.Location = new System.Drawing.Point(448, 240);
            this.datePagamento.Name = "datePagamento";
            this.datePagamento.Size = new System.Drawing.Size(200, 20);
            this.datePagamento.TabIndex = 13;
            // 
            // txtValorPago
            // 
            this.txtValorPago.Location = new System.Drawing.Point(15, 312);
            this.txtValorPago.MaxLength = 15;
            this.txtValorPago.Name = "txtValorPago";
            this.txtValorPago.Size = new System.Drawing.Size(100, 20);
            this.txtValorPago.TabIndex = 14;
            this.txtValorPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(445, 224);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(105, 13);
            this.label24.TabIndex = 33;
            this.label24.Text = "Data de Pagamento:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 296);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(85, 13);
            this.label25.TabIndex = 34;
            this.label25.Text = "Valor Pago (R$):";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(12, 368);
            this.txtObservacao.MaxLength = 200;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(1317, 263);
            this.txtObservacao.TabIndex = 15;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(14, 349);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(68, 13);
            this.label26.TabIndex = 35;
            this.label26.Text = "Observação:";
            // 
            // FrmContasAPagarBaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtValorPago);
            this.Controls.Add(this.datePagamento);
            this.Name = "FrmContasAPagarBaixa";
            this.Text = "Baixa Parcela";
            this.Load += new System.EventHandler(this.FrmContasAPagarBaixa_Load);
            this.Controls.SetChildIndex(this.btnPesquisarFornecedor, 0);
            this.Controls.SetChildIndex(this.txtNumParcela, 0);
            this.Controls.SetChildIndex(this.txtDesconto, 0);
            this.Controls.SetChildIndex(this.txtJuros, 0);
            this.Controls.SetChildIndex(this.txtValorParcela, 0);
            this.Controls.SetChildIndex(this.txtNumNota, 0);
            this.Controls.SetChildIndex(this.txtSerie, 0);
            this.Controls.SetChildIndex(this.dateEmissao, 0);
            this.Controls.SetChildIndex(this.txtMulta, 0);
            this.Controls.SetChildIndex(this.dateVencimento, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnVoltar, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.checkAtivo, 0);
            this.Controls.SetChildIndex(this.lblDataAtual, 0);
            this.Controls.SetChildIndex(this.DataAtaul, 0);
            this.Controls.SetChildIndex(this.labelDataCriacao, 0);
            this.Controls.SetChildIndex(this.labelCriacao, 0);
            this.Controls.SetChildIndex(this.lblAtualizacao, 0);
            this.Controls.SetChildIndex(this.txtForma, 0);
            this.Controls.SetChildIndex(this.txtCodForma, 0);
            this.Controls.SetChildIndex(this.txtFornecedor, 0);
            this.Controls.SetChildIndex(this.txtCodFornecedor, 0);
            this.Controls.SetChildIndex(this.datePagamento, 0);
            this.Controls.SetChildIndex(this.txtValorPago, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.txtObservacao, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker datePagamento;
        private System.Windows.Forms.TextBox txtValorPago;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Label label26;
    }
}
