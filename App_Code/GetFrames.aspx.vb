Imports System.IO
Public Class GetFrames
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        FrameParse()
    End Sub

    Private Function FrameParse()

        Dim filePath As String = Server.MapPath(Request.QueryString("Template"))
        Dim a As String = Request.QueryString("XSL")

        Dim sTemplateContent As String
        Dim sTemplateClean As String
        Dim oFsr As StreamReader
        oFsr = File.OpenText(filePath)
        sTemplateContent = oFsr.ReadToEnd()
        oFsr.Close()
        oFsr = Nothing
        sTemplateClean = RemoveNotes(sTemplateContent)
        If (a = "True") Then
            ParseXSL(sTemplateClean)
        Else
            ParseInner(sTemplateClean)
        End If
        sTemplateContent = ""
        sTemplateClean = ""

    End Function
    Public Function RemoveNotes(ByVal content As String) As String

        Dim output As String = ""
        Dim i As Integer = 0
        Dim pos As Integer = 0
        Dim temp As String
        output = content

        pos = output.IndexOf("<!--")

        While (Not pos = -1)
            Dim endpos As Integer = output.IndexOf("-->", pos)
            If (output.Substring(endpos - 2, 2) = "//") Then
                pos = output.IndexOf("<!--", endpos)
            Else
                temp = output.Remove(pos, endpos - pos + 3)
                output = temp
                pos = output.IndexOf("<!--", pos)
            End If

        End While

        RemoveNotes = output

    End Function

    Private Function ParseXSL(ByVal content As String)
        Dim output As String = ""
        Dim i As Integer = 0
        Dim pos As Integer = 0
        Dim temp As String = ""

        pos = content.IndexOf("?xml-stylesheet type")
        i = content.IndexOf("href=", pos)
        temp = content.Substring(i + 6, content.IndexOf("?>", i) - (i + 7))
        'output = temp.Replace("""", "")
        Response.Write(temp)

    End Function


    Public Function ParseInner(ByVal content As String, Optional ByVal bResponse As Boolean = True) As String

        Dim output As String = ""
        Dim i As Integer = 0
        Dim pos As Integer = 0
        Dim counter As Integer = 0


        pos = content.IndexOf("<frame name=")

        While (Not pos = -1)

            Dim fname As String
            Dim fcont As String
            Dim posstart As Integer = content.IndexOf("'", pos + 12)
            If (posstart = -1) Then
                posstart = content.IndexOf("""", pos + 12)
            End If
            Dim posend As Integer = content.IndexOf("'", posstart + 1)
            If (posend = -1) Then
                posend = content.IndexOf("""", posstart + 1)
            End If
            If (posstart = -1 Or posend = -1) Then
                output = "empty"
                Exit While
            Else
                fname = content.Substring(posstart + 1, posend - (posstart + 1))
                If (fname.ToUpper = "INDEX" Or fname.ToUpper = " INDEX ") Then
                    pos = content.IndexOf("<frame name=", pos + 12)
                Else
                    Dim posSstart As Integer = content.IndexOf("FRAME SOURCE_FILE=", posend)
                    Dim posSend As Integer = content.IndexOf(".", posend) + 5
                    fcont = content.Substring(posSstart + 18, posSend - (posSstart + 18))
                    If (Not fcont.Substring(fcont.Length - 1, 1).ToUpper = "L") Then
                        Dim temp As String = fcont.Remove(fcont.Length - 1, 1)
                        fcont = temp
                    End If
                    output = output + "|" + "(" + fname + ")" + fcont
                    pos = content.IndexOf("<frame name=", pos + 12)
                    counter = counter + 1
                End If

            End If
        End While

        If Not bResponse Then
            ParseInner = output
        Else
            Response.Write(output)
        End If
    End Function

End Class
