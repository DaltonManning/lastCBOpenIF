namespace SearchNavigationTool;

public class Row : EFVector
{
	public Row(Cells cells, int row)
		: base(cells, row)
	{
	}

	public override void Reset()
	{
		base.VectorCells.ResetRow(base.VectorIndex);
	}

	public override int GetValue(int index)
	{
		return base.VectorCells.GetValue(base.VectorIndex, index);
	}

	public override void SetValue(int index, int value)
	{
		base.VectorCells.SetValue(base.VectorIndex, index, value);
	}
}
