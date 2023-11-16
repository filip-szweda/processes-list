﻿using Prism.Commands;
using Prism.Mvvm;
using processes_list.Models;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace processes_list.ViewModels
{
    public class ProcessesListViewModel : BindableBase
    {
        private ObservableCollection<ProcessModel> _processes;
        private Timer _refreshTimer;

        public ICommand RefreshCommand { get; private set; }
        public ICommand StartRefreshingCommand { get; private set; }
        public ICommand StopRefreshingCommand { get; private set; }
        public ICommand SetRefreshIntervalCommand { get; private set; }

        public ObservableCollection<ProcessModel> Processes
        {
            get => _processes;
            set => SetProperty(ref _processes, value);
        }

        private string _textBoxContent;
        public string TextBoxContent
        {
            get { return _textBoxContent; }
            set
            {
                _textBoxContent = value;
                RaisePropertyChanged(nameof(TextBoxContent));
            }
        }

        private string _processFilter;
        public string ProcessFilter
        {
            get { return _processFilter; }
            set
            {
                _processFilter = value;
                FilterProcesses(Processes.ToList());
                RaisePropertyChanged(nameof(ProcessFilter));
            }
        }


        public ProcessesListViewModel()
        {
            RefreshCommand = new DelegateCommand(RefreshProcesses);
            StartRefreshingCommand = new DelegateCommand(() => _refreshTimer.Start());
            StopRefreshingCommand = new DelegateCommand(() => _refreshTimer.Stop());
            SetRefreshIntervalCommand = new DelegateCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Setting processes refresh interval to {TextBoxContent}s");
                if (int.TryParse(TextBoxContent, out int parsedInterval))
                {
                    int _refreshIntervalInSeconds = parsedInterval * 1000;
                    _refreshTimer.Interval = _refreshIntervalInSeconds;
                }
            });

            _processes = new ObservableCollection<ProcessModel>(LoadProcesses());
            InitializeProcessesRefreshTimer();
        }

        private List<ProcessModel> LoadProcesses()
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Loading processes");
            return Process.GetProcesses().Select(p => new ProcessModel(p)).ToList();
        }

        private void DispatchProcesses(List<ProcessModel> processes)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Processes.Clear();
                foreach (var process in processes)
                {
                    Processes.Add(process);
                }
            });
        }

        private void FilterProcesses(List<ProcessModel> processes)
        {
            var filteredProcesses = processes;

            if (!string.IsNullOrEmpty(ProcessFilter))
            {
                filteredProcesses = processes.Where(p => p.Name.Contains(ProcessFilter)).ToList();
            }

            DispatchProcesses(filteredProcesses);
        }

        private void RefreshProcesses()
        {
            var processes = LoadProcesses();
            FilterProcesses(processes);
        }

        private void OnRefresh(object source, ElapsedEventArgs e)
        {
            RefreshProcesses();
        }

        private void InitializeProcessesRefreshTimer()
        {
            int _refreshIntervalInSeconds = 5;
            _refreshTimer = new Timer(_refreshIntervalInSeconds * 1000) { AutoReset = true };
            _refreshTimer.Elapsed += OnRefresh;
            _refreshTimer.Start();
        }
    }
}
