

namespace Compilador
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cuadroTexto = new System.Windows.Forms.TextBox();
            this.arbolSintax = new System.Windows.Forms.TreeView();
            this.cuadroResultados = new System.Windows.Forms.TextBox();
            this.btnLexico = new System.Windows.Forms.Button();
            this.tablaSimbolos = new System.Windows.Forms.DataGridView();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSintac = new System.Windows.Forms.Button();
            this.btnSem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSimbolos)).BeginInit();
            this.SuspendLayout();
            // 
            // cuadroTexto
            // 
            this.cuadroTexto.AcceptsReturn = true;
            this.cuadroTexto.BackColor = System.Drawing.SystemColors.Window;
            this.cuadroTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuadroTexto.ForeColor = System.Drawing.SystemColors.Desktop;
            this.cuadroTexto.Location = new System.Drawing.Point(166, 126);
            this.cuadroTexto.Margin = new System.Windows.Forms.Padding(4);
            this.cuadroTexto.Multiline = true;
            this.cuadroTexto.Name = "cuadroTexto";
            this.cuadroTexto.Size = new System.Drawing.Size(425, 409);
            this.cuadroTexto.TabIndex = 1;
            // 
            // arbolSintax
            // 
            this.arbolSintax.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.arbolSintax.Location = new System.Drawing.Point(599, 558);
            this.arbolSintax.Margin = new System.Windows.Forms.Padding(4);
            this.arbolSintax.Name = "arbolSintax";
            this.arbolSintax.Size = new System.Drawing.Size(469, 212);
            this.arbolSintax.TabIndex = 1;
            // 
            // cuadroResultados
            // 
            this.cuadroResultados.AcceptsReturn = true;
            this.cuadroResultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuadroResultados.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cuadroResultados.Location = new System.Drawing.Point(599, 126);
            this.cuadroResultados.Margin = new System.Windows.Forms.Padding(4);
            this.cuadroResultados.Multiline = true;
            this.cuadroResultados.Name = "cuadroResultados";
            this.cuadroResultados.Size = new System.Drawing.Size(554, 409);
            this.cuadroResultados.TabIndex = 0;
            this.cuadroResultados.TextChanged += new System.EventHandler(this.cuadroResultados_TextChanged);
            // 
            // btnLexico
            // 
            this.btnLexico.BackColor = System.Drawing.Color.DarkBlue;
            this.btnLexico.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLexico.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLexico.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLexico.Location = new System.Drawing.Point(46, 217);
            this.btnLexico.Name = "btnLexico";
            this.btnLexico.Size = new System.Drawing.Size(98, 49);
            this.btnLexico.TabIndex = 11;
            this.btnLexico.Text = "Analizador Léxico";
            this.btnLexico.UseVisualStyleBackColor = false;
            this.btnLexico.Click += new System.EventHandler(this.button1_Click);
            // 
            // tablaSimbolos
            // 
            this.tablaSimbolos.AllowUserToAddRows = false;
            this.tablaSimbolos.AllowUserToDeleteRows = false;
            this.tablaSimbolos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tablaSimbolos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tablaSimbolos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.tablaSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaSimbolos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable,
            this.Tipo,
            this.Valor});
            this.tablaSimbolos.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tablaSimbolos.Location = new System.Drawing.Point(185, 558);
            this.tablaSimbolos.Margin = new System.Windows.Forms.Padding(4);
            this.tablaSimbolos.Name = "tablaSimbolos";
            this.tablaSimbolos.ReadOnly = true;
            this.tablaSimbolos.RowHeadersVisible = false;
            this.tablaSimbolos.RowHeadersWidth = 60;
            this.tablaSimbolos.Size = new System.Drawing.Size(379, 212);
            this.tablaSimbolos.TabIndex = 0;
            // 
            // Variable
            // 
            this.Variable.HeaderText = "Variable";
            this.Variable.MinimumWidth = 6;
            this.Variable.Name = "Variable";
            this.Variable.ReadOnly = true;
            this.Variable.Width = 125;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.MinimumWidth = 6;
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 125;
            // 
            // Valor
            // 
            this.Valor.HeaderText = "Valor";
            this.Valor.MinimumWidth = 6;
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            this.Valor.Width = 125;
            // 
            // btnSintac
            // 
            this.btnSintac.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSintac.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSintac.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSintac.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSintac.Location = new System.Drawing.Point(46, 290);
            this.btnSintac.Name = "btnSintac";
            this.btnSintac.Size = new System.Drawing.Size(98, 49);
            this.btnSintac.TabIndex = 13;
            this.btnSintac.Text = "Analizador Sintáctico";
            this.btnSintac.UseVisualStyleBackColor = false;
            this.btnSintac.Click += new System.EventHandler(this.btnSintac_Click);
            // 
            // btnSem
            // 
            this.btnSem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSem.Location = new System.Drawing.Point(46, 363);
            this.btnSem.Name = "btnSem";
            this.btnSem.Size = new System.Drawing.Size(98, 49);
            this.btnSem.TabIndex = 14;
            this.btnSem.Text = "Analizador Semántico";
            this.btnSem.UseVisualStyleBackColor = false;
            this.btnSem.Click += new System.EventHandler(this.btnSem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(791, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 29);
            this.label1.TabIndex = 15;
            this.label1.Text = "Resultados";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(46, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 49);
            this.button1.TabIndex = 16;
            this.button1.Text = "Código Intermedio";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(307, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 29);
            this.label2.TabIndex = 17;
            this.label2.Text = "Código";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DarkBlue;
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(1173, 401);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 106);
            this.button4.TabIndex = 20;
            this.button4.Text = "Guardar a C";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkBlue;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(1173, 305);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 69);
            this.button3.TabIndex = 19;
            this.button3.Text = "Borrar";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkBlue;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(1173, 210);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 69);
            this.button2.TabIndex = 18;
            this.button2.Text = "Abrir";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 828);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tablaSimbolos);
            this.Controls.Add(this.btnSem);
            this.Controls.Add(this.btnSintac);
            this.Controls.Add(this.btnLexico);
            this.Controls.Add(this.cuadroResultados);
            this.Controls.Add(this.cuadroTexto);
            this.Controls.Add(this.arbolSintax);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1300, 875);
            this.MinimumSize = new System.Drawing.Size(1300, 875);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Compilador";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaSimbolos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox cuadroTexto;
        private System.Windows.Forms.TextBox cuadroResultados;
        private System.Windows.Forms.TreeView arbolSintax;
        private System.Windows.Forms.DataGridView tablaSimbolos;
        private System.Windows.Forms.Button btnLexico;
        private System.Windows.Forms.Button btnSintac;
        private System.Windows.Forms.Button btnSem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
    }
}

