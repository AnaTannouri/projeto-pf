namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmCadastroPessoa
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
            this.comboPessoa = new System.Windows.Forms.ComboBox();
            this.comboClassificacao = new System.Windows.Forms.ComboBox();
            this.txtNome_RazaoSocial = new System.Windows.Forms.TextBox();
            this.txtCep = new System.Windows.Forms.TextBox();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtRua = new System.Windows.Forms.TextBox();
            this.txtRg_InscricaoEstadual = new System.Windows.Forms.TextBox();
            this.txtCpf_Cnpj = new System.Windows.Forms.TextBox();
            this.txtApelido_NomeFantasia = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblClassificacao = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(12, 53);
            // 
            // lblAtualizacao
            // 
            this.lblAtualizacao.Location = new System.Drawing.Point(118, 647);
            // 
            // comboPessoa
            // 
            this.comboPessoa.FormattingEnabled = true;
            this.comboPessoa.Location = new System.Drawing.Point(171, 52);
            this.comboPessoa.Name = "comboPessoa";
            this.comboPessoa.Size = new System.Drawing.Size(193, 21);
            this.comboPessoa.TabIndex = 5;
            this.comboPessoa.SelectedIndexChanged += new System.EventHandler(this.comboPessoa_SelectedIndexChanged);
            // 
            // comboClassificacao
            // 
            this.comboClassificacao.FormattingEnabled = true;
            this.comboClassificacao.Location = new System.Drawing.Point(501, 337);
            this.comboClassificacao.Name = "comboClassificacao";
            this.comboClassificacao.Size = new System.Drawing.Size(164, 21);
            this.comboClassificacao.TabIndex = 6;
            // 
            // txtNome_RazaoSocial
            // 
            this.txtNome_RazaoSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNome_RazaoSocial.Location = new System.Drawing.Point(15, 102);
            this.txtNome_RazaoSocial.Name = "txtNome_RazaoSocial";
            this.txtNome_RazaoSocial.Size = new System.Drawing.Size(586, 20);
            this.txtNome_RazaoSocial.TabIndex = 10;
            // 
            // txtCep
            // 
            this.txtCep.Location = new System.Drawing.Point(12, 211);
            this.txtCep.Name = "txtCep";
            this.txtCep.Size = new System.Drawing.Size(273, 20);
            this.txtCep.TabIndex = 13;
            // 
            // txtBairro
            // 
            this.txtBairro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBairro.Location = new System.Drawing.Point(607, 157);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(474, 20);
            this.txtBairro.TabIndex = 14;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(501, 157);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 20);
            this.txtNumero.TabIndex = 15;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(290, 273);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(202, 20);
            this.txtTelefone.TabIndex = 16;
            // 
            // txtEmail
            // 
            this.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmail.Location = new System.Drawing.Point(12, 273);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(273, 20);
            this.txtEmail.TabIndex = 17;
            // 
            // txtRua
            // 
            this.txtRua.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRua.Location = new System.Drawing.Point(12, 157);
            this.txtRua.Name = "txtRua";
            this.txtRua.Size = new System.Drawing.Size(483, 20);
            this.txtRua.TabIndex = 18;
            // 
            // txtRg_InscricaoEstadual
            // 
            this.txtRg_InscricaoEstadual.Location = new System.Drawing.Point(293, 338);
            this.txtRg_InscricaoEstadual.Name = "txtRg_InscricaoEstadual";
            this.txtRg_InscricaoEstadual.Size = new System.Drawing.Size(202, 20);
            this.txtRg_InscricaoEstadual.TabIndex = 19;
            // 
            // txtCpf_Cnpj
            // 
            this.txtCpf_Cnpj.Location = new System.Drawing.Point(12, 338);
            this.txtCpf_Cnpj.Name = "txtCpf_Cnpj";
            this.txtCpf_Cnpj.Size = new System.Drawing.Size(273, 20);
            this.txtCpf_Cnpj.TabIndex = 20;
            // 
            // txtApelido_NomeFantasia
            // 
            this.txtApelido_NomeFantasia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApelido_NomeFantasia.Location = new System.Drawing.Point(607, 102);
            this.txtApelido_NomeFantasia.Name = "txtApelido_NomeFantasia";
            this.txtApelido_NomeFantasia.Size = new System.Drawing.Size(474, 20);
            this.txtApelido_NomeFantasia.TabIndex = 21;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(1087, 102);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(245, 20);
            this.dateTimePicker1.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Tipo Pessoa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "CPF/ CNPJ:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Nome:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(604, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Apelido/ Nome Fantasia:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Rg/ Inscrição Estadual:";
            // 
            // lblClassificacao
            // 
            this.lblClassificacao.AutoSize = true;
            this.lblClassificacao.Location = new System.Drawing.Point(502, 321);
            this.lblClassificacao.Name = "lblClassificacao";
            this.lblClassificacao.Size = new System.Drawing.Size(72, 13);
            this.lblClassificacao.TabIndex = 29;
            this.lblClassificacao.Text = "Classificação:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Email:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(290, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Telefone:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(604, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Bairro:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Endereço:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(498, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Número:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 195);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Cep:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1084, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(162, 13);
            this.label16.TabIndex = 37;
            this.label16.Text = "Data Nascimento/ Data Criação:";
            // 
            // txtComplemento
            // 
            this.txtComplemento.Location = new System.Drawing.Point(1087, 157);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(245, 20);
            this.txtComplemento.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1084, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Complemento:";
            // 
            // FrmCadastroPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtComplemento);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblClassificacao);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtApelido_NomeFantasia);
            this.Controls.Add(this.txtCpf_Cnpj);
            this.Controls.Add(this.txtRg_InscricaoEstadual);
            this.Controls.Add(this.txtRua);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.txtCep);
            this.Controls.Add(this.txtNome_RazaoSocial);
            this.Controls.Add(this.comboClassificacao);
            this.Controls.Add(this.comboPessoa);
            this.Name = "FrmCadastroPessoa";
            this.Load += new System.EventHandler(this.FrmCadastroPessoa_Load);
            this.Controls.SetChildIndex(this.labelDataCriacao, 0);
            this.Controls.SetChildIndex(this.labelCriacao, 0);
            this.Controls.SetChildIndex(this.lblAtualizacao, 0);
            this.Controls.SetChildIndex(this.checkAtivo, 0);
            this.Controls.SetChildIndex(this.lblDataAtual, 0);
            this.Controls.SetChildIndex(this.DataAtaul, 0);
            this.Controls.SetChildIndex(this.lblUsuario, 0);
            this.Controls.SetChildIndex(this.comboPessoa, 0);
            this.Controls.SetChildIndex(this.comboClassificacao, 0);
            this.Controls.SetChildIndex(this.txtNome_RazaoSocial, 0);
            this.Controls.SetChildIndex(this.txtCep, 0);
            this.Controls.SetChildIndex(this.txtBairro, 0);
            this.Controls.SetChildIndex(this.txtNumero, 0);
            this.Controls.SetChildIndex(this.txtTelefone, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.txtRua, 0);
            this.Controls.SetChildIndex(this.txtRg_InscricaoEstadual, 0);
            this.Controls.SetChildIndex(this.txtCpf_Cnpj, 0);
            this.Controls.SetChildIndex(this.txtApelido_NomeFantasia, 0);
            this.Controls.SetChildIndex(this.dateTimePicker1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lblClassificacao, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnVoltar, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtComplemento, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.ComboBox comboPessoa;
        public System.Windows.Forms.ComboBox comboClassificacao;
        public System.Windows.Forms.TextBox txtNome_RazaoSocial;
        public System.Windows.Forms.TextBox txtCep;
        public System.Windows.Forms.TextBox txtBairro;
        public System.Windows.Forms.TextBox txtNumero;
        public System.Windows.Forms.TextBox txtTelefone;
        public System.Windows.Forms.TextBox txtEmail;
        public System.Windows.Forms.TextBox txtRua;
        public System.Windows.Forms.TextBox txtRg_InscricaoEstadual;
        public System.Windows.Forms.TextBox txtCpf_Cnpj;
        public System.Windows.Forms.TextBox txtApelido_NomeFantasia;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblClassificacao;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label2;
    }
}
