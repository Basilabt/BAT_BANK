using BusinessAccessLayer.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BAT_BANK.Controllers
{
    public class DocumentsController : Controller
    {
        
            [HttpGet]
            public IActionResult MonthlySummary()
            {               
                string filepath = clsMSWord.buildMSWordMonthlySummaryDocument();
               
                if (System.IO.File.Exists(filepath))
                {
                    
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                  
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "MonthlySummary.docx");
                }
                else
                {
                    return NotFound("The document could not be found.");
                }
            }
        
    }
}
