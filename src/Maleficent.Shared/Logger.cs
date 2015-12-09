using System.Data.SQLite;
using System.IO;

namespace Maleficent.Shared
{
    public static class Logger
    {
        private static string DatabaseLocation = @"C:\TEMP2\MaleficentServicesLog.sqlite";

        static Logger()
        {
            //var location = HttpContext.Current.Server.MapPath("~/MaleficentServicesLog.sqlite");
            if (!File.Exists(DatabaseLocation))
            {
                //SQLiteConnection.CreateFile(location);

                using (SQLiteConnection conn = new SQLiteConnection($"Data Source={DatabaseLocation};Version=3;"))
                {
                    conn.Open();

                    SQLiteCommand command = new SQLiteCommand("CREATE TABLE logs (timestamp DATETIME DEFAULT CURRENT_TIMESTAMP, thread_id INT)", conn);
                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public static void Log(LogMessage message)
        {
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={DatabaseLocation};Version=3;"))
            {
                conn.Open();

                SQLiteCommand command = new SQLiteCommand("INSERT INTO logs (timestamp, thread_id) VALUES (@timestamp,@thread_id)", conn);

                command.Parameters.Add(new SQLiteParameter("@timestamp", message.Time));
                command.Parameters.Add(new SQLiteParameter("@thread_id", message.ThreadId));

                command.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}
