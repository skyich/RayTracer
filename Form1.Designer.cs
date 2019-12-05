namespace RayTracer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkLight1 = new System.Windows.Forms.CheckBox();
            this.checkLight2 = new System.Windows.Forms.CheckBox();
            this.checkCommon = new System.Windows.Forms.CheckBox();
            this.checkMirror = new System.Windows.Forms.CheckBox();
            this.checkGlass = new System.Windows.Forms.CheckBox();
            this.checkMirrorWall = new System.Windows.Forms.CheckBox();
            this.buttonRender = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 768);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // checkLight1
            // 
            this.checkLight1.AutoSize = true;
            this.checkLight1.Location = new System.Drawing.Point(1044, 34);
            this.checkLight1.Name = "checkLight1";
            this.checkLight1.Size = new System.Drawing.Size(184, 21);
            this.checkLight1.TabIndex = 1;
            this.checkLight1.Text = "Второй источник света";
            this.checkLight1.UseVisualStyleBackColor = true;
            // 
            // checkLight2
            // 
            this.checkLight2.AutoSize = true;
            this.checkLight2.Location = new System.Drawing.Point(1044, 61);
            this.checkLight2.Name = "checkLight2";
            this.checkLight2.Size = new System.Drawing.Size(184, 21);
            this.checkLight2.TabIndex = 2;
            this.checkLight2.Text = "Третий источник света";
            this.checkLight2.UseVisualStyleBackColor = true;
            // 
            // checkCommon
            // 
            this.checkCommon.AutoSize = true;
            this.checkCommon.Location = new System.Drawing.Point(1044, 88);
            this.checkCommon.Name = "checkCommon";
            this.checkCommon.Size = new System.Drawing.Size(124, 21);
            this.checkCommon.TabIndex = 3;
            this.checkCommon.Text = "Обычный шар";
            this.checkCommon.UseVisualStyleBackColor = true;
            // 
            // checkMirror
            // 
            this.checkMirror.AutoSize = true;
            this.checkMirror.Location = new System.Drawing.Point(1044, 115);
            this.checkMirror.Name = "checkMirror";
            this.checkMirror.Size = new System.Drawing.Size(142, 21);
            this.checkMirror.TabIndex = 4;
            this.checkMirror.Text = "Зеркальный шар";
            this.checkMirror.UseVisualStyleBackColor = true;
            // 
            // checkGlass
            // 
            this.checkGlass.AutoSize = true;
            this.checkGlass.Location = new System.Drawing.Point(1044, 142);
            this.checkGlass.Name = "checkGlass";
            this.checkGlass.Size = new System.Drawing.Size(142, 21);
            this.checkGlass.TabIndex = 5;
            this.checkGlass.Text = "Стеклянный шар";
            this.checkGlass.UseVisualStyleBackColor = true;
            // 
            // checkMirrorWall
            // 
            this.checkMirrorWall.AutoSize = true;
            this.checkMirrorWall.Location = new System.Drawing.Point(1044, 169);
            this.checkMirrorWall.Name = "checkMirrorWall";
            this.checkMirrorWall.Size = new System.Drawing.Size(151, 21);
            this.checkMirrorWall.TabIndex = 6;
            this.checkMirrorWall.Text = "Зеркальная стена";
            this.checkMirrorWall.UseVisualStyleBackColor = true;
            // 
            // buttonRender
            // 
            this.buttonRender.Location = new System.Drawing.Point(1044, 266);
            this.buttonRender.Name = "buttonRender";
            this.buttonRender.Size = new System.Drawing.Size(142, 41);
            this.buttonRender.TabIndex = 7;
            this.buttonRender.Text = "Render";
            this.buttonRender.UseVisualStyleBackColor = true;
            this.buttonRender.Click += new System.EventHandler(this.ButtonRender_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1044, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Глубина просчета";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(1047, 224);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 22);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 773);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRender);
            this.Controls.Add(this.checkMirrorWall);
            this.Controls.Add(this.checkGlass);
            this.Controls.Add(this.checkMirror);
            this.Controls.Add(this.checkCommon);
            this.Controls.Add(this.checkLight2);
            this.Controls.Add(this.checkLight1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Cornell Box";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkLight1;
        private System.Windows.Forms.CheckBox checkLight2;
        private System.Windows.Forms.CheckBox checkCommon;
        private System.Windows.Forms.CheckBox checkMirror;
        private System.Windows.Forms.CheckBox checkGlass;
        private System.Windows.Forms.CheckBox checkMirrorWall;
        private System.Windows.Forms.Button buttonRender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

