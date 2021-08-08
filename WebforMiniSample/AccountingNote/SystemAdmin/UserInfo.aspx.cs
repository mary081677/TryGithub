using AccountingNote.DBSource;
using AccountingNote;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.Auth;

namespace _0728_2.SystemAdmin
{
    public partial class Userlnfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)                   //可能是按鈕跳回本頁，所以要判斷 postback
            {
                // 如果尚未登入，導入至登入夜
                if (!AuthManager.IsLogined())
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                var currentUser = AuthManager.GetCurrentUser();            
                if (currentUser == null)                      //如果帳號不存在，導入登入頁
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = currentUser.Account;  //如果存在就把dr內的帳號資料轉成文字輸出
                this.ltName.Text = currentUser.Name;        //如果存在就把dr內的姓名資料轉成文字輸出
                this.ltEmail.Text = currentUser.Email;      //如果存在就把dr內的信箱資料轉成文字輸出
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManager.Logout(); //登出，並導致登入頁
            Response.Redirect("/Login.aspx");
        }
    }
}