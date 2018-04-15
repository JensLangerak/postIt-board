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
        private ViewModelBoard _board;

        public ViewModelBoard Board
        {
            get => _board;
            set => SetProperty(ref _board, value);
        }

        public ViewModelMainWindow()
        {
            _board = new ViewModelBoard(new Board());
        }
    }
}
