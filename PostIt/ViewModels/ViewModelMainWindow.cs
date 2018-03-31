using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using PostItProject.Models;
using PostItProject.ViewModels;
using PostItProject.ViewModels.Commands;
using PostItProject.ViewModels.UserControls;

namespace PostItProject.ViewModels
{
    public class ViewModelMainWindow : ViewModelBase
    {
        private ObservableCollection<ViewModelPostIt> _SomeEntityCollection = new ObservableCollection<ViewModelPostIt>();

        public ObservableCollection<ViewModelPostIt> SomeEntityCollection
        {
            get => _SomeEntityCollection;
            set => SetProperty(ref _SomeEntityCollection, value);
        }
        private string _testString = "Hello world!";

        public string TestString
        {
            get => _testString;
            set => SetProperty(ref _testString, value);
        }

        public RelayCommand UpdateTekstCommand { get; set; }

        public ViewModelMainWindow()
        {
            UpdateTekstCommand = new RelayCommand(TestCommand);

        }

        void TestCommand(object waarde)
        {
            TestString = (string) waarde;
            if (TestString == "test")
                MessageBox.Show("Test");
        }

        public void AddItem()
        {
            ViewModelPostIt t = new ViewModelPostIt();
            PostIt p = new PostIt();
            p.Color = Colors.Aqua;
            t.Model = p;
            p.Height = 100;
            p.Width = 170;
            p.PosX = 10;
            p.PosY = 50;
            p.Text = "Hello World!";
            SomeEntityCollection.Add(t);
        }
    }
}
