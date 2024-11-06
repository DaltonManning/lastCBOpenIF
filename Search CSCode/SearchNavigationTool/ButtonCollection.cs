using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SearchNavigationTool;

public class ButtonCollection : CollectionBase, IList, ICollection, IEnumerable
{
	private readonly Form HostForm;

	private int m_nCurrentButton;

	public Button this[int index] => (Button)base.List[index];

	public int SelectedButton
	{
		get
		{
			return m_nCurrentButton;
		}
		set
		{
			if (value > -1 && value < base.List.Count - 1)
			{
				EventArgs e = new EventArgs();
				ClickHandler((Button)base.List[value], e);
			}
		}
	}

	public event EventHandler Click;

	protected virtual void OnClick(EventArgs e)
	{
		this.Click(this, e);
	}

	public ButtonCollection(Form host)
	{
		HostForm = host;
		m_nCurrentButton = -1;
	}

	public Button AddButton(int left, int top, int size)
	{
		Button button = new Button();
		base.List.Add(button);
		button.Top = top;
		button.Left = left;
		button.Width = size;
		button.Height = size;
		button.Text = "&" + base.List.Count.ToString(CultureInfo.CurrentCulture);
		button.Tag = base.List.Count - 1;
		button.TextAlign = ContentAlignment.MiddleCenter;
		button.Click += ClickHandler;
		HostForm.Controls.Add(button);
		return button;
	}

	public void RemoveAll()
	{
		while (base.List.Count > 0)
		{
			HostForm.Controls.Remove(this[base.List.Count - 1]);
			base.List.RemoveAt(base.List.Count - 1);
		}
	}

	private void ClickHandler(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		if (button != null)
		{
			if (m_nCurrentButton > -1)
			{
				((Button)base.List[m_nCurrentButton]).BackColor = Color.FromKnownColor(KnownColor.Control);
			}
			m_nCurrentButton = Convert.ToInt16(button.Tag, CultureInfo.CurrentCulture);
			button.BackColor = Color.FromArgb(250, 250, 0);
			OnClick(e);
		}
	}
}
