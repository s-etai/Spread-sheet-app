// <copyright file="CircularReferenceException.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Exception for when a cell has a circular reference.
    /// </summary>
    public class CircularReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircularReferenceException"/> class.
        /// </summary>
        public CircularReferenceException()
        : base("A circular reference was detected.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularReferenceException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public CircularReferenceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularReferenceException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public CircularReferenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
