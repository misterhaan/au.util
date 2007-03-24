using System;
using System.Collections;  // IComparer
using System.Windows.Forms;  // ListViewItem

namespace au.util.comctl {
  /// <summary>
  /// Allows ListView controls in details view to sort based on any column
  /// </summary>
  public class ListViewItemSorter : IComparer {
    #region Data Members
    private int _column;
    private bool _reverse;
    private SortType _sort;
    #endregion  // Data Members

    #region Constructors
    public ListViewItemSorter() : this(0, false, SortType.String) { }
    public ListViewItemSorter(int sortColumn) : this(sortColumn, false, SortType.String) { }
    public ListViewItemSorter(bool reverseOrder) : this(0, reverseOrder, SortType.String) { }
    public ListViewItemSorter(SortType sort) : this(0, false, sort) { }
    public ListViewItemSorter(int sortColumn, bool reverseOrder) : this(sortColumn, reverseOrder, SortType.String) { }
    public ListViewItemSorter(int sortColumn, SortType sort) : this(sortColumn, false, sort) { }
    public ListViewItemSorter(bool reverseOrder, SortType sort) : this(0, reverseOrder, sort) { }
    /// <summary>
    /// Creates a new ListViewItemSorter to sort list view items
    /// </summary>
    /// <param name="sortColumn">Index of the column to sort</param>
    /// <param name="reverseOrder">True if the column should be sorted in reverse order</param>
    /// <param name="sortType">How to sort the data</param>
    public ListViewItemSorter(int sortColumn, bool reverseOrder, SortType sort) {
      _column = sortColumn;
      _reverse = reverseOrder;
      _sort = sort;
    }
    #endregion  // Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the index of the column used to sort the list
    /// </summary>
    public int Column {
      get { return _column; }
      set { _column = value; }
    }

    /// <summary>
    /// Whether the sort order should be reversed
    /// </summary>
    public bool ReverseOrder {
      get { return _reverse; }
      set { _reverse = value; }
    }

    /// <summary>
    /// Type of sorting to use
    /// </summary>
    public SortType Sort {
      get { return _sort; }
      set { _sort = value; }
    }
    #endregion  // Properties

    #region Methods
    /// <summary>
    /// Compares two ListViewItems for sorting
    /// </summary>
    /// <param name="x">ListViewItem to compare</param>
    /// <param name="y">ListViewItem to compare</param>
    /// <returns>0 if equal, negative is x comes before y, or positive if x comes after y</returns>
    public int Compare(object x, object y) {
      if(!(x is ListViewItem))
        throw new ArgumentException("ListViewItemSorter can only compare ListViewItem objects.", "x");
      if(!(y is ListViewItem))
        throw new ArgumentException("ListViewItemSorter can only compare ListViewItem objects.", "y");
      return Compare((ListViewItem)x, (ListViewItem)y);
    }

    /// <summary>
    /// Compares two ListViewItems for sorting
    /// </summary>
    /// <param name="x">ListViewItem to compare</param>
    /// <param name="y">ListViewItem to compare</param>
    /// <returns>0 if equal, negative is x comes before y, or positive if x comes after y</returns>
    public int Compare(ListViewItem x, ListViewItem y) {
      int result = 0;
      switch(_sort) {
        case SortType.CaseSensitiveString:
          result = x.SubItems[_column].Text.CompareTo(y.SubItems[_column].Text);
          break;
        case SortType.Number:
          try {
            double numX = double.Parse(x.SubItems[_column].Text);
            double numY = double.Parse(y.SubItems[_column].Text);
            if(numX < numY)
              result = -1;
            else if(numX > numY)
              result = 1;
          } catch { }
          break;
        case SortType.Date:
          try {
            DateTime dtX = DateTime.Parse(x.SubItems[_column].Text);
            DateTime dtY = DateTime.Parse(y.SubItems[_column].Text);
            result = dtX.CompareTo(dtY);
          } catch { }
          break;
        case SortType.String:
        default:
          result = x.SubItems[_column].Text.ToLower().CompareTo(y.SubItems[_column].Text.ToLower());
          break;
      }
      if(_reverse)
        result = -result;
      return result;
    }
    #endregion  // Methods
  }
}
