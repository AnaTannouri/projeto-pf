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
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.txtMultaReais = new System.Windows.Forms.TextBox();
            this.txtJurosReais = new System.Windows.Forms.TextBox();
            this.txtDescontoReais = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDesconto
            // 
            this.txtDesconto.Enabled = false;
            // 
            // txtJuros
            // 
            this.txtJuros.Enabled = false;
            // 
            // dateEmissao
            // 
            this.dateEmissao.CustomFormat = " ";
            this.dateEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            // 
            // txtMulta
            // 
            this.txtMulta.Enabled = false;
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
            this.txtValorPago.Enabled = false;
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
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(57, 31);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(15, 20);
            this.label27.TabIndex = 36;
            this.label27.Text = "*";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(173, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 20);
            this.label28.TabIndex = 37;
            this.label28.Text = "*";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(317, 29);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(15, 20);
            this.label29.TabIndex = 38;
            this.label29.Text = "*";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(477, 32);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(15, 20);
            this.label30.TabIndex = 39;
            this.label30.Text = "*";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(968, 31);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(15, 20);
            this.label31.TabIndex = 40;
            this.label31.Text = "*";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(1132, 31);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(15, 20);
            this.label32.TabIndex = 41;
            this.label32.Text = "*";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(84, 86);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(15, 20);
            this.label33.TabIndex = 42;
            this.label33.Text = "*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.Red;
            this.label34.Location = new System.Drawing.Point(200, 88);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(15, 20);
            this.label34.TabIndex = 43;
            this.label34.Text = "*";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.Red;
            this.label38.Location = new System.Drawing.Point(556, 220);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(15, 20);
            this.label38.TabIndex = 47;
            this.label38.Text = "*";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(97, 289);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(15, 20);
            this.label39.TabIndex = 48;
            this.label39.Text = "*";
            // 
            // txtMultaReais
            // 
            this.txtMultaReais.Location = new System.Drawing.Point(333, 174);
            this.txtMultaReais.MaxLength = 15;
            this.txtMultaReais.Name = "txtMultaReais";
            this.txtMultaReais.Size = new System.Drawing.Size(100, 20);
            this.txtMultaReais.TabIndex = 49;
            // 
            // txtJurosReais
            // 
            this.txtJurosReais.Location = new System.Drawing.Point(439, 174);
            this.txtJurosReais.MaxLength = 15;
            this.txtJurosReais.Name = "txtJurosReais";
            this.txtJurosReais.Size = new System.Drawing.Size(100, 20);
            this.txtJurosReais.TabIndex = 50;
            // 
            // txtDescontoReais
            // 
            this.txtDescontoReais.Location = new System.Drawing.Point(545, 174);
            this.txtDescontoReais.MaxLength = 15;
            this.txtDescontoReais.Name = "txtDescontoReais";
            this.txtDescontoReais.Size = new System.Drawing.Size(100, 20);
            this.txtDescontoReais.TabIndex = 51;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(330, 158);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(59, 13);
            this.label35.TabIndex = 52;
            this.label35.Text = "Multa (R$):";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(436, 158);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(58, 13);
            this.label36.TabIndex = 53;
            this.label36.Text = "Juros (R$):";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(542, 158);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(79, 13);
            this.label37.TabIndex = 54;
            this.label37.Text = "Desconto (R$):";
            // 
            // FrmContasAPagarBaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.txtDescontoReais);
            this.Controls.Add(this.txtJurosReais);
            this.Controls.Add(this.txtMultaReais);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtValorPago);
            this.Controls.Add(this.datePagamento);
            this.Name = "FrmContasAPagarBaixa";
            this.Text = "Baixa Parcela";
            this.Load += new System.EventHandler(this.FrmContasAPagarBaixa_Load);
            this.Controls.SetChildIndex(this.label5, 0);
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
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.label31, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.label38, 0);
            this.Controls.SetChildIndex(this.label39, 0);
            this.Controls.SetChildIndex(this.txtMultaReais, 0);
            this.Controls.SetChildIndex(this.txtJurosReais, 0);
            this.Controls.SetChildIndex(this.txtDescontoReais, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.label37, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        public System.Windows.Forms.DateTimePicker datePagamento;
        public System.Windows.Forms.TextBox txtValorPago;
        public System.Windows.Forms.TextBox txtMultaReais;
        public System.Windows.Forms.TextBox txtJurosReais;
        public System.Windows.Forms.TextBox txtDescontoReais;
        public System.Windows.Forms.TextBox txtObservacao;
    }
}
