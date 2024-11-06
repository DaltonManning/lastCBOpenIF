using System.Globalization;
using System.Text;

namespace SearchNavigationTool;

public class Candidates
{
	private int[] m_nCandidates;

	private int m_nNrOfCandiadates;

	private bool m_bSolved;

	public int NoOfCandidates => m_nNrOfCandiadates;

	public bool IsSolved => m_bSolved;

	public bool IsSingle => m_nNrOfCandiadates == 1;

	public Candidates()
	{
		m_nCandidates = new int[9];
		Fill();
	}

	public void Reset()
	{
		m_nNrOfCandiadates = 0;
		m_bSolved = false;
	}

	public void Fill()
	{
		for (int i = 0; i < 9; i++)
		{
			m_nCandidates[i] = i + 1;
		}
		m_nNrOfCandiadates = 9;
		m_bSolved = false;
	}

	public int GetValue(int index)
	{
		if (index < 0 || index > 8)
		{
			return 0;
		}
		return m_nCandidates[index];
	}

	public void SetValue(int value)
	{
		if (value >= 1 && value <= 9)
		{
			m_nCandidates[0] = value;
			m_nNrOfCandiadates = 1;
			m_bSolved = true;
		}
	}

	public void AddValue(int value)
	{
		if (value < 1 || value > 9 || HasCandidate(value))
		{
			return;
		}
		int num = m_nNrOfCandiadates;
		bool flag = false;
		while (num > 0 && !flag)
		{
			if (m_nCandidates[num - 1] > value)
			{
				m_nCandidates[num] = m_nCandidates[num - 1];
				num--;
			}
			else
			{
				flag = true;
			}
		}
		m_nCandidates[num] = value;
		m_nNrOfCandiadates++;
	}

	public void Remove(int value)
	{
		int i = 0;
		bool flag = false;
		for (; i < m_nNrOfCandiadates; i++)
		{
			if (flag)
			{
				break;
			}
			flag = m_nCandidates[i] == value;
		}
		if (flag)
		{
			m_nNrOfCandiadates--;
		}
		for (; i < m_nNrOfCandiadates; i++)
		{
			m_nCandidates[i - 1] = m_nCandidates[i];
		}
	}

	public bool HasCandidate(int value)
	{
		for (int i = 0; i < m_nNrOfCandiadates; i++)
		{
			if (m_nCandidates[i] == value)
			{
				return true;
			}
		}
		return false;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("");
		for (int i = 0; i < m_nNrOfCandiadates; i++)
		{
			stringBuilder.Append(GetValue(i).ToString(CultureInfo.CurrentCulture));
		}
		return stringBuilder.ToString();
	}
}
