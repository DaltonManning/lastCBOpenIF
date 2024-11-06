namespace SearchNavigationTool;

public class Cells
{
	private int[,] m_nCells;

	public Cells()
	{
		m_nCells = new int[9, 9];
		Reset();
	}

	public Cells(int[,] cells)
	{
		m_nCells = cells;
	}

	public Cells Clone()
	{
		return new Cells((int[,])m_nCells.Clone());
	}

	public void Reset()
	{
		for (int i = 0; i < 9; i++)
		{
			ResetRow(i);
		}
	}

	public void ResetRow(int row)
	{
		if (row >= 0 && row <= 8)
		{
			for (int i = 0; i < 9; i++)
			{
				m_nCells[row, i] = 0;
			}
		}
	}

	public void ResetColumn(int column)
	{
		if (column >= 0 && column <= 8)
		{
			for (int i = 0; i < 9; i++)
			{
				m_nCells[i, column] = 0;
			}
		}
	}

	public void ResetBlock(int index)
	{
		if (index < 0 || index > 8)
		{
			return;
		}
		int num = index / 3;
		num *= 3;
		int num2 = index - num;
		num2 *= 3;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				m_nCells[i + num, j + num2] = 0;
			}
		}
	}

	public int GetValue(int row, int column)
	{
		if (row < 0 || row > 8)
		{
			return 0;
		}
		if (column < 0 || column > 8)
		{
			return 0;
		}
		return m_nCells[row, column];
	}

	public void SetValue(int row, int column, int value)
	{
		if (row >= 0 && row <= 8 && column >= 0 && column <= 8 && value >= 0 && value <= 9)
		{
			m_nCells[row, column] = value;
		}
	}
}
