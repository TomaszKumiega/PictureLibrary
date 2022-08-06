﻿using PictureLibraryViewModel.Commands;
using System;
using System.Windows.Input;

namespace PictureLibraryWPF.Commands
{
    public class Command : IPictureLibraryCommand
    {
        public Func<object, bool> CanExecuteProp { get; set; }
        public Action<object> ExecuteProp { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteProp?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            ExecuteProp?.Invoke(parameter);
        }
    }
}