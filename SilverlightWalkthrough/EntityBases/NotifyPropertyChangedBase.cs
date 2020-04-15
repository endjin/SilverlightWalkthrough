namespace SilverlightWalkthrough.EntityBases
{
    #region Using Directives 

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    using SilverlightWalkthrough.Commands;

    #endregion

    /// <summary>
    /// Base class that raises the property changed.
    /// </summary>
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        private static readonly Dictionary<string, PropertyChangedEventArgs> EventArgsCache =
            new Dictionary<string, PropertyChangedEventArgs>();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event using a string
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that changed
        /// </param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyProperty(propertyName);
            this.OnPropertyChangedCore(propertyName);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <typeparam name="T">
        /// The type of the property (usually implicit)
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// Raised if the property name is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Raised if the property does not point to a valid member
        /// </exception>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            var memberExpression = propertyName.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException(
                   "propertyName");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    "propertyName");
            }

            this.OnPropertyChanged(propertyInfo.Name);
        }

        private static PropertyChangedEventArgs LookupEventArgs(string propertyName)
        {
            PropertyChangedEventArgs e;
            if (!EventArgsCache.TryGetValue(propertyName, out e))
            {
                e = new PropertyChangedEventArgs(propertyName);
                EventArgsCache.Add(propertyName, e);
            }

            return e;
        }

        private void OnPropertyChangedCore(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs eventArgs = LookupEventArgs(propertyName);
                this.PropertyChanged(this, eventArgs);
            }

#if SILVERLIGHT
            CommandManager.InvalidateRequerySuggested();
#endif
        }

        private void VerifyProperty(string propertyName)
        {
            var member = this.GetType().FindMembers(
                MemberTypes.Property, BindingFlags.Public | BindingFlags.Instance, (m, o) => m.Name == propertyName, null);
            if (member.Length != 1)
            {
                throw new ArgumentException(
                    "propertyName");
            }
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyPropertyChangedBase.cs" company="Endjin Ltd">
//   Copyright © 2010 Endjin Ltd
// </copyright>
// <summary>
//   Base class that raises the property changed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------