using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace SQLite_Sandpit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BTN_TEST_Click(object sender, RoutedEventArgs e)
        {
            ///////////////////////////////////////////
            //Initial communications test with SQLite//
            ///////////////////////////////////////////
            
            //Version Check
                //Client Version
                int CLIENT_VERS = 0;

                //Open Connection
                SQLiteConnection cnn = new SQLiteConnection("Data Source=C:\\Users\\rossm\\source\\repos\\SQLite Sandpit\\SQLiteSandpit.db");
                cnn.Open();

                //Get data
                SQLiteCommand cmd = new SQLiteCommand("SELECT SERV_VERS FROM [T_SERV_VERS]");
                cmd.Connection = cnn;
                SQLiteDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                    //Check onlu one row has been returned
                    if (dt.Rows.Count == 0)
                        {
                            //No rows returned - warn and exit
                            _ = MessageBox.Show("Error checking version compatability (no data) \n \n Application will close to protect integrity", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            System.Windows.Application.Current.Shutdown();
                        }
                    else if (dt.Rows.Count > 1)
                        {
                            //More than 1 row returned - warn and exit
                            _ = MessageBox.Show("Error checking version compatability (data >1) \n \n Application will close to protect integrity", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            System.Windows.Application.Current.Shutdown();
                        }
                    else
                    {
                        //Only 1 row returned (OK)
                        if (Convert.ToInt32(dt.Rows[0]["SERV_VERS"]) == CLIENT_VERS)
                            {
                                //Version match (OK)
                                _ = MessageBox.Show("Welcome to SQLite Sandpit!", "Hello!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        else
                            {
                            //Version do not match - warn and exit
                                _ = MessageBox.Show("This version of the application is incompatible with the server!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                System.Windows.Application.Current.Shutdown();
                            }
                    }

        }
    }
}
