
namespace WindowsFormsApp1
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxLine = new System.Windows.Forms.CheckBox();
            this.checkBoxPoint = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxPan = new System.Windows.Forms.CheckBox();
            this.checkBoxBlackTheme = new System.Windows.Forms.CheckBox();
            this.checkBoxMeasurementMode = new System.Windows.Forms.CheckBox();
            this.checkBoxZoom = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(15, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "Построить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1290, 703);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // checkBoxLine
            // 
            this.checkBoxLine.AutoSize = true;
            this.checkBoxLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxLine.Location = new System.Drawing.Point(756, 37);
            this.checkBoxLine.Name = "checkBoxLine";
            this.checkBoxLine.Size = new System.Drawing.Size(58, 24);
            this.checkBoxLine.TabIndex = 2;
            this.checkBoxLine.Text = "Line";
            this.checkBoxLine.UseVisualStyleBackColor = true;
            this.checkBoxLine.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxPoint
            // 
            this.checkBoxPoint.AutoSize = true;
            this.checkBoxPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxPoint.Location = new System.Drawing.Point(855, 37);
            this.checkBoxPoint.Name = "checkBoxPoint";
            this.checkBoxPoint.Size = new System.Drawing.Size(64, 24);
            this.checkBoxPoint.TabIndex = 3;
            this.checkBoxPoint.Text = "Point";
            this.checkBoxPoint.UseVisualStyleBackColor = true;
            this.checkBoxPoint.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.checkBoxPan);
            this.groupBox2.Controls.Add(this.checkBoxBlackTheme);
            this.groupBox2.Controls.Add(this.checkBoxMeasurementMode);
            this.groupBox2.Controls.Add(this.checkBoxZoom);
            this.groupBox2.Controls.Add(this.checkBoxPoint);
            this.groupBox2.Controls.Add(this.checkBoxLine);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(123, 721);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1122, 82);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // checkBoxPan
            // 
            this.checkBoxPan.AutoSize = true;
            this.checkBoxPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxPan.Location = new System.Drawing.Point(313, 37);
            this.checkBoxPan.Name = "checkBoxPan";
            this.checkBoxPan.Size = new System.Drawing.Size(125, 24);
            this.checkBoxPan.TabIndex = 7;
            this.checkBoxPan.Text = "checkBoxPan";
            this.checkBoxPan.UseVisualStyleBackColor = true;
            this.checkBoxPan.CheckedChanged += new System.EventHandler(this.checkBoxPan_CheckedChanged);
            // 
            // checkBoxBlackTheme
            // 
            this.checkBoxBlackTheme.AutoSize = true;
            this.checkBoxBlackTheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxBlackTheme.Location = new System.Drawing.Point(945, 37);
            this.checkBoxBlackTheme.Name = "checkBoxBlackTheme";
            this.checkBoxBlackTheme.Size = new System.Drawing.Size(126, 24);
            this.checkBoxBlackTheme.TabIndex = 6;
            this.checkBoxBlackTheme.Text = "Темная тема";
            this.checkBoxBlackTheme.UseVisualStyleBackColor = true;
            this.checkBoxBlackTheme.CheckedChanged += new System.EventHandler(this.checkBoxBlackTheme_CheckedChanged);
            // 
            // checkBoxMeasurementMode
            // 
            this.checkBoxMeasurementMode.AutoSize = true;
            this.checkBoxMeasurementMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxMeasurementMode.Location = new System.Drawing.Point(474, 37);
            this.checkBoxMeasurementMode.Name = "checkBoxMeasurementMode";
            this.checkBoxMeasurementMode.Size = new System.Drawing.Size(164, 24);
            this.checkBoxMeasurementMode.TabIndex = 5;
            this.checkBoxMeasurementMode.Text = "Режим измерений";
            this.checkBoxMeasurementMode.UseVisualStyleBackColor = true;
            this.checkBoxMeasurementMode.CheckedChanged += new System.EventHandler(this.checkBoxMeasurementMode_CheckedChanged);
            // 
            // checkBoxZoom
            // 
            this.checkBoxZoom.AutoSize = true;
            this.checkBoxZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxZoom.Location = new System.Drawing.Point(657, 37);
            this.checkBoxZoom.Name = "checkBoxZoom";
            this.checkBoxZoom.Size = new System.Drawing.Size(69, 24);
            this.checkBoxZoom.TabIndex = 4;
            this.checkBoxZoom.Text = "Zoom";
            this.checkBoxZoom.UseVisualStyleBackColor = true;
            this.checkBoxZoom.CheckedChanged += new System.EventHandler(this.Zoom_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 816);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxLine;
        private System.Windows.Forms.CheckBox checkBoxPoint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxZoom;
        private System.Windows.Forms.CheckBox checkBoxMeasurementMode;
        private System.Windows.Forms.CheckBox checkBoxBlackTheme;
        private System.Windows.Forms.CheckBox checkBoxPan;
    }
}

