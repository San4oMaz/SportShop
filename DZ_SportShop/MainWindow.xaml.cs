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

namespace DZ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDataConnect dataConnect;      
        
        
        // Доробити: Винести окремий метод для виведення даних listview
        // Зробити виведення з output parametr.


        
        public MainWindow()
        {
            InitializeComponent();
            dataConnect = new MyDataConnect();
        }

        private void OpenDb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataConnect.GetSqlConnection.Open();
                MessageBox.Show("Db Opened.");

                InitTypesComboBox();
                InitManagersCombobox();
                GetAllBuyers();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitManagersCombobox()
        {
            var allManagers = dataConnect.GetAllManagers();
            foreach (var item in allManagers)
            {
                comboBoxAllManagers.Items.Add(item);
            }
        }

        private void CloseDb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataConnect.GetSqlConnection.Close();
                MessageBox.Show("Db Closed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StateConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(dataConnect.GetSqlConnection.State.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AllGoodsInfo_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
            dataConnect.InitCommand("AllGoodsInfo", System.Data.CommandType.StoredProcedure);

            using(var reader = dataConnect.GetSqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    listView.Items.Add(reader[0] + ", " + reader[1] + ", " + reader[2] + ", " + reader[3]);
                }
            }
        }

        void InitTypesComboBox()
        {
            var allTypesG = dataConnect.GetAllTypeGoods();
            foreach(var item in allTypesG)
            {
                comboBoxAllType.Items.Add(item);
            }
        }

        private void MaxCountGoods_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
            dataConnect.InitCommand("sp_MaxCountGoods", System.Data.CommandType.StoredProcedure);
            using(var reader = dataConnect.GetSqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    listView.Items.Add(reader[0] + " " + reader[1]);
                }
            }
        }

        private void MinCountGoods_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
            dataConnect.InitCommand("sp_MinCountGoods", System.Data.CommandType.StoredProcedure);
            using (var reader = dataConnect.GetSqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    listView.Items.Add(reader[0] + " " + reader[1]);
                }
            }
        }

        private void MinPriceGoods_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
            dataConnect.InitCommand("MinStartPriceGoods", System.Data.CommandType.StoredProcedure);
            using(var reader = dataConnect.GetSqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    listView.Items.Add(reader[0] + " " + reader[1]);
                }
            }
        }

        private void MaxPriceGoods_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
            dataConnect.InitCommand("MaxStartPriceGoods", System.Data.CommandType.StoredProcedure);
            using (var reader = dataConnect.GetSqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    listView.Items.Add(reader[0] + " " + reader[1]);
                }
            }
        }

        private void ShowSelectGoods_Click(object sender, RoutedEventArgs e)
        {            
            if (comboBoxAllType.SelectedItem.ToString() != null)
            {
                listView.Items.Clear();
                dataConnect.InitCommand("GetTypeGoods", System.Data.CommandType.StoredProcedure);
                dataConnect.GetSqlCommand.Parameters.Add("@typName", System.Data.SqlDbType.NVarChar).Value = comboBoxAllType.SelectedItem.ToString();
                using (var res = dataConnect.GetSqlCommand.ExecuteReader())
                {
                    while (res.Read())
                    {
                        listView.Items.Add(res[0] + " " + res[1] + " " + res[2] + " " + res[3]);
                    }
                }
            }         
        }

        private void ShowManagerGoods_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAllManagers.SelectedItem.ToString() != null)
            {
                listView.Items.Clear();
                dataConnect.InitCommand("GetManagerGoods", System.Data.CommandType.StoredProcedure);
                dataConnect.GetSqlCommand.Parameters.Add("@manName", System.Data.SqlDbType.NVarChar).Value = comboBoxAllManagers.SelectedItem.ToString();
                using (var res = dataConnect.GetSqlCommand.ExecuteReader())
                {
                    while (res.Read())
                    {
                        listView.Items.Add(res[0] + " " + res[1] + " " + res[2] + " " + res[3]);
                    }
                }
            }            
        }

        void GetAllBuyers()
        {
            dataConnect.InitCommand(@"Select Distinct BayerName from Sales Order by BayerName", System.Data.CommandType.Text);
            using (var reader = dataConnect.GetSqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    comboBoxAllBuyer.Items.Add(reader[0].ToString());
                }
            }
            
        }

        private void ShowBuyerGoods_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listView.Items.Clear();
                dataConnect.InitCommand("GetBayerGoods", System.Data.CommandType.StoredProcedure);
                dataConnect.GetSqlCommand.Parameters.Add("@buyName", System.Data.SqlDbType.NVarChar).Value = comboBoxAllBuyer.SelectedItem.ToString();
                using (var res = dataConnect.GetSqlCommand.ExecuteReader())
                {
                    while (res.Read())
                    {
                        listView.Items.Add(res[0] + " " + res[1] + " " + res[2] + " " + res[3]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LastBuyDate_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                listView.Items.Clear();
                dataConnect.InitCommand("LastBuyGood", System.Data.CommandType.StoredProcedure);
                using (var res = dataConnect.GetSqlCommand.ExecuteReader())
                {
                    while (res.Read())
                    {
                        listView.Items.Add(res[0] + " " + res[1] + " " + res[2] + " " + res[3] + " " + res[4] + " " + res[5]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AvgGoodsInTypes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listView.Items.Clear();
                dataConnect.InitCommand("AvgGoodsInTypes", System.Data.CommandType.StoredProcedure);
                using (var res = dataConnect.GetSqlCommand.ExecuteReader())
                {
                    while (res.Read())
                    {
                        listView.Items.Add(res[0] + " Avg Count: " + res[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CountManagerSaleGoods_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAllManagers.SelectedItem != null)
            {
                dataConnect.InitCommand("OutputCountGoodsSaleManager", System.Data.CommandType.StoredProcedure);
                dataConnect.GetSqlCommand.Parameters.Add("@manName", System.Data.SqlDbType.NVarChar).Value = comboBoxAllManagers.SelectedItem.ToString();

                dataConnect.GetSqlCommand.Parameters.Add("@goodCount", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
                dataConnect.GetSqlCommand.ExecuteNonQuery();
                labelName.Content = "Count Goods Sale Manager: ";
                labelResult.Content = dataConnect.GetSqlCommand.Parameters["@goodCount"].Value;
            }
            else
                MessageBox.Show("Select a manager!");
        }
    }
}
