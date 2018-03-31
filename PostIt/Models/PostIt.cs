using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PostItProject.Models
{
    public class PostIt : ModelBase
    {
        private string _text;
        private Color _color;
        private int _height;
        private int _width;
        private int _posY;
        private int _posX;

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

        public int Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public int Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public int PosY
        {
            get => _posY;
            set => SetProperty(ref _posY, value);
        }

        public int PosX
        {
            get => _posX;
            set => SetProperty(ref _posX, value);
        }

        public PostIt() { }

    }
}
