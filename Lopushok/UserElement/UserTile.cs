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


namespace Lopushok.UserElement
{
    public partial class UserTile : UserControl
    {
        private Product _product;
        private Model1 model = new Model1();

        public UserTile(Product product)
        {
            InitializeComponent();
            Fill(product);
        }

        public void Fill(Product product)
        {
            _product = product;
            labelType.Text = _product.ProductType.Title;
            labelName.Text = _product.Title;
            labelArticle.Text = _product.ArticleNumber;
            labelCost.Text = $"{_product.MinCostForAgent} руб.";
            labelMaterial.Text = $"Материалы: {_product.ProductMaterial}";
            try
            {
                pictureBox1.Image = Image.FromFile(@"products\" + _product.Image);
            }
            catch
            {
                pictureBox1.Image = Lopushok.Properties.Resources.picture;
            }
        }
        private void Delete()
        {
            DialogResult result = MessageBox.Show(
                $"Вы действительно хотите удалить продукцию с ID {_product.ID}",
                "Сообщение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    model.Product.Remove(
                        model.Product.First(x => x.ID == _product.ID));
                    model.SaveChanges();
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Clicking(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Information info = new Information(_product);
                info.ShowDialog();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Delete();
            }
        }

        private void Controls_Click(object sender, MouseEventArgs e)
        {
            Clicking(e);
        }
    }
}
