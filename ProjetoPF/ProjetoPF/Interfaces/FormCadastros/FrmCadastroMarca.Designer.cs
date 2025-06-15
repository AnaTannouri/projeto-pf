namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmCadastroMarca
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
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSalvar
            // 
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Enabled = false;
            // 
            // btnVoltar
            // 
            this.btnVoltar.TabIndex = 3;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Location = new System.Drawing.Point(171, 53);
            this.txtDescricao.MaxLength = 50;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(1161, 20);
            this.txtDescricao.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Marca:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(204, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "*";
            // 
            // FrmCadastroMarca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescricao);
            this.Name = "FrmCadastroMarca";
            this.Text = "Cadastro Marca";
            this.Load += new System.EventHandler(this.FrmCadastroMarca_Load);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnVoltar, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.checkAtivo, 0);
            this.Controls.SetChildIndex(this.lblDataAtual, 0);
            this.Controls.SetChildIndex(this.DataAtaul, 0);
            this.Controls.SetChildIndex(this.labelDataCriacao, 0);
            this.Controls.SetChildIndex(this.labelCriacao, 0);
            this.Controls.SetChildIndex(this.lblAtualizacao, 0);
            this.Controls.SetChildIndex(this.txtDescricao, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
