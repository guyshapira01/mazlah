Imports System.IO

Public Class CFileOps

    'Dim sODBCName As String = ""
    'Dim oConn As CConnection

    Public Sub New()

    End Sub

    Public Function MoveFile(ByVal src As String, ByVal dest As String) As Boolean
        Try
            File.Move(src, dest)
        Catch ex As Exception
            MoveFile = False
            Exit Function
        End Try
        MoveFile = True
    End Function

    Public Function CopyFile(ByVal src As String, ByVal dest As String) As Boolean
        Try
            File.Copy(src, dest)
        Catch ex As Exception
            CopyFile = False
            Exit Function
        End Try
        CopyFile = True
    End Function
    Public Function CopyDir(ByVal src As String, ByVal dest As String)

        Dim i As Integer
        Dim di As New DirectoryInfo(src)
        Dim dif As DirectoryInfo
        Dim fif As FileInfo
        For Each fif In di.GetFiles()
            File.Copy(src & "\" & fif.Name, dest & "\" & fif.Name, True)
        Next
        For Each dif In di.GetDirectories()
            Directory.CreateDirectory(dest & "\" & dif.Name)
            CopyDir(dif.FullName, dest & "\" & dif.Name)
        Next

    End Function
    Public Function DelFile(ByVal src As String) As Boolean
        Try
            File.Delete(src)
        Catch ex As Exception
            DelFile = False
            Exit Function
        End Try
        DelFile = True
    End Function

    Public Function FileExists(ByVal src As String) As Boolean
        Try
            File.Exists(src)
        Catch ex As Exception
            FileExists = False
            Exit Function
        End Try
        FileExists = True
    End Function

    Public Function PathCheck(ByVal path As String) As Boolean
        If Directory.Exists(path) Then
            PathCheck = True
            Exit Function
        Else
            Directory.CreateDirectory(path)
            PathCheck = True
            Exit Function
        End If
    End Function

End Class
