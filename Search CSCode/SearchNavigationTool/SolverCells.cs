using System;

namespace SearchNavigationTool;

public class SolverCells
{
	private Candidates[,] m_Candidates;

	public SolverCells()
	{
		m_Candidates = new Candidates[9, 9];
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				m_Candidates[i, j] = new Candidates();
			}
		}
	}

	public Candidates GetCandidateStack(int row, int column)
	{
		return m_Candidates[row, column];
	}

	public void Fill()
	{
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				m_Candidates[i, j].Fill();
			}
		}
	}

	public void Reset()
	{
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				m_Candidates[i, j].Reset();
			}
		}
	}

	public void SetSolvedValue(int row, int column, int value)
	{
		m_Candidates[row, column].SetValue(value);
	}

	public void Print()
	{
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				Console.Write(m_Candidates[i, j].ToString());
				Console.Write(" ");
			}
			Console.WriteLine();
		}
	}
}
