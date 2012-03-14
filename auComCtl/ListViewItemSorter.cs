using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace au.util.comctl {
  /// <summary>
  /// Allows ListView controls in details view to sort based on any column
  /// </summary>
  public class ListViewItemSorter : IComparer {
    #region SortColumnInfo
    /// <summary>
    /// Defines parameters of a sort column.
    /// </summary>
    private struct SortColumnInfo {
      /// <summary>
      /// Index of the column being sorted.
      /// </summary>
      public int Index;

      /// <summary>
      /// Type of data in the column.
      /// </summary>
      public SortType Type;

      /// <summary>
      /// Whether the sort order is reversed.
      /// </summary>
      public bool Reverse;

      /// <summary>
      /// Defines all parameters of a sort column.
      /// </summary>
      /// <param name="index">Index of the column being sorted.</param>
      /// <param name="type">Type of data in the column.</param>
      /// <param name="reverse">Whether the sort order is reversed.</param>
      public SortColumnInfo(int index, SortType type, bool reverse) {
        Index = index;
        Reverse = reverse;
        Type = type;
      }
    }
    #endregion SortColumnInfo

    private List<SortColumnInfo> SortColumns;
    private int _indicatorColumn = -1;

    /// <summary>
    /// Gets the index of the column used to sort the list.
    /// </summary>
    public int SortColumn {
      get { return SortColumns[SortColumns.Count - 1].Index; }
    }

    /// <summary>
    /// Gets the index of the column that will show a sort indicator.
    /// </summary>
    public int IndicatorColumn {
      get { return _indicatorColumn; }
    }

    /// <summary>
    /// Whether the sort order should be reversed.
    /// </summary>
    public bool ReverseOrder {
      get { return SortColumns[SortColumns.Count - 1].Reverse; }
    }

    /// <summary>
    /// Creates a new ListViewItemSorter to sort ListView items, with no initial sort.
    /// </summary>
    public ListViewItemSorter() {
      SortColumns = new List<SortColumnInfo>();
    }

    /// <summary>
    /// Creates a new ListViewItemSorter to sort ListView items.
    /// </summary>
    /// <param name="sortColumn">Index of the column to sort.</param>
    /// <param name="sortType">How to sort the data.</param>
    /// <param name="reverseOrder">True if the column should be sorted in reverse order.</param>
    /// <param name="indicatorColumn">Index of the column to show sort indicators on.</param>
    public ListViewItemSorter(int sortColumn, SortType sort, bool reverseOrder, int indicatorColumn) : this() {
      SortColumns.Add(new SortColumnInfo(sortColumn, sort, reverseOrder));
      _indicatorColumn = indicatorColumn;
    }
    public ListViewItemSorter(int sortColumn, SortType sort, bool reverseOrder) : this(sortColumn, sort, reverseOrder, sortColumn) { }
    public ListViewItemSorter(int sortColumn, SortType sort, int indicatorColumn) : this(sortColumn, sort, false, indicatorColumn) { }
    public ListViewItemSorter(int sortColumn, SortType sort) : this(sortColumn, sort, false, sortColumn) { }
    public ListViewItemSorter(int sortColumn, bool reverseOrder, int indicatorColumn) : this(sortColumn, SortType.String, reverseOrder, indicatorColumn) { }
    public ListViewItemSorter(int sortColumn, bool reverseOrder) : this(sortColumn, SortType.String, reverseOrder, sortColumn) { }
    public ListViewItemSorter(int sortColumn, int indicatorColumn) : this(sortColumn, SortType.String, false, indicatorColumn) { }
    public ListViewItemSorter(int sortColumn) : this(sortColumn, SortType.String, false, sortColumn) { }

    /// <summary>
    /// Add the specified criteria as the primary sort criteria.
    /// </summary>
    /// <param name="columnIndex">Index of the column to sort.</param>
    /// <param name="sortType">How to sort the data.</param>
    /// <param name="reverseOrder">True if the column should be sorted in reverse order.</param>
    /// <param name="indicatorColumnIndex">Index of the column that should display the sort indicator.</param>
    public void SortBy(int columnIndex, SortType sort, bool reverseOrder, int indicatorColumnIndex) {
      SortColumns.Insert(0, new SortColumnInfo(columnIndex, sort, reverseOrder));
      _indicatorColumn = indicatorColumnIndex;
      // if the same logical index is already being sorted, remove it since this sort takes care of it.
      for(int i = 1; i < SortColumns.Count; i++)
        if(SortColumns[i].Index == columnIndex) {
          SortColumns.RemoveAt(i);
          break;  // a logical index can only be in the list once, so quit if it's found.
        }
    }
    public void SortBy(int columnIndex, SortType sort, bool reverseOrder) { SortBy(columnIndex, sort, reverseOrder, columnIndex); }
    public void SortBy(int columnIndex, SortType sort, int indicatorColumnIndex) { SortBy(columnIndex, sort, false, indicatorColumnIndex); }
    public void SortBy(int columnIndex, SortType sort) { SortBy(columnIndex, sort, false, columnIndex); }
    public void SortBy(int columnIndex, bool reverseOrder, int indicatorColumnIndex) { SortBy(columnIndex, SortType.String, reverseOrder, indicatorColumnIndex); }
    public void SortBy(int columnIndex, bool reverseOrder) { SortBy(columnIndex, SortType.String, reverseOrder, columnIndex); }
    public void SortBy(int columnIndex, int indicatorColumnIndex) { SortBy(columnIndex, SortType.String, false, indicatorColumnIndex); }
    public void SortBy(int columnIndex) { SortBy(columnIndex, SortType.String, false, columnIndex); }

    /// <summary>
    /// Compares two ListViewItems for sorting.
    /// </summary>
    /// <param name="x">ListViewItem to compare.</param>
    /// <param name="y">ListViewItem to compare.</param>
    /// <returns>0 if equal, negative if x comes before y, or positive if x comes after y.</returns>
    public int Compare(object x, object y) {
      if(!(x is ListViewItem))
        throw new ArgumentException("ListViewItemSorter can only compare ListViewItem objects.", "x");
      if(!(y is ListViewItem))
        throw new ArgumentException("ListViewItemSorter can only compare ListViewItem objects.", "y");
      return Compare((ListViewItem)x, (ListViewItem)y);
    }

    /// <summary>
    /// Compares two ListViewItems for sorting.
    /// </summary>
    /// <param name="x">ListViewItem to compare.</param>
    /// <param name="y">ListViewItem to compare.</param>
    /// <returns>0 if equal, negative if x comes before y, or positive if x comes after y.</returns>
    public int Compare(ListViewItem x, ListViewItem y) {
      int result = 0;
      for(int c = 0; result == 0 && c < SortColumns.Count; c++) {
        switch(SortColumns[c].Type) {
          case SortType.Title:
            result = GetSortableTitle(x.SubItems[SortColumns[c].Index].Text).CompareTo(GetSortableTitle(y.SubItems[SortColumns[c].Index].Text));
            break;
        case SortType.CaseSensitiveString:
          result = x.SubItems[SortColumns[c].Index].Text.CompareTo(y.SubItems[SortColumns[c].Index].Text);
          break;
        case SortType.Number:
          try {
            double numX = double.Parse(x.SubItems[SortColumns[c].Index].Text);
            double numY = double.Parse(y.SubItems[SortColumns[c].Index].Text);
            if(numX < numY)
              result = -1;
            else if(numX > numY)
              result = 1;
          } catch { }
          break;
        case SortType.Date:
          try {
            DateTime dtX = DateTime.Parse(x.SubItems[SortColumns[c].Index].Text);
            DateTime dtY = DateTime.Parse(y.SubItems[SortColumns[c].Index].Text);
            result = dtX.CompareTo(dtY);
          } catch { }
          break;
        case SortType.String:
        default:
          result = x.SubItems[SortColumns[c].Index].Text.ToLower().CompareTo(y.SubItems[SortColumns[c].Index].Text.ToLower());
          break;
      }
      if(SortColumns[c].Reverse)
        result = -result;
      }
      return result;
    }

    /// <summary>
    /// Turns a title into a string suitable for case-insensitive sorting.
    /// </summary>
    /// <param name="title">Display title.</param>
    /// <returns>Sortable title.</returns>
    private string GetSortableTitle(string title) {
      title = title.ToLower();
      if(title.StartsWith("the "))
        title = title.Substring(4);
      else if(title.StartsWith("an "))
        title = title.Substring(3);
      else if(title.StartsWith("a "))
        title = title.Substring(2);
      return title;
    }
  }
}
