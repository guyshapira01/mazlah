Public Class ShowTemplate
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

    'parameters from querystring:
    'Template name
    'pkey
    'type

    Private Sub BuildAndSubmitPage()

        Dim sTemplate As String = Request.QueryString("Template")
        Dim sPkey As String = Request.QueryString("Pkey")
        Dim sType As String = Request.QueryString("Type")
        Dim sOutput As String = ""
        Dim oCHtmlGenerator As New CHtmlGenerator(Server.MapPath(sTemplate), True, Application("ODBCNAME"), sType, Request.ServerVariables("LOGON_USER"), Application("gemAppMode"), Application("ImageMapManage"))
        Dim oAclPermissions As cACLPermissions
        Dim oLWObjectTypes As New LWObjectTypes
        Dim sParentKey As String = Request.QueryString("ParentKey") & ""

        If (Request.QueryString("PkeyCatalog") = "") Then
            oCHtmlGenerator.SCatalog = ""
        Else
            oCHtmlGenerator.SCatalog = Request.QueryString("PkeyCatalog")
        End If

        'first level - father=catalog pkey
        If sParentKey = "" Then
            sParentKey = oCHtmlGenerator.SCatalog
        End If

        oCHtmlGenerator.ServerObj = Server
        oCHtmlGenerator.CurrentURL = Request.Url.AbsoluteUri
        oCHtmlGenerator.SParent = sParentKey
        Session.Timeout = 20
        'Session.Clear()


        oAclPermissions = New cACLPermissions(Request.ServerVariables("LOGON_USER"), Application("ODBCNAME"), Application("gemAppMode"), sType)

        If CBool(oAclPermissions.HasPermission(sPkey, "P_SHOW")) Then

            Select Case sType
                Case oLWObjectTypes.emPage
                    sOutput = oCHtmlGenerator.GetOutput("SELECT  T_CAT_FOLDER.PHEBDESC AS FOLDERNAME ,  " & oLWObjectTypes.emPageTable & ".*,   " & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM T_CAT_NODES, " & oLWObjectTypes.emPageTable & "," & oLWObjectTypes.emCatalogTable & ",T_CAT_FOLDER WHERE " & oLWObjectTypes.emPageTable & ".PKEY='" & sPkey & "' AND T_CAT_NODES.PKEY=T_CAT_PAGE.PKEY AND T_CAT_NODES.PPARENTKEY='" & sParentKey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPageTable & ".PKEYCATALOG  AND T_CAT_NODES.PPARENTKEY = T_CAT_FOLDER.PKEY(+)")
                Case oLWObjectTypes.emPart
                    If Application("gemAppMode") = "Access" Then
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkey & "'")
                    Else
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".* ,  " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM T_CAT_NODES, " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkey & "' AND T_CAT_NODES.PKEY=T_CAT_PART.PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & sParentKey & "' AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY(+)='" & oCHtmlGenerator.SCatalog & "' AND " & oLWObjectTypes.emStatusTable & ".pkeycatalog(+)='" & oCHtmlGenerator.SCatalog & "'")
                    End If
                Case oLWObjectTypes.emPicture
                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPicturePropertyTable & ".* ,  " & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM T_CAT_NODES, " & oLWObjectTypes.emPicturePropertyTable & "," & oLWObjectTypes.emCatalogTable & " WHERE " & oLWObjectTypes.emPicturePropertyTable & ".PKEY='" & sPkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPicturePropertyTable & ".PKEYCATALOG")
                Case oLWObjectTypes.emCatalog
                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emCatalogTable & ".*, " & oLWObjectTypes.emCatalogTable & "_1.PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emCatalogTable & ", " & oLWObjectTypes.emCatalogTable & " " & oLWObjectTypes.emCatalogTable & "_1 WHERE " & oLWObjectTypes.emCatalogTable & ".PKEY='" & sPkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emCatalogTable & "_1.PKEY")
                Case oLWObjectTypes.emWinObject
                    If Application("gemAppMode") = "Access" Then
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emWinObjectTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emWinObjectTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emWinObjectTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkey & "'")
                    Else
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emWinObjectTable & ".*,   " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM T_CAT_NODES, " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkey & "' AND T_CAT_WINOBJECT.PKEY=T_CAT_NODES.PKEY AND T_CAT_NODES.PPARENTKEY='" & sParentKey & "' AND " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY(+)='" & oCHtmlGenerator.SCatalog & "' AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & oCHtmlGenerator.SCatalog & "'")
                    End If
            End Select

            If (sTemplate.ToLower.StartsWith("index") Or sTemplate.ToLower.StartsWith("normal")) Then
                Session("ItemProp") = oCHtmlGenerator.SessionProp
            End If

            If UCase(Request.QueryString("format")) <> "EXCEL" Then
                If InStr(sTemplate, "xml", CompareMethod.Text) Then
                    Response.ContentType = "text/xml"
                Else
                    Response.ContentType = "text/html"
                End If
            Else
                Dim CinstCXmlTransformation As New CXmlXslTransformation
                Dim stringWrite As New System.IO.StringWriter

                Response.ContentType = "application/vnd.ms-excel"
                Response.ContentEncoding = System.Text.Encoding.GetEncoding(38598)
                Response.Charset = "iso-8859-8-i"
                Dim sXsl As String
                sXsl = Server.MapPath(Replace(Request.QueryString("Template"), ".xml", ".xsl"))
                If Not System.IO.File.Exists(sXsl) Then
                    sXsl = Replace(sXsl, "_excel", "", 1)
                End If
                stringWrite = CinstCXmlTransformation.TransformXmlToString(sOutput, sXsl)
                sOutput = stringWrite.ToString
                sOutput = Replace(sOutput, "utf-16", "iso-8859-8-i")


            End If
            Response.Write(sOutput & vbCrLf)
        Else

            Response.Write("<script language=""JavaScript"">alert('You dont have permission to view this page');if (history.length==0)  {window.close();}   else    {history.back();}</script>")

        End If

        oAclPermissions.Dispose()
        oAclPermissions = Nothing
        oCHtmlGenerator.Dispose()
        oCHtmlGenerator = Nothing
        oLWObjectTypes = Nothing

    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Response.AddHeader("cache-control", "private")
        Response.AddHeader("pragma", "no-cache")
        Response.ExpiresAbsolute = "#January 1, 1990 00:00:01#"
        Response.Expires = 0

        'Put user code to initialize the page here
        Dim dStartDate As Date
        Dim dStartTimer As Double
        dStartDate = Now()
        ' dStartTimer = Timer()

        BuildAndSubmitPage()
        'Dim Thread1 As New System.Threading.Thread(AddressOf BuildAndSubmitPage)
        'Thread1.Start()
        'Thread1.Join()

        'Response.Write("<!-- ShowTemplatePage Start Time: " & dStartDate & "-->" & vbCrLf)
        'Response.Write("<!-- ShowTemplatePage End Time: " & Now() & "-->" & vbCrLf)
        'Response.Write("<!-- ShowTemplatePage Duration(sec.):" & (Timer() - dStartTimer) & "-->" & vbCrLf)

    End Sub

End Class
