using System;
using System.Collections.Generic;

namespace PhoneListApp.Classes
{
    /// <summary>
    /// Producing html markup
    /// </summary>
    public class Views
    {
        static public int ROWS_ON_PAGE = 10;
        static public string SortColumn;
        static public string SortDirection;
        static public int CurrentPage;
        private DataRepository _data;

        // текущая сортировка столбца (столбец, направление)
        protected Dictionary<string, string> sortCol = new Dictionary<string, string>();
        // сортировка, на которую столбец переключится (столбец, направление)
        protected Dictionary<string, string> sortView = new Dictionary<string, string>();

        public Views()
        {
            _data = new DataRepository();
            _data.SetSortQueryParameters("ID", "asc");
        }

        public string GetSearchPageMarkup()
        {
            string result = string.Empty;
            return result;
        }

        public void SetSortParameters(string sortDir, string sortCol)
        {
            SortColumn = sortCol;
            SortDirection = sortDir;
        }

        private void AddNewSearchField(object fieldValue, string SQLfield, List<string> searchFields)
        {
            string res = string.Empty;
            if (fieldValue.GetType().Equals(typeof(int)))
            {
                int val = (int)fieldValue;
                if (val != -1)
                {
                    res = SQLfield + "='" + val.ToString() + "'";
                }
            }

            if (fieldValue.GetType().Equals(typeof(string)))
            {
                string val = (string)fieldValue;
                if (val != string.Empty)
                {
                    res = SQLfield + "='" + val + "'";
                }
            }

            if (res != string.Empty) {
                searchFields.Add(res);
            }
        }

        public void SetSearchQuery(SearchAbonent sp)
        {
            List<string> searchFields = new List<string>();
            string resultQuery = string.Empty;

            AddNewSearchField(sp.id, "id", searchFields);
            AddNewSearchField(sp.FIO, "fio", searchFields);
            //AddNewSearchField(sp.Birthday_date, "Birthday", searchFields);
            AddNewSearchField(sp.Passport_series, "Passport_series", searchFields);
            AddNewSearchField(sp.INN, "INN", searchFields);
            AddNewSearchField(sp.Work, "Work", searchFields);
            AddNewSearchField(sp.Education, "Education", searchFields);
            AddNewSearchField(sp.Address, "Address", searchFields);
            AddNewSearchField(sp.Sex, "Sex", searchFields);

            foreach(string sf in searchFields)
            {
                resultQuery += " " + sf + " " + sp.UnionOperation;
            }

            if (resultQuery != string.Empty)
            {
                resultQuery = resultQuery.Remove(resultQuery.Length - sp.UnionOperation.Length);
                _data.SetSearchQuery(resultQuery);
            }
        }

        public string GetPage()
        {
            string result = GetPaginationArrows();
            result += GetSearchLink();
            result += GetAbonentsTable();
            return result;
        }

        private string GetSearchLink()
        {
            string result = string.Empty;
            result = "<br><a href=\"Search.aspx\">Search</a>";
            return result;
        }

        private string GetPaginationArrows()
        {
            int rowsCount = _data.GetMaximumRows();
            string back = "<br><a href=\"{0}\">&lt;&lt;</a>";
            string forward = "&nbsp;<a href=\"{0}\">&gt;&gt;</a><br>";
            string backDisabled = "&lt;&lt;";
            string forwardDisabled = "&nbsp;&gt;&gt;";
            string result = (CurrentPage - 1)>=0 ? 
                string.Format(back, BuildGetParameters(SortColumn,SortDirection, CurrentPage - 1)) :
                backDisabled;
            result += (CurrentPage + 1)<=rowsCount ? 
                string.Format(forward, BuildGetParameters(SortColumn, SortDirection, CurrentPage + 1)) :
                forwardDisabled;
            return result;
        }

        public string GetAbonentsTable()
        {
            string result = String.Empty;
            _data.SetSortQueryParameters(SortDirection, SortColumn);
            _data.SetPaginationQueryParameters(CurrentPage, ROWS_ON_PAGE);

//            try
//            {
                _data.FillEducationLevelsList();
                _data.FillAbonentsList();
                _data.AddContactsToAbonentsList();
//            }
//            catch (Exception ex)
//            {
//                return "Error: " + ex.Message;
//            }

            result += "<table id=\"abonents_table\">";
            result += GetTableHead();
            foreach (var abonent in _data.Abonents)
            {
                result += "<tr class=\"tr_data\">";
                result += string.Format("<td>{0}</td>", abonent.id.ToString());
                result += string.Format("<td>{0}</td>", abonent.FIO.ToString());
                result += string.Format("<td>{0}</td>", abonent.Birthday_date.ToShortDateString());
                result += string.Format("<td>{0}</td>", GetAge(abonent));
                result += string.Format("<td>{0}</td>", GetEduLevel(abonent));
                result += string.Format("<td>{0}</td>", abonent.Address);
            }

            return result;
        }

        public string GetAbonentPage()
        {
            return string.Empty;
        }

        private int GetAge(Abonent abonent)
        {
            return DateTime.Now.Year - abonent.Birthday_date.Year;
        }

        protected string GetEduLevel(Abonent abonent)
        {
            Education e = _data.EducationLevels.Find(item => item.id == abonent.Education);
            return e.Level;
        }

        private string GetTableHead()
        {
            string result = String.Empty;
            string[][] tableCols = new string[6][];
            tableCols[0] = new string[2] { "id", "ID" };
            tableCols[1] = new string[2] { "fio", "ФИО" };
            tableCols[2] = new string[2] { "birthday", "Дата рождения" };
            tableCols[3] = new string[2] { "age", "Возраст" };
            tableCols[4] = new string[2] { "education", "Образование" };
            tableCols[5] = new string[2] { "address", "Адрес" };

            result += "<tr>";

            for (int th = 0; th < tableCols.Length; th++)
            {
                string col = tableCols[th][0];
                string name = tableCols[th][1];
                string dir = "desc";
                string dirArrow = string.Empty;
                string href = string.Empty;
                if (col == SortColumn)
                {
                    dir = SortDirection == "asc" ? "desc" : "asc";
                    dirArrow = GetDirSymbol(dir);
                }

                href = BuildGetParameters(col, dir, CurrentPage);

                result += string.Format("<td><a href=\"{0}\">{1} {2}</a></td>", href, name, dirArrow);
            }
            result += "</tr>";

            return result;
        }

        private string BuildGetParameters(string col, string dir, int pageNum)
        {
            return string.Format("?col={0}&dir={1}&p={2}", col, dir, pageNum.ToString());
        }

        private string GetDirSymbol(string dir)
        {
            if (dir == "asc")
            {
                return "&darr;";
            }
            if (dir == "desc")
            {
                return "&uarr;";
            }
            return string.Empty;
        }
    }
}