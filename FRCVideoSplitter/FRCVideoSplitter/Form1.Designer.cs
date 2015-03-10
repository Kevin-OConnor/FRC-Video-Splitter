namespace FRCVideoSplitter
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timeStampsDataGridView = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.useTbaCheckBox = new System.Windows.Forms.CheckBox();
            this.lastQualMatchNumberBox = new System.Windows.Forms.NumericUpDown();
            this.firstQualMatchNumberBox = new System.Windows.Forms.NumericUpDown();
            this.matchVideoLengthOverrideCheckBox = new System.Windows.Forms.CheckBox();
            this.elimMatchesCheckBox = new System.Windows.Forms.CheckBox();
            this.qualMatchesCheckBox = new System.Windows.Forms.CheckBox();
            this.generateTimestampButton = new System.Windows.Forms.Button();
            this.matchVideoBrowseButton = new System.Windows.Forms.Button();
            this.sourceVideoBrowseButton = new System.Windows.Forms.Button();
            this.overrideHelperLabel = new System.Windows.Forms.Label();
            this.lastQualMatchLabel = new System.Windows.Forms.Label();
            this.firstQualMatchLabel = new System.Windows.Forms.Label();
            this.elimHelperLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.elimsMatchNamesBox = new System.Windows.Forms.TextBox();
            this.matchVideoDestinationPathTextBox = new System.Windows.Forms.TextBox();
            this.eventNameTextBox = new System.Windows.Forms.TextBox();
            this.sourceVideoPathTextBox = new System.Windows.Forms.TextBox();
            this.matchLengthBox = new System.Windows.Forms.TextBox();
            this.splitVideosButton = new System.Windows.Forms.Button();
            this.eventCodeBox = new System.Windows.Forms.TextBox();
            this.eventCodeLabel = new System.Windows.Forms.Label();
            this.yearBox = new System.Windows.Forms.TextBox();
            this.yearLabel = new System.Windows.Forms.Label();
            this.getTimestampsFromTbaButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.timeStampsDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastQualMatchNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstQualMatchNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // timeStampsDataGridView
            // 
            this.timeStampsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timeStampsDataGridView.Location = new System.Drawing.Point(504, 12);
            this.timeStampsDataGridView.Name = "timeStampsDataGridView";
            this.timeStampsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.timeStampsDataGridView.Size = new System.Drawing.Size(250, 464);
            this.timeStampsDataGridView.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.useTbaCheckBox);
            this.groupBox1.Controls.Add(this.lastQualMatchNumberBox);
            this.groupBox1.Controls.Add(this.firstQualMatchNumberBox);
            this.groupBox1.Controls.Add(this.matchVideoLengthOverrideCheckBox);
            this.groupBox1.Controls.Add(this.elimMatchesCheckBox);
            this.groupBox1.Controls.Add(this.qualMatchesCheckBox);
            this.groupBox1.Controls.Add(this.getTimestampsFromTbaButton);
            this.groupBox1.Controls.Add(this.generateTimestampButton);
            this.groupBox1.Controls.Add(this.matchVideoBrowseButton);
            this.groupBox1.Controls.Add(this.sourceVideoBrowseButton);
            this.groupBox1.Controls.Add(this.overrideHelperLabel);
            this.groupBox1.Controls.Add(this.lastQualMatchLabel);
            this.groupBox1.Controls.Add(this.firstQualMatchLabel);
            this.groupBox1.Controls.Add(this.elimHelperLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.yearLabel);
            this.groupBox1.Controls.Add(this.eventCodeLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.elimsMatchNamesBox);
            this.groupBox1.Controls.Add(this.matchVideoDestinationPathTextBox);
            this.groupBox1.Controls.Add(this.yearBox);
            this.groupBox1.Controls.Add(this.eventCodeBox);
            this.groupBox1.Controls.Add(this.eventNameTextBox);
            this.groupBox1.Controls.Add(this.sourceVideoPathTextBox);
            this.groupBox1.Controls.Add(this.matchLengthBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 464);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // useTbaCheckBox
            // 
            this.useTbaCheckBox.Location = new System.Drawing.Point(252, 110);
            this.useTbaCheckBox.Name = "useTbaCheckBox";
            this.useTbaCheckBox.Size = new System.Drawing.Size(187, 17);
            this.useTbaCheckBox.TabIndex = 5;
            this.useTbaCheckBox.Text = "Get timestamps from TBA?";
            this.useTbaCheckBox.UseVisualStyleBackColor = true;
            this.useTbaCheckBox.CheckedChanged += new System.EventHandler(this.useTbaCheckBox_CheckedChanged);
            // 
            // lastQualMatchNumberBox
            // 
            this.lastQualMatchNumberBox.Enabled = false;
            this.lastQualMatchNumberBox.Location = new System.Drawing.Point(90, 157);
            this.lastQualMatchNumberBox.Name = "lastQualMatchNumberBox";
            this.lastQualMatchNumberBox.Size = new System.Drawing.Size(53, 20);
            this.lastQualMatchNumberBox.TabIndex = 4;
            // 
            // firstQualMatchNumberBox
            // 
            this.firstQualMatchNumberBox.Enabled = false;
            this.firstQualMatchNumberBox.Location = new System.Drawing.Point(90, 131);
            this.firstQualMatchNumberBox.Name = "firstQualMatchNumberBox";
            this.firstQualMatchNumberBox.Size = new System.Drawing.Size(53, 20);
            this.firstQualMatchNumberBox.TabIndex = 4;
            // 
            // matchVideoLengthOverrideCheckBox
            // 
            this.matchVideoLengthOverrideCheckBox.AutoSize = true;
            this.matchVideoLengthOverrideCheckBox.Location = new System.Drawing.Point(8, 358);
            this.matchVideoLengthOverrideCheckBox.Name = "matchVideoLengthOverrideCheckBox";
            this.matchVideoLengthOverrideCheckBox.Size = new System.Drawing.Size(202, 17);
            this.matchVideoLengthOverrideCheckBox.TabIndex = 3;
            this.matchVideoLengthOverrideCheckBox.Text = "Override Default Match Video Length";
            this.matchVideoLengthOverrideCheckBox.UseVisualStyleBackColor = true;
            this.matchVideoLengthOverrideCheckBox.CheckedChanged += new System.EventHandler(this.matchVideoLengthOverrideCheckBox_CheckedChanged);
            // 
            // elimMatchesCheckBox
            // 
            this.elimMatchesCheckBox.AutoSize = true;
            this.elimMatchesCheckBox.Location = new System.Drawing.Point(6, 193);
            this.elimMatchesCheckBox.Name = "elimMatchesCheckBox";
            this.elimMatchesCheckBox.Size = new System.Drawing.Size(163, 17);
            this.elimMatchesCheckBox.TabIndex = 3;
            this.elimMatchesCheckBox.Text = "Playoff / Elimination Matches";
            this.elimMatchesCheckBox.UseVisualStyleBackColor = true;
            this.elimMatchesCheckBox.CheckedChanged += new System.EventHandler(this.elimMatchesCheckBox_CheckedChanged);
            // 
            // qualMatchesCheckBox
            // 
            this.qualMatchesCheckBox.AutoSize = true;
            this.qualMatchesCheckBox.Location = new System.Drawing.Point(8, 110);
            this.qualMatchesCheckBox.Name = "qualMatchesCheckBox";
            this.qualMatchesCheckBox.Size = new System.Drawing.Size(128, 17);
            this.qualMatchesCheckBox.TabIndex = 3;
            this.qualMatchesCheckBox.Text = "Qualification Matches";
            this.qualMatchesCheckBox.UseVisualStyleBackColor = true;
            this.qualMatchesCheckBox.CheckedChanged += new System.EventHandler(this.qualMatchesCheckBox_CheckedChanged);
            // 
            // generateTimestampButton
            // 
            this.generateTimestampButton.Location = new System.Drawing.Point(126, 435);
            this.generateTimestampButton.Name = "generateTimestampButton";
            this.generateTimestampButton.Size = new System.Drawing.Size(225, 23);
            this.generateTimestampButton.TabIndex = 2;
            this.generateTimestampButton.Text = "Generate Timestamp Table";
            this.generateTimestampButton.UseVisualStyleBackColor = true;
            this.generateTimestampButton.Click += new System.EventHandler(this.generateTimestampButton_Click);
            // 
            // matchVideoBrowseButton
            // 
            this.matchVideoBrowseButton.Location = new System.Drawing.Point(382, 73);
            this.matchVideoBrowseButton.Name = "matchVideoBrowseButton";
            this.matchVideoBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.matchVideoBrowseButton.TabIndex = 2;
            this.matchVideoBrowseButton.Text = "Browse";
            this.matchVideoBrowseButton.UseVisualStyleBackColor = true;
            this.matchVideoBrowseButton.Click += new System.EventHandler(this.matchVideoBrowseButton_Click);
            // 
            // sourceVideoBrowseButton
            // 
            this.sourceVideoBrowseButton.Location = new System.Drawing.Point(382, 47);
            this.sourceVideoBrowseButton.Name = "sourceVideoBrowseButton";
            this.sourceVideoBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.sourceVideoBrowseButton.TabIndex = 2;
            this.sourceVideoBrowseButton.Text = "Browse";
            this.sourceVideoBrowseButton.UseVisualStyleBackColor = true;
            this.sourceVideoBrowseButton.Click += new System.EventHandler(this.sourceVideoBrowseButton_Click);
            // 
            // overrideHelperLabel
            // 
            this.overrideHelperLabel.AutoSize = true;
            this.overrideHelperLabel.Enabled = false;
            this.overrideHelperLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overrideHelperLabel.Location = new System.Drawing.Point(82, 384);
            this.overrideHelperLabel.Name = "overrideHelperLabel";
            this.overrideHelperLabel.Size = new System.Drawing.Size(67, 13);
            this.overrideHelperLabel.TabIndex = 1;
            this.overrideHelperLabel.Text = "(HH:MM:SS)";
            // 
            // lastQualMatchLabel
            // 
            this.lastQualMatchLabel.AutoSize = true;
            this.lastQualMatchLabel.Enabled = false;
            this.lastQualMatchLabel.Location = new System.Drawing.Point(27, 161);
            this.lastQualMatchLabel.Name = "lastQualMatchLabel";
            this.lastQualMatchLabel.Size = new System.Drawing.Size(63, 13);
            this.lastQualMatchLabel.TabIndex = 1;
            this.lastQualMatchLabel.Text = "Last Match:";
            // 
            // firstQualMatchLabel
            // 
            this.firstQualMatchLabel.AutoSize = true;
            this.firstQualMatchLabel.Enabled = false;
            this.firstQualMatchLabel.Location = new System.Drawing.Point(28, 135);
            this.firstQualMatchLabel.Name = "firstQualMatchLabel";
            this.firstQualMatchLabel.Size = new System.Drawing.Size(62, 13);
            this.firstQualMatchLabel.TabIndex = 1;
            this.firstQualMatchLabel.Text = "First Match:";
            // 
            // elimHelperLabel
            // 
            this.elimHelperLabel.AutoSize = true;
            this.elimHelperLabel.Enabled = false;
            this.elimHelperLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elimHelperLabel.Location = new System.Drawing.Point(90, 271);
            this.elimHelperLabel.Name = "elimHelperLabel";
            this.elimHelperLabel.Size = new System.Drawing.Size(121, 13);
            this.elimHelperLabel.TabIndex = 1;
            this.elimHelperLabel.Text = "(One match title per line)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Match Video Destination";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Event Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Source Video Location";
            // 
            // elimsMatchNamesBox
            // 
            this.elimsMatchNamesBox.AcceptsReturn = true;
            this.elimsMatchNamesBox.Enabled = false;
            this.elimsMatchNamesBox.Location = new System.Drawing.Point(25, 216);
            this.elimsMatchNamesBox.Multiline = true;
            this.elimsMatchNamesBox.Name = "elimsMatchNamesBox";
            this.elimsMatchNamesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.elimsMatchNamesBox.Size = new System.Drawing.Size(59, 127);
            this.elimsMatchNamesBox.TabIndex = 0;
            this.elimsMatchNamesBox.Text = "QF1\r\nQF2\r\nQF3\r\nQF4\r\nQF5\r\nQF6\r\nQF7\r\nQF8\r\nSF1\r\nSF2\r\nSF3\r\nSF4\r\nSF5\r\nSF6\r\nF1\r\nF2\r\nF3";
            // 
            // matchVideoDestinationPathTextBox
            // 
            this.matchVideoDestinationPathTextBox.Location = new System.Drawing.Point(126, 74);
            this.matchVideoDestinationPathTextBox.Name = "matchVideoDestinationPathTextBox";
            this.matchVideoDestinationPathTextBox.Size = new System.Drawing.Size(249, 20);
            this.matchVideoDestinationPathTextBox.TabIndex = 0;
            this.matchVideoDestinationPathTextBox.Text = "Click Browse...";
            // 
            // eventNameTextBox
            // 
            this.eventNameTextBox.Location = new System.Drawing.Point(77, 19);
            this.eventNameTextBox.Name = "eventNameTextBox";
            this.eventNameTextBox.Size = new System.Drawing.Size(298, 20);
            this.eventNameTextBox.TabIndex = 0;
            this.eventNameTextBox.TextChanged += new System.EventHandler(this.eventNameTextBox_TextChanged);
            // 
            // sourceVideoPathTextBox
            // 
            this.sourceVideoPathTextBox.Location = new System.Drawing.Point(126, 48);
            this.sourceVideoPathTextBox.Name = "sourceVideoPathTextBox";
            this.sourceVideoPathTextBox.Size = new System.Drawing.Size(249, 20);
            this.sourceVideoPathTextBox.TabIndex = 0;
            this.sourceVideoPathTextBox.Text = "Click Browse...";
            // 
            // matchLengthBox
            // 
            this.matchLengthBox.Enabled = false;
            this.matchLengthBox.Location = new System.Drawing.Point(25, 381);
            this.matchLengthBox.Name = "matchLengthBox";
            this.matchLengthBox.Size = new System.Drawing.Size(51, 20);
            this.matchLengthBox.TabIndex = 0;
            this.matchLengthBox.Text = "00:03:00";
            // 
            // splitVideosButton
            // 
            this.splitVideosButton.Enabled = false;
            this.splitVideosButton.Location = new System.Drawing.Point(12, 493);
            this.splitVideosButton.Name = "splitVideosButton";
            this.splitVideosButton.Size = new System.Drawing.Size(744, 34);
            this.splitVideosButton.TabIndex = 2;
            this.splitVideosButton.Text = "SPLIT VIDEOS";
            this.splitVideosButton.UseVisualStyleBackColor = true;
            this.splitVideosButton.Click += new System.EventHandler(this.splitVideosButton_Click);
            // 
            // eventCodeBox
            // 
            this.eventCodeBox.Enabled = false;
            this.eventCodeBox.Location = new System.Drawing.Point(341, 161);
            this.eventCodeBox.Name = "eventCodeBox";
            this.eventCodeBox.Size = new System.Drawing.Size(74, 20);
            this.eventCodeBox.TabIndex = 0;
            this.eventCodeBox.TextChanged += new System.EventHandler(this.eventNameTextBox_TextChanged);
            // 
            // eventCodeLabel
            // 
            this.eventCodeLabel.AutoSize = true;
            this.eventCodeLabel.Enabled = false;
            this.eventCodeLabel.Location = new System.Drawing.Point(269, 165);
            this.eventCodeLabel.Name = "eventCodeLabel";
            this.eventCodeLabel.Size = new System.Drawing.Size(63, 13);
            this.eventCodeLabel.TabIndex = 1;
            this.eventCodeLabel.Text = "Event Code";
            // 
            // yearBox
            // 
            this.yearBox.Enabled = false;
            this.yearBox.Location = new System.Drawing.Point(341, 135);
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(74, 20);
            this.yearBox.TabIndex = 0;
            this.yearBox.TextChanged += new System.EventHandler(this.eventNameTextBox_TextChanged);
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Enabled = false;
            this.yearLabel.Location = new System.Drawing.Point(269, 139);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(29, 13);
            this.yearLabel.TabIndex = 1;
            this.yearLabel.Text = "Year";
            // 
            // getTimestampsFromTbaButton
            // 
            this.getTimestampsFromTbaButton.Location = new System.Drawing.Point(341, 193);
            this.getTimestampsFromTbaButton.Name = "getTimestampsFromTbaButton";
            this.getTimestampsFromTbaButton.Size = new System.Drawing.Size(74, 23);
            this.getTimestampsFromTbaButton.TabIndex = 2;
            this.getTimestampsFromTbaButton.Text = "Get";
            this.getTimestampsFromTbaButton.UseVisualStyleBackColor = true;
            this.getTimestampsFromTbaButton.Click += new System.EventHandler(this.getTimestampsFromTbaButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 539);
            this.Controls.Add(this.timeStampsDataGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitVideosButton);
            this.Name = "Form1";
            this.Text = "FRC Video Splitter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeStampsDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastQualMatchNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstQualMatchNumberBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView timeStampsDataGridView;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown lastQualMatchNumberBox;
        private System.Windows.Forms.NumericUpDown firstQualMatchNumberBox;
        private System.Windows.Forms.CheckBox matchVideoLengthOverrideCheckBox;
        private System.Windows.Forms.CheckBox elimMatchesCheckBox;
        private System.Windows.Forms.CheckBox qualMatchesCheckBox;
        private System.Windows.Forms.Button generateTimestampButton;
        private System.Windows.Forms.Button matchVideoBrowseButton;
        private System.Windows.Forms.Button sourceVideoBrowseButton;
        private System.Windows.Forms.Label overrideHelperLabel;
        private System.Windows.Forms.Label lastQualMatchLabel;
        private System.Windows.Forms.Label firstQualMatchLabel;
        private System.Windows.Forms.Label elimHelperLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox elimsMatchNamesBox;
        private System.Windows.Forms.TextBox matchVideoDestinationPathTextBox;
        private System.Windows.Forms.TextBox eventNameTextBox;
        private System.Windows.Forms.TextBox sourceVideoPathTextBox;
        private System.Windows.Forms.TextBox matchLengthBox;
        private System.Windows.Forms.Button splitVideosButton;
        private System.Windows.Forms.CheckBox useTbaCheckBox;
        private System.Windows.Forms.Label eventCodeLabel;
        private System.Windows.Forms.TextBox eventCodeBox;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.TextBox yearBox;
        private System.Windows.Forms.Button getTimestampsFromTbaButton;
    }
}

