using processes_list.ViewModels;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

namespace processes_list.Views
{
    /// <summary>
    /// Interaction logic for ProcessesListView
    /// </summary>
    public partial class ProcessesListView : UserControl
    {
        public ProcessesListView()
        {
            InitializeComponent();
        }

        private void OnHeaderClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader { Column: not null } headerClicked)
            {
                if (this.DataContext is ProcessesListViewModel viewModel)
                {
                    string column = headerClicked.Column.Header as string;
                    if (!string.IsNullOrEmpty(column))
                    {
                        viewModel.SortBy(column);
                    }
                }
            }
        }
    }
}
