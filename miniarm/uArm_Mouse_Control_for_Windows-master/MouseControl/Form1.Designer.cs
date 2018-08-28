namespace MouseControl
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btOpen = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comPort = new System.Windows.Forms.Label();
            this.cmBaudRate = new System.Windows.Forms.ComboBox();
            this.cmPort = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.exit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // btOpen
            // 
            this.btOpen.Font = new System.Drawing.Font("Calibri", 11F);
            this.btOpen.Location = new System.Drawing.Point(212, 39);
            this.btOpen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(82, 60);
            this.btOpen.TabIndex = 33;
            this.btOpen.Text = "Start";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // btClose
            // 
            this.btClose.Enabled = false;
            this.btClose.Font = new System.Drawing.Font("Calibri", 11F);
            this.btClose.Location = new System.Drawing.Point(300, 12);
            this.btClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(0, 0);
            this.btClose.TabIndex = 34;
            this.btClose.Text = "Close Port";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11F);
            this.label2.Location = new System.Drawing.Point(11, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Baudrate";
            // 
            // comPort
            // 
            this.comPort.AutoSize = true;
            this.comPort.Font = new System.Drawing.Font("Calibri", 11F);
            this.comPort.Location = new System.Drawing.Point(11, 30);
            this.comPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(34, 18);
            this.comPort.TabIndex = 1;
            this.comPort.Text = "Port";
            // 
            // cmBaudRate
            // 
            this.cmBaudRate.Font = new System.Drawing.Font("Calibri", 11F);
            this.cmBaudRate.FormattingEnabled = true;
            this.cmBaudRate.Items.AddRange(new object[] {
            "75",
            "110",
            "134",
            "150",
            "300",
            "600",
            "1200",
            "1800",
            "2400",
            "4800",
            "7200",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000"});
            this.cmBaudRate.Location = new System.Drawing.Point(76, 68);
            this.cmBaudRate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmBaudRate.Name = "cmBaudRate";
            this.cmBaudRate.Size = new System.Drawing.Size(79, 26);
            this.cmBaudRate.TabIndex = 29;
            this.cmBaudRate.Text = "9600";
            // 
            // cmPort
            // 
            this.cmPort.Font = new System.Drawing.Font("Calibri", 11F);
            this.cmPort.FormattingEnabled = true;
            this.cmPort.Location = new System.Drawing.Point(76, 27);
            this.cmPort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmPort.Name = "cmPort";
            this.cmPort.Size = new System.Drawing.Size(79, 26);
            this.cmPort.TabIndex = 28;
            this.cmPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmPort_MouseDown);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sComm_DataReceived);
            // 
            // exit
            // 
            this.exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exit.Font = new System.Drawing.Font("Calibri", 11F);
            this.exit.Location = new System.Drawing.Point(246, 134);
            this.exit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(58, 30);
            this.exit.TabIndex = 35;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmBaudRate);
            this.groupBox1.Controls.Add(this.comPort);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 10.5F);
            this.groupBox1.Location = new System.Drawing.Point(10, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 111);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Port Setting";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.OrangeRed;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(50, 137);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(115, 19);
            this.linkLabel1.TabIndex = 39;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.ufactory.cc";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Firebrick;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // logo
            // 
            this.logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.InitialImage = null;
            this.logo.Location = new System.Drawing.Point(19, 132);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(29, 31);
            this.logo.TabIndex = 40;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 30);
            this.button1.TabIndex = 41;
            this.button1.Text = "Help";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.CancelButton = this.exit;
            this.ClientSize = new System.Drawing.Size(321, 176);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.exit);
            this.Font = new System.Drawing.Font("Calibri", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Mouse Control V1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label comPort;
        private System.Windows.Forms.ComboBox cmBaudRate;
        private System.Windows.Forms.ComboBox cmPort;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Button button1;


    }
}

