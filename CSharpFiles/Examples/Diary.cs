using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFiles.Examples
{    
    public class Diary
    {
        public record DiaryRecord(DateOnly Date, TimeOnly Time, string Location, string Description, int Duration = 60);
        public static List<DiaryRecord> ReadDiaryEvents(string fileName)
        {
            var diaryEvents = new List<DiaryRecord>();
            using (var streamReader = new StreamReader(fileName))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine() ?? string.Empty;
                    string[] fields = line.Split(',');
                    if (fields.Length == 5)
                    {
                        if (DateOnly.TryParseExact(fields[0], "yyyyMMdd", out DateOnly date)
                            && TimeOnly.TryParseExact(fields[1], "Hss", out TimeOnly time)
                            && int.TryParse(fields[2], out int duration))
                        {
                            var diaryEvent = new DiaryRecord(date, time, fields[3], fields[4], duration);
                            diaryEvents.Add(diaryEvent);
                        }
                    }
                }
            }
            return diaryEvents;
        }

        public static void WriteDiaryEvents(string fileName, List<DiaryRecord> diaryEvents)
        {
            using (var streamWriter = File.AppendText(fileName))
            {
                foreach (var item in diaryEvents)
                {
                    streamWriter.WriteLine($"{item.Date:yyyyMMdd},{item.Time:HHss},{item.Duration},{item.Location},{item.Description}");
                }                
            }
        }
    }
}

