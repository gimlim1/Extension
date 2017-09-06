//------------------------------------------------------------------------------
// <copyright file="VentanaRepositorio.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace R2PB.AnalisisCodigo.Extension
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("36d472e8-90e7-416b-a90f-28b9c616f176")]
    public class VentanaRepositorio : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VentanaRepositorio"/> class.
        /// </summary>
        public VentanaRepositorio() : base(null)
        {
            this.Caption = "CodeMiner";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new VentanaRepositorioControl();
        }
    }
}
