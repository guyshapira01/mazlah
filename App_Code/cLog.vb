Imports System.IO

Public Class cLog

    Public Sub New(ByVal sFullPath As String, ByVal sWriteTxt As String)
        Dim oFile As File
        Dim objStreamWriter As StreamWriter

        objStreamWriter = oFile.AppendText(sFullPath)
        'System.AppDomain.CurrentDomain.BaseDirectory() & "ApplicationStart.txt"
        objStreamWriter.Write(sWriteTxt & vbCrLf)
        objStreamWriter.Close()

    End Sub

End Class
