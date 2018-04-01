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
            InitializeComponent();
            ViewModelPostIt t = new ViewModelPostIt();
            PostIt p = new PostIt();
            p.Color = Colors.Aqua;
            t.Model = p;
            p.Height = 100;
            p.Width = 170;
            p.PosX = 10;
            p.PosY = 50;
            p.Text = "Hello World!";
            //Test.DataContext = t;
            this.UpdateLayout();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((ViewModelMainWindow) DataContext).AddItem();
           
        }
    }
}
