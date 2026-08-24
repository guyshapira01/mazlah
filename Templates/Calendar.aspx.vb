Public Class CalendarDate
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
        Session.LCID = 2057 'British LCID
        If HttpContext.Current.Request.QueryString("cur") <> "" Then
            If IsDate(HttpContext.Current.Request.QueryString("cur")) Then
                Calendar1.TodaysDate = HttpContext.Current.Request.QueryString("cur")
            Else
                Calendar1.TodaysDate = Now()
            End If
        End If
    End Sub

    Private Sub Calendar1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        Dim strjscript As String = "<script language=""javascript"">"
        strjscript = strjscript & "window.opener." & _
                    HttpContext.Current.Request.QueryString("formname") & ".value = '" & _
                    Calendar1.SelectedDate & "';window.close();"
        strjscript = strjscript & "</script" & ">" 'Don't Ask, Tool Bug

        'change the lcid to hebrew again.
        Session.LCID = 1037

        Literal1.Text = strjscript
    End Sub
End Class
