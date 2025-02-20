using BusinessAccessLayer;

namespace BAT_BANK.Models
{
    public class clsProductsViewModel
    {
        public List<clsProduct> products = new List<clsProduct>();

        public clsProductsViewModel()
        {
            this.products = clsProduct.getAllProducts();
        }

    }
}
