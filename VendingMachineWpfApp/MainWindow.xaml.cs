using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VendingMachineWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CashManager _cashManager;
        private VendingLogic _vendingLogic;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _cashManager = CashManager.RestoreState();
            _vendingLogic = VendingLogic.RestoreState();

            Refresh();

        }

        private void Refresh()
        {
            _textBlockCredit.Text = _vendingLogic.Credit.ToString();

            _productsPanel.Children.Clear();

            int productListCount = _vendingLogic.ProductList.Count;
            for (int i = 0; i < productListCount; i++)
            {
                Product currentProduct = _vendingLogic.ProductList[i];
                if (currentProduct.Quantity > 0)
                {
                    var button = new Button
                    {
                        Width = 200,
                        Height = 20,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(5),
                        Content = string.Format("{0} - {1} {2} RUB, {3} left", i + 1, currentProduct.Name, currentProduct.Price, currentProduct.Quantity),
                        Tag = currentProduct,
                        IsEnabled = currentProduct.Quantity > 0
                    };
                    _productsPanel.Children.Add(button);

                    button.Click += _button_Click;
                }
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            _cashManager.SaveState();
            _vendingLogic.SaveState();
        }

        private void _button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
                return;

            Product selectedProduct = button.Tag as Product;

            if (selectedProduct != null)
            {
                if (selectedProduct.Price > _vendingLogic.Credit)
                {
                    System.Diagnostics.Debug.WriteLine("Your credit isn't enough to buy {0}", selectedProduct.Name);
                }
                else
                {
                    _vendingLogic.ProductSelected(selectedProduct);

                }

            }
            else
            {
                if (button == _buttonChange)
                {
                    var coinList = _cashManager.GiveChange(_vendingLogic.Credit);
                    var change = 0;
                    for (int m = 0; m < coinList.Length; m++)
                    {
                        change = change + coinList[m];
                        System.Diagnostics.Debug.WriteLine("Coin {0} RUB", coinList[m]);
                    }

                    _vendingLogic.Credit = _vendingLogic.Credit - change;
                }
                else if (button == _button1)
                {
                    _cashManager.CoinAccepted(1);
                    _vendingLogic.CoinAccepted(1);
                }
                else if (button == _button2)
                {
                    _cashManager.CoinAccepted(2);
                    _vendingLogic.CoinAccepted(2);
                }
                else if (button == _button5)
                {
                    _cashManager.CoinAccepted(5);
                    _vendingLogic.CoinAccepted(5);
                }
                else if (button == _button10)
                {
                    _cashManager.CoinAccepted(10);
                    _vendingLogic.CoinAccepted(10);
                }
                else if (button == _button50)
                {
                    _cashManager.BillAccepted(50);
                    _vendingLogic.BillAccepted(50);
                }
                else if (button == _button100)
                {
                    _cashManager.BillAccepted(100);
                    _vendingLogic.BillAccepted(100);
                }
                else if (button == _button500)
                {
                    _cashManager.BillAccepted(500);
                    _vendingLogic.BillAccepted(500);
                }
                else if (button == _button1000)
                {
                    _cashManager.BillAccepted(1000);
                    _vendingLogic.BillAccepted(1000);
                }
            }

            Refresh();
        }
    }
}
