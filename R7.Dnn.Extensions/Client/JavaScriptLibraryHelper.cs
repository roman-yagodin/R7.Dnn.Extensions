﻿//
//  JavaScriptLibraryHelper.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2020 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using DotNetNuke.Common;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;

namespace R7.Dnn.Extensions.Client
{
    public static class JavaScriptLibraryHelper
    {
        /// <summary>
        /// Port the <see cref="M:DotNetNuke.Framework.JavaScriptLibraries.JavaScript.GetHighestVersionLibrary" /> private method.
        /// </summary>
        public static JavaScriptLibrary GetHighestVersionLibrary (string jsname)
        {
            // if in install process, then do not use JSL but all use the legacy versions.
            if (Globals.Status == Globals.UpgradeStatus.Install) {
                return null;
            }
            try {
                return JavaScriptLibraryController.Instance.GetLibraries (jsl => jsl.LibraryName.Equals (jsname, StringComparison.OrdinalIgnoreCase))
                                                           .OrderByDescending (jsl => jsl.Version)
                                                           .FirstOrDefault ();
            }
            catch (Exception) {
                // no library found (install or upgrade)
                return null;
            }
        }

        public static void RegisterStyleSheet (JavaScriptLibrary jsLibrary, Page page, string filePath, int priority, string provider, string name)
        {
            ClientResourceManager.RegisterStyleSheet (page, $"/Resources/Libraries/{jsLibrary.LibraryName}/{FormatVersionPath (jsLibrary.Version)}/{filePath}",
                priority, provider, name, jsLibrary.Version.ToString ());
        }

        public static void RegisterScript (JavaScriptLibrary jsLibrary, Page page, string filePath, int priority, string provider, string name)
        {
            ClientResourceManager.RegisterScript (page, $"/Resources/Libraries/{jsLibrary.LibraryName}/{FormatVersionPath (jsLibrary.Version)}/{filePath}",
                priority, provider, name, jsLibrary.Version.ToString ());
        }

        public static void RegisterStyleSheet (JavaScriptLibrary jsLibrary, Page page, string filePath)
        {
            RegisterStyleSheet (jsLibrary, page, filePath, (int) FileOrder.Css.DefaultPriority, "DnnPageHeaderProvider", ExtractBaseFileName (filePath));
        }

        public static void RegisterScript (JavaScriptLibrary jsLibrary, Page page, string filePath)
        {
            RegisterScript (jsLibrary, page, filePath, (int) FileOrder.Js.DefaultPriority, "DnnFormBottomProvider", ExtractBaseFileName (filePath));
        }

        static string FormatVersionPath (Version version)
        {
            return Globals.FormatVersion (version, "00", 3, "_");
        }

        static string ExtractBaseFileName (string filePath)
        {
            return Path.GetFileNameWithoutExtension (filePath).Replace (".min", "");
        }
    }
}
