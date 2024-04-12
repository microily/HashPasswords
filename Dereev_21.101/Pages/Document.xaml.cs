using Dereev_21._101.Models;
using HashPassword;
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


namespace Dereev_21._101.Pages
{
    /// <summary>
    /// Логика взаимодействия для Document.xaml
    /// </summary>
    public partial class Document : Page
    {
        public Document()
        {
            InitializeComponent();

            using (var context = new AtelieEntities())
            {
                var products = context.Product_list.ToList();

                if (products != null && products.Any())
                {
                    flowDoc.Blocks.Clear(); // Очищаем существующие блоки

                    Paragraph paragraph = new Paragraph();
                    foreach (var product in products)
                    {
                        // Создаем элементы разметки для каждого товара
                        TextBlock nameTextBlock = new TextBlock();
                        nameTextBlock.Inlines.Add(new Run($"Название: {product.Name}"));

                        TextBlock priceTextBlock = new TextBlock();
                        priceTextBlock.Inlines.Add(new Run($"Цена: {product.Price}"));

                        // Добавляем элементы разметки в параграф
                        paragraph.Inlines.Add(nameTextBlock);
                        paragraph.Inlines.Add(new LineBreak()); // Перенос на новую строку
                        paragraph.Inlines.Add(priceTextBlock);
                        paragraph.Inlines.Add(new LineBreak()); // Перенос на новую строку
                    }

                    // Добавляем параграф с товарами в FlowDocument
                    flowDoc.Blocks.Add(paragraph);
                }
            }
        }

        private void btnSaveDocument_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = flowDoc;
                pd.PrintDocument(idp.DocumentPaginator, Title);
            }
        }
    }
}