namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmCadastroFornecedor
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
            this.label7 = new System.Windows.Forms.Label();
            this.btnCadastrarCidade = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCodigoCidade = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtValorMin = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtCodigoCondicao = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.txtCondicao = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtUF = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
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
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica",
            "Física",
            "Jurídica"});
            this.comboPessoa.TabIndex = 1;
            this.comboPessoa.SelectedIndexChanged += new System.EventHandler(this.comboPessoa_SelectedIndexChanged);
            // 
            // comboClassificacao
            // 
            this.comboClassificacao.Location = new System.Drawing.Point(505, 326);
            this.comboClassificacao.Size = new System.Drawing.Size(181, 21);
            this.comboClassificacao.TabIndex = 15;
            // 
            // txtNome_RazaoSocial
            // 
            this.txtNome_RazaoSocial.TabIndex = 2;
            // 
            // txtCep
            // 
            this.txtCep.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCep.TabIndex = 9;
            // 
            // txtBairro
            // 
            this.txtBairro.TabIndex = 7;
            // 
            // txtNumero
            // 
            this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumero.TabIndex = 6;
            this.txtNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTelefone
            // 
            this.txtTelefone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTelefone.TabIndex = 12;
            // 
            // txtEmail
            // 
            this.txtEmail.TabIndex = 11;
            // 
            // txtRua
            // 
            this.txtRua.TabIndex = 5;
            // 
            // txtRg_InscricaoEstadual
            // 
            this.txtRg_InscricaoEstadual.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRg_InscricaoEstadual.Location = new System.Drawing.Point(293, 327);
            this.txtRg_InscricaoEstadual.TabIndex = 14;
            // 
            // txtCpf_Cnpj
            // 
            this.txtCpf_Cnpj.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCpf_Cnpj.Location = new System.Drawing.Point(12, 327);
            this.txtCpf_Cnpj.TabIndex = 13;
            // 
            // txtApelido_NomeFantasia
            // 
            this.txtApelido_NomeFantasia.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.TabIndex = 4;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 311);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(290, 311);
            // 
            // lblClassificacao
            // 
            this.lblClassificacao.Location = new System.Drawing.Point(502, 310);
            // 
            // txtComplemento
            // 
            this.txtComplemento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComplemento.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(169, 36);
            // 
            // lblAtualizacao
            // 
            this.lblAtualizacao.Location = new System.Drawing.Point(122, 647);
            // 
            // btnSalvar
            // 
            this.btnSalvar.TabIndex = 18;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 20);
            this.label7.TabIndex = 39;
            this.label7.Text = "Fornecedor";
            // 
            // btnCadastrarCidade
            // 
            this.btnCadastrarCidade.Location = new System.Drawing.Point(692, 209);
            this.btnCadastrarCidade.Name = "btnCadastrarCidade";
            this.btnCadastrarCidade.Size = new System.Drawing.Size(201, 23);
            this.btnCadastrarCidade.TabIndex = 10;
            this.btnCadastrarCidade.Text = "Selecionar Cidade";
            this.btnCadastrarCidade.UseVisualStyleBackColor = true;
            this.btnCadastrarCidade.Click += new System.EventHandler(this.btnCadastrarCidade_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(298, 197);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 71;
            this.label15.Text = "Código Cidade:";
            // 
            // txtCodigoCidade
            // 
            this.txtCodigoCidade.Enabled = false;
            this.txtCodigoCidade.Location = new System.Drawing.Point(298, 211);
            this.txtCodigoCidade.Name = "txtCodigoCidade";
            this.txtCodigoCidade.Size = new System.Drawing.Size(135, 20);
            this.txtCodigoCidade.TabIndex = 70;
            this.txtCodigoCidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(436, 195);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 69;
            this.label14.Text = "Cidade:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Red;
            this.label36.Location = new System.Drawing.Point(345, 250);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(15, 20);
            this.label36.TabIndex = 67;
            this.label36.Text = "*";
            // 
            // txtValorMin
            // 
            this.txtValorMin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValorMin.Location = new System.Drawing.Point(12, 450);
            this.txtValorMin.Name = "txtValorMin";
            this.txtValorMin.Size = new System.Drawing.Size(126, 20);
            this.txtValorMin.TabIndex = 17;
            this.txtValorMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 434);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 13);
            this.label17.TabIndex = 74;
            this.label17.Text = "Valor Minímo de Pedido:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.Red;
            this.label35.Location = new System.Drawing.Point(281, 366);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(15, 20);
            this.label35.TabIndex = 86;
            this.label35.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(169, 371);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 13);
            this.label23.TabIndex = 84;
            this.label23.Text = "Condição Pagamento:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(13, 371);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 13);
            this.label22.TabIndex = 83;
            this.label22.Text = "Código Condição:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(412, 384);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(201, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Selecionar Condição";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtCodigoCondicao
            // 
            this.txtCodigoCondicao.Enabled = false;
            this.txtCodigoCondicao.Location = new System.Drawing.Point(12, 387);
            this.txtCodigoCondicao.Name = "txtCodigoCondicao";
            this.txtCodigoCondicao.Size = new System.Drawing.Size(153, 20);
            this.txtCodigoCondicao.TabIndex = 77;
            this.txtCodigoCondicao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(241, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 20);
            this.label19.TabIndex = 87;
            this.label19.Text = "*";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(150, 79);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(15, 20);
            this.label20.TabIndex = 88;
            this.label20.Text = "*";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(71, 128);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(15, 20);
            this.label24.TabIndex = 89;
            this.label24.Text = "*";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(485, 192);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(15, 20);
            this.label27.TabIndex = 92;
            this.label27.Text = "*";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(548, 136);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 20);
            this.label28.TabIndex = 93;
            this.label28.Text = "*";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(644, 136);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(15, 20);
            this.label29.TabIndex = 94;
            this.label29.Text = "*";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(37, 303);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(15, 20);
            this.label30.TabIndex = 95;
            this.label30.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(47, 252);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 20);
            this.label18.TabIndex = 98;
            this.label18.Text = "*";
            // 
            // txtCidade
            // 
            this.txtCidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCidade.Enabled = false;
            this.txtCidade.Location = new System.Drawing.Point(439, 211);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(165, 20);
            this.txtCidade.TabIndex = 99;
            // 
            // txtCondicao
            // 
            this.txtCondicao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCondicao.Enabled = false;
            this.txtCondicao.Location = new System.Drawing.Point(172, 387);
            this.txtCondicao.Name = "txtCondicao";
            this.txtCondicao.Size = new System.Drawing.Size(234, 20);
            this.txtCondicao.TabIndex = 100;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(127, 429);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 20);
            this.label21.TabIndex = 101;
            this.label21.Text = "*";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(606, 197);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 13);
            this.label25.TabIndex = 116;
            this.label25.Text = "UF:";
            // 
            // txtUF
            // 
            this.txtUF.Enabled = false;
            this.txtUF.Location = new System.Drawing.Point(608, 211);
            this.txtUF.Name = "txtUF";
            this.txtUF.Size = new System.Drawing.Size(78, 20);
            this.txtUF.TabIndex = 115;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(37, 190);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 20);
            this.label26.TabIndex = 117;
            this.label26.Text = "*";
            // 
            // FrmCadastroFornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtUF);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtCondicao);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtCodigoCondicao);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtValorMin);
            this.Controls.Add(this.btnCadastrarCidade);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCodigoCidade);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label7);
            this.Name = "FrmCadastroFornecedor";
            this.Text = "Cadastro Fornecedor";
            this.Load += new System.EventHandler(this.FrmCadastroFornecedor_Load);
            this.Controls.SetChildIndex(this.labelDataCriacao, 0);
            this.Controls.SetChildIndex(this.labelCriacao, 0);
            this.Controls.SetChildIndex(this.lblAtualizacao, 0);
            this.Controls.SetChildIndex(this.checkAtivo, 0);
            this.Controls.SetChildIndex(this.lblDataAtual, 0);
            this.Controls.SetChildIndex(this.DataAtaul, 0);
            this.Controls.SetChildIndex(this.lblUsuario, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lblClassificacao, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.txtComplemento, 0);
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
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.txtCodigoCidade, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.btnCadastrarCidade, 0);
            this.Controls.SetChildIndex(this.txtValorMin, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.txtCodigoCondicao, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.txtCidade, 0);
            this.Controls.SetChildIndex(this.txtCondicao, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.txtUF, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCadastrarCidade;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtValorMin;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtCidade;
        public System.Windows.Forms.TextBox txtCodigoCidade;
        public System.Windows.Forms.TextBox txtCodigoCondicao;
        public System.Windows.Forms.TextBox txtCondicao;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox txtUF;
        private System.Windows.Forms.Label label26;
    }
}
