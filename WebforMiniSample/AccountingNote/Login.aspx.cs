using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] is null)
            {
                this.plcLogin.Visible = true;
            }
            else
            {
                //使用者假設有登入過
                this.plcLogin.Visible = false;
                //有登入過直接找使用者資料
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {            
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPWD.Text;

            //check empty
            if(string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            {
                this.ltlMsg.Text = "Account / PWD is required.";
                return;
            }

            var dr = UserInfoManager.GetUserInfoByAccount(inp_Account);

            //check null
            if (dr == null)
            {
                this.ltlMsg.Text = "Account doesn't exists.";
                return;
            }
            //check account / pwd
            if (string.Compare(dr["Account"].ToString(), inp_Account, true) == 0 &&
                string.Compare(dr["PWD"].ToString(), inp_PWD, false) == 0)
            {   //確認Session["UserLoginInfo"] = 帳號後跳頁
                this.Session["UserLoginInfo"] = dr["Account"].ToString();   
                Response.Redirect("/SystemAdmin/UserInfo.aspx"); //從SystemAdmin中導入UserInfo資料
            }
            else
            {   //否則就提示登入失敗請檢查帳號密碼並退出
                this.ltlMsg.Text = "Login fail. Please check Account / PWD.";
                return;
            }
        }
    }
}