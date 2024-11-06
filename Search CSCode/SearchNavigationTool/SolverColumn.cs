namespace SearchNavigationTool;

public class SolverColumn : SolverVector
{
	public SolverColumn(SolverCells solverCells, int index)
		: base(solverCells, index)
	{
	}

	public override Candidates GetCandidateStack(int index)
	{
		return solverCells.GetCandidateStack(index, nIndex);
	}
}
