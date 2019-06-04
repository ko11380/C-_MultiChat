using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketGlobal;

namespace Server
{
    #region 유저 연결 델리게이트
    //델리게이트 선언
    /// <summary>
    /// 유저 접속
    /// </summary>
    public delegate void dgConnect(UserInfo sender);
    /// <summary>
    /// 유저 접속 끊김
    /// </summary>
    public delegate void dgDisconnect(UserInfo sender);
    /// <summary>
    /// 유저 메시지 요청
    /// </summary>
    public delegate void dgMessage(UserInfo sender, MessageEventArgs e);
    #endregion
    public partial class ServerMain : Form
    {
        private byte[] szData;

        /// <summary>
        /// 접속한 유저 리스트(로그인 완료전 포함)
        /// </summary>
        private List<UserInfo> m_listUser = null;

        private Socket socketServer;
        private List<Socket> m_ClientSocket;


        public ServerMain()
        {
            
            InitializeComponent();
        }

        private void ServerMain_Load(object sender, EventArgs e)
        {

        }
        private void ServerStart()
        {
            //소켓서버 서버 세팅

            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 7000); //IP를 자신의 IP로 지정, 포트를 7000으로 지정
            MainLog.Items.Add("- 서버 IP 및 포트 설정완료...");

            socketServer.Bind(ipep); //Bind
            MainLog.Items.Add("- Server Bind 완료...");

            socketServer.Listen(int.MaxValue); //최대접속자수 최대로 적용 and Listen Start
            MainLog.Items.Add("- Server Listen 시작...");

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();

            //유저가 연결되었을때 이벤트
            args.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);

            //유저 접속 대기 시작
            socketServer.AcceptAsync(args);

            //유저 리스트 생성
            m_listUser = new List<UserInfo>();

            //서버 시작완료 로그 표시
            MainLog.Items.Add("- 서버시작 완료");
        }

        private void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            //클라리언트 접속

            //유저 객체 생성
            UserInfo insUser = new UserInfo(e.AcceptSocket);
            //각 이벤트 연결
            insUser.OnConnected += insUser_OnConnected;
            insUser.OnDisconnected += insUser_OnDisconnected;
            insUser.OnMessaged += insUser_OnMessaged;

            //리스트에 클라이언트 추가
            m_listUser.Add(insUser);

            //다시 클라이언트 접속대기
            Socket socketServer = (Socket)sender;
            e.AcceptSocket = null;
            socketServer.AcceptAsync(e);
        }

        /// <summary>
        /// 명령 처리 - 메시지 보내기
        /// </summary>
        /// <param name="sMsg"></param>
        private void Commd_SendMsg(string sMsg)
        {
            StringBuilder sbMsg = new StringBuilder();
            //명령어 부착
            sbMsg.Append(gCommand.Command.Msg.GetHashCode().ToString());
            //구분자 부착
            sbMsg.Append(claGlobal.g_Division);
            //메시지 완성
            sbMsg.Append(sMsg);

            //전체 유저에게 메시지 전송
            AllUser_Send(sbMsg.ToString());
        }


        /// <summary>
        /// 접속중인 모든 유저에게 메시지를 보냅니다.
        /// </summary>
        /// <param name="sMsg"></param>
        private void AllUser_Send(string sMsg)
        {
            //모든 유저에게 메시지를 전송 한다.
            foreach (UserInfo insUser in m_listUser)
            {
                insUser.SendMsg_User(sMsg);
            }

            //로그 출력
            DisplayLog(sMsg);
        }
        /// <summary>
        /// 전체 유저중 지정한 유저를 제외하고 메시지를 전송 합니다.
        /// </summary>
        /// <param name="sMsg"></param>
        /// <param name="insUser">제외할 유저</param>
        private void AllUser_Send(string sMsg, UserInfo insUser)
        {
            //모든 유저에게 메시지를 전송 한다.
            foreach (UserInfo insUser_Temp in m_listUser)
            {
                //제외 유저
                if (insUser_Temp.UserID != insUser.UserID)
                {
                    //제외 유저가 아니라면 메시지를 보낸다.
                    insUser_Temp.SendMsg_User(sMsg);
                }
            }

            //로그 출력
            DisplayLog(sMsg);
        }
        #region 유저이벤트
        void insUser_OnConnected(UserInfo sender)
        {
            //유저 추가는 'Accept_Completed'에서 하므로 
            //여기서 하는 것은 무결성 검사가 끝난 유저를 처리 해주는 것이다.

            StringBuilder sbMsg = new StringBuilder(); ;

            //로그인이 완료된 유저에게 유저 리스트를 보낸다.
            Commd_User_List_Get(sender);

            //전체 유저에게 접속자를 알린다.
            sbMsg.Clear();
            sbMsg.Append(gCommand.Command.User_Connect.GetHashCode());
            sbMsg.Append(claGlobal.g_Division);
            sbMsg.Append(sender.UserID);

            //전체 유저에게 메시지 전송(지금 로그인 한 접속자는 제외)
            AllUser_Send(sbMsg.ToString(), sender);

            //로그 유저 리스트에 추가
            this.Invoke(new Action(
                delegate ()
                {
                    UserList.Items.Add(sender.UserID);
                }));

            //로그 남기기
            sbMsg.Clear();
            sbMsg.Append("*** 접속자 : ");
            sbMsg.Append(sender.UserID);
            sbMsg.Append(" ***");
            DisplayLog(sbMsg.ToString());
        }

        /// <summary>
        /// 유저 끊김 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void insUser_OnDisconnected(UserInfo sender)
        {
            //로그리스트에서 유저를 지움
            //출력
            this.Invoke(new Action(
                        delegate ()
                        {
                            UserList.Items.RemoveAt(UserList.FindString(sender.UserIP));
                        }));

            //로그 기록
            //DisplayLog(sbMsg.ToString());

            //리스트에서 유저를 지움
            m_listUser.Remove(sender);

        }

        /// <summary>
        /// 유저 메시지 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void insUser_OnMessaged(UserInfo sender, MessageEventArgs e)
        {
            StringBuilder sbMsg = new StringBuilder();


            switch (e.m_typeCommand)
            {
                case gCommand.Command.Msg:    //메시지
                    sbMsg.Append(sender.UserID);
                    sbMsg.Append(" : ");
                    sbMsg.Append(e.m_strMsg);

                    Commd_SendMsg(sbMsg.ToString());
                    break;
                case gCommand.Command.ID_Check:   //id체크
                    Commd_IDCheck(sender, e.m_strMsg);
                    break;
                case gCommand.Command.Connect_Info:
                    //Add_CntUser(sender, e.m_strMsg);
                    break;
                case gCommand.Command.User_List_Get:  //유저 리스트 갱신 요청
                    Commd_User_List_Get(sender);
                    break;
            }
        }
        #endregion
        /// <summary>
        /// 콘솔 명령어 구분 실행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendCommand_Click(object sender, EventArgs e)
        {
            switch (Cmmd_tb.Text)
            {
                case "@Help":
                    //HelpPrint();
                    MainLog.Items.Add("");
                    break;
                case "@Start":
                    ServerStart();
                    MainLog.Items.Add("");
                    break;
                case "@Stop":
                    //ServerStop();
                    MainLog.Items.Add("");
                    break;
                case "@Clear":
                    MainLog.Items.Clear();
                    MainLog.Items.Add("");
                    break;
                case "@UserList":
                    for (int i = 0; i < m_listUser.Count; i++)
                    {
                        MainLog.Items.Add(m_listUser[i].UserIP);
                    }
                    break;
            }
            Cmmd_tb.Text = ""; //명령어수행후 초기화
        }

        private void ServerStop()
        {
            //서버 중지
            socketServer.Close();
            MainLog.Items.Add("- 서버 종료");
        }
        private void HelpPrint()
        {

            MainLog.Items.Add("===========================");
            MainLog.Items.Add("        Command List       ");
            MainLog.Items.Add("===========================");
            MainLog.Items.Add("@Start  :  서버를 시작합니다.");
            MainLog.Items.Add("@Stop  :  서버를 종료합니다.");
        }

        /// <summary>
        /// 콘솔 명령어 수행 - Enter Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmmd_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (e.KeyChar == '\r')
                {
                SendCommand_Click(sender, e);
                }
        }

        /// <summary>
        /// 받아온 메시지를 출력 한다.
        /// </summary>
        /// <param name="nMessage"></param>
        /// <param name="nType"></param>
        public void DisplayLog(String nMessage)
        {
            StringBuilder buffer = new StringBuilder();

            //출력할 메시지 완성
            buffer.Append(nMessage);

            //출력
            this.Invoke(new Action(
                        delegate ()
                        {
                            MainLog.Items.Add(nMessage);
                        }));

        }
        /// <summary>
        /// 명령 처리 - 유저 리스트 갱신 요청
        /// </summary>
        /// <param name="insUser"></param>
        private void Commd_User_List_Get(UserInfo insUser)
        {
            StringBuilder sbList = new StringBuilder();

            //명령 만들기
            sbList.Append(gCommand.Command.User_List.GetHashCode());
            sbList.Append(claGlobal.g_Division);

            //리스트 만들기
            foreach (UserInfo insUser_Temp in m_listUser)
            {
                sbList.Append(insUser_Temp.UserID);
                sbList.Append(",");
            }

            //요청에 응답해준다.
            insUser.SendMsg_User(sbList.ToString());
        }

        /// <summary>
		/// 명령 처리 - ID체크
		/// </summary>
		/// <param name="sID"></param>
		private void Commd_IDCheck(UserInfo insUser, string sID)
        {
            //사용 가능 여부
            bool bReturn = true;

            //모든 유저의 아이디 체크
            foreach (UserInfo insUserTemp in m_listUser)
            {
                if (insUserTemp.UserID == sID)
                {
                    //같은 유저가 있다!
                    //같은 유저가 있으면 그만 검사한다.
                    bReturn = false;
                    break;
                }
            }

            if (true == bReturn)
            {
                //사용 가능

                //아이디를 지정하고
                insUser.UserID = sID;

                //유저에게 로그인이 성공했음을 알림
                StringBuilder sbMsg = new StringBuilder();
                sbMsg.Append(gCommand.Command.ID_Check_Ok.GetHashCode());
                sbMsg.Append(claGlobal.g_Division);
                insUser.SendMsg_User(sbMsg.ToString());

            }
            else
            {
                //실패

                StringBuilder sbMsg = new StringBuilder();

                sbMsg.Append(gCommand.Command.ID_Check_Fail.GetHashCode().ToString());
                sbMsg.Append(claGlobal.g_Division);

                insUser.SendMsg_User(sbMsg.ToString());
            }
        }
    }
}
