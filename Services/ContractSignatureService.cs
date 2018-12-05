using System;
using System.Collections.Generic;
using System.IO;
using AutoRepairShop.Data.Models.Humans;
using Microsoft.Office.Interop.Word;

namespace AutoRepairShop.Services
{
    class ContractSignatureService
    {
        public void AppendContractText()
        {
            Application app = new Application();
            Document doc = app.Documents.Open(Path.Combine(Environment.CurrentDirectory, "Report.doc"));

            Dictionary<string, string> bookmarks =
                new Dictionary<string, string> {{"Amount", "3000"}, {"Date", "12-5-2018"}, {"Name", "Ivan"}, {"Work", "Services"}};

            foreach (var bookmark in bookmarks)
            {
                Bookmark bm = doc.Bookmarks[bookmark.Key];
                Range range = bm.Range;
                range.Text = bookmark.Value;
                doc.Bookmarks.Add(bookmark.Key, range);
            }

            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            //Table oTable;
            //Range wrdRng = Bookmarks.get_Item(ref oEndOfDoc).Range;
            //oTable = doc.Tables.Add(wrdRng, 3, 5, ref oMissing, ref oMissing);
            //oTable.Range.ParagraphFormat.SpaceAfter = 6;
            //int r, c;
            //string strText;
            //for (r = 1; r <= 3; r++)
            //for (c = 1; c <= 5; c++)
            //{
            //    strText = "r" + r + "c" + c;
            //    oTable.Cell(r, c).Range.Text = strText;
            //}
            //oTable.Rows[1].Range.Font.Bold = 1;
            //oTable.Rows[1].Range.Font.Italic = 1;
            doc.SaveAs("MyFile.doc");
        }


        public void Sign(Human human)
        {
            
        }

        public void Save()
        {
            
        }
    }
}
