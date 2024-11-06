using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SearchNavigationTool;

public class LabelCollection : CollectionBase
{
	private readonly UserControl HostControl;

	private Label this[int Index] => (Label)base.List[Index];

	public event MouseEventHandler MouseUp;

	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		this.MouseUp(this, e);
	}

	public LabelCollection(UserControl host)
	{
		HostControl = host;
	}

	private Label AddLabel(int value)
	{
		bool flag = false;
		int num = 0;
		int num2 = base.List.Count;
		while (num < num2 && !flag)
		{
			if (Convert.ToInt16(this[num].Tag, CultureInfo.CurrentCulture) > value)
			{
				flag = true;
			}
			else
			{
				num++;
			}
		}
		int num3 = num2 / 3;
		int num4 = HostControl.Height - 4;
		num3 = num4 * num3 / 3 + 2;
		num4 /= 3;
		int num5 = num2 % 3;
		int num6 = HostControl.Width - 4;
		num5 = num6 * num5 / 3 + 2;
		num6 /= 3;
		Label label = new Label();
		label.Top = num3;
		label.Left = num5;
		label.Height = num4;
		label.Width = num6;
		label.TextAlign = ContentAlignment.TopCenter;
		base.List.Add(label);
		while (num2 > num)
		{
			this[num2].Text = this[num2 - 1].Text;
			this[num2].Tag = this[num2 - 1].Tag;
			num2--;
		}
		this[num2].Text = value.ToString(CultureInfo.CurrentCulture);
		this[num2].Tag = value;
		HostControl.Controls.Add(label);
		label.MouseUp += MouseUpHandler;
		label.Paint += PaintHandler;
		return label;
	}

	private void Remove(int Value)
	{
		if (Value < 1 || Value > 9)
		{
			return;
		}
		int i = 0;
		bool flag = false;
		int count;
		for (count = base.List.Count; i < count; i++)
		{
			if (flag)
			{
				break;
			}
			if (Convert.ToInt16(this[i].Tag, CultureInfo.CurrentCulture) == Value)
			{
				flag = true;
			}
		}
		if (flag)
		{
			for (; i < count; i++)
			{
				this[i - 1].Text = this[i].Text;
				this[i - 1].Tag = this[i].Tag;
			}
			i--;
			HostControl.Controls.Remove(this[i]);
			base.List.RemoveAt(i);
		}
	}

	public void RemoveAll()
	{
		while (base.List.Count > 0)
		{
			HostControl.Controls.Remove(this[base.List.Count - 1]);
			base.List.RemoveAt(base.List.Count - 1);
		}
	}

	public void Memorize(int value)
	{
		if (value < 1 || value > 9)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < base.List.Count; i++)
		{
			if (flag)
			{
				break;
			}
			if (Convert.ToInt16(this[i].Tag, CultureInfo.CurrentCulture) == value)
			{
				flag = true;
			}
		}
		if (flag)
		{
			Remove(value);
		}
		else
		{
			AddLabel(value);
		}
	}

	private void MouseUpHandler(object sender, MouseEventArgs e)
	{
		OnMouseUp(e);
	}

	private void PaintHandler(object sender, PaintEventArgs pe)
	{
		Label label = (Label)sender;
		if (label != null)
		{
			float emSize = (float)label.Height * 0.6f;
			Font font2 = (label.Font = new Font(label.Font.FontFamily, emSize));
			font2.Dispose();
		}
	}
}
