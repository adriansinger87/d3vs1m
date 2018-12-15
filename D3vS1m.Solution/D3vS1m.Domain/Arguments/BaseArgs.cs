﻿using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Arguments
{
    /// <summary>
    /// This class is an abstract base class for all concrete argument implementations used by the simulation models
    /// </summary>
    public abstract class BaseArgs
    {
        /// <summary>
        /// Gets or sets the name of the specific arguments class
        /// </summary>
        public string Name { get; set; }
    }
}
