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
    public partial class UserPassword : System.Web.UI.Page
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

        protected void btnNewPassword_Click(object sender, EventArgs e)
        {
            string inp_PWD = this.txtPWD.Text;
            string msg;
            if (!AuthManager.TryLogin(inp_PWD, out msg))
            {
                this.ltMsg.Text = msg;
                return;
            }
            //登入認證成功
            this.PWDPlaceHolder.Visible = true;
            this.ltMsg.Text = "設置密碼時，請將密碼長度設定於8~16碼之間";

            //檢查密碼字數對不對
            if (string.IsNullOrWhiteSpace(this.txtPWD.Text))
            {
                this.ltMsg.Text = "密碼沒打";
                return;
            }
            else if (string.IsNullOrWhiteSpace(this.txtNewPWD.Text))
            {
                this.ltMsg.Text = "確認密碼欄位沒打";
                return;
            }
            else if (this.txtNewPWD.Text.CompareTo(this.txtCheckPWD.Text) != 0)
            {
                this.ltMsg.Text = "兩次輸入的密碼不相同";
                return;
            }
            else if (this.txtNewPWD.Text.Length < 8 || this.txtNewPWD.Text.Length > 16)
            {
                this.ltMsg.Text = "密碼長度必須介於8~16碼之間";
                return;
            }
            this.ltMsg.Text = "更改完成！";
            var currentUser = AuthManager.GetCurrentUser();
            UserInfoManager.UpdateUserPassword(currentUser.ID, this.txtNewPWD.Text);
            Response.Redirect($"UserDetail.aspx?ID={currentUser.ID}&Txt=changeisgood");
        }
    }
}
