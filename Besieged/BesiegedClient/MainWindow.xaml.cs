using System.Collections.Generic;
using System.ServiceModel;
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

        private Framework.GameState game = new Framework.GameState();
        private TaskScheduler _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        public Framework.Server.Services.IMessageService _messageService;

        public MainWindow()
        {
            InitializeComponent();
            DrawGrid(10, 10);

            EndpointAddress endpointAddress = new EndpointAddress("http://localhost:31337/BesiegedServer/BesiegedMessage");
            DuplexChannelFactory<Framework.Server.Services.IMessageService> duplexChannelFactory = new DuplexChannelFactory<Framework.Server.Services.IMessageService>(game, new WSDualHttpBinding(), endpointAddress);
            _messageService = duplexChannelFactory.CreateChannel();

            //Task.Factory.StartNew(() => _messageService.Subscribe());
            Task.Factory.StartNew(() => _messageService.SendCommand("Shane"));
            //SendMessageToServer("Shane");
        }

        private void SendMessageToServer(string message)
        {
            Task.Factory.StartNew(() => _messageService.SendCommand(message));
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