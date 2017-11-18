using System;
using System.IO;
using System.Collections.Generic;

namespace VendingMachineWpfApp
{
    [Serializable]
    public class VendingLogic
    {
        private const string _fileName = "VendingLogic.bin"; 

        // Store credit and information about products here
        private int _credit;
        private List<Product> _productList = new List<Product>();

        public int Credit
        {
            get { return _credit; }
            set { _credit = value; }
        }

		public List<Product> ProductList
		{
            get { return _productList; }
            private set { _productList = value; }
		}

        public void LoadProductsForSale()
        {
			ProductList.Clear();
			ProductList.AddRange(new[] {
							new Product("Mineral water", 50, 10),
							new Product("Cola", 30, 10),
							new Product("Orange Juice", 40, 10),
							new Product("Snacks", 45, 10),
							new Product("Sandwich", 80, 10),
						});

        }

		public void BillAccepted(int billValue)
        {
            // Increase credit
            Credit = Credit + billValue;

        }

		public void CoinAccepted(int coinValue)
		{
			// Increase credit
			Credit = Credit + coinValue;
		}

        public void ProductSelected(Product product)
        {
            // Decrease stock and credit
            product.Quantity = product.Quantity - 1;
            Credit = Credit - product.Price;
        }

        // + Save-restore methods
        public void SaveState()
        {
            VendingMachineStateHandler.Save<VendingLogic>(_fileName, this);
        }

        public static VendingLogic RestoreState()
        {
            if (File.Exists(_fileName))
                return VendingMachineStateHandler.Restore<VendingLogic>(_fileName);

            VendingLogic vendingLogic = new VendingLogic();
            vendingLogic.LoadProductsForSale();
            return vendingLogic;

        }
    }
}
