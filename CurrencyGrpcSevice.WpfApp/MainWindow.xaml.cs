using System.Windows;

using Grpc.Net.Client;
using CurrencyMessages;
using System.ComponentModel;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace CurrencyGrpcService.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
{
        private readonly GrpcChannel _channel;
        private readonly Currency.CurrencyClient _client;
        public MainWindow()
        {
            InitializeComponent();
            _channel = CreateGrpcChannel();
            _client = CreateConversionToStringClient();
        }

        private Currency.CurrencyClient CreateConversionToStringClient() => new Currency.CurrencyClient(_channel);

        private GrpcChannel CreateGrpcChannel() => GrpcChannel.ForAddress("https://localhost:5001");

        private async void ConvertToString_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCurrency.Text))
            {
                MessageBox.Show("Currency is required.");
                return;
            }

            double value;
            if (!double.TryParse(textBoxCurrency.Text, out value))
            {
                MessageBox.Show("Currency has to be a double.");
                return;
            }

            var convertedCurrency = await _client.ConvertDollarToStringAsync(new CurrencyNumber { Value = value });
            MessageBox.Show($"{convertedCurrency.Value}");
        }

        // Validate fields.
        private void textBoxCurrency_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            
            Regex decimalRegex = new Regex(@"^[0-9]{1,9}([\,][0-9]{1,2})?$");
            e.Cancel = !decimalRegex.IsMatch(txt.Text);
        }
    }
}
