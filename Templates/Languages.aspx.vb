Imports System.Data

Public Class Languages
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
        If Page.IsPostBack Then

        Else
            Dim oPrefs As New PersonalPref(Application("ODBCNAME"))
            Dim dbPrefset As New Data.DataSet
            Dim sLang As String
            dbPrefset = oPrefs.GetPrefs("GROUP", "Everyone", "default", "", "", "", "", "", "PTEXTLANG", "PPREFS")
            If dbPrefset.Tables(0).Rows.Count > 0 Then
                sLang = dbPrefset.Tables(0).Rows(0)("PREFVALUE") & ""
            End If
            If sLang = "English" Then
                imgEnglish.BorderColor = Drawing.Color.Yellow
                imgEnglish.BorderStyle = BorderStyle.Solid
                imgGerman.BorderColor = Drawing.Color.Transparent
                imgGerman.BorderStyle = BorderStyle.None
                imgGlobal.BorderColor = Drawing.Color.Transparent
                imgGlobal.BorderStyle = BorderStyle.None
            ElseIf sLang = "German" Then
                imgEnglish.BorderColor = Drawing.Color.Transparent
                imgEnglish.BorderStyle = BorderStyle.None
                imgGlobal.BorderColor = Drawing.Color.Transparent
                imgGlobal.BorderStyle = BorderStyle.None
                imgGerman.BorderColor = Drawing.Color.Yellow
                imgGerman.BorderStyle = BorderStyle.Solid
            Else
                imgEnglish.BorderColor = Drawing.Color.Transparent
                imgEnglish.BorderStyle = BorderStyle.None
                imgGerman.BorderColor = Drawing.Color.Transparent
                imgGerman.BorderStyle = BorderStyle.None
                imgGlobal.BorderColor = Drawing.Color.Yellow
                imgGlobal.BorderStyle = BorderStyle.Solid
            End If

            oPrefs = Nothing

        End If
    End Sub

    Private Sub imgEnglish_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEnglish.Click
        Dim oPrefs As New PersonalPref(Application("ODBCNAME"))
        Dim dbPrefset As New DataSet

        dbPrefset = oPrefs.SetPrefs("GROUP", "Everyone", "default", "", "", "", "", "", "PTEXTLANG", "English", "PPREFS")
        imgEnglish.BorderColor = Drawing.Color.Yellow
        imgEnglish.BorderStyle = BorderStyle.Solid
        imgGerman.BorderColor = Drawing.Color.Transparent
        imgGerman.BorderStyle = BorderStyle.None
        imgGlobal.BorderColor = Drawing.Color.Transparent
        imgGlobal.BorderStyle = BorderStyle.None

        oPrefs = Nothing

        Dim scrt As String = "<script language=JavaScript>window.opener.document.location.href = window.opener.document.location.href;window.close();</script>"
        Response.Flush()
        Response.Write(scrt)

    End Sub

    Private Sub imgGerman_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGerman.Click
        Dim oPrefs As New PersonalPref(Application("ODBCNAME"))
        Dim dbPrefset As New DataSet

        dbPrefset = oPrefs.SetPrefs("GROUP", "Everyone", "default", "", "", "", "", "", "PTEXTLANG", "German", "PPREFS")
        imgEnglish.BorderColor = Drawing.Color.Transparent
        imgEnglish.BorderStyle = BorderStyle.None
        imgGlobal.BorderColor = Drawing.Color.Transparent
        imgGlobal.BorderStyle = BorderStyle.None
        imgGerman.BorderColor = Drawing.Color.Yellow
        imgGerman.BorderStyle = BorderStyle.Solid

        oPrefs = Nothing

        Dim scrt As String = "<script language=JavaScript>window.opener.document.location.href = window.opener.document.location.href;window.close();</script>"
        Response.Flush()
        Response.Write(scrt)

    End Sub

    Private Sub imgGlobal_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGlobal.Click

        Dim oPrefs As New PersonalPref(Application("ODBCNAME"))
        Dim dbPrefset As New Data.DataSet

        dbPrefset = oPrefs.SetPrefs("GROUP", "Everyone", "default", "", "", "", "", "", "PTEXTLANG", "Global", "PPREFS")
        imgEnglish.BorderColor = Drawing.Color.Transparent
        imgEnglish.BorderStyle = BorderStyle.None
        imgGerman.BorderColor = Drawing.Color.Transparent
        imgGerman.BorderStyle = BorderStyle.None
        imgGlobal.BorderColor = Drawing.Color.Yellow
        imgGlobal.BorderStyle = BorderStyle.Solid

        oPrefs = Nothing

        Dim scrt As String = "<script language=JavaScript>window.opener.document.location.href = window.opener.document.location.href;window.close();</script>"
        Response.Flush()
        Response.Write(scrt)

    End Sub
End Class
