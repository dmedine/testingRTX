using System.Windows.Controls;
using testingRTX.viewModel;

namespace testingRTX.view
{
    /// <summary>
    /// Interaction logic for SomeUC.xaml
    /// </summary>
    public partial class SomeUC : UserControl
    {
        public SomeUC()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
