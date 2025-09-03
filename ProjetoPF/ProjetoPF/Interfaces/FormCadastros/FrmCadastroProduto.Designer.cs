namespace ProjetoPF.Interfaces.FormCadastros
{
    partial class FrmCadastroProduto
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
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.txtUltCompra = new System.Windows.Forms.TextBox();
            this.txtEstoque = new System.Windows.Forms.TextBox();
            this.txtPrecoCusto = new System.Windows.Forms.TextBox();
            this.txtPrecoVenda = new System.Windows.Forms.TextBox();
            this.txtCodCategoria = new System.Windows.Forms.TextBox();
            this.txtCategoria = new System.Windows.Forms.TextBox();
            this.btnCategoria = new System.Windows.Forms.Button();
            this.btnFornecedor = new System.Windows.Forms.Button();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.txtCodFornecedor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnMedida = new System.Windows.Forms.Button();
            this.txtUnidadeMedida = new System.Windows.Forms.TextBox();
            this.txtCodUnidadeMedida = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnMarca = new System.Windows.Forms.Button();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.txtCodMarca = new System.Windows.Forms.TextBox();
            this.txtObser = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtMargemLucro = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSalvar
            // 
            this.btnSalvar.TabIndex = 9;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Enabled = false;
            // 
            // btnVoltar
            // 
            this.btnVoltar.TabIndex = 10;
            // 
            // txtProduto
            // 
            this.txtProduto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduto.Location = new System.Drawing.Point(189, 53);
            this.txtProduto.MaxLength = 50;
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(472, 20);
            this.txtProduto.TabIndex = 1;
            // 
            // txtUltCompra
            // 
            this.txtUltCompra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUltCompra.Enabled = false;
            this.txtUltCompra.Location = new System.Drawing.Point(1016, 116);
            this.txtUltCompra.Name = "txtUltCompra";
            this.txtUltCompra.Size = new System.Drawing.Size(311, 20);
            this.txtUltCompra.TabIndex = 0;
            this.txtUltCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEstoque
            // 
            this.txtEstoque.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstoque.Enabled = false;
            this.txtEstoque.Location = new System.Drawing.Point(12, 116);
            this.txtEstoque.Name = "txtEstoque";
            this.txtEstoque.Size = new System.Drawing.Size(153, 20);
            this.txtEstoque.TabIndex = 16;
            // 
            // txtPrecoCusto
            // 
            this.txtPrecoCusto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecoCusto.Enabled = false;
            this.txtPrecoCusto.Location = new System.Drawing.Point(189, 116);
            this.txtPrecoCusto.MaxLength = 10;
            this.txtPrecoCusto.Name = "txtPrecoCusto";
            this.txtPrecoCusto.Size = new System.Drawing.Size(250, 20);
            this.txtPrecoCusto.TabIndex = 3;
            this.txtPrecoCusto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPrecoVenda
            // 
            this.txtPrecoVenda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecoVenda.Location = new System.Drawing.Point(457, 116);
            this.txtPrecoVenda.MaxLength = 10;
            this.txtPrecoVenda.Name = "txtPrecoVenda";
            this.txtPrecoVenda.Size = new System.Drawing.Size(265, 20);
            this.txtPrecoVenda.TabIndex = 4;
            this.txtPrecoVenda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecoVenda.TextChanged += new System.EventHandler(this.txtPrecoVenda_TextChanged);
            // 
            // txtCodCategoria
            // 
            this.txtCodCategoria.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodCategoria.Enabled = false;
            this.txtCodCategoria.Location = new System.Drawing.Point(12, 185);
            this.txtCodCategoria.Name = "txtCodCategoria";
            this.txtCodCategoria.Size = new System.Drawing.Size(153, 20);
            this.txtCodCategoria.TabIndex = 0;
            this.txtCodCategoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCategoria
            // 
            this.txtCategoria.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCategoria.Enabled = false;
            this.txtCategoria.Location = new System.Drawing.Point(189, 185);
            this.txtCategoria.Name = "txtCategoria";
            this.txtCategoria.Size = new System.Drawing.Size(268, 20);
            this.txtCategoria.TabIndex = 0;
            // 
            // btnCategoria
            // 
            this.btnCategoria.Location = new System.Drawing.Point(481, 185);
            this.btnCategoria.Name = "btnCategoria";
            this.btnCategoria.Size = new System.Drawing.Size(180, 23);
            this.btnCategoria.TabIndex = 5;
            this.btnCategoria.Text = "Selecionar Categoria";
            this.btnCategoria.UseVisualStyleBackColor = true;
            this.btnCategoria.Click += new System.EventHandler(this.btnCategoria_Click);
            // 
            // btnFornecedor
            // 
            this.btnFornecedor.Location = new System.Drawing.Point(481, 253);
            this.btnFornecedor.Name = "btnFornecedor";
            this.btnFornecedor.Size = new System.Drawing.Size(180, 23);
            this.btnFornecedor.TabIndex = 7;
            this.btnFornecedor.Text = "Selecionar Fornecedor";
            this.btnFornecedor.UseVisualStyleBackColor = true;
            this.btnFornecedor.Click += new System.EventHandler(this.btnFornecedor_Click);
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFornecedor.Enabled = false;
            this.txtFornecedor.Location = new System.Drawing.Point(189, 253);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(268, 20);
            this.txtFornecedor.TabIndex = 0;
            // 
            // txtCodFornecedor
            // 
            this.txtCodFornecedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodFornecedor.Enabled = false;
            this.txtCodFornecedor.Location = new System.Drawing.Point(12, 253);
            this.txtCodFornecedor.Name = "txtCodFornecedor";
            this.txtCodFornecedor.Size = new System.Drawing.Size(153, 20);
            this.txtCodFornecedor.TabIndex = 0;
            this.txtCodFornecedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Produto:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Estoque";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(186, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Preço custo (R$):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(454, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Preço venda (R$):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1013, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Custo última compra (R$):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Código categoria:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(186, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Categoria:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 237);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Código fornecedor:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(186, 235);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Fornecedor:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(233, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 20);
            this.label12.TabIndex = 35;
            this.label12.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(544, 95);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 20);
            this.label15.TabIndex = 38;
            this.label15.Text = "*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(242, 164);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 20);
            this.label16.TabIndex = 39;
            this.label16.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(250, 231);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(15, 20);
            this.label17.TabIndex = 40;
            this.label17.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 310);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 13);
            this.label18.TabIndex = 42;
            this.label18.Text = "Observação:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(955, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 48;
            this.label3.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(855, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 47;
            this.label13.Text = "Unidade de Medida:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(678, 37);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 46;
            this.label19.Text = "Código medida:";
            // 
            // btnMedida
            // 
            this.btnMedida.Location = new System.Drawing.Point(1150, 53);
            this.btnMedida.Name = "btnMedida";
            this.btnMedida.Size = new System.Drawing.Size(180, 23);
            this.btnMedida.TabIndex = 2;
            this.btnMedida.Text = "Selecionar Medida";
            this.btnMedida.UseVisualStyleBackColor = true;
            this.btnMedida.Click += new System.EventHandler(this.btnMedida_Click);
            // 
            // txtUnidadeMedida
            // 
            this.txtUnidadeMedida.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnidadeMedida.Enabled = false;
            this.txtUnidadeMedida.Location = new System.Drawing.Point(858, 53);
            this.txtUnidadeMedida.Name = "txtUnidadeMedida";
            this.txtUnidadeMedida.Size = new System.Drawing.Size(268, 20);
            this.txtUnidadeMedida.TabIndex = 0;
            // 
            // txtCodUnidadeMedida
            // 
            this.txtCodUnidadeMedida.Enabled = false;
            this.txtCodUnidadeMedida.Location = new System.Drawing.Point(681, 53);
            this.txtCodUnidadeMedida.Name = "txtCodUnidadeMedida";
            this.txtCodUnidadeMedida.Size = new System.Drawing.Size(153, 20);
            this.txtCodUnidadeMedida.TabIndex = 0;
            this.txtCodUnidadeMedida.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(855, 170);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(40, 13);
            this.label21.TabIndex = 53;
            this.label21.Text = "Marca:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(678, 172);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(75, 13);
            this.label22.TabIndex = 52;
            this.label22.Text = "Código marca:";
            // 
            // btnMarca
            // 
            this.btnMarca.Location = new System.Drawing.Point(1150, 188);
            this.btnMarca.Name = "btnMarca";
            this.btnMarca.Size = new System.Drawing.Size(180, 23);
            this.btnMarca.TabIndex = 6;
            this.btnMarca.Text = "Selecionar Marca";
            this.btnMarca.UseVisualStyleBackColor = true;
            this.btnMarca.Click += new System.EventHandler(this.btnMarca_Click);
            // 
            // txtMarca
            // 
            this.txtMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMarca.Enabled = false;
            this.txtMarca.Location = new System.Drawing.Point(858, 188);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(268, 20);
            this.txtMarca.TabIndex = 0;
            // 
            // txtCodMarca
            // 
            this.txtCodMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodMarca.Enabled = false;
            this.txtCodMarca.Location = new System.Drawing.Point(681, 188);
            this.txtCodMarca.Name = "txtCodMarca";
            this.txtCodMarca.Size = new System.Drawing.Size(153, 20);
            this.txtCodMarca.TabIndex = 0;
            this.txtCodMarca.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtObser
            // 
            this.txtObser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObser.Location = new System.Drawing.Point(12, 338);
            this.txtObser.MaxLength = 500;
            this.txtObser.Multiline = true;
            this.txtObser.Name = "txtObser";
            this.txtObser.Size = new System.Drawing.Size(1315, 155);
            this.txtObser.TabIndex = 8;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(733, 100);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(116, 13);
            this.label20.TabIndex = 55;
            this.label20.Text = "Margem de Lucro (R$):";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMargemLucro
            // 
            this.txtMargemLucro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMargemLucro.Location = new System.Drawing.Point(735, 116);
            this.txtMargemLucro.MaxLength = 10;
            this.txtMargemLucro.Name = "txtMargemLucro";
            this.txtMargemLucro.Size = new System.Drawing.Size(265, 20);
            this.txtMargemLucro.TabIndex = 54;
            this.txtMargemLucro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMargemLucro.TextChanged += new System.EventHandler(this.txtMargemLucro_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(11, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 20);
            this.label14.TabIndex = 56;
            this.label14.Text = "Produtos";
            // 
            // FrmCadastroProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtMargemLucro);
            this.Controls.Add(this.txtObser);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.btnMarca);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.txtCodMarca);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnMedida);
            this.Controls.Add(this.txtUnidadeMedida);
            this.Controls.Add(this.txtCodUnidadeMedida);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFornecedor);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.txtCodFornecedor);
            this.Controls.Add(this.btnCategoria);
            this.Controls.Add(this.txtCategoria);
            this.Controls.Add(this.txtCodCategoria);
            this.Controls.Add(this.txtPrecoVenda);
            this.Controls.Add(this.txtPrecoCusto);
            this.Controls.Add(this.txtEstoque);
            this.Controls.Add(this.txtUltCompra);
            this.Controls.Add(this.txtProduto);
            this.Name = "FrmCadastroProduto";
            this.Text = "Cadastro Produto";
            this.Load += new System.EventHandler(this.FrmCadastroProduto_Load);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnVoltar, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.checkAtivo, 0);
            this.Controls.SetChildIndex(this.lblDataAtual, 0);
            this.Controls.SetChildIndex(this.DataAtaul, 0);
            this.Controls.SetChildIndex(this.labelDataCriacao, 0);
            this.Controls.SetChildIndex(this.labelCriacao, 0);
            this.Controls.SetChildIndex(this.lblAtualizacao, 0);
            this.Controls.SetChildIndex(this.txtProduto, 0);
            this.Controls.SetChildIndex(this.txtUltCompra, 0);
            this.Controls.SetChildIndex(this.txtEstoque, 0);
            this.Controls.SetChildIndex(this.txtPrecoCusto, 0);
            this.Controls.SetChildIndex(this.txtPrecoVenda, 0);
            this.Controls.SetChildIndex(this.txtCodCategoria, 0);
            this.Controls.SetChildIndex(this.txtCategoria, 0);
            this.Controls.SetChildIndex(this.btnCategoria, 0);
            this.Controls.SetChildIndex(this.txtCodFornecedor, 0);
            this.Controls.SetChildIndex(this.txtFornecedor, 0);
            this.Controls.SetChildIndex(this.btnFornecedor, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.txtCodUnidadeMedida, 0);
            this.Controls.SetChildIndex(this.txtUnidadeMedida, 0);
            this.Controls.SetChildIndex(this.btnMedida, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtCodMarca, 0);
            this.Controls.SetChildIndex(this.txtMarca, 0);
            this.Controls.SetChildIndex(this.btnMarca, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.txtObser, 0);
            this.Controls.SetChildIndex(this.txtMargemLucro, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.TextBox txtUltCompra;
        private System.Windows.Forms.TextBox txtEstoque;
        private System.Windows.Forms.TextBox txtPrecoCusto;
        private System.Windows.Forms.TextBox txtPrecoVenda;
        private System.Windows.Forms.Button btnCategoria;
        private System.Windows.Forms.Button btnFornecedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnMedida;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnMarca;
        public System.Windows.Forms.TextBox txtMarca;
        public System.Windows.Forms.TextBox txtCodMarca;
        public System.Windows.Forms.TextBox txtUnidadeMedida;
        public System.Windows.Forms.TextBox txtCodUnidadeMedida;
        public System.Windows.Forms.TextBox txtCodCategoria;
        public System.Windows.Forms.TextBox txtCategoria;
        public System.Windows.Forms.TextBox txtFornecedor;
        public System.Windows.Forms.TextBox txtCodFornecedor;
        private System.Windows.Forms.TextBox txtObser;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtMargemLucro;
        private System.Windows.Forms.Label label14;
    }
}
