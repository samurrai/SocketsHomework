using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace SocketsHomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetInfoButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Некорректные данные");
                return;
            }
            if (comboBox.SelectedIndex == 0)
            {
                try
                {
                    var ipAddress = Dns.GetHostByName(textBox.Text);
                    foreach (var address in ipAddress.AddressList)
                    {
                        listBox.Items.Add(address.ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (IPAddress.TryParse(textBox.Text, out IPAddress address))
                {
                    try
                    {
                        socket.Connect(address, 80);
                        if (socket.Connected)
                        {
                            MessageBox.Show("Подключение прошло успешно");
                        }
                        else
                        {
                            MessageBox.Show("Не удалось подключиться");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Некорректный IP");
                }
            }
        }
    }
}
