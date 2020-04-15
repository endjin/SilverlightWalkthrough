namespace SilverlightWalkthrough.Commands
{
    #region

    using System;
    using System.Collections.Generic;

    using SilverlightWalkthrough.Async;

    #endregion

    /// <summary>
    /// The command manager.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        ///   Commands we know about
        /// </summary>
        private readonly List<WeakReference> eventHandlers = new List<WeakReference>();

        /// <summary>
        ///   The current command manager
        /// </summary>
        private static CommandManager current;

        /// <summary>
        ///   Determines if we have already queued a request
        /// </summary>
        private bool hasCanExecuteQueued;

        public static event EventHandler RequerySuggested
        {
            add
            {
                AddWeakReferenceHandler(Current.eventHandlers, value);
            }

            remove
            {
                RemoveWeakReferenceHandler(Current.eventHandlers, value);
            }
        }

        /// <summary>
        ///   Gets Current.
        /// </summary>
        public static CommandManager Current
        {
            get
            {
                return current ?? (current = new CommandManager());
            }
        }

        /// <summary>
        /// The invalidate requery suggested.
        /// </summary>
        public static void InvalidateRequerySuggested()
        {
            Current.RaiseCanExecuteChanged();
        }

        private static void RemoveWeakReferenceHandler(List<WeakReference> weakReferences, EventHandler handler)
        {
            for (int i = weakReferences.Count - 1; i >= 0; i--)
            {
                WeakReference reference = weakReferences[i];
                EventHandler target = reference.Target as EventHandler;
                if ((target == null) || (target == handler))
                {
                    weakReferences.RemoveAt(i);
                }
            }
        }

        private static void AddWeakReferenceHandler(List<WeakReference> weakReferences, EventHandler handler)
        {
            weakReferences.Add(new WeakReference(handler));
        }

        private static void CallWeakReferenceHandlers(List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                EventHandler[] handlerArray = new EventHandler[handlers.Count];
                int index = 0;
                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = handlers[i];
                    EventHandler target = reference.Target as EventHandler;
                    if (target == null)
                    {
                        handlers.RemoveAt(i);
                    }
                    else
                    {
                        handlerArray[index] = target;
                        index++;
                    }
                }

                for (int j = 0; j < index; j++)
                {
                    EventHandler handler2 = handlerArray[j];
                    handler2(null, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Raises requery changes for registered commands
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            if (this.hasCanExecuteQueued)
            {
                return;
            }

            this.hasCanExecuteQueued = true;

            DispatcherHelper.UIDispatcher.BeginInvoke(
                () =>
                {
                    CallWeakReferenceHandlers(eventHandlers);
                    this.hasCanExecuteQueued = false;
                });
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandManager.cs" company="Endjin Ltd">
//   Copyright © 2010 Endjin Ltd
// </copyright>
// <summary>
//   The command manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
