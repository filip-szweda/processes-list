using Prism.Commands;
using Prism.Mvvm;
using processes_list.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace processes_list.ViewModels
{
    public class ProcessDetailsViewModel : BindableBase
    {
        private ProcessModel _selectedProcess;
        private ProcessPriorityClass _selectedPriority;

        public ICommand KillCommand { get; private set; }
        public ICommand SetPriorityCommand { get; private set; }
        public List<ProcessPriorityClass> AccessiblePriorities { get; } = Enum.GetValues(typeof(ProcessPriorityClass)).Cast<ProcessPriorityClass>().ToList();

        public bool IsSelectedProcess => SelectedProcess != null;

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

        public ProcessPriorityClass SelectedPriority
        {
            get => _selectedPriority;
            set => SetProperty(ref _selectedPriority, value);
        }

        public ProcessDetailsViewModel()
        {
            KillCommand = new DelegateCommand(Kill);
            SetPriorityCommand = new DelegateCommand(SetPriority);
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

        public void SetPriority()
        {
            try
            {
                SelectedProcess.Process.PriorityClass = SelectedPriority;
                SelectedProcess.Priority = SelectedPriority;
                SelectedProcess.PriorityString = SelectedProcess.Priority.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
