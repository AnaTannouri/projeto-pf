namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmCadastroEstado
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
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.txtUf = new System.Windows.Forms.TextBox();
            this.txtCodPais = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPaís = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPais = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSalvar
            // 
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click_1);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(12, 55);
            // 
            // btnVoltar
            // 
            this.btnVoltar.TabIndex = 0;
            // 
            // lblAtualizacao
            // 
            this.lblAtualizacao.Location = new System.Drawing.Point(124, 647);
            // 
            // txtEstado
            // 
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Location = new System.Drawing.Point(188, 57);
            this.txtEstado.MaxLength = 100;
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(371, 20);
            this.txtEstado.TabIndex = 1;
            // 
            // txtUf
            // 
            this.txtUf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUf.Location = new System.Drawing.Point(12, 119);
            this.txtUf.MaxLength = 2;
            this.txtUf.Name = "txtUf";
            this.txtUf.Size = new System.Drawing.Size(100, 20);
            this.txtUf.TabIndex = 2;
            // 
            // txtCodPais
            // 
            this.txtCodPais.Enabled = false;
            this.txtCodPais.Location = new System.Drawing.Point(146, 119);
            this.txtCodPais.Name = "txtCodPais";
            this.txtCodPais.Size = new System.Drawing.Size(100, 20);
            this.txtCodPais.TabIndex = 0;
            this.txtCodPais.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Estado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Uf:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Código País:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "País:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(226, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(32, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(307, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "*";
            // 
            // btnPaís
            // 
            this.btnPaís.Location = new System.Drawing.Point(523, 117);
            this.btnPaís.Name = "btnPaís";
            this.btnPaís.Size = new System.Drawing.Size(137, 23);
            this.btnPaís.TabIndex = 3;
            this.btnPaís.Text = "Selecionar País";
            this.btnPaís.UseVisualStyleBackColor = true;
            this.btnPaís.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "Estado";
            // 
            // txtPais
            // 
            this.txtPais.Enabled = false;
            this.txtPais.Location = new System.Drawing.Point(267, 119);
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(250, 20);
            this.txtPais.TabIndex = 19;
            // 
            // FrmCadastroEstado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.txtPais);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPaís);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodPais);
            this.Controls.Add(this.txtUf);
            this.Controls.Add(this.txtEstado);
            this.Name = "FrmCadastroEstado";
            this.Text = "Cadastro Estado";
            this.Load += new System.EventHandler(this.FrmCadastroEstado_Load);
            this.Controls.SetChildIndex(this.labelDataCriacao, 0);
            this.Controls.SetChildIndex(this.labelCriacao, 0);
            this.Controls.SetChildIndex(this.lblAtualizacao, 0);
            this.Controls.SetChildIndex(this.checkAtivo, 0);
            this.Controls.SetChildIndex(this.lblDataAtual, 0);
            this.Controls.SetChildIndex(this.DataAtaul, 0);
            this.Controls.SetChildIndex(this.lblUsuario, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnVoltar, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtEstado, 0);
            this.Controls.SetChildIndex(this.txtUf, 0);
            this.Controls.SetChildIndex(this.txtCodPais, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.btnPaís, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtPais, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.TextBox txtUf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPaís;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtPais;
        public System.Windows.Forms.TextBox txtCodPais;
    }
}
