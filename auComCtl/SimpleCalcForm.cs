using System;
using System.Windows.Forms;  // Form

namespace au.util.comctl {
	internal partial class SimpleCalcForm : Form {
		private const string NUM_FORMAT = "{0:#,##0.######}";

		private enum Operator {
			Add,
			Subtract,
			Multiply,
			Divide
		}

		#region Data Members
		private double _total;
		private Operator _op;
		#endregion

		#region Constructors
		public SimpleCalcForm() {
			InitializeComponent();
			_total = 0;
			_op = Operator.Add;
		}
		#endregion  // Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the current value held by the simple calculator
		/// </summary>
		public double Value {
			get { return double.Parse(_txtNumber.Text); }
			set { _txtNumber.Text = string.Format(NUM_FORMAT, value); }
		}
		#endregion  // Properties

		#region Methods
		/// <summary>
    /// Clear the current display value or reset the calculator to zero
    /// </summary>
    private void Clear() {
      if(_txtNumber.Text == "0") {
        _total = 0;
        _op = Operator.Add;
      } else
        _txtNumber.Text = "0";
    }

    /// <summary>
    /// Perform queued operation
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
    /// Perform queued operation and queue the next operation
    /// </summary>
    /// <param name="op">Next operation</param>
    private void Operate(Operator op) {
      Operate();
      _txtNumber.Text = "0";
      _op = op;
    }

    /// <summary>
    /// Append a digit to the currently displayed value
    /// </summary>
    /// <param name="d">The digit to append</param>
    private void Digit(int d) {
      if(_txtNumber.Text == "0")
        _txtNumber.Text = d.ToString();
      else if(_txtNumber.Text.IndexOf('.') > -1)
        _txtNumber.Text += d.ToString();
      else {
        _txtNumber.Text = string.Format(NUM_FORMAT, Value * 10 + d);
      }
    }

    /// <summary>
    /// Place a decimal point at the end of the displayed number
    /// </summary>
    private void Decimal() {
      _txtNumber.Text += ".";
    }

    /// <summary>
    /// Evaluate the queued operation and place the result in the display
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
		#endregion  // Methods

		#region Event Handlers
		private void SimpleCalcForm_KeyPress(object sender, KeyPressEventArgs e) {
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

		private void _btnClear_Click(object sender, EventArgs e) {
			Clear();
		}

		private void _btnDivide_Click(object sender, EventArgs e) {
			Operate(Operator.Divide);
		}

		private void _btnMultiply_Click(object sender, EventArgs e) {
			Operate(Operator.Multiply);
		}

		private void _btnSubtract_Click(object sender, EventArgs e) {
			Operate(Operator.Subtract);
		}

		private void _btnAdd_Click(object sender, EventArgs e) {
			Operate(Operator.Add);
		}

		private void _btnEquals_Click(object sender, EventArgs e) {
			Evaluate();
		}

		private void _btn9_Click(object sender, EventArgs e) {
			Digit(9);
		}

		private void _btn8_Click(object sender, EventArgs e) {
			Digit(8);
		}

		private void _btn7_Click(object sender, EventArgs e) {
			Digit(7);
		}

		private void _btn6_Click(object sender, EventArgs e) {
			Digit(6);
		}

		private void _btn5_Click(object sender, EventArgs e) {
			Digit(5);
		}

		private void _btn4_Click(object sender, EventArgs e) {
			Digit(4);
		}

		private void _btn3_Click(object sender, EventArgs e) {
			Digit(3);
		}

		private void _btn2_Click(object sender, EventArgs e) {
			Digit(2);
		}

		private void _btn1_Click(object sender, EventArgs e) {
			Digit(1);
		}

		private void _btn0_Click(object sender, EventArgs e) {
			Digit(0);
		}

		private void _btnDecimal_Click(object sender, EventArgs e) {
			Decimal();
		}

		private void _btnOK_Click(object sender, EventArgs e) {
			Close();
		}

		private void _btnCancel_Click(object sender, EventArgs e) {
			Close();
		}
		#endregion  // Event Handlers
	}
}