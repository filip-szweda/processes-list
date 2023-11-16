using Prism.Commands;
using Prism.Mvvm;
using processes_list.Models;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace processes_list.ViewModels
{
    public class ProcessesListViewModel : BindableBase
    {
        private ObservableCollection<ProcessModel> _processes;
        private int _refreshIntervalInSeconds = 5;
        private Timer _refreshTimer;

        public ObservableCollection<ProcessModel> Processes
        {
            get => _processes;
            set => SetProperty(ref _processes, value);
        }

        public ProcessesListViewModel()
        {
            _processes = new ObservableCollection<ProcessModel>(LoadProcesses());
            StartProcessesRefreshTimer();
        }

        private List<ProcessModel> LoadProcesses()
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Loading processes");
            return Process.GetProcesses().Select(p => new ProcessModel(p)).ToList();
        }

        private void OnRefresh(object source, ElapsedEventArgs e)
        {
            var updatedProcesses = LoadProcesses();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Processes.Clear();
                foreach (var process in updatedProcesses)
                {
                    Processes.Add(process);
                }
            });
        }

        private void StartProcessesRefreshTimer()
        {
            _refreshTimer = new Timer(_refreshIntervalInSeconds * 1000) { AutoReset = true };
            _refreshTimer.Elapsed += OnRefresh;
            _refreshTimer.Start();
        }
    }
}
