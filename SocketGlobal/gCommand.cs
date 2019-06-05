using System;

namespace SocketGlobal
{
    /// <summary>
    /// 명령어 클래스
    /// </summary>
    public class gCommand
    {
        public enum Command
        {
            /// <summary>
            /// 기본 상태
            /// </summary>
            None = 0,
            /// <summary>
            /// 아이디 체크
            /// </summary>
            ID_Check,
            /// <summary>
            /// 아이디 체크 성공
            /// </summary>
            ID_Check_Ok,
            /// <summary>
            /// 아이디 체크 실패
            /// </summary>
            ID_Check_Fail,
            
            /// <summary>
            /// 접속시 접속자정보 보내기
            /// </summary>
            Connect_Info,

            /// <summary>
            /// 접속한 유저가 있다
            /// </summary>
            User_Connect,
            /// <summary>
            /// 접속을 끊은 유저가 있다
            /// </summary>
            User_Disconnect,


            /// <summary>
            /// 유저 리스트를 보냅니다
            /// </summary>
            User_List,
            /// <summary>
            /// 유저 리스트 갱신을 요청 합니다
            /// </summary>
            User_List_Get,

            /// <summary>
            ///아이디 무결성 확인후 호출
            /// </summary>
            Login,
            /// <summary>
            /// 서버에서 모든 로그인 과정이 완료될경우 클라이언트에게 알림
            /// </summary>
            Login_Complete,
            /// <summary>
            /// 로그아웃
            /// </summary>
            Logout,


            /// <summary>
            /// 메시지 전송
            /// </summary>
            Msg,

            /// <summary>
            /// 테스트 커멘드
            /// </summary>
            Test_Ok,
        }

        private CustomClass.Number m_insNumber = new CustomClass.Number();

        /// <summary>
        /// 문자열로된 숫자를 명령어 타입으로 바꿔줍니다.
        /// 입력된 문자열이 올바르지 않다면 기본상태를 줍니다.
        /// </summary>
        /// <param name="sData"></param>
        /// <returns></returns>
        public Command StrIntToType(string sData)
        {
            //넘어온 명령
            gCommand.Command typeCommand = gCommand.Command.None;

            if (true == m_insNumber.IsNumeric(sData))
            {
                /**
                입력된 명령이 숫자라면 명령 타입으로 변환한다.
                입력된 명령이 숫자가 아니면 명령 없음 처리(기본값)를 한다.
                */
                typeCommand = (gCommand.Command)Convert.ToInt32(sData);
            }

            return typeCommand;
        }
    }
}
