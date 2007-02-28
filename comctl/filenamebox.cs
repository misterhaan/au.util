using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace au.comctl {

  /// <summary>control which allows selection of a file</summary>
  [ToolboxBitmap(typeof(FilenameBox), @"browseFile.ico")]
  public class FilenameBox : UserControl {
    
    #region controls
    private System.Windows.Forms.TextBox txtfilename;
    private System.Windows.Forms.Button browsebutton;
    private System.Windows.Forms.OpenFileDialog opendialog;
    private System.Windows.Forms.ToolTip tooltip;
    private System.ComponentModel.IContainer components;  // Required designer variable.
    #endregion

    #region events
    public event FileChangedHandler Changed;
    #endregion

    #region data members
    private string _basepath;
    private bool _stripbase;
    private bool _limitbase;
    #endregion

    #region constructors
    public FilenameBox() {
      InitializeComponent();  // This call is required by the Windows.Forms Form Designer.
    }
    #endregion

    #region properties
		/// <summary>currently selected file</summary>
		public string Filename {
			get {return txtfilename.Text;}
			set {
				if(value.Length == 0) {
					txtfilename.Text = @"";
          if(Changed != null)
            Changed(this, new EventArgs());
				} else {
					if(_stripbase) {
						if(!File.Exists(_basepath + value))
							throw new FileNotFoundException();
					} else {
						if(_limitbase && !value.StartsWith(_basepath))
							throw new PathMismatchException();
						if(!File.Exists(value))
							throw new FileNotFoundException();
					}
					txtfilename.Text = value;
          if(Changed != null)
            Changed(this, new EventArgs());
        }
			}
		}

		/// <summary>full path to currently selected file</summary>
		public string FileFullname {
			get {
				if(_stripbase)
					return _basepath + txtfilename.Text;
				return txtfilename.Text;
			}
			set {
				if(value.Length == 0) {
					txtfilename.Text = @"";
          if(Changed != null)
            Changed(this, new EventArgs());
				} else {
					if(_limitbase && !value.StartsWith(_basepath))
						throw new PathMismatchException();
					if(!File.Exists(value))
						throw new FileNotFoundException();
					if(_stripbase)
						txtfilename.Text = value.Substring(_basepath.Length);
					else
						txtfilename.Text = value;
          if(Changed != null)
            Changed(this, new EventArgs());
        }
			}
		}

		/// <summary>starting point for file selection</summary>
		public string BasePath {
			get {return _basepath;}
			set {
				_basepath = value;
				if(_basepath == null)
					_basepath = @"";
				if(_basepath.Length > 0 && !_basepath.EndsWith(@"\"))
					_basepath += @"\";
			}
		}

		/// <summary>whether the base path should be displayed to the user and returned with the foldername property</summary>
		public bool StripBase {
			get {return _stripbase;}
			set {
				_stripbase = value;
				if(_stripbase)
					_limitbase = true;
			}
		}

		/// <summary>whether the selection should be limited to folders within the base path</summary>
		public bool LimitBase {
			get {return _limitbase;}
			set {
				if(!_stripbase)
					_limitbase = value;
			}
		}

		/// <summary>
		/// Filters to use in the file open dialog.
		/// </summary>
		public string Filter {
			get {return opendialog.Filter;}
			set {opendialog.Filter = value;}
		}

		/// <summary>title to display on the file open dialog</summary>
		public string Title {
			get {return opendialog.Title;}
			set {opendialog.Title = value;}
		}

		/// <summary>true when the selected file exists</summary>
		public bool FileExists {
			get {return File.Exists(this.FileFullname);}
		}
		#endregion

    #region generated
    /// <summary>Clean up any resources being used.</summary>
		protected override void Dispose(bool disposing) 
    {
			if(disposing) {
				if(components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

    #region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FilenameBox));
			this.txtfilename = new System.Windows.Forms.TextBox();
			this.browsebutton = new System.Windows.Forms.Button();
			this.opendialog = new System.Windows.Forms.OpenFileDialog();
			this.tooltip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// txtfilename
			// 
			this.txtfilename.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtfilename.Location = new System.Drawing.Point(0, 0);
			this.txtfilename.Name = "txtfilename";
			this.txtfilename.Size = new System.Drawing.Size(124, 20);
			this.txtfilename.TabIndex = 0;
			this.txtfilename.Text = "";
			this.txtfilename.Validating += new System.ComponentModel.CancelEventHandler(this.txtfilename_Validating);
			// 
			// browsebutton
			// 
			this.browsebutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.browsebutton.Image = ((System.Drawing.Image)(resources.GetObject("browsebutton.Image")));
			this.browsebutton.Location = new System.Drawing.Point(128, 0);
			this.browsebutton.Name = "browsebutton";
			this.browsebutton.Size = new System.Drawing.Size(20, 20);
			this.browsebutton.TabIndex = 1;
			this.tooltip.SetToolTip(this.browsebutton, "Browse for file");
			this.browsebutton.Click += new System.EventHandler(this.browsebutton_Click);
			// 
			// opendialog
			// 
			this.opendialog.FileOk += new System.ComponentModel.CancelEventHandler(this.opendialog_FileOk);
			// 
			// filenamebox
			// 
			this.Controls.Add(this.browsebutton);
			this.Controls.Add(this.txtfilename);
			this.Name = "filenamebox";
			this.Size = new System.Drawing.Size(148, 20);
			this.EnabledChanged += new System.EventHandler(this.filenamebox_EnabledChanged);
			this.ResumeLayout(false);

		}
		#endregion
    #endregion // generated

    #region event handlers
		private void browsebutton_Click(object sender, System.EventArgs e) 
    {
			opendialog.InitialDirectory = _basepath;
			opendialog.ShowDialog(this);
		}

		private void opendialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {
			if(_limitbase && !opendialog.FileName.StartsWith(_basepath))
				throw new PathMismatchException();
			if(_stripbase)
				txtfilename.Text = opendialog.FileName.Substring(_basepath.Length);
			else
				txtfilename.Text = opendialog.FileName;
      if(Changed != null)
        Changed(this, new EventArgs());
		}

		private void txtfilename_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			string filename = txtfilename.Text;
			if(filename.Length > 0) {
				if(_stripbase)
					filename = _basepath + filename;
				else if(_limitbase && !txtfilename.Text.StartsWith(_basepath)) {
					MessageBox.Show(this, "File must be contained in base path:\n" + _basepath, "File Selection");
					e.Cancel = true;
				}
				if(!e.Cancel && !File.Exists(filename)) {
					MessageBox.Show(this, "File specified does not exist:\n" + filename, "File Selection");
					e.Cancel = true;
				}
			}
			if(Changed != null)
				Changed(this, new EventArgs());
		}

		private void filenamebox_EnabledChanged(object sender, System.EventArgs e) {
			UserControl uc = this;
			txtfilename.Enabled = browsebutton.Enabled = uc.Enabled;
		}
		#endregion

  }

  public delegate void FileChangedHandler(object sender, EventArgs e);
}
