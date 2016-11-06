using PhoneListApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneListApp
{
    public partial class ViewAbonent : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Views view = new Views();
            if (SetIdFromGETParam())
            {
                view.GetAbonentInfo();
                SetLabelsText(view);
            }
            else
            {
                PutMarkupInContentPlaceHolder("Пользователь не найден.");
            }
        }

        private void SetLabelsText(Views view)
        {
            Abonent abonent = Views.abonentInfo;
            lbID.Text = abonent.id.ToString();
            lbFIO.Text = abonent.FIO;
            lbBirthday.Text = abonent.Birthday_date.ToShortDateString();
            lbPassport.Text = abonent.Passport_series;
            lbINN.Text = abonent.INN;
            lbWork.Text = abonent.Work;
            lbEducation.Text = view.GetEduLevel(abonent);
            lbAddress.Text = abonent.Address;
            lbSex.Text = abonent.Sex == "F" ? "женский" : abonent.Sex == "M" ? "мужской" : "не определен";
        }
    }
}