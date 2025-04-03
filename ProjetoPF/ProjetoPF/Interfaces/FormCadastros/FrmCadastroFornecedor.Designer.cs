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
            this.comboCidade = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtValorMin = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.comboCondicoes = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCodigoCondicao = new System.Windows.Forms.TextBox();
            this.txtCodigoForma = new System.Windows.Forms.TextBox();
            this.comboFormas = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
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
            "Jurídica"});
            // 
            // btnSalvar
            // 
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
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
            this.btnCadastrarCidade.Location = new System.Drawing.Point(353, 241);
            this.btnCadastrarCidade.Name = "btnCadastrarCidade";
            this.btnCadastrarCidade.Size = new System.Drawing.Size(75, 23);
            this.btnCadastrarCidade.TabIndex = 72;
            this.btnCadastrarCidade.Text = "Cadastrar";
            this.btnCadastrarCidade.UseVisualStyleBackColor = true;
            this.btnCadastrarCidade.Click += new System.EventHandler(this.btnCadastrarCidade_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(94, 228);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 71;
            this.label15.Text = "Código Cidade";
            // 
            // txtCodigoCidade
            // 
            this.txtCodigoCidade.Enabled = false;
            this.txtCodigoCidade.Location = new System.Drawing.Point(97, 244);
            this.txtCodigoCidade.Name = "txtCodigoCidade";
            this.txtCodigoCidade.Size = new System.Drawing.Size(81, 20);
            this.txtCodigoCidade.TabIndex = 70;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(185, 228);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 69;
            this.label14.Text = "Cidades";
            // 
            // comboCidade
            // 
            this.comboCidade.FormattingEnabled = true;
            this.comboCidade.Location = new System.Drawing.Point(187, 243);
            this.comboCidade.Name = "comboCidade";
            this.comboCidade.Size = new System.Drawing.Size(155, 21);
            this.comboCidade.TabIndex = 68;
            this.comboCidade.SelectedIndexChanged += new System.EventHandler(this.comboCidade_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Red;
            this.label36.Location = new System.Drawing.Point(236, 223);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(15, 20);
            this.label36.TabIndex = 67;
            this.label36.Text = "*";
            // 
            // txtValorMin
            // 
            this.txtValorMin.Location = new System.Drawing.Point(12, 393);
            this.txtValorMin.Name = "txtValorMin";
            this.txtValorMin.Size = new System.Drawing.Size(126, 20);
            this.txtValorMin.TabIndex = 73;
            this.txtValorMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 377);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 13);
            this.label17.TabIndex = 74;
            this.label17.Text = "Valor Minímo de Pedido";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.Red;
            this.label35.Location = new System.Drawing.Point(628, 318);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(15, 20);
            this.label35.TabIndex = 86;
            this.label35.Text = "*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.Red;
            this.label34.Location = new System.Drawing.Point(206, 321);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(15, 20);
            this.label34.TabIndex = 85;
            this.label34.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(516, 324);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(114, 13);
            this.label23.TabIndex = 84;
            this.label23.Text = "Condições Pagamento";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(421, 324);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 13);
            this.label22.TabIndex = 83;
            this.label22.Text = "Código Condição";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(713, 339);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 82;
            this.button2.Text = "Cadastrar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboCondicoes
            // 
            this.comboCondicoes.FormattingEnabled = true;
            this.comboCondicoes.Location = new System.Drawing.Point(519, 340);
            this.comboCondicoes.Name = "comboCondicoes";
            this.comboCondicoes.Size = new System.Drawing.Size(188, 21);
            this.comboCondicoes.TabIndex = 81;
            this.comboCondicoes.SelectedIndexChanged += new System.EventHandler(this.comboCondicoes_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(108, 324);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 13);
            this.label21.TabIndex = 80;
            this.label21.Text = "Formas Pagamento";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 324);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 79;
            this.label18.Text = "Código Forma";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 78;
            this.button1.Text = "Cadastrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCodigoCondicao
            // 
            this.txtCodigoCondicao.Enabled = false;
            this.txtCodigoCondicao.Location = new System.Drawing.Point(424, 341);
            this.txtCodigoCondicao.Name = "txtCodigoCondicao";
            this.txtCodigoCondicao.Size = new System.Drawing.Size(89, 20);
            this.txtCodigoCondicao.TabIndex = 77;
            this.txtCodigoCondicao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCodigoForma
            // 
            this.txtCodigoForma.Enabled = false;
            this.txtCodigoForma.Location = new System.Drawing.Point(12, 342);
            this.txtCodigoForma.Name = "txtCodigoForma";
            this.txtCodigoForma.Size = new System.Drawing.Size(89, 20);
            this.txtCodigoForma.TabIndex = 76;
            this.txtCodigoForma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboFormas
            // 
            this.comboFormas.FormattingEnabled = true;
            this.comboFormas.Location = new System.Drawing.Point(107, 342);
            this.comboFormas.Name = "comboFormas";
            this.comboFormas.Size = new System.Drawing.Size(188, 21);
            this.comboFormas.TabIndex = 75;
            this.comboFormas.SelectedIndexChanged += new System.EventHandler(this.comboFormas_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(177, 32);
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
            this.label20.Location = new System.Drawing.Point(114, 81);
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
            this.label24.Location = new System.Drawing.Point(45, 127);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(15, 20);
            this.label24.TabIndex = 89;
            this.label24.Text = "*";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(249, 128);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(15, 20);
            this.label25.TabIndex = 90;
            this.label25.Text = "*";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(37, 174);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 20);
            this.label26.TabIndex = 91;
            this.label26.Text = "*";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(315, 176);
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
            this.label28.Location = new System.Drawing.Point(413, 176);
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
            this.label29.Location = new System.Drawing.Point(628, 176);
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
            this.label30.Location = new System.Drawing.Point(70, 270);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(15, 20);
            this.label30.TabIndex = 95;
            this.label30.Text = "*";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(310, 270);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(15, 20);
            this.label31.TabIndex = 96;
            this.label31.Text = "*";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(446, 272);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(15, 20);
            this.label32.TabIndex = 97;
            this.label32.Text = "*";
            // 
            // FrmCadastroFornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboCondicoes);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCodigoCondicao);
            this.Controls.Add(this.txtCodigoForma);
            this.Controls.Add(this.comboFormas);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtValorMin);
            this.Controls.Add(this.btnCadastrarCidade);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCodigoCidade);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.comboCidade);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label7);
            this.Name = "FrmCadastroFornecedor";
            this.Text = "Cadastro Fornecedor";
            this.Load += new System.EventHandler(this.FrmCadastroFornecedor_Load);
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
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.comboCidade, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.txtCodigoCidade, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.btnCadastrarCidade, 0);
            this.Controls.SetChildIndex(this.txtValorMin, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.comboFormas, 0);
            this.Controls.SetChildIndex(this.txtCodigoForma, 0);
            this.Controls.SetChildIndex(this.txtCodigoCondicao, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.comboCondicoes, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.label30, 0);
            this.Controls.SetChildIndex(this.label31, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCadastrarCidade;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCodigoCidade;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboCidade;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtValorMin;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboCondicoes;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCodigoCondicao;
        private System.Windows.Forms.TextBox txtCodigoForma;
        private System.Windows.Forms.ComboBox comboFormas;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
    }
}
