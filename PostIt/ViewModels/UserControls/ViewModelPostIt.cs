using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            Model = pModel;
            DeleteCommand = new RelayCommand(DeletePostIt);
        }

        public PostIt Model
        {
            get => _model;
            set
            {
                if (_model == value) return;
                
                if (this.Model != null)
                {
                    this.Model.PropertyChanged -= ModelChanged;
                }
                SetProperty(ref _model, value);
                this.Model.PropertyChanged += ModelChanged;
            }
        }

        private void ModelChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.PosX):
                case nameof(Model.PosY):
                case nameof(Model.Width):
                case nameof(Model.Height):
                    OnPropertyChanged(nameof(MostRightCoord));
                    OnPropertyChanged(nameof(MostDownCoord));
                    break;
                default:
                    break;
            }
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

        public double MostRightCoord
        {
            get
            {
                double corner_x = Model.Width / 2;
                double corner_y = Model.Height / 2;
               
                double center_x = Model.PosX + corner_x;
                double angle = Model.Rotation * 2 * Math.PI / 360.0;

                double dx = corner_x * Math.Cos(angle) + corner_y * Math.Sin(angle);
                return center_x + Math.Abs(dx);
            }
        }

        public double MostLeftCoord
        {
            get
            {
                double corner_x = Model.Width / 2;
                double corner_y = Model.Height / 2;

                double center_x = Model.PosX + corner_x;
                double angle = Model.Rotation * 2 * Math.PI / 360.0;

                double dx = corner_x * Math.Cos(angle) + corner_y * Math.Sin(angle);
                return center_x - Math.Abs(dx);
            }
        }

        public double MostDownCoord
        {
            get
            {
                double corner_x = Model.Width / 2;
                double corner_y = Model.Height / 2;

                double center_y = Model.PosY + corner_y;
                double angle = Model.Rotation * 2 * Math.PI / 360.0;

                double dy = corner_x * Math.Sin(angle) + corner_y * Math.Cos(angle);
                return center_y + Math.Abs(dy);
            }
        }

        public double MostTopCoord
        {
            get
            {
                double corner_x = Model.Width / 2;
                double corner_y = Model.Height / 2;

                double center_y = Model.PosY + corner_y;
                double angle = Model.Rotation * 2 * Math.PI / 360.0;

                double dy = corner_x * Math.Sin(angle) + corner_y * Math.Cos(angle);
                return center_y - Math.Abs(dy);
            }
        }
    }
}
