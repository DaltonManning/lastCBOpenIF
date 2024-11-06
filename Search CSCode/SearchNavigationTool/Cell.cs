using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SearchNavigationTool;

public class Cell : UserControl
{
	private Container components;

	private bool m_bStaticValue;

	private int m_nCorrectValue;

	private int m_nSelectedValue;

	private bool m_bValueSet;

	private bool m_bEnabled;

	private LabelCollection m_LabelArray;

	private Label lblValue;

	public bool Enable
	{
		get
		{
			return m_bEnabled;
		}
		set
		{
			m_bEnabled = value;
		}
	}

	public bool StaticValue
	{
		get
		{
			return m_bStaticValue;
		}
		set
		{
			m_bStaticValue = value;
			lblValue.Visible = m_bStaticValue;
			if (m_bStaticValue)
			{
				lblValue.ForeColor = Color.FromArgb(0, 0, 128);
				if (m_nCorrectValue > 0)
				{
					lblValue.Text = m_nCorrectValue.ToString(CultureInfo.CurrentCulture);
				}
				else
				{
					lblValue.Text = "";
				}
			}
		}
	}

	public int CorrectValue
	{
		get
		{
			return m_nCorrectValue;
		}
		set
		{
			if (value > -1 && value < 10)
			{
				m_nCorrectValue = value;
			}
		}
	}

	public int SelectedValue
	{
		get
		{
			return m_nSelectedValue;
		}
		set
		{
			if (value > 0 && value < 10)
			{
				m_nSelectedValue = value;
			}
		}
	}

	public Cell()
	{
		InitializeComponent();
		m_LabelArray = new LabelCollection(this);
		m_LabelArray.MouseUp += Cell_MouseUp;
		ClearContent();
		lblValue.Location = new Point(base.Location.X + 2, base.Location.Y + 2);
		lblValue.Size = new Size(base.Width - 4, base.Height - 4);
		m_bEnabled = true;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.lblValue = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lblValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblValue.CausesValidation = false;
		this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblValue.Location = new System.Drawing.Point(16, 16);
		this.lblValue.Name = "lblValue";
		this.lblValue.Size = new System.Drawing.Size(144, 144);
		this.lblValue.TabIndex = 0;
		this.lblValue.Text = "Number";
		this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblValue.MouseUp += new System.Windows.Forms.MouseEventHandler(Cell_MouseUp);
		base.CausesValidation = false;
		base.Controls.Add(this.lblValue);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "Cell";
		base.Size = new System.Drawing.Size(176, 176);
		base.MouseUp += new System.Windows.Forms.MouseEventHandler(Cell_MouseUp);
		base.Paint += new System.Windows.Forms.PaintEventHandler(Cell_Paint);
		base.ResumeLayout(false);
	}

	private void Cell_Paint(object sender, PaintEventArgs pe)
	{
		int num = 1;
		int num2 = 1;
		int num3 = base.Width - 2;
		int num4 = base.Height - 2;
		Graphics graphics = pe.Graphics;
		Pen pen = new Pen(Color.FromKnownColor(KnownColor.ControlLightLight), 1f);
		graphics.DrawLine(pen, num, num2, num3 - 1, num2);
		graphics.DrawLine(pen, num, num2, num, num4);
		pen.Color = Color.FromKnownColor(KnownColor.ControlDark);
		graphics.DrawLine(pen, num + 1, num4, num3, num4);
		graphics.DrawLine(pen, num3, num2, num3, num4);
		float emSize = (float)lblValue.Height * 0.6f;
		Font font = new Font(lblValue.Font.FontFamily, emSize);
		lblValue.Font = font;
	}

	public void ClearContent()
	{
		m_bStaticValue = false;
		m_nCorrectValue = 0;
		m_nSelectedValue = 0;
		m_bValueSet = false;
		lblValue.Text = "";
		lblValue.Visible = false;
		lblValue.ForeColor = Color.FromArgb(0, 0, 128);
		m_LabelArray.RemoveAll();
	}

	private void Cell_MouseUp(object sender, MouseEventArgs e)
	{
		if (m_bStaticValue || !m_bEnabled)
		{
			return;
		}
		if (e.Button == MouseButtons.Left)
		{
			if (!m_bValueSet)
			{
				m_bValueSet = true;
				m_LabelArray.RemoveAll();
				if (m_nSelectedValue == m_nCorrectValue)
				{
					lblValue.ForeColor = Color.FromArgb(0, 0, 255);
				}
				else
				{
					lblValue.ForeColor = Color.FromArgb(255, 0, 0);
				}
				lblValue.Text = m_nSelectedValue.ToString(CultureInfo.CurrentCulture);
				lblValue.Visible = m_nSelectedValue > 0;
			}
		}
		else if (m_bValueSet)
		{
			m_bValueSet = false;
			lblValue.Text = "";
			lblValue.Visible = false;
		}
		else
		{
			m_LabelArray.Memorize(m_nSelectedValue);
		}
	}
}
