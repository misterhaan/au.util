using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace au.util.comctl {
  public delegate void FileChangedHandler(object sender);

  /// <summary>
  /// File selection control
  /// </summary>
  [ToolboxBitmap(typeof(FilenameBox), "Properties.Resources.browseFile.png")]
  public partial class FilenameBox : UserControl {

    #region Events
    /// <summary>
    /// Triggered whenever a different file is selected
    /// </summary>
    public event FileChangedHandler Changed;
    #endregion  // Events

    #region Data Members
    private string _basepath;
    private bool _stripbase;
    private bool _limitbase;
    #endregion  // Data Members

    #region Constructor
    public FilenameBox() {
      InitializeComponent();
    }
    #endregion  // Constructor

    #region Properties
    /// <summary>
    /// Currently selected file
    /// </summary>
    [Description("Currently selected file"), Category("FilenameBox")]
    public string Filename {
      get { return _txtFilename.Text; }
      set {
        if(string.IsNullOrEmpty(value)) {
          _txtFilename.Text = "";
          if(Changed != null)
            Changed(this);
        } else {
          if(_stripbase) {
            if(!File.Exists(Path.Combine(_basepath, value)))
              throw new FileNotFoundException(string.Format(au.util.comctl.Properties.Resources.FileNotFoundExceptionMessage, Path.Combine(_basepath, value)));
          } else {
            if(_limitbase && !value.StartsWith(_basepath))
              throw new PathMismatchException(_basepath, value);
            if(!File.Exists(value))
              throw new FileNotFoundException(string.Format(au.util.comctl.Properties.Resources.FileNotFoundExceptionMessage, value));
          }
          _txtFilename.Text = value;
          if(Changed != null)
            Changed(this);
        }
      }
    }

    /// <summary>
    /// Full path to currently selected file
    /// </summary>
    [Description("Full path to currently selected file"), Category("FilenameBox")]
    public string FileFullname {
      get {
        if(_stripbase)
          return Path.Combine(_basepath, _txtFilename.Text);
        return _txtFilename.Text;
      }
      set {
        if(string.IsNullOrEmpty(value)) {
          _txtFilename.Text = "";
          if(Changed != null)
            Changed(this);
        } else {
          if(_limitbase && !value.StartsWith(_basepath))
            throw new PathMismatchException(_basepath, value);
          if(!File.Exists(value))
            throw new FileNotFoundException(string.Format(Properties.Resources.FileNotFoundExceptionMessage, value));
          if(_stripbase)
            _txtFilename.Text = StripBase(value);
          else
            _txtFilename.Text = value;
          if(Changed != null)
            Changed(this);
        }
      }
    }

    /// <summary>
    /// Gets or sets an option that controls how automatic filename completion works
    /// </summary>
    [Description("Gets or sets an option that controls how automatic filename completion works"), Category("FilenameBox"), DefaultValue(AutoCompleteMode.None)]
    public AutoCompleteMode AutoCompleteMode {
      get { return _txtFilename.AutoCompleteMode; }
      set { _txtFilename.AutoCompleteSource = (_txtFilename.AutoCompleteMode = value) == AutoCompleteMode.None ? AutoCompleteSource.None : AutoCompleteSource.FileSystem; }
    }

    /// <summary>
    /// Starting point for file selection
    /// </summary>
    [Description("Starting point for file selection"), Category("FilenameBox")]
    public string BasePath {
      get { return _basepath; }
      set {
        _basepath = value;
        if(_basepath == null)
          _basepath = "";
        if(_basepath.Length > 0 && !_basepath.EndsWith(@"\"))
          _basepath += @"\";
      }
    }

    /// <summary>
    /// Whether the base path should be displayed to the user and returned with the Filename property
    /// </summary>
    [Description("Whether the base path should be displayed to the user and returned with the Filename property"), Category("FilenameBox")]
    public bool StripBasePath {
      get { return _stripbase; }
      set {
        if(_stripbase = value)
          _limitbase = true;
      }
    }

    /// <summary>
    /// Whether the selection should be limited to files within the base path
    /// </summary>
    [Description("Whether the selection should be limited to files within the base path"), Category("FilenameBox")]
    public bool LimitBase {
      get { return _limitbase; }
      set {
        if(!_stripbase)
          _limitbase = value;
      }
    }

    /// <summary>
    /// Gets or sets the current file name filter string, which determines the choices that appear in the "Files of type" box in the open file dialog
    /// </summary>
    [Description("Gets or sets the current file name filter string, which determines the choices that appear in the \"Files of type\" box in the open file dialog"), Category("FilenameBox")]
    public string Filter {
      get { return _dlgFileOpen.Filter; }
      set { _dlgFileOpen.Filter = value; }
    }

    /// <summary>
    /// Gets or sets the open file dialog box title
    /// </summary>
    [Description("Gets or sets the open file dialog box title"), Category("FilenameBox")]
    public string Title {
      get { return _dlgFileOpen.Title; }
      set { _dlgFileOpen.Title = value; }
    }

    /// <summary>
    /// Gets or sets whether the button will appear flat
    /// </summary>
    [Description("Gets or sets whether the button will appear flat"), Category("Appearance")]
    public bool FlatButton {
      get { return _btnBrowse.FlatStyle == FlatStyle.Flat; }
      set { _btnBrowse.FlatStyle = value ? _btnBrowse.FlatStyle = FlatStyle.Flat : _btnBrowse.FlatStyle = FlatStyle.Standard; }
    }

    /// <summary>
    /// Whether the selected file exists
    /// </summary>
    [Browsable(false)]
    public bool FileExists {
      get { return File.Exists(FileFullname); }
    }
    #endregion  // Properties

    #region Methods
    /// <summary>
    /// Strip the base path from a filename
    /// </summary>
    /// <param name="filename">Filename to strip base path from</param>
    /// <returns>Filename with base path removed</returns>
    private string StripBase(string filename) {
      filename = filename.Substring(_basepath.Length);
      while(filename.StartsWith(@"\"))  // strip off any backslashes at the beginning of the filename
        filename = filename.Substring(1);
      return filename;
    }
    #endregion  // Methods

    #region Event Handlers
    private void _btnBrowse_Click(object sender, EventArgs e) {
      _dlgFileOpen.InitialDirectory = _basepath;
      _dlgFileOpen.ShowDialog(this);
    }

    private void _dlgFileOpen_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {
      if(_limitbase && !_dlgFileOpen.FileName.StartsWith(_basepath))
        throw new PathMismatchException(_basepath, _dlgFileOpen.FileName);
      if(_stripbase)
        _txtFilename.Text = StripBase(_dlgFileOpen.FileName);
      else
        _txtFilename.Text = _dlgFileOpen.FileName;
      if(Changed != null)
        Changed(this);
    }

    private void _txtFilename_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
      string filename = _txtFilename.Text;
      if(filename.Length > 0) {
        if(_stripbase)
          filename = Path.Combine(_basepath, filename);
        else if(_limitbase && !_txtFilename.Text.StartsWith(_basepath)) {
          MessageBox.Show(this, string.Format(au.util.comctl.Properties.Resources.PathMismatchMessageBoxMessage, _basepath), au.util.comctl.Properties.Resources.FilenameBoxMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
          e.Cancel = true;
        }
        if(!e.Cancel && !File.Exists(filename)) {
          MessageBox.Show(this, string.Format(au.util.comctl.Properties.Resources.FileNotFoundMessageBoxMessage, filename), au.util.comctl.Properties.Resources.FilenameBoxMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
          e.Cancel = true;
        }
      }
      if(Changed != null)
        Changed(this);
    }

    private void FilenameBox_EnabledChanged(object sender, EventArgs e) {
      _txtFilename.Enabled = _btnBrowse.Enabled = Enabled;
    }
    #endregion  // Event Handlers

  }
}
