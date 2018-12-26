using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoRepairShop.Data.Models;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;
using Microsoft.Office.Interop.Word;

namespace AutoRepairShop.Services
{
    internal class ContractSignatureService
    {
        List<List<string>> servicesList = new List<List<string>>();

        private static object _threadLock = new object();

        private void BuildData(Customer customer)
        {
            servicesList.Add(new List<string>() { "Service", "Part", "Part Cost", "Service Cost" });
            servicesList.Add(new List<string>() { "Diagnostics", "-", "0", Garage.ServicesCatalogue["Diagnoze"].ToString() });
            foreach (var t in customer.MyAgreement.PartsToRepair)
            {
                servicesList.Add(new List<string>() { "Repair", t.Name, "0", Garage.ServicesCatalogue["Repair"].ToString() });
            }
            foreach (var t in customer.MyAgreement.PartsToReplace)
            {
                servicesList.Add(new List<string>() { "Replace", t.Name, t.Cost.ToString(), Garage.ServicesCatalogue["Replace"].ToString() });
            }
            servicesList.Add(new List<string>() { "", "", "TOTAL:", GetTotal(customer).ToString() });
        }

        public double GetTotal(Customer customer)
        {
            var total = customer.MyAgreement.PartsToRepair.Count *
                        Garage.ServicesCatalogue["Repair"];
            foreach (var carPart in customer.MyAgreement.PartsToReplace)
            {
                total += carPart.Cost;
                total += Garage.ServicesCatalogue["Replace"];
            }
            total += Garage.ServicesCatalogue["Diagnoze"];
            return total;
        }

        public void AppendContractText(Customer customer)
        {
            Application app;
            Document doc;
            string docPath;
            lock (_threadLock)
            {
                BuildData(customer);
                app = new Application();
                doc = app.Documents.Open(Path.Combine(@"C:\Users\Yuri.Pustovoy\Documents\Visual Studio 2017\Projects\AutoRepairShop\AutoRepairShop\bin\Debug\AutoRepairContract.docx"));
                docPath = Path.Combine(
                    @"D:\test",
                    $"AgreementsFor_{TimeTool.GetGameTime().ToString(FileFolderManagementService.DatetimeFormat)}",
                    $"AutoRepairAgreement_{customer.Get_Name()}_{TimeTool.GetGameTime().ToString(FileFolderManagementService.DatetimeFormat)}.docx");
                customer.MyAgreement.DocPath = docPath;
                doc.SaveAs(docPath);
                app.Quit();
            }
            app = new Application();
            doc = app.Documents.Open(customer.MyAgreement.DocPath);
            Dictionary<string, string> bookmarks =
                new Dictionary<string, string>
                { {"Amount", ShopManager.Lucy.ApproximateCost(customer).ToString()},
                    { "Date", TimeTool.GetGameTime().Date.ToString()},
                    { "Name", customer.Name},
                    { "Owner", customer.Name} };

            foreach (var bookmark in bookmarks)
            {
                Bookmark bm = doc.Bookmarks[bookmark.Key];
                Range range = bm.Range;
                range.Text = bookmark.Value;
                doc.Bookmarks.Add(bookmark.Key, range);
            }

            Bookmark services = doc.Bookmarks["Work"];
            Range tableRange = services.Range;
            var tableLength = customer.MyAgreement.PartsToRepair.Count +
                              customer.MyAgreement.PartsToReplace.Count + 3;
            var oTable = doc.Tables.Add(tableRange, tableLength, 4);
            oTable.Range.ParagraphFormat.SpaceAfter = 1;
           
            EditTable(oTable, tableLength, 4, servicesList);
         
            doc.SaveAs(customer.MyAgreement.DocPath);
            app.Quit();
        }

        public void EditTable(Table oTable, int columnLength, int rowLength, List<List<string>> data)
        {
            for (int i = 0; i < columnLength; i++)
            {
                EditRow(oTable, i+1, rowLength, data[i]);
            }
        }

        public void EditRow(Table oTable, int row, int rowLength, List<string> text) 
        {
            for (int i = 0; i < rowLength; i++)
            {
                EditCell(oTable, row, i+1, text[i]);
            }

        }

        public void EditCell(Table oTable, int row, int column, string text)
        {
            oTable.Cell(row, column).Range.Text = text;
        }

        public void AddMoreServices(string service, string part, string partCost, string serviceCost, string path, Customer customer)
        {
            Application app = new Application();
            Document doc = app.Documents.Open(path);
            Table servicesTable = app.ActiveDocument.Tables[2];
            var selectedRow = servicesTable.Rows.Count;
            app.Visible = true;
            List<string> additionalService = new List<string>() {service, part, partCost, serviceCost};
            EditRow(servicesTable, selectedRow, 4, additionalService);
            servicesTable.Rows.Add();
            selectedRow = servicesTable.Rows.Count;
            EditCell(servicesTable, selectedRow, 3, "TOTAL");
            EditCell(servicesTable, selectedRow, 4, customer.MyAgreement.GetTotal().ToString());
            doc.SaveAs(path);
            app.Quit();
        }
    }
}