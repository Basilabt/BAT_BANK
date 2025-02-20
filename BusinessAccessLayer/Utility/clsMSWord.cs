using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Word = Microsoft.Office.Interop.Word;

namespace BusinessAccessLayer.Utility
{
    public class clsMSWord
    {
        public static string buildMSWordMonthlySummaryDocument()
        {
            Word.Application wordApp = new Word.Application();
            try
            {
                wordApp.Visible = false; 

                Word.Document documnet = wordApp.Documents.Add();  
                Word.Paragraph paragraph = documnet.Paragraphs.Add();
                

                List<clsActionHistoryDTO> actions = clsAction.getAccountActionsHistory(clsGlobal.account.accountID);
                List<clsEPurchaseHistoryDTO> purchases = clsEPurchase.getAllPurchasesHistory(clsGlobal.account.accountID);

                paragraph.Range.Text += "\t\t\t\t\t*** Actions ***";
                paragraph.Range.Text += "\n\n";

                foreach (var action in actions)
                {
                    string line = $"Action: {action.actionType}\t|\t Receiver: {action.receivedAccountNumber}\t|\tAmount: {action.amount}";
                    paragraph.Range.Text += line;
                }

                paragraph.Range.Text += "\n\n\n\n";
                paragraph.Range.Text += "\t\t\t\t\t*** E Purchases ***";
                paragraph.Range.Text += "\n\n";

                foreach (var purchase in purchases)
                {
                    string line = $"Item: {purchase.name}\t|\t Price: {purchase.price}\t|Date: {purchase.purchaseDate}";
                    paragraph.Range.Text += line;
                }


                
                string filepath = @"C:\Users\User\Desktop/MyWordDocument.docx";
                documnet.SaveAs2(filepath);
                documnet.Close();

                return filepath;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                wordApp.Quit();  
            }

            return "";
        }



    }
}
