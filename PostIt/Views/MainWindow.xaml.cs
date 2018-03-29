﻿using System;
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
using PostIt.ViewModels.UserControls;

namespace PostIt.Views
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
            Models.PostIt p = new Models.PostIt();
            p.Color = Colors.Aqua;
            t.Model = p;
            Test.DataContext = t;
            p.Text = "Hello World!";
            Test.UpdateLayout();

        }
    }
}
