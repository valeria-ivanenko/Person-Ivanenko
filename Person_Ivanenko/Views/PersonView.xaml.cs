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
using Desktop.Person_Ivanenko.ViewModels;

namespace Desktop.Person_Ivanenko.Views
{

    public partial class PersonView : UserControl
    {
        private PersonViewModel _viewModel;
        public PersonView()
        {
            InitializeComponent();
            DataContext = _viewModel = new PersonViewModel();
        }

        private void TboxesChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
           if (string.IsNullOrWhiteSpace(TbFirstName.Text) || string.IsNullOrWhiteSpace(TbLastName.Text)
                || string.IsNullOrWhiteSpace(TbEmail.Text))
            {
                BProceed.IsEnabled = false;
            } else
            {
                BProceed.IsEnabled = true;
            }
        }

    }
}
