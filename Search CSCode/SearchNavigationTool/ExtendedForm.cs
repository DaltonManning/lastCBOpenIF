using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace SearchNavigationTool;

public class ExtendedForm : Form
{
	private Container components;

	private Board m_Board;

	private CellCollection m_Cells;

	private int m_nLevel;

	private MainMenu mnuSudoku;

	private MenuItem mnuFile;

	private MenuItem mnuOptions;

	private MenuItem mnuHelp;

	private MenuItem mnuFile_Exit;

	private MenuItem mnuHelp_About;

	private MenuItem mnuFile_New;

	private MenuItem mnuFile_New_Trivial;

	private MenuItem mnuFile_New_Easy;

	private StatusBar stbStatus;

	private Panel pnlBlack;

	private ButtonCollection m_Buttons;

	public ExtendedForm()
	{
		InitializeComponent();
		m_Board = new Board();
		m_Cells = new CellCollection(this);
		m_Cells.Enable = false;
		m_Buttons = new ButtonCollection(this);
		m_Buttons.Click += m_Buttons_Click;
		GenerateCells();
		GenerateButtons();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(SearchNavigationTool.ExtendedForm));
		this.mnuSudoku = new System.Windows.Forms.MainMenu();
		this.mnuFile = new System.Windows.Forms.MenuItem();
		this.mnuFile_New = new System.Windows.Forms.MenuItem();
		this.mnuFile_New_Trivial = new System.Windows.Forms.MenuItem();
		this.mnuFile_New_Easy = new System.Windows.Forms.MenuItem();
		this.mnuFile_Exit = new System.Windows.Forms.MenuItem();
		this.mnuOptions = new System.Windows.Forms.MenuItem();
		this.mnuHelp = new System.Windows.Forms.MenuItem();
		this.mnuHelp_About = new System.Windows.Forms.MenuItem();
		this.stbStatus = new System.Windows.Forms.StatusBar();
		this.pnlBlack = new System.Windows.Forms.Panel();
		base.SuspendLayout();
		this.mnuSudoku.MenuItems.AddRange(new System.Windows.Forms.MenuItem[3] { this.mnuFile, this.mnuOptions, this.mnuHelp });
		this.mnuFile.Index = 0;
		this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[2] { this.mnuFile_New, this.mnuFile_Exit });
		this.mnuFile.Text = "File";
		this.mnuFile_New.Index = 0;
		this.mnuFile_New.MenuItems.AddRange(new System.Windows.Forms.MenuItem[2] { this.mnuFile_New_Trivial, this.mnuFile_New_Easy });
		this.mnuFile_New.Text = "New";
		this.mnuFile_New_Trivial.Index = 0;
		this.mnuFile_New_Trivial.Text = "Trivial";
		this.mnuFile_New_Trivial.Click += new System.EventHandler(mnuFile_New_Trivial_Click);
		this.mnuFile_New_Easy.Index = 1;
		this.mnuFile_New_Easy.Text = "Easy";
		this.mnuFile_New_Easy.Click += new System.EventHandler(mnuFile_New_Easy_Click);
		this.mnuFile_Exit.Index = 1;
		this.mnuFile_Exit.Text = "Exit";
		this.mnuFile_Exit.Click += new System.EventHandler(mnuFile_Exit_Click);
		this.mnuOptions.Index = 1;
		this.mnuOptions.Text = "Options";
		this.mnuHelp.Index = 2;
		this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1] { this.mnuHelp_About });
		this.mnuHelp.Text = "Help";
		this.mnuHelp_About.Index = 0;
		this.mnuHelp_About.Text = "About Sudoku...";
		this.stbStatus.Location = new System.Drawing.Point(0, 539);
		this.stbStatus.Name = "stbStatus";
		this.stbStatus.Size = new System.Drawing.Size(480, 24);
		this.stbStatus.TabIndex = 0;
		this.stbStatus.Text = "Status";
		this.pnlBlack.BackColor = System.Drawing.SystemColors.ControlText;
		this.pnlBlack.Location = new System.Drawing.Point(0, 0);
		this.pnlBlack.Name = "pnlBlack";
		this.pnlBlack.Size = new System.Drawing.Size(480, 480);
		this.pnlBlack.TabIndex = 1;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		base.ClientSize = new System.Drawing.Size(480, 563);
		base.Controls.Add(this.pnlBlack);
		base.Controls.Add(this.stbStatus);
		base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
		base.Menu = this.mnuSudoku;
		base.Name = "ExtendedForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Sudoku";
		base.ResumeLayout(false);
	}

	[STAThread]
	private static void Main()
	{
		Application.Run(new ExtendedForm());
	}

	private void GenerateCells()
	{
		int num = 5;
		int num2 = num;
		int num3 = num;
		int num4 = (base.Width - 2 * num - 4) / 9;
		int num5 = num4 * 9 + 4;
		pnlBlack.Top = num - 2;
		pnlBlack.Left = num - 2;
		pnlBlack.Width = num5 + 2;
		pnlBlack.Height = num5 + 2;
		for (int i = 0; i < 9; i++)
		{
			num3 = num;
			for (int j = 0; j < 9; j++)
			{
				Cell cell = m_Cells.AddCell(num3, num2);
				cell.Height = num4;
				cell.Width = num4;
				cell.BringToFront();
				num3 += num4;
				if (j % 3 == 2)
				{
					num3++;
				}
			}
			num2 += num4;
			if (i % 3 == 2)
			{
				num2++;
			}
		}
	}

	private void GenerateButtons()
	{
		int top = base.Width;
		int num = base.Width / 4;
		int num2 = base.Width / 18;
		for (int i = 0; i < 9; i++)
		{
			m_Buttons.AddButton(num, top, num2);
			num += num2;
		}
		m_Buttons.SelectedButton = 0;
	}

	private void NewBoard(int Level, string LevelText)
	{
		string text = Text;
		int num = text.IndexOf("-", StringComparison.Ordinal);
		if (num > 0)
		{
			text = text.Substring(0, num - 1);
		}
		Text = text + " - " + LevelText;
		m_nLevel = Level;
		m_Board.Fill();
		Board board = m_Board.Clone();
		board.HideNumbers(m_nLevel);
		m_Cells.ClearContent();
		for (int i = 0; i < 9; i++)
		{
			Row row = m_Board.Row(i);
			for (int j = 0; j < 9; j++)
			{
				num = i * 9 + j;
				int value = row.GetValue(j);
				m_Cells[num].CorrectValue = value;
				m_Cells[num].StaticValue = board.Row(i).GetValue(j) != 0;
			}
		}
		m_Cells.SetSelectedValue(m_Buttons.SelectedButton + 1);
		m_Cells.Enable = true;
	}

	private void m_Buttons_Click(object sender, EventArgs e)
	{
		m_Cells.SetSelectedValue(m_Buttons.SelectedButton + 1);
	}

	private void mnuFile_Exit_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void mnuFile_New_Trivial_Click(object sender, EventArgs e)
	{
		NewBoard(0, ((MenuItem)sender).Text);
	}

	private void mnuFile_New_Easy_Click(object sender, EventArgs e)
	{
		NewBoard(1, ((MenuItem)sender).Text);
	}
}
