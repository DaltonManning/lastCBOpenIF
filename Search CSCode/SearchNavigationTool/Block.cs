namespace SearchNavigationTool;

public class Block : EFVector
{
	public Block(Cells cells, int index)
		: base(cells, index)
	{
	}

	public override void Reset()
	{
		base.VectorCells.ResetBlock(base.VectorIndex);
	}

	public override int GetValue(int index)
	{
		int num = index / 3;
		int column = index - num * 3;
		return GetValue(num, column);
	}

	public int GetValue(int row, int column)
	{
		int num = base.VectorIndex / 3;
		num *= 3;
		int num2 = base.VectorIndex - num;
		num2 *= 3;
		num += row;
		num2 += column;
		return base.VectorCells.GetValue(num, num2);
	}

	public override void SetValue(int index, int value)
	{
		int num = index / 3;
		int column = index - num * 3;
		SetValue(num, column, value);
	}

	public void SetValue(int row, int column, int value)
	{
		int num = base.VectorIndex / 3;
		num *= 3;
		int num2 = base.VectorIndex - num;
		num2 *= 3;
		num += row;
		num2 += column;
		base.VectorCells.SetValue(num, num2, value);
	}
}
