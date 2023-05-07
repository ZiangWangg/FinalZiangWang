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

namespace FinalZiangWang
{
    /// <summary>
    /// Interaction logic for AddNewProduct.xaml
    /// </summary>
    public partial class AddNewProduct : Window
    {
        public AddNewProduct()
        {
            InitializeComponent();
            LoadCombobox();
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnaddProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var ca = new List<Product>();
                string Category = comboCat.SelectedItem.ToString();
                var category = (from c in context.Categories
                                where c.CategoryName == Category
                                select c.CategoryID).ToList();

               

                Product product = new Product();
                product.ProductName = txtProductName.Text;
                foreach (var categoryId in category)
                {
                    product.CategoryID = categoryId;
                }
                product.UnitPrice = int.Parse(txtPrice.Text);
                context.Products.Add(product);
                context.SaveChanges();

                MessageBox.Show("Add Successfully");
            }
        }
    }
}
