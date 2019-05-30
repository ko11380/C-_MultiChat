namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserList = new System.Windows.Forms.ListBox();
            this.MainList = new System.Windows.Forms.ListBox();
            this.Login = new System.Windows.Forms.Panel();
            this.Connect = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Login.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserList
            // 
            this.UserList.FormattingEnabled = true;
            this.UserList.ItemHeight = 12;
            this.UserList.Location = new System.Drawing.Point(356, 41);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(162, 280);
            this.UserList.TabIndex = 1;
            // 
            // MainList
            // 
            this.MainList.FormattingEnabled = true;
            this.MainList.ItemHeight = 12;
            this.MainList.Location = new System.Drawing.Point(3, 41);
            this.MainList.Margin = new System.Windows.Forms.Padding(0);
            this.MainList.Name = "MainList";
            this.MainList.Size = new System.Drawing.Size(350, 280);
            this.MainList.TabIndex = 2;
            // 
            // Login
            // 
            this.Login.BackColor = System.Drawing.Color.Silver;
            this.Login.Controls.Add(this.Connect);
            this.Login.Controls.Add(this.id);
            this.Login.Controls.Add(this.label2);
            this.Login.Controls.Add(this.port);
            this.Login.Controls.Add(this.label1);
            this.Login.Dock = System.Windows.Forms.DockStyle.Top;
            this.Login.Location = new System.Drawing.Point(0, 0);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(521, 38);
            this.Login.TabIndex = 3;
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(436, 9);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(76, 21);
            this.Connect.TabIndex = 4;
            this.Connect.Text = "접속하기";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(354, 9);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(77, 21);
            this.id.TabIndex = 3;
            this.id.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F);
            this.label2.Location = new System.Drawing.Point(297, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "닉네임 :";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(211, 9);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(77, 21);
            this.port.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F);
            this.label1.Location = new System.Drawing.Point(164, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "포트 :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 324);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(350, 21);
            this.textBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 21);
            this.button1.TabIndex = 5;
            this.button1.Text = "보내기";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.UserList);
            this.Controls.Add(this.MainList);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Login.ResumeLayout(false);
            this.Login.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.ListBox MainList;
        private System.Windows.Forms.Panel Login;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

