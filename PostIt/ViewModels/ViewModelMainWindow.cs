using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using PostItProject.Models;
using PostItProject.ViewModels;
using PostItProject.ViewModels.Commands;
using PostItProject.ViewModels.UserControls;
using PostItProject.Views.Recources.Languages;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace PostItProject.ViewModels
{
    public class ViewModelMainWindow : ViewModelBase
    {
        private ViewModelBoard _board;

        public ViewModelBoard Board
        {
            get => _board;
            set => SetProperty(ref _board, value);
        }

        public ViewModelMainWindow()
        {
            _board = new ViewModelBoard(new Board());
            ExitCommand = new RelayCommand(Exit);
            NewCommand = new RelayCommand(NewBoard);
            LoadCommand = new RelayCommand(LoadBoard);
            SaveCommand = new RelayCommand(SaveBoard);
            SaveAsCommand = new RelayCommand(SaveAsBoard);
        }

        public RelayCommand NewCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand SaveAsCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }

        private static void Exit(object p)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void NewBoard(object p)
        {
            Board = new ViewModelBoard(new Board());
        }

        private void LoadBoard(object p)
        {
            var serializer = new XmlSerializer(Board.Model.GetType());
            var dialog = new OpenFileDialog();
            ConfigureFileDiaglog(dialog);

            if (dialog.ShowDialog() != DialogResult.OK) return;

            var stream = dialog.OpenFile();
            var model = (Board) serializer.Deserialize(stream);

            Board = new ViewModelBoard(model);
            stream.Close();
            model.FilePath = dialog.FileName;
        }

        private void SaveBoard(object p)
        {
            if (string.IsNullOrEmpty(Board.Model.FilePath))
            {
                SaveAsBoard(p);
                return;
            }

            var serializer = new XmlSerializer(Board.Model.GetType());
            TextWriter writer = new StreamWriter(Board.Model.FilePath);

            serializer.Serialize(writer, Board.Model);
            writer.Close();
        }

        private void SaveAsBoard(object p)
        {
            var serializer = new XmlSerializer(Board.Model.GetType());
            var dialog = new SaveFileDialog();
            ConfigureFileDiaglog(dialog);
            if (!string.IsNullOrEmpty(Board.Model.FilePath))
            {
                var file = new FileInfo(Board.Model.FilePath);
                dialog.FileName = file.Name;
                dialog.InitialDirectory = file.DirectoryName;
            }

            if (dialog.ShowDialog() != DialogResult.OK) return;

            var stream = dialog.OpenFile();
            serializer.Serialize(stream, Board.Model);
            stream.Close();

            Board.Model.FilePath = dialog.FileName;
        }

        private static void ConfigureFileDiaglog(FileDialog pDialog)
        {
            pDialog.DefaultExt = ".pi";
            pDialog.Filter = "Post It files (*.pi)|*.pi|All files (*.*)|*.*";
            pDialog.RestoreDirectory = true;
        }
    }
}
