﻿using System;
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
        private double _height;
        private double _width;
        private double _posY;
        private double _posX;
        private int _zIndex;
        private double _rotation;

        public int ZIndex
        {
            get => _zIndex;
            set => SetProperty(ref _zIndex, value);
        }

        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }

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

        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public double PosY
        {
            get => _posY;
            set => SetProperty(ref _posY, value);
        }

        public double PosX
        {
            get => _posX;
            set => SetProperty(ref _posX, value);
        }

        public PostIt()
        {
            _color = Colors.Gold;
            _width = 180;
            _height = 100;
        }

    }
}
