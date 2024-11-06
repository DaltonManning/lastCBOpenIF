using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CONTROLBUILDERLib;
using DeepSearchLib;

namespace SearchNavigationTool;

[Guid("AA21966D-0B72-4724-98B9-89D8D4823891")]
[ClassInterface(ClassInterfaceType.None)]
public class SearchNavigationCOMFacade : ISearchNavigationCOMFacade
{
	private delegate void ErrorMsgBoxDelegate(string str);

	private delegate void SetModeDelegate(CBModeType mode);

	private delegate void SetSearchResultDelegate(string result);

	private delegate void SetSearchStringsDelegate(string symbol, string path);

	private delegate void DoTheSearchDelegate();

	private static SearchNavigationForm snForm;

	private static CBSearchNavigationClass CBSN;

	private static DeepSearcher DS;

	private Thread snThread;

	private ErrorMsgBoxDelegate ErrorMsgBoxDge;

	private SetModeDelegate SetModeDge;

	private SetSearchResultDelegate SetSearchResultDge;

	private SetSearchStringsDelegate SetSearchStringsDge;

	private DoTheSearchDelegate DoTheSearchDge;

	private static CBModeType CBMode = CBModeType.Offline;

	private string symbolString = "";

	private string pathString = "";

	private string searchResultString = "";

	private static SNQueryMatchType oldMatchType = SNQueryMatchType.SN_QWholeWord;

	private static int oldMaxHitsInt = 100;

	private static string[] oldSearchInStrings;

	private static string[] oldSearchForStrings;

	private LocalStringClass Strings
	{
		get
		{
			if (snForm == null)
			{
				StartSNForm();
			}
			return snForm.Strings;
		}
	}

	public SearchNavigationCOMFacade()
	{
		CBSN = new CBSearchNavigationClass();
		if (CBSN == null)
		{
			throw new ArgumentNullException(CBSN.ToString());
		}
		DS = new DeepSearcher();
		if (DS == null)
		{
			throw new ArgumentNullException(DS.ToString());
		}
		oldSearchForStrings = new string[21];
		oldSearchInStrings = new string[21];
	}

	private void StartSNForm()
	{
		try
		{
			int systemLanguage = CBSN.GetSystemLanguage();
			CultureInfo cultureInfo = new CultureInfo(systemLanguage);
			snThread.CurrentCulture = cultureInfo;
			snThread.CurrentUICulture = cultureInfo;
			snForm = new SearchNavigationForm();
			snForm.SetSearchFor(symbolString);
			snForm.SetSearchIn(pathString);
			snForm.SetMode(CBMode);
			snForm.SetMatchType(oldMatchType);
			snForm.SetMaxHits(oldMaxHitsInt);
			snForm.SetSearchForInStrings(oldSearchForStrings, oldSearchInStrings);
			if (searchResultString.Length > 0 && CBMode == CBModeType.Offline)
			{
				snForm.SetResult(searchResultString);
			}
			snForm.CloseSNForm += CloseSNForm;
			snForm.RebuildScbDB += SNFormRebuildSourceCodeDB;
			snForm.Query += SNFormQuery;
			snForm.GotoPouEditor += SNFormGoToPOUEditor;
			snForm.GoToProjectExplorer += SNFormGotoProjExp;
			snForm.GetRegistryValue += SNFormGetRegistryValue;
			snForm.ExecuteSearchInstantly += SNFormExecuteSearchInstantly;
			snForm.OpenHelp += SNFormOpenHelp;
			snForm.StoreComboBoxListsAndFilterInComFacade += SNFormStoreComboBoxListsAndFilterInComFacade;
			ErrorMsgBoxDge = snForm.ErrorMsgBox;
			SetModeDge = snForm.SetMode;
			SetSearchStringsDge = snForm.SetSearchStrings;
			SetSearchResultDge = snForm.SetResult;
			DoTheSearchDge = snForm.DoTheSearch;
			DS.ReferenceFound += DS_ReferenceFound;
			DS.FirstSymbolFound += DS_FirstSymbolFound;
			if (CBMode != 0)
			{
				if (symbolString.Length == 0 || symbolString == "*")
				{
					snForm.DisableSearchButton();
				}
				else if (snForm.GetExecuteSearchInstantly)
				{
					snForm.DoTheSearch();
				}
			}
			snForm.ShowDialog();
		}
		catch (Exception)
		{
			CloseSNForm();
		}
	}

	private void DS_FirstSymbolFound(object sender, EventArgs e)
	{
		snForm.queryDSSymbolString = ((FoundSymbol)e).XmlSymbol.OuterXml;
		snForm.Addsymbol(CBSN.GetProjectName());
	}

	private void DS_ReferenceFound(object sender, EventArgs e)
	{
		snForm.queryDSResultString = ((SearchResult)e).XmlNode.OuterXml;
		snForm.AddReferences();
	}

	private void InitFormAndCB()
	{
		if (snForm == null && (snThread == null || !snThread.IsAlive))
		{
			snThread = new Thread(StartSNForm);
			snThread.Name = "SearchNavigationTool";
			snThread.TrySetApartmentState(ApartmentState.STA);
			snThread.Start();
		}
	}

	public void Open(string symbol, string path, CBModeType mode, string searchResult)
	{
		if (snForm == null)
		{
			symbolString = symbol;
			pathString = path;
			searchResultString = searchResult;
			CBMode = mode;
			InitFormAndCB();
			return;
		}
		SetMode(mode);
		object[] array = new object[2];
		array.SetValue(symbol, 0);
		array.SetValue(path, 1);
		snForm.Invoke(SetSearchStringsDge, array);
		object[] array2 = new object[1];
		array2.SetValue(searchResult, 0);
		snForm.Invoke(SetSearchResultDge, array2);
		if (CBMode != 0)
		{
			snForm.Invoke(DoTheSearchDge);
		}
	}

	public void Enable(bool enableGui)
	{
		if (snForm != null)
		{
			snForm.Enabled = enableGui;
		}
	}

	private void ErrorMsgBox(string str)
	{
		try
		{
			object[] array = new object[1];
			array.SetValue(str, 0);
			snForm.Invoke(ErrorMsgBoxDge, array);
		}
		catch (Exception)
		{
		}
	}

	public void SetMode(CBModeType mode)
	{
		try
		{
			object[] array = new object[1];
			array.SetValue(mode, 0);
			snForm.Invoke(SetModeDge, array);
		}
		catch (Exception ex)
		{
			ErrorMsgBox(ex.ToString());
		}
	}

	public void CloseSNForm()
	{
		if (snForm != null)
		{
			GC.Collect();
			snForm.Close();
			snForm = null;
		}
	}

	public void ReportModalDialog(bool on)
	{
		Enable(!on);
	}

	public void CloseCBConnection()
	{
		CloseSNForm();
		if (CBSN != null)
		{
			Marshal.ReleaseComObject(CBSN);
			CBSN = null;
		}
		if (DS != null)
		{
			Marshal.ReleaseComObject(DS);
			DS = null;
		}
	}

	public void SNFormRebuildSourceCodeDB(object sender)
	{
		CBSN.RebuildSearchData("");
	}

	internal string SNFormQuery(object sender, SearchNavigationForm.QueryEventArgs e)
	{
		try
		{
			oldMatchType = e.match;
			oldMaxHitsInt = e.maxNoHits;
			string text = "";
			if (e.cbMode == CBModeType.OnlineIterative || e.cbMode == CBModeType.TestModeIterative || e.cbMode == CBModeType.OnlineIterativeSearchFieldsDisabled || e.cbMode == CBModeType.TestModeIterativeSearchFieldsDisabled)
			{
				text = "";
				DS.DeepSearch(e.path, e.symbol);
			}
			else
			{
				text = CBSN.Query(e.path, e.symbol, e.match, e.maxNoHits);
			}
			return text;
		}
		catch (Exception ex)
		{
			ErrorMsgBox(ex.ToString());
			return "";
		}
	}

	public bool SNFormExecuteSearchInstantly()
	{
		return CBSN.ExecuteSearchInstantly();
	}

	private void GotoPOUEditorEventArgsFix(ref SearchNavigationForm.GoToPOUEditorEventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder("");
		string[] array = e.path.Split('.');
		StringBuilder stringBuilder2 = new StringBuilder("");
		string[] array2 = e.specification.Split('.');
		int num = 0;
		while (num < array.Length - 1)
		{
			stringBuilder.Append(array[num]);
			num++;
			if (num < array.Length - 1)
			{
				stringBuilder.Append(".");
			}
		}
		num = 0;
		while (num < array2.Length - 1)
		{
			stringBuilder2.Append(array2[num]);
			num++;
			if (num < array2.Length - 1)
			{
				stringBuilder2.Append(".");
			}
		}
		if (array2[array2.Length - 1] == Strings.FunctionBlock || array2[array2.Length - 1] == Strings.Parameter || array2[array2.Length - 1] == Strings.Variable || array2[array2.Length - 1] == Strings.GlobalVariable || array2[array2.Length - 1] == Strings.ExternalVariable || array2[array2.Length - 1] == Strings.StructComponent || array2[array2.Length - 1] == Strings.CommunicationVariable)
		{
			e.guiType = array2[array2.Length - 2];
			e.path = stringBuilder.ToString();
			e.specification = stringBuilder2.ToString();
		}
		else
		{
			e.guiType = array2[array2.Length - 1];
		}
	}

	private void RemoveCollectionObjectsFromPath(ref string path, bool ignoreProjectConstants)
	{
		string[] array = path.Split('.');
		StringBuilder stringBuilder = new StringBuilder("");
		int num = 0;
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (num >= 0 && (!Strings.IsCollectionObject(text) || (ignoreProjectConstants && text == Strings.ProjectConstants)))
			{
				stringBuilder.Append(text);
				if (num < array.Length - 1)
				{
					stringBuilder.Append(".");
				}
			}
			num++;
		}
		path = stringBuilder.ToString();
	}

	private static string RemoveHWChannelFromPath(ref string path)
	{
		StringBuilder stringBuilder = new StringBuilder("");
		string[] array = path.Split('.');
		int num = 0;
		while (num < array.Length - 1)
		{
			stringBuilder.Append(array[num]);
			num++;
			if (num < array.Length - 1)
			{
				stringBuilder.Append(".");
			}
		}
		path = stringBuilder.ToString();
		return array[array.Length - 1];
	}

	internal void SNFormGoToPOUEditor(object sender, SearchNavigationForm.GoToPOUEditorEventArgs e)
	{
		snForm.SetStatusBarText("", clearOld: true);
		RemoveCollectionObjectsFromPath(ref e.path, ignoreProjectConstants: false);
		RemoveCollectionObjectsFromPath(ref e.specification, ignoreProjectConstants: true);
		try
		{
			if (snForm.TopMost && snForm.WindowState == FormWindowState.Maximized)
			{
				snForm.WindowState = FormWindowState.Normal;
			}
			if (string.IsNullOrEmpty(e.position) && !string.IsNullOrEmpty(e.tab))
			{
				e.position = e.tab;
			}
			CBSN.OpenEditor(e.path, e.specification, e.row, e.position, "", "", e.element);
		}
		catch (COMException ex)
		{
			if (ex.ErrorCode == -2147218237)
			{
				ErrorMsgBox(ex.Message + " Try to Rebuild the Search and Navigation Data");
			}
			else
			{
				ErrorMsgBox(ex.Message);
			}
		}
		catch (Exception ex2)
		{
			ErrorMsgBox(ex2.Message);
		}
	}

	internal void SNFormGotoProjExp(object sender, SearchNavigationForm.GoToPOUEditorEventArgs e)
	{
		snForm.SetStatusBarText("", clearOld: true);
		RemoveCollectionObjectsFromPath(ref e.path, ignoreProjectConstants: false);
		RemoveCollectionObjectsFromPath(ref e.specification, ignoreProjectConstants: true);
		try
		{
			if (snForm.TopMost && snForm.WindowState == FormWindowState.Maximized)
			{
				snForm.WindowState = FormWindowState.Normal;
			}
			CBMode = SearchNavigationForm.Mode;
			while ((e.guiType == Strings.FunctionBlock && CBMode == CBModeType.Offline) || e.guiType == Strings.Parameter || e.guiType == Strings.Variable || e.guiType == Strings.GlobalVariable || e.guiType == Strings.ExternalVariable || e.guiType == Strings.StructComponent || e.guiType == Strings.CommunicationVariable)
			{
				GotoPOUEditorEventArgsFix(ref e);
			}
			if (e.guiType == Strings.IOChannelIn || e.guiType == Strings.IOChannelOut)
			{
				RemoveHWChannelFromPath(ref e.path);
			}
			if (e.guiType == Strings.AccessVariable)
			{
				SearchNavigationForm.GoToPOUEditorEventArgs goToPOUEditorEventArgs = e;
				goToPOUEditorEventArgs.path = goToPOUEditorEventArgs.path + "." + Strings.AccessVariables;
				SearchNavigationForm.GoToPOUEditorEventArgs goToPOUEditorEventArgs2 = e;
				goToPOUEditorEventArgs2.specification = goToPOUEditorEventArgs2.specification + "." + e.guiType;
			}
			if (e.tab == Strings.FunctionBlocks || e.tab == "Function blocks" || e.guiType == Strings.ControlModule)
			{
				SearchNavigationForm.GoToPOUEditorEventArgs goToPOUEditorEventArgs3 = e;
				goToPOUEditorEventArgs3.path = goToPOUEditorEventArgs3.path + "." + e.element;
			}
			RemoveTailingObjectsFromPath(ref e.path, ref e.specification);
			CBSN.SelectInProjectExplorer(e.path);
		}
		catch (COMException ex)
		{
			if (ex.ErrorCode == -2147218237)
			{
				ErrorMsgBox(ex.Message + " Try to Rebuild the Search and Navigation Data");
			}
			else
			{
				ErrorMsgBox(ex.Message);
			}
		}
		catch (Exception ex2)
		{
			ErrorMsgBox(ex2.Message);
		}
	}

	private static void RemoveTailingObjectsFromPath(ref string path, ref string specification)
	{
		string text = "";
		string[] array = path.Split('.');
		string[] array2 = specification.Split('.');
		int i = 0;
		int num = array2.Length;
		int num2 = array.Length;
		bool flag = false;
		if (array.Length > array2.Length && path.Contains(" "))
		{
			for (; i < num && i < num2; i++)
			{
				if (!flag && array[i].IndexOf(' ') > -1)
				{
					array[i] = array[i] + "." + array[i + 1];
					if (i + 2 < array.Length)
					{
						array[i + 1] = array[i + 2];
					}
					flag = true;
				}
			}
		}
		for (i = 0; i < num && i < num2 && array2[i] != "CodeBlock"; i++)
		{
			text += array[i];
			text += ".";
		}
		path = text.Substring(0, text.Length - 1);
	}

	internal string SNFormGetRegistryValue(object sender, SearchNavigationForm.GetRegistryValueEventArgs e)
	{
		try
		{
			return CBSN.GetRegistryValue(e.keyName);
		}
		catch (Exception ex)
		{
			ErrorMsgBox(ex.ToString());
			return "";
		}
	}

	public void SNFormOpenHelp()
	{
		CBSN.Help();
	}

	internal void SNFormStoreComboBoxListsAndFilterInComFacade(object sender, SearchNavigationForm.StoreComboBoxListsAndFilterInComFacadeEventArgs e)
	{
		try
		{
			e.searchForStrings.CopyTo(oldSearchForStrings, 0);
			e.searchInStrings.CopyTo(oldSearchInStrings, 0);
		}
		catch (Exception ex)
		{
			ErrorMsgBox(ex.ToString());
		}
	}
}
