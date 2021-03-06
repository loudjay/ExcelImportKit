﻿using ExcelService;
using ModelImport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFromTest
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                IList<ImportError> errors = new List<ImportError>();
                IList<SampleImport> importList;
                var filePath = RootDirectoryHelper.GetFilePath("./Excels/sampleImport.xlsx");
                var cfgNodeName = "Sample";

                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    importList = new ExcelImportService<SampleImport>().GetParsedPositionImport(fs, errors, cfgNodeName);
                }

                #region custom
                if (errors.Count > 0) LogImportErrors();
                FilterConflictData(importList, errors);
                #endregion

                importList.Where(m => !m.IsError).ToList().ForEach(
                    item => Console.WriteLine(
                            $"{item.Age} - {item.Name} - {item.Height} - " +
                            $"{item.GenderName} - {item.Birthday} - {item.Money} - {item.StateName}")
                        );

                Console.Read();
            }
        }

        private static void LogImportErrors() { }

        private static void FilterConflictData(IList<SampleImport> importList, IList<ImportError> errors) { } 
    }
}
