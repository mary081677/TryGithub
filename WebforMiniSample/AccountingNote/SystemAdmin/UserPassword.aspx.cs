using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1.UserDetail
{
    public partial class UserPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static bool Equals(string txtCheckPWD, string txtNewPWD)
        {
            List<string> msgList = new List<string>();
            if (txtCheckPWD.ToString() == txtNewPWD.ToString())
            {
                msgList.Add("密碼修改成功 !");
            }
            else
            {
                 msgList.Add("請重新確認密碼是否一致");
            }
            if (msgList.Count == 0)//沒有錯誤訊息
                return true;
            else                   //有錯誤訊息
                return false;
        }
        protected void btnNewPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/Login.aspx");
        }
    }
}