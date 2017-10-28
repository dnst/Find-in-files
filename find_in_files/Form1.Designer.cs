namespace find_in_files
{
    partial class FindInFiles
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.selectDirButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.keywordsTextBox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.extensionCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.registerCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectDirButton
            // 
            this.selectDirButton.Location = new System.Drawing.Point(531, 9);
            this.selectDirButton.Name = "selectDirButton";
            this.selectDirButton.Size = new System.Drawing.Size(75, 23);
            this.selectDirButton.TabIndex = 0;
            this.selectDirButton.Text = "Обзор";
            this.selectDirButton.UseVisualStyleBackColor = true;
            this.selectDirButton.Click += new System.EventHandler(this.selectDirButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(12, 12);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(513, 20);
            this.pathTextBox.TabIndex = 1;
            // 
            // keywordsTextBox
            // 
            this.keywordsTextBox.AccessibleDescription = "";
            this.keywordsTextBox.Location = new System.Drawing.Point(12, 38);
            this.keywordsTextBox.Multiline = true;
            this.keywordsTextBox.Name = "keywordsTextBox";
            this.keywordsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keywordsTextBox.Size = new System.Drawing.Size(513, 143);
            this.keywordsTextBox.TabIndex = 2;
            this.keywordsTextBox.Text = "Введите ключевые слова";
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(531, 187);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 3;
            this.findButton.Text = "Поиск";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // extensionCheckedListBox
            // 
            this.extensionCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.extensionCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.extensionCheckedListBox.CheckOnClick = true;
            this.extensionCheckedListBox.FormattingEnabled = true;
            this.extensionCheckedListBox.Items.AddRange(new object[] {
            ".txt ",
            ".log",
            ".html",
            ".xml",
            ".doc",
            ".docx",
            ".xls",
            ".xlsx"});
            this.extensionCheckedListBox.Location = new System.Drawing.Point(531, 38);
            this.extensionCheckedListBox.Name = "extensionCheckedListBox";
            this.extensionCheckedListBox.Size = new System.Drawing.Size(56, 120);
            this.extensionCheckedListBox.TabIndex = 6;
            // 
            // registerCheckBox
            // 
            this.registerCheckBox.AutoSize = true;
            this.registerCheckBox.Location = new System.Drawing.Point(531, 164);
            this.registerCheckBox.Name = "registerCheckBox";
            this.registerCheckBox.Size = new System.Drawing.Size(120, 17);
            this.registerCheckBox.TabIndex = 8;
            this.registerCheckBox.Text = "С учетом регистра";
            this.registerCheckBox.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 187);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(512, 23);
            this.progressBar.TabIndex = 9;
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(630, 187);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // FindInFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 222);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.registerCheckBox);
            this.Controls.Add(this.extensionCheckedListBox);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.keywordsTextBox);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.selectDirButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FindInFiles";
            this.Text = "Find in files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button selectDirButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.TextBox keywordsTextBox;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.CheckedListBox extensionCheckedListBox;
        private System.Windows.Forms.CheckBox registerCheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button cancelButton;

    }
}

