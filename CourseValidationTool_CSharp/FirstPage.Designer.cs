﻿namespace CourseValidationTool_CSharp
{
    partial class FirstPage
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
            this.OpenFileFolderBtn = new System.Windows.Forms.Button();
            this.TestEncodingBtn = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.fileFolderText = new System.Windows.Forms.TextBox();
            this.enCodingList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.validateCourseBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenFileFolderBtn
            // 
            this.OpenFileFolderBtn.Location = new System.Drawing.Point(410, 13);
            this.OpenFileFolderBtn.Name = "OpenFileFolderBtn";
            this.OpenFileFolderBtn.Size = new System.Drawing.Size(111, 23);
            this.OpenFileFolderBtn.TabIndex = 0;
            this.OpenFileFolderBtn.Text = "打开文件目录";
            this.OpenFileFolderBtn.UseVisualStyleBackColor = true;
            this.OpenFileFolderBtn.Click += new System.EventHandler(this.OpenFileFolderBtn_Click);
            // 
            // TestEncodingBtn
            // 
            this.TestEncodingBtn.Location = new System.Drawing.Point(12, 102);
            this.TestEncodingBtn.Name = "TestEncodingBtn";
            this.TestEncodingBtn.Size = new System.Drawing.Size(92, 31);
            this.TestEncodingBtn.TabIndex = 1;
            this.TestEncodingBtn.Text = "测试编码";
            this.TestEncodingBtn.UseVisualStyleBackColor = true;
            this.TestEncodingBtn.Click += new System.EventHandler(this.TestEncodingBtn_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(12, 139);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(776, 299);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            // 
            // fileFolderText
            // 
            this.fileFolderText.Location = new System.Drawing.Point(13, 13);
            this.fileFolderText.Name = "fileFolderText";
            this.fileFolderText.Size = new System.Drawing.Size(391, 22);
            this.fileFolderText.TabIndex = 3;
            // 
            // enCodingList
            // 
            this.enCodingList.FormattingEnabled = true;
            this.enCodingList.ItemHeight = 16;
            this.enCodingList.Items.AddRange(new object[] {
            "GB2312",
            "UTF-8"});
            this.enCodingList.Location = new System.Drawing.Point(12, 60);
            this.enCodingList.Name = "enCodingList";
            this.enCodingList.Size = new System.Drawing.Size(120, 36);
            this.enCodingList.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择编码方式";
            // 
            // validateCourseBtn
            // 
            this.validateCourseBtn.Location = new System.Drawing.Point(116, 102);
            this.validateCourseBtn.Name = "validateCourseBtn";
            this.validateCourseBtn.Size = new System.Drawing.Size(95, 31);
            this.validateCourseBtn.TabIndex = 6;
            this.validateCourseBtn.Text = "校验课程";
            this.validateCourseBtn.UseVisualStyleBackColor = true;
            this.validateCourseBtn.Click += new System.EventHandler(this.validateCourseBtn_Click);
            // 
            // FirstPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 475);
            this.Controls.Add(this.validateCourseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enCodingList);
            this.Controls.Add(this.fileFolderText);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.TestEncodingBtn);
            this.Controls.Add(this.OpenFileFolderBtn);
            this.Name = "FirstPage";
            this.Text = "FirstPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFileFolderBtn;
        private System.Windows.Forms.Button TestEncodingBtn;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.TextBox fileFolderText;
        private System.Windows.Forms.ListBox enCodingList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button validateCourseBtn;
    }
}

