Public Class CHandlers

    Dim sODBCName As String = ""
    Dim oConn As CConnection
    Dim sPkey As String
    Dim sParentKey As String
    Dim sPkeyCatalog As String
    Dim sObjectType As String
    Dim sPkeyType As String
    Dim sProp As String
    Dim sPropValue As String
    Dim sPoperator As String
    Dim sPasteOption As String
    Dim oPrefs As PersonalPref
    Dim alEffectedPkeys As ArrayList = Nothing


    Public Sub New(ByVal sODBCName As String)

        Me.sODBCName = sODBCName

    End Sub

    Public Function ObjectEvents(ByVal sEvent As String, ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, Optional ByVal sPasteOption As String = "", Optional ByRef alEffectedPkeys As ArrayList = Nothing) As Boolean

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sHandler As String = ""
        Dim oLWObjectTypes As New LWObjectTypes
        oPrefs = New PersonalPref(sODBCName)

        Me.sPkey = sPkey
        Me.sParentKey = sParentKey
        Me.sPkeyCatalog = sPkeyCatalog
        Me.sPasteOption = sPasteOption

        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCName)

        sSqlQuery = "SELECT * FROM T_CAT_NODES WHERE PKEY='" & sPkey & "'"
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        If dbDataSet.Tables(0).Rows.Count > 0 Then
            Me.sObjectType = dbDataSet.Tables(0).Rows(0)("POBJECTTYPE") & ""
        End If

        Select Case sObjectType
            Case oLWObjectTypes.emPart
                sSqlQuery = "SELECT PKEY,PKEYTYPE FROM T_CAT_PART WHERE PKEY='" & sPkey & "'"
            Case oLWObjectTypes.emWinObject
                sSqlQuery = "SELECT PKEY,PKEYTYPE FROM T_CAT_WINOBJECT WHERE PKEY='" & sPkey & "'"
            Case Else
                oConn.Close_Connection()
                Exit Function
        End Select

        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        If dbDataSet.Tables(0).Rows.Count > 0 Then
            Me.sPkeyType = dbDataSet.Tables(0).Rows(0)("PKEYTYPE") & ""
        End If

        Me.alEffectedPkeys = alEffectedPkeys

        dbDataSet.Clear()
        'dbDataSet = oPrefs.GetPrefsByObject("GROUP", "Everyone", sPkeyCatalog, sObjectType, sPkeyType, "", "", "", sEvent, "HANDLER")
        dbDataSet = oPrefs.GetPrefs("GROUP", "Everyone", sPkeyCatalog, sObjectType, sPkeyType, "", "", "", sEvent, "HANDLER")
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To dbDataSet.Tables(0).Rows.Count - 1
                sHandler = dbDataSet.Tables(0).Rows(i)("PREFVALUE") & ""
                Me.sProp = dbDataSet.Tables(0).Rows(i)("PROP") & ""
                Me.sPropValue = dbDataSet.Tables(0).Rows(i)("PROPVALUE") & ""
                Me.sPoperator = dbDataSet.Tables(0).Rows(i)("POPERATOR") & ""
                If sPasteOption = "" Then
                    CallHandler(sHandler)
                Else
                    If sPoperator = sPasteOption Then
                        CallHandler(sHandler)
                    End If
                End If
            Next
        End If

        oConn.Close_Connection()
        oConn = Nothing

    End Function

    Private Function CallHandler(ByVal sFunction As String)

        Select Case sFunction
            Case "StartTextEditor"
                StartTextEditor(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType)
            Case "taskCount33"
                taskCount33(sPkey, sPkeyCatalog, sObjectType, sPkeyType)
            Case "taskCount34"
                taskCount34(sPkey, sPkeyCatalog, sObjectType, sPkeyType)
            Case "InheritParentPropToCurrent"
                InheritParentPropToCurrent(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "InheritParentPropToChilds"
                InheritParentPropToChilds(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "UpdateParentPropToChilds"
                UpdateParentPropToChilds(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "MoveChildsToNewItem"
                MoveChildsToNewItem(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "FillImportKeyProp"
                FillImportKeyProp(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "UpdatePropsOnUpdateReport"
                UpdatePropsOnUpdateReport(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "UpdateInfoBagLink"
                UpdateInfoBagLink(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "UpdateNetworkPath"
                UpdateNetworkPath(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "CreateGroupDocument"
                CreateGroupDocument(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "UpdateGroupLeader"
                UpdateGroupLeader(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue)
            Case "MultiDuplicatePaste"
                MultiDuplicatePaste(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue, sProp)
            Case "SetUniqueIndex"
                SetUniqueIndex(sPkey, sParentKey, sPkeyCatalog, sObjectType, sPkeyType, sPropValue, sProp)
            Case Else
                Exit Function
        End Select

    End Function

    Private Function CreateGroupDocument(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sParentPkeyType As String
        Dim sCheckProp As String = sProp
        Dim sCheckPropValue As String
        Dim sNewItemPkey As String
        Dim sPartTemplate As String = sPoperator
        Dim sGroupPkeyType As String = "5"
        Dim sOldPath As String
        Dim sPLinkToNetworkFile As String

        sSqlQuery = "SELECT * FROM T_CAT_WINOBJECT WHERE PKEY='" & sPkey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sCheckPropValue = dbDataSet.Tables(0).Rows(0)(sCheckProp) & ""
            sOldPath = dbDataSet.Tables(0).Rows(0)("PORGFILENAME") & ""
            sPLinkToNetworkFile = dbDataSet.Tables(0).Rows(0)("PLINKTONETWORKFILE") & ""
        End If

        If sCheckPropValue = sPropValue Then

            sSqlQuery = "SELECT PKEY_SEQNUMBER.NEXTVAL FROM DUAL"
            dbDataSet.Reset()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
            If dbDataSet.Tables(0).Rows.Count > 0 Then
                sNewItemPkey = System.Web.HttpContext.Current.Application("PkeyPrefix") & "" & dbDataSet.Tables(0).Rows(0).Item(0)
            End If

            sSqlQuery = "INSERT INTO t_cat_part (PKEY, PENGDESC, PHEBDESC, PKEYCATALOG, PKEYUSER, PSTATUS, PKEYTYPE, PCREATEDATE, PUPDATEDATE, PPROP1, PREVISION, PPROP2, PPROP3, PPROP4, PPROP5, PPROP6, PPROP7, PPROP8, PPROP9, PPROP10, PCHECKOUTUSER, PPARTTEMPLATE) " _
                     & "SELECT '" & sNewItemPkey & "', PENGDESC, PHEBDESC, PKEYCATALOG, PKEYUSER, PSTATUS, '" & sGroupPkeyType & "', PCREATEDATE, PUPDATEDATE, PPROP1, PREVISION, PPROP2, PPROP3, PPROP4, PPROP5, PPROP6, PPROP7, PPROP8, PPROP9, PPROP10, PCHECKOUTUSER,'" & sPartTemplate & "'  FROM  t_cat_winobject WHERE t_cat_winobject.PKEY='" & sPkey & "'"

            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)

            sSqlQuery = "INSERT INTO t_cat_nodes (PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20)" _
             & "(SELECT '" & sNewItemPkey & "','" & sParentKey & "','10', PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20 FROM t_cat_nodes WHERE PKEY='" & sPkey & "' and rownum<2)"

            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)

            sSqlQuery = "update t_cat_nodes set PPARENTKEY='" & sNewItemPkey & "',PNPROP7='leader' where t_Cat_nodes.pkey='" & sPkey & "' and t_Cat_nodes.PPARENTKEY='" & sParentKey & "'"

            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)

            Dim sNewPath As String
            sNewPath = UpdateNetworkPath(sNewItemPkey, sParentKey, sPkeyCatalog, "10", sGroupPkeyType, "")

            If sNewPath <> "" Then
                Dim sMapped As String = System.Web.HttpContext.Current.Application("MappedDrive") & ""
                Dim sMappedUNC As String = System.Web.HttpContext.Current.Application("MappedDriveUNC") & ""
                Dim sOrgPath As String = sNewPath

                If sMapped <> "" Then
                    sMappedUNC = sMappedUNC & "\"
                    sNewPath = sNewPath.ToLower.Replace(sMapped.ToLower, sMappedUNC)
                    sOldPath = sOldPath.ToLower.Replace(sMapped.ToLower, sMappedUNC)
                End If
                Dim sFileName As String = Mid(sOldPath, InStrRev(sOldPath, "\") + 1)
                System.IO.Directory.CreateDirectory(sNewPath)

                If Not (System.IO.File.Exists(sNewPath & "\" & sFileName)) Then
                    System.IO.File.Move(sOldPath, sNewPath & "\" & sFileName)
                End If

                sSqlQuery = "update t_cat_winobject set porgfilename='" & sOrgPath & "\" & sFileName & "' where t_Cat_winobject.pkey='" & sPkey & "'"

                dbDataSet.Clear()
                oConn.Open_Dataset(sSqlQuery, dbDataSet)
            End If
        End If

    End Function

    Private Function UpdateNetworkPath(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String) As String

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sPathProp As String = ""
        Dim sPropValueToAdd As String = "infolink"
        Dim sParentPathProp As String
        Dim sPkeyName As String
        Dim sPkeyPath As String = ""

        dbDataSet = oPrefs.GetPrefs("GROUP", "Everyone", sPkeyCatalog, "", "", "", "", "", "CopyFileToNetworkDefaultPathOption", "")
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sPathProp = dbDataSet.Tables(0).Rows(0)("PREFVALUE") & ""
            If sPathProp <> "PARENTANDCURRENT" Then
                Exit Function
            End If
        Else
            Exit Function
        End If

        sPathProp = ""
        dbDataSet = oPrefs.GetPrefs("GROUP", "Everyone", sPkeyCatalog, "", "", "", "", "", "CopyFileToNetworkPhysicalPathProp")

        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sPathProp = dbDataSet.Tables(0).Rows(0)("PREFVALUE") & ""
        End If

        If sPathProp <> "" Then

            sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sParentKey & "'"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
            If dbDataSet.Tables(0).Rows.Count > 0 Then
                sParentPathProp = dbDataSet.Tables(0).Rows(0)(sPathProp) & ""
            End If

            If sParentPathProp = "" Then
                Exit Function
            End If

            sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sPkey & "'"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
            If dbDataSet.Tables(0).Rows.Count > 0 Then
                sPkeyName = dbDataSet.Tables(0).Rows(0)("PHEBDESC") & ""
            End If

            If sPkeyName = "" Then
                Exit Function
            End If

            sPkeyPath = sParentPathProp & "\" & sPkeyName

            sSqlQuery = "update t_cat_part set " & sPathProp & "='" & sPkeyPath & "' where t_Cat_part.pkey='" & sPkey & "'"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)

        End If

        UpdateNetworkPath = sPkeyPath

    End Function


    Private Function UpdateInfoBagLink(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sParentPkeyType As String
        Dim sPropValueToAdd As String = "infolink"


        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sParentKey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sParentPkeyType = dbDataSet.Tables(0).Rows(0)("PKEYTYPE") & ""
        End If

        'If sParentPkeyType = "4" Then
        sSqlQuery = "update t_cat_nodes set " & sPropValue & "='" & sPropValueToAdd & "' where t_Cat_nodes.pkey='" & sPkey & "' and t_Cat_nodes.PPARENTKEY='" & sParentKey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        'End If

    End Function

    Private Function UpdateGroupLeader(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sIsLeader As String = ""

        sSqlQuery = "SELECT * FROM T_CAT_NODES WHERE PPARENTKEY='" & sParentKey & "' and PKEY='" & sPkey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sIsLeader = dbDataSet.Tables(0).Rows(0)(sPropValue) & ""
        End If

        If sIsLeader = "leader" Then
            sSqlQuery = "update t_cat_nodes set " & sPropValue & "='' where t_Cat_nodes.pkey<>'" & sPkey & "' and t_Cat_nodes.PPARENTKEY='" & sParentKey & "'"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
        End If

    End Function

    Private Function MultiDuplicatePaste(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String, ByVal sProp As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sMultiFormula As String = sProp
        Dim sOptionalParams As String = sPropValue

        Dim arrForumula As String() = sMultiFormula.Split("|")
        Dim sFormula As String = arrForumula(0)
        Dim sIndexProp As String = arrForumula(1)
        Dim sIndexValue As String = arrForumula(2)
        Dim arrUpdateProps As String() = {}

        Dim oHandlers As CHandlers

        If sOptionalParams <> "" Then
            arrUpdateProps = sOptionalParams.Split("|")
        End If

        Dim arrUpdateProp As String()

        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sPkey & "'"
        oConn.Open_Dataset(sSqlQuery, dbDataSet, "CurrentPkey")

        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sParentKey & "'"
        oConn.Open_Dataset(sSqlQuery, dbDataSet, "ParentPkey")

        Dim iStartIndex As Integer = 0
        Dim iEndIndex As Integer = 0
        Dim iValue As Integer = 0

        sFormula = ConvertToArithmetic(sFormula, "pkey.", dbDataSet.Tables("CurrentPkey").Rows(0))
        sFormula = ConvertToArithmetic(sFormula, "parent.", dbDataSet.Tables("ParentPkey").Rows(0))

        Dim calc As New CCalc
        calc.Function = sFormula
        calc.BuildFunctionTree()
        Dim iObjectsCount As Decimal = calc.Result
        Dim i As Integer
        Dim propschange As New Hashtable

        For i = 0 To arrUpdateProps.Length - 1
            arrUpdateProp = arrUpdateProps(i).Split(",")
            sFormula = arrUpdateProp(1)
            'check if it string or aritmethic text (1-2+4 or date for example10/20/1899)
            If sFormula.StartsWith("'") Then
                sFormula = "S" & sFormula.Substring(1, sFormula.Length - 2)
            Else
                'check if we need to calc expressions.
                If FindAritmetic(sFormula, 0) < sFormula.Length Then
                    sFormula = ConvertToArithmetic(sFormula, "pkey.", dbDataSet.Tables("CurrentPkey").Rows(0))
                    sFormula = "N" & ConvertToArithmetic(sFormula, "parent.", dbDataSet.Tables("ParentPkey").Rows(0))
                Else
                    sFormula = ConvertToArithmetic(sFormula, "pkey.", dbDataSet.Tables("CurrentPkey").Rows(0))
                    sFormula = "S" & ConvertToArithmetic(sFormula, "parent.", dbDataSet.Tables("ParentPkey").Rows(0))
                End If
            End If
            arrUpdateProp(1) = sFormula
            propschange.Add(LCase(arrUpdateProp(0)), arrUpdateProp(1))
        Next

        Dim j As Integer
        Dim sSquQuery As String
        Dim sColumns As String
        Dim sColumnsValues As String
        Dim oColumn As Data.DataColumn
        Dim sNewPkey As String
        Dim sExtraColumns As String
        Dim sExtraColumnsValues As String
        Dim obj As String
        Dim iCurrentIndexValue As Integer
        Dim sFormulaValue As String
        Dim dbDynamicDataset As Data.DataSet

        For i = 1 To iObjectsCount

            sColumns = ""
            sExtraColumns = ""
            sExtraColumnsValues = ""

            iCurrentIndexValue = i + CInt(sIndexValue) - 1

            For Each oColumn In dbDataSet.Tables(0).Columns
                If LCase(oColumn.ColumnName) <> "pkey" And LCase(oColumn.ColumnName) <> "pkeypage" And Not propschange.ContainsKey(LCase(oColumn.ColumnName)) And LCase(sIndexProp) <> LCase(oColumn.ColumnName) Then
                    sColumns = sColumns & oColumn.ColumnName & ", "
                End If
            Next
            Dim sResult As String = ""
            For Each obj In propschange.Keys
                sExtraColumns = sExtraColumns & obj & ", "
                sFormulaValue = propschange(obj)
                If (sFormulaValue.StartsWith("N") And FindAritmetic(sFormulaValue, 0) < sFormulaValue.Length) Or sFormulaValue = "index" Then
                    sFormulaValue = sFormulaValue.Replace("index", iCurrentIndexValue.ToString())
                    calc.Function = sFormulaValue.Substring(1)
                    calc.BuildFunctionTree()
                    sResult = calc.Result.ToString()
                Else
                    sResult = sFormulaValue.Substring(1)
                End If
                sExtraColumnsValues = sExtraColumnsValues & "'" & sResult & "', "
            Next

            sColumnsValues = sColumns & sExtraColumnsValues
            sColumnsValues = Mid(sColumnsValues, 1, sColumnsValues.Length - 2)
            sColumns = sColumns & sExtraColumns
            sColumns = Mid(sColumns, 1, sColumns.Length - 2)

            dbDynamicDataset = New Data.DataSet
            sNewPkey = getNewPkey()
            sSqlQuery = "INSERT INTO T_CAT_PART(PKEY,PKEYPAGE, " & sIndexProp & "," & sColumns & ") SELECT '" & sNewPkey & "','" & sParentKey & "','" & iCurrentIndexValue & "', " & sColumnsValues & " FROM T_CAT_PART WHERE PKEY='" & sPkey & "'"
            oConn.Open_Dataset(sSqlQuery, dbDynamicDataset)

            sSqlQuery = "INSERT INTO T_CAT_NODES(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20) (SELECT '" & sNewPkey & "','" & sParentKey & "',POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20 FROM T_CAT_NODES WHERE PKEY='" & sPkey & "' AND PPARENTKEY='" & sParentKey & "')"
            oConn.Open_Dataset(sSqlQuery, dbDynamicDataset)
            dbDynamicDataset.Dispose()
            dbDynamicDataset = Nothing

            oHandlers = New CHandlers(Me.sODBCName)
            oHandlers.ObjectEvents("DUPLICATEOBJECT", sNewPkey, sParentKey, Me.sPkeyCatalog)
            oHandlers = Nothing

            If Not alEffectedPkeys Is Nothing Then
                alEffectedPkeys.Add(sNewPkey)
            End If

        Next

        dbDataSet.Dispose()
        dbDataSet = Nothing


    End Function

    Private Function SetUniqueIndex(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String, ByVal sProp As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sPropToChange As String = sProp
        Dim sFunctionParameters As String = sPropValue
        Dim arrFunctionParameters As String() = sFunctionParameters.Split("|")
        Dim sTableCheck As String = arrFunctionParameters(0)
        Dim sRangeCheck As String = arrFunctionParameters(1)
        Dim sPropCheck As String = arrFunctionParameters(2)
        Dim lValue As Long = 0

        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sPkey & "'"
        oConn.Open_Dataset(sSqlQuery, dbDataSet, "CurrentPkey")

        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sParentKey & "'"
        oConn.Open_Dataset(sSqlQuery, dbDataSet, "ParentPkey")

        sRangeCheck = ConvertToArithmetic(sRangeCheck, "pkey.", dbDataSet.Tables("CurrentPkey").Rows(0))
        sRangeCheck = ConvertToArithmetic(sRangeCheck, "parent.", dbDataSet.Tables("ParentPkey").Rows(0))

        Dim oValidateFormPage As New ValidateForm
        lValue = oValidateFormPage.P_GetUniqueIndex(sTableCheck, sRangeCheck, sPropCheck)
        oValidateFormPage.Dispose()

        sSqlQuery = "UPDATE T_CAT_PART SET " & sPropToChange & "='" & lValue.ToString() & "' WHERE PKEY='" & sPkey & "'"
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        dbDataSet.Dispose()
        dbDataSet = Nothing


    End Function

    Function ConvertToArithmetic(ByVal sFormula As String, ByVal sReplaceString As String, ByVal oDataRow As Data.DataRow) As String

        Dim iStartIndex As Integer
        Dim iEndIndex As Integer
        Dim iValue As Long
        Dim sValue As String

        While sFormula.IndexOf(sReplaceString) > -1
            iStartIndex = sFormula.IndexOf(sReplaceString) + sReplaceString.Length
            iEndIndex = FindAritmetic(sFormula, iStartIndex)

            'maybe the string contain 'pkey.pprop52' -> remove '
            If sFormula.Substring(iStartIndex, iEndIndex - iStartIndex).StartsWith("'") Then
                iStartIndex = iStartIndex + 1
            End If

            If sFormula.Substring(iStartIndex, iEndIndex - iStartIndex).EndsWith("'") Then
                iEndIndex = iEndIndex - 1
            End If

            If Not IsDBNull(oDataRow(sFormula.Substring(iStartIndex, iEndIndex - iStartIndex))) Then
                If IsNumeric(oDataRow(sFormula.Substring(iStartIndex, iEndIndex - iStartIndex))) Then
                    iValue = CLng(oDataRow(sFormula.Substring(iStartIndex, iEndIndex - iStartIndex)))
                    sValue = iValue.ToString()
                Else
                    sValue = oDataRow(sFormula.Substring(iStartIndex, iEndIndex - iStartIndex))
                End If
            Else
                sValue = ""
            End If
            sFormula = sFormula.Replace(sFormula.Substring(iStartIndex - sReplaceString.Length, iEndIndex - iStartIndex + sReplaceString.Length), sValue)
        End While
        ConvertToArithmetic = sFormula
    End Function

    Function getNewPkey() As String

        Dim sSqlQuery As String
        Dim oTempConn As New CConnection
        Dim sNewPkey As String
        Dim dbDataSet As Data.DataSet

        If System.Web.HttpContext.Current.Application("gemAppMode") = "Access" Then
            sSqlQuery = "SELECT T_CAT_MAINOPTION.PCVALUE FROM(T_CAT_MAINOPTION) WHERE (((T_CAT_MAINOPTION.PCNAME)='PLASTSEQNUMBER'))"
        Else
            sSqlQuery = "SELECT PKEY_SEQNUMBER.NEXTVAL FROM DUAL"
        End If

        If System.Web.HttpContext.Current.Application("gemAppMode") = "Access" Then
            oTempConn.Open_Access_Connection(System.Web.HttpContext.Current.Application("ODBCNAME"))
        Else
            oTempConn.Open_Oracle_Connection(System.Web.HttpContext.Current.Application("ODBCNAME"))
        End If

        dbDataSet = New Data.DataSet
        oTempConn.Open_Dataset(sSqlQuery, dbDataSet)
        sNewPkey = System.Web.HttpContext.Current.Application("PkeyPrefix") & "" & dbDataSet.Tables(0).Rows(0).Item(0)
        dbDataSet = Nothing

        'only for access
        If System.Web.HttpContext.Current.Application("gemAppMode") = "Access" Then
            dbDataSet = New Data.DataSet
            sSqlQuery = "UPDATE T_CAT_MAINOPTION SET T_CAT_MAINOPTION.PCVALUE = PCVALUE+1 WHERE (((T_CAT_MAINOPTION.PCNAME)='PLASTSEQNUMBER'))"
            oTempConn.Open_Dataset(sSqlQuery, dbDataSet)
            dbDataSet = Nothing
        End If

        oTempConn.Close_Connection()
        oTempConn = Nothing
        getNewPkey = sNewPkey

    End Function

    Function FindAritmetic(ByVal sFormula As String, ByVal iIndex As Integer) As Integer
        Dim arr(7) As Integer
        Dim iMin As Integer = sFormula.Length
        Dim i As Integer

        arr(0) = sFormula.IndexOf("*", iIndex)
        arr(1) = sFormula.IndexOf("/", iIndex)
        arr(2) = sFormula.IndexOf("+", iIndex)
        arr(3) = sFormula.IndexOf("-", iIndex)
        arr(4) = sFormula.IndexOf(")", iIndex)
        arr(5) = sFormula.IndexOf("(", iIndex)
        arr(6) = sFormula.IndexOf("%", iIndex)
        arr(7) = sFormula.IndexOf("@", iIndex)

        For i = 0 To arr.Length - 1
            If arr(i) <> -1 And arr(i) < iMin Then
                iMin = arr(i)
            End If
        Next

        FindAritmetic = iMin
    End Function

    Private Function UpdatePropsOnUpdateReport(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String

        Dim dbDataSet As New Data.DataSet
        Dim sPropValueToAdd As String
        Dim sProps As String()
        Dim sWhereProp As String
        Dim sIDProp As String
        Dim sLimitProp As String
        Dim sSqlProps As String = ""
        Dim sCheckProps As String()
        Dim i As Integer
        Dim iLimit As Integer = 0


        sProps = sPropValue.Split("|")
        sWhereProp = sProp
        sCheckProps = sPoperator.Split("|")

        sIDProp = sCheckProps(0)
        sLimitProp = sCheckProps(1)

        For i = 0 To sProps.Length - 1
            sSqlProps = sSqlProps & sProps(i) & ","
        Next

        sSqlProps = sSqlProps & sIDProp & "," & sLimitProp

        sSqlQuery = "SELECT " & sSqlProps & " FROM T_CAT_PART WHERE PKEY='" & sPkey & "'"

        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        sSqlProps = ""
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            For i = 0 To sProps.Length - 1
                sSqlProps = sSqlProps & sProps(i) & "='" & dbDataSet.Tables(0).Rows(0)(sProps(i)) & "',"
            Next
        End If

        Dim sIdPropValue As String = dbDataSet.Tables(0).Rows(0)(sIDProp)

        sSqlProps = sSqlProps.Remove(sSqlProps.Length - 1, 1)

        If dbDataSet.Tables(0).Rows(0)(sIDProp) <> "0" Then

            If IsNumeric(dbDataSet.Tables(0).Rows(0)(sLimitProp)) Then
                iLimit = CInt(dbDataSet.Tables(0).Rows(0)(sLimitProp))
            End If

            sSqlQuery = "UPDATE t_cat_part set " & sSqlProps & " where PKEY <> '" & sPkey & "' and " & sIDProp & "='" & dbDataSet.Tables(0).Rows(0)(sIDProp) & "' and " & sWhereProp & " and rownum <" & iLimit.ToString()
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)

            sSqlQuery = "UPDATE t_cat_part set " & sLimitProp & "='0' where " & sIDProp & "='" & sIdPropValue & "'"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
            dbDataSet.Dispose()

        End If

    End Function

    Private Function MoveChildsToNewItem(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sObjects As String
        Dim saObjects As String()
        Dim sObjectsParent As String

        If Not System.Web.HttpContext.Current.Session("PasteAsDup") Is Nothing Then
            If Not System.Web.HttpContext.Current.Session("SavedObjects") Is Nothing Then
                sObjects = System.Web.HttpContext.Current.Session("SavedObjects")
                sObjectsParent = System.Web.HttpContext.Current.Session("SavedObjectsParent")

                saObjects = sObjects.Split("|")
                Dim i As Integer
                For i = 0 To saObjects.Length - 1
                    If saObjects(i) <> "" Then
                        sSqlQuery = "update t_cat_nodes set pparentkey='" & sPkey & "' where t_Cat_nodes.pkey='" & saObjects(i) & "' and t_Cat_nodes.pparentkey='" & sObjectsParent & "'"
                        dbDataSet.Clear()
                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                    End If
                Next
                'Dim valform As New DefaultValidator

                System.Web.HttpContext.Current.Session.Remove("SavedObjects")
            End If

            Dim sProps As String()
            Dim sPropSrc As String
            Dim sPropDest As String
            Dim sVal As String

            sProps = sPropValue.Split("|")
            sPropSrc = sProps(0)
            sPropDest = sProps(1)

            sSqlQuery = "select * from t_cat_part where pkey='" & sPkey & "'"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)

            If dbDataSet.Tables(0).Rows.Count > 0 Then
                sVal = dbDataSet.Tables(0).Rows(0)(sPropSrc) & ""
            End If

            Dim valiform As New ValidateForm
            Dim sNewVal As String = valiform.GenerateFlightID(sVal, sPropDest, System.Web.HttpContext.Current.Application("ODBCNAME"))

            sSqlQuery = "update t_cat_part set phebdesc='New ' || phebdesc," & sPropDest & "='" & sNewVal & "' where t_Cat_part.pkey='" & sPkey & "'"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)

            System.Web.HttpContext.Current.Session("NewObjectPkey") = sPkey
        End If

    End Function

    Private Function UpdateParentPropToChilds(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sPropValueToAdd As String
        Dim sProps As String()
        Dim sBoolProp As String
        Dim sPropSrc As String
        Dim sPropDest As String

        sProps = sPropValue.Split("|")
        sPropSrc = sProps(0)
        sPropDest = sProps(1)

        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sPkey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sPropValueToAdd = dbDataSet.Tables(0).Rows(0)(sPropSrc) & ""
            If sProp <> "" Then
                sBoolProp = dbDataSet.Tables(0).Rows(0)(sProp) & ""
            Else
                sBoolProp = "true"
            End If
        End If

        If sBoolProp.ToLower() = "true" Then
            sSqlQuery = "update t_cat_part set " & sPropDest & "='" & sPropValueToAdd & "' where PKEY in (select distinct pkey from t_cat_nodes where pglobalstatus<>4 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sPkey & "')"
            dbDataSet.Clear()
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
        End If

    End Function

    Private Function StartTextEditor(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String)

        Dim sTemplate As String = "Site_New_Text_E.xsl"
        Dim sJavaScript As String = "w1=window.open('addObject.aspx?PkeyCatalog=" & sPkeyCatalog & "&FatherPkey=" & sPkey & "&ParentFather=" & sParentKey & "&Template=" & sTemplate & "','AddObj','width=950,height=700,toolbar=no,scrollbars=yes');w1.focus();"

        System.Web.HttpContext.Current.Response.Write("<script>" & sJavaScript & "</script>")

    End Function

    Private Function taskCount33(ByVal sPkey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String)

        Dim sSqlQuery As String
        Dim nTaskCount As Long = 5
        Dim sPPROP10 As String
        Dim dbDataSet As New Data.DataSet

        sSqlQuery = "select TASKCOUNTER.NEXTVAL from dual"
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            nTaskCount = dbDataSet.Tables(0).Rows(0).Item(0)
        End If

        sSqlQuery = "select t_Cat_part.pprop41 || TO_CHAR(" & CStr(nTaskCount) & ",'0000') from t_Cat_part where t_Cat_part.PKEY in (select t_caT_nodes.PPARENTKEY from t_caT_nodes where pkey='" & sPkey & "')"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sPPROP10 = dbDataSet.Tables(0).Rows(0)(1) & ""
        End If

        sSqlQuery = "update t_cat_part set PPROP10='" & sPPROP10 & "' where t_Cat_part.PKEY='" & sPkey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)


    End Function

    Private Function taskCount34(ByVal sPkey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String)

        Dim sSqlQuery As String
        Dim nTaskCount As Long = 5
        Dim sPPROP1 As String
        Dim dbDataSet As New Data.DataSet

        sSqlQuery = "select t_Cat_part.pprop10 || '-'|| substr('00' || t_Cat_part.pprop5,length('00' || t_Cat_part.pprop5)-1) from t_Cat_part where t_Cat_part.PKEY in (select t_caT_nodes.PPARENTKEY from t_caT_nodes where pkey='" & sPkey & "')"
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sPPROP1 = dbDataSet.Tables(0).Rows(0).Item(0) & ""
        End If

        sSqlQuery = "update t_cat_part set PPROP1='" & sPPROP1 & "' where t_Cat_part.PKEY='" & sPkey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        sSqlQuery = "update t_Cat_part set pprop5=cast(pprop5 as number) +1  where t_Cat_part.PKEY in (select t_caT_nodes.PPARENTKEY from t_caT_nodes where pkey='" & sPkey & "')"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

    End Function

    Private Function InheritParentPropToCurrent(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sPropValueToAdd As String
        Dim sProps As String()
        Dim sPropSrc As String
        Dim sPropDest As String

        sProps = sPropValue.Split("|")
        sPropSrc = sProps(0)
        sPropDest = sProps(1)

        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sParentKey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sPropValueToAdd = dbDataSet.Tables(0).Rows(0)(sPropSrc) & ""
        End If

        sSqlQuery = "update t_cat_part set " & sPropDest & "='" & sPropValueToAdd & "' where t_Cat_part.PKEY='" & sPkey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

    End Function

    Private Function InheritParentPropToChilds(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim dbDataSet As New Data.DataSet
        Dim sPropValueToAdd As String
        Dim sProps As String()
        Dim sPropSrc As String
        Dim sPropDest As String

        sProps = sPropValue.Split("|")
        sPropSrc = sProps(0)
        sPropDest = sProps(1)

        sSqlQuery = "SELECT * FROM T_CAT_PART WHERE PKEY='" & sParentKey & "'"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sPropValueToAdd = dbDataSet.Tables(0).Rows(0)(sPropSrc) & ""
        End If

        sSqlQuery = "update t_cat_part set " & sPropDest & "='" & sPropValueToAdd & "' where PKEY in (select distinct pkey from t_cat_nodes where pglobalstatus<>4 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sPkey & "')"
        dbDataSet.Clear()
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

    End Function

    Private Function FillImportKeyProp(ByVal sPkey As String, ByVal sParentKey As String, ByVal sPkeyCatalog As String, ByVal sObjectType As String, ByVal sPkeyType As String, ByVal sPropValue As String)

        Dim sSqlQuery As String
        Dim sKeyProp As String
        Dim dbDataSet As New Data.DataSet

        sSqlQuery = "SELECT * FROM T_CAT_PART_LABLES WHERE PKEYCATALOG='" & sPkeyCatalog & "' AND PIMPORTKEY=1"
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        If dbDataSet.Tables(0).Rows.Count > 0 Then
            sKeyProp = dbDataSet.Tables(0).Rows(0)("PKEY") & ""
            sSqlQuery = "UPDATE T_CAT_PART SET " & sKeyProp & "=PKEY WHERE PKEY='" & sPkey & "' AND " & sKeyProp & " IS NULL"
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
        End If

    End Function

End Class
