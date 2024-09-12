using DotnetDocViewer.DocumentationParser;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DotnetDocViewer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<TreeViewItem> _DocumentationNodes;

        public List<TreeViewItem> DocumentationNodes
        {
            get => _DocumentationNodes;
            set
            {
                _DocumentationNodes = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            _DocumentationNodes = new List<TreeViewItem>();
        }

        public void UpdateSource()
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                CheckFileExists = true,
                Filter = "Dotnet generated docs|*.xml"
            };

            if (!fileDialog.ShowDialog() ?? false)
            {
                MessageBox.Show("File wasn't selected");
                return;
            }

            try
            {
                DocumentationNodes = DotnetDocumentation.FromXmlFile(fileDialog.FileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Failed to read documetation file.\n\n{0}", exc.ToString());
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
