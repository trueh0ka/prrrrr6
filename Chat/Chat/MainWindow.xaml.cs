using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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

namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Socket socket;
        public MainWindow()
        {
            InitializeComponent();

            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("26.188.38.104", 8888);
            ReciveMesg();
        }

        private async Task ReciveMesg()
        {
            while (true)
            {
                List<byte> bts = new List<byte>();
                await socket.ReceiveAsync(bts.ToArray(), SocketFlags.None);
                string mesg  = Encoding.UTF8.GetString(bts.ToArray());
                ListBox1.Items.Add(mesg);

            }
        }

        private async Task _Sends(string msg)
        {
            byte[] bts = Encoding.UTF8.GetBytes(msg);
            await socket.SendAsync(bts, SocketFlags.None);
        }

        private void Sends(object sender, RoutedEventArgs e)
        {
            _Sends(Box.Text);
        }

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _Sends(Box.Text);
                Box.Text = "";
            }
        }

    }
}
