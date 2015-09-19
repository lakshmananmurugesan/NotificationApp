using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvHelper;
using System.IO;
using System.Threading;
namespace NotifyMe
{
    class HelperClass
    {
        public HelperClass()
        {
        }

        public static string ProcessCSVFile(string FilePath)
        {
            StringBuilder sBuilder = new StringBuilder();
            string content = string.Empty;
            using (var sr = new StreamReader(@FilePath))
            {
                content = sr.ReadToEnd();
            }

            if (String.IsNullOrEmpty(content))
                return string.Empty;

            var AllEntrys = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (!(AllEntrys.Length > 0))
                return string.Empty;

            var Items = new string[] { };
            List<FileAttribute> fileEntry = new List<FileAttribute>();
            foreach (string Entry in AllEntrys)
            {
                Items = Entry.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (Items.Length == 3)
                    fileEntry.Add(new FileAttribute()
                    {
                        ItemID = Convert.ToInt32(Items[0]),
                        AttributeName = Items[1],
                        AttributeValue = Items[2]
                    });
            }

            if (!(fileEntry.Count > 0))
                return string.Empty;

            fileEntry.ForEach(x => sBuilder.AppendLine(String.Format("{0},{1},{2}", x.ItemID, x.AttributeName, x.AttributeValue)));

            return sBuilder.ToString();
        }
    }

    class FileAttribute
    {
        public int ItemID { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}
