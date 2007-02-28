using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace au.comctl {
	/// <summary>
	/// Summary description for simplecalc.
	/// </summary>
	internal class SimpleCalcForm : Form {

	  private const string NUM_FORMAT = @"{0:#,##0.######}";

	  private enum Operator {
	    Add,
	    Subtract,
	    Multiply,
	    Divide
	  }

    #region data members
    private double _total;
    private Operator _op;
    #endregion  // data members

    #region controls
    private System.Windows.Forms.TextBox _txtNumber;
    private System.Windows.Forms.Button _btn1;
    private System.Windows.Forms.Button _btn2;
    private System.Windows.Forms.Button _btn3;
    private System.Windows.Forms.Button _btn4;
    private System.Windows.Forms.Button _btn5;
    private System.Windows.Forms.Button _btn6;
    private System.Windows.Forms.Button _btn7;
    private System.Windows.Forms.Button _btn8;
    private System.Windows.Forms.Button _btn9;
    private System.Windows.Forms.Button _btn0;
    private System.Windows.Forms.Button _btnDecimal;
    private System.Windows.Forms.Button _btnEquals;
    private System.Windows.Forms.Button _btnAdd;
    private System.Windows.Forms.Button _btnSubtract;
    private System.Windows.Forms.Button _btnMultiply;
    private System.Windows.Forms.Button _btnDivide;
    private System.Windows.Forms.Button _btnClear;
    private System.Windows.Forms.Button _btnOK;
    private System.Windows.Forms.Button _btnCancel;
    private System.Windows.Forms.PictureBox _picCalc;
    #endregion  // controls

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region constructors
		public SimpleCalcForm() {
			InitializeComponent();  // Required for Windows Form Designer support
			_total = 0;
			_op = Operator.Add;
		}
		#endregion  // construction

    #region properties
    public double Value {
      get {return double.Parse(_txtNumber.Text);}
      set {_txtNumber.Text = string.Format(NUM_FORMAT, value);}
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SimpleCalcForm));
      this._txtNumber = new System.Windows.Forms.TextBox();
      this._btn1 = new System.Windows.Forms.Button();
      this._btn2 = new System.Windows.Forms.Button();
      this._btn3 = new System.Windows.Forms.Button();
      this._btn4 = new System.Windows.Forms.Button();
      this._btn5 = new System.Windows.Forms.Button();
      this._btn6 = new System.Windows.Forms.Button();
      this._btn7 = new System.Windows.Forms.Button();
      this._btn8 = new System.Windows.Forms.Button();
      this._btn9 = new System.Windows.Forms.Button();
      this._btn0 = new System.Windows.Forms.Button();
      this._btnDecimal = new System.Windows.Forms.Button();
      this._btnEquals = new System.Windows.Forms.Button();
      this._btnAdd = new System.Windows.Forms.Button();
      this._btnSubtract = new System.Windows.Forms.Button();
      this._btnMultiply = new System.Windows.Forms.Button();
      this._btnDivide = new System.Windows.Forms.Button();
      this._btnClear = new System.Windows.Forms.Button();
      this._btnOK = new System.Windows.Forms.Button();
      this._btnCancel = new System.Windows.Forms.Button();
      this._picCalc = new System.Windows.Forms.PictureBox();
      this.SuspendLayout();
      // 
      // _txtNumber
      // 
      this._txtNumber.Location = new System.Drawing.Point(6, 6);
      this._txtNumber.Name = "_txtNumber";
      this._txtNumber.Size = new System.Drawing.Size(180, 20);
      this._txtNumber.TabIndex = 0;
      this._txtNumber.TabStop = false;
      this._txtNumber.Text = "";
      this._txtNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // _btn1
      // 
      this._btn1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn1.Location = new System.Drawing.Point(6, 126);
      this._btn1.Name = "_btn1";
      this._btn1.Size = new System.Drawing.Size(23, 23);
      this._btn1.TabIndex = 0;
      this._btn1.TabStop = false;
      this._btn1.Text = "1";
      this._btn1.Click += new System.EventHandler(this._btn1_Click);
      // 
      // _btn2
      // 
      this._btn2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn2.Location = new System.Drawing.Point(36, 126);
      this._btn2.Name = "_btn2";
      this._btn2.Size = new System.Drawing.Size(23, 23);
      this._btn2.TabIndex = 0;
      this._btn2.TabStop = false;
      this._btn2.Text = "2";
      this._btn2.Click += new System.EventHandler(this._btn2_Click);
      // 
      // _btn3
      // 
      this._btn3.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn3.Location = new System.Drawing.Point(66, 126);
      this._btn3.Name = "_btn3";
      this._btn3.Size = new System.Drawing.Size(23, 23);
      this._btn3.TabIndex = 0;
      this._btn3.TabStop = false;
      this._btn3.Text = "3";
      this._btn3.Click += new System.EventHandler(this._btn3_Click);
      // 
      // _btn4
      // 
      this._btn4.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn4.Location = new System.Drawing.Point(6, 96);
      this._btn4.Name = "_btn4";
      this._btn4.Size = new System.Drawing.Size(23, 23);
      this._btn4.TabIndex = 0;
      this._btn4.TabStop = false;
      this._btn4.Text = "4";
      this._btn4.Click += new System.EventHandler(this._btn4_Click);
      // 
      // _btn5
      // 
      this._btn5.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn5.Location = new System.Drawing.Point(36, 96);
      this._btn5.Name = "_btn5";
      this._btn5.Size = new System.Drawing.Size(23, 23);
      this._btn5.TabIndex = 0;
      this._btn5.TabStop = false;
      this._btn5.Text = "5";
      this._btn5.Click += new System.EventHandler(this._btn5_Click);
      // 
      // _btn6
      // 
      this._btn6.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn6.Location = new System.Drawing.Point(66, 96);
      this._btn6.Name = "_btn6";
      this._btn6.Size = new System.Drawing.Size(23, 23);
      this._btn6.TabIndex = 0;
      this._btn6.TabStop = false;
      this._btn6.Text = "6";
      this._btn6.Click += new System.EventHandler(this._btn6_Click);
      // 
      // _btn7
      // 
      this._btn7.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn7.Location = new System.Drawing.Point(6, 66);
      this._btn7.Name = "_btn7";
      this._btn7.Size = new System.Drawing.Size(23, 23);
      this._btn7.TabIndex = 0;
      this._btn7.TabStop = false;
      this._btn7.Text = "7";
      this._btn7.Click += new System.EventHandler(this._btn7_Click);
      // 
      // _btn8
      // 
      this._btn8.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn8.Location = new System.Drawing.Point(36, 66);
      this._btn8.Name = "_btn8";
      this._btn8.Size = new System.Drawing.Size(23, 23);
      this._btn8.TabIndex = 0;
      this._btn8.TabStop = false;
      this._btn8.Text = "8";
      this._btn8.Click += new System.EventHandler(this._btn8_Click);
      // 
      // _btn9
      // 
      this._btn9.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn9.Location = new System.Drawing.Point(66, 66);
      this._btn9.Name = "_btn9";
      this._btn9.Size = new System.Drawing.Size(23, 23);
      this._btn9.TabIndex = 0;
      this._btn9.TabStop = false;
      this._btn9.Text = "9";
      this._btn9.Click += new System.EventHandler(this._btn9_Click);
      // 
      // _btn0
      // 
      this._btn0.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btn0.Location = new System.Drawing.Point(6, 156);
      this._btn0.Name = "_btn0";
      this._btn0.Size = new System.Drawing.Size(53, 23);
      this._btn0.TabIndex = 0;
      this._btn0.TabStop = false;
      this._btn0.Text = "0";
      this._btn0.Click += new System.EventHandler(this._btn0_Click);
      // 
      // _btnDecimal
      // 
      this._btnDecimal.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnDecimal.Location = new System.Drawing.Point(66, 156);
      this._btnDecimal.Name = "_btnDecimal";
      this._btnDecimal.Size = new System.Drawing.Size(23, 23);
      this._btnDecimal.TabIndex = 0;
      this._btnDecimal.TabStop = false;
      this._btnDecimal.Text = ".";
      this._btnDecimal.Click += new System.EventHandler(this._btnDecimal_Click);
      // 
      // _btnEquals
      // 
      this._btnEquals.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnEquals.Location = new System.Drawing.Point(96, 126);
      this._btnEquals.Name = "_btnEquals";
      this._btnEquals.Size = new System.Drawing.Size(23, 53);
      this._btnEquals.TabIndex = 0;
      this._btnEquals.TabStop = false;
      this._btnEquals.Text = "=";
      this._btnEquals.Click += new System.EventHandler(this._btnEquals_Click);
      // 
      // _btnAdd
      // 
      this._btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnAdd.Location = new System.Drawing.Point(96, 66);
      this._btnAdd.Name = "_btnAdd";
      this._btnAdd.Size = new System.Drawing.Size(23, 53);
      this._btnAdd.TabIndex = 0;
      this._btnAdd.TabStop = false;
      this._btnAdd.Text = "+";
      this._btnAdd.Click += new System.EventHandler(this._btnAdd_Click);
      // 
      // _btnSubtract
      // 
      this._btnSubtract.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnSubtract.Location = new System.Drawing.Point(96, 36);
      this._btnSubtract.Name = "_btnSubtract";
      this._btnSubtract.Size = new System.Drawing.Size(23, 23);
      this._btnSubtract.TabIndex = 0;
      this._btnSubtract.TabStop = false;
      this._btnSubtract.Text = "-";
      this._btnSubtract.Click += new System.EventHandler(this._btnSubtract_Click);
      // 
      // _btnMultiply
      // 
      this._btnMultiply.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnMultiply.Location = new System.Drawing.Point(66, 36);
      this._btnMultiply.Name = "_btnMultiply";
      this._btnMultiply.Size = new System.Drawing.Size(23, 23);
      this._btnMultiply.TabIndex = 0;
      this._btnMultiply.TabStop = false;
      this._btnMultiply.Text = "×";
      this._btnMultiply.Click += new System.EventHandler(this._btnMultiply_Click);
      // 
      // _btnDivide
      // 
      this._btnDivide.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnDivide.Location = new System.Drawing.Point(36, 36);
      this._btnDivide.Name = "_btnDivide";
      this._btnDivide.Size = new System.Drawing.Size(23, 23);
      this._btnDivide.TabIndex = 0;
      this._btnDivide.TabStop = false;
      this._btnDivide.Text = "÷";
      this._btnDivide.Click += new System.EventHandler(this._btnDivide_Click);
      // 
      // _btnClear
      // 
      this._btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this._btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnClear.Location = new System.Drawing.Point(6, 36);
      this._btnClear.Name = "_btnClear";
      this._btnClear.Size = new System.Drawing.Size(23, 23);
      this._btnClear.TabIndex = 0;
      this._btnClear.TabStop = false;
      this._btnClear.Text = "C";
      this._btnClear.Click += new System.EventHandler(this._btnClear_Click);
      // 
      // _btnOK
      // 
      this._btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnOK.Location = new System.Drawing.Point(132, 42);
      this._btnOK.Name = "_btnOK";
      this._btnOK.Size = new System.Drawing.Size(54, 23);
      this._btnOK.TabIndex = 1;
      this._btnOK.Text = "OK";
      this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
      // 
      // _btnCancel
      // 
      this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this._btnCancel.Location = new System.Drawing.Point(132, 72);
      this._btnCancel.Name = "_btnCancel";
      this._btnCancel.Size = new System.Drawing.Size(54, 23);
      this._btnCancel.TabIndex = 2;
      this._btnCancel.Text = "Cancel";
      this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
      // 
      // _picCalc
      // 
      this._picCalc.Image = ((System.Drawing.Image)(resources.GetObject("_picCalc.Image")));
      this._picCalc.Location = new System.Drawing.Point(132, 121);
      this._picCalc.Name = "_picCalc";
      this._picCalc.Size = new System.Drawing.Size(54, 58);
      this._picCalc.TabIndex = 20;
      this._picCalc.TabStop = false;
      // 
      // SimpleCalcForm
      // 
      this.AcceptButton = this._btnEquals;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(192, 188);
      this.Controls.Add(this._picCalc);
      this.Controls.Add(this._btnCancel);
      this.Controls.Add(this._btnOK);
      this.Controls.Add(this._btnClear);
      this.Controls.Add(this._btnDivide);
      this.Controls.Add(this._btnMultiply);
      this.Controls.Add(this._btnSubtract);
      this.Controls.Add(this._btnAdd);
      this.Controls.Add(this._btnEquals);
      this.Controls.Add(this._btnDecimal);
      this.Controls.Add(this._btn0);
      this.Controls.Add(this._btn9);
      this.Controls.Add(this._btn8);
      this.Controls.Add(this._btn7);
      this.Controls.Add(this._btn6);
      this.Controls.Add(this._btn5);
      this.Controls.Add(this._btn4);
      this.Controls.Add(this._btn3);
      this.Controls.Add(this._btn2);
      this.Controls.Add(this._btn1);
      this.Controls.Add(this._txtNumber);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SimpleCalcForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Number Entry";
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SimpleCalcForm_KeyPress);
      this.ResumeLayout(false);

    }
		#endregion
    #endregion  // generated
   
    #region methods
    /// <summary>
    /// Clear the current display value or reset the calculator to zero.
    /// </summary>
    private void Clear() {
      if(_txtNumber.Text == @"0") {
        _total = 0;
        _op = Operator.Add;
      } else
        _txtNumber.Text = @"0";
    }

    /// <summary>
    /// Perform queued operation.
    /// </summary>
    private void Operate() {
      switch(_op) {
        case Operator.Add:
          _total += double.Parse(_txtNumber.Text);
          break;
        case Operator.Subtract:
          _total -= double.Parse(_txtNumber.Text);
          break;
        case Operator.Multiply:
          _total *= double.Parse(_txtNumber.Text);
          break;
        case Operator.Divide:
          _total /= double.Parse(_txtNumber.Text);
          break;
      }
    }

    /// <summary>
    /// Perform queued operation and queue the next operation.
    /// </summary>
    /// <param name="op">Next operation</param>
    private void Operate(Operator op) {
      Operate();
      _txtNumber.Text = @"0";
      _op = op;
    }

    /// <summary>
    /// Append a digit to the currently displayed value
    /// </summary>
    /// <param name="d">The digit to append</param>
    private void Digit(int d) {
      if(_txtNumber.Text == @"0")
        _txtNumber.Text = d.ToString();
      else if(_txtNumber.Text.IndexOf('.') > -1)
        _txtNumber.Text += d.ToString();
      else {
        _txtNumber.Text = string.Format(NUM_FORMAT, Value * 10 + d);
      }
    }

    /// <summary>
    /// Place a decimal point at the end of the displayed number.
    /// </summary>
    private void Decimal() {
      _txtNumber.Text += @".";
    }

    /// <summary>
    /// Evaluate the queued operation and place the result in the display.
    /// </summary>
    private void Evaluate() {
      Operate();
      _txtNumber.Text = string.Format(NUM_FORMAT, _total);
      _total = 0;
      _op = Operator.Add;
    }

    private void Backspace() {
      string tmp = _txtNumber.Text.Substring(0, _txtNumber.Text.Length - 1);
      if(tmp.IndexOf('.') > -1)
        _txtNumber.Text = tmp;
      else
        _txtNumber.Text = string.Format(NUM_FORMAT, double.Parse(tmp));
    }
    #endregion  // methods

    #region event handlers
    private void SimpleCalcForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
      switch(e.KeyChar) {
        case 'c': Clear(); break;
        case 'C': Clear(); break;
        case '+': Operate(Operator.Add); break;
        case '-': Operate(Operator.Subtract); break;
        case '*': Operate(Operator.Multiply); break;
        case '/': Operate(Operator.Divide); break;
        case '0': Digit(0); break;
        case '1': Digit(1); break;
        case '2': Digit(2); break;
        case '3': Digit(3); break;
        case '4': Digit(4); break;
        case '5': Digit(5); break;
        case '6': Digit(6); break;
        case '7': Digit(7); break;
        case '8': Digit(8); break;
        case '9': Digit(9); break;
        case '.': Decimal(); break;
        case (char)8: Backspace(); break;
        case '=': Evaluate(); break;
      }
    }

    private void _btnClear_Click(object sender, System.EventArgs e) {
      Clear();
    }

    private void _btnDivide_Click(object sender, System.EventArgs e) {
      Operate(Operator.Divide);
    }

    private void _btnMultiply_Click(object sender, System.EventArgs e) {
      Operate(Operator.Multiply);
    }

    private void _btnSubtract_Click(object sender, System.EventArgs e) {
      Operate(Operator.Subtract);
    }

    private void _btnAdd_Click(object sender, System.EventArgs e) {
      Operate(Operator.Add);
    }

    private void _btnEquals_Click(object sender, System.EventArgs e) {
      Evaluate();
    }

    private void _btn9_Click(object sender, System.EventArgs e) {
      Digit(9);
    }

    private void _btn8_Click(object sender, System.EventArgs e) {
      Digit(8);
    }

    private void _btn7_Click(object sender, System.EventArgs e) {
      Digit(7);
    }

    private void _btn6_Click(object sender, System.EventArgs e) {
      Digit(6);
    }

    private void _btn5_Click(object sender, System.EventArgs e) {
      Digit(5);
    }

    private void _btn4_Click(object sender, System.EventArgs e) {
      Digit(4);
    }

    private void _btn3_Click(object sender, System.EventArgs e) {
      Digit(3);
    }

    private void _btn2_Click(object sender, System.EventArgs e) {
      Digit(2);
    }

    private void _btn1_Click(object sender, System.EventArgs e) {
      Digit(1);
    }

    private void _btn0_Click(object sender, System.EventArgs e) {
      Digit(0);
    }

    private void _btnDecimal_Click(object sender, System.EventArgs e) {
      Decimal();
    }

    private void _btnOK_Click(object sender, System.EventArgs e) {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void _btnCancel_Click(object sender, System.EventArgs e) {
      DialogResult = DialogResult.Cancel;
      Close();
    }
    #endregion  // event handlers
  }
}
