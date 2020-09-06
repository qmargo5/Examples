using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XlsxSqlServerImporter
{
    class Program
    {
        private static List<string> ColumsName = new List<string>
            {
                        "Статус",
                        "GUID",
                        "Сокращенное наименование",
                        "Полное наименование",
                        "Юридический адрес",
                        "ИНН",
                        "КПП",
                        "Код ОКПО",
                        "ОГРН",
                        "Фамилия",
                        "Имя",
                        "Отчество",
                        "Головная организация"
            };

        private static int Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                BulkCopy(stopWatch);
            }
            catch (Exception ex)
            {
                Log(stopWatch, ex.Message + " " + ex.ToString());
                return 1;
            }

            stopWatch.Stop();
            return 0;
        }

        private static void BulkCopy(Stopwatch stopWatch)
        {
            Log(stopWatch, "Prepairing...");
            using (var stream = File.Open("./ALL.xls", FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration
            {

            }))
            {
                Log(stopWatch, "P: Excel reader created");
                var headers = new List<int>();
                var asDataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true,
                        ReadHeaderRow = rowReader =>
                        {
                            for (var i = 0; i < rowReader.FieldCount; i++)
                            {
                                var v = Convert.ToString(rowReader.GetValue(i));
                                if (ColumsName.Contains(v))
                                    headers.Add(i);
                            }
                        },
                        FilterColumn = (c, i) => headers.Contains(i),
                        FilterRow = (q) => q.GetValue(5).Equals("2, Эталон")
                    },
                    FilterSheet = (e, i) => e.Name.Contains("Контрагенты"),
                }
                );
                Log(stopWatch, "P: Dataset opened");
                String ConString = File.ReadAllText("./ConnectionString.txt");
                SqlBulkCopy bulkCopy = new SqlBulkCopy(ConString)
                {
                    DestinationTableName = "[dbo].[Контрагенты]",
                    BatchSize = 10000,
                };

                ColumsName.Remove("Статус");
                foreach (var name in ColumsName)
                {
                    bulkCopy.ColumnMappings.Add(name, name);
                }
                Log(stopWatch, "P: Bulk copier created");
                Log(stopWatch, "Prepairing completed");

                for (var i = 0; i < asDataSet.Tables.Count; i++)
                {
                    var dataTableReader = asDataSet.Tables[i].CreateDataReader();
                    bulkCopy.WriteToServer(dataTableReader);
                    Log(stopWatch, (i + 1) + " table uploaded");
                }

                Log(stopWatch, "All tables uploaded!");

                bulkCopy.Close();
            }
        }

        private static void Log(Stopwatch stopWatch, string data = "")
        {
            try
            {
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);

                var logline = $"{DateTime.Now} {elapsedTime} {data}{Environment.NewLine}";
                File.AppendAllText("./log.txt", logline);
                Console.WriteLine(logline);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.ToString());
            }
        }
    }
}
