using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace au.comctl {
	/// <summary>
	/// Summary description for numberbox.
	/// </summary>
  [ToolboxBitmap(typeof(NumberBox), @"calc.ico")]
  public class NumberBox : UserControl 
  {

	  #region data members
	  private string _format;
	  private bool _hideZero;
    private bool _showCalc;
	  #endregion  // data members

	  #region controls
    private System.Windows.Forms.Button _btnCalc;
    private System.Windows.Forms.TextBox _txtNumber;
    private System.Windows.Forms.ToolTip _tip;
    private System.ComponentModel.IContainer components;
    #endregion  // controls

    #region events
    /// <summary>
    /// Triggered whenever the number has changed
    /// </summary>
    public event NumberChangedHandler Changed;
    #endregion  // events

		#region constructors
		public NumberBox() {
			InitializeComponent();  // This call is required by the Windows.Forms Form Designer.
		}
		#endregion  // constructors
		
		#region properties
		/// <summary>
		/// Gets or sets whether the calculator button is shown.
		/// </summary>
		public bool ShowCalcButton {
		  get {return _showCalc;}
		  set {
		    _showCalc = value;
        if(_showCalc) {
          _txtNumber.Width = Width - _btnCalc.Width - 4;
          _btnCalc.Visible = true;
		    } else {
          _txtNumber.Width = Width;
          _btnCalc.Visible = false;
        }
		  }
		}
		
		/// <summary>
		/// Gets or sets the format string used to format the number displayed.
		/// </summary>
		public string NumberFormat {
		  get {return _format;}
		  set {_format = value;}
		}
		
		/// <summary>
		/// Gets or sets whether zero values should be hidden.
		/// </summary>
		public bool HideZeroValues {
		  get {return _hideZero;}
		  set {
		    _hideZero = value;
		    if(_hideZero) {
		      if(Value == 0)
		        _txtNumber.Text = @"";
		    } else {
		      if(_txtNumber.Text.Length == 0)
		        _txtNumber.Text = string.Format(@"{0:" + _format + @"}", 0);
		    }
		  }
		}
		
		/// <summary>
		/// Gets or sets the numeric value of the control.
		/// </summary>
		public double Value {
		  get {
		    string numText = @"";
        char[] chars = _txtNumber.Text.ToCharArray();
        for(int i = 0; i < chars.Length; i++)
          if(chars[i] >= '0' && chars[i] <= '9' || chars[i] == '.' && numText.IndexOf('.') == -1 || chars[i] == '-' && numText.Length == 0)
            numText += chars[i];
        if(numText.Length > 0)
          return double.Parse(numText);
        else
          return 0;
      }
      set {
        if(value == 0 && _hideZero)
          _txtNumber.Text = @"";
        else
          _txtNumber.Text = string.Format(@"{0:" + _format + "}", value);
        if(Changed != null)
          Changed(this, new EventArgs());
      }
		}
		
		/// <summary>
		/// Indicates how the text should be aligned for edit controls.
		/// </summary>
		public HorizontalAlignment TextAlign {
		  get {return _txtNumber.TextAlign;}
		  set {_txtNumber.TextAlign = value;}
		}
		#endregion  // properties

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NumberBox));
      this._txtNumber = new System.Windows.Forms.TextBox();
      this._btnCalc = new System.Windows.Forms.Button();
      this._tip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // _txtNumber
      // 
      this._txtNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this._txtNumber.Location = new System.Drawing.Point(0, 0);
      this._txtNumber.Name = "_txtNumber";
      this._txtNumber.TabIndex = 0;
      this._txtNumber.Text = "";
      this._txtNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this._txtNumber.Leave += new System.EventHandler(this._txtNumber_Leave);
      // 
      // _btnCalc
      // 
      this._btnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this._btnCalc.Image = ((System.Drawing.Image)(resources.GetObject("_btnCalc.Image")));
      this._btnCalc.Location = new System.Drawing.Point(104, 0);
      this._btnCalc.Name = "_btnCalc";
      this._btnCalc.Size = new System.Drawing.Size(20, 20);
      this._btnCalc.TabIndex = 1;
      this._tip.SetToolTip(this._btnCalc, "Show calculator to enter value");
      this._btnCalc.Click += new System.EventHandler(this._btnCalc_Click);
      // 
      // NumberBox
      // 
      this.Controls.Add(this._btnCalc);
      this.Controls.Add(this._txtNumber);
      this.Name = "NumberBox";
      this.Size = new System.Drawing.Size(124, 20);
      this.Resize += new System.EventHandler(this.NumberBox_Resize);
      this.ResumeLayout(false);

    }
		#endregion
		#endregion  // generated

    #region event handlers
    private void NumberBox_Resize(object sender, System.EventArgs e) {
      _btnCalc.Width = _btnCalc.Height;
      if(_btnCalc.Visible)
        _txtNumber.Width = Width - _btnCalc.Width - 4;
    }

    private void _txtNumber_Leave(object sender, System.EventArgs e) {
      string numText = @"";
      double number;
      char[] chars = _txtNumber.Text.ToCharArray();
      for(int i = 0; i < chars.Length; i++)
        if(chars[i] >= '0' && chars[i] <= '9' || chars[i] == '.' && numText.IndexOf('.') == -1 || chars[i] == '-' && numText.Length == 0)
          numText += chars[i];
      if(numText.Length > 0)
        number = double.Parse(numText);
      else
        number = 0;
      if(number == 0 && _hideZero)
        _txtNumber.Text = @"";
      else
        _txtNumber.Text = string.Format(@"{0:" + _format + @"}", number);
      if(Changed != null)
        Changed(this, new EventArgs());
    }

    private void _btnCalc_Click(object sender, System.EventArgs e) {
      SimpleCalcForm sc = new SimpleCalcForm();
      sc.Value = Value;
      if(sc.ShowDialog(this) == DialogResult.OK) {
        Value = sc.Value;
        if(Changed != null)
          Changed(this, new EventArgs());
      }
    }
    #endregion  // event handlers
  }
  public delegate void NumberChangedHandler(object sender, EventArgs e);
}
