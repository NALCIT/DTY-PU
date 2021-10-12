Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid



Public Class FormDTYPU
    Public ApplicationFontString As String = "Segoe UI Semibold"
    Private ProductionConnectionString As String = "Data source = 172.16.216.7; initial catalog= procreport; uid=strad; pwd=;"
    Private DTYCardConnectionString As String = "Data source = 172.16.216.7; initial catalog= StapleReport; uid=strad; pwd=;"
    Private WithEvents MyDetailedPUHistoryGridview As New MyDEVXGRIDVIEW(GridControl_PU)
    Private PUProductionTable As DataTable
    Private PUDetailedHistoryTable As DataTable
    Private RelationName As String = "PUHistory"
    Dim DetailedPUDataColumnText As String() = {"PU SET", "Days", "Assembly Date", "Actual Start Date", "M/C No", "Lot No", "Spec", "POY Lot", "MT Date", "FO", "User Name"}
    Dim DetailedPUDataColumnWidths As Integer() = {40, 50, 105, 105, 50, 60, 80, 70, 105, 90, 0, 0, 0}
    Dim TotalW As Integer = 50
    Dim TotalW2 As Integer = 60

    Public Sub New()
        InitializeComponent()
        ' SetChartPropertiesHistogram()
        PUDetailedHistoryTable = _
                POPULATE(ProductionConnectionString, "SELECT TOP 1 '' AS PU_SetNo, '' AS PUDays, '' AS AssemblyDate, '' AS StartDate, '' AS dt_mac,  '' AS DTY_LotNo, '' AS Spec, '' AS POYLot, '' AS MTDate,  '' AS FinishOil FROM dty_detail")
        PUDetailedHistoryTable.Rows.Clear()
        MyDetailedPUHistoryGridview = New MyDEVXGRIDVIEW(GridControl_PU)
    End Sub
    Private Sub FormDTYPU_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillCurrentProductionData()
        Dim NullPUHistoryBool As Boolean = (PUDetailedHistoryTable IsNot Nothing AndAlso PUDetailedHistoryTable.Rows.Count >= 1)
        If NullPUHistoryBool Then
            INITIALIZE_ENABLE_MASTER_DETAILS_RELATIONS(PUProductionTable, PUDetailedHistoryTable)
            Load_PUHistoryDetail_Gridview(PUDetailedHistoryTable)
        End If
        GridviewSetup()
        ToolStripComboBox_PUSelector.SelectedIndex = 0
        If ToolStripComboBox_PUSelector.SelectedIndex < 0 Then SplitContainer1.Panel2Collapsed = True Else SplitContainer1.Panel2Collapsed = False

        Me.Width = TotalW + TotalW2     'SET Form Width 1st
        SplitContainer1.SplitterDistance = Math.Max(780, TotalW)            'SET Split Distance 2nd
    End Sub
    Private Sub GridviewSetup()
        Dim ColumnText As String() = {"M/C No", "PU SET", "SET Days", "Assembly Date", "Lot No", "Spec", "POY Lot", "POY Oil", "Hard ness", "Stack", "Last MT Date"}
        Dim ColumnWidth As Integer() = {45, 50, 50, 95, 60, 80, 60, 90, 60, 70, 90}
        For Each MyColumn As GridColumn In GridView_OverView.Columns
            Dim MyColumnIndex As Integer = GridView_OverView.Columns.IndexOf(MyColumn)
            MyColumn.Width = ColumnWidth(MyColumnIndex)
            MyColumn.Caption = ColumnText(MyColumnIndex)
            TotalW = TotalW + MyColumn.Width
            If ColumnWidth(MyColumnIndex) = 0 Then MyColumn.Visible = False
        Next

        Dim HistoryColumnText2 As String() = {"PU SET", "Days", "Assembly Date", "Lot No", "M/C No", "Production Date", "User Name"}
        Dim HistoryColumnWidth2 As Integer() = {45, 50, 100, 70, 50, 100, 175}
        With DataGridView_AllTimeHistory
            For i As Integer = HistoryColumnWidth2.GetLowerBound(0) To HistoryColumnWidth2.GetUpperBound(0)
                .Columns.Add(HistoryColumnText2(i), HistoryColumnText2(i))
                .Columns(i).Width = HistoryColumnWidth2(i)
                TotalW2 = TotalW2 + .Columns(i).Width
            Next
        End With

        Using myGraphics As Graphics = Me.CreateGraphics()
            Dim HeaderFontSize As Single = 96 / myGraphics.DpiX * 12
            Dim RowFontSize As Single = 96 / myGraphics.DpiX * 11
            GridView_OverView.Appearance.HeaderPanel.Font = New Font(ApplicationFontString, HeaderFontSize, FontStyle.Bold)
            GridView_OverView.Appearance.Row.Font = New Font(ApplicationFontString, RowFontSize)
            MyDetailedPUHistoryGridview.Appearance.HeaderPanel.Font = New Font(ApplicationFontString, 96 / myGraphics.DpiX * 12, FontStyle.Bold)
            MyDetailedPUHistoryGridview.Appearance.Row.Font = New Font(ApplicationFontString, 96 / myGraphics.DpiX * 11, FontStyle.Regular)
            DataGridView_AllTimeHistory.ColumnHeadersDefaultCellStyle.Font = New Font(ApplicationFontString, HeaderFontSize, FontStyle.Bold)
            DataGridView_AllTimeHistory.RowsDefaultCellStyle.Font = New Font(ApplicationFontString, RowFontSize)
        End Using

    End Sub
    Private Sub FillCurrentProductionData()
        '===Set Master Datatable DTY_Detail Table (ProcReport.dbo) and Merge Field Data from Condition Table (StapleReport.dbo) 
        Dim TotalH As Integer = 55
        Dim OverViewString = "SELECT dt_mac, '' AS PU_SetNo, '' AS PU_Days, '' AS AssemblyDate, MAX(dt_lotno) AS dt_lotno, MAX(dt_spec) AS dt_sepc, " & _
                                            "MAX(dt_poylot) AS poyLot,  MAX(dt_poyoil) AS dt_poyoil, '' as HardNess, '' AS Stack, " & _
                                            "MAX(CONVERT(nvarchar, cast(dt_mtdate as datetime), 101)) AS dt_mtDate  " & _
                                            "FROM dty_detail WHERE " & _
                                            "(dt_pdate = '" & Format(Today.AddDays(-1), "yyyyMMdd") & "') " & _
                                            "GROUP BY   dt_mac " & _
                                            "ORDER BY dt_mac, dt_lotno, dt_mtdate"        '"(dt_poylot <> '' AND dt_lotno <> '' AND dt_totalbobins > 0) AND " & _
        PUProductionTable = POPULATE(ProductionConnectionString, OverViewString)
        If PUProductionTable IsNot Nothing AndAlso PUProductionTable.Rows.Count > 0 Then
            GridControl_PU.DataSource = PUProductionTable
            Dim MyView As GridView = GridControl_PU.MainView
            With MyView
                .RowHeight = 27
                For I As Integer = 0 To MyView.RowCount - 1
                    Dim MCRow As DataRow = .GetDataRow(I)
                    Dim MC As String = MCRow("dt_mac")
                    Dim DTYLot As String = MCRow("dt_lotno")
                    If DTYLot = "" Then DTYLot = "-"
                    Dim PUNo As String = "N/A" & Strings.Right(MC, 2)
                    Dim PUData As Dictionary(Of String, String) = GETPUDataList(DTYLot, MC)
                    If PUData IsNot Nothing AndAlso PUData.Count > 0 Then
                        PUNo = Trim(PUData("Set No"))
                        If DTYLot = "-" Or DTYLot = "-" Then PUNo = "NA" & Strings.Right(MC, 2)
                        Dim AssemblyDate As String = Format(Date.Parse(Trim(PUData("Assembly Date"))), "yyyy/MM/dd")
                        Dim PUDays As String = FillDetailedPUHistory(PUNo, AssemblyDate, MC)
                        Dim HardNess As String = Trim(PUData("Hardness"))
                        Dim Stack As String = Trim(PUData("Stack"))
                        .FocusedRowHandle = I
                        .SetFocusedRowCellValue("PU SET", PUNo)
                        .SetFocusedRowCellValue("SET Days", PUDays)
                        .SetFocusedRowCellValue("Assembly Date", AssemblyDate)
                        .SetFocusedRowCellValue("Hard ness", HardNess)
                        .SetFocusedRowCellValue("Stack", Stack)
                        With PUProductionTable
                            .Rows(I)("PU_SetNo") = PUNo
                            .Rows(I)("PU_Days") = PUDays
                            .Rows(I)("AssemblyDate") = AssemblyDate
                            .Rows(I)("HardNess") = HardNess
                            .Rows(I)("Stack") = Stack
                            .AcceptChanges()
                        End With
                    End If
                    TotalH = TotalH + MyView.RowHeight
                Next
                TotalH = TotalH + .ColumnPanelRowHeight + .ViewCaptionHeight
            End With
            Me.Height = TotalH + ChartControl1.Height
        End If
    End Sub
    Private Sub INITIALIZE_ENABLE_MASTER_DETAILS_RELATIONS(ByVal ThisMasterDatatable As DataTable, ByVal DetailPUHistoryData As DataTable)
        If DetailPUHistoryData.Rows.Count > 0 Then

            Dim DatasetOfDataRelations As New DataSet
            With DatasetOfDataRelations
                ThisMasterDatatable.TableName = "MasterProductionTable"
                DetailPUHistoryData.TableName = "DetailedPUHistoryTable"
                Dim ThisRelationName As String = RelationName
                Dim ThisDatasetIsNew As Boolean = (DatasetOfDataRelations.Tables.Count < 1)
                If ThisDatasetIsNew Then
                    .Tables.Add(ThisMasterDatatable)
                    .Tables.Add(DetailPUHistoryData)
                Else
                    .Relations.Remove(ThisRelationName)
                    .Tables(0).Constraints.Clear()
                    .Tables(1).Constraints.Clear()
                    .Tables.Clear()
                    .Tables.Add(ThisMasterDatatable)
                    .Tables.Add(DetailPUHistoryData)
                End If
                Try
                    Dim MasterColumn As DataColumn() = {ThisMasterDatatable.Columns("PU_SetNo")}
                    Dim DetailColumn As DataColumn() = {DetailPUHistoryData.Columns("PU_SetNo")}
                    Dim NewRelation As DataRelation = .Relations.Add(ThisRelationName, MasterColumn, DetailColumn)
                    GridControl_PU.LevelTree.Nodes.Add(NewRelation.RelationName, MyDetailedPUHistoryGridview)
                Catch ex As Exception
                    MsgBox("Possible Duplicate PU Set Or Missing Condition Card in History !")
                End Try
            End With
        End If
    End Sub
    Private Sub Load_PUHistoryDetail_Gridview(ByVal ThisDetailPUData As DataTable)
        With MyDetailedPUHistoryGridview
            .PopulateColumns(ThisDetailPUData)
            .SET_UP_HEADER(DetailedPUDataColumnText, DetailedPUDataColumnWidths)
            .ViewCaption = "PU Life History"
            .OptionsView.ShowAutoFilterRow = False
            .OptionsView.ShowFooter = False

            'From Expand_details Sub
            .Appearance.FocusedRow.BackColor = Color.Green
            .Appearance.FocusedRow.ForeColor = Color.Black
            '----------------------------------------------------------------------------------------------------------------------------------------------
            'FORCE Gridview to Refresh and Update ALL Views, To Enable Master-Detail Mode for 1st time
            Dim MyPUGridview As GridView = GridControl_PU.MainView
            MyPUGridview.Columns(0).Width = MyPUGridview.Columns(0).Width + 1
            MyPUGridview.Columns(0).Width = MyPUGridview.Columns(0).Width - 1
            '----------------------------------------------------------------------------------------------------------------------------------------------
        End With
    End Sub
    Private Sub Refresh_ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Refresh.Click
        Cursor = Cursors.WaitCursor
        PUDetailedHistoryTable.Rows.Clear()
        FillCurrentProductionData()
        Dim TempPUhistoryTable As DataTable = PUDetailedHistoryTable.Clone
        For Each PURow As DataRow In PUDetailedHistoryTable.Rows
            TempPUhistoryTable.ImportRow(PURow)
        Next
        INITIALIZE_ENABLE_MASTER_DETAILS_RELATIONS(PUProductionTable, TempPUhistoryTable)
        Load_PUHistoryDetail_Gridview(PUDetailedHistoryTable)
        Cursor = Cursors.Default
    End Sub
    Private Sub PUSelector_Change_ToolStripComboBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripComboBox_PUSelector.SelectedIndexChanged
        Cursor = Cursors.WaitCursor
        Dim SelectedPUValue As String = ToolStripComboBox_PUSelector.Text
        Dim AssemblyDateString As String = GETLastAssemblyDate(SelectedPUValue)
        If AssemblyDateString IsNot Nothing Then
            Dim GetAllTimeHistoryData As DataTable = GetAllTimePUHistory(SelectedPUValue, AssemblyDateString)
            If GetAllTimeHistoryData IsNot Nothing AndAlso GetAllTimeHistoryData.Rows.Count > 0 Then
                DataGridView_AllTimeHistory.Rows.Clear()
                ShowPUAllTimeHistory(GetAllTimeHistoryData)
                Dim ThisHistogramData As DataTable = GroupPUByAssemblyDate(GetAllTimeHistoryData)
                'Add_Histogram(ThisHistogramData)
                Add_Histogram_New(ThisHistogramData)
            Else
                MsgBox("No Info is available for this Set No")
            End If
        End If
        Panel1.Select()
        Cursor = Cursors.Default

    End Sub
    Private Function GETLastAssemblyDate(ByVal ThisPUSet As String) As String
        Dim LastAssemblyDateString As String = _
                                                                    "SELECT CONVERT(nvarchar, CAST(PU_Assemble_Date AS datetime), 101) AS AssemblyDate, Create_pDate,  MC_No   " & _
                                                                    "FROM  (SELECT   TOP 1 PU_Assemble_Date, Create_pDate,  MC_No    " & _
                                                                    "FROM  Dty_Cond_Hist AS A " & _
                                                                    "WHERE (PU_SetNo = '" & ThisPUSet & "') " & _
                                                                    "ORDER BY     Create_pDate DESC) AS B " & _
                                                                    "UNION ALL " & _
                                                                    "SELECT  TOP 1 CONVERT(nvarchar, CAST(PU_AssembleDate AS datetime), 101) AS AssemblyDate, Create_pDate,  MC_No  " & _
                                                                    "FROM DTY_TMTCond_Hist_NEW " & _
                                                                    "WHERE            (LTRIM(PU_SetNo) = '" & ThisPUSet & "') " & _
                                                                    "ORDER BY     Create_pDate DESC"
        Dim LastAssemblyDateData As DataTable = POPULATE(DTYCardConnectionString, LastAssemblyDateString)
        Return LastAssemblyDateData.Rows(0)("AssemblyDate")
    End Function
    Private Function GETPUDataList(ByVal ThisLot As String, ByVal ThisMC As String) As Dictionary(Of String, String)
        '=== FROM Production Data, get the PU Data (Set No, Assembly Date, HardNess) for each MC/DTY Lot as a List
        Dim PUTableName As String = "Dty_Cond_Now"
        Select Case ThisLot.Substring(0, 1)
            Case "1"
                ThisLot = ThisLot.Substring(0, Math.Min(5, ThisLot.Length))
            Case "-"
                ThisLot = ""
            Case Else
                ThisLot = ThisLot.Substring(0, 4)
        End Select
        Dim GETPUString As String = "SELECT PU_SetNo, DTY_LotNo, CONVERT(nvarchar, CAST(PU_Assemble_Date AS datetime), 111) AS AssemblyDate, PU_hardness, PU_Stack " &
                                                    "FROM " & PUTableName & " WHERE " &
                                                    "(DTY_LotNo LIKE '" & Strings.Replace(ThisLot, "1", "", 1, 1) & "%') AND " & vbCrLf & "(MC_No = '" & ThisMC & "') " & vbCrLf &
                                                    "UNION ALL " & " " &
                                                    "SELECT PU_SetNo, DTY_LotNo, CONVERT(nvarchar, CAST(PU_Assemble_Date As datetime), 111) As AssemblyDate, PU_hardness, PU_Stack " &
                                                    "FROM [Dty_Cond_Hist] WHERE " &
                                                    "(DTY_LotNo LIKE '" & Strings.Replace(ThisLot, "1", "", 1, 1) & "%') AND " & vbCrLf & "(MC_No = '" & ThisMC & "') " & vbCrLf &
                                                    "ORDER BY   AssemblyDate DESC "

        If ThisMC = "115" Or ThisMC = "116" Then
            PUTableName = "Dty_TMT_CondNow_new "
            GETPUString = "SELECT PU_SetNo, DTY_LotNo, CONVERT(nvarchar, CAST(PU_AssemleDate AS Datetime), 111) AS AssemblyDate, PU_HardNess, SUBSTRING(PU_Stack, 1,8) as PU_Stack " &
                                    "FROM " & PUTableName & " WHERE " &
                                    "(DTY_LotNo LIKE '%" & Strings.Replace(ThisLot, "1", "", 1, 1) & "%') AND " & vbCrLf & "(MC_No = '" & ThisMC & "') AND " & vbCrLf & "(LEN(PU_Stack) > 5) " & vbCrLf &
                                    "UNION ALL " & " " &
                                    "SELECT PU_SetNo, DTY_LotNo, CONVERT(nvarchar, CAST(PU_AssembleDate As datetime), 111) As AssemblyDate, PU_hardness, PU_Stack " &
                                   "FROM [DTY_TMTCond_Hist_New] WHERE " &
                                   "(DTY_LotNo LIKE '%" & Strings.Replace(ThisLot, "1", "", 1, 1) & "%') AND " & vbCrLf & "(MC_No = '" & ThisMC & "') AND " & vbCrLf & "(LEN(PU_Stack) > 5) " & vbCrLf &
                                    "ORDER BY   AssemblyDate DESC"
        End If
        Dim PUData As DataTable = POPULATE(DTYCardConnectionString, GETPUString)
        If PUData.Rows.Count > 0 Then
            Dim PUList As New Dictionary(Of String, String)
            With PUList
                If Not PUData.Rows(0).IsNull("PU_SetNo") Then
                    .Add("Set No", Trim(PUData.Rows(0)("PU_SetNo")))
                    .Add("Assembly Date", Trim(PUData.Rows(0)("AssemblyDate")))
                    .Add("Hardness", Trim(PUData.Rows(0)("PU_Hardness")))
                    .Add("Stack", Strings.Replace(Trim(PUData.Rows(0)("PU_Stack")), ",", ""))
                    Return PUList
                End If
            End With
        End If
        Return Nothing
    End Function
    Private Function FillDetailedPUHistory(ByVal ThisPUSet As String, ByVal ThisDate As String, ByVal ThisMC As String) As String
        '===Fill UP the Detail PUHistory Datatable with Each PU Set/MC, and Get PU Days ==================
        Dim GetLotList As List(Of String) = GetLotsList(ThisPUSet, ThisDate, ThisMC)
        Dim GetLotString As String = GetLotsQueryString(GetLotList)
        Dim QueryLots As String = "dt_lotno LIKE '"
        Dim SearchIndex As Integer = 0
        For Each LotString As String In GetLotList
            QueryLots = QueryLots + LotString.Insert(Len(LotString), "%' OR dt_lotno LIKE '")
            QueryLots = QueryLots + Strings.Replace(LotString.Insert(Len(LotString), "%'"), "1", "")
            If GetLotList.IndexOf(LotString, SearchIndex) < GetLotList.Count - 1 Then
                QueryLots = QueryLots + " OR dt_lotno LIKE '"
                SearchIndex += 1
            End If
        Next
        QueryLots = QueryLots & If(GetLotList.Count = 0, Chr(39), "") & ")"
        Dim GETPUDaysString As String = _
                                                            "SELECT '" & ThisPUSet & "' AS PU_SetNo, COUNT(dt_pdate) AS PUDays, '" & ThisDate & "' AS AssemblyDate, " & vbCrLf & _
                                                            "MIN(SUBSTRING(dt_pdate,1,4) + '/' + SUBSTRING(dt_pdate,5,2) + '/' + SUBSTRING(dt_pdate,7,2)) AS StartDate, dt_mac, " & vbCrLf & _
                                                            "MAX(CASE WHEN SUBSTRING(dt_lotno, 1, 1) = '1' THEN SUBSTRING(dt_lotno, 1, 5) ELSE '1' + SUBSTRING(dt_lotno, 1, 4) END) AS DTY_LotNo,   " & vbCrLf & _
                                                            "MAX(dt_spec) AS Spec, MAX(dt_poylot) AS POYLot, " & vbCrLf & _
                                                            "MIN(REPLACE(SUBSTRING(CONVERT(Nvarchar, dt_mtdate, 121),1,10), '-', '/')) AS MTdate,  " & vbCrLf & _
                                                            "MAX(dt_poyoil) AS FinishOil  " & vbCrLf & _
                                                            "FROM dty_detail WHERE  " & vbCrLf & _
                                                            "(dt_pdate >= '" & Strings.Replace(ThisDate, "/", "") & "')  AND " & _
                                                            "(Dt_totalBobins > 0)  AND " & _
                                                            "(dt_lotno IN " & GetLotString & "  OR " & _
                                                            QueryLots & " " & _
                                                            "GROUP BY dt_mac, CASE WHEN SUBSTRING(dt_lotno, 1, 1) = '1' THEN SUBSTRING(dt_lotno, 1, 5) ELSE '1' + SUBSTRING(dt_lotno, 1, 4) END " & vbCrLf & _
                                                            "ORDER BY StartDate DESC"
        Dim GETPUDaysData As DataTable = POPULATE(ProductionConnectionString, GETPUDaysString)
        If GETPUDaysData IsNot Nothing And GETPUDaysData.Rows.Count > 0 Then
            Dim CumRunningDays As Integer = 0
            For Each PUDaysRow As DataRow In GETPUDaysData.Rows
                PUDetailedHistoryTable.ImportRow(PUDaysRow)
                CumRunningDays = CumRunningDays + PUDaysRow("PUDays")
            Next
            Return CumRunningDays
        Else
            Return 0
        End If
    End Function
    Private Function GetLotsList(ByVal ThisPUSet As String, ByVal ThisAssemblyDate As String, ByVal ThisMC As String) As List(Of String)
        Dim TMTMC As String() = {"115", "116"}
        Dim SelectLotString As String = "SELECT DTY_LotNo  FROM  Dty_Cond_Hist " & _
                                                        "WHERE (PU_SetNo = '" & ThisPUSet & "') AND " & _
                                                        "(CONVERT(datetime, PU_Assemble_Date)  >= '" & ThisAssemblyDate & "') " & _
                                                        "GROUP BY DTY_LotNo "
        If TMTMC.Contains(ThisMC) Then
            SelectLotString = "SELECT SUBSTRING(DTY_LotNo, 1, 5) AS DTY_LotNo " & _
                                        "FROM  Dty_TMTCond_Hist_New WHERE " & _
                                        "(LTRIM(PU_SetNo) = '" & ThisPUSet & "') AND " & _
                                        "(CONVERT(datetime, PU_AssembleDate)  >= '" & ThisAssemblyDate & "') " & _
                                        "GROUP BY SUBSTRING(DTY_LotNo, 1, 5)"
        End If
        Dim SelectLotData As DataTable = POPULATE(DTYCardConnectionString, SelectLotString)
        Dim LotsStringList As New List(Of String)
        For Each LotsRunData As DataRow In SelectLotData.Rows
            Dim ThisLot As String = LotsRunData("dty_lotNO").ToString
            If TMTMC.Contains(ThisMC) Then
                LotsStringList.Add(Strings.Mid(ThisLot, 1, 5))
            Else
                LotsStringList.Add("1" + Strings.Mid(ThisLot, 1, 4))
            End If
        Next
        Return LotsStringList
    End Function
    Private Function GetLotsQueryString(ByVal ThisLotsString As List(Of String)) As String
        '===Converts the List of Lots for this MC into Query String used to get PU Days
        Dim LotsString As String = "("
        For Each ThisLot As String In ThisLotsString
            LotsString = LotsString + Chr(39) + Strings.Mid(ThisLot, 1, 5) + Chr(39)
            If ThisLotsString.IndexOf(ThisLot) <= ThisLotsString.Count - 1 Then
                LotsString = LotsString + ", "
            End If
        Next
        Dim LastCommaIndex As Integer = InStrRev(LotsString, "',", -1)
        If Trim(Strings.Right(LotsString, 2)) = "," Or LastCommaIndex > 0 Then
            LotsString = Strings.Mid(LotsString, 1, LastCommaIndex)
        End If
        LotsString = LotsString & ")"
        If LotsString = "()" Then LotsString = "('')"
        Return LotsString
    End Function
    Private Function GETPUDaysAgain(ByVal ThisPUSet As String, ByVal ThisDate As String, ByVal ThisMC As String, ByVal ThisLot As String) As String
        Dim GETPUDaysString As String = _
                                                            "SELECT SUM(CountOfDays) AS PUDays " & _
                                                            "FROM     (SELECT  COUNT(dt_pdate) AS CountOfDays " & _
                                                            "FROM dty_detail WHERE  " & _
                                                            "(dt_pdate >= '" & Strings.Replace(ThisDate, "/", "") & "')  AND " & _
                                                            "(Dt_totalBobins > 0)  AND " & _
                                                            "(SUBSTRING(dt_lotno, 1, 5) LIKE ('" & ThisLot & "%') OR SUBSTRING(dt_lotno, 1, 4) LIKE ('" & Strings.Mid(ThisLot, 2, 4) & "%'))  " & _
                                                            "GROUP BY  dt_mac, CASE WHEN SUBSTRING(dt_lotno, 1, 1) = '1' THEN SUBSTRING(dt_lotno, 1, 5) ELSE '1' + SUBSTRING(dt_lotno, 1, 4) END , SUBSTRING(dt_pdate, 1, 4)) AS A"

        Dim GETPUDaysData As DataTable = POPULATE(ProductionConnectionString, GETPUDaysString)
        If GETPUDaysData IsNot Nothing AndAlso GETPUDaysData.Rows.Count > 0 AndAlso Not GETPUDaysData.Rows(0).IsNull("PUDays") Then Return GETPUDaysData.Rows(0)("PUDays")
        Return 0
    End Function
    Private Function GetAllTimePUHistory(ByVal ThisSetNo As String, ByVal ThisAssemblyDate As String) As DataTable
        'Get ALL Time History of Certain Set No, ThisAssemblyDate (MM/dd/yy) has wrong format from Create_Pdate (yyyy/MM/dd) hence=======================
        'PU_AssemblyDate IS Grouped by MINIMUM
        Dim GetHistoryString As String = "SELECT   MAX(PU_SetNo) AS PU_SetNo, MIN(Create_pDate) AS StartDate, REPLACE(MAX(DTY_LotNo), 1, '') AS DTY_LotNo, " & vbCrLf & _
                                                        "MIN(CONVERT(nvarchar, CAST(PU_Assemble_Date AS datetime), 111)) AS AssemblyDate, CAST(0 AS Numeric) AS PUDays, MC_No, " & vbCrLf & _
                                                        "MAX(dUserName) AS dUserName, " & vbCrLf & _
                                                        "SUBSTRING(CONVERT(nvarchar, CAST(PU_Assemble_Date AS datetime), 111), 1, 10) AS SubGroupDate " & vbCrLf & _
                                                        "FROM Dty_Cond_Hist WHERE " & vbCrLf & _
                                                        "(PU_SetNo = '" & ThisSetNo & "')  AND " & vbCrLf & _
                                                        "(DTY_LotNo <> '')  AND " & vbCrLf & _
                                                        "(Create_pdate >= '" & ThisAssemblyDate & "') " & vbCrLf & _
                                                        "GROUP BY   Substring(DTY_LotNo,1,4), MC_No, SUBSTRING(CONVERT(nvarchar, CAST(PU_Assemble_Date AS datetime), 111), 1, 10) " & vbCrLf & _
                                                        "UNION ALL " & vbCrLf & _
                                                        "SELECT  MAX(PU_SetNo) AS PU_SetNo, MIN(Create_pDate) AS StartDate, MAX(DTY_LotNo) AS DTY_LotNo, " & vbCrLf & _
                                                        "MIN(CONVERT(nvarchar, CAST(PU_AssembleDate AS datetime), 111)) AS AssemblyDate, CAST(0 AS Numeric) AS PUDays, MC_No, " & vbCrLf & _
                                                        "MAX(dUserName) AS dUserName, " & vbCrLf & _
                                                        "SUBSTRING(CONVERT(nvarchar, CAST(PU_AssembleDate AS datetime), 111), 1, 10) AS SubGroupDate " & vbCrLf & _
                                                        "FROM Dty_TMTCond_Hist_New  WHERE " & vbCrLf & _
                                                        "(PU_SetNo = '" & ThisSetNo & "')  AND " & vbCrLf & _
                                                        "(DTY_LotNo <> '')  AND " & vbCrLf & _
                                                        "(Create_pdate >= '" & ThisAssemblyDate & "') " & vbCrLf & _
                                                        "GROUP BY   Substring(DTY_LotNo,1,5), MC_No, SUBSTRING(CONVERT(nvarchar, CAST(PU_AssembleDate AS datetime), 111), 1, 10)  " & vbCrLf & _
                                                        "ORDER BY SUBSTRING(CONVERT(nvarchar, CAST(PU_Assemble_Date AS datetime), 111), 1, 10) DESC "
        'Changed SUBSTRING(CONVERT(nvarchar, CAST(PU_Assemble_Date AS datetime), 111), 1, 7) as SubGroupDate 2019/07/15
        Dim PUHistoryData As DataTable = POPULATE(DTYCardConnectionString, GetHistoryString)
        Return PUHistoryData
    End Function
    Private Sub ShowPUAllTimeHistory(ByVal ThisPUHistoryData As DataTable)
        '====
        Dim PUSet As String = ""
        Dim CreateDate As String = ""
        Dim DTYLot As String = ""
        Dim PUAssemblyDate As String = ""
        Dim MCNo As String = ""
        Dim PUDays As String = Nothing
        Dim CreateUser As String = ""
        For Each PUHistoryRow As DataRow In ThisPUHistoryData.Rows
            If PUHistoryRow.IsNull("PU_SetNo") = False Then PUSet = PUHistoryRow("PU_SetNo")
            If PUHistoryRow.IsNull("StartDate") = False Then CreateDate = PUHistoryRow("StartDate")
            If PUHistoryRow.IsNull("DTY_LotNo") = False Then DTYLot = PUHistoryRow("DTY_LotNo")
            If PUHistoryRow.IsNull("AssemblyDate") = False Then PUAssemblyDate = PUHistoryRow("AssemblyDate")
            If PUHistoryRow.IsNull("MC_No") = False Then MCNo = PUHistoryRow("MC_No")
            If PUHistoryRow.IsNull("dUserName") = False Then CreateUser = PUHistoryRow("dUserName") Else CreateUser = "'"
            If PUHistoryRow.IsNull("PUDays") = False Then
                Dim ThisLot As String = 1 & Strings.Left(DTYLot, 4)
                If MCNo = "115" Or MCNo = "116" Then ThisLot = Strings.Left(DTYLot, 5)
                PUDays = GETPUDaysAgain(PUSet, CreateDate, MCNo, ThisLot)
                PUHistoryRow("PUDays") = PUDays
            End If
            With DataGridView_AllTimeHistory
                .Rows.Add(PUSet, PUDays, PUAssemblyDate, DTYLot, MCNo, CreateDate, CreateUser)
            End With
        Next
        SplitContainer1.Panel2Collapsed = False
    End Sub
    Private Function GroupPUByAssemblyDate(ByVal ThisAllPUHistoryData As DataTable) As DataTable
        '====Group PUAllTimeHistory by assembly date, and add the PU Life for Charting Purpose
        Dim ThisModifiedPUHistoryData As DataTable = ThisAllPUHistoryData.Clone
        Dim PUSet As String = Nothing
        Dim CreateDate As String = Nothing
        Dim DTYLot As String = Nothing
        Dim PUAssemblyDate As String = Nothing
        Dim MCNo As String = Nothing
        Dim PUDays As Integer = Nothing
        Dim PreviousRow As DataRow = Nothing
        Dim PreviousAssemblyDate As String = Nothing
        For Each PUHistoryRow As DataRow In ThisAllPUHistoryData.Rows
            If PUHistoryRow.IsNull("PU_SetNo") = False Then PUSet = PUHistoryRow("PU_SetNo") Else PUSet = "'"
            If PUHistoryRow.IsNull("StartDate") = False Then CreateDate = PUHistoryRow("StartDate") Else CreateDate = "'"
            If PUHistoryRow.IsNull("DTY_LotNo") = False Then DTYLot = PUHistoryRow("DTY_LotNo") Else DTYLot = "'"
            If PUHistoryRow.IsNull("AssemblyDate") = False Then PUAssemblyDate = PUHistoryRow("AssemblyDate") Else PUAssemblyDate = "'"
            If PUHistoryRow.IsNull("MC_No") = False Then MCNo = PUHistoryRow("MC_No") Else MCNo = "'"
            If Not PUHistoryRow.IsNull("PUDays") Then
                Dim ThisLot As String = 1 & Strings.Left(DTYLot, 4)
                If MCNo = "115" Or MCNo = "116" Then ThisLot = Strings.Left(DTYLot, 5)
                PUDays = GETPUDaysAgain(PUSet, CreateDate, MCNo, ThisLot)
                PUHistoryRow("PUDays") = PUDays
            End If
            If PreviousAssemblyDate IsNot Nothing Then
                Dim ThresholdDays As Integer = 5
                Dim PUAssemblyThreshold As Integer = DateDiff(DateInterval.Day, Date.Parse(PUAssemblyDate), Date.Parse(PreviousAssemblyDate))
                Dim SamePUAssemblyDateBool As Boolean = (PUAssemblyThreshold <= ThresholdDays And PUAssemblyThreshold >= -ThresholdDays)
                If SamePUAssemblyDateBool Then
                    PreviousRow("PUDays") = Integer.Parse(PreviousRow("PUDays")) + PUDays
                Else
                    If PreviousRow IsNot Nothing AndAlso PreviousRow("PUDAYS") > 0 Then ThisModifiedPUHistoryData.ImportRow(PreviousRow)
                    PreviousRow = PUHistoryRow
                    PreviousAssemblyDate = PUAssemblyDate
                End If
            Else
                PreviousRow = PUHistoryRow
                PreviousAssemblyDate = PUAssemblyDate
            End If
        Next
        If PreviousRow IsNot Nothing AndAlso PreviousRow("PUDAYS") > 0 Then ThisModifiedPUHistoryData.ImportRow(PreviousRow)
        ThisModifiedPUHistoryData.AcceptChanges()
        Return ThisModifiedPUHistoryData
    End Function
    Private Sub GridView_OverView_CellContentClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles _
            GridView_OverView.RowCellClick
        If e.RowHandle > -1 Then
            Dim MyView As GridView = GridControl_PU.MainView
            With MyView
                Dim OriginalPUValue = Trim(.GetFocusedRowCellDisplayText("PU_SetNo").ToString)
                Dim CurrentColumn As GridColumn = .FocusedColumn
                Select Case Strings.Replace(CurrentColumn.Name, "col", "")
                    Case "PU_SetNo"
                        If OriginalPUValue <> "" Then
                            ToolStripComboBox_PUSelector.Text = OriginalPUValue
                        End If
                    Case Else
                        If Me.Width <= 1000 Then
                            Me.Width = TotalW + TotalW2     'SET Form Width 1st
                            SplitContainer1.SplitterDistance = TotalW            'SET Split Distance 2nd
                        End If
                End Select
            End With
        End If
    End Sub
    Private Sub PUSelector_ToolStripComboBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripComboBox_PUSelector.MouseDown
        If ToolStripComboBox_PUSelector.DroppedDown = False Then
            ToolStripComboBox_PUSelector.DroppedDown = True
        End If
    End Sub

    Private Function POPULATE(ByVal ThisConnectionString As String, ByVal ThisCommandString As String) As DataTable
        Dim MyTable As New DataTable
        Using Connection As New SqlClient.SqlConnection(ThisConnectionString), MyCommand As New SqlClient.SqlCommand(ThisCommandString, Connection)
            Connection.Open()
            MyTable.Locale = Globalization.CultureInfo.InvariantCulture
            Dim MyAdapter As New SqlClient.SqlDataAdapter
            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(MyTable)
        End Using
        Return MyTable
    End Function
    Private Sub Add_Histogram_New(ByVal ThisData As DataTable)
        Dim FontString As String = "Segoe UI"
        If ThisData.Rows.Count > 0 Then
            With ChartControl1
                If .Series.Count > 0 Then
                    .Series.Clear()
                End If
                .DataSource = ThisData
                .Legend.Font = New Font(FontString, 11)
                .Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left
                Dim XSeries As New DevExpress.XtraCharts.Series("Life Days", DevExpress.XtraCharts.ViewType.Bar)
                .Series.Add(XSeries)
                With XSeries
                    .ArgumentDataMember = "AssemblyDate"
                    .ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Auto
                    .ValueDataMembers.AddRange(New String() {"PUDays"})
                    .ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical
                    .Label.Antialiasing = True
                    .Label.Font = New Font(FontString, 10)
                    .Label.Border.Visible = False
                    .Label.LineVisible = False
                    .Label.TextAlignment = StringAlignment.Near
                    .Label.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty
                    .LabelsVisibility = True
                    .Label.TextColor = Color.Black
                    .CrosshairEnabled = False
                    .CrosshairHighlightPoints = False
                    .View.Color = Color.OrangeRed
                    Dim PULifeView As DevExpress.XtraCharts.BarSeriesView = CType(XSeries.View, DevExpress.XtraCharts.BarSeriesView)
                    PULifeView.Color = Color.Orange
                    PULifeView.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid
                    Dim HistoDiagram As DevExpress.XtraCharts.XYDiagram = CType(ChartControl1.Diagram, DevExpress.XtraCharts.XYDiagram)
                    If HistoDiagram IsNot Nothing Then
                        With HistoDiagram
                            .AxisX.GridLines.Visible = True
                            .AxisX.Label.Angle = 270
                            .AxisX.Label.Font = New Font(FontString, 10)
                            .AxisY.Label.Font = New Font(FontString, 10)
                            .AxisX.Label.EnableAntialiasing = True
                            .AxisY.Label.EnableAntialiasing = True
                            .AxisY.Title.Text = "Days"
                            .AxisY.Title.Font = New Font(FontString, 10)
                            .AxisY.Title.Visible = True
                            .AxisX.Title.Text = "PU Assembly Date"
                            .AxisX.Title.Font = New Font(FontString, 11)
                            .AxisX.Title.Visible = True
                            .AxisX.Reverse = True
                        End With
                    End If
                    'Dim MyLineView As DevExpress.XtraCharts.LineSeriesView = CType(XSeries.View, DevExpress.XtraCharts.LineSeriesView)
                    'Dim DV As New DataView(ThisData)
                    'Dim HistogramSeries As New Series
                    'HistogramSeries = .Series.Add("Life Days")
                    'With HistogramSeries
                    '    .Color = Color.Orange
                    '    .ChartType = SeriesChartType.StackedColumn
                    '    .ChartArea = "ChartArea1"
                    '    .Font = New Font(ApplicationFontString, 10, FontStyle.Bold)
                    '    .IsXValueIndexed = True
                    '    .IsValueShownAsLabel = True
                    '    .Points.DataBindXY(DV, "AssemblyDate", DV, "PUDays")
                    '    Dim MaxValue As DataPoint = .Points.FindMaxByValue()
                    '    Chart2.ChartAreas("ChartArea1").AxisY.Maximum = MaxValue.YValues(0) + 10
                    'End With
                End With
            End With
        End If
    End Sub


    '=============NTO USED===========
    Private Function GetPUHistoryNEW(ByVal ThisPUSet As String, ByVal ThisDate As String, Optional ByVal ThisMC As String = "101") As DataTable
        '=====NOT USED============================================================================
        Dim GetLotString As String = GetLots(ThisPUSet, ThisDate, ThisMC)
        Dim GetHistoryString As String = "SELECT dt_mac AS MC_No, SUBSTRING(dt_lotno, 1, 5) AS DTY_LotNo, MAX(dt_poylot) AS POYLot, MAX(dt_spec) AS Spec, MAX(dt_poyoil) AS FinishOil, " & _
                                                    "MIN(SUBSTRING(dt_pdate,1,4) + '/' + SUBSTRING(dt_pdate,5,2) + '/' + SUBSTRING(dt_pdate,7,2)) AS StartDate, " & _
                                                    "MIN(REPLACE(SUBSTRING(CONVERT(Nvarchar, dt_mtdate, 121),1,10), '-', '/')) AS MTdate, '" & ThisPUSet & "' AS PU_SetNo, '" & ThisDate & "' AS AssemblyDate, " & _
                                                    "COUNT(dt_pdate) AS PUDays, '' AS dUserName " & _
                                                    "FROM dty_detail WHERE  " & _
                                                    "(dt_pdate >= '" & Strings.Replace(ThisDate, "/", "") & "')  AND " & _
                                                    "(Dt_totalBobins > 0)  AND " & _
                                                    "(dt_lotno IN " & GetLotString & ")  " & _
                                                    "GROUP BY     dt_mac, SUBSTRING(dt_lotno, 1, 5) " & _
                                                    "ORDER BY StartDate DESC"
        Dim PUHistoryData As DataTable = POPULATE(ProductionConnectionString, GetHistoryString)
        Return PUHistoryData
    End Function
    Private Function GetLots(ByVal ThisPUSet As String, ByVal ThisAssemblyDate As String, ByVal TMTMC As String) As String
        '====NOT USED, Replace by GetLotsQueryString
        Dim SelectLotString As String = "SELECT DISTINCT DTY_LotNo  FROM  Dty_Cond_Hist " & _
                                                        "WHERE (PU_SetNo = '" & ThisPUSet & "') AND " & _
                                                        "(Create_pDate >= '" & ThisAssemblyDate & "')"
        If TMTMC = "115" Or TMTMC = "116" Then
            SelectLotString = "SELECT DISTINCT DTY_LotNo  FROM  Dty_TMTCond_Hist_New " & _
                                                        "WHERE (LTRIM(PU_SetNo) = '" & ThisPUSet & "') AND " & _
                                                        "(Create_pDate >= '" & ThisAssemblyDate & "')"
        End If
        Dim SelectLotData As DataTable = POPULATE(DTYCardConnectionString, SelectLotString)
        Dim LotsString As String = "("
        For Each LotsRunData As DataRow In SelectLotData.Rows
            Dim ThisLot As String = LotsRunData("dty_lotNO").ToString
            If TMTMC = "115" Or TMTMC = "116" Then LotsString = LotsString + Chr(39) + Strings.Mid(ThisLot, 1, 5) + Chr(39) Else _
                LotsString = LotsString + Chr(39) + "1" + Strings.Mid(ThisLot, 1, 4) + Chr(39)
            If SelectLotData.Rows.IndexOf(LotsRunData) <= SelectLotData.Rows.Count - 1 Then LotsString = LotsString + ", "

        Next
        Dim LastCommaIndex As Integer = InStrRev(LotsString, "',", -1)
        If Trim(Strings.Right(LotsString, 2)) = "," Or LastCommaIndex > 0 Then
            LotsString = Strings.Mid(LotsString, 1, LastCommaIndex)
        End If
        LotsString = LotsString + ")"
        Return LotsString
    End Function

End Class

#Region "GridControl_And_LayoutView_Property"
Public Class MyDEVXGRIDVIEW
    Inherits DevExpress.XtraGrid.Views.Grid.GridView
    Public MyParentGrid As DevExpress.XtraGrid.GridControl
    Private ReadOnly PrintContentFontName As String = "Segoe UI Semibold"         '"Consolas"
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then

        Else

        End If
        MyBase.Dispose(disposing)
    End Sub
    Public Sub New(Optional ByVal ThisGridControl As DevExpress.XtraGrid.GridControl = Nothing)
        MyParentGrid = ThisGridControl
        If MyParentGrid IsNot Nothing Then MyParentGrid.ViewCollection.Add(Me)
        Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Appearance.FooterPanel.ForeColor = Color.Black
        Appearance.FooterPanel.Font = New Font(PrintContentFontName, 11)        'SmallerFooterFont
        Appearance.HeaderPanel.Font = New Font(PrintContentFontName, 13)
        Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Appearance.SelectedRow.BackColor = Color.FromArgb(232, 255, 180)
        Appearance.SelectedRow.ForeColor = Color.Black
        Appearance.Row.Font = New Font(PrintContentFontName, 12, FontStyle.Regular)
        Appearance.ViewCaption.Font = New Font(PrintContentFontName, 12)
        Appearance.ViewCaption.BackColor = Color.Blue
        BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder          'setting is non-effective
        ColumnPanelRowHeight = 55
        FocusRectStyle = Views.Grid.DrawFocusRectStyle.None                    'This makes cell selection solid line
        FooterPanelHeight = 10
        OptionsBehavior.Editable = False
        OptionsCustomization.AllowColumnMoving = False
        OptionsCustomization.AllowColumnResizing = True
        OptionsCustomization.AllowFilter = False
        OptionsCustomization.AllowGroup = False
        OptionsCustomization.AllowRowSizing = False
        OptionsCustomization.AllowSort = True
        'MASTER/DEtail VIEW SETTING -------------------
        DetailHeight = 600
        DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top
        LevelIndent = 80
        OptionsDetail.AllowZoomDetail = True
        OptionsDetail.AllowExpandEmptyDetails = True
        OptionsDetail.AllowOnlyOneMasterRowExpanded = True
        OptionsDetail.AutoZoomDetail = False
        OptionsDetail.EnableDetailToolTip = True
        OptionsDetail.EnableMasterViewMode = True
        OptionsDetail.ShowDetailTabs = True
        OptionsDetail.SmartDetailExpand = True
        OptionsDetail.SmartDetailExpandButtonMode = Views.Grid.DetailExpandButtonMode.AlwaysEnabled
        OptionsView.ShowDetailButtons = True
        SynchronizeClones = True
        'MASTER/Detail VIEW SETTING -------------------
        OptionsMenu.EnableFooterMenu = False
        OptionsMenu.EnableColumnMenu = False
        OptionsSelection.EnableAppearanceFocusedCell = False
        OptionsSelection.MultiSelect = True
        OptionsSelection.MultiSelectMode = Views.Grid.GridMultiSelectMode.RowSelect
        OptionsSelection.UseIndicatorForSelection = False
        OptionsView.ColumnAutoWidth = False
        OptionsView.EnableAppearanceEvenRow = True
        OptionsView.EnableAppearanceOddRow = True
        OptionsView.ShowColumnHeaders = True
        OptionsView.ShowFooter = True
        OptionsView.ShowGroupPanel = False
        OptionsView.ShowIndicator = False
        OptionsView.ShowAutoFilterRow = True
        OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button
        PaintStyleName = "Skin"
        RowHeight = 28
        ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll
        ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll
    End Sub
    Public Function GETFILTEREDDATA(ByVal view As Views.Base.ColumnView, Optional ByVal SortDescription As String = Nothing) As DataTable
        ' Used in Combination with Filter Row, to populate a datatable and customize Crystal Report
        If view Is Nothing Then
            Return Nothing
        End If
        If view.ActiveFilter Is Nothing OrElse (Not view.ActiveFilterEnabled) OrElse view.ActiveFilter.Expression = "" Then
            Return TryCast(view.DataSource, DataTable)
        End If
        Dim table As DataTable = (CType(view.DataSource, DataView)).Table
        Dim filteredDataView As New DataView(table)
        filteredDataView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(view.ActiveFilterCriteria)
        filteredDataView.Sort = SortDescription
        Dim FilterDatatable As DataTable = filteredDataView.ToTable        '(CType(filteredDataView, DataView)).Table
        Return FilterDatatable
    End Function
    Public Sub SET_UP_HEADER(ByVal HeaderText As String(), ByVal HeaderWidth As Integer(), Optional ByVal ThisGridView As MyDEVXGRIDVIEW = Nothing)
        '// Late Binding Event, Takes Effect after Datasource is assigned
        With Me
            If Me IsNot Nothing Then
                .OptionsView.ShowFilterPanelMode = Views.Base.ShowFilterPanelMode.Never
                Dim ColIdx As Integer = 0
                For Each myGColum As DevExpress.XtraGrid.Columns.GridColumn In Me.Columns
                    With myGColum
                        .OptionsColumn.AllowEdit = False
                        '  .AppearanceHeader.Font = New Font("Calibri", 14, FontStyle.Bold)
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
                        .AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        .AllowSummaryMenu = True
                        .Caption = HeaderText(ColIdx)
                        '============================================================================================
                        'DO NOT RENAME Columns, because Column Mapping and Master-Detail construction are based on original Column Name
                        '  .Name = HeaderText(ColIdx)               
                        '============================================================================================
                        .Width = HeaderWidth(ColIdx)
                        .VisibleIndex = ColIdx
                        '============================================================================================
                        'this is to set Frozen Columns
                        If .Name = "No" Or Name = "Location" Then
                            .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                            Me.FixedLineWidth = 0
                        End If
                        '============================================================================================
                        ColIdx += 1
                    End With
                Next
            End If
        End With
    End Sub
    Public Sub FILL_No_COLUMN(ByVal DataRowsToBeFilled As DataTable, Optional ByVal ShowRowNoBool As Boolean = True)
        Dim Count As Integer
        Dim RowsCount As Integer = DataRowsToBeFilled.Rows.Count
        Dim DisplayFormat As Integer
        Dim DisplayTimeAsString As Boolean = False
        Dim DisplayTimeColumnIndex As Integer
        Using DataRowsToBeFilled
            For Each DTColumn As DataColumn In DataRowsToBeFilled.Columns
                If DTColumn.ColumnName = "Actual Change Date" Or DTColumn.ColumnName = "Installed Date" Or DTColumn.ColumnName = "Sceduled Date" Then
                    DisplayTimeAsString = True
                    DisplayTimeColumnIndex = DTColumn.Ordinal
                End If
            Next
            For Each DTrow As DataRow In DataRowsToBeFilled.Rows
                Count += 1
                If ShowRowNoBool = True Then
                    DisplayFormat = Strings.FormatNumber(Count, 0, TriState.True)
                    DTrow("No") = DisplayFormat                 'Format(Count, "00000").ToString
                End If
                If DisplayTimeAsString Then
                    If Not IsDBNull(DTrow(DisplayTimeColumnIndex)) Then
                        DTrow(DisplayTimeColumnIndex) = (DTrow(DisplayTimeColumnIndex))
                    End If
                End If
            Next
        End Using
    End Sub
    Public Sub UPDATE_SUMMARY(ByVal Columns As DevExpress.XtraGrid.Columns.GridColumn(), Optional ByVal SummaryType As String = Nothing)
        For Each MyColumns As DevExpress.XtraGrid.Columns.GridColumn In Columns
            Dim ColumnSummaryNOTShown As Boolean = String.IsNullOrEmpty(MyColumns.SummaryText)
            Dim ColumnTypeString As String = MyColumns.ColumnType.Name.ToString
            Dim ColumnDisplayFormat As String = "{0:n1}"
            Select Case ColumnTypeString
                Case "Double"
                    ColumnDisplayFormat = "{0:n2}"
                Case "Int32"
                    ColumnDisplayFormat = "{0:n0}"
            End Select
            If ColumnSummaryNOTShown Then
                Select Case SummaryType
                    Case "COUNT"
                        MyColumns.Summary.Add(DevExpress.Data.SummaryItemType.Count, MyColumns.FieldName.ToString, ColumnDisplayFormat)
                    Case Else
                        MyColumns.Summary.Add(DevExpress.Data.SummaryItemType.Sum, MyColumns.FieldName.ToString, ColumnDisplayFormat)
                End Select
            End If
        Next
    End Sub
End Class

#End Region