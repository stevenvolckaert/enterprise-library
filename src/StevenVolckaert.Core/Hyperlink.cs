﻿namespace StevenVolckaert
{
    using System;

    /// <summary>
    ///     Represents a hyperlink.
    /// </summary>
    public struct Hyperlink
    {
        /// <summary>
        ///     Gets or sets the label of the hyperlink.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        ///     Gets or sets the URI of the hyperlink.
        /// </summary>
        public Uri Uri { get; set; }
    }
}
