using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace au.io {
	public class InputBox : Form {
    private System.Windows.Forms.Label _lblPrompt;
    private System.Windows.Forms.TextBox _txtValue;
    private System.Windows.Forms.Button _btnOK;
    private System.Windows.Forms.Button _btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InputBox(string caption, string prompt, string defaultValue) {
			InitializeComponent();  // Required for Windows Form Designer support
			Text = caption;
			_lblPrompt.Text = prompt;
			_txtValue.Text = defaultValue;
		}

		#region generated
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this._lblPrompt = new System.Windows.Forms.Label();
      this._txtValue = new System.Windows.Forms.TextBox();
      this._btnOK = new System.Windows.Forms.Button();
      this._btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // _lblPrompt
      // 
      this._lblPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this._lblPrompt.Location = new System.Drawing.Point(6, 6);
      this._lblPrompt.Name = "_lblPrompt";
      this._lblPrompt.Size = new System.Drawing.Size(338, 54);
      this._lblPrompt.TabIndex = 0;
      this._lblPrompt.Text = "_lblPrompt";
      // 
      // _txtValue
      // 
      this._txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this._txtValue.Location = new System.Drawing.Point(6, 60);
      this._txtValue.Name = "_txtValue";
      this._txtValue.Size = new System.Drawing.Size(204, 20);
      this._txtValue.TabIndex = 1;
      this._txtValue.Text = "_txtValue";
      // 
      // _btnOK
      // 
      this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnOK.Location = new System.Drawing.Point(216, 60);
      this._btnOK.Name = "_btnOK";
      this._btnOK.Size = new System.Drawing.Size(60, 20);
      this._btnOK.TabIndex = 2;
      this._btnOK.Text = "OK";
      this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
      // 
      // _btnCancel
      // 
      this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnCancel.Location = new System.Drawing.Point(282, 60);
      this._btnCancel.Name = "_btnCancel";
      this._btnCancel.Size = new System.Drawing.Size(60, 20);
      this._btnCancel.TabIndex = 3;
      this._btnCancel.Text = "Cancel";
      this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
      // 
      // InputBox
      // 
      this.AcceptButton = this._btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this._btnCancel;
      this.ClientSize = new System.Drawing.Size(348, 86);
      this.Controls.Add(this._btnCancel);
      this.Controls.Add(this._btnOK);
      this.Controls.Add(this._txtValue);
      this.Controls.Add(this._lblPrompt);
      this.DockPadding.Left = 6;
      this.DockPadding.Right = 6;
      this.DockPadding.Top = 6;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InputBox";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "inputbox";
      this.ResumeLayout(false);

    }
		#endregion
		#endregion  // generated
		
		#region methods
		public static string Read(IWin32Window owner, string prompt, string caption, string defaultValue) {
		  InputBox ib = new InputBox(caption, prompt, defaultValue);
		  ib.ShowDialog(owner);
		  if(ib.DialogResult == DialogResult.OK)
		    return ib._txtValue.Text;
		  else
		    return defaultValue;
		}
		
		public static string Read(IWin32Window owner, string prompt, string caption) {
		  return Read(owner, prompt, caption, @"");
		}
		#endregion  // methods

    #region event handlers
    private void _btnOK_Click(object sender, System.EventArgs e) {
      DialogResult = DialogResult.OK;
    }

    private void _btnCancel_Click(object sender, System.EventArgs e) {
      DialogResult = DialogResult.Cancel;
    }
    #endregion  // event handlers
	}
}
