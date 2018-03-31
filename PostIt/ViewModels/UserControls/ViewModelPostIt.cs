using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostItProject.Models;

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
    }
}
