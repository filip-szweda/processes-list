﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace processes_list.Models
{
    public class ProcessModel
    {
        public System.Diagnostics.Process Process { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ProcessPriorityClass Priority { get; set; }
        public string PriorityString { get; set; }

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
        }
    }
}
