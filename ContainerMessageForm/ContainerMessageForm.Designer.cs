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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label_Floder = new System.Windows.Forms.Label();
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.button_OpenFolder = new System.Windows.Forms.Button();
            this.label_TotalFileCount = new System.Windows.Forms.Label();
            this.textBox_totalFileCount = new System.Windows.Forms.TextBox();
            this.progressBar_DataDealProcess = new System.Windows.Forms.ProgressBar();
            this.label_DataDealProcess = new System.Windows.Forms.Label();
            this.groupBox_FileOperate = new System.Windows.Forms.GroupBox();
            this.groupBox_DataAnalyze = new System.Windows.Forms.GroupBox();
            this.button_StartAnalyze = new System.Windows.Forms.Button();
            this.groupBox_AnalyzeResultOperate = new System.Windows.Forms.GroupBox();
            this.button_DeriviveToExcel = new System.Windows.Forms.Button();
            this.dataGridView_AnalyzeResult = new System.Windows.Forms.DataGridView();
            this.button_SearchFileInIE = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button_CleanAnalyzeResult = new System.Windows.Forms.Button();
            this.button_GetAnalyzeResult = new System.Windows.Forms.Button();
            this.button_CreateExcel = new System.Windows.Forms.Button();
            this.groupBox_FileOperate.SuspendLayout();
            this.groupBox_DataAnalyze.SuspendLayout();
            this.groupBox_AnalyzeResultOperate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AnalyzeResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Floder
            // 
            this.label_Floder.AutoSize = true;
            this.label_Floder.Location = new System.Drawing.Point(6, 28);
            this.label_Floder.Name = "label_Floder";
            this.label_Floder.Size = new System.Drawing.Size(41, 12);
            this.label_Floder.TabIndex = 0;
            this.label_Floder.Text = "文件夹";
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.Location = new System.Drawing.Point(65, 25);
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.Size = new System.Drawing.Size(406, 21);
            this.textBox_Folder.TabIndex = 1;
            // 
            // button_OpenFolder
            // 
            this.button_OpenFolder.Location = new System.Drawing.Point(477, 24);
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
            this.label_TotalFileCount.Location = new System.Drawing.Point(6, 76);
            this.label_TotalFileCount.Name = "label_TotalFileCount";
            this.label_TotalFileCount.Size = new System.Drawing.Size(53, 12);
            this.label_TotalFileCount.TabIndex = 3;
            this.label_TotalFileCount.Text = "总报文数";
            // 
            // textBox_totalFileCount
            // 
            this.textBox_totalFileCount.Location = new System.Drawing.Point(65, 73);
            this.textBox_totalFileCount.Name = "textBox_totalFileCount";
            this.textBox_totalFileCount.Size = new System.Drawing.Size(100, 21);
            this.textBox_totalFileCount.TabIndex = 4;
            // 
            // progressBar_DataDealProcess
            // 
            this.progressBar_DataDealProcess.Location = new System.Drawing.Point(65, 64);
            this.progressBar_DataDealProcess.Name = "progressBar_DataDealProcess";
            this.progressBar_DataDealProcess.Size = new System.Drawing.Size(406, 23);
            this.progressBar_DataDealProcess.TabIndex = 5;
            // 
            // label_DataDealProcess
            // 
            this.label_DataDealProcess.AutoSize = true;
            this.label_DataDealProcess.Location = new System.Drawing.Point(227, 101);
            this.label_DataDealProcess.Name = "label_DataDealProcess";
            this.label_DataDealProcess.Size = new System.Drawing.Size(83, 12);
            this.label_DataDealProcess.TabIndex = 6;
            this.label_DataDealProcess.Text = "0% Processing";
            // 
            // groupBox_FileOperate
            // 
            this.groupBox_FileOperate.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox_FileOperate.Controls.Add(this.label_Floder);
            this.groupBox_FileOperate.Controls.Add(this.textBox_Folder);
            this.groupBox_FileOperate.Controls.Add(this.button_OpenFolder);
            this.groupBox_FileOperate.Controls.Add(this.textBox_totalFileCount);
            this.groupBox_FileOperate.Controls.Add(this.label_TotalFileCount);
            this.groupBox_FileOperate.Location = new System.Drawing.Point(12, 12);
            this.groupBox_FileOperate.Name = "groupBox_FileOperate";
            this.groupBox_FileOperate.Size = new System.Drawing.Size(556, 106);
            this.groupBox_FileOperate.TabIndex = 7;
            this.groupBox_FileOperate.TabStop = false;
            this.groupBox_FileOperate.Text = "文件操作";
            // 
            // groupBox_DataAnalyze
            // 
            this.groupBox_DataAnalyze.Controls.Add(this.button_StartAnalyze);
            this.groupBox_DataAnalyze.Controls.Add(this.progressBar_DataDealProcess);
            this.groupBox_DataAnalyze.Controls.Add(this.label_DataDealProcess);
            this.groupBox_DataAnalyze.Location = new System.Drawing.Point(12, 134);
            this.groupBox_DataAnalyze.Name = "groupBox_DataAnalyze";
            this.groupBox_DataAnalyze.Size = new System.Drawing.Size(556, 127);
            this.groupBox_DataAnalyze.TabIndex = 8;
            this.groupBox_DataAnalyze.TabStop = false;
            this.groupBox_DataAnalyze.Text = "文件分析操作";
            // 
            // button_StartAnalyze
            // 
            this.button_StartAnalyze.Location = new System.Drawing.Point(229, 20);
            this.button_StartAnalyze.Name = "button_StartAnalyze";
            this.button_StartAnalyze.Size = new System.Drawing.Size(75, 23);
            this.button_StartAnalyze.TabIndex = 7;
            this.button_StartAnalyze.Text = "开始分析";
            this.button_StartAnalyze.UseVisualStyleBackColor = true;
            this.button_StartAnalyze.Click += new System.EventHandler(this.button_StartAnalyze_Click);
            // 
            // groupBox_AnalyzeResultOperate
            // 
            this.groupBox_AnalyzeResultOperate.Controls.Add(this.button_CreateExcel);
            this.groupBox_AnalyzeResultOperate.Controls.Add(this.button_DeriviveToExcel);
            this.groupBox_AnalyzeResultOperate.Controls.Add(this.dataGridView_AnalyzeResult);
            this.groupBox_AnalyzeResultOperate.Controls.Add(this.button_CleanAnalyzeResult);
            this.groupBox_AnalyzeResultOperate.Controls.Add(this.button_GetAnalyzeResult);
            this.groupBox_AnalyzeResultOperate.Location = new System.Drawing.Point(12, 282);
            this.groupBox_AnalyzeResultOperate.Name = "groupBox_AnalyzeResultOperate";
            this.groupBox_AnalyzeResultOperate.Size = new System.Drawing.Size(556, 277);
            this.groupBox_AnalyzeResultOperate.TabIndex = 9;
            this.groupBox_AnalyzeResultOperate.TabStop = false;
            this.groupBox_AnalyzeResultOperate.Text = "分析结果操作";
            // 
            // button_DeriviveToExcel
            // 
            this.button_DeriviveToExcel.Location = new System.Drawing.Point(350, 20);
            this.button_DeriviveToExcel.Name = "button_DeriviveToExcel";
            this.button_DeriviveToExcel.Size = new System.Drawing.Size(88, 23);
            this.button_DeriviveToExcel.TabIndex = 3;
            this.button_DeriviveToExcel.Text = "导出分析结果";
            this.button_DeriviveToExcel.UseVisualStyleBackColor = true;
            this.button_DeriviveToExcel.Click += new System.EventHandler(this.button_DeriviveToExcel_Click);
            // 
            // dataGridView_AnalyzeResult
            // 
            this.dataGridView_AnalyzeResult.AllowUserToAddRows = false;
            this.dataGridView_AnalyzeResult.AllowUserToDeleteRows = false;
            this.dataGridView_AnalyzeResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_AnalyzeResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_AnalyzeResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView_AnalyzeResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView_AnalyzeResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_AnalyzeResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.button_SearchFileInIE});
            this.dataGridView_AnalyzeResult.Location = new System.Drawing.Point(8, 61);
            this.dataGridView_AnalyzeResult.Name = "dataGridView_AnalyzeResult";
            this.dataGridView_AnalyzeResult.ReadOnly = true;
            this.dataGridView_AnalyzeResult.RowTemplate.Height = 23;
            this.dataGridView_AnalyzeResult.Size = new System.Drawing.Size(542, 200);
            this.dataGridView_AnalyzeResult.TabIndex = 2;
            this.dataGridView_AnalyzeResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_AnalyzeResult_CellContentClick);
            // 
            // button_SearchFileInIE
            // 
            this.button_SearchFileInIE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.NullValue = "浏览";
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Aqua;
            this.button_SearchFileInIE.DefaultCellStyle = dataGridViewCellStyle1;
            this.button_SearchFileInIE.FillWeight = 80F;
            this.button_SearchFileInIE.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_SearchFileInIE.HeaderText = "操作";
            this.button_SearchFileInIE.Name = "button_SearchFileInIE";
            this.button_SearchFileInIE.ReadOnly = true;
            this.button_SearchFileInIE.Text = "";
            this.button_SearchFileInIE.Width = 35;
            // 
            // button_CleanAnalyzeResult
            // 
            this.button_CleanAnalyzeResult.Location = new System.Drawing.Point(229, 20);
            this.button_CleanAnalyzeResult.Name = "button_CleanAnalyzeResult";
            this.button_CleanAnalyzeResult.Size = new System.Drawing.Size(88, 23);
            this.button_CleanAnalyzeResult.TabIndex = 1;
            this.button_CleanAnalyzeResult.Text = "清除分析结果";
            this.button_CleanAnalyzeResult.UseVisualStyleBackColor = true;
            this.button_CleanAnalyzeResult.Click += new System.EventHandler(this.button_CleanAnalyzeResult_Click);
            // 
            // button_GetAnalyzeResult
            // 
            this.button_GetAnalyzeResult.Location = new System.Drawing.Point(94, 20);
            this.button_GetAnalyzeResult.Name = "button_GetAnalyzeResult";
            this.button_GetAnalyzeResult.Size = new System.Drawing.Size(88, 23);
            this.button_GetAnalyzeResult.TabIndex = 0;
            this.button_GetAnalyzeResult.Text = "获取分析结果";
            this.button_GetAnalyzeResult.UseVisualStyleBackColor = true;
            this.button_GetAnalyzeResult.Click += new System.EventHandler(this.button_GetAnalyzeResult_Click);
            // 
            // button_CreateExcel
            // 
            this.button_CreateExcel.Location = new System.Drawing.Point(465, 20);
            this.button_CreateExcel.Name = "button_CreateExcel";
            this.button_CreateExcel.Size = new System.Drawing.Size(75, 23);
            this.button_CreateExcel.TabIndex = 4;
            this.button_CreateExcel.Text = "创建Excel";
            this.button_CreateExcel.UseVisualStyleBackColor = true;
            this.button_CreateExcel.Click += new System.EventHandler(this.button_CreateExcel_Click);
            // 
            // ContainerMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 571);
            this.Controls.Add(this.groupBox_AnalyzeResultOperate);
            this.Controls.Add(this.groupBox_DataAnalyze);
            this.Controls.Add(this.groupBox_FileOperate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ContainerMessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSA01报文校验";
            this.groupBox_FileOperate.ResumeLayout(false);
            this.groupBox_FileOperate.PerformLayout();
            this.groupBox_DataAnalyze.ResumeLayout(false);
            this.groupBox_DataAnalyze.PerformLayout();
            this.groupBox_AnalyzeResultOperate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AnalyzeResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Floder;
        private System.Windows.Forms.TextBox textBox_Folder;
        private System.Windows.Forms.Button button_OpenFolder;
        private System.Windows.Forms.Label label_TotalFileCount;
        private System.Windows.Forms.TextBox textBox_totalFileCount;
        private System.Windows.Forms.ProgressBar progressBar_DataDealProcess;
        private System.Windows.Forms.Label label_DataDealProcess;
        private System.Windows.Forms.GroupBox groupBox_FileOperate;
        private System.Windows.Forms.GroupBox groupBox_DataAnalyze;
        private System.Windows.Forms.Button button_StartAnalyze;
        private System.Windows.Forms.GroupBox groupBox_AnalyzeResultOperate;
        private System.Windows.Forms.DataGridView dataGridView_AnalyzeResult;
        private System.Windows.Forms.Button button_CleanAnalyzeResult;
        private System.Windows.Forms.Button button_GetAnalyzeResult;
        private System.Windows.Forms.DataGridViewButtonColumn button_SearchFileInIE;
        private System.Windows.Forms.Button button_DeriviveToExcel;
        private System.Windows.Forms.Button button_CreateExcel;
    }
}

