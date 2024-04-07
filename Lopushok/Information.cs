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
    public partial class Information : Form
    {
        private Product _product;

        public Information(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadData();
        }

        private void LoadData()
        {
            textBoxArticle.Text = _product.ArticleNumber.ToString();
            textBoxName.Text = _product.Title;
            textBoxType.Text = _product.ProductType.Title;
            textBoxCost.Text = _product.MinCostForAgent.ToString();
            textBoxDescription.Text = _product.Description;

            try
            {
                pictureBox1.Image = Image.FromFile(@"products\" + _product.Image);
            }
            catch
            {
                pictureBox1.Image = Lopushok.Properties.Resources.picture;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
