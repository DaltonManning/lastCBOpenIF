using System;
using System.Globalization;
using System.Text;

namespace SearchNavigationTool;

public abstract class EFVector
{
	private int nVectorIndex;

	private Cells vectorCells;

	private int[] m_nFillResult;

	private int m_nNrOfFillPositions;

	private int[] m_nOriginalState;

	private int[] m_nIterationResult;

	private int[] m_nCurrentPositions;

	private long[] m_lIterations;

	private long m_lIterationsLeft;

	protected int VectorIndex => nVectorIndex;

	protected Cells VectorCells => vectorCells;

	public int GetCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < 9; i++)
			{
				if (GetValue(i) != 0)
				{
					num++;
				}
			}
			return num;
		}
	}

	protected EFVector(Cells cells, int index)
	{
		vectorCells = cells;
		nVectorIndex = index;
		m_nFillResult = new int[9];
		m_nOriginalState = new int[9];
		m_nIterationResult = new int[9];
		m_nCurrentPositions = new int[9];
		m_lIterations = new long[9];
		for (int i = 0; i < 9; i++)
		{
			m_nFillResult[i] = 0;
			m_nOriginalState[i] = GetValue(i);
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("");
		for (int i = 0; i < 9; i++)
		{
			stringBuilder.Append(GetValue(i).ToString(CultureInfo.CurrentCulture));
			if (i < 8)
			{
				stringBuilder.Append(" ");
			}
		}
		return stringBuilder.ToString();
	}

	public abstract void Reset();

	public abstract int GetValue(int index);

	public abstract void SetValue(int index, int value);

	public bool HasValue(int value)
	{
		for (int i = 0; i < 9; i++)
		{
			if (GetValue(i) == value)
			{
				return true;
			}
		}
		return false;
	}

	public int ValuePosition(int value)
	{
		for (int i = 0; i < 9; i++)
		{
			if (GetValue(i) == value)
			{
				return i;
			}
		}
		return -1;
	}

	public bool IsValid()
	{
		bool[] array = new bool[9];
		for (int i = 0; i < 9; i++)
		{
			array[i] = false;
		}
		for (int i = 0; i < 9; i++)
		{
			int num = GetValue(i) - 1;
			if (num > -1)
			{
				if (array[num])
				{
					return false;
				}
				array[num] = true;
			}
		}
		return true;
	}

	public void FillRandom()
	{
		int[] array = new int[9];
		int num = 0;
		int i = 0;
		for (int j = 0; j < 9; j++)
		{
			m_nOriginalState[j] = GetValue(j);
		}
		for (int k = 1; k < 10; k++)
		{
			if (!HasValue(k))
			{
				array[num] = k;
				num++;
			}
		}
		m_nNrOfFillPositions = num;
		Random random = new Random((int)DateTime.Now.Ticks);
		while (num > 0)
		{
			int j = random.Next(0, num);
			num--;
			m_nFillResult[i] = array[j];
			i++;
			for (; j < num; j++)
			{
				array[j] = array[j + 1];
			}
		}
		for (; i < 9; i++)
		{
			m_nFillResult[i] = 0;
		}
		InitiateIterator();
	}

	public void ResetToOriginalState()
	{
		for (int i = 0; i < 9; i++)
		{
			SetValue(i, m_nOriginalState[i]);
		}
	}

	private void InitiateIterator()
	{
		for (int i = 0; i < 9; i++)
		{
			m_nIterationResult[i] = 0;
			m_nCurrentPositions[i] = 0;
			m_lIterations[i] = 0L;
		}
		m_lIterationsLeft = Faculty(m_nNrOfFillPositions);
		for (int i = m_nNrOfFillPositions - 1; i > -1; i--)
		{
			m_nCurrentPositions[i] = i;
			m_lIterations[i] = Faculty(i);
		}
		Iterate();
	}

	public long Iterate()
	{
		int i = 0;
		for (int j = 0; j < 9; j++)
		{
			m_nIterationResult[j] = 0;
		}
		PlaceNumber(m_nNrOfFillPositions - 1);
		ResetToOriginalState();
		for (int j = 0; j < m_nNrOfFillPositions; j++)
		{
			int num = m_nIterationResult[j];
			int value = m_nFillResult[num];
			for (; GetValue(i) > 0; i++)
			{
			}
			SetValue(i, value);
			i++;
		}
		m_lIterationsLeft--;
		return m_lIterationsLeft;
	}

	private void PlaceNumber(int CurrentIndex)
	{
		if (CurrentIndex < 0 || CurrentIndex > 8)
		{
			return;
		}
		int num = m_nCurrentPositions[CurrentIndex];
		if (m_lIterations[CurrentIndex] == 0L)
		{
			num--;
			if (num < 0)
			{
				num = m_nNrOfFillPositions - 1;
			}
		}
		while (m_nIterationResult[num] != 0)
		{
			num--;
			if (num < 0)
			{
				num = m_nNrOfFillPositions - 1;
			}
		}
		if (num != m_nCurrentPositions[CurrentIndex])
		{
			m_lIterations[CurrentIndex] = Faculty(CurrentIndex);
			m_nCurrentPositions[CurrentIndex] = num;
		}
		m_nIterationResult[num] = CurrentIndex;
		m_lIterations[CurrentIndex]--;
		if (CurrentIndex > 0)
		{
			PlaceNumber(CurrentIndex - 1);
		}
	}

	public long Faculty(int value)
	{
		if (value > 0)
		{
			return value * Faculty(value - 1);
		}
		return 1L;
	}
}
