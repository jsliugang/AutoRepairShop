using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;
using Microsoft.Office.Interop.Word;

namespace AutoRepairShop.Services
{
    internal class ContractSignatureService
    {
        public void AppendContractText()
        {
            Application app = new Application();
            Document doc = app.Documents.Open(Path.Combine(@"C:\Users\Yuri.Pustovoy\Documents\Visual Studio 2017\Projects\AutoRepairShop\AutoRepairShop\bin\Debug\AutoRepairContract.docx"));

            Dictionary<string, string> bookmarks =
                new Dictionary<string, string>
                { {"Amount", ShopManager.Lucy.ApproximateCost().ToString()},
                    { "Date", TimeTool.GetGameTime().Date.ToString()},
                    { "Name", ShopManager.CurrentCustomer.Name},
                    { "Owner", ShopManager.CurrentCustomer.Name} };

            foreach (var bookmark in bookmarks)
            {
                Bookmark bm = doc.Bookmarks[bookmark.Key];
                Range range = bm.Range;
                range.Text = bookmark.Value;
                doc.Bookmarks.Add(bookmark.Key, range);
            }

            Bookmark services = doc.Bookmarks["Work"];
            Range tableRange = services.Range;
            var tableLength = ShopManager.CurrentCustomer.MyAgreement.PartsToRepair.Count +
                              ShopManager.CurrentCustomer.MyAgreement.PartsToReplace.Count + 3;
            var oTable = doc.Tables.Add(tableRange, tableLength, 4);
            oTable.Range.ParagraphFormat.SpaceAfter = 1;
            int r, c;
            StringBuilder sb = new StringBuilder();
            oTable.Cell(1, 1).Range.Text = "Service";
            oTable.Cell(1, 2).Range.Text = "Part";
            oTable.Cell(1, 3).Range.Text = "Part Cost";
            oTable.Cell(1, 4).Range.Text = "Service Cost";
            oTable.Cell(2, 1).Range.Text = "Diagnostics";
            oTable.Cell(2, 4).Range.Text = ShopManager.ServicesCatalogue["Diagnoze"].ToString();
            if (ShopManager.CurrentCustomer.MyAgreement.PartsToRepair.Count != 0)
            {
                var part = 0;
                for (r = 3; r <= ShopManager.CurrentCustomer.MyAgreement.PartsToRepair.Count + 2; r++)
                {
                    for (c = 1; c <= 4; c++)
                    {
                        switch (c)
                        {
                            case 1:
                            {
                                sb.Append($"Repair");
                                oTable.Cell(r, c).Range.Text = sb.ToString();
                                sb.Clear();
                                    break;
                            }
                            case 2:
                            {                             
                                sb.Append($"{ShopManager.CurrentCustomer.MyAgreement.PartsToRepair[part].Name}");
                                part++;
                                oTable.Cell(r, c).Range.Text = sb.ToString();
                                sb.Clear();
                                    break;
                            }
                            case 3:
                            {
                                break;
                            }
                            case 4:
                            {
                                sb.Append($"{ShopManager.ServicesCatalogue["Repair"]}");
                                oTable.Cell(r, c).Range.Text = sb.ToString();
                                sb.Clear();
                                    break;
                            }
                        }
                    }
                }
            }

            if (ShopManager.CurrentCustomer.MyAgreement.PartsToReplace.Count != 0)
            {
                var part = 0;
                for (r = ShopManager.CurrentCustomer.MyAgreement.PartsToRepair.Count + 3; r <= ShopManager.CurrentCustomer.MyAgreement.PartsToReplace.Count + 2 + ShopManager.CurrentCustomer.MyAgreement.PartsToRepair.Count; r++)
                {
                    for (c = 1; c <= 4; c++)
                    {
                        switch (c)
                        {
                            case 1:
                            {
                                sb.Append("Replace");
                                oTable.Cell(r, c).Range.Text = sb.ToString();
                                sb.Clear();
                                    break;
                            }
                            case 2:
                            {
                                sb.Append($"{ShopManager.CurrentCustomer.MyAgreement.PartsToReplace[part].Name}");
                                oTable.Cell(r, c).Range.Text = sb.ToString();
                                sb.Clear();
                                    break;
                            }
                            case 3:
                            {
                                sb.Append($"{ShopManager.CurrentCustomer.MyAgreement.PartsToReplace[part].Cost}");
                                oTable.Cell(r, c).Range.Text = sb.ToString();
                                sb.Clear();
                                part++;
                                    break;
                            }
                            case 4:
                            {
                                sb.Append($"{ShopManager.ServicesCatalogue["Replace"]}");
                                oTable.Cell(r, c).Range.Text = sb.ToString();
                                sb.Clear();
                                    break;
                            }
                        }
                    }
                }
            }         
            oTable.Cell(tableLength, 3).Range.Text = "TOTAL";
            var total = ShopManager.CurrentCustomer.MyAgreement.PartsToRepair.Count *
                           ShopManager.ServicesCatalogue["Repair"];
            foreach (var carPart in ShopManager.CurrentCustomer.MyAgreement.PartsToReplace)
            {
                total += carPart.Cost;
                total += ShopManager.ServicesCatalogue["Replace"];
            }
            oTable.Cell(tableLength, 4).Range.Text = total.ToString();
            oTable.Rows[tableLength].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Bold = 1;
            string docPath = Path.Combine(
                @"D:\test",
                $"AgreementsFor_{TimeTool.GetGameTime().ToString(FileFolderManagementService.DatetimeFormat)}",
                $"AutoRepairAgreement_{ShopManager.CurrentCustomer.Get_Name()}_{TimeTool.GetGameTime().ToString(FileFolderManagementService.DatetimeFormat)}.docx");
            ShopManager.CurrentCustomer.MyAgreement.DocPath = docPath;
            doc.SaveAs(docPath);
            app.Quit();
        }

        public void AddMoreServices(string service, string part, string partCost, string serviceCost, string path)
        {
            Application app = new Application();
            Document doc = app.Documents.Open(path);
            Table servicesTable = app.ActiveDocument.Tables[2];
            var selectedRow = servicesTable.Rows.Count;
            app.Visible = true;
            servicesTable.Cell(selectedRow, 1).Range.Text = service;
            servicesTable.Cell(selectedRow, 2).Range.Text = part;
            servicesTable.Cell(selectedRow, 3).Range.Text = partCost;
            servicesTable.Cell(selectedRow, 4).Range.Text = serviceCost;
            servicesTable.Rows.Add();
            selectedRow = servicesTable.Rows.Count;
            servicesTable.Cell(selectedRow, 3).Range.Text = "TOTAL";
            servicesTable.Cell(selectedRow, 4).Range.Text = (ShopManager.CurrentCustomer.MyAgreement.TotalPartCost+ ShopManager.CurrentCustomer.MyAgreement.TotalServicesCost).ToString();
            doc.SaveAs(path);
            app.Quit();
        }
    }
}
