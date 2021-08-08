using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1.SystemAdmin
{
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check is logined 檢查登入
            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            //用Session取得資料再轉型
            string account = this.Session["UserLoginInfo"] as string;
            var currentUser = AuthManager.GetCurrentUser();
            if (currentUser == null)                      //如果帳號不存在，導入登入頁
            {
                this.Session["UserLoginInfo"] = null;   //清理不必要的資料再導回
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
                        var drAccounting = AccountingManager.GetAccounting(id, currentUser.ID);

                        if (drAccounting == null)
                        {
                            this.ltMsg.Text = "Data doesn't exist";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {   
                            //兩者資料相同才做輸出//if (drAccounting["UserID"].ToString() == drUserInfo["userID"].ToString())                                        
                            this.ddlActType.SelectedValue = drAccounting["ActType"].ToString();
                            this.txtAmount.Text = drAccounting["Amount"].ToString();
                            this.txtCaption.Text = drAccounting["Caption"].ToString();
                            this.txtDesc.Text = drAccounting["Body"].ToString();
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
            //檢查控制項是否通過
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            UserInfoModel currentUser = AuthManager.GetCurrentUser();  
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string userID = currentUser.ID;
            string actTypeText = this.ddlActType.SelectedValue;
            string amountText = this.txtAmount.Text;
            string caption = this.txtCaption.Text;
            string body = this.txtDesc.Text;

            int amount = Convert.ToInt32(amountText); //轉型 
            int actType = Convert.ToInt32(actTypeText);

            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText))
            {
                //Execute 'Insert into db'、新增
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else
            {   //編輯
                int id;
                if (int.TryParse(idText, out id))
                {
                    //Execute 'update db'
                    AccountingManager.UpdateAccounting(id, userID, caption, amount, actType, body);
                }
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {   //字串清單
            List<string> msgList = new List<string>();
            //Type；確認值是不是0跟1
            if (this.ddlActType.SelectedValue != "0"
                && this.ddlActType.SelectedValue != "1")
            {   //假如不是0或1
                msgList.Add("Type must be 0 or 1.");
            }

            //Amount
            if (string.IsNullOrEmpty(this.txtAmount.Text))
            {   //如果是空字串提示這是必填
                msgList.Add("Amount is required");
            }
            else
            {   //如果轉換整數失敗要提示
                int tempInt;
                if (!int.TryParse(this.txtAmount.Text, out tempInt))
                {
                    msgList.Add("Amount must be a number. required");
                }

                if (tempInt < 0 || tempInt > 1000000)
                {
                    msgList.Add("Amount must between 0 and 1,000,000 .");
                }
            }

            errorMsgList = msgList;

            if (msgList.Count == 0)//沒有錯誤訊息
                return true;
            else                   //有錯誤訊息
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
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
    }
}