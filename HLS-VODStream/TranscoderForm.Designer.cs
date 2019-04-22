namespace HLS_VODStream
{
    partial class TranscoderForm
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
            this.btnPickFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.brnCancel = new System.Windows.Forms.Button();
            this.btnTranscode = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // btnPickFile
            // 
            this.btnPickFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPickFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPickFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPickFile.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnPickFile.Location = new System.Drawing.Point(389, 35);
            this.btnPickFile.Name = "btnPickFile";
            this.btnPickFile.Size = new System.Drawing.Size(72, 22);
            this.btnPickFile.TabIndex = 37;
            this.btnPickFile.Text = "Browse..";
            this.btnPickFile.UseVisualStyleBackColor = false;
            this.btnPickFile.Click += new System.EventHandler(this.btnPickFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.BackColor = System.Drawing.Color.White;
            this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFile.Enabled = false;
            this.txtFile.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.txtFile.Location = new System.Drawing.Point(37, 35);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(353, 22);
            this.txtFile.TabIndex = 38;
            // 
            // brnCancel
            // 
            this.brnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.brnCancel.BackColor = System.Drawing.Color.Crimson;
            this.brnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.brnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.brnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brnCancel.ForeColor = System.Drawing.Color.White;
            this.brnCancel.Location = new System.Drawing.Point(320, 89);
            this.brnCancel.Name = "brnCancel";
            this.brnCancel.Size = new System.Drawing.Size(141, 36);
            this.brnCancel.TabIndex = 39;
            this.brnCancel.Text = "Cancel";
            this.brnCancel.UseVisualStyleBackColor = false;
            this.brnCancel.Click += new System.EventHandler(this.brnCancel_Click);
            // 
            // btnTranscode
            // 
            this.btnTranscode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTranscode.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnTranscode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTranscode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTranscode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTranscode.ForeColor = System.Drawing.Color.White;
            this.btnTranscode.Location = new System.Drawing.Point(37, 89);
            this.btnTranscode.Name = "btnTranscode";
            this.btnTranscode.Size = new System.Drawing.Size(277, 36);
            this.btnTranscode.TabIndex = 40;
            this.btnTranscode.Text = "Transcode";
            this.btnTranscode.UseVisualStyleBackColor = false;
            this.btnTranscode.Click += new System.EventHandler(this.btnTranscode_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(37, 62);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(424, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 41;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 125);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transcoder";
            // 
            // TranscoderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 150);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.brnCancel);
            this.Controls.Add(this.btnTranscode);
            this.Controls.Add(this.btnPickFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(510, 189);
            this.MinimumSize = new System.Drawing.Size(510, 189);
            this.Name = "TranscoderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transcoder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPickFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button brnCancel;
        private System.Windows.Forms.Button btnTranscode;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

