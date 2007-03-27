using System;
using System.Collections.Generic;  // List<>
using System.Runtime.InteropServices;  // StructLayout, MarshalAs, DllImport
using System.Windows.Forms;  // ListView, ColumnHeader

namespace au.util.comctl {
  /// <summary>
  /// Provides utility functions for working with ListView controls
  /// </summary>
  public static class ListViewUtil {
    #region External Data Structures
    [StructLayout(LayoutKind.Sequential)]
    private struct HDITEM {
      public int mask;
      public int cxy;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string pszText;
      public IntPtr hbm;
      public int cchTextMax;
      public int fmt;
      public int lParam;
      public int iImage;
      public int iOrder;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    private struct LV_COLUMN {
      public uint mask;
      public int fmt;
      public int cx;
      public string pszText;
      public int cchTextMax;
      public int iSubItem;
      public int iImage;
      public int iOrder;
    }  
    #endregion  // External Data Structures

    #region External Constants
    private const int LVM_GETHEADER = 0x101f;
    private const int LVM_GETCOLUMN = 0x105f;
    private const int HDI_FORMAT = 4;
    private const int HDF_LEFT = 0;
    private const int HDF_RIGHT = 1;
    private const int HDF_CENTER = 2;
    private const int HDF_SORTDOWN = 0x200;
    private const int HDF_SORTUP = 0x400;
    private const int HDF_BITMAP_ON_RIGHT = 0x1000;
    private const int HDF_STRING = 0x4000;
    private const int HDM_SETITEM = 0x120c;
    private const int LVCF_ORDER = 0x20;
    #endregion  // External Constants

    #region DetailSort
    /// <summary>
    /// Sorts the items in a ListView in detail view by one of the columns
    /// </summary>
    /// <param name="list">ListView being sorted</param>
    /// <param name="sortColumnIndex">Column to sort by</param>
    /// <param name="indicatorColumnIndex">Column to show sort indicator on</param>
    /// <param name="sort">How to sort the data in the column</param>
    public static void DetailSort(ListView list, int sortColumnIndex, int indicatorColumnIndex, SortType sort) {
      if(list.View != View.Details)
        throw new ArgumentException(Properties.Resources.DetailsViewRequiredMessage, "list");
      ListViewItemSorter sorter = list.ListViewItemSorter as ListViewItemSorter;
      if(sorter == null) {
        sorter = new ListViewItemSorter(sortColumnIndex, sort);
        list.ListViewItemSorter = sorter;
      } else {
        if(sorter.SortColumn == sortColumnIndex)
          sorter.ReverseOrder = !sorter.ReverseOrder;
        else {
          HideSortIndicator(list, sorter.IndicatorColumn);
          sorter.SortColumn = sortColumnIndex;
          sorter.ReverseOrder = false;
        }
      }
      ShowSortIndicator(list, indicatorColumnIndex, sorter.ReverseOrder);
      list.Sort();
    }
    public static void DetailSort(ListView list, ColumnHeader sortColumn, ColumnHeader indicatorColumn, SortType sort) { DetailSort(list, sortColumn.Index, indicatorColumn.Index, sort); }
    public static void DetailSort(ListView list, ColumnHeader sortColumn, int indicatorColumnIndex, SortType sort) { DetailSort(list, sortColumn.Index, indicatorColumnIndex, sort); }
    public static void DetailSort(ListView list, int sortColumnIndex, ColumnHeader indicatorColumn, SortType sort) { DetailSort(list, sortColumnIndex, indicatorColumn.Index, sort); }
    public static void DetailSort(ListView list, ColumnHeader sortColumn, SortType sort) { DetailSort(list, sortColumn.Index, sortColumn.Index, sort); }
    public static void DetailSort(ListView list, int sortColumnIndex, SortType sort) { DetailSort(list, sortColumnIndex, sortColumnIndex, sort); }
    public static void DetailSort(ListView list, ColumnHeader sortColumn, ColumnHeader indicatorColumn) { DetailSort(list, sortColumn.Index, indicatorColumn.Index, SortType.String); }
    public static void DetailSort(ListView list, ColumnHeader sortColumn, int indicatorColumnIndex) { DetailSort(list, sortColumn.Index, indicatorColumnIndex, SortType.String); }
    public static void DetailSort(ListView list, int sortColumnIndex, ColumnHeader indicatorColumn) { DetailSort(list, sortColumnIndex, indicatorColumn.Index, SortType.String); }
    public static void DetailSort(ListView list, int sortColumnIndex, int indicatorColumnIndex) { DetailSort(list, sortColumnIndex, indicatorColumnIndex, SortType.String); }
    public static void DetailSort(ListView list, ColumnHeader sortColumn) { DetailSort(list, sortColumn.Index, sortColumn.Index, SortType.String); }
    public static void DetailSort(ListView list, int sortColumnIndex) { DetailSort(list, sortColumnIndex, sortColumnIndex, SortType.String); }

    /// <summary>
    /// Shows a sort indicator (generally a triangle pointing up or down) on the column header
    /// </summary>
    /// <param name="list">ListView that needs a sort indicator on a column header</param>
    /// <param name="columnIndex">Column to show sort indicator on</param>
    /// <param name="reverse">True if a reverse sort indicator should be shown</param>
    private static void ShowSortIndicator(ListView list, int columnIndex, bool reverse) {
      HDITEM hd = new HDITEM();
      hd.mask = HDI_FORMAT;
      hd.fmt = HDF_STRING | HDF_BITMAP_ON_RIGHT;
      try {
        switch(list.Columns[columnIndex].TextAlign) {
          case HorizontalAlignment.Right:
            hd.fmt |= HDF_RIGHT;
            break;
          case HorizontalAlignment.Center:
            hd.fmt |= HDF_CENTER;
            break;
          case HorizontalAlignment.Left:
            hd.fmt |= HDF_LEFT;
            break;
        }
      } catch { }
      if(reverse)
        hd.fmt |= HDF_SORTDOWN;
      else
        hd.fmt |= HDF_SORTUP;
      IntPtr header = SendMessage(list.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
      SendMessage(header, HDM_SETITEM, new IntPtr(columnIndex), ref hd);
    }

    /// <summary>
    /// Hides the sort indicator which might be showing on a column header
    /// </summary>
    /// <param name="list">ListView that needs a sort indicator removed</param>
    /// <param name="columnIndex">Column that might be showing a sort indicator</param>
    private static void HideSortIndicator(ListView list, int columnIndex) {
      HDITEM hd = new HDITEM();
      hd.mask = HDI_FORMAT;
      hd.fmt = HDF_STRING;
      try {
        switch(list.Columns[columnIndex].TextAlign) {
          case HorizontalAlignment.Right:
            hd.fmt |= HDF_RIGHT;
            break;
          case HorizontalAlignment.Center:
            hd.fmt |= HDF_CENTER;
            break;
          case HorizontalAlignment.Left:
            hd.fmt |= HDF_LEFT;
            break;
        }
      } catch { }
      IntPtr header = SendMessage(list.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
      SendMessage(header, HDM_SETITEM, new IntPtr(columnIndex), ref hd);
    }
    #endregion  // DetailSort

    #region GetOrderedColumns
    /// <summary>
    /// Gets an array of ColumnHeaders from a ListView in the order in which they are actually displayed.
    /// </summary>
    /// <param name="list">The ListView to get the ordered columns from</param>
    /// <returns>ColumnHeaders in the ListView, in display order</returns>
    public static ColumnHeader[] GetOrderedColumns(ListView list) {
      List<OrderedColumn> cols = new List<OrderedColumn>();
      foreach(ColumnHeader col in list.Columns) {
        LV_COLUMN pcol = new LV_COLUMN();
        pcol.mask = LVCF_ORDER;
        SendMessage(list.Handle, LVM_GETCOLUMN, new IntPtr(col.Index), ref pcol);
        cols.Add(new OrderedColumn(col, pcol.iOrder));
      }
      cols.Sort();
      ColumnHeader[] ret = new ColumnHeader[cols.Count];
      for(int i = 0; i < cols.Count; i++)
        ret[i] = cols[i].Column;
      return ret;
    }

    /// <summary>
    /// Simple class associating a sort order number with a ColumnHeader
    /// </summary>
    private class OrderedColumn : IComparable {
      private ColumnHeader _col;
      private int _order;

      /// <summary>
      /// Create a new OrderedColumn
      /// </summary>
      /// <param name="col">Column to include in the OrderedColumn</param>
      /// <param name="order">Order position of the ColumnHeader</param>
      public OrderedColumn(ColumnHeader col, int order) {
        _col = col;
        _order = order;
      }

      /// <summary>
      /// Gets the ColumnHeader contained in the OrderedColumn
      /// </summary>
      public ColumnHeader Column {
        get { return _col; }
      }

      /// <summary>
      /// Compares the OrderedColumn to another OrderedColumn
      /// </summary>
      /// <param name="obj">The OrderedColumn to compare this one to</param>
      /// <returns>Less than 0 if this comes before obj, greater than 0 if after, or 0 if same</returns>
      public int CompareTo(object obj) {
        if(obj is OrderedColumn)
          return CompareTo(obj as OrderedColumn);
        throw new ArgumentException("Unable to compare a OrderedColumn to an object of type " + obj.GetType(), "obj");
      }

      /// <summary>
      /// Compares the OrderedColumn to another OrderedColumn
      /// </summary>
      /// <param name="oc">The OrderedColumn to compare this one to</param>
      /// <returns>Less than 0 if this comes before obj, greater than 0 if after, or 0 if same</returns>
      public int CompareTo(OrderedColumn oc) {
        return _order - oc._order;
      }
    }
    #endregion  // GetOrderedColumns

    #region User32 Functions
    [DllImport("user32")]
    private static extern IntPtr SendMessage(IntPtr Handle, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32")]
    private static extern IntPtr SendMessage(IntPtr Handle, int msg, IntPtr wParam, ref HDITEM lParam);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr Handle, int msg, IntPtr wParam, ref LV_COLUMN lParam);
    #endregion  // User32 Functions
  }
}
