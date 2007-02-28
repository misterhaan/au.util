namespace au.util.comctl
{
	partial class FoldernameBox
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FoldernameBox));
      this._txtFoldername = new System.Windows.Forms.TextBox();
      this._btnBrowse = new System.Windows.Forms.Button();
      this._dlgFolderBrowse = new System.Windows.Forms.FolderBrowserDialog();
      this._tooltip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // _txtFoldername
      // 
      resources.ApplyResources(this._txtFoldername, "_txtFoldername");
      this._txtFoldername.Name = "_txtFoldername";
      this._txtFoldername.Validating += new System.ComponentModel.CancelEventHandler(this._txtFoldername_Validating);
      // 
      // _btnBrowse
      // 
      resources.ApplyResources(this._btnBrowse, "_btnBrowse");
      this._btnBrowse.Image = global::au.util.comctl.Properties.Resources.browseFolder;
      this._btnBrowse.Name = "_btnBrowse";
      this._tooltip.SetToolTip(this._btnBrowse, resources.GetString("_btnBrowse.ToolTip"));
      this._btnBrowse.UseVisualStyleBackColor = true;
      this._btnBrowse.Click += new System.EventHandler(this._btnBrowse_Click);
      // 
      // FoldernameBox
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._btnBrowse);
      this.Controls.Add(this._txtFoldername);
      this.MaximumSize = new System.Drawing.Size(32767, 20);
      this.MinimumSize = new System.Drawing.Size(50, 20);
      this.Name = "FoldernameBox";
      this.EnabledChanged += new System.EventHandler(this.FoldernameBox_EnabledChanged);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _txtFoldername;
		private System.Windows.Forms.Button _btnBrowse;
		private System.Windows.Forms.FolderBrowserDialog _dlgFolderBrowse;
		private System.Windows.Forms.ToolTip _tooltip;
	}
}
