Public Class CPKeyChildsManage

    Private sODBCNAME As String
    Private oConn As CConnection

    Public Sub New(ByVal sODBCName As String)
        Me.sODBCNAME = sODBCName
    End Sub

    Function Add_to_PkeyChilds(ByVal sPkey As String, ByVal sPkeyChilds As String) As String
        'sPkey - the pkey to add
        'sPkeyChilds - the current pkeychilds

        ' check if pkey not already in pkeychilds
        If InStr(sPkeyChilds, sPkey, CompareMethod.Text) <> 0 Then
            ' pkey is already in pkeychilds so delete it and put it at the end
            'sPkeyChilds = Replace(sPkeyChilds, sPkey & "|", "", 1)
            Add_to_PkeyChilds = sPkeyChilds
        Else
            Add_to_PkeyChilds = sPkeyChilds & sPkey & "|"
        End If

    End Function

    Sub UpdatePkeyChilds(ByVal sPkey As String, ByVal sType As Integer)
        'find the current pkeychilds
        Dim sCurPkeyChilds As String
        Dim sNewPkeyChilds As String
        Dim sArrayList As New ArrayList
        Dim sItem As Object
        Dim sParent As String
        Dim sArray(1) As String

        If sType = 1 Then
            'puts the curent pkeychilds of the parent of the pkey in a collection
            sArrayList = Find_Current_PKeyChilds(sPkey)
            'for each parent, 
            If sArrayList.Count > 0 Then
                For Each sItem In sArrayList
                    sCurPkeyChilds = sItem(1)
                    sParent = sItem(0)
                    sNewPkeyChilds = Add_to_PkeyChilds(sPkey, sCurPkeyChilds)
                    UpdatePkeyChildsInDB(sParent, sNewPkeyChilds)
                Next
            End If
        End If
    End Sub

    Function Find_Current_PKeyChilds(ByVal sPKey As String) As ArrayList
        Dim sSqlQuery As String
        Dim oDbDataSet As Data.DataSet
        Dim i As Integer
        Dim sArrayList As New ArrayList

        Dim sItem As Object

        Dim sCurPkeyChilds As String
        Dim sNewPkeyChilds As String
        Dim sParent As String
        Dim sCurPkey As String

        'find the current pkeychilds
        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCNAME)

        'bring all parts
        sSqlQuery = "select t_cat_part.pkeychilds, t_cat_nodes.pparentkey from t_cat_part,t_cat_nodes where t_cat_part.pkey =  t_cat_nodes.pparentkey and t_cat_nodes.PKEY = '" & sPKey & "'"

        oDbDataSet = New Data.DataSet
        oConn.Open_Dataset(sSqlQuery, oDbDataSet)
        If oDbDataSet.Tables(0).Rows.Count > 0 Then
            For i = 0 To oDbDataSet.Tables(0).Rows.Count - 1
                Dim sArray(1) As String
                'ReDim Preserve sArray(1, i)
                'first is the parent kry
                sArray(0) = New String(oDbDataSet.Tables(0).Rows(i).Item("pparentkey"))
                'second is the pkychilds
                If Not IsDBNull(oDbDataSet.Tables(0).Rows(i).Item("pkeychilds")) Then
                    sArray(1) = New String(oDbDataSet.Tables(0).Rows(i).Item("pkeychilds"))
                Else
                    sArray(1) = New String("")
                End If
                sArrayList.Add(sArray)
            Next
        End If

        oDbDataSet = Nothing

        'check for each parent its childs in t_cat_nodes
        If sArrayList.Count > 0 Then
            For Each sItem In sArrayList

                sCurPkeyChilds = sItem(1)
                sParent = sItem(0)
                sSqlQuery = "select * from t_cat_nodes where pglobalstatus<>4 and t_cat_nodes.pparentkey = '" & sParent & "'"

                oDbDataSet = New Data.DataSet
                oConn.Open_Dataset(sSqlQuery, oDbDataSet)
                If oDbDataSet.Tables(0).Rows.Count > 0 Then
                    For i = 0 To oDbDataSet.Tables(0).Rows.Count - 1
                        sCurPkey = oDbDataSet.Tables(0).Rows(i).Item("pkey")
                        If sPKey <> sCurPkey Then
                            'if current pkey is not the new one then
                            If InStr(sCurPkeyChilds, sCurPkey & "|", CompareMethod.Text) = 0 Then
                                'if current pkey is not in childs pkey then
                                sCurPkeyChilds = Add_to_PkeyChilds(sCurPkey, sCurPkeyChilds)
                                sItem(1) = sCurPkeyChilds
                            End If
                        End If
                    Next
                End If
            Next
        End If

        oConn.Close_Connection()
        oConn = Nothing
        oDbDataSet = Nothing

        Find_Current_PKeyChilds = sArrayList
    End Function

    Sub UpdatePkeyChildsInDB(ByVal sPKey As String, ByVal sPkeyChilds As String)

        Dim sSqlQuery As String
        Dim oDbDataSet As Data.DataSet
        Dim i As Integer
        Dim strError As String

        ' check if pkey not already in pkeychilds

        'find the current pkeychilds
        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCNAME)

        'bring all parts
        sSqlQuery = "update t_cat_part set pkeychilds='" & sPkeyChilds & "' where t_cat_part.pkey = '" & sPKey & "'"

        oDbDataSet = New Data.DataSet
        Try
            oConn.Open_Dataset(sSqlQuery, oDbDataSet)
            'oConn.Execute_Statement(sSqlQuery, "Oracle",ODBCName)    oDbDataSet)
        Catch ex As Exception
            strError = ex.Message()
        End Try


        oConn.Close_Connection()
        oConn = Nothing
        oDbDataSet = Nothing


    End Sub
End Class
