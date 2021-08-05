using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1.UserDetail
{
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)                   //可能是按鈕跳回本頁，所以要判斷 postback
            {
                string account = this.Session["UserLoginInfo"] as string;   //把UserLoginInfo轉成字串
                DataRow dr = UserInfoManager.GetUserInfoByAccount(account); //檢查使用者資料存不存在

                if (dr == null)                      //如果帳號不存在，導入登入頁
                {
                    this.Session["UserLoginInfo"] = null;   //清理不必要的資料再導回
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = dr["Account"].ToString();  //如果存在就把dr內的帳號資料轉成文字輸出
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserList.aspx");

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];
            if (!string.IsNullOrWhiteSpace(idText))
                return;

            int id;
            if (int.TryParse(idText, out id))
            {
                //Execute 'delete db'、刪除
                AccountingManager.DeleteAccounting(id);
            }
            Response.Redirect("/SystemAdmin/UserList.aspx");
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserPassword.aspx");
        }
    }
}