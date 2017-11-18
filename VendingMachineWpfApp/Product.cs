using System;

namespace VendingMachineWpfApp
{
    [Serializable]
    public class Product
    {
        private string _name;
        private int _price;
        private int _quantity;

        public Product(string name, int price, int quantity)
		{
            _name = name;
            _price= price;
            _quantity = quantity;
		}

        public int Quantity
        {
            get{ return _quantity; }
            set { _quantity = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}
