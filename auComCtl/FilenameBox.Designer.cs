namespace au.util.comctl
{
	partial class FilenameBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilenameBox));
      this._txtFilename = new System.Windows.Forms.TextBox();
      this._btnBrowse = new System.Windows.Forms.Button();
      this._dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this._tooltip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // _txtFilename
      // 
      resources.ApplyResources(this._txtFilename, "_txtFilename");
      this._txtFilename.Name = "_txtFilename";
      this._txtFilename.Validating += new System.ComponentModel.CancelEventHandler(this._txtFilename_Validating);
      // 
      // _btnBrowse
      // 
      resources.ApplyResources(this._btnBrowse, "_btnBrowse");
      this._btnBrowse.FlatAppearance.BorderSize = 0;
      this._btnBrowse.Image = global::au.util.comctl.Properties.Resources.browseFile;
      this._btnBrowse.Name = "_btnBrowse";
      this._tooltip.SetToolTip(this._btnBrowse, resources.GetString("_btnBrowse.ToolTip"));
      this._btnBrowse.UseVisualStyleBackColor = true;
      this._btnBrowse.Click += new System.EventHandler(this._btnBrowse_Click);
      // 
      // _dlgFileOpen
      // 
      this._dlgFileOpen.FileOk += new System.ComponentModel.CancelEventHandler(this._dlgFileOpen_FileOk);
      // 
      // FilenameBox
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._btnBrowse);
      this.Controls.Add(this._txtFilename);
      this.MaximumSize = new System.Drawing.Size(32767, 20);
      this.MinimumSize = new System.Drawing.Size(50, 20);
      this.Name = "FilenameBox";
      this.EnabledChanged += new System.EventHandler(this.FilenameBox_EnabledChanged);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _txtFilename;
		private System.Windows.Forms.Button _btnBrowse;
		private System.Windows.Forms.OpenFileDialog _dlgFileOpen;
		private System.Windows.Forms.ToolTip _tooltip;
	}
}
