using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lopushok.ModelData;

namespace Lopushok
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        Model1 model = new Model1();

        private void Add_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = model.ProductType.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxArticle.Text) ||
                String.IsNullOrWhiteSpace(textBoxName.Text) ||
                String.IsNullOrWhiteSpace(textBoxCost.Text) ||
                String.IsNullOrWhiteSpace(comboBoxType.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            Product product = new Product();
            product.ID = 0;
            product.ArticleNumber = textBoxArticle.Text;
            product.Title = textBoxName.Text;
            product.Description = textBoxDescription.Text;
            product.ProductTypeID = comboBoxType.SelectedIndex;
            product.MinCostForAgent = 0;
            try
            {
                model.Product.Add(product);
                model.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Данные добавлены!");
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите фото продукции";
            ofd.Filter = "Файл JPG, PNG|*.jpg;*.png|Все файлы (*.*)|*.*";
            if(ofd.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(ofd.FileName);
        }
    }
}
