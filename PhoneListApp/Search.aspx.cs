using PhoneListApp.Classes;
using System;

namespace PhoneListApp
{
    public partial class Search : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: write code to display the search markup
            // TODO: create methods in Views
            if (IsPostBack)
            {
                btnSubmitSearch.Click += SubmitSearch_Click;
            }
        }

        private void SubmitSearch_Click(object sender, EventArgs e)
        {
            SearchAbonent abonent = new SearchAbonent();
            int outId = 0;
            abonent.id = int.TryParse(tbID.Text, out outId) ? outId : -1;
            abonent.FIO = tbFIO.Text;
            //abonent.Birthday_date
            abonent.Passport_series = tbPassport.Text;
            abonent.INN = tbINN.Text;
            abonent.Work = tbWork.Text;
            abonent.Education = int.Parse(ddlEducation.SelectedValue);
            abonent.Address = tbAddress.Text;
            abonent.Sex = ddlSex.SelectedValue!="N" ? ddlSex.SelectedValue : string.Empty;
            abonent.UnionOperation = "or";

            // TODO: call the search method and display results
            Views view = new Views();
            view.SetSearchQuery(abonent);

            string htmlOutput = view.GetPage();
            PutMarkupInContentPlaceHolder(htmlOutput);
            //Response.Write(htmlOutput);
        }
    }
}