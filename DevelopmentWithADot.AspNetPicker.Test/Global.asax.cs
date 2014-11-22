using System;
using System.Web;

namespace DevelopmentWithADot.AspNetPicker.Test
{
    public class Global : HttpApplication
    {
        void Application_Error(object sender, EventArgs e)
        {
	        e.ToString();
        }
    }
}