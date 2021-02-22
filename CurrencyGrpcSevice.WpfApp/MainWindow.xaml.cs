using System.Windows;

using Grpc.Net.Client;
using CurrencyMessages;
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

            // Allow spaces in the input string
            string sWithoutWhitespace = _regexWhitespace.Replace(textBoxCurrency.Text, "");

            // The given number has to be between 0 und 999 999 999,99
            if (!_regexDouble.IsMatch(sWithoutWhitespace))
            {
                MessageBox.Show("Currency has to be a double between 0 and 999 999 999,99 and with less than 99 cents.");
                return;
            }

            double value;
            if (!double.TryParse(sWithoutWhitespace, out value))
            {
                MessageBox.Show("Currency has to be a double.");
                return;
            }

            var convertedCurrency = await _client.ConvertDollarToStringAsync(new CurrencyNumber { Value = value });
            MessageBox.Show($"{convertedCurrency.Value}");
        }

        private static readonly Regex _regexWhitespace = new Regex(@"\s+");
        private static readonly Regex _regexDouble = new Regex(@"^[0-9]{1,9}([\,][0-9]{1,2})?$");
    }
}
