using CSharpEgitimKampi501.Dto;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi501
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

         SqlConnection connection = new SqlConnection("Server =DESKTOP-7CKJMG6;initial Catalog = EgitimKampi501Db;integrated security = true");

        private async void btnList_Click(object sender, EventArgs e)
        {
            string query = "Select * From Tbl_Product ";
            var values = await connection.QueryAsync<ResultProductDto>(query);
            dataGridView1.DataSource = values;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "insert into Tbl_Product (ProductName, ProductStock, ProductPrice, ProductCategory) values" +
                "(@productName, @productStock, @productPrice, @productCategory)";
            var parameters = new DynamicParameters();
            parameters.Add("@productName",txtProductName.Text);
            parameters.Add("@productStock",txtStock.Text);
            parameters.Add("@productPrice",txtPrice.Text);
            parameters.Add("@productCategory",txtCategory.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Yeni Kitap Ekleme Başarılı.");
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "delete from Tbl_Product where ProductId = @productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId",txtProductId.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Kitap Silme İşlemi Başarılı");
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "update Tbl_Product set ProductName = @productName, ProductPrice = @productPrice, ProductStock = @productStock," +
                "ProductCategory = @productCategory where ProductId = @productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productName", txtProductName.Text);
            parameters.Add("@productPrice", txtPrice.Text);
            parameters.Add("@productStock", txtStock.Text);
            parameters.Add("@productCategory", txtCategory.Text);
            parameters.Add("@productId", txtProductId.Text);
            await connection.ExecuteAsync (query, parameters);
            MessageBox.Show("Kitap Güncelleme İşlemi Başarılı");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string query1 = "select count(*) from Tbl_Product";
            var productTotalCount = await connection.QueryFirstOrDefaultAsync<int>(query1);
            lblTotalProductCount.Text = productTotalCount.ToString();

            string query2 = "select ProductName from Tbl_Product where ProductPrice = (select Max(ProductPrice) from Tbl_Product)";
            var maxPriceProductName = await connection.QueryFirstOrDefaultAsync<string>(query2);
            lblMaxPriceProductName.Text = maxPriceProductName.ToString();

            string query3 = "select Count(distinct(ProductCategory)) from Tbl_Product";
            var distincCategoryCount = await connection.QueryFirstOrDefaultAsync<int>(query3);
            lblDistinctCategoryCount.Text = distincCategoryCount.ToString();
        }
    }
}
