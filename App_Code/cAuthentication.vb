Public Class cAuthentication
    Implements IDisposable

    Private sLogonUser As String
    Private sFullLogonUser As String
    Private sPermissions() As String
    Private oConn As CConnection
    Private isConnectionShared As Boolean = False

    Public Sub New(ByVal sLogonUserVariable As String, ByVal sODBCName As String, ByVal gemAppMode As String, Optional ByRef oConn As CConnection = Nothing)
        REM Dim iDomainDelimiter As Integer
        REM Dim dbDataSetPermissions As Data.DataSet
        REM Dim sSqlQueryPermissions As String
        REM Dim oLWObjectTypes As New LWObjectTypes
        REM Dim pIsAdmin As Integer

        REM If Not oConn Is Nothing Then
            REM isConnectionShared = True
            REM Me.oConn = oConn
        REM Else
            REM 'Open oracle connection
            REM Me.oConn = New CConnection
            REM If gemAppMode = "Access" Then
                REM Me.oConn.Open_Access_Connection(sODBCName)
            REM Else
                REM Me.oConn.Open_Oracle_Connection(sODBCName)
            REM End If
        REM End If

        REM sFullLogonUser = sLogonUserVariable

        REM iDomainDelimiter = InStr(1, sLogonUserVariable, "\")
        REM If iDomainDelimiter > 1 Then
            REM sLogonUser = Mid(sLogonUserVariable, iDomainDelimiter + 1)
        REM Else
            REM sLogonUser = sLogonUserVariable
        REM End If

        REM 'get permissions from t_cat_users
        REM dbDataSetPermissions = New Data.DataSet
        REM sSqlQueryPermissions = "SELECT PISADMIN FROM " & oLWObjectTypes.emUsersTable & " WHERE PUSERNAME='" & sLogonUser & "' OR PUSERNAME='" & sFullLogonUser & "'"
        REM Me.oConn.Open_Dataset(sSqlQueryPermissions, dbDataSetPermissions)

        REM If dbDataSetPermissions.Tables(0).Rows.Count > 0 Then
            REM pIsAdmin = dbDataSetPermissions.Tables(0).Rows(0).Item(0)
        REM Else
            REM Dim strSQL As String
            REM strSQL = "INSERT INTO T_CAT_USERS(PKEY, PUSERNAME, PUSERPASSWORD, PUSERDESCRIPTION, PUSERGENDER, PUSERSTATUS, PUSERFIRSTNAME, " _
            REM & "PUSERLASTNAME, PUSERPHONE, PUSERMOBILE, PUSEREXPDATE, PUSERCOMPANY, PUSEREMAIL, PUSERPROFSSION, PUSERCOMMENTS) "
            REM strSQL = strSQL & " VALUES("
            REM strSQL = strSQL & "'" & sFullLogonUser & "' "
            REM strSQL = strSQL & ",'" & sFullLogonUser & "' "
            REM strSQL = strSQL & ",'" & sFullLogonUser & "' "
            REM strSQL = strSQL & ",'New user from AD' "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",0 "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",TO_DATE('01/01/2070','DD/MM/YYYY') "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",'' "
            REM strSQL = strSQL & ",'') "
            REM If sFullLogonUser <> "" Then
                REM Me.oConn.Open_Dataset(strSQL, dbDataSetPermissions)
            REM End If
            REM pIsAdmin = 0
        REM End If

        'If pIsAdmin = 0 Then
            ReDim sPermissions(0)
            sPermissions(0) = "R"
        REM Else
            REM ReDim sPermissions(2)
            REM sPermissions(0) = "A"
            REM sPermissions(1) = "U"
            REM sPermissions(2) = "D"
        REM End If

        REM dbDataSetPermissions = Nothing
        REM oLWObjectTypes = Nothing
	REM Me.oConn.Close_Connection()
    End Sub

    Public Property LogonUser()
        Get
            LogonUser = sLogonUser
        End Get
        Set(ByVal Value)
            sLogonUser = Value
        End Set
    End Property

    Public Property FullLogonUser()
        Get
            FullLogonUser = sFullLogonUser
        End Get
        Set(ByVal Value)
            sFullLogonUser = Value
        End Set
    End Property

    Public ReadOnly Property Permissions()
        Get
            Permissions = sPermissions
        End Get
    End Property

    Public Function IsAdmin()
        IsAdmin = sPermissions.Length = 3
    End Function

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            ' Free other state (managed objects).
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
