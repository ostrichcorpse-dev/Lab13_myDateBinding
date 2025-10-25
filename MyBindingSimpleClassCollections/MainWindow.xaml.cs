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

namespace MyBindingSimpleClassCollections
{
    public partial class MainWindow : Window
    {
        private Person _person;

        public MainWindow()
        {
            InitializeComponent();
            _person = new Person();
            SetupBindings();
        }

        private void SetupBindings()
        {
            // 1. Привязка TextBox Имя к свойству FirstName
            var firstNameBinding = new Binding("FirstName");
            firstNameBinding.Source = _person;
            firstNameBinding.Mode = BindingMode.TwoWay;
            firstNameBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtFirstName.SetBinding(TextBox.TextProperty, firstNameBinding);

            // 2. Привязка TextBox Фамилия к свойству LastName
            var lastNameBinding = new Binding("LastName");
            lastNameBinding.Source = _person;
            lastNameBinding.Mode = BindingMode.TwoWay;
            lastNameBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtLastName.SetBinding(TextBox.TextProperty, lastNameBinding);

            // 3. Привязка TextBlock к вычисляемому свойству FullName - FIXED
            var fullNameBinding = new Binding("FullName");
            fullNameBinding.Source = _person;
            fullNameBinding.Mode = BindingMode.OneWay; // CORRECTED - removed the unused line
            txtFullName.SetBinding(TextBlock.TextProperty, fullNameBinding);
        }
    }
}