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
using Lopushok.UserElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Lopushok
{
    public partial class MainForm : Form
    {
        private enum SwipeType
        {
            Left, Right
        }
        private Model1 model = new Model1();
        private List<Product> products = new List<Product>();
        private int SwipeID;

        private void Loadproducts()
        {
            products.Clear();
            SwipeID = 0;
            products = model.Product.ToList();
        }

        private void Sort()
        {
            if (checkBoxMax.Checked == false)
            {
                if (comboBoxFiltr.SelectedIndex == 0)
                    products = products.OrderByDescending(x => x.Title).ToList();
                else if (comboBoxFiltr.SelectedIndex == 1)
                    products = products.OrderByDescending(x => x.ProductType.Title).ToList();
                else if (comboBoxFiltr.SelectedIndex == 2)
                    products = products.OrderByDescending(x => x.MinCostForAgent).ToList();
            }
            else
            {
                if (comboBoxFiltr.SelectedIndex == 0)
                    products = products.OrderBy(x => x.Title).ToList();
                else if (comboBoxFiltr.SelectedIndex == 1)
                    products = products.OrderBy(x => x.ProductType.Title).ToList();
                else if (comboBoxFiltr.SelectedIndex == 2)
                    products = products.OrderBy(x => x.MinCostForAgent).ToList();
            }           
            CreateTile();
        }

        private void SetTextlabel()
        {
            if (products.Count != 0)
            {
                labelCount.Text = products.Count >= 6 ?
                    $"с {SwipeID + 1} по {SwipeID + 6} из {products.Count} товаров" :
                    $"с 1 по {products.Count} товаров";
            }
            else
                labelCount.Text = $"с 0 из {products.Count} товаров";
        }

        private void CreateTile()
        {
            FLPTile.Controls.Clear();
            SetTextlabel();
            for (int i = 0; i < 6; i++)
            {
                if (products.Count > i)
                {
                    int count = i + SwipeID;
                    UserTile tile = new UserTile(products[count]);
                    FLPTile.Controls.Add(tile);
                }
            }
        }

        private void Search()
        {
            products.Clear();
            SwipeID = 0;
            products = model.Product.Where(
                x => x.Title.Contains(textBoxSearch.Text)).ToList();
            labelNothing.Visible = products.Count == 0 ? true : false;
            CreateTile();
        }

        private void Swipe(SwipeType swipeType)
        {
            if (swipeType == SwipeType.Left && SwipeID != 0)
            {
                SwipeID--;
                CreateTile();
            }
            if (swipeType == SwipeType.Right && SwipeID + 5 < products.Count - 1)
            {
                SwipeID++;
                CreateTile();
            }
        }


        public MainForm()
        {
            InitializeComponent();
            Loadproducts();
            CreateTile();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            Swipe(SwipeType.Left);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            Swipe(SwipeType.Right);
        }

        private void buttonLeft2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
                Swipe(SwipeType.Left);
        }

        private void buttonRight2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
                Swipe(SwipeType.Right);
        }

        private void comboBoxFiltr_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sort();
        }

        private void checkBoxMax_CheckedChanged(object sender, EventArgs e)
        {
            Sort();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.ShowDialog();
        }
    }
}
