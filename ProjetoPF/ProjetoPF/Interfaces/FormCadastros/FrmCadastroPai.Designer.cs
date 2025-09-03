namespace ProjetoPF.FormCadastros
{
    partial class FrmCadastroPai
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
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkAtivo = new System.Windows.Forms.CheckBox();
            this.lblDataAtual = new System.Windows.Forms.Label();
            this.DataAtaul = new System.Windows.Forms.Label();
            this.labelDataCriacao = new System.Windows.Forms.Label();
            this.labelCriacao = new System.Windows.Forms.Label();
            this.lblAtualizacao = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 53);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(153, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Location = new System.Drawing.Point(1206, 659);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(126, 23);
            this.btnVoltar.TabIndex = 1;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1074, 659);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(126, 23);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Código:";
            // 
            // checkAtivo
            // 
            this.checkAtivo.AutoSize = true;
            this.checkAtivo.Location = new System.Drawing.Point(1317, 12);
            this.checkAtivo.Name = "checkAtivo";
            this.checkAtivo.Size = new System.Drawing.Size(15, 14);
            this.checkAtivo.TabIndex = 0;
            this.checkAtivo.UseVisualStyleBackColor = true;
            // 
            // lblDataAtual
            // 
            this.lblDataAtual.AutoSize = true;
            this.lblDataAtual.Location = new System.Drawing.Point(12, 648);
            this.lblDataAtual.Name = "lblDataAtual";
            this.lblDataAtual.Size = new System.Drawing.Size(87, 13);
            this.lblDataAtual.TabIndex = 4;
            this.lblDataAtual.Text = "Data de Criação:";
            // 
            // DataAtaul
            // 
            this.DataAtaul.AutoSize = true;
            this.DataAtaul.Location = new System.Drawing.Point(12, 669);
            this.DataAtaul.Name = "DataAtaul";
            this.DataAtaul.Size = new System.Drawing.Size(106, 13);
            this.DataAtaul.TabIndex = 5;
            this.DataAtaul.Text = "Data de Atualização:";
            // 
            // labelDataCriacao
            // 
            this.labelDataCriacao.AutoSize = true;
            this.labelDataCriacao.Location = new System.Drawing.Point(105, 648);
            this.labelDataCriacao.Name = "labelDataCriacao";
            this.labelDataCriacao.Size = new System.Drawing.Size(0, 13);
            this.labelDataCriacao.TabIndex = 7;
            // 
            // labelCriacao
            // 
            this.labelCriacao.AutoSize = true;
            this.labelCriacao.Location = new System.Drawing.Point(105, 648);
            this.labelCriacao.Name = "labelCriacao";
            this.labelCriacao.Size = new System.Drawing.Size(10, 13);
            this.labelCriacao.TabIndex = 0;
            this.labelCriacao.Text = "-";
            // 
            // lblAtualizacao
            // 
            this.lblAtualizacao.AutoSize = true;
            this.lblAtualizacao.Location = new System.Drawing.Point(124, 669);
            this.lblAtualizacao.Name = "lblAtualizacao";
            this.lblAtualizacao.Size = new System.Drawing.Size(10, 13);
            this.lblAtualizacao.TabIndex = 8;
            this.lblAtualizacao.Text = "-";
            // 
            // FrmCadastroPai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.lblAtualizacao);
            this.Controls.Add(this.labelCriacao);
            this.Controls.Add(this.labelDataCriacao);
            this.Controls.Add(this.DataAtaul);
            this.Controls.Add(this.lblDataAtual);
            this.Controls.Add(this.checkAtivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.txtCodigo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(1360, 728);
            this.MinimumSize = new System.Drawing.Size(1360, 728);
            this.Name = "FrmCadastroPai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário cadastro pai";
            this.Load += new System.EventHandler(this.FrmCadastroPai_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnSalvar;
        public System.Windows.Forms.TextBox txtCodigo;
        public System.Windows.Forms.Button btnVoltar;
        public System.Windows.Forms.CheckBox checkAtivo;
        public System.Windows.Forms.Label lblDataAtual;
        public System.Windows.Forms.Label DataAtaul;
        public System.Windows.Forms.Label labelDataCriacao;
        public System.Windows.Forms.Label labelCriacao;
        public System.Windows.Forms.Label lblAtualizacao;
        public System.Windows.Forms.Label label1;
    }
}