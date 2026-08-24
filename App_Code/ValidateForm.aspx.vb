Public Class ValidateForm
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim subCheck As String = ""
    Dim valCheck As String = ""
    Dim propCheck As String = ""
    Dim tableCheck As String = ""
    Dim rangeCheck As String = ""
    Dim typeNumerator As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        subCheck = Request.QueryString("subCheck") & ""
        valCheck = Request.QueryString("valCheck") & ""
        propCheck = Request.QueryString("propCheck") & ""
        tableCheck = Request.QueryString("table") & ""
        rangeCheck = Request.QueryString("range") & ""
        typeNumerator = Request.QueryString("typeNumerator") & ""

        Select Case subCheck
            Case "ValidateDocNum"
                ValidateDocNum(valCheck)
            Case "GenerateFlightID"
                GenerateFlightID(valCheck, propCheck)
            Case "GenerateQuestionID"
                GenerateQuestionID(valCheck, propCheck)
            Case "GenerateDocumentID"
                GenerateDocumentID(valCheck, propCheck)
            Case "UniqueCheck"
                UniqueCheck(tableCheck, rangeCheck, propCheck, valCheck)
            Case "UniqueCheckInsensitive"
                UniqueCheckInsensitive(tableCheck, rangeCheck, propCheck, valCheck)
            Case "GetUniqueIndex"
                GetUniqueIndex(tableCheck, rangeCheck, propCheck)
            Case "GenerateUnique"
                GenerateUnique(tableCheck, rangeCheck, propCheck, valCheck, typeNumerator)
            Case "GetLOV"
                GenerateLOV(propCheck)
            Case "GetLOVByName"
                GenerateLOVByName(propCheck)
            Case "GenerateMultiLOV"
                GenerateMultiLOV(valCheck, propCheck)
            Case "CheckUniqueName"
                CheckUniqueName(valCheck, propCheck)
            Case "CheckWinUniqueName"
                CheckWinUniqueName(valCheck, propCheck)
            Case "CheckWinUniqueDocNumber"
                CheckWinUniqueDocNumber(valCheck)
            Case Else
                Response.Write("Error")
        End Select

    End Sub
    Private Function GenerateDocumentID(ByVal fval As String, ByVal fprop As String)

        Dim fnewval As Integer
        Dim fsval As String = ""
        'Dim sSqlQuery As String = "SELECT distinct " & fprop & " FROM T_CAT_WINOBJECT order by " & fprop & ""
        Dim sSqlQuery As String = "SELECT PKEY_SEQNUMBER.NEXTVAL FROM DUAL"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))

        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then

            fsval = dbDataset.Tables(0).Rows(0)(0) & ""

            'If fsval = "" Then
            '    fnewval = 0
            'Else
            '    fnewval = CInt(fsval) + 1
            'End If

        End If

        Response.Write(fsval)

    End Function

    Private Function CheckUniqueName(ByVal fval As String, ByVal fprop As String)

        Dim fvallow As String = fval.ToLower()
        Dim sSqlQuery As String = "SELECT t_cat_part.phebdesc FROM T_CAT_PART,t_cat_nodes WHERE t_cat_part.pkey=t_cat_nodes.pkey and t_cat_nodes.pparentkey='" & fprop & "' and T_CAT_NODES.PGLOBALSTATUS<>4 and LOWER(t_cat_part.phebdesc)='" & fvallow & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim fret As String = "true"

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))

        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            fret = "false"
        End If

        Response.Clear()
        Response.Write(fret)

    End Function

    Private Function CheckWinUniqueName(ByVal fval As String, ByVal fprop As String)

        Dim fvallow As String = fval.ToLower()
        Dim sSqlQuery As String = "SELECT t_cat_winobject.phebdesc FROM t_cat_winobject,t_cat_nodes WHERE t_cat_winobject.pkey=t_cat_nodes.pkey and t_cat_nodes.pparentkey='" & fprop & "' and T_CAT_NODES.PGLOBALSTATUS<>4 and LOWER(t_cat_winobject.phebdesc)='" & fvallow & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim fret As String = "true"

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))

        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            fret = "false"
        End If

        Response.Clear()
        Response.Write(fret)

    End Function

    Private Function CheckWinUniqueDocNumber(ByVal fval As String)

        Dim fvallow As String = fval.ToLower()
        Dim sSqlQuery As String = "SELECT t_cat_winobject.phebdesc FROM t_cat_winobject,t_cat_nodes WHERE t_cat_winobject.pkey=t_cat_nodes.pkey and T_CAT_NODES.PGLOBALSTATUS<>4 and LOWER(t_cat_winobject.pdocnumber)='" & fvallow & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim fret As String = "true"

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))

        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            fret = "false"
        End If

        Response.Clear()
        Response.Write(fret)

    End Function

    Private Function GenerateQuestionID(ByVal fval As String, ByVal fprop As String)

        Dim fnewval As Integer
        Dim fsval As String = ""
        Dim sSqlQuery As String = "SELECT distinct " & fprop & " FROM T_CAT_PART WHERE pkeytype=89 or pkeytype=92 order by " & fprop & " DESC"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))

        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            fsval = dbDataset.Tables(0).Rows(0)(0)
            fnewval = CInt(fsval) + 1
        End If

        Response.Write(fnewval)

    End Function

    Private Function GenerateLOV(ByVal sPkeyParent As String)

        Dim fsval As String = "<?xml version=""1.0"" encoding=""Windows-1255""?>" & vbCrLf
        Dim sSqlQuery As String = "SELECT PHEBDESC FROM T_CAT_PART,T_CAT_NODES WHERE T_CAT_NODES.PPARENTKEY='" & sPkeyParent & "' AND T_CAT_NODES.PKEY=T_CAT_PART.PKEY AND T_CAT_NODES.POBJECTTYPE=10 ORDER BY PHEBDESC"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim i As Integer

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQuery, dbDataset)

        fsval = fsval & "<LOV>" & vbCrLf

        For i = 0 To dbDataset.Tables(0).Rows.Count - 1
            fsval = fsval & "  <PROPERTY><![CDATA[" & dbDataset.Tables(0).Rows(i)(0) & "]]></PROPERTY>" & vbCrLf
        Next

        oConn.Close_Connection()
        oConn = Nothing
        dbDataset.Dispose()
        dbDataset = Nothing

        fsval = fsval & "</LOV>" & vbCrLf
        Response.ContentType = "text/xml"
        Response.Write(fsval)

    End Function

    Private Function GenerateLOVByName(ByVal sLOVName As String)

        Dim fsval As String = "<?xml version=""1.0"" encoding=""Windows-1255""?>" & vbCrLf
        Dim sSqlQuery As String = "SELECT T_CAT_PART_LOV.PHEBDESC FROM T_CAT_PART,T_CAT_NODES,T_CAT_PART T_CAT_PART_LOV,T_CAT_OBJECTTYPES WHERE T_CAT_PART.PHEBDESC='" & sLOVName & "' AND T_CAT_NODES.PPARENTKEY=T_CAT_PART.PKEY AND T_CAT_NODES.PKEY=T_CAT_PART_LOV.PKEY AND T_CAT_NODES.POBJECTTYPE=10 AND T_CAT_NODES.PGLOBALSTATUS<>3 AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY AND T_CAT_OBJECTTYPES.PTYPENAME='Property' ORDER BY PHEBDESC"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim i As Integer

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQuery, dbDataset)

        fsval = fsval & "<LOV>" & vbCrLf

        For i = 0 To dbDataset.Tables(0).Rows.Count - 1
            fsval = fsval & "  <PROPERTY><![CDATA[" & dbDataset.Tables(0).Rows(i)(0) & "]]></PROPERTY>" & vbCrLf
        Next

        oConn.Close_Connection()
        oConn = Nothing
        dbDataset.Dispose()
        dbDataset = Nothing

        fsval = fsval & "</LOV>" & vbCrLf
        Response.ContentType = "text/xml"
        Response.Write(fsval)

    End Function

    Private Function GenerateMultiLOV(ByVal sSelProps As String, ByVal sWhereClause As String)

        Dim fsval As String = "<?xml version=""1.0"" encoding=""Windows-1255""?>" & vbCrLf
        'default
        Dim sTable As String = "T_CAT_PART"
        Dim sExtraQuery As String = ""

        If sSelProps.IndexOf(".") > 0 Then
            sTable = sSelProps.Substring(0, sSelProps.IndexOf("."))
            If sTable.IndexOf("(") > 0 Then
                'concat functions etc.
                sTable = sTable.Substring(sTable.LastIndexOf("(") + 1)
            End If
        End If

        If sTable.ToLower() = "t_cat_part" Or sTable.ToLower() = "t_cat_winobject" Or sTable.ToLower() = "t_cat_page" Or sTable.ToLower() = "t_cat_folder" Then
            'with nodes
            sExtraQuery = ",t_cat_nodes WHERE t_cat_nodes.pkey=" & sTable & ".pkey and t_cat_nodes.pglobalstatus<>4 "
            If sWhereClause <> "" Then
                sExtraQuery = sExtraQuery & " and "
            End If
        Else
            'without nodes
            If sWhereClause <> "" Then
                sExtraQuery = " WHERE "
            End If
        End If

        Dim sSqlQuery As String = "SELECT DISTINCT " & sSelProps & " FROM " & sTable & sExtraQuery & sWhereClause
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim i As Integer
        Dim j As Integer

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQuery, dbDataset)

        fsval = fsval & "<LOV>" & vbCrLf
        Dim sColVals As String

        For i = 0 To dbDataset.Tables(0).Rows.Count - 1
            sColVals = ""
            For j = 0 To dbDataset.Tables(0).Columns.Count - 1
                sColVals = sColVals & dbDataset.Tables(0).Rows(i)(j) & "|"
            Next
            If sColVals.Length > 1 Then
                sColVals = sColVals.Remove(sColVals.Length - 1, 1)
            End If
            fsval = fsval & "  <PROPERTY><![CDATA[" & sColVals & "]]></PROPERTY>" & vbCrLf
        Next

        oConn.Close_Connection()
        oConn = Nothing
        dbDataset.Dispose()
        dbDataset = Nothing

        fsval = fsval & "</LOV>" & vbCrLf
        Response.ContentType = "text/xml"
        Response.Write(fsval)

    End Function

    Public Function GenerateFlightID1(ByVal fval As String, ByVal fprop As String, Optional ByVal sMode As String = "") As String

        Dim fnewval As String = ""
        Dim fdate As DateTime = DateTime.Parse(fval)
        Dim fchar As String = "A"
        Dim fdmonth As String = ""
        Dim fdday As String = ""

        If fdate.Month < 10 Then
            fdmonth = "0" & fdate.Month.ToString()
        Else
            fdmonth = fdate.Month
        End If
        If fdate.Day < 10 Then
            fdday = "0" & fdate.Day.ToString()
        Else
            fdday = fdate.Day
        End If


        fnewval = fdate.Year & "-" & fdmonth & "-" & fdday & "-" & fchar

        Dim sSqlQuery As String = "SELECT * FROM T_CAT_PART WHERE " & fprop & "='" & fnewval & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim bWhile As Boolean = True

        If sMode = "" Then
            oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        Else
            oConn.Open_Oracle_Connection(sMode)
        End If


        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            While bWhile
                fdmonth = ""
                fdday = ""
                fchar = Chr(Asc(fchar) + 1)
                If fdate.Month < 10 Then
                    fdmonth = "0" & fdate.Month.ToString()
                Else
                    fdmonth = fdate.Month
                End If
                If fdate.Day < 10 Then
                    fdday = "0" & fdate.Day.ToString()
                Else
                    fdday = fdate.Day
                End If

                fnewval = fdate.Year & "-" & fdmonth & "-" & fdday & "-" & fchar

                sSqlQuery = "SELECT * FROM T_CAT_PART WHERE " & fprop & "='" & fnewval & "'"
                dbDataset.Dispose()
                dbDataset = New Data.DataSet
                oConn.Open_Dataset(sSqlQuery, dbDataset)
                If dbDataset.Tables(0).Rows.Count > 0 Then
                    bWhile = True
                Else
                    bWhile = False
                End If
            End While
        End If

        If sMode = "" Then
            Response.Write(fnewval)
        Else
            GenerateFlightID1 = fnewval
        End If

    End Function

    Public Function GenerateFlightID(ByVal fval As String, ByVal fprop As String, Optional ByVal sMode As String = "") As String

        Dim fnewval As String = ""
        Dim fdate As DateTime = DateTime.Parse(fval)
        Dim fchar As String = "A"
        Dim fdmonth As String = ""
        Dim fdday As String = ""

        If fdate.Month < 10 Then
            fdmonth = "0" & fdate.Month.ToString()
        Else
            fdmonth = fdate.Month
        End If
        If fdate.Day < 10 Then
            fdday = "0" & fdate.Day.ToString()
        Else
            fdday = fdate.Day
        End If


        fnewval = fdate.Year & "-" & fdmonth & "-" & fdday & "-" & fchar

        Dim sSqlQuery As String = "SELECT * FROM T_CAT_PART WHERE " & fprop & "='" & fnewval & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim bWhile As Boolean = True

        If sMode = "" Then
            oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        Else
            oConn.Open_Oracle_Connection(sMode)
        End If


        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            While bWhile
                fdmonth = ""
                fdday = ""
                fchar = Chr(Asc(fchar) + 1)
                If fdate.Month < 10 Then
                    fdmonth = "0" & fdate.Month.ToString()
                Else
                    fdmonth = fdate.Month
                End If
                If fdate.Day < 10 Then
                    fdday = "0" & fdate.Day.ToString()
                Else
                    fdday = fdate.Day
                End If

                fnewval = fdate.Year & "-" & fdmonth & "-" & fdday & "-" & fchar

                sSqlQuery = "SELECT * FROM T_CAT_PART WHERE " & fprop & "='" & fnewval & "'"
                dbDataset.Dispose()
                dbDataset = New Data.DataSet
                oConn.Open_Dataset(sSqlQuery, dbDataset)
                If dbDataset.Tables(0).Rows.Count > 0 Then
                    bWhile = True
                Else
                    bWhile = False
                End If
            End While
        End If

        If sMode = "" Then
            Response.Write(fnewval)
        Else
            GenerateFlightID = fnewval
        End If

    End Function

    Private Function ValidateDocNum(ByVal docid As String)

        Dim sSqlQuery As String = "SELECT * FROM T_CAT_WINOBJECT WHERE PDOCNUMBER='" & docid & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            Response.Write("false")
        Else
            Response.Write("true")
        End If

    End Function

    Private Function UniqueCheck(ByVal tableCheck As String, ByVal rangeCheck As String, ByVal propCheck As String, ByVal valCheck As String)

        Dim sSqlQuery As String = "SELECT * FROM " & tableCheck & ",T_CAT_NODES WHERE " & tableCheck & ".PKEY=T_CAT_NODES.PKEY AND T_CAT_NODES.PGLOBALSTATUS<>4 AND " & propCheck & "='" & valCheck & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet

        If rangeCheck <> "" Then
            sSqlQuery = sSqlQuery & " AND " & rangeCheck
        End If

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            Response.Write("false")
        Else
            Response.Write("true")
        End If

        dbDataset.Dispose()
        oConn.Close_Connection()
        oConn = Nothing
        dbDataset = Nothing


    End Function

    Private Function UniqueCheckInsensitive(ByVal tableCheck As String, ByVal rangeCheck As String, ByVal propCheck As String, ByVal valCheck As String)

        Dim sSqlQuery As String = "SELECT * FROM " & tableCheck & ",T_CAT_NODES WHERE " & tableCheck & ".PKEY=T_CAT_NODES.PKEY AND T_CAT_NODES.PGLOBALSTATUS<>4 AND LOWER(" & propCheck & ")=LOWER('" & valCheck & "')"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet

        If rangeCheck <> "" Then
            sSqlQuery = sSqlQuery & " AND " & rangeCheck
        End If

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            Response.Write("false")
        Else
            Response.Write("true")
        End If

        dbDataset.Dispose()
        oConn.Close_Connection()
        oConn = Nothing
        dbDataset = Nothing


    End Function

    Public Function P_GetUniqueIndex(ByVal tableCheck As String, ByVal rangeCheck As String, ByVal propCheck As String) As Long

        Dim sSqlQuery As String = "SELECT MAX(ToNumber(" & propCheck & ")*1) FROM " & tableCheck
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet

        If rangeCheck <> "" Then
            sSqlQuery = sSqlQuery & " WHERE " & rangeCheck
        End If

        oConn.Open_Oracle_Connection(System.Web.HttpContext.Current.Application("ODBCNAME"))
        Try
            'if the range is only digits.
            oConn.Open_Dataset(sSqlQuery, dbDataset)
        Catch ex As Exception
            dbDataset.Dispose()
            oConn.Close_Connection()
            oConn = Nothing
            dbDataset = Nothing
            P_GetUniqueIndex = 0
            Exit Function
        End Try

        If dbDataset.Tables(0).Rows.Count > 0 Then
            If IsNumeric(dbDataset.Tables(0).Rows(0)(0)) Then
                P_GetUniqueIndex = CLng(dbDataset.Tables(0).Rows(0)(0)) + 1
            Else
                P_GetUniqueIndex = 0
            End If
        Else
            P_GetUniqueIndex = 1
        End If

        dbDataset.Dispose()
        oConn.Close_Connection()
        oConn = Nothing
        dbDataset = Nothing

    End Function

    Private Function GetUniqueIndex(ByVal tableCheck As String, ByVal rangeCheck As String, ByVal propCheck As String)
        'ToNumber - oracle function:
        ''''      create or replace function ToNumber(p_num in varchar2 ) return number
        ''''as
        ''''        x number;
        ''''      begin()
        ''''        x := to_number( p_num );
        ''''        return x;
        ''''      Exception()
        ''''        when others then return 0;
        ''''end;
        ''''/

        Response.Write(P_GetUniqueIndex(tableCheck, rangeCheck, propCheck))

    End Function

    Function GenerateUnique(ByVal tableCheck As String, ByVal rangeCheck As String, ByVal propCheck As String, ByVal valCheck As String, ByVal typeNumerator As String)

        Dim fnewval As String = ""
        Dim fchar As String = "A"

        If typeNumerator.ToLower() = "numeric" Then
            fchar = "1"
        End If

        fnewval = valCheck & "-" & fchar

        Dim sSqlQuery As String = "SELECT * FROM " & tableCheck & ",T_CAT_NODES WHERE " & tableCheck & ".PKEY=T_CAT_NODES.PKEY AND T_CAT_NODES.PGLOBALSTATUS<>4 AND " & propCheck & "='" & fnewval & "'"
        Dim oConn As New CConnection
        Dim dbDataset As New Data.DataSet
        Dim bWhile As Boolean = True

        If rangeCheck <> "" Then
            sSqlQuery = sSqlQuery & " AND " & rangeCheck
        End If

        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQuery, dbDataset)

        If dbDataset.Tables(0).Rows.Count > 0 Then
            While bWhile
                fchar = Chr(Asc(fchar) + 1)
                fnewval = valCheck & "-" & fchar

                sSqlQuery = "SELECT * FROM " & tableCheck & ",T_CAT_NODES WHERE " & tableCheck & ".PKEY=T_CAT_NODES.PKEY AND T_CAT_NODES.PGLOBALSTATUS<>4 AND " & propCheck & "='" & fnewval & "'"
                If rangeCheck <> "" Then
                    sSqlQuery = sSqlQuery & " AND " & rangeCheck
                End If

                dbDataset.Dispose()
                dbDataset = New Data.DataSet
                oConn.Open_Dataset(sSqlQuery, dbDataset)
                If dbDataset.Tables(0).Rows.Count > 0 Then
                    bWhile = True
                Else
                    bWhile = False
                End If
            End While
        End If

        oConn.Close_Connection()
        oConn = Nothing
        dbDataset.Dispose()
        dbDataset = Nothing

        Response.Write(fnewval)

    End Function
End Class
