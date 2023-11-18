using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace processes_list.Models
{
    public class ProcessModel : INotifyPropertyChanged
    {
        public System.Diagnostics.Process Process { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ProcessPriorityClass Priority { get; set; }
        public string PriorityString { get; set; }
        public int ThreadsNumber { get; set; }
        public ProcessThreadCollection Threads { get; set; }

        public ProcessModel(System.Diagnostics.Process process)
        {
            Process = process;
            Id = process.Id;
            Name = process.ProcessName;
            try
            {
                Priority = process.PriorityClass;
                PriorityString = Priority.ToString();
            }
            catch (Exception e)
            {
                PriorityString = e.Message;
            }
            ThreadsNumber = process.Threads.Count;
            Threads = process.Threads;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
