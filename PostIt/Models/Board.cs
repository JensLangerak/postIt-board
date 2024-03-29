﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostItProject.Models;
using PostItProject.ViewModels.UserControls;

namespace PostItProject.Models 
{
    public class Board : ModelBase
    {

        private ObservableCollection<PostIt> _postIts = new ObservableCollection<PostIt>();
        private string _filePath;

        public ObservableCollection<PostIt> PostIts
        {
            get => _postIts;
            set => SetProperty(ref _postIts, value);
        }

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }
    }
}
