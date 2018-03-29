using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PostIt.Models
{
    public class PostIt : ModelBase
    {
        private string _text;
        private Color _color;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public PostIt() { }

    }
}
