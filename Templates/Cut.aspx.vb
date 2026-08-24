Public Class Cut
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
        Dim sCutObjects As String = Request.QueryString("Objects")
        Dim sFatherObj As String
        Dim sPkeyCatalog As String = Request.QueryString("PkeyCatalog")

        If Request.QueryString("Self").ToLower() = "true" Then
            sFatherObj = Request.QueryString("Parent") & ""
        Else
            sFatherObj = Request.QueryString("Pkey") & ""
        End If

        Session.Add("Action", "Cut")
        Session.Add("Objects", sCutObjects)
        Session.Add("Father", sFatherObj)
        Session.Add("CutCopyCatalog", sPkeyCatalog)
        Session.Add("CutCopySelf", Request.QueryString("Self").ToLower())

        Response.Write("<script language=""JavaScript"">window.close();</script>")

    End Sub

End Class
