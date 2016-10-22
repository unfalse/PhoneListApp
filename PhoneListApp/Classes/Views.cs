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

        public void SetSortParameters(string sortDir, string sortCol)
        {
            SortColumn = sortCol;
            SortDirection = sortDir;
        }

        public Views()
        {
            _data = new DataRepository();
            _data.SetSortQueryParameters("ID", "asc");
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
                string dirArrow = "";
                if (col == SortColumn)
                {
                    dir = SortDirection=="asc" ? "desc" : "asc";
                    dirArrow = GetDirSymbol(dir);
                }
                
                result += string.Format("<td><a href=\"?col={0}&dir={1}\">{2} {3}</a></td>", col, dir, name, dirArrow);
            }
            result += "</tr>";

            return result;
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

        public string GetPage()
        {
            string result = GetPaginationArrows() + GetAbonentsTable();
            return result;
        }

        private string GetPaginationArrows()
        {
            string back = "<br><a href=\"?p={0}\">&lt;&lt;</a>";
            string forward = "&nbsp;<a href=\"?p={0}\">&gt;&gt;</a><br>";
            string result = string.Format(back, CurrentPage - 1) + string.Format(forward, CurrentPage + 1);
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
    }
}