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
            //AllUser_Send(sbMsg.ToString());
        }
        void insUser_OnConnected(UserInfo sender)
        {

            /*
            //유저 추가는 'Accept_Completed'에서 하므로 
            //무결성 검사가 끝난 유저를 처리

            StringBuilder sbMsg = new StringBuilder(); ;


            //로그인이 완료된 유저에게 유저 리스트 전속
            Commd_User_List_Get(sender);
            //전체 유저에게 접속자를 알린다.
            sbMsg.Clear();
            sbMsg.Append(gCommand.Command.User_Connect.GetHashCode());
            sbMsg.Append(claGlobal.g_Division);
            sbMsg.Append(sender.UserID);

            //전체 유저에게 메시지 전송(지금 로그인 한 접속자는 제외)
            AllUser_Send(sbMsg.ToString(), sender);
            */

            //Commd_RoomList_Get(sender);

            //로그 유저 리스트에 추가
            this.Invoke(new Action(
                delegate ()
                {
                    UserList.Items.Add(sender.UserIP);
                }));

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
                    //Commd_IDCheck(sender, e.m_strMsg, e.m_strPw);
                    break;
                case gCommand.Command.Connect_Info:
                    //Add_CntUser(sender, e.m_strMsg);
                    break;
            }
        }

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
                Cmmd_bt_Click(sender, e);
                }
        }

        /// <summary>
        /// 콘솔 명령어 수행 - Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmmd_bt_Click(object sender, EventArgs e)
        {

        }
    }
}
