using System.Drawing;
using System.Windows.Forms;

namespace au.util.comctl {
  public partial class CaptionedPictureBox : UserControl {
    public CaptionedPictureBox() {
      InitializeComponent();
    }

    public Image Image {
      get { return _pb.Image; }
      set { _pb.Image = value; }
    }

    public PictureBoxSizeMode SizeMode {
      get { return _pb.SizeMode; }
      set { _pb.SizeMode = value; }
    }

    public override string Text {
      get { return _lblCaption.Text; }
      set { _lblCaption.Text = value; }
    }

    public int CaptionHeight {
      get { return _lblCaption.Height; }
      set { _lblCaption.Height = value; }
    }

    public int Gap {
      get { return _lblCaption.Padding.Top; }
      set { _lblCaption.Padding = new Padding(_lblCaption.Padding.Left, value, _lblCaption.Padding.Right, _lblCaption.Padding.Bottom); }
    }

    private void subControl_Click(object sender, System.EventArgs e) {
      OnClick(e);
    }

    private void subControl_DoubleClick(object sender, System.EventArgs e) {
      OnDoubleClick(e);
    }
  }
}
