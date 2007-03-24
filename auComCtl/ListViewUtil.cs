using System;
using System.Windows.Forms;  // ListView, ColumnHeader
using System.Runtime.InteropServices;  // StructLayout, MarshalAs, DllImport

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
    #endregion  // External Data Structures

    #region External Constants
    private const int LVM_GETHEADER = 0x101f;
    private const int HDI_FORMAT = 4;
    private const int HDF_LEFT = 0;
    private const int HDF_RIGHT = 1;
    private const int HDF_CENTER = 2;
    private const int HDF_SORTDOWN = 0x200;
    private const int HDF_SORTUP = 0x400;
    private const int HDF_BITMAP_ON_RIGHT = 0x1000;
    private const int HDF_STRING = 0x4000;
    private const int HDM_SETITEM = 0x120c;
    #endregion  // External Constants

    #region DetailSort
    public static void DetailSort(ListView list, ColumnHeader column) { DetailSort(list, column.Index, SortType.String); }
    public static void DetailSort(ListView list, int columnIndex) { DetailSort(list, columnIndex, SortType.String); }
    public static void DetailSort(ListView list, ColumnHeader column, SortType sort) { DetailSort(list, column.Index, sort); }
    /// <summary>
    /// Sorts the items in a ListView in detail view by one of the columns
    /// </summary>
    /// <param name="list">ListView being sorted</param>
    /// <param name="columnIndex">Column to sort by</param>
    /// <param name="sort">How to sort the data in the column</param>
    public static void DetailSort(ListView list, int columnIndex, SortType sort) {
      if(list.View != View.Details)
        throw new ArgumentException(Properties.Resources.DetailsViewRequiredMessage, "list");
      ListViewItemSorter sorter = list.ListViewItemSorter as ListViewItemSorter;
      if(sorter == null) {
        sorter = new ListViewItemSorter(columnIndex, sort);
        list.ListViewItemSorter = sorter;
      } else {
        if(sorter.Column == columnIndex)
          sorter.ReverseOrder = !sorter.ReverseOrder;
        else {
          HideSortIndicator(list, sorter.Column);
          sorter.Column = columnIndex;
          sorter.ReverseOrder = false;
        }
      }
      ShowSortIndicator(list, columnIndex, sorter.ReverseOrder);
      list.Sort();
    }

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

    #region User32 Functions
    [DllImport("user32")]
    static extern IntPtr SendMessage(IntPtr Handle, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32")]
    static extern IntPtr SendMessage(IntPtr Handle, int msg, IntPtr wParam, ref HDITEM lParam);
    #endregion  // User32 Functions
  }
}
