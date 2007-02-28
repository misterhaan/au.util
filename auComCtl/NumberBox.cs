using System;
using System.Drawing;  // ToolboxBitmap
using System.Windows.Forms;  // UserControl
using System.ComponentModel; // Description, Category

namespace au.util.comctl {
	public delegate void NumberChangedHandler(object sender);

	[ToolboxBitmap(typeof(NumberBox), "Properties.Resources.calc.png")]
	public partial class NumberBox : UserControl {
		#region Data Members
		private string _format;
		private bool _hideZero;
		private bool _showCalc;
		#endregion  // Data Members

		#region Events
		/// <summary>
		/// Triggered whenever the number has changed
		/// </summary>
		public event NumberChangedHandler Changed;
		#endregion  // Events

		#region Constructors
		public NumberBox() {
			InitializeComponent();
		}
		#endregion  // Constructors

		#region Properties
    /// <summary>
    /// Gets or sets whether the calculator button is shown
    /// </summary>
    [Description("Gets or sets whether the calculator button is shown"), Category("NumberBox")]
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
    /// Gets or sets the format string used to format the number displayed
    /// </summary>
    [Description("Gets or sets the format string used to format the number displayed"), Category("NumberBox")]
		public string NumberFormat {
		  get {return _format;}
		  set {_format = value;}
		}

    /// <summary>
    /// Gets or sets whether zero values should be hidden
    /// </summary>
    [Description("Gets or sets whether zero values should be hidden"), Category("NumberBox")]
		public bool HideZeroValues {
		  get {return _hideZero;}
		  set {
		    _hideZero = value;
		    if(_hideZero) {
		      if(Value == 0)
		        _txtNumber.Text = "";
		    } else {
		      if(_txtNumber.Text.Length == 0)
		        _txtNumber.Text = string.Format("{0:" + _format + "}", 0);
		    }
		  }
		}

    /// <summary>
    /// Gets or sets the numeric value of the control
    /// </summary>
    [Description("Gets or sets the numeric value of the control"), Category("NumberBox")]
		public double Value {
		  get {
		    string numText = "";
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
          _txtNumber.Text = "";
        else
          _txtNumber.Text = string.Format("{0:" + _format + "}", value);
        if(Changed != null)
          Changed(this);
      }
		}

    /// <summary>
    /// Indicates how the text should be aligned for edit controls
    /// </summary>
    [Description("Indicates how the text should be aligned for edit controls"), Category("NumberBox")]
		public HorizontalAlignment TextAlign {
		  get {return _txtNumber.TextAlign;}
		  set {_txtNumber.TextAlign = value;}
		}
		#endregion  // Properties

		#region Event Handlers
		private void NumberBox_Resize(object sender, EventArgs e) {
			_btnCalc.Width = _btnCalc.Height;
			if(_btnCalc.Visible)
				_txtNumber.Width = Width - _btnCalc.Width - 4;
		}

		private void _txtNumber_Leave(object sender, EventArgs e) {
			string numText = "";
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
				_txtNumber.Text = "";
			else
				_txtNumber.Text = string.Format("{0:" + _format + "}", number);
			if(Changed != null)
				Changed(this);
		}

		private void _btnCalc_Click(object sender, EventArgs e) {
      SimpleCalcForm sc = new SimpleCalcForm();
      sc.Value = Value;
      if(sc.ShowDialog(this) == DialogResult.OK) {
        Value = sc.Value;
        if(Changed != null)
          Changed(this);
      }
		}
		#endregion // Event Handlers
	}
}
