namespace Server
{
    partial class ServerMain
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
            this.MainLog = new System.Windows.Forms.ListBox();
            this.UserList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cmmd_bt = new System.Windows.Forms.Button();
            this.Cmmd_tb = new System.Windows.Forms.TextBox();
            this.File_List = new System.Windows.Forms.ListView();
            this.전송자 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.종류 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.파일이름 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.다운로드 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // MainLog
            // 
            this.MainLog.BackColor = System.Drawing.Color.Black;
            this.MainLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainLog.Font = new System.Drawing.Font("Arial", 10F);
            this.MainLog.ForeColor = System.Drawing.Color.Lime;
            this.MainLog.FormattingEnabled = true;
            this.MainLog.ItemHeight = 16;
            this.MainLog.Location = new System.Drawing.Point(13, 9);
            this.MainLog.Name = "MainLog";
            this.MainLog.ScrollAlwaysVisible = true;
            this.MainLog.Size = new System.Drawing.Size(370, 274);
            this.MainLog.TabIndex = 0;
            // 
            // UserList
            // 
            this.UserList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserList.FormattingEnabled = true;
            this.UserList.ItemHeight = 12;
            this.UserList.Location = new System.Drawing.Point(389, 29);
            this.UserList.Name = "UserList";
            this.UserList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.UserList.Size = new System.Drawing.Size(139, 254);
            this.UserList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(389, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "접속자";
            // 
            // Cmmd_bt
            // 
            this.Cmmd_bt.Location = new System.Drawing.Point(389, 288);
            this.Cmmd_bt.Name = "Cmmd_bt";
            this.Cmmd_bt.Size = new System.Drawing.Size(139, 21);
            this.Cmmd_bt.TabIndex = 3;
            this.Cmmd_bt.Text = "Send";
            this.Cmmd_bt.UseVisualStyleBackColor = true;
            this.Cmmd_bt.Click += new System.EventHandler(this.SendCommand_Click);
            // 
            // Cmmd_tb
            // 
            this.Cmmd_tb.Location = new System.Drawing.Point(13, 288);
            this.Cmmd_tb.Name = "Cmmd_tb";
            this.Cmmd_tb.Size = new System.Drawing.Size(370, 21);
            this.Cmmd_tb.TabIndex = 4;
            this.Cmmd_tb.TextChanged += new System.EventHandler(this.Cmmd_tb_TextChanged);
            this.Cmmd_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cmmd_tb_KeyPress);
            // 
            // File_List
            // 
            this.File_List.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.File_List.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.File_List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.전송자,
            this.종류,
            this.파일이름,
            this.다운로드});
            this.File_List.Location = new System.Drawing.Point(12, 334);
            this.File_List.Name = "File_List";
            this.File_List.Size = new System.Drawing.Size(515, 195);
            this.File_List.TabIndex = 17;
            this.File_List.UseCompatibleStateImageBehavior = false;
            this.File_List.View = System.Windows.Forms.View.Details;
            // 
            // 전송자
            // 
            this.전송자.Text = "전송자";
            this.전송자.Width = 86;
            // 
            // 종류
            // 
            this.종류.Text = "종류";
            this.종류.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.종류.Width = 74;
            // 
            // 파일이름
            // 
            this.파일이름.Text = "파일이름";
            this.파일이름.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.파일이름.Width = 91;
            // 
            // 다운로드
            // 
            this.다운로드.Text = "다운로드";
            this.다운로드.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.다운로드.Width = 254;
            // 
            // ServerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(544, 316);
            this.Controls.Add(this.File_List);
            this.Controls.Add(this.Cmmd_tb);
            this.Controls.Add(this.Cmmd_bt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserList);
            this.Controls.Add(this.MainLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerMain";
            this.Text = "ServerMain";
            this.Load += new System.EventHandler(this.ServerMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MainLog;
        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cmmd_bt;
        private System.Windows.Forms.TextBox Cmmd_tb;
        private System.Windows.Forms.ListView File_List;
        private System.Windows.Forms.ColumnHeader 전송자;
        private System.Windows.Forms.ColumnHeader 종류;
        private System.Windows.Forms.ColumnHeader 파일이름;
        private System.Windows.Forms.ColumnHeader 다운로드;
    }
}

