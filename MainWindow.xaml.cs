using System.Collections.ObjectModel;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalFinanceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Transaction> Transactions { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Transactions = new ObservableCollection<Transaction>();
            DataContext = this;
        }

        private void AddTransaction(string description, decimal amount, DateTime date, string type)
        {
            var transaction = new Transaction
            {
                Description = description,
                Amount = amount,
                Date = date,
                Type = type
            };
            Transactions.Add(transaction);
        }

        private void OnAddTransaction(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionInput.Text) ||
                string.IsNullOrWhiteSpace(AmountInput.Text) ||
                TypeInput.SelectedItem == null ||
                DateInput.SelectedDate == null)
            {
                MessageBox.Show("Preencha todos os campos corretamente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(AmountInput.Text, out decimal amount))
            {
                MessageBox.Show("Insira um valor numérico válido!", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string description = DescriptionInput.Text;
            DateTime date = DateInput.SelectedDate.Value;
            string type = ((ComboBoxItem)TypeInput.SelectedItem).Content.ToString();

            AddTransaction(description, amount, date, type);

            // Limpar os campos
            DescriptionInput.Clear();
            AmountInput.Clear();
            TypeInput.SelectedIndex = -1;
            DateInput.SelectedDate = null;
        }
    }
}
