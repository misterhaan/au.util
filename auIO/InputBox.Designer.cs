namespace au.util.io {
  partial class InputBox {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if(disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this._lblPrompt = new System.Windows.Forms.Label();
      this._txtValue = new System.Windows.Forms.TextBox();
      this._btnOK = new System.Windows.Forms.Button();
      this._btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // _lblPrompt
      // 
      this._lblPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
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
      this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this._btnOK.Location = new System.Drawing.Point(216, 60);
      this._btnOK.Name = "_btnOK";
      this._btnOK.Size = new System.Drawing.Size(60, 20);
      this._btnOK.TabIndex = 2;
      this._btnOK.Text = "OK";
      this._btnOK.UseVisualStyleBackColor = true;
      // 
      // _btnCancel
      // 
      this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this._btnCancel.Location = new System.Drawing.Point(282, 60);
      this._btnCancel.Name = "_btnCancel";
      this._btnCancel.Size = new System.Drawing.Size(60, 20);
      this._btnCancel.TabIndex = 3;
      this._btnCancel.Text = "Cancel";
      this._btnCancel.UseVisualStyleBackColor = true;
      // 
      // InputBox
      // 
      this.AcceptButton = this._btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this._btnCancel;
      this.ClientSize = new System.Drawing.Size(346, 84);
      this.Controls.Add(this._btnCancel);
      this.Controls.Add(this._btnOK);
      this.Controls.Add(this._txtValue);
      this.Controls.Add(this._lblPrompt);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InputBox";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "InputBox";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label _lblPrompt;
    private System.Windows.Forms.TextBox _txtValue;
    private System.Windows.Forms.Button _btnOK;
    private System.Windows.Forms.Button _btnCancel;
  }
}