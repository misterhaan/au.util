using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace au.util.comctl {
  public delegate void FolderChangedHandler(object sender);

  /// <summary>
  /// Folder selection control
  /// </summary>
  [ToolboxBitmap(typeof(FoldernameBox), "Properties.Resources.browseFolder.png")]
  public partial class FoldernameBox : UserControl {

    #region Events
    /// <summary>
    /// Triggered whenever a different folder is selected
    /// </summary>
    public event FolderChangedHandler Changed;
    #endregion  // Events

    #region Data Members
    private string _basepath;
    private bool _stripbase;
    private bool _limitbase;
    private bool _mustExist = true;
    #endregion  // Data Members

    #region Constructor
    public FoldernameBox() {
      InitializeComponent();
    }
    #endregion  // Constructor

    #region Properties
    /// <summary>
    /// Currently selected folder
    /// </summary>
    [Description("Currently selected folder"), Category("FoldernameBox")]
    public string FolderName {
      get { return _txtFoldername.Text; }
      set {
        if(value.Length == 0)
          _txtFoldername.Text = "";
        else {
          if(_stripbase) {
            if(_mustExist && !Directory.Exists(Path.Combine(_basepath, value)))
              throw new FolderNotFoundException(Path.Combine(_basepath, value));
          } else {
            if(_limitbase && !value.StartsWith(_basepath))
              throw new PathMismatchException(_basepath, value);
            if(_mustExist && !Directory.Exists(value))
              throw new FolderNotFoundException(value);
          }
          _txtFoldername.Text = value;
          if(!_txtFoldername.Text.EndsWith(@"\"))
            _txtFoldername.Text += @"\";
        }
        if(Changed != null)
          Changed(this);
      }
    }

    /// <summary>
    /// Full path to the currently selected folder
    /// </summary>
    [Description("Full path to the currently selected folder"), Category("FoldernameBox")]
    public string FolderFullName {
      get {
        if(_stripbase)
          return Path.Combine(_basepath, _txtFoldername.Text);
        return _txtFoldername.Text;
      }
      set {
        if(value.Length == 0)
          _txtFoldername.Text = "";
        else {
          if(_limitbase && !value.StartsWith(_basepath))
            throw new PathMismatchException(_basepath, value);
          if(_mustExist && !Directory.Exists(value))
            throw new FolderNotFoundException(value);
          if(_stripbase)
            _txtFoldername.Text = FolderStripBase(value);
          else
            _txtFoldername.Text = value;
          if(!_txtFoldername.Text.EndsWith(@"\"))
            _txtFoldername.Text += @"\";
        }
        if(Changed != null)
          Changed(this);
      }
    }

    /// <summary>
    /// Gets or sets an option that controls how automatic path completion works
    /// </summary>
    [Description("Gets or sets an option that controls how automatic path completion works"), Category("FilenameBox"), DefaultValue(AutoCompleteMode.None)]
    public AutoCompleteMode AutoCompleteMode {
      get { return _txtFoldername.AutoCompleteMode; }
      set { _txtFoldername.AutoCompleteSource = (_txtFoldername.AutoCompleteMode = value) == AutoCompleteMode.None ? AutoCompleteSource.None : AutoCompleteSource.FileSystemDirectories; }
    }

    /// <summary>
    /// True if only existing folders can be selected
    /// </summary>
    [Description("True if only existing folders can be selected"), Category("FoldernameBox")]
    public bool FolderMustExist {
      get { return _mustExist; }
      set { _mustExist = value; }
    }

    /// <summary>
    /// Starting point for folder selection
    /// </summary>
    [Description("Starting point for folder selection"), Category("FoldernameBox")]
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
    /// 
    /// </summary>
    [Description("Whether the base path should be displayed to the user and returned with the foldername property"), Category("FoldernameBox")]
    public bool StripBase {
      get { return _stripbase; }
      set {
        _stripbase = value;
        if(_stripbase)
          _limitbase = true;
      }
    }

    /// <summary>
    /// Whether the selection should be limited to folders within the base path
    /// </summary>
    [Description("Whether the selection should be limited to folders within the base path"), Category("FoldernameBox")]
    public bool LimitBase {
      get { return _limitbase; }
      set {
        if(!_stripbase)
          _limitbase = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    [Description("Text to display above the browse dialog"), Category("FoldernameBox")]
    public string Description {
      get { return _dlgFolderBrowse.Description; }
      set { _dlgFolderBrowse.Description = value; }
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
    /// True when the selected folder exists
    /// </summary>
    [Browsable(false)]
    public bool FolderExists {
      get { return Directory.Exists(FolderFullName); }
    }
    #endregion  // Properties

    private string FolderStripBase(string path) {
      path = path.Substring(_basepath.Length);
      while(path.StartsWith(@"\"))
        path = path.Substring(1);
      return path;
    }

    #region Event Handlers
    private void _btnBrowse_Click(object sender, EventArgs e) {
      if(_txtFoldername.Text.Length > 0)
        if(_stripbase)
          _dlgFolderBrowse.SelectedPath = _basepath + _txtFoldername.Text;
        else
          _dlgFolderBrowse.SelectedPath = _txtFoldername.Text;
      else
        _dlgFolderBrowse.SelectedPath = _basepath;
      _dlgFolderBrowse.ShowDialog(this);
      if(_dlgFolderBrowse.SelectedPath != _basepath) {
        if(_stripbase)
          if(_dlgFolderBrowse.SelectedPath.StartsWith(_basepath))
            _txtFoldername.Text = _dlgFolderBrowse.SelectedPath.Substring(_basepath.Length);
          else
            throw new PathMismatchException(_basepath, _dlgFolderBrowse.SelectedPath);
        else
          _txtFoldername.Text = _dlgFolderBrowse.SelectedPath;
        if(!_txtFoldername.Text.EndsWith(@"\"))
          _txtFoldername.Text += @"\";
        if(Changed != null)
          Changed(this);
      }
    }

    private void _txtFoldername_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
      string foldername = _txtFoldername.Text;
      if(foldername.Length > 0) {
        if(_stripbase)
          foldername = _basepath + foldername;
        else if(_limitbase && !_txtFoldername.Text.StartsWith(_basepath)) {
          MessageBox.Show(this, string.Format(Properties.Resources.PathMismatchMessageBoxMessage, BasePath), Properties.Resources.FoldernameBoxMessageBoxCaption);
          e.Cancel = true;
        }
        if(!e.Cancel && _mustExist && !Directory.Exists(foldername)) {
          MessageBox.Show(this, string.Format(Properties.Resources.FolderNotFoundMessageBoxMessage, foldername), Properties.Resources.FoldernameBoxMessageBoxCaption);
          e.Cancel = true;
        }
        if(!e.Cancel && !foldername.EndsWith(@"\"))
          _txtFoldername.Text += @"\";
      }
      if(Changed != null)
        Changed(this);
    }

    private void FoldernameBox_EnabledChanged(object sender, EventArgs e) {
      _txtFoldername.Enabled = _btnBrowse.Enabled = Enabled;
    }
    #endregion  // event handlers
  }
}
