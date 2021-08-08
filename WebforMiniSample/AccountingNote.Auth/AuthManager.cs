﻿using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    /// <summary> 負責處理登入的元件 </summary>
    public class AuthManager
    {
        /// <summary> 檢查目前是否登入 </summary>
            /// <returns></returns>
        public static bool IsLogined()
        {           
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;

        }

        /// <summary> 取得已登入的使用者資訊 (如果沒有登入就回應 null) </summary>
        /// <returns></returns>
        public static UserInfoModel GetCurrentUser()
        {   //檢查現在有沒有人登入
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;
            //沒有登入回傳null，有登入才回傳資訊
            if (account == null)
                return null;

            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

            if (dr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }
                
            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;
        }
   
        /// <summary> 清除登入 </summary>
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
    }
}
