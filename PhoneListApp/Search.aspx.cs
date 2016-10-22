using PhoneListApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneListApp
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: write code to display the search markup
            // TODO: create methods in Views

            SubmitSearch.Click += SubmitSearch_Click;
        }

        private void SubmitSearch_Click(object sender, EventArgs e)
        {
            string id = TextBoxID.Text;
            if (id != string.Empty)
            {
                // TODO: call the search method and display results
                Views view = new Views();
                string htmlOutput = view.GetPage();
                Response.Write(htmlOutput);
            }
        }
    }
}