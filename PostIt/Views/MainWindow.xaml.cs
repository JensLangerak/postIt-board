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

        private Point _startScrollMouseCanvasCoord;
        private void Sv_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startScrollMouseCanvasCoord = e.GetPosition(sv);
            _startScrollMouseCanvasCoord.X += sv.HorizontalOffset;
            _startScrollMouseCanvasCoord.Y += sv.VerticalOffset;

            sv.CaptureMouse();
        }

        private void Sv_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (sv.IsMouseCaptured)
            {
                sv.ScrollToHorizontalOffset(_startScrollMouseCanvasCoord.X - e.GetPosition(sv).X);
                sv.ScrollToVerticalOffset(_startScrollMouseCanvasCoord.Y - e.GetPosition(sv).Y);
            }
        }

        private void Sv_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sv.ReleaseMouseCapture();
        }
    }
}
