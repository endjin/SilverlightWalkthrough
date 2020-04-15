namespace SilverlightWalkthrough.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// A generic command whose sole purpose is to relay its functionality to other
    ///   objects by invoking delegates. The default return value for the CanExecute
    ///   method is 'true'. This class allows you to accept command parameters in the
    ///   Execute and CanExecute callback methods.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the command parameter.
    /// </typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// The execute.
        /// </summary>
        private readonly Action<T> execute;

        /// <summary>
        /// The can execute.
        /// </summary>
        private readonly Predicate<T> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class. 
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">
        /// The execution logic.
        /// </param>
        /// <param name="canExecute">
        /// The execution status logic.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the execute argument is null.
        /// </exception>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        ///   Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data 
        ///   to be passed, this object can be set to a null reference
        /// </param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute((T)parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data 
        ///   to be passed, this object can be set to a null reference
        /// </param>
        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommandGeneric.cs" company="Endjin Ltd">
//   Copyright © 2010 Endjin Ltd
// </copyright>
// <summary>
//   A generic command whose sole purpose is to relay its functionality to other
//   objects by invoking delegates. The default return value for the CanExecute
//   method is 'true'. This class allows you to accept command parameters in the
//   Execute and CanExecute callback methods.
// </summary>
// <credits>This class was originally developed by Laurent Bugnion (http://www.galasoft.ch) 
// and released under the MIT license.</credits>
// --------------------------------------------------------------------------------------------------------------------