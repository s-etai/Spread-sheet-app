// <copyright file="SelfReferenceException.cs" company="Elliott Tai 11844538">
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
    /// Exeption for when a cell references it self.
    /// </summary>
    public class SelfReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfReferenceException"/> class.
        /// </summary>
        public SelfReferenceException()
            : base("Self Reference Detected.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfReferenceException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public SelfReferenceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfReferenceException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public SelfReferenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
