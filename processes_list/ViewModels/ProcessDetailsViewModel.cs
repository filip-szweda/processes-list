using Prism.Commands;
using Prism.Mvvm;
using processes_list.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace processes_list.ViewModels
{
    public class ProcessDetailsViewModel : BindableBase
    {
        public ICommand KillCommand { get; private set; }
        
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
            KillCommand = new DelegateCommand(Kill);
        }

        public void Update(ProcessModel process)
        {
            SelectedProcess = process;
            RaisePropertyChanged(nameof(IsSelectedProcess));
        }

        public void Kill()
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Killed process {SelectedProcess.Name} with Id: {SelectedProcess.Id}");
            SelectedProcess.Process.Kill();
        }
    }
}
