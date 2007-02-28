using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace au.comctl {

	/// <summary>control which allows selection of a folder</summary>
  [ToolboxBitmap(typeof(FoldernameBox), @"browseFolder16x16x16.ico")]
  public class FoldernameBox : UserControl 
  {

		#region controls
		private TextBox txtfoldername;
		private Button browsebutton;
		private FolderBrowserDialog folderdialog;
		private System.Windows.Forms.ToolTip tooltip;
		private System.ComponentModel.IContainer components;  // Required designer variable.
		#endregion

		#region events
		/// <summary>
		/// Triggered whenever the selected folder has changed.
		/// </summary>
		public event FolderChangedHandler Changed;
		#endregion

		#region data members
		private string _basepath;
		private bool _stripbase;
		private bool _limitbase;
		#endregion

		#region constructors
		public FoldernameBox() {
			InitializeComponent();  // This call is required by the Windows.Forms Form Designer.
		}
		#endregion

		#region properties
      /// <summary>currently selected folder</summary>
      public string FolderName {
        get {return txtfoldername.Text;}
        set {
          if(value.Length == 0)
            txtfoldername.Text = @"";
          else {
            if(_stripbase) {
              if(!Directory.Exists(_basepath + value))
                throw new FolderNotFoundException();
            } else {
              if(_limitbase && !value.StartsWith(_basepath))
                throw new PathMismatchException();
              if(!Directory.Exists(value))
                throw new FolderNotFoundException();
            }
						txtfoldername.Text = value;
						if(!txtfoldername.Text.EndsWith(@"\"))
              txtfoldername.Text += @"\";
          }
          if(Changed != null)
						Changed(this, new EventArgs());
        }
      }

		  /// <summary>full path to currently selected folder</summary>
		  public string FolderFullname {
		    get {
          if(_stripbase)
            return _basepath + txtfoldername.Text;
          return txtfoldername.Text;
        }
        set {
          if(value.Length == 0)
            txtfoldername.Text = @"";
          else {
            if(_limitbase && !value.StartsWith(_basepath))
              throw new PathMismatchException();
            if(!Directory.Exists(value))
              throw new FolderNotFoundException();
            if(_stripbase)
              txtfoldername.Text = value.Substring(_basepath.Length);
            else
              txtfoldername.Text = value;
            if(!txtfoldername.Text.EndsWith(@"\"))
              txtfoldername.Text += @"\";
          }
					if(Changed != null)
						Changed(this, new EventArgs());
				}
		  }
		  /// <summary>starting point for folder selection</summary>
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

		  /// <summary>text to display above the browse dialog</summary>
		  public string Description {
				get {return folderdialog.Description;}
				set {folderdialog.Description = value;}
		  }

			/// <summary>true when the selected folder exists</summary>
			public bool FolderExists {
				get {return Directory.Exists(this.FolderFullname);}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FoldernameBox));
			this.txtfoldername = new System.Windows.Forms.TextBox();
			this.browsebutton = new System.Windows.Forms.Button();
			this.folderdialog = new System.Windows.Forms.FolderBrowserDialog();
			this.tooltip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// txtfoldername
			// 
			this.txtfoldername.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtfoldername.Location = new System.Drawing.Point(0, 0);
			this.txtfoldername.Name = "txtfoldername";
			this.txtfoldername.Size = new System.Drawing.Size(124, 20);
			this.txtfoldername.TabIndex = 1;
			this.txtfoldername.Text = "";
			this.txtfoldername.Validating += new System.ComponentModel.CancelEventHandler(this.txtfoldername_Validating);
			// 
			// browsebutton
			// 
			this.browsebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browsebutton.Image = ((System.Drawing.Image)(resources.GetObject("browsebutton.Image")));
			this.browsebutton.Location = new System.Drawing.Point(128, 0);
			this.browsebutton.Name = "browsebutton";
			this.browsebutton.Size = new System.Drawing.Size(20, 20);
			this.browsebutton.TabIndex = 2;
			this.tooltip.SetToolTip(this.browsebutton, "Browse for folder");
			this.browsebutton.Click += new System.EventHandler(this.browsebutton_Click);
			// 
			// foldernamebox
			// 
			this.Controls.Add(this.browsebutton);
			this.Controls.Add(this.txtfoldername);
			this.Name = "foldernamebox";
			this.Size = new System.Drawing.Size(148, 20);
			this.EnabledChanged += new System.EventHandler(this.foldernamebox_EnabledChanged);
			this.ResumeLayout(false);

		}
		#endregion
    #endregion // generated

		#region event handlers
		private void browsebutton_Click(object sender, System.EventArgs e) {
			if(txtfoldername.Text.Length > 0)
				if(_stripbase)
					folderdialog.SelectedPath = _basepath + txtfoldername.Text;
				else
					folderdialog.SelectedPath = txtfoldername.Text;
			else
				folderdialog.SelectedPath = _basepath;
			folderdialog.ShowDialog(this);
			if(folderdialog.SelectedPath != _basepath) {
				if(_stripbase)
					if(folderdialog.SelectedPath.StartsWith(_basepath))
						txtfoldername.Text = folderdialog.SelectedPath.Substring(_basepath.Length);
					else
						throw new PathMismatchException();
				else
				  txtfoldername.Text = folderdialog.SelectedPath;
				if(!txtfoldername.Text.EndsWith(@"\"))
					txtfoldername.Text += @"\";
				if(Changed != null)
					Changed(this, new EventArgs());
			}
		}

		private void txtfoldername_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			string foldername = txtfoldername.Text;
			if(foldername.Length > 0)  {
				if(_stripbase)
					foldername = _basepath + foldername;
				else if(_limitbase && !txtfoldername.Text.StartsWith(_basepath)) 
				{
					MessageBox.Show(this, "Folder must be contained in base path:\n" + _basepath, "Folder Selection");
					e.Cancel = true;
				}
				if(!e.Cancel && !Directory.Exists(foldername)) 
				{
					MessageBox.Show(this, "Folder specified does not exist:\n" + foldername, "Folder Selection");
					e.Cancel = true;
				}
				if(!e.Cancel && !foldername.EndsWith(@"\"))
					txtfoldername.Text += @"\";
			}
			if(Changed != null)
				Changed(this, new EventArgs());
		}

		private void foldernamebox_EnabledChanged(object sender, System.EventArgs e) {
			UserControl uc = this;
			txtfoldername.Enabled = browsebutton.Enabled = uc.Enabled;
		}
		#endregion

	}

	#region exceptions
	class PathMismatchException : ApplicationException {}
	class FolderNotFoundException : ApplicationException {}
	#endregion

  public delegate void FolderChangedHandler(object sender, EventArgs e);
}
