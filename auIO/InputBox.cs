using System;
using System.Windows.Forms;

namespace au.util.io {
  public partial class InputBox : Form {
    /// <summary>
    /// Creates a new InputBox to prompt for an input string
    /// </summary>
    /// <param name="caption">Caption to display as the window title</param>
    /// <param name="prompt">Prompt for input to display in the window</param>
    /// <param name="defaultValue">Default value</param>
    private InputBox(string caption, string prompt, string defaultValue) {
      InitializeComponent();
      Text = caption;
      _lblPrompt.Text = prompt;
      _txtValue.Text = defaultValue;
    }

    #region Methods
    /// <summary>
    /// Prompts the user for an input string
    /// </summary>
    /// <param name="owner">The window requesting the input string</param>
    /// <param name="prompt">Prompt to display in the input window</param>
    /// <param name="caption">Caption to use as the title of the input window</param>
    /// <param name="defaultValue">Default input value</param>
    /// <returns>User-input string</returns>
    public static string Read(IWin32Window owner, string prompt, string caption, string defaultValue) {
      InputBox ib = new InputBox(caption, prompt, defaultValue);
      ib.ShowDialog(owner);
      if(ib.DialogResult == DialogResult.OK)
        return ib._txtValue.Text;
      else
        return defaultValue;
    }

    public static string Read(IWin32Window owner, string prompt, string caption) {
      return Read(owner, prompt, caption, "");
    }
    #endregion  // Methods
  }
}