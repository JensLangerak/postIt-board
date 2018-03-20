using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PostIt.ViewModels.Commands;

namespace PostIt.ViewModels
{
    public class ViewModelMainWindow : ViewModelBase
    {
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

    }
}
