using System;

namespace SearchNavigationTool;

public class Board
{
	private Cells m_Cells;

	private Row[] m_Rows;

	private Column[] m_Columns;

	private Block[] m_Blocks;

	private SolverCells m_SolverCells;

	public Board()
	{
		m_Cells = new Cells();
		m_Rows = new Row[9];
		m_Columns = new Column[9];
		m_Blocks = new Block[9];
		m_SolverCells = new SolverCells();
		for (int i = 0; i < 9; i++)
		{
			m_Rows[i] = new Row(m_Cells, i);
			m_Columns[i] = new Column(m_Cells, i);
			m_Blocks[i] = new Block(m_Cells, i);
		}
	}

	public Board(Cells cells)
	{
		m_Cells = cells;
		m_Rows = new Row[9];
		m_Columns = new Column[9];
		m_Blocks = new Block[9];
		m_SolverCells = new SolverCells();
		for (int i = 0; i < 9; i++)
		{
			m_Rows[i] = new Row(m_Cells, i);
			m_Columns[i] = new Column(m_Cells, i);
			m_Blocks[i] = new Block(m_Cells, i);
		}
	}

	public Board Clone()
	{
		return new Board(m_Cells.Clone());
	}

	public void Clear()
	{
		m_Cells.Reset();
	}

	public Row Row(int index)
	{
		return m_Rows[index];
	}

	public Column Column(int index)
	{
		return m_Columns[index];
	}

	public Block Block(int index)
	{
		return m_Blocks[index];
	}

	private bool Validate(bool validateRows, bool validateColumns, bool validateBlocks)
	{
		if (validateRows)
		{
			for (int i = 0; i < 9; i++)
			{
				if (!m_Rows[i].IsValid())
				{
					return false;
				}
			}
		}
		if (validateColumns)
		{
			for (int i = 0; i < 9; i++)
			{
				if (!m_Columns[i].IsValid())
				{
					return false;
				}
			}
		}
		if (validateBlocks)
		{
			for (int i = 0; i < 9; i++)
			{
				if (!m_Blocks[i].IsValid())
				{
					return false;
				}
			}
		}
		return true;
	}

	public void Fill()
	{
		bool flag = false;
		bool flag2 = true;
		int num = 0;
		EFVector eFVector = m_Rows[0];
		int num2 = 0;
		bool flag3 = true;
		bool flag4 = true;
		bool flag5 = true;
		long num3 = 1L;
		bool flag6 = false;
		m_Cells.Reset();
		while (!flag)
		{
			flag3 = true;
			flag4 = true;
			flag5 = true;
			switch (num)
			{
			case 0:
				num2 = 0;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 1:
				num2 = 0;
				eFVector = m_Blocks[num2];
				flag5 = false;
				break;
			case 2:
				num2 = 0;
				eFVector = m_Columns[num2];
				flag4 = false;
				break;
			case 3:
				num2 = 1;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 4:
				num2 = 2;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 5:
				num2 = 1;
				eFVector = m_Columns[num2];
				flag4 = false;
				break;
			case 6:
				num2 = 2;
				eFVector = m_Columns[num2];
				flag4 = false;
				break;
			case 7:
				num2 = 3;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 8:
				num2 = 4;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 9:
				num2 = 5;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 10:
				num2 = 6;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 11:
				num2 = 7;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			case 12:
				num2 = 8;
				eFVector = m_Rows[num2];
				flag3 = false;
				break;
			}
			if (flag2)
			{
				eFVector.FillRandom();
				flag2 = false;
				num3 = 1L;
			}
			else
			{
				num3 = eFVector.Iterate();
			}
			flag6 = Validate(flag3, flag4, flag5);
			while (!flag6 && num3 > 0)
			{
				num3 = eFVector.Iterate();
				flag6 = Validate(flag3, flag4, flag5);
			}
			if (flag6)
			{
				flag = num == 12;
				num++;
				flag2 = true;
			}
			else
			{
				eFVector.ResetToOriginalState();
				num--;
			}
		}
	}

	public void HideNumbers(int level)
	{
		int num = 0;
		int[,] array = new int[9, 9];
		int[] array2 = new int[9];
		int[] array3 = new int[9];
		int num2 = 9;
		for (num = 0; num < 9; num++)
		{
			array2[num] = 9;
			array3[num] = num;
			for (int i = 0; i < 9; i++)
			{
				array[num, i] = i;
			}
		}
		Random random = new Random((int)DateTime.Now.Ticks);
		num = 0;
		m_SolverCells.Reset();
		while (num < 9)
		{
			int i = random.Next(0, 9);
			if (SolvableByCounting(num, i))
			{
				int value = m_Cells.GetValue(num, i);
				m_SolverCells.GetCandidateStack(num, i).AddValue(value);
				m_Cells.SetValue(num, i, 0);
				array2[num]--;
				for (int j = i; j < array2[num]; j++)
				{
					array[num, j] = array[num, j + 1];
				}
				num++;
			}
		}
		if (level < 1)
		{
			return;
		}
		while (num2 > 0)
		{
			int k = random.Next(0, num2);
			num = array3[k];
			int j = random.Next(0, array2[num]);
			int i = array[num, j];
			if (SolvableByScaning(num, i))
			{
				int value = m_Cells.GetValue(num, i);
				m_SolverCells.GetCandidateStack(num, i).AddValue(value);
				m_Cells.SetValue(num, i, 0);
			}
			array2[num]--;
			for (; j < array2[num]; j++)
			{
				array[num, j] = array[num, j + 1];
			}
			if (array2[num] == 0)
			{
				for (num2--; k < num2; k++)
				{
					array3[k] = array3[k + 1];
				}
			}
		}
		m_SolverCells.Print();
	}

	private bool SolvableByCounting(int Row, int Column)
	{
		int value = m_Cells.GetValue(Row, Column);
		m_Cells.SetValue(Row, Column, 0);
		bool flag = this.Column(Column).GetCount > 7;
		if (flag)
		{
			int index = Row / 3 * 3 + Column / 3;
			flag = Block(index).GetCount > 7;
		}
		m_Cells.SetValue(Row, Column, value);
		return flag;
	}

	private bool SolvableByScaning(int Row, int Column)
	{
		int num = Row / 3;
		int num2 = Column / 3;
		int num3 = Row % 3;
		int num4 = Column % 3;
		bool flag = true;
		int value = m_Cells.GetValue(Row, Column);
		for (int i = 0; i < 2; i++)
		{
			num3++;
			if (num3 > 2)
			{
				num3 = 0;
			}
			int num5 = num * 3 + num3;
			if (m_Cells.GetValue(num5, Column) == 0)
			{
				flag = this.Row(num5).HasValue(value);
			}
			if (!flag)
			{
				return false;
			}
		}
		for (int i = 0; i < 2; i++)
		{
			num4++;
			if (num4 > 2)
			{
				num4 = 0;
			}
			int num6 = num2 * 3 + num4;
			if (m_Cells.GetValue(Row, num6) == 0)
			{
				flag = this.Column(num6).HasValue(value);
			}
			if (!flag)
			{
				return false;
			}
		}
		return flag;
	}

	public void Print()
	{
		for (int i = 0; i < 9; i++)
		{
			Console.WriteLine(m_Rows[i].ToString());
		}
		Console.WriteLine();
	}
}
