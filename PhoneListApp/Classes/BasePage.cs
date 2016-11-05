using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneListApp.Classes
{
    public class BasePage : System.Web.UI.Page
    {
        protected void PutMarkupInContentPlaceHolder(string markup)
        {
            ContentPlaceHolder c = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            if (c != null)
            {
                LiteralControl l = new LiteralControl();
                l.Text = markup;
                c.Controls.Add(l);
            }
        }

        protected void SetColAndDirFromGETParam(Views view)
        {
            string sortCol = Request.QueryString["col"] ?? "ID";
            string sortDir = Request.QueryString["dir"] ?? "asc";
            view.SetSortParameters(sortDir, sortCol);
        }

        protected void SetPaginationFromGETParam()
        {
            string reqPage = Request.QueryString["p"] ?? "0";
            int pageNum = int.TryParse(reqPage, out pageNum) ? pageNum : 0;
            Views.CurrentPage = pageNum;
        }

        protected bool SetIdFromGETParam()
        {
            bool res = false;
            string id = Request.QueryString["id"] ?? "";
            int abonentId = int.TryParse(id, out abonentId) ? abonentId : -1;
            if (abonentId > -1)
            {
                res = true;
                Views.abonentId = abonentId;
            }
            return res;
        }
    }
}