Public Class Copy
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

        If Not Request.QueryString("RetVal") Is Nothing Then
            Dim sVal As String = Request.QueryString("RetVal")
            Dim sRetVal As String = Session(sVal) & ""
            Response.Write(sRetVal)
            Response.End()
            Exit Sub
        End If

        Dim sCopyObjects As String
        Dim sParentKey As String = Request.QueryString("Pkey") & ""
        Dim sPkeyCatalog As String
        Dim sFromReport As String = "false"

        sCopyObjects = Request.QueryString("Objects")
        sPkeyCatalog = Request.QueryString("PkeyCatalog")

        If Request.QueryString("Self").ToLower() = "savesession" Then
            Session.Add("SavedObjects", sCopyObjects)
            Session.Add("SavedObjectsParent", sParentKey)
            Response.Write("<script language=""JavaScript"">window.close();</script>")
            Exit Sub
        End If

        If Request.QueryString("Self").ToLower() = "true" Then
            sParentKey = Request.QueryString("Parent") & ""
        Else
            sParentKey = Request.QueryString("Pkey") & ""
        End If

        sFromReport = Request.QueryString("FromReport") & ""

        'saves the current pkey, parentkey

        Session.Add("Action", "Copy")
        Session.Add("Objects", sCopyObjects)
        Session.Add("Father", sParentKey)
        Session.Add("CutCopyCatalog", sPkeyCatalog)
        Session.Add("CutCopySelf", Request.QueryString("Self").ToLower())

        Session.Add("FromReport", sFromReport)

        Response.Write("<script language=""JavaScript"">window.close();</script>")

    End Sub

End Class
