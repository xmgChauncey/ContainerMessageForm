namespace ContainerMessageForm
{
    partial class ContainerMessageForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Floder = new System.Windows.Forms.Label();
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.button_OpenFolder = new System.Windows.Forms.Button();
            this.label_TotalFileCount = new System.Windows.Forms.Label();
            this.textBox_totalFileCount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_Floder
            // 
            this.label_Floder.AutoSize = true;
            this.label_Floder.Location = new System.Drawing.Point(12, 16);
            this.label_Floder.Name = "label_Floder";
            this.label_Floder.Size = new System.Drawing.Size(41, 12);
            this.label_Floder.TabIndex = 0;
            this.label_Floder.Text = "文件夹";
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.Location = new System.Drawing.Point(61, 13);
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.Size = new System.Drawing.Size(418, 21);
            this.textBox_Folder.TabIndex = 1;
            // 
            // button_OpenFolder
            // 
            this.button_OpenFolder.Location = new System.Drawing.Point(485, 12);
            this.button_OpenFolder.Name = "button_OpenFolder";
            this.button_OpenFolder.Size = new System.Drawing.Size(75, 23);
            this.button_OpenFolder.TabIndex = 2;
            this.button_OpenFolder.Text = "打开文件夹";
            this.button_OpenFolder.UseVisualStyleBackColor = true;
            this.button_OpenFolder.Click += new System.EventHandler(this.button_OpenFolder_Click);
            // 
            // label_TotalFileCount
            // 
            this.label_TotalFileCount.AutoSize = true;
            this.label_TotalFileCount.Location = new System.Drawing.Point(14, 64);
            this.label_TotalFileCount.Name = "label_TotalFileCount";
            this.label_TotalFileCount.Size = new System.Drawing.Size(53, 12);
            this.label_TotalFileCount.TabIndex = 3;
            this.label_TotalFileCount.Text = "总报文数";
            // 
            // textBox_totalFileCount
            // 
            this.textBox_totalFileCount.Location = new System.Drawing.Point(73, 61);
            this.textBox_totalFileCount.Name = "textBox_totalFileCount";
            this.textBox_totalFileCount.Size = new System.Drawing.Size(100, 21);
            this.textBox_totalFileCount.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 450);
            this.Controls.Add(this.textBox_totalFileCount);
            this.Controls.Add(this.label_TotalFileCount);
            this.Controls.Add(this.button_OpenFolder);
            this.Controls.Add(this.textBox_Folder);
            this.Controls.Add(this.label_Floder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Floder;
        private System.Windows.Forms.TextBox textBox_Folder;
        private System.Windows.Forms.Button button_OpenFolder;
        private System.Windows.Forms.Label label_TotalFileCount;
        private System.Windows.Forms.TextBox textBox_totalFileCount;
    }
}

