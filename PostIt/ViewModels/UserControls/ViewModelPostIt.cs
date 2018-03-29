using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostIt.ViewModels.UserControls
{
    public class ViewModelPostIt : ViewModelBase
    {
        private Models.PostIt _model;

        public Models.PostIt Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }
    }
}
