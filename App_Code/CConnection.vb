Imports Microsoft.VisualBasic
'Imports System.Data.OracleClient
Imports System.Data
Imports System.IO

Public Class CConnection
    
     'Private LW_SqlConnection As OracleClient.OracleConnection
    'Private LW_SqlTransaction As OracleClient.OracleTransaction
    'Private LW_ODBCDataAdapter As OracleClient.OracleDataAdapter
    'Private LW_Command As OracleClient.OracleCommand

     Private LW_SqlConnection As Data.OleDb.OleDbConnection
    'Private LW_SqlTransaction As Data.OleDb.
    Private LW_ODBCDataAdapter As Data.OleDb.OleDbDataAdapter
    Private LW_Command As Data.OleDb.OleDbCommand
    Public Property SqlConnection As Data.OleDb.OleDbConnection
        Get
            Return LW_SqlConnection
        End Get
        Set(value As Data.OleDb.OleDbConnection)
            LW_SqlConnection = value
        End Set
    End Property

    Public Sub Open_Connection(ByVal gemAppMode As String, ByVal ODBCName As String, ByRef strError As String)
        '===============================================================================
        'Check Connection Type - Supporting Access/Oracle/SqlServer
        '===============================================================================
        Select Case gemAppMode
            Case Is = "Oracle"
                Open_Oracle_Connection(ODBCName, strError)    'Open connection Oracle
            Case Is = "Access"
                Open_Access_Connection(ODBCName)    'Open connection Access
            Case Is = "SqlSever"
                Open_SqlServer_Connection(ODBCName, strError) 'Open connection Sql Server
        End Select
    End Sub

    Public Sub Open_Connection(ByVal gemAppMode As String, ByVal ODBCName As String)
       	dim strError as string
	Open_Connection(gemAppMode ,ODBCName ,strError)
    End Sub
    Public Sub Begin_Transaction(ByVal conn As Data.OracleClient.OracleConnection)


       

    End Sub
    Public Sub Commit_transaction(ByVal con As Data.OracleClient.OracleConnection, ByRef strError As String)

       
    End Sub



    Public Sub Open_Access_Connection(ByVal pszFileName As String)
        'check if we use ODBC or full connection string
        'Connection string
        Dim strCon As String

        If InStr(LCase(pszFileName), "driver") Or InStr(LCase(pszFileName), "provider") Then
            strCon = pszFileName
        Else
            strCon = "DSN=" & pszFileName
        End If

        'Access connection
        LW_SqlConnection = New Data.OleDb.OleDbConnection(strCon)
        LW_SqlConnection.Open()


    End Sub

    Public Sub Open_Oracle_Connection(ByVal pszFileName As String, ByRef strError As String)
       
    End Sub

    Public Sub Open_Oracle_Connection(ByVal pszFileName As String)
        
    End Sub

    Public Sub Open_SqlServer_Connection(ByVal pszFileName As String, ByRef strError As String)
        

    End Sub
	
	public sub WriteLog(byval pszMSG as string)
		Using sw as StreamWriter=File.AppendText("c:\temp\raz_log.txt")
			sw.WriteLine(pszMSG)
		end using
	end sub

    Public Function Open_Dataset(ByVal pszSQLString As String, ByVal LW_DataSet As Data.DataSet)
        'Step 1 DataAdapter
        Dim strError As String
        Try
            'LW_Command.CommandText = pszSQLString
            'LW_Command.ExecuteNonQuery()
            LW_ODBCDataAdapter = New Data.OleDb.OleDbDataAdapter(pszSQLString, LW_SqlConnection)
        Catch ex As Exception
            strError = ex.Message
        End Try

        'Step Insert To the data to the DataSet
        Try
            LW_ODBCDataAdapter.Fill(LW_DataSet, "Catalogs")
			'WriteLog(pszSQLString)
        Catch ex As Exception
            strError = ex.Message
        End Try
		Open_Dataset = strError

        LW_ODBCDataAdapter = Nothing

    End Function

    Public Function Open_Dataset(ByVal pszSQLString As String, ByVal LW_DataSet As Data.DataSet, ByVal srcTable As String)
        'Step 1 DataAdapter
        LW_ODBCDataAdapter = New Data.OleDb.OleDbDataAdapter(pszSQLString, LW_SqlConnection)

        'Step Insert To the data to the DataSet
        Try
            LW_ODBCDataAdapter.Fill(LW_DataSet, srcTable)
			'WriteLog(pszSQLString)
        Catch ex As Exception

        End Try


        LW_ODBCDataAdapter = Nothing

    End Function

    Public Function Close_Connection()
        Try
            LW_SqlConnection.Close()
        Catch ex As Exception

        End Try

    End Function
    Public Function Open_Command(ByVal pszSQLString As String, ByRef dbReader As Data.OleDb.OleDbDataReader)
        'Step 1 Command

        LW_Command = New Data.OleDb.OleDbCommand(pszSQLString, LW_SqlConnection)
        'reader
        Try
            dbReader = LW_Command.ExecuteReader()
        Catch ex As Exception

        End Try


    End Function

    Public Function Open_Command(ByVal pszSQLString As String)
        'Step 1 Command
        Try
            LW_Command = New Data.OleDb.OleDbCommand(pszSQLString, LW_SqlConnection)
        Catch e As Exception
            Console.WriteLine("error was encountered description:" + e.ToString)
        End Try

    End Function

    Public Function Open_Command_Param(ByVal pszSQLString As String, ByVal aSqlParamCol() As Data.OleDb.OleDbParameter) As Integer
        Dim iRet As Integer = -1
        Try
            LW_Command = New Data.OleDb.OleDbCommand(pszSQLString, LW_SqlConnection)
            Dim i As Integer
            For i = 0 To aSqlParamCol.Length - 1
                LW_Command.Parameters.Add(aSqlParamCol(i))
            Next
            iRet = LW_Command.ExecuteNonQuery()
            Open_Command_Param = iRet
        Catch e As Exception
            Console.WriteLine("error was encountered description:" + e.ToString)
            Open_Command_Param = iRet
        End Try

    End Function

    Public Function Execute_Statement(ByVal pszSQLString As String, ByVal gemAppMode As String, ByVal ODBCName As String, byref strError as string)
       Dim retInt As Integer	
       retInt =Execute_Statement(pszSQLString , gemAppMode , ODBCName )
       Execute_Statement=retInt
    end function

    Public Function Execute_Statement(ByVal pszSQLString As String, ByVal gemAppMode As String, ByVal ODBCName As String)
        Dim retInt As Integer
        Open_Connection(gemAppMode, ODBCName)
        '' pszSQLString = "UPDATE T_CAT_WINOBJECT SET PHEBDESC1='111' WHERE PKEY='TSGa4910'"
        Open_Command(pszSQLString)
        'Begin_Transaction(LW_SqlConnection)
        Try
            retInt = LW_Command.ExecuteNonQuery()
        Catch ex As Exception

        End Try

        'Commit_transaction(LW_SqlConnection)
    End Function

    Public Sub Execute_ScalarStatement(ByVal pszSQLString As String, ByVal gemAppMode As String, ByVal ODBCName As String, ByRef strError As String, ByRef nCount As Integer)
        'Dim retInt As Integer
        Open_Connection(gemAppMode, ODBCName, strError)
        
        Open_Command(pszSQLString)
        
        Try
            nCount = LW_Command.ExecuteScalar
        Catch ex As Exception
            strError = ex.Message
        End Try

        
    End Sub
End Class
