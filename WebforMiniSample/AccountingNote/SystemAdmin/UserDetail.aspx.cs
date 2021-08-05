using System;
using System.Collections.Generic;
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

            
        }
    

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //檢查控制項是否通過
            Response.Redirect("/SystemAdmin/UserList.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserDetail.aspx");
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserPassword.aspx");
        }
    }
}