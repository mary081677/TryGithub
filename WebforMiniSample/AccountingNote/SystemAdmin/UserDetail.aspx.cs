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
            //check is logined
            //看看有沒有登入，把帳號拿出來
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            //用Session取得資料再轉型
            string account = this.Session["UserLoginInfo"] as string;
            var drUserInfo = UserInfoManager.GetUserInfoByAccount(account); //透過帳號去查個人資訊的ID

            if (drUserInfo == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            if (!this.IsPostBack)
            {
                // Check is create mode or edit mode;判斷是新增還是編輯模式
                if (this.Request.QueryString["ID"] == null)
                {
                    this.btnDelete.Visible = false;
                }
                else
                {
                    this.btnDelete.Visible = true;

                    string idText = this.Request.QueryString["ID"];
                    int id;
                    if (int.TryParse(idText, out id))
                    {
                        var drAccounting = AccountingManager.GetAccounting(id, drUserInfo["ID"].ToString());

                        if (drAccounting == null)
                        {
                            this.ltMsg.Text = "Data doesn't exist";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
                            //兩者資料相同才做輸出
                            if (drAccounting["UserID"].ToString() == drUserInfo["userID"].ToString())
                                this.ltAccount.Text = drAccounting["Account"].ToString();
                            this.txtName.Text = drAccounting["Name"].ToString();
                            this.txtEmail.Text = drAccounting["Email"].ToString();
                        }
                    }
                    else
                    {
                        this.ltMsg.Text = "ID is required";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }
            Response.Redirect("/SystemAdmin/UserList.aspx");

        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.ltAccount.Text)) msgList.Add("請輸入帳號");
            if (string.IsNullOrWhiteSpace(this.txtName.Text)) msgList.Add("請輸入姓名");
            if (string.IsNullOrWhiteSpace(this.txtEmail.Text)) msgList.Add("請輸入信箱");
            if (this.txtPWD.Visible)
            {
                if (string.IsNullOrWhiteSpace(this.txtPWD.Text)) msgList.Add("請輸入密碼");
                else if (string.IsNullOrWhiteSpace(this.txtCheckPWD.Text)) msgList.Add("請輸入確認密碼欄位");
                else if (this.txtPWD.Text.CompareTo(this.txtCheckPWD.Text) != 0) msgList.Add("密碼輸入不相同請確認");
                else if (this.txtPWD.Text.Length < 8 || this.txtPWD.Text.Length > 16) msgList.Add("密碼必須在8~16碼之間");
            }

            errorMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
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