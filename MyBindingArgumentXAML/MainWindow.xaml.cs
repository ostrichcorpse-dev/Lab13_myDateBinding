using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MyBindingArgumentXAML
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupBindings();
        }

        private void SetupBindings()
        {
            // Одна двусторонняя привязка между InputTextBox и OutputSlider
            Binding binding = new Binding("Value");
            binding.Source = OutputSlider;
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged; 
            binding.StringFormat = "{0:P0}";
            InputTextBox.SetBinding(TextBox.TextProperty, binding);

            // Привязка SourceTextBox к TargetTextBlock
            Binding textBind = new Binding("Text");
            textBind.Source = SourceTextBox;
            textBind.Mode = BindingMode.OneWay;
            TargetTextBlock.SetBinding(Label.ContentProperty, textBind);
        }
    }
}