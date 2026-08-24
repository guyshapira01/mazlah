Public Class CXmlXslTransformation
    Public Function WriteXmlToFile(ByVal thisDataSet As Data.DataSet, ByVal tmpXmlFileName As String)
        If thisDataSet Is Nothing Then

        End If
        ' Create a file name to write to.
        Dim filename As String = tmpXmlFileName
        ' Create the FileStream to write with.
        Dim myFileStream As New System.IO.FileStream _
        (filename, System.IO.FileMode.OpenOrCreate)
        ' Create an XmlTextWriter with the fileStream.
        Dim myXmlWriter As New System.Xml.XmlTextWriter _
        (myFileStream, System.Text.Encoding.Unicode)
        ' Write to the file with the WriteXml method.
        thisDataSet.WriteXml(myXmlWriter)
        myXmlWriter.Close()
        myFileStream.Close()
        myXmlWriter = Nothing
        myFileStream = Nothing
    End Function
    Public Function TransformXmlToString(ByRef thisDataSet As Data.DataSet, ByVal xslFileName As String) As System.IO.StringWriter

        '-------------------------------------------------------
        Dim writerResult As New System.IO.StringWriter

        '=======================================================
        'Perform the transform
        Dim xslTrans As New System.Xml.Xsl.XslTransform   'The  XslTransformation Object
        xslTrans.Load(xslFileName)        'Load The Xsl 

        'load xml document
        Dim xmlDoc As New System.Xml.XmlDataDocument(thisDataSet)
        'enforce constratints because we change the xml...
        thisDataSet.EnforceConstraints = False

        'changing the DATE format to DD/MM/YYYY -> Its changes also the dataset value!!!!
        Dim node As System.Xml.XmlNode
        Dim dt As DateTime
        'For Each node In xmlDoc.SelectSingleNode("NewDataSet/Catalogs").ChildNodes
        '    If InStr(LCase(node.Name), "date") Then
        '        If node.ChildNodes.Count > 0 Then
        '            dt = DateTime.Parse(node.FirstChild.Value)
        '            node.FirstChild.Value = dt.ToString("dd/mm/yyyy")
        '        End If
        '    End If
        'Next

        Dim i As Integer
        'changes the date format for catalog childs
        For i = 0 To thisDataSet.Tables(0).Rows(0).ItemArray.Length - 1
            If LCase(thisDataSet.Tables(0).Rows(0).Item(i).GetType.ToString) = "system.datetime" Then
                node = xmlDoc.SelectSingleNode("NewDataSet/Catalogs/" & thisDataSet.Tables(0).Columns(i).ColumnName)
                If node.ChildNodes.Count > 0 Then
                    dt = DateTime.Parse(node.FirstChild.Value)
                    node.FirstChild.Value = dt.ToString("dd/MM/yyyy")
                End If
            End If
        Next

        'change the date format for TodayDate Childs
        For i = 0 To thisDataSet.Tables("TodayDate").Rows(0).ItemArray.Length - 1
            If LCase(thisDataSet.Tables("TodayDate").Rows(0).Item(i).GetType.ToString) = "system.datetime" Then
                node = xmlDoc.SelectSingleNode("NewDataSet/TodayDate/" & thisDataSet.Tables("TodayDate").Columns(i).ColumnName)
                If node.ChildNodes.Count > 0 Then
                    dt = DateTime.Parse(node.FirstChild.Value)
                    node.FirstChild.Value = dt.ToString("dd/MM/yyyy")
                End If
            End If
        Next

        'transform into html
        xslTrans.Transform(xmlDoc, Nothing, writerResult, Nothing)
        xmlDoc = Nothing
        xslTrans = Nothing
        '====================================
        'Return 
        Return writerResult
        '==================================

    End Function

    Public Function TransformXmlToString(ByVal thisString As String, ByVal xslFileName As String) As System.IO.StringWriter

        Dim writerResult As New System.IO.StringWriter
        '=======================================================
        'Perform the transform
        'writerResult.Encoding = System.Text.Encoding.GetEncoding(0)
        Dim xslTrans As New System.Xml.Xsl.XslTransform   'The  XslTransformation Object
        xslTrans.Load(xslFileName)         'Load The Xsl 
        Dim xmlDoc As New System.Xml.XmlDataDocument
        xmlDoc.LoadXml(thisString)
        xslTrans.Transform(xmlDoc, Nothing, writerResult, Nothing)
        xmlDoc = Nothing
        xslTrans = Nothing
        '====================================
        'Return 
        Return writerResult
        '==================================

    End Function


    Public Function ChangeDateFormat(ByRef thisDataSet As Data.DataSet) As String

        Dim node As System.Xml.XmlNode
        Dim nodeList As System.Xml.XmlNodeList
        Dim dt As DateTime
        Dim i As Integer
        'load xml document
        Dim xmlDoc As New System.Xml.XmlDataDocument(thisDataSet)

        thisDataSet.EnforceConstraints = False
        'changes the date format for catalog childs
        If thisDataSet.Tables.Count > 0 Then
            If thisDataSet.Tables(0).Rows.Count > 0 Then
                For i = 0 To thisDataSet.Tables(0).Rows(0).ItemArray.Length - 1
                    If LCase(thisDataSet.Tables(0).Rows(0).Item(i).GetType.ToString) = "system.datetime" Then
                        'can be some multiple nodes with the same name.
                        nodeList = xmlDoc.SelectNodes("NewDataSet/" & thisDataSet.Tables(0).TableName & "/" & thisDataSet.Tables(0).Columns(i).ColumnName)
                        If nodeList.Count > 0 Then
                            For Each node In nodeList
                                If node.ChildNodes.Count > 0 Then
                                    If IsDate(node.FirstChild.Value) Then
                                        dt = DateTime.Parse(node.FirstChild.Value)
                                        node.FirstChild.Value = dt.ToString("dd/MM/yyyy")
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next
            End If
        End If

        If Not thisDataSet.Tables("TodayDate") Is Nothing Then
            'change the date format for TodayDate Childs
            If thisDataSet.Tables("TodayDate").Rows.Count > 0 Then
                For i = 0 To thisDataSet.Tables("TodayDate").Rows(0).ItemArray.Length - 1
                    If LCase(thisDataSet.Tables("TodayDate").Rows(0).Item(i).GetType.ToString) = "system.datetime" Then
                        node = xmlDoc.SelectSingleNode("NewDataSet/TodayDate/" & thisDataSet.Tables("TodayDate").Columns(i).ColumnName)
                        If node.ChildNodes.Count > 0 Then
                            dt = DateTime.Parse(node.FirstChild.Value)
                            node.FirstChild.Value = dt.ToString("dd/MM/yyyy")
                        End If
                    End If
                Next
            End If
        End If

        If Not thisDataSet.Tables("ParentObject") Is Nothing Then
            'change the date format for paretobject Childs
            If thisDataSet.Tables("ParentObject").Rows.Count > 0 Then
                For i = 0 To thisDataSet.Tables("ParentObject").Rows(0).ItemArray.Length - 1
                    If LCase(thisDataSet.Tables("ParentObject").Rows(0).Item(i).GetType.ToString) = "system.datetime" Then
                        node = xmlDoc.SelectSingleNode("NewDataSet/ParentObject/" & thisDataSet.Tables("ParentObject").Columns(i).ColumnName)
                        If node.ChildNodes.Count > 0 Then
                            dt = DateTime.Parse(node.FirstChild.Value)
                            node.FirstChild.Value = dt.ToString("dd/MM/yyyy")
                        End If
                    End If
                Next
            End If
        End If

        ChangeDateFormat = xmlDoc.OuterXml()

        xmlDoc = Nothing

    End Function
End Class
