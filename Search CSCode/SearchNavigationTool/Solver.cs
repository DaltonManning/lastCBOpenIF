namespace SearchNavigationTool;

public class Solver
{
	private Cells m_Cells;

	private SolverCells m_SolverCells;

	private SolverRow[] m_Rows;

	private SolverColumn[] m_Columns;

	private SolverBlock[] m_Blocks;

	private int m_nCellsLeft;

	private int[] m_nCellsToSolve;

	public Solver(Cells cells)
	{
		m_Cells = cells.Clone();
		m_nCellsLeft = 81;
		m_nCellsToSolve = new int[81];
		for (int i = 0; i < 81; i++)
		{
			m_nCellsToSolve[i] = i;
		}
		m_SolverCells = new SolverCells();
		m_Rows = new SolverRow[9];
		m_Columns = new SolverColumn[9];
		m_Blocks = new SolverBlock[9];
		for (int i = 0; i < 9; i++)
		{
			m_Rows[i] = new SolverRow(m_SolverCells, i);
			m_Columns[i] = new SolverColumn(m_SolverCells, i);
			m_Blocks[i] = new SolverBlock(m_SolverCells, i);
		}
		for (int j = 0; j < 9; j++)
		{
			for (int k = 0; k < 9; k++)
			{
				int value = m_Cells.GetValue(j, k);
				if (value > 0)
				{
					CellSolved(j, k, value);
				}
			}
		}
	}

	private SolverRow Row(int Index)
	{
		return m_Rows[Index];
	}

	private SolverColumn Column(int Index)
	{
		return m_Columns[Index];
	}

	private SolverBlock Block(int Row, int Column)
	{
		int num = Row / 3 * 3 + Column / 3;
		return m_Blocks[num];
	}

	private void CellSolved(int Row, int Column, int Value)
	{
		if (Row < 0 || Row > 8 || Column < 0 || Column > 8)
		{
			return;
		}
		m_Cells.SetValue(Row, Column, Value);
		m_SolverCells.SetSolvedValue(Row, Column, Value);
		int num = Row * 9 + Column;
		int i = 0;
		bool flag = false;
		for (; i < m_nCellsLeft; i++)
		{
			if (flag)
			{
				break;
			}
			flag = m_nCellsToSolve[i] == num;
		}
		if (flag)
		{
			m_nCellsLeft--;
		}
		for (; i < m_nCellsLeft; i++)
		{
			m_nCellsToSolve[i - 1] = m_nCellsToSolve[i];
		}
		this.Row(Row).RemoveCandidate(Value);
		this.Column(Column).RemoveCandidate(Value);
		Block(Row, Column).RemoveCandidate(Value);
	}

	public bool Solve()
	{
		while (m_nCellsLeft > 0)
		{
			FindSingles();
			FindHiddenSingles();
		}
		return false;
	}

	private void FindSingles()
	{
		int num = 0;
		bool flag = true;
		while (!flag)
		{
			flag = false;
			while (num < m_nCellsLeft)
			{
				int num2 = m_nCellsToSolve[num] / 9;
				int num3 = Row(num2).FindFirstSingle();
				if (num3 > -1)
				{
					int value = Row(num2).GetCandidateStack(num3).GetValue(0);
					CellSolved(num2, num3, value);
					flag = true;
				}
				else
				{
					num++;
				}
			}
		}
	}

	private void FindHiddenSingles()
	{
		int num = 0;
		int value = 0;
		bool flag = true;
		while (!flag)
		{
			flag = false;
			while (num < m_nCellsLeft)
			{
				int num2 = m_nCellsToSolve[num] / 9;
				int num3 = Row(num2).FindFirstHiddenSingle(ref value);
				flag = num3 > -1;
				if (!flag)
				{
					num3 = m_nCellsToSolve[num] % 9;
					num2 = Column(num3).FindFirstHiddenSingle(ref value);
					flag = num2 > -1;
				}
				if (!flag)
				{
					num2 = m_nCellsToSolve[num] / 9;
					int num4 = Block(num2, num3).FindFirstHiddenSingle(ref value);
					if (num4 > -1)
					{
						num2 = num2 / 3 * 3;
						num3 = num3 / 3 * 3;
						num2 += num4 / 3;
						num3 += num4 % 3;
						flag = true;
					}
				}
				if (flag)
				{
					CellSolved(num2, num3, value);
				}
				else
				{
					num++;
				}
			}
		}
	}
}
