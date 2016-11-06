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
            if (SetIdFromGETParam())
            {
                view.GetAbonentInfo();
                SetControlsValues();
                btnSave.Click += BtnSave_Click;
            }
            else
            {
                PutMarkupInContentPlaceHolder("Пользователь не найден.");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Abonent editAbonent = new Abonent();
            editAbonent.FIO = tbFIO.Text;
            editAbonent.Birthday_date = DateTime.Parse(tbBirthday.Text, new CultureInfo("en-US"));
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
            Abonent abonent = Views.abonentInfo;
            lbID.Text = abonent.id.ToString();
            tbFIO.Text = abonent.FIO;
            tbBirthday.Text = abonent.Birthday_date.ToString(new CultureInfo("en-US"));
            tbPassport.Text = abonent.Passport_series;
            tbINN.Text = abonent.INN;
            tbWork.Text = abonent.Work;
            ddlEducation.SelectedIndex = abonent.Education;
            tbAddress.Text = abonent.Address;
            ddlSex.SelectedValue = abonent.Sex;
        }
    }
}