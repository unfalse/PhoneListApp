using PhoneListApp.Classes;
using System;

namespace PhoneListApp
{
    public partial class Index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Views view = new Views();
            SetColAndDirFromGETParam(view);
            // TODO: complete the pagination support - markup and code behind
            SetPaginationFromGETParam();

            string htmlOutput = view.GetPage();
            Response.Write(htmlOutput);
        }

        private void SetColAndDirFromGETParam(Views view)
        {
            string sortCol = Request.QueryString["col"] ?? "ID";
            string sortDir = Request.QueryString["dir"] ?? "asc";
            view.SetSortParameters(sortDir, sortCol);
        }

        private void SetPaginationFromGETParam()
        {
            // TODO: rewrite to support validation
            string reqPage = Request.QueryString["p"] ?? "0";
            int pageNum = int.TryParse(reqPage, out pageNum) ? pageNum : 0;
            Views.CurrentPage = pageNum;
        }

    }
}