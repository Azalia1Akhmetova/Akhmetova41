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
using System.Windows.Shapes;

namespace Akhmetova41
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        List<OrderProduct> selectedOrderProducts = new List<OrderProduct>();
        List<Product> selectedProducts=new List<Product>();
        private Order currentOrder=new Order();
        private OrderProduct currentOrderProduct=new OrderProduct();
        public OrderWindow(List<OrderProduct> selectedOrderProducts, List<Product> selectedProducts, string FIO)
        {
            InitializeComponent();
            var currentPickups = Akhmetova41Entities.GetContext().PickUpPoint.ToList();
            PickupCombo.ItemsSource = currentPickups;

            ClientTB.Text = FIO;
            TBOrderID.Text = selectedOrderProducts.First().OrderID.ToString();

            OrderListView.ItemsSource = selectedProducts;
            foreach(Product p in selectedProducts)
            {
                p.ProductQuantityInStock = 1;
                foreach(OrderProduct q in selectedOrderProducts)
                {
                    if(p.ProductArticleNumber==q.ProductArticleNumber)
                    {
                        p.ProductQuantityInStock = q.Amount;
                    }
                }

            }
            this.selectedOrderProducts = selectedOrderProducts;
            this.selectedProducts = selectedProducts;
            OrderDP.Text = DateTime.Now.ToString();
            //
        }

        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(OrderListView.SelectedIndex>=0)
            {
                var prod = OrderListView.SelectedItem as Product;
                selectedProducts.Add(prod);

                var newOrderProd=new OrderProduct();
                newOrderProd.OrderID = newOredrID;

                newOrderProd.ProductArticleNumber=prod.ProductArticleNumber;
                newOrderProd.Amount = 1;

                var selOP = selectedOrderProducts.Where(p=>Equals(p.ProductArticleNumber, prod.ProductArticleNumber));
                if(selOP.Count()==0)
                {
                    selectedOrderProducts.Add(newOrderProd);
                }
                else
                {
                    foreach(OrderProduct p in selectedOrderProducts)
                    {
                        if (p.ProductArticleNumber == prod.ProductArticleNumber)
                            p.Amount++;
                    }
                }
                
                OrderListView.SelectedIndex = -1;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            var prod=(sender as Button).DataContext as Product;
            prod.ProductQuantityInStock++;
            var selectedOP=selectedOrderProducts.FirstOrDefault(p=>p.ProductArticleNumber==prod.ProductArticleNumber);
            int index = selectedOrderProducts.IndexOf(selectedOP);
            selectedOrderProducts[index].Amount++;
            //
            OrderListView.Items.Refresh();
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            var prod = (sender as Button).DataContext as Product;
            prod.ProductQuantityInStock--;
            var selectedOP = selectedOrderProducts.FirstOrDefault(p => p.ProductArticleNumber == prod.ProductArticleNumber);
            int index = selectedOrderProducts.IndexOf(selectedOP);
            selectedOrderProducts[index].Amount--;
            //
            OrderListView.Items.Refresh();
        }
    }
}
