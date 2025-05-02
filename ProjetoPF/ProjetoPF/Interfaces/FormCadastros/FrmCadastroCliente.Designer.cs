namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmCadastroCliente
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
            this.txtCodigoCondicao = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCodigoCidade = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCadastrarCidade = new System.Windows.Forms.Button();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.txtCondicao = new System.Windows.Forms.TextBox();
            this.txtUF = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboPessoa
            // 
            this.comboPessoa.Items.AddRange(new object[] {
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "FÍSICA",
            "JURÍDICA",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica"});
            this.comboPessoa.Location = new System.Drawing.Point(176, 52);
            this.comboPessoa.TabIndex = 1;
            this.comboPessoa.SelectedIndexChanged += new System.EventHandler(this.comboPessoa_SelectedIndexChanged);
            // 
            // comboClassificacao
            // 
            this.comboClassificacao.ItemHeight = 13;
            this.comboClassificacao.Location = new System.Drawing.Point(498, 325);
            this.comboClassificacao.MaxDropDownItems = 15;
            this.comboClassificacao.TabIndex = 15;
            // 
            // txtNome_RazaoSocial
            // 
            this.txtNome_RazaoSocial.MaxLength = 150;
            this.txtNome_RazaoSocial.TabIndex = 2;
            // 
            // txtCep
            // 
            this.txtCep.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCep.MaxLength = 10;
            this.txtCep.TabIndex = 9;
            // 
            // txtBairro
            // 
            this.txtBairro.MaxLength = 100;
            this.txtBairro.TabIndex = 7;
            // 
            // txtNumero
            // 
            this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumero.MaxLength = 10;
            this.txtNumero.TabIndex = 6;
            this.txtNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTelefone
            // 
            this.txtTelefone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTelefone.Location = new System.Drawing.Point(289, 273);
            this.txtTelefone.MaxLength = 20;
            this.txtTelefone.TabIndex = 12;
            // 
            // txtEmail
            // 
            this.txtEmail.MaxLength = 100;
            this.txtEmail.TabIndex = 11;
            // 
            // txtRua
            // 
            this.txtRua.MaxLength = 150;
            this.txtRua.TabIndex = 5;
            // 
            // txtRg_InscricaoEstadual
            // 
            this.txtRg_InscricaoEstadual.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRg_InscricaoEstadual.Location = new System.Drawing.Point(290, 325);
            this.txtRg_InscricaoEstadual.TabIndex = 14;
            // 
            // txtCpf_Cnpj
            // 
            this.txtCpf_Cnpj.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCpf_Cnpj.Location = new System.Drawing.Point(11, 325);
            this.txtCpf_Cnpj.TabIndex = 13;
            // 
            // txtApelido_NomeFantasia
            // 
            this.txtApelido_NomeFantasia.MaxLength = 150;
            this.txtApelido_NomeFantasia.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.TabIndex = 4;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 309);
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.Text = "CPF:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(291, 309);
            // 
            // lblClassificacao
            // 
            this.lblClassificacao.Location = new System.Drawing.Point(498, 309);
            // 
            // txtComplemento
            // 
            this.txtComplemento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComplemento.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(173, 36);
            // 
            // btnSalvar
            // 
            this.btnSalvar.TabIndex = 17;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click_1);
            // 
            // btnVoltar
            // 
            this.btnVoltar.TabIndex = 0;
            // 
            // lblAtualizacao
            // 
            this.lblAtualizacao.Location = new System.Drawing.Point(124, 647);
            // 
            // txtCodigoCondicao
            // 
            this.txtCodigoCondicao.Enabled = false;
            this.txtCodigoCondicao.Location = new System.Drawing.Point(12, 375);
            this.txtCodigoCondicao.Name = "txtCodigoCondicao";
            this.txtCodigoCondicao.Size = new System.Drawing.Size(151, 20);
            this.txtCodigoCondicao.TabIndex = 0;
            this.txtCodigoCondicao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(460, 372);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Selecionar Condição";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 358);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 13);
            this.label22.TabIndex = 47;
            this.label22.Text = "Código Condição:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(171, 357);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 13);
            this.label23.TabIndex = 48;
            this.label23.Text = "Condição Pagamento:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(12, 12);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 20);
            this.label24.TabIndex = 49;
            this.label24.Text = "Cliente";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(237, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(15, 20);
            this.label25.TabIndex = 50;
            this.label25.Text = "*";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(113, 76);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 20);
            this.label26.TabIndex = 51;
            this.label26.Text = "*";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(62, 130);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(15, 20);
            this.label27.TabIndex = 52;
            this.label27.Text = "*";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(538, 130);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 20);
            this.label28.TabIndex = 53;
            this.label28.Text = "*";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(635, 130);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(15, 20);
            this.label31.TabIndex = 56;
            this.label31.Text = "*";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(47, 302);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(15, 20);
            this.label33.TabIndex = 58;
            this.label33.Text = "*";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.Red;
            this.label35.Location = new System.Drawing.Point(283, 352);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(15, 20);
            this.label35.TabIndex = 60;
            this.label35.Text = "*";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(41, 250);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(15, 20);
            this.label29.TabIndex = 54;
            this.label29.Text = "*";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(335, 252);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(15, 20);
            this.label30.TabIndex = 55;
            this.label30.Text = "*";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Red;
            this.label36.Location = new System.Drawing.Point(481, 193);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(15, 20);
            this.label36.TabIndex = 61;
            this.label36.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(440, 196);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 63;
            this.label14.Text = "Cidade:";
            // 
            // txtCodigoCidade
            // 
            this.txtCodigoCidade.Enabled = false;
            this.txtCodigoCidade.Location = new System.Drawing.Point(295, 213);
            this.txtCodigoCidade.Name = "txtCodigoCidade";
            this.txtCodigoCidade.Size = new System.Drawing.Size(139, 20);
            this.txtCodigoCidade.TabIndex = 0;
            this.txtCodigoCidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(293, 195);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 65;
            this.label15.Text = "Código Cidade:";
            // 
            // btnCadastrarCidade
            // 
            this.btnCadastrarCidade.Location = new System.Drawing.Point(710, 211);
            this.btnCadastrarCidade.Name = "btnCadastrarCidade";
            this.btnCadastrarCidade.Size = new System.Drawing.Size(203, 23);
            this.btnCadastrarCidade.TabIndex = 10;
            this.btnCadastrarCidade.Text = "Selecionar Cidade";
            this.btnCadastrarCidade.UseVisualStyleBackColor = true;
            this.btnCadastrarCidade.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtCidade
            // 
            this.txtCidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCidade.Enabled = false;
            this.txtCidade.Location = new System.Drawing.Point(440, 213);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(177, 20);
            this.txtCidade.TabIndex = 0;
            // 
            // txtCondicao
            // 
            this.txtCondicao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCondicao.Enabled = false;
            this.txtCondicao.Location = new System.Drawing.Point(174, 375);
            this.txtCondicao.Name = "txtCondicao";
            this.txtCondicao.Size = new System.Drawing.Size(280, 20);
            this.txtCondicao.TabIndex = 0;
            // 
            // txtUF
            // 
            this.txtUF.Enabled = false;
            this.txtUF.Location = new System.Drawing.Point(623, 213);
            this.txtUF.Name = "txtUF";
            this.txtUF.Size = new System.Drawing.Size(81, 20);
            this.txtUF.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(620, 196);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(24, 13);
            this.label21.TabIndex = 67;
            this.label21.Text = "UF:";
            // 
            // FrmCadastroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtUF);
            this.Controls.Add(this.txtCondicao);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.btnCadastrarCidade);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCodigoCidade);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtCodigoCondicao);
            this.Name = "FrmCadastroCliente";
            this.Text = "Cadastro Cliente";
            this.Load += new System.EventHandler(this.FrmCadastroCliente_Load_1);
            this.Controls.SetChildIndex(this.labelDataCriacao, 0);
            this.Controls.SetChildIndex(this.labelCriacao, 0);
            this.Controls.SetChildIndex(this.lblAtualizacao, 0);
            this.Controls.SetChildIndex(this.checkAtivo, 0);
            this.Controls.SetChildIndex(this.lblDataAtual, 0);
            this.Controls.SetChildIndex(this.DataAtaul, 0);
            this.Controls.SetChildIndex(this.lblUsuario, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtComplemento, 0);
            this.Controls.SetChildIndex(this.txtCodigoCondicao, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.label31, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.txtCodigoCidade, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.btnCadastrarCidade, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lblClassificacao, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnVoltar, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
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
            this.Controls.SetChildIndex(this.txtCidade, 0);
            this.Controls.SetChildIndex(this.txtCondicao, 0);
            this.Controls.SetChildIndex(this.txtUF, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.Button btnCadastrarFormas;
        public System.Windows.Forms.Button btnCadastrarCondicoes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnCadastrarCidade;
        public System.Windows.Forms.TextBox txtCidade;
        public System.Windows.Forms.TextBox txtCodigoCidade;
        public System.Windows.Forms.TextBox txtCodigoCondicao;
        public System.Windows.Forms.TextBox txtCondicao;
        public System.Windows.Forms.TextBox txtUF;
        private System.Windows.Forms.Label label21;
    }
}