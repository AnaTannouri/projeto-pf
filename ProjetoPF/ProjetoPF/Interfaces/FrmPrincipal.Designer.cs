namespace ProjetoPF
{
    partial class FrmPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.pagamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formaDePagamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.condiçãoDePagamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localizaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paísToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cidadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fornecedorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.funcionárioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pagamentoToolStripMenuItem,
            this.localizaçãoToolStripMenuItem,
            this.clienteToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // pagamentoToolStripMenuItem
            // 
            this.pagamentoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formaDePagamentoToolStripMenuItem,
            this.condiçãoDePagamentoToolStripMenuItem});
            this.pagamentoToolStripMenuItem.Name = "pagamentoToolStripMenuItem";
            this.pagamentoToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.pagamentoToolStripMenuItem.Text = "Pagamento";
            // 
            // formaDePagamentoToolStripMenuItem
            // 
            this.formaDePagamentoToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.formaDePagamentoToolStripMenuItem.Name = "formaDePagamentoToolStripMenuItem";
            this.formaDePagamentoToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.formaDePagamentoToolStripMenuItem.Text = "Forma de Pagamento";
            this.formaDePagamentoToolStripMenuItem.Click += new System.EventHandler(this.formaDePagamentoToolStripMenuItem_Click);
            // 
            // condiçãoDePagamentoToolStripMenuItem
            // 
            this.condiçãoDePagamentoToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.condiçãoDePagamentoToolStripMenuItem.Name = "condiçãoDePagamentoToolStripMenuItem";
            this.condiçãoDePagamentoToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.condiçãoDePagamentoToolStripMenuItem.Text = "Condição de Pagamento";
            this.condiçãoDePagamentoToolStripMenuItem.Click += new System.EventHandler(this.condiçãoDePagamentoToolStripMenuItem_Click);
            // 
            // localizaçãoToolStripMenuItem
            // 
            this.localizaçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paísToolStripMenuItem,
            this.estadoToolStripMenuItem,
            this.cidadeToolStripMenuItem});
            this.localizaçãoToolStripMenuItem.Name = "localizaçãoToolStripMenuItem";
            this.localizaçãoToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.localizaçãoToolStripMenuItem.Text = "Localização";
            // 
            // paísToolStripMenuItem
            // 
            this.paísToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.paísToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.paísToolStripMenuItem.Name = "paísToolStripMenuItem";
            this.paísToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.paísToolStripMenuItem.Text = "País";
            this.paísToolStripMenuItem.Click += new System.EventHandler(this.paísToolStripMenuItem_Click);
            // 
            // estadoToolStripMenuItem
            // 
            this.estadoToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.estadoToolStripMenuItem.Name = "estadoToolStripMenuItem";
            this.estadoToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.estadoToolStripMenuItem.Text = "Estado";
            this.estadoToolStripMenuItem.Click += new System.EventHandler(this.estadoToolStripMenuItem_Click);
            // 
            // cidadeToolStripMenuItem
            // 
            this.cidadeToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cidadeToolStripMenuItem.Name = "cidadeToolStripMenuItem";
            this.cidadeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.cidadeToolStripMenuItem.Text = "Cidade";
            this.cidadeToolStripMenuItem.Click += new System.EventHandler(this.cidadeToolStripMenuItem_Click);
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clienteToolStripMenuItem1,
            this.fornecedorToolStripMenuItem1,
            this.funcionárioToolStripMenuItem1});
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.clienteToolStripMenuItem.Text = "Pessoa";
            // 
            // clienteToolStripMenuItem1
            // 
            this.clienteToolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.clienteToolStripMenuItem1.Name = "clienteToolStripMenuItem1";
            this.clienteToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.clienteToolStripMenuItem1.Text = "Cliente";
            this.clienteToolStripMenuItem1.Click += new System.EventHandler(this.clienteToolStripMenuItem1_Click);
            // 
            // fornecedorToolStripMenuItem1
            // 
            this.fornecedorToolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.fornecedorToolStripMenuItem1.Name = "fornecedorToolStripMenuItem1";
            this.fornecedorToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.fornecedorToolStripMenuItem1.Text = "Fornecedor";
            this.fornecedorToolStripMenuItem1.Click += new System.EventHandler(this.fornecedorToolStripMenuItem1_Click);
            // 
            // funcionárioToolStripMenuItem1
            // 
            this.funcionárioToolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.funcionárioToolStripMenuItem1.Name = "funcionárioToolStripMenuItem1";
            this.funcionárioToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.funcionárioToolStripMenuItem1.Text = "Funcionário";
            this.funcionárioToolStripMenuItem1.Click += new System.EventHandler(this.funcionárioToolStripMenuItem1_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.Text = "Menu Principal";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem pagamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formaDePagamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem condiçãoDePagamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localizaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paísToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cidadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fornecedorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem funcionárioToolStripMenuItem1;
    }
}

