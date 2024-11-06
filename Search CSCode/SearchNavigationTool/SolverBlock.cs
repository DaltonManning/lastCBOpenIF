namespace SearchNavigationTool;

public class SolverBlock : SolverVector
{
	public SolverBlock(SolverCells solverCells, int index)
		: base(solverCells, index)
	{
	}

	public override Candidates GetCandidateStack(int index)
	{
		int num = index / 3;
		int column = index - num * 3;
		return GetCandidateStack(num, column);
	}

	public Candidates GetCandidateStack(int row, int column)
	{
		int num = nIndex / 3;
		num *= 3;
		int num2 = nIndex - num;
		num2 *= 3;
		num += row;
		num2 += column;
		return solverCells.GetCandidateStack(num, num2);
	}
}
