using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PhoneListApp.Classes
{
    /// <summary>
    /// Call order:
    /// 1. FillEducationLevelsList
    /// 2. FillAbonentsList
    /// 3. AddContactsToAbonentsList
    /// </summary>
    public class DataRepository
    {
        private const string CONNECTION_STRING_NAME = "PhoneListDBConnectionString";

        public List<Abonent> Abonents { get { return _abonents; } }
        public List<Education> EducationLevels { get { return _educationLevels; } }

        private List<Abonent> _abonents = new List<Abonent>();
        private List<Education> _educationLevels = new List<Education>();

        private string _connectionString;

        private string _educationQuery = @"select * from EducationLevels";
        private string _abonentsQuery = @"select * from Abonents as a";
        private string _abonentsRowsCount = @"select count(id) RowsCount from Abonents";
        private string _contactTypes = @"select * from ContactTypes";
        // TODO: write search templates for every field or make it in another way
        private string _searchTemplate = @" where {0}";
        private string _sortQueryTemplate = @" order by a.{0} {1}";
        private string _paginateTemplate = @" offset {0} rows fetch next {1} rows only"; // "offset" is a keyword since MS SQL 2012
        private string _contactsQueryTemplate = @"select * from Contacts where AbonentId in ({0})";
        private string _contactsNestedAbonentsQuery = @"select id from Abonents as a";
        
        private string _resultAbonentQuery = string.Empty;
        private string _contactsQuery = string.Empty;
        private string _paginateQuery = string.Empty;
        private string _sortQuery = string.Empty;
        private string _searchQuery = string.Empty;

        public DataRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
        }

        public void SetSearchQuery(string rightPartOfQuery)
        {
            _searchQuery = _abonentsQuery + string.Format(_searchTemplate, rightPartOfQuery);
        }

        public void SetSortQueryParameters(string sortDir, string sortCol)
        {
            // фикс для сортировки по возрасту
            if (sortCol == "age")
            {
                sortCol = "birthday";
            }
            _sortQuery = string.Format(_sortQueryTemplate, sortCol, sortDir);
        }

        public void SetPaginationQueryParameters(int currentPage, int ROWS_ON_PAGE)
        {
            int startFromRow = currentPage * ROWS_ON_PAGE;
            _paginateQuery = string.Format(_paginateTemplate, startFromRow, ROWS_ON_PAGE);
        }

        public int GetMaximumRows()
        {
            int rowsCount = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(_abonentsRowsCount, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rowsCount = reader.GetFieldValue<int>(0);
                    }
                }
            }
            return rowsCount;
        }

        public void FillEducationLevelsList()
        {
            _educationLevels.Clear();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmdEdu = new SqlCommand(_educationQuery, connection);
                using (SqlDataReader reader = cmdEdu.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Education edu = new Education();
                            edu.id = reader.GetFieldValue<Int32>(0);
                            edu.Level = reader.GetFieldValue<String>(1);
                            _educationLevels.Add(edu);
                        }
                    }
                }
            }
        }

        public void FillAbonentsList()
        {
            _abonents.Clear();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (_searchQuery != string.Empty)
                {
                    _resultAbonentQuery = _searchQuery;
                }
                else
                {
                    _resultAbonentQuery = _abonentsQuery + _sortQuery + _paginateQuery;
                }

                SqlCommand cmd = new SqlCommand(_resultAbonentQuery, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int rowId = reader.GetFieldValue<Int32>(0);
                            Abonent abonent = new Abonent();
                            abonent.id = reader.GetFieldValue<Int32>(0);
                            abonent.FIO = reader.GetFieldValue<String>(1);
                            abonent.Birthday_date = reader.GetFieldValue<DateTime>(2);
                            abonent.Passport_series = reader.GetFieldValue<String>(3);
                            abonent.INN = reader.GetFieldValue<String>(4);
                            abonent.Work = reader.GetFieldValue<String>(5);
                            abonent.Education = reader.GetFieldValue<Int32>(6);
                            abonent.Address = reader.IsDBNull(7) ? String.Empty : reader.GetFieldValue<String>(7);
                            abonent.Sex = reader.GetFieldValue<String>(8);
                            abonent.Photo = reader.IsDBNull(9) ? String.Empty : reader.GetFieldValue<String>(9);
                            abonent.Contacts = null;

                            _abonents.Add(abonent);
                        }
                    }
                }
            }
        }

        public void AddContactsToAbonentsList()
        {
            string abonentsQuery = _contactsNestedAbonentsQuery + _sortQuery + _paginateQuery;
            _contactsQuery = string.Format(_contactsQueryTemplate, abonentsQuery);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(_contactsQuery, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int contactId = -1;
                        int contactType = 0;
                        string contactValue = "";
                        bool newRow = reader.Read();

                        while (newRow)
                        {
                            int rowId = reader.GetFieldValue<Int32>(0);
                            Abonent abonent = _abonents.SingleOrDefault(a => a.id == rowId);
                            if (abonent != null)
                            {
                                do
                                {
                                    contactId = reader.IsDBNull(0) ? -1 : reader.GetFieldValue<Int32>(0);
                                    contactType = reader.IsDBNull(1) ? -1 : reader.GetFieldValue<Int32>(1);
                                    contactValue = reader.IsDBNull(2) ? "" : reader.GetFieldValue<String>(2);
                                    if (contactType != -1)
                                    {
                                        if (abonent.Contacts == null)
                                        {
                                            abonent.Contacts = new List<Contact>();
                                        }
                                        abonent.Contacts.Add(new Contact()
                                        {
                                            ContactId = contactId,
                                            ContactType = contactType,
                                            ContactValue = contactValue
                                        });
                                    }

                                    newRow = reader.Read();
                                    if (newRow)
                                        rowId = reader.GetFieldValue<Int32>(0);
                                    else
                                        break;
                                } while (rowId == abonent.id);
                            }
                            else
                            {
                                // abonent was not found, get the next row
                                newRow = reader.Read();
                            }
                        }

                    }
                }
            }
        }

    }
}