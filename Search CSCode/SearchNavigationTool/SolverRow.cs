namespace SearchNavigationTool;

public class SolverRow : SolverVector
{
	public SolverRow(SolverCells solverCells, int index)
		: base(solverCells, index)
	{
	}

	public override Candidates GetCandidateStack(int index)
	{
		return solverCells.GetCandidateStack(nIndex, index);
	}
}
