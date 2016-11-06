using PhoneListApp.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneListApp
{
    public partial class EditAbonent : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SetIdFromGETParam())
                {
                    view.GetAbonentInfo();
                    SetControlsValues();
                }
                else
                {
                    PutMarkupInContentPlaceHolder("Пользователь не найден.");
                }
            }
            else
            {
                btnSave.Click += BtnSave_Click;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // TODO: вынести culture в константы
            string culture = "ru-RU";
            Abonent editAbonent = new Abonent();
            editAbonent.id = int.Parse(lbID.Text);
            editAbonent.FIO = tbFIO.Text;
            editAbonent.Birthday_date = 
                DateTime.Parse(tbBirthday.Text, new CultureInfo(culture));
            editAbonent.Passport_series = tbPassport.Text;
            editAbonent.INN = tbINN.Text;
            editAbonent.Work = tbWork.Text;
            editAbonent.Education = int.Parse(ddlEducation.SelectedValue);
            editAbonent.Address = tbAddress.Text;
            editAbonent.Sex = ddlSex.SelectedValue != "N" ? ddlSex.SelectedValue : string.Empty;
            string resultMessage = view.UpdateAbonent(editAbonent);
            PutMarkupInContentPlaceHolder(resultMessage);
        }

        private void SetControlsValues()
        {
            string culture = "ru-RU";
            Abonent abonent = Views.abonentInfo;
            lbID.Text = abonent.id.ToString();
            tbFIO.Text = abonent.FIO;
            tbBirthday.Text = 
                abonent.Birthday_date.ToString(
                    CultureInfo.CreateSpecificCulture(culture).DateTimeFormat.ShortDatePattern, 
                    new CultureInfo(culture));
            tbPassport.Text = abonent.Passport_series;
            tbINN.Text = abonent.INN;
            tbWork.Text = abonent.Work;
            ddlEducation.SelectedValue = abonent.Education.ToString();
            tbAddress.Text = abonent.Address;
            ddlSex.SelectedValue = abonent.Sex;
            hlEditLink.NavigateUrl = "EditAbonent.aspx?id="
                + abonent.id.ToString() + "&remove=true";
        }
    }
}