using Prism.Commands;
using Prism.Mvvm;
using processes_list.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace processes_list.ViewModels
{
    public class ProcessDetailsViewModel : BindableBase
    {
        public bool IsSelectedProcess => SelectedProcess != null;
        private ProcessModel _selectedProcess;
        public ProcessModel SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                if (SetProperty(ref _selectedProcess, value))
                {
                    RaisePropertyChanged(nameof(IsSelectedProcess));
                }
            }
        }

        public ProcessDetailsViewModel()
        {

        }

        public void Update(ProcessModel process)
        {
            SelectedProcess = process;
            RaisePropertyChanged(nameof(IsSelectedProcess));
        }
    }
}
