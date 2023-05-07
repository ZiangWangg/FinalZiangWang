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

namespace FinalZiangWang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddNewProduct();
            window.Owner = this;
            window.ShowDialog();
        }

        private void btnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var Product = context.Products.ToList();

                grdDisplay.ItemsSource = Product;
            }
        }

        private void LoadCombobox()
        {
            using (var context = new NorthwindEntities())
            {
                var Categories = (from c in context.Categories
                                  select c.CategoryName).ToList();   


               foreach (var category in Categories)
                {
                    comboCat.Items.Add(category);
                }
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = new List<Product>();
            using (var context = new NorthwindEntities())
            {
                if (comboCat.SelectedItem != null)
                {
                    string Category = comboCat.SelectedItem.ToString();
                    var category = (from c in context.Categories
                                    where c.CategoryName == Category
                                    select c.CategoryID).ToList();

                    foreach (var categoryId in category)
                    {
                        product = context.Products.Where(p => p.CategoryID == categoryId).ToList();
                    }



                    grdDisplay.ItemsSource = product;
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var Product = context.Products.Where(p => p.ProductName.Contains(txtPname.Text)).ToList();

                grdDisplay.ItemsSource = Product;
            }
        }

        private void btnCleandata_Click(object sender, RoutedEventArgs e)
        {
            comboCat.SelectedItem = null;
            txtPname.Text = "";
            grdDisplay.ItemsSource = "";
        }
    }
}
