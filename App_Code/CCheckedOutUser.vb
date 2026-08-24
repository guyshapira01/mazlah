Public Class CCheckedOutUser

    Private sCheckOutUser As String
    Private sField As String
    Private sTable As String

    Public Sub New(ByVal sObject As String, ByVal sODBCName As String, ByVal gemAppMode As String)

        Dim oConn As New CConnection
        Dim dbDataSet As New Data.DataSet
        Dim oLwObjectTypes As New LWObjectTypes
        Dim sSqlQuery As String

        'check if the object is not check out by another user.
        If gemAppMode = "Access" Then
            oConn.Open_Access_Connection(sODBCName)
        Else
            oConn.Open_Oracle_Connection(sODBCName)
        End If

        If gemAppMode = "Access" Then
            sSqlQuery = "SELECT T_CAT_NODES.PKEY, T_CAT_PART.PCHECKOUTUSER AS PartOwner, T_CAT_WINOBJECT.PCHECKOUTUSER AS WinObjectOwner, T_CAT_PAGE.PKEYUSER AS PageOwner, T_CAT_FOLDER.PKEYUSER AS FolderOwner, T_CAT_CATALOG.PKEYUSER AS CatalogOwner, T_CAT_NODES.POBJECTTYPE " _
                      & "FROM T_CAT_FOLDER RIGHT JOIN (T_CAT_PAGE RIGHT JOIN (T_CAT_CATALOG RIGHT JOIN (T_CAT_WINOBJECT RIGHT JOIN (T_CAT_PART RIGHT JOIN T_CAT_NODES ON T_CAT_PART.PKEY = T_CAT_NODES.PKEY) ON T_CAT_WINOBJECT.PKEY = T_CAT_NODES.PKEY) ON T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY) ON T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY) ON T_CAT_FOLDER.PKEY=T_CAT_NODES.PKEY " _
                      & "WHERE T_CAT_NODES.PKEY='" & sObject & "'"
        Else
            sSqlQuery = "SELECT T_CAT_NODES.PKEY, T_CAT_TEXT.PKEYUSER AS TextOwner, T_CAT_PART.PCHECKOUTUSER AS PartOwner, T_CAT_WINOBJECT.PCHECKOUTUSER AS WinObjectOwner, T_CAT_PAGE.PKEYUSER AS PageOwner, T_CAT_FOLDER.PKEYUSER AS FolderOwner, T_CAT_CATALOG.PKEYUSER AS CatalogOwner, T_CAT_NODES.POBJECTTYPE " _
                      & "FROM T_CAT_FOLDER, T_CAT_TEXT, T_CAT_PAGE, T_CAT_CATALOG, T_CAT_WINOBJECT, T_CAT_PART, T_CAT_NODES WHERE T_CAT_TEXT.PKEY(+)=T_CAT_NODES.PKEY AND T_CAT_PART.PKEY(+)=T_CAT_NODES.PKEY AND T_CAT_WINOBJECT.PKEY(+)=T_CAT_NODES.PKEY AND T_CAT_CATALOG.PKEY(+)=T_CAT_NODES.PKEY AND T_CAT_PAGE.PKEY(+)=T_CAT_NODES.PKEY AND T_CAT_FOLDER.PKEY(+)=T_CAT_NODES.PKEY " _
                      & "AND T_CAT_NODES.PKEY='" & sObject & "'"
        End If

        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        If dbDataSet.Tables(0).Rows.Count > 0 Then
            Select Case dbDataSet.Tables(0).Rows(0).Item("POBJECTTYPE")
                Case oLwObjectTypes.emCatalog
                    sCheckOutUser = "CatalogOwner"
                    sField = "PKEYUSER"
                    sTable = oLwObjectTypes.emCatalogTable
                Case oLwObjectTypes.emPage
                    sCheckOutUser = "PageOwner"
                    sField = "PKEYUSER"
                    sTable = oLwObjectTypes.emPageTable
                Case oLwObjectTypes.emPart
                    sCheckOutUser = "PartOwner"
                    sField = "PCHECKOUTUSER"
                    sTable = oLwObjectTypes.emPartTable
                Case oLwObjectTypes.emWinObject
                    sCheckOutUser = "WinObjectOwner"
                    sField = "PCHECKOUTUSER"
                    sTable = oLwObjectTypes.emWinObjectTable
                Case oLwObjectTypes.emFolder
                    sCheckOutUser = "FolderOwner"
                    sField = "PKEYUSER"
                    sTable = oLwObjectTypes.emFolderTable
                Case "6" 'text obj
                    sCheckOutUser = "TextOwner"
                    sField = "PKEYUSER"
                    sTable = oLwObjectTypes.emTextTable
            End Select
            sCheckOutUser = dbDataSet.Tables(0).Rows(0).Item(sCheckOutUser) & ""
        End If


        oConn.Close_Connection()
        dbDataSet.Clear()
        oLwObjectTypes = Nothing
        dbDataSet = Nothing
        oConn = Nothing

    End Sub

    'gets the checkout user
    Public Property CheckOutUser()
        Get
            CheckOutUser = sCheckOutUser
        End Get
        Set(ByVal Value)
            sCheckOutUser = Value
        End Set
    End Property

    'gets the table name of the current check out user
    Public ReadOnly Property TableName()
        Get
            TableName = sTable
        End Get
    End Property

    'gets the field name of the current check out user
    Public ReadOnly Property FieldName()
        Get
            FieldName = sField
        End Get
    End Property

    'gets if the login user is the check out user of the object
    Public Function bIsCurrentUser(ByVal sLogonUser As String) As Boolean
        If sLogonUser = sCheckOutUser Then
            bIsCurrentUser = True
        Else
            bIsCurrentUser = False
        End If
    End Function
End Class
