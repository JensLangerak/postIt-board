using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PostItProject.Models;
using PostItProject.ViewModels.Commands;
using PostItProject.ViewModels.UserControls;

namespace PostItProject.ViewModels
{
    public class ViewModelBoard : ViewModelBase
    {
        private ObservableCollection<ViewModelPostIt> _postItsViewModels = new ObservableCollection<ViewModelPostIt>();
        private Board _model;
        private double _height;
        private double _width;
        private bool _possibleResizeInProgress = false;

        public ViewModelBoard(Board pModel)
        {
            Model = pModel;
            CreateNewPostItCommand = new RelayCommand(CreateNewPostIt);
            StartPossibleResizeCommand = new RelayCommand(StartPossibleResize);
            EndPossibleResizeCommand = new RelayCommand(EndPossibleResize);
        }

        private void PostIts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    PostItAdded(sender, e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    PostItRemoved(sender, e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    PostItReset(sender, e);
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                default:
                    throw new NotImplementedException();
            }
        }

        private void PostItReset(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var vm in _postItsViewModels)
            {
                vm.RemovePostIt -= RemovePostIt;
                vm.PropertyChanged -= PostItChanged;
            }
            _postItsViewModels.Clear();
            foreach (var p in Model.PostIts)
            {
                AddPostIt(p);
            }
        }

        private void PostItChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModelPostIt.MostRightCoord):
                case nameof(ViewModelPostIt.MostDownCoord):
                    UpdateBoardSize();
                    break;
                default:
                    break;
            }
        }

        private void UpdateBoardSize()
        {
            var newHeight =  PostItsViewModels.Select(x => x.MostDownCoord).DefaultIfEmpty(0).Max() + 100;
            var newWidth = PostItsViewModels.Select(x => x.MostRightCoord).DefaultIfEmpty(0).Max() + 100;

            if (!_possibleResizeInProgress || Height < newHeight)
                Height = newHeight;
            if (!_possibleResizeInProgress || Width < newWidth)
                Width = newWidth;
        }

        private void PostItAdded(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (PostIt p in e.NewItems)
            {
                AddPostIt(p);
            }
        }

        private void AddPostIt(PostIt pPostIt)
        {
            var vm = new ViewModelPostIt(pPostIt);
            vm.RemovePostIt += RemovePostIt;
            vm.PropertyChanged += PostItChanged;
            _postItsViewModels.Add(vm);
        }

        private void RemovePostIt(PostIt pPostIt)
        {
            _model.PostIts.Remove(pPostIt);
        }

        private void PostItRemoved(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (PostIt p in e.OldItems)
            {
                var vm = _postItsViewModels.Single(x => x.Model == p);
                vm.RemovePostIt -= RemovePostIt;
                vm.PropertyChanged -= PostItChanged;
                _postItsViewModels.Remove(vm);
            }
        }

        public Board Model
        {
            get => _model;
            set
            {
                if (_model == value) return;

                if (this.Model != null)
                {
                    _model.PostIts.CollectionChanged -= PostIts_CollectionChanged;
                    foreach (var vm in _postItsViewModels)
                    {
                        vm.RemovePostIt -= RemovePostIt;
                        vm.PropertyChanged -= PostItChanged;
                    }
                    _postItsViewModels.Clear();
                }

                SetProperty(ref _model, value);

                _model.PostIts.CollectionChanged += PostIts_CollectionChanged;
                foreach (var postIt in _model.PostIts)
                {
                    AddPostIt(postIt);
                }
            }
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

        public ObservableCollection<ViewModelPostIt> PostItsViewModels
        {
            get => _postItsViewModels;
            set => SetProperty(ref _postItsViewModels, value);
        }

        public RelayCommand CreateNewPostItCommand { get; set; }

        private void CreateNewPostIt(object pParameter)
        {
            var postIt = new PostIt();
            if (pParameter is IInputElement)
            {
                Point mousePos = Mouse.GetPosition((IInputElement)pParameter);
                postIt.PosX = mousePos.X;
                postIt.PosY = mousePos.Y;
            }

            _model.PostIts.Add(postIt);
        }

        public RelayCommand StartPossibleResizeCommand { get; set; }
        public RelayCommand EndPossibleResizeCommand { get; set; }

        private void StartPossibleResize(object pParameter)
        {
            _possibleResizeInProgress = true;
        }

        private void EndPossibleResize(object pParameter)
        {
            _possibleResizeInProgress = false;
            UpdateBoardSize();
        }

    }
}
