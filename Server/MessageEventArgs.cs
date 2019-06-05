using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SocketGlobal;

namespace Server
{
	/// <summary>
	/// 메시지 이벤트용 형식입니다.
	/// </summary>
	public class MessageEventArgs : EventArgs
	{
		/// <summary>
		/// 메시지
		/// </summary>
		public readonly string m_strMsg = "";
        public readonly string m_strPw = "";
		/// <summary>
		/// 메시지 타입
		/// </summary>
		public gCommand.Command m_typeCommand = gCommand.Command.None;

		/// <summary>
		/// 메시지 설정
		/// </summary>
		/// <param name="strMsg"></param>
		/// <param name="typeCommand"></param>
		public MessageEventArgs(string strMsg, gCommand.Command typeCommand)
		{
			this.m_strMsg = strMsg;
			this.m_typeCommand = typeCommand;
		}
        public MessageEventArgs(string strMsg, string strPw, gCommand.Command typeCommand)
        {
            this.m_strMsg = strMsg;
            this.m_strPw = strPw;
            this.m_typeCommand = typeCommand;
        }
        
    }
}
