using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using PostItProject.Models;
using PostItProject.ViewModels.Commands;

namespace PostItProject.ViewModels.UserControls
{
    public class ViewModelPostIt : ViewModelBase
    {
        private PostIt _model;

        public delegate void RemoveEventHandler(PostIt pPostIt);
        public event RemoveEventHandler RemovePostIt;

        public ViewModelPostIt(PostIt pModel)
        {
            _model = pModel;
            DeleteCommand = new RelayCommand(DeletePostIt);
        }

        public PostIt Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        private Color _color;

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public RelayCommand DeleteCommand { get; set; }

        private void DeletePostIt(object pParameter)
        {
            RemovePostIt?.Invoke(_model);
        }
    }
}
