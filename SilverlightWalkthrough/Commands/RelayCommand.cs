namespace SilverlightWalkthrough.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// A command whose purpose is to relay its functionality to other
    ///   objects by invoking delegates. The default return value for the CanExecute
    ///   method is 'true'.  This class does not allow you to accept command parameters in the
    ///   Execute and CanExecute callback methods.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action execute;

        private readonly Func<bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">
        /// The execution logic.
        /// </param>
        /// <param name="canExecute">
        /// The execution status logic.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the execute argument is null.
        /// </exception>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
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
        /// This parameter will always be ignored.
        /// </param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// This parameter will always be ignored.
        /// </param>
        public void Execute(object parameter)
        {
            this.execute();
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommand.cs" company="Endjin Ltd">
//   Copyright © 2010 Endjin Ltd
// </copyright>
// <summary>
//   A command whose sole purpose is to relay its functionality to other
//   objects by invoking delegates. The default return value for the CanExecute
//   method is 'true'.  This class does not allow you to accept command parameters in the
//   Execute and CanExecute callback methods.
// </summary>
// <credits>This class was originally developed by Josh Smith (http://joshsmithonwpf.wordpress.com) and
// slightly modified by Laurent Bugnion (http://www.galasoft.ch) with Josh's permission. Modified again by Endjin to integrate
// with the Silverlight CommandManager. 
// </credits>
// --------------------------------------------------------------------------------------------------------------------