using MyBindingSimpleClassCollections;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MyBindingDataContext
{
    public partial class MainWindow : Window
    {
        private Person _currentPerson;
        private ObservableCollection<Person> _people;

        public MainWindow()
        {
            InitializeComponent();
            _currentPerson = new Person();
            _people = new ObservableCollection<Person>();
            SetupBindings();
        }

        private void SetupBindings()
        {
            // Привязка TextBox'ов к текущему человеку для ввода новых данных
            Binding firstNameBinding = new Binding("FirstName");
            firstNameBinding.Source = _currentPerson;
            firstNameBinding.Mode = BindingMode.TwoWay;
            firstNameBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtFirstName.SetBinding(TextBox.TextProperty, firstNameBinding);

            Binding lastNameBinding = new Binding("LastName");
            lastNameBinding.Source = _currentPerson;
            lastNameBinding.Mode = BindingMode.TwoWay;
            lastNameBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtLastName.SetBinding(TextBox.TextProperty, lastNameBinding);

            // Привязка ListBox к коллекции
            lstPeople.ItemsSource = _people;
        }

        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_currentPerson.FirstName) &&
            !string.IsNullOrWhiteSpace(_currentPerson.LastName))
            {
                var newPerson = new Person
                {
                    FirstName = _currentPerson.FirstName,
                    LastName = _currentPerson.LastName
                };

                _people.Add(newPerson);
                ClearPersonFields();
                ClearSelectedPersonFields();
                txtFirstName.Focus();
                lstPeople.SelectedItem = null;

                MessageBox.Show($"Человек '{newPerson.FullName}' добавлен в коллекцию!",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Заполните имя и фамилию!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRemovePerson_Click(object sender, RoutedEventArgs e)
        {
            if (lstPeople.SelectedItem is Person selectedPerson)
            {
                var result = MessageBox.Show($"Удалить '{selectedPerson.FullName}' из коллекции?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _people.Remove(selectedPerson);
                    ClearSelectedPersonFields();
                }
            }
            else
            {
                MessageBox.Show("Выберите человека для удаления!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void lstPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPeople.SelectedItem is Person selectedPerson)
            {
                txtSelectedFirstName.Text = selectedPerson.FirstName;
                txtSelectedLastName.Text = selectedPerson.LastName;
                txtSelectedFullName.Text = selectedPerson.FullName;
            }
            else
            {
                ClearSelectedPersonFields();
            }
        }

        private void lstPeople_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstPeople.SelectedItem is Person selectedPerson)
            {
                _currentPerson.FirstName = selectedPerson.FirstName;
                _currentPerson.LastName = selectedPerson.LastName;
                txtFirstName.Focus();
                txtFirstName.SelectAll();
            }
        }

        private void ClearSelectedPersonFields()
        {
            txtSelectedFirstName.Text = string.Empty;
            txtSelectedLastName.Text = string.Empty;
            txtSelectedFullName.Text = string.Empty;
            lstPeople.SelectedItem = null;
        }

        private void ClearPersonFields()
        {
            _currentPerson.FirstName = string.Empty;
            _currentPerson.LastName = string.Empty;
        }

        private void ClearSelectedPersonFields_Click(object sender, RoutedEventArgs e)
        {
            ClearSelectedPersonFields();
        }

        private void ClearPersonFields_Click(object sender, RoutedEventArgs e)
        {
            ClearPersonFields();
        }
    }
}