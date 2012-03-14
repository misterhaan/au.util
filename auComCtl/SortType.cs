using System;

namespace au.util.comctl {
  /// <summary>
  /// Types of sorting
  /// </summary>
  public enum SortType {
    /// <summary>
    /// Case-insensitive string sort
    /// </summary>
    String,
    /// <summary>
    /// Case-sensitive string sort
    /// </summary>
    CaseSensitiveString,
    /// <summary>
    /// Numeric sort
    /// </summary>
    Number,
    /// <summary>
    /// Date/time sort
    /// </summary>
    Date,
    /// <summary>
    /// Case-insensitive title string sort (ignores leading the/a/an)
    /// </summary>
    Title

  }
}