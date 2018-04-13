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


        public RelayCommand UpdateColorCommand { get; set; }

        public ViewModelPostIt()
        {
            UpdateColorCommand = new RelayCommand(UpdateColor);
        }


        protected void UpdateColor(object p)
        {
            Model.Color = Colors.Red;
        }
    }
}
