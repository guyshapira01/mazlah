Public Class EditText
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

    Dim sPkey As String = ""
    Dim sMode As String = ""
    Dim sTemplate As String = ""
    Dim oConn As CConnection


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.btnEdit.Attributes.Add("onmouseover", "this.style.cursor='Hand';")
        Me.btnDel.Attributes.Add("onmouseover", "this.style.cursor='Hand';")
        Me.btnClose.Attributes.Add("onclick", "window.close();")

        sPkey = Request.QueryString("pkey") & ""
        sMode = Request.QueryString("Mode") & ""
        sTemplate = Request.QueryString("template") & ""
        Dim sParent As String = Request.QueryString("ParentKey") & ""

        Dim sSqlQuery As String = ""
        Dim dbDataset As New Data.DataSet

        If Not Page.IsPostBack Then
            oConn = New CConnection
            oConn.Open_Oracle_Connection(Application("ODBCNAME"))

            Select Case sMode
                Case "Edittext"
                    btnDel.Visible = False
                    sSqlQuery = "SELECT t_cat_text.* FROM t_cat_text,t_cat_nodes WHERE t_cat_nodes.pparentkey='" & sPkey & "' and t_cat_nodes.pobjecttype=6 and pglobalstatus<>4 and t_cat_nodes.pkey=t_cat_text.pkey"
                    oConn.Open_Dataset(sSqlQuery, dbDataset)
                    Dim i As Integer
                    For i = 0 To dbDataset.Tables(0).Rows.Count - 1
                        seltextBox.Items.Add(New ListItem(dbDataset.Tables(0).Rows(i)("PHEBDESC") & "", dbDataset.Tables(0).Rows(i)("PKEY") & ""))
                    Next
                    If seltextBox.Items.Count > 0 Then
                        seltextBox.SelectedIndex = 0
                        If seltextBox.Items.Count = 1 Then
                            btnClick()
                        End If
                    End If
                Case "Deltext"
                    btnEdit.Visible = False
                    sSqlQuery = "SELECT t_cat_text.* FROM t_cat_text,t_cat_nodes WHERE t_cat_nodes.pparentkey='" & sPkey & "' and t_cat_nodes.pobjecttype=6 and pglobalstatus<>4 and t_cat_nodes.pkey=t_cat_text.pkey"
                    oConn.Open_Dataset(sSqlQuery, dbDataset)
                    Dim i As Integer
                    For i = 0 To dbDataset.Tables(0).Rows.Count - 1
                        seltextBox.Items.Add(New ListItem(dbDataset.Tables(0).Rows(i)("PHEBDESC") & "", dbDataset.Tables(0).Rows(i)("PKEY") & ""))
                    Next
                    If seltextBox.Items.Count > 0 Then
                        seltextBox.SelectedIndex = 0
                        If seltextBox.Items.Count = 1 Then
                            btnDelClick()
                            Response.Write("<script>try{if (window.opener.top.frames.length != 0) window.opener.top.frames('Index').LinkSwitch('" & sPkey & "','" & sParent & "');else window.opener.document.location.reload();}catch(e){}window.close();</script>")
                            Response.Flush()
                        End If
                    End If
                Case "Delimage"
                    sSqlQuery = "SELECT t_cat_picture_property.* FROM t_cat_picture_property,t_cat_nodes WHERE t_cat_nodes.pparentkey='" & sPkey & "' and t_cat_nodes.pobjecttype=3 and pglobalstatus<>4 and t_cat_nodes.pkey=t_cat_picture_property.pkey"
                    oConn.Open_Dataset(sSqlQuery, dbDataset)
                    Dim i As Integer
                    For i = 0 To dbDataset.Tables(0).Rows.Count - 1
                        seltextBox.Items.Add(New ListItem(dbDataset.Tables(0).Rows(i)("PHEBDESC") & "", dbDataset.Tables(0).Rows(i)("PKEY") & ""))
                    Next
                    If seltextBox.Items.Count > 0 Then
                        seltextBox.SelectedIndex = 0
                        If seltextBox.Items.Count = 1 Then
                            btnDelClick()
                            Response.Write("<script>try{if (window.opener.top.frames.length != 0) window.opener.top.frames('Index').LinkSwitch('" & sPkey & "','" & sParent & "'); else window.opener.document.location.reload();}catch(e){}window.close();</script>")
                            Response.Flush()
                        End If
                    End If
            End Select

            oConn.Close_Connection()
            oConn = Nothing
        End If

    End Sub

    Private Sub btnClick()

        Dim sUrl As String = ""
        Dim sCatalog As String = Request.QueryString("PkeyCatalog") & ""
        Dim sParent As String = Request.QueryString("ParentKey") & ""
        Dim pkey As String = seltextBox.SelectedValue

        If (sTemplate = "") Then
            sTemplate = "Edit_Text_E.xsl"
        End If

        sUrl = "updateObject.aspx?PkeyCatalog=" & sCatalog & "&Pkey=" & sPkey & "&ParentFather=" & sParent & "&Self=False&Object=" & pkey & "&Table=T_CAT_TEXT&Template=" & sTemplate
        Response.Write("<script>window.resizeTo(700,600);</script>")
        Response.Redirect(sUrl)

    End Sub

    Private Sub btnDelClick()
        Dim pkey As String = seltextBox.SelectedValue
        Dim sSqlQ As String = "UPDATE T_CAT_NODES SET PGLOBALSTATUS=4 WHERE PKEY='" & pkey & "' AND PPARENTKEY='" & sPkey & "'"
        Dim dbDataset As New Data.DataSet

        oConn = New CConnection
        oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        oConn.Open_Dataset(sSqlQ, dbDataset)
        oConn.Close_Connection()
        oConn = Nothing

    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        If seltextBox.SelectedIndex <> -1 Then
            btnClick()
        End If

    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click

        If seltextBox.SelectedIndex <> -1 Then
            Dim sParent As String = Request.QueryString("ParentKey") & ""
            Dim pkey As String = seltextBox.SelectedValue
            btnDelClick()
            seltextBox.Items.RemoveAt(seltextBox.SelectedIndex)
            If seltextBox.Items.Count > 0 Then
                Response.Write("<script>try{if (window.opener.top.frames.length != 0) window.opener.top.frames('Index').LinkSwitch('" & sPkey & "','" & sParent & "'); else window.opener.document.location.reload();}catch(e){}</script>")
            Else
                Response.Write("<script><script>try{if (window.opener.top.frames.length != 0) window.opener.top.frames('Index').LinkSwitch('" & sPkey & "','" & sParent & "'); else window.opener.document.location.reload();}catch(e){}window.close();</script>")
            End If
        End If
    End Sub
End Class
