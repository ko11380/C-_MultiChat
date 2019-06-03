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
            else if (port.Text == "")
            {
                MessageBox.Show("포트를 입력후 시도해 주세요", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //ui setting
                Set_UI(typeState.Connecting);

                Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Convert.ToInt32(port.Text));

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

                //서버에 보낼 객체를 만든다.
                SocketAsyncEventArgs saeaReceiveArgs = new SocketAsyncEventArgs();
                //보낼 데이터를 설정하고
                saeaReceiveArgs.UserToken = mdReceiveMsg;
                //데이터 길이 세팅
                saeaReceiveArgs.SetBuffer(mdReceiveMsg.GetBuffer(), 0, 4);
                //받음 완료 이벤트 연결
                saeaReceiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Recieve_Completed);
                //받음 보냄
                m_socketMe.ReceiveAsync(saeaReceiveArgs);

                DisplayMsg("*** 서버 연결 성공 ***");
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
        /// 접속이 끊겼다.
        /// </summary>
        private void Disconnection()
        {
            //접속 끊김
            m_socketMe = null;

            //유아이를 세팅하고
            Set_UI(typeState.None);

            DisplayMsg("*** 서버 연결 끊김 ***");
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
                    case gCommand.Command.User_Connect:   //다른 유저가 접속 했다.
                        SendMeg_User_Connect(sData[1]);
                        break;
                    case gCommand.Command.User_Disonnect: //다른 유저가 접속을 끊었다.
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

                    LoginPanal.Enabled = true;
                    break;
                case typeState.Connecting:
                    LoginPanal.Enabled = false;
                    break;
                case typeState.Connect:

                    MainList.Enabled = true;
                    UserList.Enabled = true;

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
            StringBuilder sbData = new StringBuilder();
            sbData.Append(gCommand.Command.Msg.GetHashCode());
            sbData.Append(claGlobal.g_Division);
            sbData.Append(Msg_txt.Text);

            SendMsg(sbData.ToString());

            Msg_txt.Text = ""; //메시지 전송후 초기화
        }
        /// <summary>
        /// 메시지전송 엔터키 입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Msg_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Sand_Msg_Click(sender, e);
            }
        }
    }
}
