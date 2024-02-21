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

namespace Akhmetova41
{
    /// <summary>
    /// Логика взаимодействия для Shoes.xaml
    /// </summary>
    public partial class Shoes : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;
        List<Shoes> CurrentPageList=new List<Shoes>();
        List<Shoes> TableList;
        public Shoes()
        {
            InitializeComponent();
            var currentShoes = Akhmetova41Entities.GetContext().Product.ToList();
            ShoesListView.ItemsSource = currentShoes;
            ComboType.SelectedIndex = 0;
            UpdateShoes();
        }
        private void UpdateShoes()
        {
            var currentShoes = Akhmetova41Entities.GetContext().Product.ToList();

            int initialcount = currentShoes.Count,
                remaincount;

            if(ComboType.SelectedIndex==0)
            {
                currentShoes = currentShoes.ToList();
            }
            if(ComboType.SelectedIndex == 1)
            {
                currentShoes = currentShoes.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount < 10)).ToList();
            }
            if(ComboType.SelectedIndex==2)
            {
                currentShoes = currentShoes.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15)).ToList();
            }
            if(ComboType.SelectedIndex==3)
            {
                currentShoes=currentShoes.Where(p=>(p.ProductDiscountAmount>=15)).ToList();
            }

            currentShoes = currentShoes.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if(RBittonUp.IsChecked.Value)
            {
                currentShoes=currentShoes.OrderBy(p => p.ProductCost).ToList();
            }
            if(RButtonDown.IsChecked.Value)
            {
                currentShoes=currentShoes.OrderByDescending(p => p.ProductCost).ToList();
            }

            remaincount = currentShoes.Count;

            Count1.Text = remaincount.ToString();
            Count2.Text=initialcount.ToString();

            ShoesListView.ItemsSource = currentShoes;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateShoes();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateShoes();
        }

        private void RBittonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateShoes();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateShoes();
        }
        

        
    }
}
