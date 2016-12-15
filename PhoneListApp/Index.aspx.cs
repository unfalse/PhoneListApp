using PhoneListApp.Classes;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneListApp
{
    public partial class Index1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Views view = new Views();
            SetColAndDirFromGETParam(view);
            // TODO: complete the pagination support - goto start and goto end
            SetPaginationFromGETParam();

            string htmlOutput = view.GetPage();
            PutMarkupInContentPlaceHolder(htmlOutput);
        }
    }
}