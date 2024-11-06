using System.Collections;
using System.Windows.Forms;

namespace SearchNavigationTool;

public class CellCollection : CollectionBase
{
	private readonly Form HostForm;

	private bool m_bEnabled;

	public Cell this[int index] => (Cell)base.List[index];

	public bool Enable
	{
		get
		{
			return m_bEnabled;
		}
		set
		{
			m_bEnabled = value;
			int num = base.List.Count;
			while (num > 0)
			{
				num--;
				this[num].Enable = m_bEnabled;
			}
		}
	}

	public CellCollection(Form host)
	{
		HostForm = host;
		m_bEnabled = true;
	}

	public Cell AddCell(int left, int top)
	{
		Cell cell = new Cell();
		base.List.Add(cell);
		HostForm.Controls.Add(cell);
		cell.Top = top;
		cell.Left = left;
		cell.Enable = m_bEnabled;
		return cell;
	}

	public void Remove()
	{
		if (base.Count > 0)
		{
			HostForm.Controls.Remove(this[base.Count - 1]);
			base.List.RemoveAt(base.Count - 1);
		}
	}

	public void SetSelectedValue(int value)
	{
		int num = base.Count;
		while (num > 0)
		{
			num--;
			this[num].SelectedValue = value;
		}
	}

	public void ClearContent()
	{
		for (int i = 0; i < base.List.Count; i++)
		{
			this[i].ClearContent();
		}
	}
}
