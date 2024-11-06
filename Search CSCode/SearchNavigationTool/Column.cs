namespace SearchNavigationTool;

public class Column : EFVector
{
	public Column(Cells cells, int column)
		: base(cells, column)
	{
	}

	public override void Reset()
	{
		base.VectorCells.ResetColumn(base.VectorIndex);
	}

	public override int GetValue(int index)
	{
		return base.VectorCells.GetValue(index, base.VectorIndex);
	}

	public override void SetValue(int index, int value)
	{
		base.VectorCells.SetValue(index, base.VectorIndex, value);
	}
}
