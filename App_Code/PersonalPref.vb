Public Class PersonalPref

    Dim sODBCName As String = ""
    Dim oConn As CConnection

    Public Sub New(ByVal sODBCName As String)

        Me.sODBCName = sODBCName

    End Sub
    Private Function BuildGetSQL(ByVal fAccessor As String, ByVal fAccessorValue As String, Optional ByVal fPkeyWS As String = "", Optional ByVal fObjectClassKey As String = "", Optional ByVal fObjectTypeKey As String = "", Optional ByVal fProp As String = "", Optional ByVal fPOperator As String = "", Optional ByVal fPropValue As String = "", Optional ByVal fPref As String = "", Optional ByVal fPrefType As String = "", Optional ByVal fPrefREM As String = "") As String

        Dim sSqlGetQuery As String = ""

        sSqlGetQuery = "SELECT Accessor,AccessorValue,PkeyWS,Objectclasskey,Objecttypekey,Prop,POperator,Propvalue,Pref,Prefvalue,Preftype FROM LWWORKSPACEPREF WHERE Accessor='" & fAccessor & "' AND AccessorValue='" & fAccessorValue & "' "

        If (Not fPkeyWS = "") Then
            sSqlGetQuery = sSqlGetQuery & "AND PKEYWS='" & fPkeyWS & "' "
        End If

        If (Not fObjectClassKey = "") Then
            If (fObjectClassKey = "NULL") Then
                sSqlGetQuery = sSqlGetQuery & "AND OBJECTCLASSKEY is " & fObjectClassKey & " "
            Else
                sSqlGetQuery = sSqlGetQuery & "AND OBJECTCLASSKEY=" & CInt(fObjectClassKey) & " "
            End If

        End If

        If (Not fObjectTypeKey = "") Then
            If (fObjectTypeKey = "NULL") Then
                sSqlGetQuery = sSqlGetQuery & "AND OBJECTTYPEKEY is " & fObjectTypeKey & " "
            Else
                sSqlGetQuery = sSqlGetQuery & "AND OBJECTTYPEKEY='" & fObjectTypeKey & "' "
            End If

        End If

        If (Not fProp = "") Then
            If (fProp = "NULL") Then
                sSqlGetQuery = sSqlGetQuery & "AND PROP is " & fProp & " "
            Else
                sSqlGetQuery = sSqlGetQuery & "AND PROP='" & fProp & "' "
            End If
        End If

        If (Not fPOperator = "") Then
            If (fProp = "NULL") Then
                sSqlGetQuery = sSqlGetQuery & "AND POPERATOR is " & fPOperator & " "
            Else
                sSqlGetQuery = sSqlGetQuery & "AND POPERATOR='" & fPOperator & "' "
            End If

        End If

        If (Not fPropValue = "") Then
            If (fProp = "URL" Or fObjectClassKey = "19") Then
                If (fPropValue.ToLower().IndexOf("generatereport") <> -1) Then
                    Dim pos As Integer = fPropValue.IndexOf("&", fPropValue.IndexOf("&XSL") + 1)
                    If (pos = -1) Then
                        pos = fPropValue.Length
                    End If
                    fPropValue = fPropValue.Remove(fPropValue.IndexOf("&XSL"), pos - fPropValue.IndexOf("&XSL"))
                End If
            End If
            Dim temp As String = fPropValue.Replace("&", "*")
            If (fPropValue = "NULL") Then
                sSqlGetQuery = sSqlGetQuery & "AND PROPVALUE is " & temp & " "
            Else
                sSqlGetQuery = sSqlGetQuery & "AND PROPVALUE='" & temp & "' "
            End If

        End If

        If (Not fPref = "") Then
            If fPrefREM = "REMOVE" Then
                sSqlGetQuery = sSqlGetQuery & "AND " & fPref & " "
            Else
                sSqlGetQuery = sSqlGetQuery & "AND PREF='" & fPref & "' "
            End If

        End If

        If (Not fPrefType = "") Then
            sSqlGetQuery = sSqlGetQuery & "AND PREFTYPE='" & fPrefType & "' "
        End If

        BuildGetSQL = sSqlGetQuery

    End Function

    Public Function GetPrefs(ByVal fAccessor As String, ByVal fAccessorValue As String, Optional ByVal fPkeyWS As String = "", Optional ByVal fObjectClassKey As String = "", Optional ByVal fObjectTypeKey As String = "", Optional ByVal fProp As String = "", Optional ByVal fPOperator As String = "", Optional ByVal fPropValue As String = "", Optional ByVal fPref As String = "", Optional ByVal fPrefType As String = "", Optional ByVal bSpec As Boolean = False) As Data.DataSet

        Dim sSqlBuiltQuery As String
        Dim dbDataSetUser As New Data.DataSet
        Dim dbDataSetTemp As New Data.DataSet
        Dim prefstring As String = "("
        Dim prefAvail As Boolean = False


        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCName)

        sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, fPkeyWS, fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, fPref, fPrefType)
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetUser)

        If dbDataSetUser.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To dbDataSetUser.Tables(0).Rows.Count - 1
                If Not IsDBNull(dbDataSetUser.Tables(0).Rows(i)("PREF")) Then
                    prefstring = prefstring & "PREF <> '" & dbDataSetUser.Tables(0).Rows(i)("PREF") & "' AND "
                    prefAvail = True
                End If
            Next
            prefstring = prefstring & ") "
            prefstring = prefstring.Remove(prefstring.LastIndexOf("AND"), 3)

            sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, "default", fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
        Else
            sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, "default", fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, fPref, fPrefType)
        End If

        If (bSpec) Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
                If (prefstring.Length > 3) Then
                    prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                Else
                    prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                    prefstring = prefstring & ") "
                End If
            Next
            prefAvail = True
            sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, fPkeyWS, fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
        Else
            If prefAvail Then
                sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, fPkeyWS, fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
            Else
                sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, fPkeyWS, fObjectClassKey, "NULL", "NULL", "NULL", "NULL", fPref, fPrefType)
            End If
        End If


        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        dbDataSetTemp.Clear()
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
                If (prefstring.Length > 3) Then
                    prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                Else
                    prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                    prefstring = prefstring & ") "
                End If
            Next
            prefAvail = True
            sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, "default", fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
        Else
            If prefAvail Then
                sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, "default", fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
            Else
                sSqlBuiltQuery = BuildGetSQL(fAccessor, fAccessorValue, "default", fObjectClassKey, "NULL", "NULL", "NULL", "NULL", fPref, fPrefType)
            End If
        End If


        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        dbDataSetTemp.Clear()
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
                If (prefstring.Length > 3) Then
                    prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                Else
                    prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                    prefstring = prefstring & ") "
                End If
            Next
            prefAvail = True
            sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
        Else
            If prefAvail Then
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
            Else
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, fPref, fPrefType)
            End If
        End If


        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        dbDataSetTemp.Clear()
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
                If (prefstring.Length > 3) Then
                    prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                Else
                    prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                    prefstring = prefstring & ") "
                End If
            Next
            prefAvail = True
            sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
        Else
            If prefAvail Then
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
            Else
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, fPref, fPrefType)
            End If
        End If


        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        dbDataSetTemp.Clear()
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
                If (prefstring.Length > 3) Then
                    prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                Else
                    prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                    prefstring = prefstring & ") "
                End If
            Next
            prefAvail = True
            sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
        Else
            If prefAvail Then
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
            Else
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, "NULL", "NULL", "NULL", "NULL", fPref, fPrefType)
            End If
        End If


        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        dbDataSetTemp.Clear()
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
                If (prefstring.Length > 3) Then
                    prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                Else
                    prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                    prefstring = prefstring & ") "
                End If
            Next
            prefAvail = True
            sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
        Else
            If prefAvail Then
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
            Else
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", fProp, fPOperator, fPropValue, fPref, fPrefType)
            End If
        End If

        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        dbDataSetTemp.Clear()
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
                If (prefstring.Length > 3) Then
                    prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                Else
                    prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
                    prefstring = prefstring & ") "
                End If
            Next
            prefAvail = True
            sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
        Else
            If prefAvail Then
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", "NULL", "NULL", "NULL", prefstring, fPrefType, "REMOVE")
            Else
                sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", "NULL", "NULL", "NULL", fPref, fPrefType)
            End If
        End If

        ''dbDataSetTemp.Clear()
        ''oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)

        ''If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
        ''    Dim j As Integer
        ''    For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
        ''        dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
        ''        If (prefstring.Length > 3) Then
        ''            prefstring = prefstring.Insert(prefstring.Length - 2, "AND PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
        ''        Else
        ''            prefstring = prefstring.Insert(1, "PREF<>'" & dbDataSetTemp.Tables(0).Rows(j)("PREF") & "' ")
        ''            prefstring = prefstring & ") "
        ''        End If
        ''    Next
        ''    prefAvail = True
        ''    sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
        ''Else
        ''    If prefAvail Then
        ''        sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", fProp, fPOperator, fPropValue, prefstring, fPrefType, "REMOVE")
        ''    Else
        ''        sSqlBuiltQuery = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "NULL", fProp, fPOperator, fPropValue, fPref, fPrefType)
        ''    End If
        ''End If


        If (prefAvail) And (fPref <> "") Then
            oConn.Close_Connection()
            GetPrefs = dbDataSetUser
            Exit Function
        End If

        dbDataSetTemp.Clear()
        oConn.Open_Dataset(sSqlBuiltQuery, dbDataSetTemp)


        If dbDataSetTemp.Tables(0).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dbDataSetTemp.Tables(0).Rows.Count - 1
                dbDataSetUser.Tables(0).ImportRow(dbDataSetTemp.Tables(0).Rows(j))
            Next
        End If

        oConn.Close_Connection()
        GetPrefs = dbDataSetUser

    End Function
    Public Function GetPrefsByObject(ByVal fAccessor As String, ByVal fAccessorValue As String, Optional ByVal fPkeyWS As String = "", Optional ByVal fObjectClassKey As String = "", Optional ByVal fObjectTypeKey As String = "", Optional ByVal fProp As String = "", Optional ByVal fPOperator As String = "", Optional ByVal fPropValue As String = "", Optional ByVal fPref As String = "", Optional ByVal fPrefType As String = "", Optional ByVal bSpec As Boolean = False) As Data.DataSet

        Dim dbPrefbase As New Data.DataSet
        Dim sSqlString As String

        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCName)

        sSqlString = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, fObjectTypeKey, "", "", "", fPref, "")
        oConn.Open_Dataset(sSqlString, dbPrefbase)
        If (dbPrefbase.Tables(0).Rows.Count > 0) Then
            oConn.Close_Connection()
            GetPrefsByObject = dbPrefbase
            Exit Function
        Else
            dbPrefbase.Clear()
            sSqlString = BuildGetSQL("GROUP", "Everyone", fPkeyWS, fObjectClassKey, "", "", "", "", fPref, "")
            oConn.Open_Dataset(sSqlString, dbPrefbase)
            If (dbPrefbase.Tables(0).Rows.Count > 0) Then
                oConn.Close_Connection()
                GetPrefsByObject = dbPrefbase
                Exit Function
            Else
                dbPrefbase.Clear()
                sSqlString = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, fObjectTypeKey, "", "", "", fPref, "")
                oConn.Open_Dataset(sSqlString, dbPrefbase)
                If (dbPrefbase.Tables(0).Rows.Count > 0) Then
                    oConn.Close_Connection()
                    GetPrefsByObject = dbPrefbase
                    Exit Function
                Else
                    dbPrefbase.Clear()
                    sSqlString = BuildGetSQL("GROUP", "Everyone", "default", fObjectClassKey, "", "", "", "", fPref, "")
                    oConn.Open_Dataset(sSqlString, dbPrefbase)
                    If (dbPrefbase.Tables(0).Rows.Count > 0) Then
                        oConn.Close_Connection()
                        GetPrefsByObject = dbPrefbase
                        Exit Function
                    Else
                        oConn.Close_Connection()
                        GetPrefsByObject = dbPrefbase
                        Exit Function
                    End If
                End If
            End If
        End If

        GetPrefsByObject = dbPrefbase

        dbPrefbase.Dispose()
        dbPrefbase = Nothing

    End Function

    Public Function SetPrefs(ByVal fAccessor As String, ByVal fAccessorValue As String, Optional ByVal fPkeyWS As String = "", Optional ByVal fObjectClassKey As String = "", Optional ByVal fObjectTypeKey As String = "", Optional ByVal fProp As String = "", Optional ByVal fPOperator As String = "", Optional ByVal fPropValue As String = "", Optional ByVal fPref As String = "", Optional ByVal fPrefValue As String = "", Optional ByVal fPrefType As String = "")
        Dim sSqlGetQuery As String
        Dim dbDataSet As New Data.DataSet

        Dim dbTempSet As Data.DataSet = GetPrefs(fAccessor, fAccessorValue, fPkeyWS, fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, fPref, fPrefType, True)

        If dbTempSet.Tables(0).Rows.Count <> 0 Then
            DeletePref(fAccessor, fAccessorValue, fPkeyWS, fObjectClassKey, fObjectTypeKey, fProp, fPOperator, fPropValue, fPref)
        End If

        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCName)

        sSqlGetQuery = "INSERT INTO LWWORKSPACEPREF (Accessor,AccessorValue,PkeyWS,Objectclasskey,Objecttypekey,Prop,POperator,Propvalue,Pref,Prefvalue,Preftype) VALUES ('" & fAccessor & "','" & fAccessorValue & "'"

        sSqlGetQuery = sSqlGetQuery & ",'" & fPkeyWS & "'"

        If Not fObjectClassKey = "" Then
            sSqlGetQuery = sSqlGetQuery & "," & CInt(fObjectClassKey) & ""
        Else
            sSqlGetQuery = sSqlGetQuery & "," & "null" & ""
        End If

        sSqlGetQuery = sSqlGetQuery & ",'" & fObjectTypeKey & "'"

        sSqlGetQuery = sSqlGetQuery & ",'" & fProp & "'"

        sSqlGetQuery = sSqlGetQuery & ",'" & fPOperator & "'"

        If (Not fPropValue = "") Then
            If (fProp = "URL" Or fObjectClassKey = 19) Then
                If (fPropValue.ToLower().IndexOf("generatereport") <> -1) Then
                    Dim pos As Integer = fPropValue.IndexOf("&", fPropValue.IndexOf("&XSL") + 1)
                    If (pos = -1) Then
                        pos = fPropValue.Length
                    End If
                    fPropValue = fPropValue.Remove(fPropValue.IndexOf("&XSL"), pos - fPropValue.IndexOf("&XSL"))
                End If
            End If
            Dim temp As String = fPropValue.Replace("&", "*")
            sSqlGetQuery = sSqlGetQuery & ",'" & temp & "'"
        Else
            sSqlGetQuery = sSqlGetQuery & ",''"
        End If

        sSqlGetQuery = sSqlGetQuery & ",'" & fPref & "'"

        sSqlGetQuery = sSqlGetQuery & ",'" & fPrefValue & "'"

        sSqlGetQuery = sSqlGetQuery & ",'" & fPrefType & "'"

        sSqlGetQuery = sSqlGetQuery & ")"

        oConn.Open_Dataset(sSqlGetQuery, dbDataSet)
        oConn.Close_Connection()

    End Function

    Public Function DeletePref(ByVal fAccessor As String, ByVal fAccessorValue As String, Optional ByVal fPkeyWS As String = "", Optional ByVal fObjectClassKey As String = "", Optional ByVal fObjectTypeKey As String = "", Optional ByVal fProp As String = "", Optional ByVal fPOperator As String = "", Optional ByVal fPropValue As String = "", Optional ByVal fPref As String = "", Optional ByVal fPrefValue As String = "")

        Dim sSqlGetQuery As String
        Dim dbDataSet As New Data.DataSet

        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCName)

        sSqlGetQuery = "DELETE FROM LWWORKSPACEPREF WHERE Accessor='" & fAccessor & "' AND AccessorValue='" & fAccessorValue & "' "

        If (Not fPkeyWS = "") Then
            sSqlGetQuery = sSqlGetQuery & "AND PKEYWS='" & fPkeyWS & "' "
        Else
            sSqlGetQuery = sSqlGetQuery & "AND PKEYWS is null "
        End If

        If (Not fObjectClassKey = "") Then
            sSqlGetQuery = sSqlGetQuery & "AND OBJECTCLASSKEY=" & CInt(fObjectClassKey) & " "
        Else
            sSqlGetQuery = sSqlGetQuery & "AND OBJECTCLASSKEY is null "
        End If

        'If (Not fObjectTypeKey = "") Then
        '    sSqlGetQuery = sSqlGetQuery & "AND OBJECTTYPEKEY='" & fObjectTypeKey & "' "
        'End If

        If (Not fProp = "") Then
            sSqlGetQuery = sSqlGetQuery & "AND PROP='" & fProp & "' "
        Else
            sSqlGetQuery = sSqlGetQuery & "AND PROP is null "
        End If

        If (Not fPOperator = "") Then
            sSqlGetQuery = sSqlGetQuery & "AND POPERATOR='" & fPOperator & "' "
        Else
            sSqlGetQuery = sSqlGetQuery & "AND POPERATOR is null "
        End If

        If (Not fPropValue = "") Then
            If (fProp = "URL" Or fObjectClassKey = 19) Then
                If (fPropValue.ToLower().IndexOf("generatereport") <> -1) Then
                    Dim pos As Integer = fPropValue.IndexOf("&", fPropValue.IndexOf("&XSL") + 1)
                    If (pos = -1) Then
                        pos = fPropValue.Length
                    End If
                    fPropValue = fPropValue.Remove(fPropValue.IndexOf("&XSL"), pos - fPropValue.IndexOf("&XSL"))
                End If
            End If
            Dim temp As String = fPropValue.Replace("&", "*")
            sSqlGetQuery = sSqlGetQuery & "AND PROPVALUE='" & temp & "' "
        Else
            sSqlGetQuery = sSqlGetQuery & "AND PROPVALUE is null "
        End If

        If (Not fPref = "") Then
            sSqlGetQuery = sSqlGetQuery & "AND PREF='" & fPref & "' "
        Else
            sSqlGetQuery = sSqlGetQuery & "AND PREF is null"
        End If

        oConn.Open_Dataset(sSqlGetQuery, dbDataSet)
        oConn.Close_Connection()

    End Function

End Class
