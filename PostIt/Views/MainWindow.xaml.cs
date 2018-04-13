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
using PostItProject.Models;
using PostItProject.ViewModels;
using PostItProject.ViewModels.UserControls;

namespace PostItProject.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            InitializeComponent();
        }



        private void HandleEsc(object sender, KeyEventArgs e)//TODO move to canvas
        {
            if (e.Key == Key.Escape)
                Keyboard.ClearFocus();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((ViewModelMainWindow) DataContext).AddItem();
           
        }
    }
}
