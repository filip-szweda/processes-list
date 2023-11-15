using Prism.Commands;
using Prism.Mvvm;
using processes_list.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace processes_list.ViewModels
{
    public class ProcessesListViewModel : BindableBase
    {
        private ObservableCollection<ProcessModel> _processes;

        public ObservableCollection<ProcessModel> Processes
        {
            get => _processes;
            set => SetProperty(ref _processes, value);
        }

        public ProcessesListViewModel()
        {
            _processes = new ObservableCollection<ProcessModel>(LoadProcesses());
        }

        private List<ProcessModel> LoadProcesses()
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Loading processes");
            return Process.GetProcesses().Select(p => new ProcessModel(p)).ToList();
        }
    }
}
