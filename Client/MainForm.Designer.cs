﻿namespace Client
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
            this.LoginPanal = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DialogCall = new System.Windows.Forms.Button();
            this.file_link = new System.Windows.Forms.TextBox();
            this.Sand = new System.Windows.Forms.Button();
            this.Select_File = new System.Windows.Forms.RadioButton();
            this.Select_img = new System.Windows.Forms.RadioButton();
            this.iTalk_TextBox = new System.Windows.Forms.TextBox();
            this.File_List = new System.Windows.Forms.ListView();
            this.전송자 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.종류 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.파일이름 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.다운로드 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.list = new System.Windows.Forms.Button();
            this.Sand_Msg = new iTalk.iTalk_Button_2();
            this.LoginPanal.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserList
            // 
            this.UserList.FormattingEnabled = true;
            this.UserList.ItemHeight = 12;
            this.UserList.Location = new System.Drawing.Point(396, 44);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(127, 280);
            this.UserList.TabIndex = 1;
            // 
            // MainList
            // 
            this.MainList.FormattingEnabled = true;
            this.MainList.ItemHeight = 12;
            this.MainList.Location = new System.Drawing.Point(8, 44);
            this.MainList.Margin = new System.Windows.Forms.Padding(0);
            this.MainList.Name = "MainList";
            this.MainList.Size = new System.Drawing.Size(385, 280);
            this.MainList.TabIndex = 2;
            // 
            // LoginPanal
            // 
            this.LoginPanal.BackColor = System.Drawing.Color.Silver;
            this.LoginPanal.Controls.Add(this.Title);
            this.LoginPanal.Controls.Add(this.Connect);
            this.LoginPanal.Controls.Add(this.id);
            this.LoginPanal.Controls.Add(this.label2);
            this.LoginPanal.Dock = System.Windows.Forms.DockStyle.Top;
            this.LoginPanal.Location = new System.Drawing.Point(0, 0);
            this.LoginPanal.Name = "LoginPanal";
            this.LoginPanal.Size = new System.Drawing.Size(531, 38);
            this.LoginPanal.TabIndex = 3;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(6, 7);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(132, 24);
            this.Title.TabIndex = 5;
            this.Title.Text = "Multi Chatting";
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(447, 9);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(76, 21);
            this.Connect.TabIndex = 4;
            this.Connect.Text = "접속하기";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(365, 9);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(77, 21);
            this.id.TabIndex = 3;
            this.id.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F);
            this.label2.Location = new System.Drawing.Point(308, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "닉네임 :";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // DialogCall
            // 
            this.DialogCall.Location = new System.Drawing.Point(396, 352);
            this.DialogCall.Name = "DialogCall";
            this.DialogCall.Size = new System.Drawing.Size(29, 21);
            this.DialogCall.TabIndex = 6;
            this.DialogCall.Text = "...";
            this.DialogCall.UseVisualStyleBackColor = true;
            this.DialogCall.Click += new System.EventHandler(this.Button1_Click);
            // 
            // file_link
            // 
            this.file_link.Location = new System.Drawing.Point(112, 352);
            this.file_link.Name = "file_link";
            this.file_link.ReadOnly = true;
            this.file_link.Size = new System.Drawing.Size(281, 21);
            this.file_link.TabIndex = 7;
            // 
            // Sand
            // 
            this.Sand.Location = new System.Drawing.Point(428, 352);
            this.Sand.Name = "Sand";
            this.Sand.Size = new System.Drawing.Size(95, 21);
            this.Sand.TabIndex = 8;
            this.Sand.Text = "Upload";
            this.Sand.UseVisualStyleBackColor = true;
            this.Sand.Click += new System.EventHandler(this.Sand_Click);
            // 
            // Select_File
            // 
            this.Select_File.AutoSize = true;
            this.Select_File.Checked = true;
            this.Select_File.Location = new System.Drawing.Point(13, 355);
            this.Select_File.Name = "Select_File";
            this.Select_File.Size = new System.Drawing.Size(43, 16);
            this.Select_File.TabIndex = 9;
            this.Select_File.TabStop = true;
            this.Select_File.Text = "File";
            this.Select_File.UseVisualStyleBackColor = true;
            // 
            // Select_img
            // 
            this.Select_img.AutoSize = true;
            this.Select_img.Location = new System.Drawing.Point(62, 355);
            this.Select_img.Name = "Select_img";
            this.Select_img.Size = new System.Drawing.Size(44, 16);
            this.Select_img.TabIndex = 10;
            this.Select_img.Text = "img";
            this.Select_img.UseVisualStyleBackColor = true;
            // 
            // iTalk_TextBox
            // 
            this.iTalk_TextBox.Location = new System.Drawing.Point(8, 327);
            this.iTalk_TextBox.Name = "iTalk_TextBox";
            this.iTalk_TextBox.Size = new System.Drawing.Size(385, 21);
            this.iTalk_TextBox.TabIndex = 15;
            this.iTalk_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ITalk_TextBox_KeyPress);
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
            this.File_List.Location = new System.Drawing.Point(8, 397);
            this.File_List.Name = "File_List";
            this.File_List.Size = new System.Drawing.Size(515, 10);
            this.File_List.TabIndex = 16;
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
            // list
            // 
            this.list.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.list.Font = new System.Drawing.Font("Arial", 6F);
            this.list.Location = new System.Drawing.Point(222, 379);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(84, 12);
            this.list.TabIndex = 17;
            this.list.Text = "▼";
            this.list.UseVisualStyleBackColor = true;
            this.list.Click += new System.EventHandler(this.List_Click);
            // 
            // Sand_Msg
            // 
            this.Sand_Msg.BackColor = System.Drawing.Color.Transparent;
            this.Sand_Msg.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Sand_Msg.ForeColor = System.Drawing.Color.White;
            this.Sand_Msg.Image = null;
            this.Sand_Msg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Sand_Msg.Location = new System.Drawing.Point(396, 327);
            this.Sand_Msg.Name = "Sand_Msg";
            this.Sand_Msg.Size = new System.Drawing.Size(127, 21);
            this.Sand_Msg.TabIndex = 13;
            this.Sand_Msg.Text = "보내기";
            this.Sand_Msg.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Sand_Msg.Click += new System.EventHandler(this.Sand_Msg_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 352);
            this.Controls.Add(this.list);
            this.Controls.Add(this.File_List);
            this.Controls.Add(this.iTalk_TextBox);
            this.Controls.Add(this.Sand_Msg);
            this.Controls.Add(this.Select_img);
            this.Controls.Add(this.Select_File);
            this.Controls.Add(this.Sand);
            this.Controls.Add(this.file_link);
            this.Controls.Add(this.DialogCall);
            this.Controls.Add(this.LoginPanal);
            this.Controls.Add(this.UserList);
            this.Controls.Add(this.MainList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.LoginPanal.ResumeLayout(false);
            this.LoginPanal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.ListBox MainList;
        private System.Windows.Forms.Panel LoginPanal;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button DialogCall;
        private System.Windows.Forms.TextBox file_link;
        private System.Windows.Forms.Button Sand;
        private System.Windows.Forms.RadioButton Select_File;
        private System.Windows.Forms.RadioButton Select_img;
        private System.Windows.Forms.Label Title;
        private iTalk.iTalk_Button_2 Sand_Msg;
        private System.Windows.Forms.TextBox iTalk_TextBox;
        private System.Windows.Forms.ListView File_List;
        private System.Windows.Forms.ColumnHeader 전송자;
        private System.Windows.Forms.ColumnHeader 종류;
        private System.Windows.Forms.ColumnHeader 다운로드;
        private System.Windows.Forms.Button list;
        private System.Windows.Forms.ColumnHeader 파일이름;
    }
}

