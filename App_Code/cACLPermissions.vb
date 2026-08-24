Public Class cACLPermissions
    Implements IDisposable

    Private sLogonUser As String
    Private sUserGroup As String
    Private sUserGroupTemp As String
    Private sPkeyCatalogTemp As String
    Private sPkeyObject As String
    Private sCatalogPkey As String
    Private sObjectClass As String
    Private sPublish As Integer
    Private sObjectType As String
    Private sObjectStatus As String
    Private sObjectOwningUser As String
    Private sObjectOwningGroup As String
    Private dbDatasetAcl As New Data.DataSet
    Private dbDatasetACLPermissions As Data.DataSet
    Private oLWObjectTypes As New LWObjectTypes
    Private sODBCName As String
    Private gemAppMode As String
    Private oConn As CConnection
    Private cPPROP As Collection
    Private sAction As String
    Private isConnectionShared As Boolean = False

    Public Sub New(ByVal sLogonUser As String, ByVal sOdbcName As String, ByVal gemAppMode As String, Optional ByVal sObjectClass As String = "", Optional ByRef oConn As CConnection = Nothing)
        Dim ssqlquery As String
        Dim dbDatasetGroup As New Data.DataSet
        Dim i As Integer

        Me.sLogonUser = sLogonUser
        Me.sPkeyObject = sPkeyObject
        Me.sODBCName = sOdbcName
        'Me.sUserGroup = "Everyone"
        Me.gemAppMode = gemAppMode

        If Not oConn Is Nothing Then
            isConnectionShared = True
            Me.oConn = oConn
        Else
            'Open oracle connection
            Me.oConn = New CConnection
            If Me.gemAppMode = "Access" Then
                Me.oConn.Open_Access_Connection(sOdbcName)
            Else
                Me.oConn.Open_Oracle_Connection(sOdbcName)
            End If
        End If


    End Sub

    Public Function HasPermission(ByVal sPkeyObject As String, ByVal sAction As String, Optional ByVal sObjectClass As String = "") As String

        HasPermission = 1
    End Function

    Function SearchTree(ByVal ParentPkey As String) As Integer

        Dim odRow() As Data.DataRow
        Dim iRow As Integer
        Dim sConditionType As String
        Dim iOperator As Integer
        Dim sConditionValue As String
        Dim iAclPkey As Integer
        Dim iConditionPkey As Integer
        Dim iReturnValue As Integer
        Dim sDBValue As String
        odRow = dbDatasetAcl.Tables(0).Select("PARENTKEY=" & ParentPkey)

        If odRow.Length = 0 Then
            SearchTree = 0
        Else
            For iRow = 0 To odRow.Length - 1

                sConditionType = odRow(iRow).Item("ConditionType")
                iOperator = odRow(iRow).Item("Operator")
                sConditionValue = odRow(iRow).Item("ConditionValue")
                iAclPkey = odRow(iRow).Item("ACLPkey")
                iConditionPkey = odRow(iRow).Item("PKEY")

                If sConditionValue = "#CurrentUser#" Then
                    sConditionValue = sLogonUser
                End If

                If sConditionValue = "#CurrentGroup#" Then
                    sConditionValue = sUserGroup
                End If

                Select Case sConditionType
                    Case "ObjectClass"
                        iReturnValue = CheckCondition(sObjectClass, sConditionValue, iOperator, iAclPkey, iConditionPkey)
                        If iReturnValue <> 0 Then
                            Return iReturnValue
                        End If
                    Case "ObjectType"
                        iReturnValue = CheckCondition(sObjectType, sConditionValue, iOperator, iAclPkey, iConditionPkey)
                        If iReturnValue <> 0 Then
                            Return iReturnValue
                        End If
                    Case "ObjectStatus"
                        iReturnValue = CheckCondition(sObjectStatus, sConditionValue, iOperator, iAclPkey, iConditionPkey)
                        If iReturnValue <> 0 Then
                            Return iReturnValue
                        End If
                    Case "CurrentUser"
                        iReturnValue = CheckCondition(UCase(sLogonUser), UCase(sConditionValue), iOperator, iAclPkey, iConditionPkey)
                        If iReturnValue <> 0 Then
                            Return iReturnValue
                        End If
                    Case "OwningUser"
                        If UCase(sConditionValue) = "CURRENT" Then
                            iReturnValue = CheckCondition(UCase(sObjectOwningUser), UCase(sLogonUser), iOperator, iAclPkey, iConditionPkey)
                        Else
                            iReturnValue = CheckCondition(UCase(sObjectOwningUser), UCase(sConditionValue), iOperator, iAclPkey, iConditionPkey)
                        End If
                        If iReturnValue <> 0 Then
                            Return iReturnValue
                        End If
                    Case "CurrentGroup"
                        Dim TempArr() As String
                        Dim TempReturnValue As Boolean
                        Dim i As Integer

                        TempArr = Split(sUserGroupTemp, "|")
                        For i = 0 To UBound(TempArr, 1)
                            sUserGroup = TempArr(i)
                            If sUserGroup <> "" Then
                                iReturnValue = CheckCondition(UCase(sUserGroup), UCase(sConditionValue), iOperator, iAclPkey, iConditionPkey)
                                If iReturnValue <> 0 Then
                                    Return iReturnValue
                                End If
                            End If
                        Next
                    Case "OwningGroup"
                        iReturnValue = CheckCondition(sObjectOwningGroup, sConditionValue, iOperator, iAclPkey, iConditionPkey)
                        If iReturnValue <> 0 Then
                            Return iReturnValue
                        End If
                    Case "Catalog"
                        Dim TempArr() As String
                        Dim TempReturnValue As Boolean
                        Dim i As Integer

                        TempArr = Split(sPkeyCatalogTemp, "|")
                        For i = 0 To UBound(TempArr, 1)
                            sCatalogPkey = TempArr(i)
                            If sCatalogPkey <> "" Then
                                iReturnValue = CheckCondition(sCatalogPkey, sConditionValue, iOperator, iAclPkey, iConditionPkey)
                                If iReturnValue <> 0 Then
                                    Return iReturnValue
                                End If
                            End If
                        Next
                    Case "Publish"
                        iReturnValue = CheckCondition(sPublish, sConditionValue, iOperator, iAclPkey, iConditionPkey)
                        If iReturnValue <> 0 Then
                            Return iReturnValue
                        End If

                    Case Else
                        'prop.
                        If UCase(Left(sConditionType, 5)) = "PPROP" Then
                            Try
                                sDBValue = cPPROP.Item(UCase(sConditionType))
                                iReturnValue = CheckCondition(sDBValue, sConditionValue, iOperator, iAclPkey, iConditionPkey)
                            Catch ex As Exception
                                iReturnValue = 0
                            End Try

                            If iReturnValue <> 0 Then
                                Return iReturnValue
                            End If
                        End If
                End Select

            Next
        End If
    End Function

    Function CheckCondition(ByVal sConditionType As String, ByVal sConditionValue As String, ByVal iOperator As Integer, ByVal iAclPkey As Integer, ByVal iConditionPkey As Integer) As Integer
        If Int(iOperator) = Int((sConditionValue = sConditionType)) Then
            If iAclPkey = 0 Then
                Return SearchTree(iConditionPkey)
            Else
                Return iAclPkey
            End If
        End If
    End Function

    Function GetACLPermission(ByVal iAclPkey As Integer, ByVal sAction As String) As String

        Dim sSqlQueryACLPermission As String
        Dim bReturnValue As String

        Dim oDRowACLPermissions() As Data.DataRow
        oDRowACLPermissions = dbDatasetACLPermissions.Tables(0).Select("PKEY = " & iAclPkey)

        If oDRowACLPermissions.Length > 0 Then
            bReturnValue = oDRowACLPermissions(0).Item(sAction)
        End If


        Return bReturnValue

    End Function

    Sub InitObject()
        Dim sSqlQuery As String
        Dim dbDataSetObject As New Data.DataSet
        Dim dbDatasetCatalog As New Data.DataSet
        'temp connection - because open cursor exceeded
        Dim sSqlQueryNodes As String
        Dim sSqlQueryObject As String
        Dim i, j As Integer

        '''sSqlQueryNodes = "SELECT POBJECTTYPE FROM T_CAT_NODES WHERE PKEY='" & sPkeyObject & "'"
        '''oConn.Open_Dataset(sSqlQueryNodes, dbDataSetObject, "Nodes")

        'if he dont exist at nodes -> he is winobject old revision
        If sObjectClass = "" Then
            sSqlQueryNodes = "SELECT POBJECTTYPE FROM T_CAT_NODES WHERE PKEY='" & sPkeyObject & "'"
            oConn.Open_Dataset(sSqlQueryNodes, dbDataSetObject, "Nodes")
            If dbDataSetObject.Tables(0).Rows.Count > 0 Then
                sObjectClass = dbDataSetObject.Tables(0).Rows(0).Item(0)
            Else
                sObjectClass = oLWObjectTypes.emWinObject
            End If
            dbDataSetObject.Dispose()
            dbDataSetObject = Nothing
        End If

        dbDataSetObject = New Data.DataSet

        Select Case sObjectClass
            Case oLWObjectTypes.emCatalog
                sSqlQueryObject = "SELECT '' as ObjectType, '' as ObjectStatus, '' as OwningUser, '' as OwningGroup, pkey as PkeyCatalog,1 as PPUBLISH FROM " & oLWObjectTypes.emCatalogTable & " WHERE PKEY='" & sPkeyObject & "'"
            Case oLWObjectTypes.emPage
                If gemAppMode = "Access" Then
                    sSqlQueryObject = "SELECT PKEYTYPE as ObjectType, PSTATUS as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog ," & oLWObjectTypes.emPageTable & ".* FROM " & oLWObjectTypes.emPageTable & " LEFT JOIN " & oLWObjectTypes.emUsersTable & " ON " & oLWObjectTypes.emPageTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY WHERE " & oLWObjectTypes.emPageTable & ".PKEY='" & sPkeyObject & "'"
                Else
                    sSqlQueryObject = "SELECT PKEYTYPE as ObjectType, PSTATUS as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog as PkeyCatalog ," & oLWObjectTypes.emPageTable & ".* FROM " & oLWObjectTypes.emPageTable & ", " & oLWObjectTypes.emUsersTable & " WHERE " & oLWObjectTypes.emPageTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY (+) " & "AND " & oLWObjectTypes.emPageTable & ".PKEY='" & sPkeyObject & "'"
                End If
            Case oLWObjectTypes.emPart
                If gemAppMode = "Access" Then
                    sSqlQueryObject = "SELECT PKEYTYPE as ObjectType, PSTATUS as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog ," & oLWObjectTypes.emPartTable & ".* FROM " & oLWObjectTypes.emPartTable & " LEFT JOIN " & oLWObjectTypes.emUsersTable & " ON " & oLWObjectTypes.emPartTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkeyObject & "'"
                Else
                    sSqlQueryObject = "SELECT PKEYTYPE as ObjectType, PSTATUS as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog as PkeyCatalog ," & oLWObjectTypes.emPartTable & ".* FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emUsersTable & " WHERE " & oLWObjectTypes.emPartTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY (+) " & "AND " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkeyObject & "'"
                End If
            Case oLWObjectTypes.emWinObject
                If gemAppMode = "Access" Then
                    sSqlQueryObject = "SELECT PKEYTYPE as ObjectType, PSTATUS as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog , " & oLWObjectTypes.emWinObjectTable & ".*  FROM " & oLWObjectTypes.emWinObjectTable & " LEFT JOIN " & oLWObjectTypes.emUsersTable & " ON " & oLWObjectTypes.emWinObjectTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkeyObject & "'"
                Else
                    sSqlQueryObject = "SELECT PKEYTYPE as ObjectType, PSTATUS as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog as PkeyCatalog , " & oLWObjectTypes.emWinObjectTable & ".*  FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emUsersTable & " WHERE " & oLWObjectTypes.emWinObjectTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY (+) " & "AND " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkeyObject & "'"
                End If
            Case "6"
                If gemAppMode = "Access" Then
                    sSqlQueryObject = "SELECT PKEYTYPE as ObjectType, PSTATUS as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog , " & oLWObjectTypes.emWinObjectTable & ".*  FROM " & oLWObjectTypes.emWinObjectTable & " LEFT JOIN " & oLWObjectTypes.emUsersTable & " ON " & oLWObjectTypes.emWinObjectTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkeyObject & "'"
                Else
                    sSqlQueryObject = "SELECT '' as ObjectType, '' as ObjectStatus, PUSERNAME as OwningUser, '' as OwningGroup, PkeyCatalog as PkeyCatalog , " & oLWObjectTypes.emTextTable & ".*  FROM " & oLWObjectTypes.emTextTable & ", " & oLWObjectTypes.emUsersTable & " WHERE " & oLWObjectTypes.emTextTable & "." & "PKEYUSER=" & oLWObjectTypes.emUsersTable & ".PKEY (+) " & "AND " & oLWObjectTypes.emTextTable & ".PKEY='" & sPkeyObject & "'"
                End If
        End Select

        oConn.Open_Dataset(sSqlQueryObject, dbDataSetObject, "ObjectDetails")

        'for add catalog - there is not permission needed
        If dbDataSetObject.Tables(0).Rows.Count > 0 Then

            sObjectType = dbDataSetObject.Tables(0).Rows(0).Item(0) & ""
            sObjectStatus = dbDataSetObject.Tables(0).Rows(0).Item(1) & ""
            sObjectOwningUser = dbDataSetObject.Tables(0).Rows(0).Item(2) & ""
            sObjectOwningGroup = dbDataSetObject.Tables(0).Rows(0).Item(3) & ""
            sCatalogPkey = dbDataSetObject.Tables(0).Rows(0).Item(4) & ""
            If IsDBNull(dbDataSetObject.Tables(0).Rows(0).Item("PPUBLISH")) Then
                sPublish = 1
            Else
                sPublish = dbDataSetObject.Tables(0).Rows(0).Item("PPUBLISH")
            End If

            cPPROP = New Collection
            For i = 0 To dbDataSetObject.Tables(0).Columns.Count - 1
                cPPROP.Add(dbDataSetObject.Tables(0).Rows(0).Item(i) & "", UCase(dbDataSetObject.Tables(0).Columns(i).Caption))
            Next

        End If


        dbDataSetObject.Clear()
        dbDataSetObject.Dispose()
        dbDataSetObject = Nothing

        If sPkeyCatalogTemp = "" Then

            sSqlQuery = "select distinct pkey from " & oLWObjectTypes.emNodesTable & " where (pglobalstatus<>4 and pobjecttype=1) connect by prior " & oLWObjectTypes.emNodesTable & ".pparentkey=" & oLWObjectTypes.emNodesTable & ".pkey start with " & oLWObjectTypes.emNodesTable & ".pkey='" & Me.sPkeyObject & "'"

            oConn.Open_Dataset(sSqlQuery, dbDatasetCatalog)
            If dbDatasetCatalog.Tables(0).Rows.Count > 0 Then
                sPkeyCatalogTemp = ""
                For i = 0 To dbDatasetCatalog.Tables(0).Rows.Count - 1
                    sPkeyCatalogTemp = sPkeyCatalogTemp & dbDatasetCatalog.Tables(0).Rows(i).Item("PKEY") & "|"
                Next
            End If
			oConn.Close_Connection()
            dbDatasetCatalog.Dispose()
            dbDatasetCatalog = Nothing

        End If
		

    End Sub

    Public Function ShowMessage(ByVal strMessage As String, ByVal iMessageType As Integer)
        Return "<script>alert('" & strMessage & "')</script>"
    End Function

    Public Function getUserGroups() As String
        getUserGroups = sUserGroupTemp
    End Function

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            ' Free other state (managed objects).
            cPPROP = Nothing
            dbDatasetAcl.Dispose()
            dbDatasetAcl = Nothing
            oLWObjectTypes = Nothing
            'dbDatasetACLPermissions.Dispose()
            dbDatasetACLPermissions = Nothing
            'closing connection.
            If Not isConnectionShared Then
                oConn.Close_Connection()
                oConn = Nothing
            End If
        End If
        ' Free your own state (unmanaged objects).
        ' Set large fields to null.
    End Sub

    Protected Overrides Sub Finalize()
        ' Simply call Dispose(False).
        Dispose(False)
    End Sub

End Class

