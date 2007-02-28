namespace au.util.comctl
{
	partial class NumberBox
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberBox));
      this._txtNumber = new System.Windows.Forms.TextBox();
      this._btnCalc = new System.Windows.Forms.Button();
      this._tip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // _txtNumber
      // 
      resources.ApplyResources(this._txtNumber, "_txtNumber");
      this._txtNumber.Name = "_txtNumber";
      this._txtNumber.Leave += new System.EventHandler(this._txtNumber_Leave);
      // 
      // _btnCalc
      // 
      resources.ApplyResources(this._btnCalc, "_btnCalc");
      this._btnCalc.Image = global::au.util.comctl.Properties.Resources.calc;
      this._btnCalc.Name = "_btnCalc";
      this._tip.SetToolTip(this._btnCalc, resources.GetString("_btnCalc.ToolTip"));
      this._btnCalc.UseVisualStyleBackColor = true;
      this._btnCalc.Click += new System.EventHandler(this._btnCalc_Click);
      // 
      // NumberBox
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.Controls.Add(this._btnCalc);
      this.Controls.Add(this._txtNumber);
      this.Name = "NumberBox";
      resources.ApplyResources(this, "$this");
      this.Resize += new System.EventHandler(this.NumberBox_Resize);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _txtNumber;
		private System.Windows.Forms.Button _btnCalc;
		private System.Windows.Forms.ToolTip _tip;
	}
}
