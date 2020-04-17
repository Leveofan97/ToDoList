using System;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Commands
{
    class ItemCommand : ICommand
    {
        private Action<Item> execute;
        public ItemCommand(Action<Item> execute)
        {
            this.execute = execute;
        }

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// check if command can be execurted 
        /// </summary>
        /// <param name="parameter"> parameters send by button (selected Item)</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (parameter == null) return false;
            return !String.IsNullOrWhiteSpace(((Item)parameter).Description);
        }

        /// <summary>
        /// run if command can be executed
        /// </summary>
        /// <param name="parameter"> parameters send by button (selected Item)</param>
        public void Execute(object parameter)
        {
            execute.Invoke((Item)(parameter));
        }

        #endregion
    }
}
