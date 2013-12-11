﻿// SEVENENGINE LISCENSE:
// You are free to use, modify, and distribute any or all code segments/files for any purpose
// including commercial use under the following condition: any code using or originally taken 
// from the SevenEngine project must include citation to its original author(s) located at the
// top of each source code file, or you may include a reference to the SevenEngine project as
// a whole but you must include the current SevenEngine official website URL and logo.
// - Thanks.  :)  (support: seven@sevenengine.com)

// Author(s):
// - Zachary Aaron Patten (aka Seven) seven@sevenengine.com
// Last Edited: 11-16-13

using System;
using System.IO;

namespace SevenEngine
{
  /// <summary>Will generate filepaths in a cross platform manor.</summary>
  public static class FilePath
  {
    public static string FromRelative(string path)
    {
      if ((path[0] == '\\' || path[0] == '/') && (path[path.Length - 1] == '\\' || path[path.Length - 1] == '/'))
        return Directory.GetCurrentDirectory() + Path.PathSeparator + Path.Combine(path.Split('\\')) + Path.PathSeparator;
      else if (path[0] == '\\' || path[0] == '/')
        return Directory.GetCurrentDirectory() + Path.PathSeparator + Path.Combine(path.Split('\\'));
      else if (path[path.Length - 1] == '\\' || path[path.Length - 1] == '/')
        return Directory.GetCurrentDirectory() + Path.Combine(path.Split('\\')) + Path.PathSeparator;
      else
        return Directory.GetCurrentDirectory() + Path.Combine(path.Split('\\'));
    }
  }
}
