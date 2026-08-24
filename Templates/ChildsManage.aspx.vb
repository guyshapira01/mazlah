Public Class ChildsManage
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

        'check permissions
        Dim oAclPermissions As cACLPermissions
        oAclPermissions = New cACLPermissions(Request.ServerVariables("LOGON_USER"), Application("ODBCNAME"), Application("gemAppMode"))
        Dim hasPerm As Boolean = False
        Dim oConn As New CConnection
        Dim dbDataSet As Data.DataSet
        Dim dbDataSetChilds As Data.DataSet

        Dim aPkeys() As String
        Dim parsedUser As String
        Dim sMode As String = Request.QueryString("Mode")
        Dim sPkey As String = Request.QueryString("Object")
        Dim pID As String = Request.QueryString("ID")
        Dim myurl As String = Request.RawUrl.Substring(Request.RawUrl.IndexOf("RefUrl=") + 7, Request.RawUrl.Length - (Request.RawUrl.IndexOf("RefUrl=") + 7))

        If Not Page.IsPostBack() Then

            If (sMode = "Report") Then
                Dim i As Integer
                aPkeys = sPkey.Split("|")
                For i = 0 To aPkeys.Length - 1
                    If CBool(oAclPermissions.HasPermission(aPkeys(i), "P_MODIFY")) Then
                        hasPerm = True
                    Else
                        hasPerm = False
                        Exit For
                    End If
                Next
            Else
                    If CBool(oAclPermissions.HasPermission(Request.QueryString("Object"), "P_MODIFY")) Then
                        hasPerm = True
                    End If
            End If


            If hasPerm Then

                If Application("gemAppMode") = "Access" Then
                    oConn.Open_Access_Connection(Application("ODBCNAME"))
                Else
                    oConn.Open_Oracle_Connection(Application("ODBCNAME"))
                End If

                'check if we change the childs order:
                If Request.Form.Count = 0 Then

                    Dim dbDataSetOrder As Data.DataSet
                    Dim sSqlQuery As String
                    Dim sPkeyChilds As String
                    Dim oLWObjectTypes As New LWObjectTypes
                    Dim aPkeyChilds() As String
                    Dim curuser As String = Request.ServerVariables("LOGON_USER")

                    parsedUser = curuser.Substring(curuser.LastIndexOf("\") + 1)
                    dbDataSet = New Data.DataSet

                    If (sMode = "Report") Then
                        Dim cPerPrefs As New PersonalPref(Application("ODBCNAME"))
                        dbDataSet = cPerPrefs.GetPrefs("USER", parsedUser, "", "19", "", "URL", "IS", myurl, "PORDER" & pID)

                        If dbDataSet.Tables(0).Rows.Count > 0 Then
                            lblPartName.Text = "Reorder Report " & pID
                            If Not IsDBNull(dbDataSet.Tables(0).Rows(0)("PREFVALUE")) Then
                                sPkeyChilds = dbDataSet.Tables(0).Rows(0)("PREFVALUE")
                                aPkeyChilds = Split(sPkeyChilds, "|")
                            End If
                        End If

                        dbDataSet.Dispose()
                        dbDataSet = Nothing

                    Else
                        sSqlQuery = "SELECT T_CAT_PART.PKEYCHILDS, PHEBDESC FROM " & oLWObjectTypes.emPartTable & " WHERE PKEY='" & sPkey & "'"

                        oConn.Open_Dataset(sSqlQuery, dbDataSet)

                        If dbDataSet.Tables(0).Rows.Count > 0 Then
                            lblPartName.Text = dbDataSet.Tables(0).Rows(0).Item(1)
                            If Not IsDBNull(dbDataSet.Tables(0).Rows(0).Item(0)) Then
                                sPkeyChilds = dbDataSet.Tables(0).Rows(0).Item(0)
                                aPkeyChilds = Split(sPkeyChilds, "|")
                            End If
                        End If

                        dbDataSet.Dispose()
                        dbDataSet = Nothing
                    End If

                    'check if he has childs

                    If (sMode <> "Report") Then

                        dbDataSetChilds = New Data.DataSet
                        sSqlQuery = "SELECT " & oLWObjectTypes.emNodesTable & ".PKEY FROM " & oLWObjectTypes.emNodesTable & " WHERE PPARENTKEY LIKE '" & sPkey & "' AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
                        oConn.Open_Dataset(sSqlQuery, dbDataSetChilds)

                        If dbDataSetChilds.Tables(0).Rows.Count > 1 Then
                            Dim sSubQueryStatement As String
                            Dim iSubQueryStatement As Integer
                            If Not aPkeyChilds Is Nothing Then
                                sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,0 AS PORDER from dual union "
                                For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                    sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "'," & iSubQueryStatement & " from dual union "
                                Next
                                sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                            Else
                                sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                            End If

                            sSqlQuery = "SELECT OBJECTS.PKEY,PHEBDESC,POBJECTCLASS, PORDER, ptypename FROM (SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".PKEY, " & oLWObjectTypes.emPartTable & ".PHEBDESC,10 AS POBJECTCLASS, " & oLWObjectTypes.emObjectTypesTable & ".ptypename FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emNodesTable & "," & oLWObjectTypes.emObjectTypesTable & " WHERE " & oLWObjectTypes.emObjectTypesTable & ".pkey(+) = " & oLWObjectTypes.emPartTable & ".pkeytype" & " and PPARENTKEY ='" & sPkey & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY union " _
                                        & "SELECT DISTINCT " & oLWObjectTypes.emWinObjectTable & ".PKEY, " & oLWObjectTypes.emWinObjectTable & ".PFILEMODIFIEDDATE || ' - ' || " & oLWObjectTypes.emWinObjectTable & ".PDOCNUMBER || ' - ' ||" & oLWObjectTypes.emWinObjectTable & ".PHEBDESC AS PHEBDESC,18 AS POBJECTCLASS , ptypename FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emNodesTable & "," & oLWObjectTypes.emObjectTypesTable & " WHERE " & oLWObjectTypes.emObjectTypesTable & ".pkey(+) = " & oLWObjectTypes.emWinObjectTable & ".pkeytype" & " and PPARENTKEY ='" & sPkey & "' AND POBJECTTYPE=" & oLWObjectTypes.emWinObject & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emWinObjectTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY union " _
                                        & "SELECT DISTINCT " & oLWObjectTypes.emPicturePropertyTable & ".PKEY, " & oLWObjectTypes.emPicturePropertyTable & ".PHEBDESC,3 AS POBJECTCLASS , null FROM " & oLWObjectTypes.emPicturePropertyTable & ", " & oLWObjectTypes.emNodesTable & " WHERE PPARENTKEY ='" & sPkey & "' AND POBJECTTYPE=" & oLWObjectTypes.emPicture & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPicturePropertyTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY) OBJECTS, " _
                                        & sSubQueryStatement & " tmpChilds " _
                                        & "WHERE OBJECTS.PKEY = TMPCHILDS.PKEY (+) " _
                                        & "ORDER BY POBJECTCLASS, TMPCHILDS.PORDER "

                            dbDataSetOrder = New Data.DataSet
                            oConn.Open_Dataset(sSqlQuery, dbDataSetOrder)

                            dbDataSetChilds.Dispose()
                            dbDataSetChilds = Nothing

                        End If

                    Else

                        Dim sSubQueryStatement As String
                        Dim iSubQueryStatement As Integer
                        If Not aPkeyChilds Is Nothing Then
                            sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,0 AS PORDER from dual union "
                            For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "'," & iSubQueryStatement & " from dual union "
                            Next
                            sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                        Else
                            sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                        End If


                        Dim cKeys As String = sPkey.Replace("|", "','")
                        'cKeys.Insert(0, "'")
                        'cKeys.Insert(cKeys.Length - 1, "'")

                        sSqlQuery = "SELECT OBJECTS.PKEY,PHEBDESC,POBJECTCLASS, PORDER FROM (SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".PKEY, " & oLWObjectTypes.emPartTable & ".PHEBDESC,10 AS POBJECTCLASS FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emNodesTable & " WHERE T_CAT_NODES.PKEY IN('" & cKeys & "') AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY union " _
                                & "SELECT DISTINCT " & oLWObjectTypes.emWinObjectTable & ".PKEY, " & oLWObjectTypes.emWinObjectTable & ".PFILEMODIFIEDDATE || ' - ' || " & oLWObjectTypes.emWinObjectTable & ".PDOCNUMBER || ' - ' ||" & oLWObjectTypes.emWinObjectTable & ".PHEBDESC AS PHEBDESC,18 AS POBJECTCLASS FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emNodesTable & " WHERE T_CAT_NODES.PKEY IN('" & cKeys & "') AND POBJECTTYPE=" & oLWObjectTypes.emWinObject & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emWinObjectTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY union " _
                                & "SELECT DISTINCT " & oLWObjectTypes.emPicturePropertyTable & ".PKEY, " & oLWObjectTypes.emPicturePropertyTable & ".PHEBDESC,3 AS POBJECTCLASS FROM " & oLWObjectTypes.emPicturePropertyTable & ", " & oLWObjectTypes.emNodesTable & " WHERE T_CAT_NODES.PKEY IN('" & cKeys & "') AND POBJECTTYPE=" & oLWObjectTypes.emPicture & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPicturePropertyTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY) OBJECTS, " _
                                & sSubQueryStatement & " tmpChilds " _
                                & "WHERE OBJECTS.PKEY = TMPCHILDS.PKEY (+) " _
                                & "ORDER BY TMPCHILDS.PORDER "

                        dbDataSetOrder = New Data.DataSet
                        oConn.Open_Dataset(sSqlQuery, dbDataSetOrder)
                    End If

                    Dim sHtml As String = ""
                    Dim iCount As Integer
                    Dim sObjectPkey As String
                    Dim sObjectName As String
                    Dim iObjectClass As Integer
                    Dim sPType As String
                    'Dim color As String = "White"

                    sObjectPkey = dbDataSetOrder.Tables(0).Rows(0).Item("PKEY")
                    sObjectName = dbDataSetOrder.Tables(0).Rows(0).Item("PHEBDESC")
                    iObjectClass = dbDataSetOrder.Tables(0).Rows(0).Item("POBJECTCLASS")
                    If sMode <> "Report" Then
                        sPType = dbDataSetOrder.Tables(0).Rows(0).Item("ptypename") & ""
                        If sPType = "" Then
                            sPType = "EmptySpace"
                        End If
                    Else
                        sPType = "EmptySpace"
                    End If

                    sHtml = sHtml & "<!-- invisible image to use for dragging --><IMG id=cover " _
                                & "ondragstart=doOnDragStart(0); " _
                                & "style=""LEFT: 0px; POSITION: absolute; TOP: 0px"" height=22 " _
                                & "src=""../Images/clear.gif"" width=100%> <!-- invisible image to use for drop target --><IMG id=target " _
                                & "ondragenter=doOnDragEnter(0); ondrop=doOnDrop(0); " _
                                & "Style = ""LEFT: 0px; VISIBILITY: hidden; POSITION: absolute; TOP: 0px""" _
                                & "ondragover=doOnDragOver(); ondragleave=doOnDragLeave(0); " _
                                & "height=22 src=""../Images/clear.gif"" width=100%> <!-- invisible divider becomes visible when dragged over --> " _
                                & "<TABLE height=4 cellSpacing=0 cellPadding=0 width=100% " _
                                & "border=0>" _
                                & "<TBODY>" _
                                & "<TR>" _
                                & "<TD id=divider></TD></TR></TBODY></TABLE><!-- task -->" _
                                & "<TABLE " _
                                & "Style = ""BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid""" _
                                & "height=25 cellSpacing=0 cellPadding=0 width=100% border=0 class=""" & GetCssClass(iObjectClass) & """>" _
                                & "<TBODY>" _
                                & "<TR>" _
                                & "<TD width=100% nowrap  id=task " _
                                & "originalIndex=""" & sObjectPkey & """><table width=100%><tr><td Style = ""FONT-FAMILY:TAHOMA; FONT-SIZE:12px; PADDING-RIGHT: 2pt; PADDING-LEFT: 2pt; PADDING-BOTTOM: 2pt; PADDING-TOP: 2pt""  width=100% class=collabDraggableItem align=left>" & sObjectName & " " _
                                & "</td><td align = right><IMG id=typeitem height=16 src=""IconTypes/" & sPType & ".gif""></td></tr></table></TD></TR></TBODY></TABLE>"

                    For iCount = 1 To dbDataSetOrder.Tables(0).Rows.Count - 2
                        sObjectPkey = dbDataSetOrder.Tables(0).Rows(iCount).Item("PKEY")
                        sObjectName = dbDataSetOrder.Tables(0).Rows(iCount).Item("PHEBDESC")
                        iObjectClass = dbDataSetOrder.Tables(0).Rows(iCount).Item("POBJECTCLASS")
                        If sMode <> "Report" Then
                            sPType = dbDataSetOrder.Tables(0).Rows(iCount).Item("ptypename") & ""
                            If sPType = "" Then
                                sPType = "EmptySpace"
                            End If
                        Else
                            sPType = "EmptySpace"
                        End If

                        sHtml = sHtml & "<!-- invisible image to use for dragging --><IMG " _
                                    & "id=cover ondragstart=doOnDragStart(" & iCount & "); " _
                                    & "style=""LEFT: 0px; POSITION: absolute; TOP: 0px"" height=22 " _
                                    & "src=""../Images/clear.gif"" width=100%> <!-- invisible image to use for drop target --><IMG id=target " _
                                    & "ondragenter=doOnDragEnter(" & iCount & "); ondrop=doOnDrop(" & iCount & "); " _
                                    & "Style = ""LEFT: 0px; VISIBILITY: hidden; POSITION: absolute; TOP: 0px""" _
                                    & "ondragover=doOnDragOver(); ondragleave=doOnDragLeave(" & iCount & "); " _
                                    & "height=22 src=""../Images/clear.gif"" width=100%> <!-- invisible divider becomes visible when dragged over -->" _
                                    & "<TABLE height=4 cellSpacing=0 cellPadding=0 width=100% " _
                                    & "border=0>" _
                                    & "<TBODY>" _
                                    & "<TR>" _
                                    & "<TD id=divider></TD></TR></TBODY></TABLE><!-- task -->" _
                                    & "<TABLE " _
                                    & "Style = ""BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid""" _
                                    & "height=25 cellSpacing=0 cellPadding=0 width=100% border=0 class=""" & GetCssClass(iObjectClass) & """>" _
                                    & "<TBODY>" _
                                    & "<TR>" _
                                    & "<TD width=100% nowrap  id=task " _
                                    & "originalIndex=""" & sObjectPkey & """><table width=100%><tr><td Style = ""FONT-FAMILY:TAHOMA; FONT-SIZE:12px; PADDING-RIGHT: 2pt; PADDING-LEFT: 2pt; PADDING-BOTTOM: 2pt; PADDING-TOP: 2pt""  width=100% class=collabDraggableItem align=left>" & sObjectName & " " _
                                    & "</td><td align = right><IMG id=typeitem height=16 src=""IconTypes/" & sPType & ".gif""></td></tr></table></TD></TR></TBODY></TABLE>"
                    Next
                    sObjectPkey = dbDataSetOrder.Tables(0).Rows(dbDataSetOrder.Tables(0).Rows.Count - 1).Item("PKEY")
                    sObjectName = dbDataSetOrder.Tables(0).Rows(dbDataSetOrder.Tables(0).Rows.Count - 1).Item("PHEBDESC")
                    iObjectClass = dbDataSetOrder.Tables(0).Rows(iCount).Item("POBJECTCLASS")
                    If sMode <> "Report" Then
                        sPType = dbDataSetOrder.Tables(0).Rows(dbDataSetOrder.Tables(0).Rows.Count - 1).Item("ptypename") & ""
                        If sPType = "" Then
                            sPType = "EmptySpace"
                        End If
                    Else
                        sPType = "EmptySpace"
                    End If

                    'for the last div
                    sHtml = sHtml & "<!-- invisible image to use for dragging --><IMG " _
                                & "id=cover ondragstart=doOnDragStart(" & dbDataSetOrder.Tables(0).Rows.Count - 1 & "); " _
                                & "style=""LEFT: 0px; POSITION: absolute; TOP: 0px"" height=22 " _
                                & "src=""../Images/clear.gif"" width=100%> <!-- invisible image to use for drop target --><IMG id=target " _
                                & "ondragenter=doOnDragEnter(" & dbDataSetOrder.Tables(0).Rows.Count - 1 & "); ondrop=doOnDrop(" & dbDataSetOrder.Tables(0).Rows.Count - 1 & "); " _
                                & "Style = ""LEFT: 0px; VISIBILITY: hidden; POSITION: absolute; TOP: 0px""" _
                                & "ondragover=doOnDragOver(); ondragleave=doOnDragLeave(" & dbDataSetOrder.Tables(0).Rows.Count - 1 & "); " _
                                & "height=22 src=""../Images/clear.gif"" width=100%> <!-- invisible divider becomes visible when dragged over -->" _
                                & "<TABLE height=4 cellSpacing=0 cellPadding=0 width=100% " _
                                & "border=0>" _
                                & "<TBODY>" _
                                & "<TR>" _
                                & "<TD id=divider></TD></TR></TBODY></TABLE><!-- task -->" _
                                & "<TABLE " _
                                & "Style = ""BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid""" _
                                & "height=25 cellSpacing=0 cellPadding=0 width=100% border=0 class=""" & GetCssClass(iObjectClass) & """>" _
                                & "<TBODY>" _
                                & "<TR>" _
                                & "<TD width=100% nowrap  id=task " _
                                & "originalIndex=""" & sObjectPkey & """><table width=100%><tr><td Style = ""FONT-FAMILY:TAHOMA; FONT-SIZE:12px; PADDING-RIGHT: 2pt; PADDING-LEFT: 2pt; PADDING-BOTTOM: 2pt; PADDING-TOP: 2pt""  width=100% class=collabDraggableItem align=left>" & sObjectName & " " _
                                & "</td><td align = right><IMG id=typeitem height=22 src=""IconTypes/" & sPType & ".gif""></td></tr></table></TD></TR></TBODY></TABLE><!-- invisible image to use for drop target --><IMG " _
                                & "id=target ondragenter=doOnDragEnter(" & dbDataSetOrder.Tables(0).Rows.Count & "); ondrop=doOnDrop(" & dbDataSetOrder.Tables(0).Rows.Count & "); " _
                                & "Style = ""LEFT: 0px; VISIBILITY: hidden; POSITION: absolute; TOP: 0px""" _
                                & "ondragover=doOnDragOver(); ondragleave=doOnDragLeave(" & dbDataSetOrder.Tables(0).Rows.Count & "); " _
                                & "height=22 src=""../Images/clear.gif"" width=100%> <!-- invisible divider becomes visible when dragged over -->" _
                                & "<TABLE height=4 cellSpacing=0 cellPadding=0 width=100% " _
                                & "border=0>" _
                                & "<TBODY>" _
                                & "<TR>" _
                                & "<TD id=divider ondragenter=doOnDragEnter(this); " _
                                & "ondrop=doOnDrop(" & dbDataSetOrder.Tables(0).Rows.Count & "); ondragover=doOnDragOver(); " _
                                & "ondragleave=doOnDragLeave(this);></TD></TR></TBODY></TABLE>"

                    lblTableLoop.Text = sHtml
                    lblScript.Text = "<SCRIPT src=""../Scripts/DragAndDrop/dragndrop.js""></SCRIPT>"

                    lbltaskIndices.Text = "<INPUT type=""hidden"" value="""
                    For iCount = 0 To dbDataSetOrder.Tables(0).Rows.Count - 1
                        lbltaskIndices.Text = lbltaskIndices.Text & iCount & ","
                    Next
                    lbltaskIndices.Text = Left(lbltaskIndices.Text, Len(lbltaskIndices.Text) - 1) & """ name=""taskIndices"">"

                    '''lstChilds.DataTextField = "PHEBDESC"
                    '''lstChilds.DataValueField = "PKEY"
                    '''lstChilds.DataSource = dbDataSetOrder
                    '''lstChilds.DataBind()

                    dbDataSetOrder.Dispose()
                    dbDataSetOrder = Nothing

                Else

                    If (sMode <> "Report") Then

                        Dim sNewChilds As String
                        Dim sPkeyToChange As String
                        Dim dbDataSetUpdate As New Data.DataSet
                        Dim sSqlQuery As String

                        sNewChilds = Request.Form("taskIndices")
                        sNewChilds = Replace(sNewChilds, ",", "|") & "|"
                        sPkeyToChange = Request.QueryString("Object")

                        sSqlQuery = "UPDATE T_CAT_PART SET PKEYCHILDS='" & sNewChilds & "' WHERE PKEY='" & sPkeyToChange & "'"

                        oConn.Open_Dataset(sSqlQuery, dbDataSetUpdate)

                        dbDataSetUpdate = Nothing

                        Response.Write("<script language=""JavaScript"">window.opener.top.document.location.reload(); window.close();</script>")

                    Else
                        Dim sNewChilds As String
                        sNewChilds = Request.Form("taskIndices")
                        sNewChilds = Replace(sNewChilds, ",", "|") & "|"

                        Dim curuser As String = Request.ServerVariables("LOGON_USER")
                        parsedUser = curuser.Substring(curuser.LastIndexOf("\") + 1)

                        Dim cSetPref As New PersonalPref(Application("ODBCNAME"))

                        cSetPref.SetPrefs("USER", parsedUser, " ", "19", " ", "URL", "IS", myurl, "PORDER" & pID, sNewChilds, "PPREFS")

                        Response.Write("<script language=""JavaScript"">window.opener.document.location.reload(); window.close();</script>")

                    End If


                End If

                oConn.Close_Connection()
                oConn = Nothing

            Else

                Response.Write("<script language=""JavaScript"">alert('You dont have permission to reorder object.');window.close();</script>")

            End If
        End If

        oAclPermissions.Dispose()
        oAclPermissions = Nothing

    End Sub

    Function GetCssClass(ByVal iObjectClass As Integer) As String

        Dim sReturnValue As String

        Select Case iObjectClass
            Case 3
                'picture
                sReturnValue = "Picture"
            Case 10
                'part
                sReturnValue = "Part"
            Case 18
                'document
                sReturnValue = "Document"
        End Select

        GetCssClass = sReturnValue

    End Function
    '''Private Sub btnDownArrow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownArrow.Click
    '''    Dim sItem As ListItem
    '''    Dim iItemIndex As Integer
    '''    If lstChilds.SelectedIndex < lstChilds.Items.Count - 1 Then
    '''        sItem = lstChilds.SelectedItem
    '''        iItemIndex = lstChilds.SelectedIndex
    '''        lstChilds.Items.Remove(lstChilds.SelectedItem)
    '''        lstChilds.Items.Insert(iItemIndex + 1, sItem)
    '''    End If
    '''End Sub

    '''Private Sub btnUpArrow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpArrow.Click
    '''    Dim sItem As ListItem
    '''    Dim iItemIndex As Integer
    '''    If lstChilds.SelectedIndex > 0 Then
    '''        sItem = lstChilds.SelectedItem
    '''        iItemIndex = lstChilds.SelectedIndex
    '''        lstChilds.Items.Remove(lstChilds.SelectedItem)
    '''        lstChilds.Items.Insert(iItemIndex - 1, sItem)
    '''    End If
    '''End Sub

    '''Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click

    '''End Sub
End Class
