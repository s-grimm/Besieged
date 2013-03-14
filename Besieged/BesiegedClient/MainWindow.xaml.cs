using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BesiegedClient
{
    public class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SolidColorBrush _blackBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _greenBrush = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _blueBrush = new SolidColorBrush(Colors.Blue);
        private List<Rectangle> _rectangles = new List<Rectangle>();

        private app_code.Client _client = new app_code.Client();
        private string _clientIdentifier;
        private bool _isServerConnectionEstablished = false;
        private TaskScheduler _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private IBesiegedServer _besiegedServer;

        public MainWindow()
        {
            InitializeComponent();
            DrawGrid(10, 10);

            EndpointAddress endpointAddress = new EndpointAddress("http://localhost:31337/BesiegedServer/BesiegedMessage");
            DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(_client, new WSDualHttpBinding(), endpointAddress);
            _besiegedServer = duplexChannelFactory.CreateChannel();

            // Subscribe in a separate thread to preserve the UI thread
            Task.Factory.StartNew(() =>
            {
                CommandConnect commandConnect = new CommandConnect();
                _besiegedServer.SendCommand(commandConnect.ToXml());
            });

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command message = _client.MessageQueue.Take();
                    ProcessMessage(message);
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void ProcessMessage(Command command)
        {
            if (command is CommandConnectionSuccessful)
            {
                CommandConnectionSuccessful commandConnectionSuccessful = command as CommandConnectionSuccessful;
                _clientIdentifier = commandConnectionSuccessful.ClientId;
                _isServerConnectionEstablished = true;
                Task.Factory.StartNew(() =>
                {
                    MessageBox.Show("Connection successful!");
                }, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
            }
            else if (command is CommandChatMessage)
            {
                Task.Factory.StartNew(() =>
                {
                    CommandChatMessage chatMessageCommand = command as CommandChatMessage;
                   // listboxChatWindow.Items.Add(chatMessage.Contents);
                }, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
            }
        }

        private void SendMessageToServer(string message)
        {
            //Task.Factory.StartNew(() => _messageService.SendCommand(message));
        }

        public void DrawGrid(int rows, int columns)
        {
            Dimensions windowDimensions = new Dimensions()
            {
                Width = (int)cvsGameWindow.Width,
                Height = (int)cvsGameWindow.Height
            };

            //init rectangles
            for (int i = 0; i < windowDimensions.Width; i += 50)
            {
                for (int y = 0; y < windowDimensions.Height; y += 50)
                {
                    Rectangle rect = new Rectangle(); //create the rectangle
                    rect.StrokeThickness = 1;  //border to 1 stroke thick
                    rect.Stroke = _blackBrush; //border color to black
                    rect.Width = 50;
                    rect.Height = 50;
                    rect.Name = "box" + i.ToString();
                    Canvas.SetLeft(rect, i);
                    Canvas.SetTop(rect, y);
                    _rectangles.Add(rect);
                }
            }
            foreach (var rect in _rectangles)
            {
                cvsGameWindow.Children.Add(rect);
            }
        }

        private void txtMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMessage.Text == "Send a Message")
            {
                txtMessage.Clear();
            }
        }

        private void txtMessage_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtMessage.Text.Trim() == "")
            {
                txtMessage.Text = "Send a Message";
            }
        }
    }
}