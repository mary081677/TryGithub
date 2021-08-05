using AccountingNote.DBSource;
using AccountingNote;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _0728_2.SystemAdmin
{
    public partial class Userlnfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)                   //可能是按鈕跳回本頁，所以要判斷 postback
            {
                if (this.Session["UserLoginInfo"] == null)  //檢查是否登入過，沒登入過導回登入頁
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                string account = this.Session["UserLoginInfo"] as string;   //把UserLoginInfo轉成字串
                DataRow dr = UserInfoManager.GetUserInfoByAccount(account); //檢查使用者資料存不存在

                if (dr == null)                      //如果帳號不存在，導入登入頁
                {
                    this.Session["UserLoginInfo"] = null;   //清理不必要的資料再導回
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = dr["Account"].ToString();  //如果存在就把dr內的帳號資料轉成文字輸出
                this.ltName.Text = dr["Name"].ToString();        //如果存在就把dr內的姓名資料轉成文字輸出
                this.ltEmail.Text = dr["Email"].ToString();      //如果存在就把dr內的信箱資料轉成文字輸出
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this.Session["UserLoginInfo"] = null;   //清除登入資訊，導回登入頁
            Response.Redirect("/Login.aspx");
        }
    }
}