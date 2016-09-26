<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception error = Server.GetLastError();
        string errorPage = @"~\Error.aspx";
        Page currentPage = Context.CurrentHandler as Page;
        if (currentPage!=null&&currentPage.Master==null)
        {
            errorPage = @"~\DialogError.aspx";
        }
        //Server.ClearError();
        SGM.Common.Exception.ExceptionHandler.HandleUIException(error);
        Response.Redirect(errorPage);
    }

    void Session_Start(object sender, EventArgs e)
    {
        //Session["ECountServiceProxy"] = new ECountServiceProxy("ECountClient");
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && User.Identity.AuthenticationType == "Forms")
        {
            FormsIdentity identity = User.Identity as FormsIdentity;
            ECountIdentity newIdentiy = new ECountIdentity(identity.Ticket);
            ECountPrincipal newPrincipal = new ECountPrincipal(newIdentiy);
            Context.User = newPrincipal;
            System.Threading.Thread.CurrentPrincipal = newPrincipal;

        }
    }

</script>

