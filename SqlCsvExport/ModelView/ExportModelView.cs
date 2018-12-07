using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;

namespace SqlCsvExport.ModelView
{
    public class ExportModelView : ModelView, IFolderChange
    {
        private string folderName;
        private string sqlQuery;

        public string FolderName
        {
            get => folderName;

            set
            {
                folderName = value;
                OnPropertyChanged();
            }
        }

        public string SqlQuery
        {
            get => sqlQuery;
            set
            {
                sqlQuery = value;
                OnPropertyChanged();
            }
        }

        internal bool DoExport()
        {
            using (var context = new Model.NorthWind())
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    if (context.Database.Connection is SqlConnection conn)
                    {
                        connection.ConnectionString = conn.ConnectionString;

                        connection.Open();

                        using (var cmd = new SqlCommand())
                        {
                            cmd.Connection = connection;

                            cmd.CommandText = SqlQuery;

                            SqlDataReader reader = null;
                            try
                            {
                                reader = cmd.ExecuteReader();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Ошибка выполнения запроса");
                                return false;
                            }

                            FileStream stream;
                            string FileName = FolderName + "\\out.cvs";
                            try
                            {
                                stream = new FileStream(FileName, FileMode.Create);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Ошибка создания файла");
                                return false;
                            }

                            using (stream)
                            {
                                using (var sw = new StreamWriter(stream))
                                {
                                    var columnNames = Enumerable.Range(0, reader.FieldCount)
                                        .Select(reader.GetName).ToList();

                                    sw.Write(string.Join(",", columnNames));

                                    sw.Write("\n");

                                    int maxRows = RowCount ?? int.MaxValue;
                                    for (int i = 0; i < maxRows; ++i)
                                    {
                                        if (!reader.Read())
                                            break;

                                        var values = Enumerable.Range(0, reader.FieldCount)
                                            .Select(x=>FormatToCvs(reader, x)).ToList();

                                        sw.Write(string.Join(",", values));
                                        sw.Write("\n");
                                    }
                                }
                            }
                        }
                        return true;
                    }

                    MessageBox.Show("Неправильный контекст");
                    return false;
                }
            }
        }


        private static string FormatToCvs(SqlDataReader reader, int i)
        {
            object obj = reader.GetValue(i);

            if (obj == null )
                return string.Empty;

            Type type = obj.GetType();

            if (type == typeof(DBNull))
                return string.Empty;

            if (type == typeof(Byte[]))
            {
                byte[] arr = obj as byte[];
                var ret = "\"" + System.Convert.ToBase64String(arr) + "\"";
                return ret;
            }

            if (type == typeof(System.String))
                return "\"" + obj.ToString().Replace("\"", "\"\"") + "\"";

            return obj.ToString();
        }

        public int? RowCount
        {
            get;
            set;
        }
    }
}
