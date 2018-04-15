using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ViewModelBoard(Board pModel)
        {
            _model = pModel;
            CreateNewPostItCommand = new RelayCommand(CreateNewPostIt);
            _model.PostIts.CollectionChanged += PostIts_CollectionChanged;
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
            _postItsViewModels.Clear();
            foreach (var p in Model.PostIts)
            {
                AddPostIt(p);
            }
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
                _postItsViewModels.Remove(_postItsViewModels.Single(x => x.Model == p));
            }
        }

        public Board Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public ObservableCollection<ViewModelPostIt> PostItsViewModels
        {
            get => _postItsViewModels;
            set => SetProperty(ref _postItsViewModels, value);
        }

        public RelayCommand CreateNewPostItCommand { get; set; }

        private void CreateNewPostIt(object pParameter)
        {
            _model.PostIts.Add(new PostIt());
        }

    }
}
