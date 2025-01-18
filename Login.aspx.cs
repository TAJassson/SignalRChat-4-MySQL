﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRChat
{
    public partial class Login : System.Web.UI.Page
    {
        //Class Object
        ConnClass ConnC = new ConnClass();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string Query = "SELECT * FROM livechat_user WHERE Email='" + txtEmail.Value + "' AND Password='" + txtPassword.Value + "'";
            if (ConnC.IsExist(Query))
            {
                string userName = ConnC.GetColumnVal(Query, "UserName");
                Session["UserName"] = userName;
                Session["Email"] = txtEmail.Value;

                // Check if the user is connected
                string connectedQuery = "SELECT COUNT(*) FROM livechat_connected";
                int connectedCount = ConnC.GetCount(connectedQuery);

                if (connectedCount > 0)
                {
                    Response.Redirect("Chat.aspx");

                    // Increment the Connected count in livechat_connected table
                    string updateQuery = "UPDATE livechat_connected SET Connected = Connected + 1";
                    ConnC.ExecuteQuery(updateQuery);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please wait!!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Invalid Email or Password!!');", true);
            }
        }
    }
}