Imports System.Xml
Imports System.Xml.Xsl

Public Class CXmlConfig
    Implements IDisposable

    '''   <assembly>
    '''	<name>WebEditor</name>
    '''	<version>1.0.1464.18135</version>
    '''	<fullname>WebEditor, Version=1.0.1464.18135, Culture=neutral, PublicKeyToken=null</fullname>
    '''</assembly>
    '''<members>
    '''	<member name="T:WebEditor.CConnection">
    '''		<summary>asdfsdg</summary>
    '''	</member>
    '''	<member name="M:WebEditor.CXmlConfig.GetParameter(System.String)">
    '''		<summary>Test</summary>
    '''	</member>
    '''</members>

    Private document As New XmlDocument

    Public Sub New(ByVal filePath As String)
        MyBase.new()
        document.Load(filePath)
    End Sub
    Public Function GetParameter(ByVal param As String) As String

        Dim returnValue As String

        If document.DocumentElement.SelectSingleNode(param).ChildNodes.Count = 1 Then
            returnValue = document.DocumentElement.SelectSingleNode(param).FirstChild.Value
        Else
            returnValue = ""
        End If

        GetParameter = returnValue

    End Function

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            ' Free other state (managed objects).
            document = Nothing
        End If
        ' Free your own state (unmanaged objects).
        ' Set large fields to null.
    End Sub

    Protected Overrides Sub Finalize()
        ' Simply call Dispose(False).
        Dispose(False)
    End Sub

End Class
