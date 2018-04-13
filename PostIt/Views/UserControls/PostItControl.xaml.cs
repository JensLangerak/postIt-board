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
using PostItProject.ViewModels.UserControls;
using Xceed.Wpf.Toolkit;

namespace PostItProject.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PostItControl.xaml
    /// </summary>
    public partial class PostItControl : UserControl
    {
        public PostItControl()
        {
            InitializeComponent();
        }

        private void test_Click(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            ; // throw new NotImplementedException();
    //        ((ViewModelPostIt)this.DataContext).Model.Color = ((ColorPicker)sender).SelectedColor;
        }
    }
}
