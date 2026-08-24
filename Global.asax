<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started

        Dim sOdbcName As String
        Dim gemAppMode As String
        Dim sCatalogPath As String
        Dim sSiteUrl As String
        Dim sRecordsPerPage As String
        Dim sLogPath As String
        Dim sVolumeDirectory As String
        Dim sCheckOutCheckOnEdit As String
        Dim sMustCheckOnEdit As String
        Dim sCacheDirectory As String
        Dim sImageMapManage As String
        Dim sGenralLinkToHide As String
        Dim sMappedDrive As String
        Dim sMappedDriveUNC As String
        Dim sMappedWorkingDrive As String
        Dim sMappedWorkingDriveUNC As String
        Dim sPublishOfflineDirectory As String
        Dim sPublishCDDirectory As String
        Dim sWebSharingMappedDriveName As String
        Dim pwdStr As String
        Dim sLeftCornerColor As String
        Dim sLeftCornerColorText As String
        Dim sFooterBorder As String
        Dim sFooterBackColor As String
        Dim sDayBarColor As String
        Dim sBackStyle As String
        Dim sBackStyleAlternate As String
        Dim sBorderColor As String
        Dim sLicense As String
        Dim sPkeyPrefix As String
		Dim sStatisticsDefaultMonthPriod As String
        Dim oCCrypto As New ClsCrypto
        Dim oCXmlConfig As New CXmlConfig(System.AppDomain.CurrentDomain.BaseDirectory() & "Config.xml")

        'Get The ODBC Name
        sOdbcName = oCXmlConfig.GetParameter("AppConfiguration/ConnectionString")
        'Get The Aplication Mode Access/Oracle/Sql Server 2000
        gemAppMode = oCXmlConfig.GetParameter("AppConfiguration/ApplicationMode")
        sRecordsPerPage = oCXmlConfig.GetParameter("CatalogList/NumofRecordsPerPage")
        ''sCatalogPath = oCXmlConfig.GetParameter("AppConfiguration/CatalogPath")
        ''sSiteUrl = oCXmlConfig.GetParameter("AppConfiguration/SiteUrl")
        sLogPath = oCXmlConfig.GetParameter("AppConfiguration/LogFile")
        sVolumeDirectory = oCXmlConfig.GetParameter("AppConfiguration/VolumeDirectory")
        sCheckOutCheckOnEdit = oCXmlConfig.GetParameter("AppConfiguration/CheckOutCheckOnEdit")
        sMustCheckOnEdit = oCXmlConfig.GetParameter("AppConfiguration/MustCheckOnEdit")
        sGenralLinkToHide = oCXmlConfig.GetParameter("AppConfiguration/GenralLinkToHide")
        sCacheDirectory = oCXmlConfig.GetParameter("AppConfiguration/CacheDirectory")
        sImageMapManage = oCXmlConfig.GetParameter("AppConfiguration/ImageMapManage")
        sMappedDrive = oCXmlConfig.GetParameter("AppConfiguration/MappedDrive")
        sMappedDriveUNC = oCXmlConfig.GetParameter("AppConfiguration/MappedDriveUNC")
        sMappedWorkingDrive = oCXmlConfig.GetParameter("AppConfiguration/MappedWorkingDrive")
        sMappedWorkingDriveUNC = oCXmlConfig.GetParameter("AppConfiguration/MappedWorkingDriveUNC")
        sPublishOfflineDirectory = oCXmlConfig.GetParameter("AppConfiguration/PublishOfflineDirectory")
        sPublishCDDirectory = oCXmlConfig.GetParameter("AppConfiguration/PublishCDDirectory")
        sWebSharingMappedDriveName = oCXmlConfig.GetParameter("AppConfiguration/WebSharingMappedDriveName")
        sLicense = oCXmlConfig.GetParameter("licenseCode")


        sLeftCornerColor = oCXmlConfig.GetParameter("LWCalendar/LeftCornerColor")
        sLeftCornerColorText = oCXmlConfig.GetParameter("LWCalendar/LeftCornerColorText")
        sFooterBorder = oCXmlConfig.GetParameter("LWCalendar/FooterBorder")
        sFooterBackColor = oCXmlConfig.GetParameter("LWCalendar/FooterBackColor")
        sDayBarColor = oCXmlConfig.GetParameter("LWCalendar/DayBarColor")
        sBackStyle = oCXmlConfig.GetParameter("LWCalendar/BackStyle")
        sBackStyleAlternate = oCXmlConfig.GetParameter("LWCalendar/BackStyleAlternate")
        sBorderColor = oCXmlConfig.GetParameter("LWCalendar/BorderColor")

        sPkeyPrefix = oCXmlConfig.GetParameter("AppConfiguration/PkeyPrefix")
		sStatisticsDefaultMonthPriod = oCXmlConfig.GetParameter("AppConfiguration/StatisticsDefaultMonthPriod")
        oCXmlConfig = Nothing

        Application.Lock()
        ''Application("CatalogPath") = sCatalogPath
        pwdStr = sOdbcName.Substring(sOdbcName.IndexOf("PWD=") + 4)
        Try
        
            pwdStr = oCCrypto.clsCrypto(pwdStr.Substring(0, pwdStr.Length - 1), False)
            sOdbcName = sOdbcName.Substring(0, sOdbcName.IndexOf("PWD=") + 4) & pwdStr & ";"
        Catch
            
        End Try
        
        
        
        
        Application("OdbcName") = sOdbcName
        Application("gemAppMode") = gemAppMode
        Application("RecordsPerPage") = sRecordsPerPage
        Application("VolumeDirectory") = sVolumeDirectory
        Application("CheckOutCheckOnEdit") = sCheckOutCheckOnEdit
        Application("MustCheckOnEdit") = sMustCheckOnEdit
        Application("GenralLinkToHide") = sGenralLinkToHide
        Application("CacheDirectory") = sCacheDirectory
        Application("ImageMapManage") = sImageMapManage
        Application("MappedDrive") = sMappedDrive
        Application("MappedDriveUNC") = sMappedDriveUNC
        Application("MappedWorkingDrive") = sMappedWorkingDrive
        Application("MappedWorkingDriveUNC") = sMappedWorkingDriveUNC
        Application("PublishOfflineDirectory") = sPublishOfflineDirectory
        Application("PublishCDDirectory") = sPublishCDDirectory
        Application("WebSharingMappedDriveName") = sWebSharingMappedDriveName
        ''Application("SiteUrl") = sSiteUrl

        Application("LeftCornerColor") = sLeftCornerColor
        Application("LeftCornerColorText") = sLeftCornerColorText
        Application("FooterBorder") = sFooterBorder
        Application("FooterBackColor") = sFooterBackColor
        Application("DayBarColor") = sDayBarColor
        Application("BackStyle") = sBackStyle
        Application("BackStyleAlternate") = sBackStyleAlternate
        Application("BorderColor") = sBorderColor
        Application("License") = sLicense
        Application("PkeyPrefix") = sPkeyPrefix
		Application("StatisticsDefaultMonthPriod") = sStatisticsDefaultMonthPriod
		
        Try
            Application("NumOfConcurrentUsersLicense") = CInt(oCCrypto.clsCrypto(sLicense, False))
        Catch ex As Exception
            Application("NumOfConcurrentUsersLicense") = 1
        End Try
        Application("NumOfConcurrentUsers") = 0
        oCCrypto = Nothing
        Application.UnLock()

        'write the log file
        Try

            Dim cLog As New cLog(sLogPath, "Date:" & Now() & vbCrLf & vbCrLf & vbCrLf)
            cLog = Nothing

        Catch ex As Exception

            'Response.Write("Error:" & ex.Message & "<br>" & "Source:" & ex.Source)

        End Try

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
        If Application("NumOfConcurrentUsers") = Application("NumOfConcurrentUsersLicense") Then
            Session.Abandon()
            Response.End()
        Else
            Application("NumOfConcurrentUsers") = Application("NumOfConcurrentUsers") + 1
        End If
		Dim host, browser, OperatingSystem, userid As String
        host = Request.ServerVariables("HTTP_HOST").ToLower()
        browser = Request.ServerVariables("http_user_agent")
        OperatingSystem = Request.ServerVariables("HTTP_UA_CPU")
        userid = Request.ServerVariables("LOGON_USER").ToLower()
        Session("LOGON_USER")=userid
        
        InsertIntoStatisticsLogins(host, browser, OperatingSystem, userid)
    End Sub

    Sub InsertIntoStatisticsLogins(ByVal host As String, ByVal browser As String, ByVal OperatingSystem As String, ByVal userid As String)
        Dim strSQLExecute, strError As String
        strError = ""
        Dim oConn As New CConnectionR
        strSQLExecute = "INSERT INTO LWSTATISTICS_LOGINS (USERID,BROWSERNAME,HOSTNAME,TIMESTAMP, OPERATINGSYSTEM) VALUES ('" & _
                userid & "','" & browser & "','" & host & "',(select  sysdate from dual),'" & OperatingSystem & "')"
        oConn.Execute_Statement(strSQLExecute, "Oracle", Application("OdbcName"), strError)
    End Sub
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        'Dim ErrorDescription As String = Server.GetLastError.ToString

        ''Creation of event log if it does not exist  
        'Dim EventLogName As String = "ErrorSample"
        'If (Not Diagnostics.EventLog.SourceExists(EventLogName)) Then
        '    Diagnostics.EventLog.CreateEventSource(EventLogName, EventLogName)
        'End If
        
        Application("QueryParameters") = Request.ServerVariables("QUERY_STRING")
        'Dim strTemplate As String = Request.QueryString("Template")
        'Dim strReferer As String = Request.ServerVariables("HTTP_REFERER")

        '' Inserting into event log
        'Dim Log As New Diagnostics.EventLog()
        'Log.Source = EventLogName
        'Log.WriteEntry(ErrorDescription, Diagnostics.EventLogEntryType.Error)
    End Sub

    'Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Code that runs when a new session is started
    'End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>