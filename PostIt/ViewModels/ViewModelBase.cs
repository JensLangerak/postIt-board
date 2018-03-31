using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PostItProject.ViewModels
{
    /// <summary>
    /// Taken from https://stackoverflow.com/questions/36149863/how-to-write-viewmodelbase-in-mvvm-wpf
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
}
