namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmCadastroFuncionario
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
            this.btnCadastrarCidade = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCodigoCidade = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboCidade = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.txtSalario = new System.Windows.Forms.TextBox();
            this.txtCargaHoraria = new System.Windows.Forms.TextBox();
            this.comboTurno = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.Dataadm = new System.Windows.Forms.DateTimePicker();
            this.DataDem = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
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
            // btnCadastrarCidade
            // 
            this.btnCadastrarCidade.Location = new System.Drawing.Point(351, 241);
            this.btnCadastrarCidade.Name = "btnCadastrarCidade";
            this.btnCadastrarCidade.Size = new System.Drawing.Size(75, 23);
            this.btnCadastrarCidade.TabIndex = 78;
            this.btnCadastrarCidade.Text = "Cadastrar";
            this.btnCadastrarCidade.UseVisualStyleBackColor = true;
            this.btnCadastrarCidade.Click += new System.EventHandler(this.btnCadastrarCidade_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(92, 228);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 77;
            this.label15.Text = "Código Cidade";
            // 
            // txtCodigoCidade
            // 
            this.txtCodigoCidade.Enabled = false;
            this.txtCodigoCidade.Location = new System.Drawing.Point(95, 244);
            this.txtCodigoCidade.Name = "txtCodigoCidade";
            this.txtCodigoCidade.Size = new System.Drawing.Size(81, 20);
            this.txtCodigoCidade.TabIndex = 76;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(183, 228);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 75;
            this.label14.Text = "Cidades";
            // 
            // comboCidade
            // 
            this.comboCidade.FormattingEnabled = true;
            this.comboCidade.Location = new System.Drawing.Point(185, 243);
            this.comboCidade.Name = "comboCidade";
            this.comboCidade.Size = new System.Drawing.Size(155, 21);
            this.comboCidade.TabIndex = 74;
            this.comboCidade.SelectedIndexChanged += new System.EventHandler(this.comboCidade_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Red;
            this.label36.Location = new System.Drawing.Point(234, 223);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(15, 20);
            this.label36.TabIndex = 73;
            this.label36.Text = "*";
            // 
            // txtMatricula
            // 
            this.txtMatricula.Location = new System.Drawing.Point(302, 55);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(135, 20);
            this.txtMatricula.TabIndex = 79;
            // 
            // txtCargo
            // 
            this.txtCargo.Location = new System.Drawing.Point(12, 337);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.Size = new System.Drawing.Size(180, 20);
            this.txtCargo.TabIndex = 80;
            // 
            // txtSalario
            // 
            this.txtSalario.Location = new System.Drawing.Point(201, 337);
            this.txtSalario.Name = "txtSalario";
            this.txtSalario.Size = new System.Drawing.Size(150, 20);
            this.txtSalario.TabIndex = 81;
            // 
            // txtCargaHoraria
            // 
            this.txtCargaHoraria.Location = new System.Drawing.Point(201, 377);
            this.txtCargaHoraria.Name = "txtCargaHoraria";
            this.txtCargaHoraria.Size = new System.Drawing.Size(150, 20);
            this.txtCargaHoraria.TabIndex = 83;
            // 
            // comboTurno
            // 
            this.comboTurno.FormattingEnabled = true;
            this.comboTurno.Location = new System.Drawing.Point(12, 377);
            this.comboTurno.Name = "comboTurno";
            this.comboTurno.Size = new System.Drawing.Size(180, 21);
            this.comboTurno.TabIndex = 85;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(301, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 86;
            this.label7.Text = "Matricula";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 321);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 87;
            this.label17.Text = "Cargo";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(198, 321);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 88;
            this.label18.Text = "Salário";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 361);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 13);
            this.label19.TabIndex = 89;
            this.label19.Text = "Turno";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(201, 361);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 13);
            this.label20.TabIndex = 90;
            this.label20.Text = "Carga Horária";
            // 
            // Dataadm
            // 
            this.Dataadm.Location = new System.Drawing.Point(358, 337);
            this.Dataadm.Name = "Dataadm";
            this.Dataadm.Size = new System.Drawing.Size(200, 20);
            this.Dataadm.TabIndex = 91;
            // 
            // DataDem
            // 
            this.DataDem.Location = new System.Drawing.Point(358, 377);
            this.DataDem.Name = "DataDem";
            this.DataDem.Size = new System.Drawing.Size(200, 20);
            this.DataDem.TabIndex = 92;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(359, 321);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 13);
            this.label21.TabIndex = 93;
            this.label21.Text = "Data admissão";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(359, 361);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 13);
            this.label22.TabIndex = 94;
            this.label22.Text = "Data Demissão";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(12, 12);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 20);
            this.label23.TabIndex = 95;
            this.label23.Text = "Funcionário";
            // 
            // FrmCadastroFuncionario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.DataDem);
            this.Controls.Add(this.Dataadm);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboTurno);
            this.Controls.Add(this.txtCargaHoraria);
            this.Controls.Add(this.txtSalario);
            this.Controls.Add(this.txtCargo);
            this.Controls.Add(this.txtMatricula);
            this.Controls.Add(this.btnCadastrarCidade);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCodigoCidade);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.comboCidade);
            this.Controls.Add(this.label36);
            this.Name = "FrmCadastroFuncionario";
            this.Text = "Cadastro Funcionário";
            this.Load += new System.EventHandler(this.FrmCadastroFuncionario_Load);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lblClassificacao, 0);
            this.Controls.SetChildIndex(this.label16, 0);
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
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnVoltar, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.comboCidade, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.txtCodigoCidade, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.btnCadastrarCidade, 0);
            this.Controls.SetChildIndex(this.txtMatricula, 0);
            this.Controls.SetChildIndex(this.txtCargo, 0);
            this.Controls.SetChildIndex(this.txtSalario, 0);
            this.Controls.SetChildIndex(this.txtCargaHoraria, 0);
            this.Controls.SetChildIndex(this.comboTurno, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.Dataadm, 0);
            this.Controls.SetChildIndex(this.DataDem, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCadastrarCidade;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCodigoCidade;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboCidade;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.TextBox txtSalario;
        private System.Windows.Forms.TextBox txtCargaHoraria;
        private System.Windows.Forms.ComboBox comboTurno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker Dataadm;
        private System.Windows.Forms.DateTimePicker DataDem;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
    }
}
