using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

using SocketGlobal;

namespace Client
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 명령어 클래스
        /// </summary>
        private gCommand m_insCommand = new gCommand();

        /// <summary>
        /// 내 소켓
        /// </summary>
        private Socket m_socketMe = null;

        /// <summary>
        /// 나의 상태
        /// </summary>
        enum typeState
        {
            /// <summary>
            /// 없음
            /// </summary>
            None = 0,
            /// <summary>
            /// 연결중
            /// </summary>
            Connecting,
            /// <summary>
            /// 연결 완료
            /// </summary>
            Connect,
        }

        public MainForm()
        {
            InitializeComponent();
            Set_UI(typeState.None);

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if ("" == id.Text)
            {
                MessageBox.Show("아이디를 입력후 시도해 주세요", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UserList.Items.Clear();
                //ui setting
                Set_UI(typeState.Connecting);

                Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);

                SocketAsyncEventArgs saeaServer = new SocketAsyncEventArgs();
                saeaServer.RemoteEndPoint = ipepServer;
                //연결 완료 이벤트 연결
                saeaServer.Completed += new EventHandler<SocketAsyncEventArgs>(Connect_Completed);

                //서버 메시지 대기
                socketServer.ConnectAsync(saeaServer);
            }
        }

        /// <summary>
		/// 연결 완료
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Connect_Completed(object sender, SocketAsyncEventArgs e)
        {
            m_socketMe = (Socket)sender;

            if (true == m_socketMe.Connected)
            {
                MessageData mdReceiveMsg = new MessageData();


                SocketAsyncEventArgs saeaReceiveArgs = new SocketAsyncEventArgs();
                saeaReceiveArgs.UserToken = mdReceiveMsg;
                saeaReceiveArgs.SetBuffer(mdReceiveMsg.GetBuffer(), 0, 4);
                saeaReceiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Recieve_Completed);

                m_socketMe.ReceiveAsync(saeaReceiveArgs);

                DisplayMsg("※ 서버 연결 성공");
                //서버 연결이 성공하면 id체크를 시작한다.
                Login();
            }
            else
            {
                Disconnection();
            }
        }

        private void Recieve_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket socketClient = (Socket)sender;
            MessageData mdRecieveMsg = (MessageData)e.UserToken;
            mdRecieveMsg.SetLength(e.Buffer);
            mdRecieveMsg.InitData();

            if (true == socketClient.Connected)
            {
                //연결이 되어 있다.

                //데이터 수신
                socketClient.Receive(mdRecieveMsg.Data, mdRecieveMsg.DataLength, SocketFlags.None);

                //명령을 분석 한다.
                MsgAnalysis(mdRecieveMsg.GetData());

                //다음 메시지를 받을 준비를 한다.
                socketClient.ReceiveAsync(e);
            }
            else
            {
                Disconnection();
            }
        }
        /// <summary>
        /// 아이디 체크 요청
        /// </summary>
        private void Login()
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append(gCommand.Command.ID_Check.GetHashCode());
            sbData.Append(claGlobal.g_Division);
            sbData.Append(id.Text);

            SendMsg(sbData.ToString());
        }

        /// <summary>
        /// 접속 끊킴
        /// </summary>
        private void Disconnection()
        {
            m_socketMe = null;

            Set_UI(typeState.None);

            DisplayMsg("※ 서버 연결 끊김");
        }


        /// <summary>
        /// 넘어온 데이터를 분석 한다.
        /// </summary>
        /// <param name="sMsg"></param>
        private void MsgAnalysis(string sMsg)
        {
            //구분자로 명령을 구분 한다.
            string[] sData = sMsg.Split(claGlobal.g_Division);

            //데이터 개수 확인
            if ((1 <= sData.Length))
            {
                //0이면 빈메시지이기 때문에 별도의 처리는 없다.

                //넘어온 명령
                gCommand.Command typeCommand
                    = m_insCommand.StrIntToType(sData[0]);

                switch (typeCommand)
                {
                    case gCommand.Command.None:   //없다
                        break;
                    case gCommand.Command.Msg:    //메시지인 경우
                        Command_Msg(sData[1]);
                        break;
                    case gCommand.Command.ID_Check_Ok:    //아이디 성공
                        SendMeg_IDCheck_Ok();
                        break;
                    case gCommand.Command.ID_Check_Fail:  //아이디 실패
                        SendMeg_IDCheck_Fail();
                        break;
                    case gCommand.Command.User_Connect:   //다른 유저가 접속
                        SendMeg_User_Connect(sData[1]);
                        break;
                    case gCommand.Command.User_Disconnect: //다른 유저가 접속해제
                        SendMeg_User_Disconnect(sData[1]);
                        break;
                    case gCommand.Command.User_List:  //유저 리스트 갱신
                        SendMeg_User_List(sData[1]);
                        break;
                }
            }
        }

        /// <summary>
        /// 메시지 출력
        /// </summary>
        /// <param name="sMsg"></param>
        private void Command_Msg(string sMsg)
        {
            DisplayMsg(sMsg);
        }

        /// <summary>
        /// 받아온 메시지를 출력 한다.
        /// </summary>
        /// <param name="nMessage"></param>
        /// <param name="nType"></param>
        private void DisplayMsg(String nMessage)
        {
            StringBuilder buffer = new StringBuilder();

            //출력할 메시지 완성
            buffer.Append(nMessage);

            //출력
            this.Invoke(new Action(
                        delegate ()
                        {
                            MainList.Items.Add(nMessage);
                        }));
            MainList.SelectedIndex = MainList.Items.Count - 1;

        }

        /// <summary>
        /// 서버로 메시지를 전달 합니다.
        /// </summary>
        /// <param name="sMsg"></param>
        private void SendMsg(string sMsg)
        {
            MessageData mdSendMsg = new MessageData();

            //데이터를 넣고
            mdSendMsg.SetData(sMsg);

            //서버에 보낼 객체를 만든다.
            SocketAsyncEventArgs saeaServer = new SocketAsyncEventArgs();
            //데이터 길이 세팅
            saeaServer.SetBuffer(BitConverter.GetBytes(mdSendMsg.DataLength), 0, 4);
            //보내기 완료 이벤트 연결
            saeaServer.Completed += new EventHandler<SocketAsyncEventArgs>(Send_Completed);
            //보낼 데이터 설정
            saeaServer.UserToken = mdSendMsg;
            //보내기
            m_socketMe.SendAsync(saeaServer);

        }
        /// <summary>
        /// 서버로 데이터를 전달 합니다.
        /// </summary>
        private void SendData()
        {

        }
        /// <summary>
        /// 메시지 보내기 완료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket socketSend = (Socket)sender;
            MessageData mdMsg = (MessageData)e.UserToken;
            //데이터 보내기 마무리
            socketSend.Send(mdMsg.Data);
        }

        /// <summary>
        /// Setting UI
        /// </summary>
        /// <param name="Set"></param>
        private void Set_UI(typeState Set)
        {
            switch(Set)
            {
                case typeState.None:
                    MainList.Enabled = false;
                    UserList.Enabled = false;

                    iTalk_TextBox.Enabled = false;
                    Sand_Msg.Enabled = false;

                    Select_File.Enabled = false;
                    Select_img.Enabled = false;

                    file_link.Enabled = false;
                    DialogCall.Enabled = false;
                    Sand.Enabled = false;


                    LoginPanal.Enabled = true;
                    break;
                case typeState.Connecting:
                    LoginPanal.Enabled = false;
                    break;
                case typeState.Connect:

                    MainList.Enabled = true;
                    UserList.Enabled = true;


                    iTalk_TextBox.Enabled = true;
                    Sand_Msg.Enabled = true;

                    Select_File.Enabled = true;
                    Select_img.Enabled = true;

                    file_link.Enabled = true;
                    DialogCall.Enabled = true;
                    Sand.Enabled = true;

                    break;
            }
        }
        /// <summary>
		/// 유저리스트 
		/// </summary>
		/// <param name="sUserList"></param>
		private void SendMeg_User_List(string sUserList)
        {
            this.Invoke(new Action(
                delegate ()
                {
                    //리스트를 비우고
                    UserList.Items.Clear();

                    //리스트를 다시 채워준다.
                    string[] sList = sUserList.Split(',');
                    for (int i = 0; i < sList.Length; ++i)
                    {
                        UserList.Items.Add(sList[i]);
                    }

                }));
            UserList.Items.Remove("");
        }
        /// <summary>
        /// 다른유저가 접속
        /// </summary>
        private void SendMeg_User_Connect(string sUserID)
        {
            this.Invoke(new Action(
                        delegate ()
                        {
                            UserList.Items.Add(sUserID);
                        }));

            DisplayMsg(" - " + sUserID + " : 님이 입장하셧습니다 - ");
        }

        /// <summary>
        /// 다른유저 로그아웃
        /// </summary>
        /// <param name="sUserID"></param>
        private void SendMeg_User_Disconnect(string sUserID)
        {
            this.Invoke(new Action(
                        delegate ()
                        {
                            UserList.Items.RemoveAt(UserList.FindString(sUserID));
                        }));
            DisplayMsg(" - "  + sUserID + " : 님이 퇴장하셧습니다 - ");

        }

        /// <summary>
        /// 아이디 성공
        /// </summary>
        private void SendMeg_IDCheck_Ok()
        {

            //UI갱신
            Set_UI(typeState.Connect);

            //아이디확인이 되었으면 서버에 로그인 요청을 하여 로그인을 끝낸다.
            StringBuilder sbData = new StringBuilder();
            sbData.Append(gCommand.Command.Login.GetHashCode());
            sbData.Append(claGlobal.g_Division);

            SendMsg(sbData.ToString());
        }

        /// <summary>
        /// 아이디체크 실패
        /// </summary>
        private void SendMeg_IDCheck_Fail()
        {
            DisplayMsg("로그인 실패 : 다른 아이디를 이용해 주세요.");
            //연결 끊기
            Disconnection();
        }


        /// <summary>
        /// 메시지전송
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sand_Msg_Click(object sender, EventArgs e)
        {
            if (iTalk_TextBox.Text != "")
            {
                StringBuilder sbData = new StringBuilder();
                sbData.Append(gCommand.Command.Msg.GetHashCode());
                sbData.Append(claGlobal.g_Division);
                sbData.Append(iTalk_TextBox.Text);

                SendMsg(sbData.ToString());

                iTalk_TextBox.Text = ""; //메시지 전송후 초기화
            }
        }
        /// <summary>
        /// 메시지전송 엔터키 입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ITalk_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                Sand_Msg_Click(sender, e);
            }
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog link = new OpenFileDialog();

            if (Select_img.Checked == true)
            {
                link.DefaultExt = "jpg";
                link.Filter = "image files (*.jpg;*.png)|*.jpg;*.png";
            }
            link.ShowDialog();
            if(link.FileName.Length > 0)
                file_link.Text = link.FileName;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Sand_Click(object sender, EventArgs e)
        {

        }
        private void List_Click(object sender, EventArgs e)
        {
            if(list.Text == "▼")
            {
                this.Height = 640;
                list.Text = "▲";
            }
            else if(list.Text == "▲")
            {
                this.Height = 436;
                list.Text = "▼";
            }
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
