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
            this.comboFormas = new System.Windows.Forms.ComboBox();
            this.txtCodigoForma = new System.Windows.Forms.TextBox();
            this.txtCodigoCondicao = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.comboCondicoes = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.comboCidade = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCodigoCidade = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCadastrarCidade = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboPessoa
            // 
            this.comboPessoa.Items.AddRange(new object[] {
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
            // 
            // txtNome_RazaoSocial
            // 
            this.txtNome_RazaoSocial.MaxLength = 150;
            // 
            // txtCep
            // 
            this.txtCep.MaxLength = 10;
            // 
            // txtBairro
            // 
            this.txtBairro.MaxLength = 100;
            // 
            // txtNumero
            // 
            this.txtNumero.MaxLength = 10;
            // 
            // txtTelefone
            // 
            this.txtTelefone.MaxLength = 20;
            // 
            // txtEmail
            // 
            this.txtEmail.MaxLength = 100;
            // 
            // txtRua
            // 
            this.txtRua.MaxLength = 150;
            // 
            // txtApelido_NomeFantasia
            // 
            this.txtApelido_NomeFantasia.MaxLength = 150;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click_1);
            // 
            // comboFormas
            // 
            this.comboFormas.FormattingEnabled = true;
            this.comboFormas.Location = new System.Drawing.Point(107, 343);
            this.comboFormas.Name = "comboFormas";
            this.comboFormas.Size = new System.Drawing.Size(188, 21);
            this.comboFormas.TabIndex = 39;
            this.comboFormas.SelectedIndexChanged += new System.EventHandler(this.comboFormas_SelectedIndexChanged_1);
            // 
            // txtCodigoForma
            // 
            this.txtCodigoForma.Enabled = false;
            this.txtCodigoForma.Location = new System.Drawing.Point(12, 343);
            this.txtCodigoForma.Name = "txtCodigoForma";
            this.txtCodigoForma.Size = new System.Drawing.Size(89, 20);
            this.txtCodigoForma.TabIndex = 40;
            this.txtCodigoForma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCodigoCondicao
            // 
            this.txtCodigoCondicao.Enabled = false;
            this.txtCodigoCondicao.Location = new System.Drawing.Point(424, 342);
            this.txtCodigoCondicao.Name = "txtCodigoCondicao";
            this.txtCodigoCondicao.Size = new System.Drawing.Size(89, 20);
            this.txtCodigoCondicao.TabIndex = 41;
            this.txtCodigoCondicao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 342);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 42;
            this.button1.Text = "Cadastrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 325);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 43;
            this.label7.Text = "Código Forma";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(108, 325);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 13);
            this.label21.TabIndex = 44;
            this.label21.Text = "Formas Pagamento";
            // 
            // comboCondicoes
            // 
            this.comboCondicoes.FormattingEnabled = true;
            this.comboCondicoes.Location = new System.Drawing.Point(519, 341);
            this.comboCondicoes.Name = "comboCondicoes";
            this.comboCondicoes.Size = new System.Drawing.Size(188, 21);
            this.comboCondicoes.TabIndex = 45;
            this.comboCondicoes.SelectedIndexChanged += new System.EventHandler(this.comboCondicoes_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(713, 340);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 46;
            this.button2.Text = "Cadastrar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(421, 325);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 13);
            this.label22.TabIndex = 47;
            this.label22.Text = "Código Condição";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(516, 325);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(114, 13);
            this.label23.TabIndex = 48;
            this.label23.Text = "Condições Pagamento";
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
            this.label25.Location = new System.Drawing.Point(179, 35);
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
            this.label26.Location = new System.Drawing.Point(114, 81);
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
            this.label27.Location = new System.Drawing.Point(43, 127);
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
            this.label28.Location = new System.Drawing.Point(245, 129);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 20);
            this.label28.TabIndex = 53;
            this.label28.Text = "*";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(39, 176);
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
            this.label30.Location = new System.Drawing.Point(312, 176);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(15, 20);
            this.label30.TabIndex = 55;
            this.label30.Text = "*";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(412, 177);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(15, 20);
            this.label31.TabIndex = 56;
            this.label31.Text = "*";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(624, 177);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(15, 20);
            this.label32.TabIndex = 57;
            this.label32.Text = "*";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(428, 272);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(15, 20);
            this.label33.TabIndex = 58;
            this.label33.Text = "*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.Red;
            this.label34.Location = new System.Drawing.Point(206, 322);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(15, 20);
            this.label34.TabIndex = 59;
            this.label34.Text = "*";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.Red;
            this.label35.Location = new System.Drawing.Point(628, 319);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(15, 20);
            this.label35.TabIndex = 60;
            this.label35.Text = "*";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Red;
            this.label36.Location = new System.Drawing.Point(250, 222);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(15, 20);
            this.label36.TabIndex = 61;
            this.label36.Text = "*";
            // 
            // comboCidade
            // 
            this.comboCidade.FormattingEnabled = true;
            this.comboCidade.Location = new System.Drawing.Point(201, 242);
            this.comboCidade.Name = "comboCidade";
            this.comboCidade.Size = new System.Drawing.Size(155, 21);
            this.comboCidade.TabIndex = 62;
            this.comboCidade.SelectedIndexChanged += new System.EventHandler(this.comboCidade_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(199, 227);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 63;
            this.label14.Text = "Cidades";
            // 
            // txtCodigoCidade
            // 
            this.txtCodigoCidade.Enabled = false;
            this.txtCodigoCidade.Location = new System.Drawing.Point(111, 243);
            this.txtCodigoCidade.Name = "txtCodigoCidade";
            this.txtCodigoCidade.Size = new System.Drawing.Size(81, 20);
            this.txtCodigoCidade.TabIndex = 64;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(108, 227);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 65;
            this.label15.Text = "Código Cidade";
            // 
            // btnCadastrarCidade
            // 
            this.btnCadastrarCidade.Location = new System.Drawing.Point(367, 242);
            this.btnCadastrarCidade.Name = "btnCadastrarCidade";
            this.btnCadastrarCidade.Size = new System.Drawing.Size(75, 23);
            this.btnCadastrarCidade.TabIndex = 66;
            this.btnCadastrarCidade.Text = "Cadastrar";
            this.btnCadastrarCidade.UseVisualStyleBackColor = true;
            this.btnCadastrarCidade.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmCadastroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCadastrarCidade);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCodigoCidade);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.comboCidade);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label32);
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
            this.Controls.Add(this.comboCondicoes);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCodigoCondicao);
            this.Controls.Add(this.txtCodigoForma);
            this.Controls.Add(this.comboFormas);
            this.Name = "FrmCadastroCliente";
            this.Text = "Cadastro Cliente";
            this.Load += new System.EventHandler(this.FrmCadastroCliente_Load_1);
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
            this.Controls.SetChildIndex(this.checkEstrangeiro, 0);
            this.Controls.SetChildIndex(this.comboFormas, 0);
            this.Controls.SetChildIndex(this.txtCodigoForma, 0);
            this.Controls.SetChildIndex(this.txtCodigoCondicao, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.comboCondicoes, 0);
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
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.comboCidade, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.txtCodigoCidade, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.btnCadastrarCidade, 0);
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
        private System.Windows.Forms.ComboBox comboFormas;
        private System.Windows.Forms.TextBox txtCodigoForma;
        private System.Windows.Forms.TextBox txtCodigoCondicao;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboCondicoes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
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
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox comboCidade;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCodigoCidade;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnCadastrarCidade;
    }
}