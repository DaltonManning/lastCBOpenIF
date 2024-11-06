namespace SearchNavigationTool;

public abstract class SolverVector
{
	internal int nIndex;

	internal SolverCells solverCells;

	protected SolverVector(SolverCells solverCells, int index)
	{
		this.solverCells = solverCells;
		nIndex = index;
	}

	public abstract Candidates GetCandidateStack(int index);

	public void RemoveCandidate(int value)
	{
		for (int i = 0; i < 9; i++)
		{
			if (!GetCandidateStack(i).IsSolved)
			{
				GetCandidateStack(i).Remove(value);
			}
		}
	}

	public int FindFirstSingle()
	{
		for (int i = 0; i < 9; i++)
		{
			if (!GetCandidateStack(i).IsSolved && GetCandidateStack(i).IsSingle)
			{
				return i;
			}
		}
		return -1;
	}

	public int FindFirstHiddenSingle(ref int value)
	{
		for (int i = 1; i < 10; i++)
		{
			int num = 0;
			int result = 0;
			for (int j = 0; j < 9; j++)
			{
				if (!GetCandidateStack(j).IsSolved && !GetCandidateStack(j).IsSingle && GetCandidateStack(j).HasCandidate(i))
				{
					num++;
					result = j;
				}
			}
			if (num == 1)
			{
				value = i;
				return result;
			}
		}
		return -1;
	}
}
