'Overload #1 
'A Dataset 
'The response object of the web page 
'Overload #2 
'A Dataset 
'An index value of which table from the dataset 
'The response object of the web page 
'Overload #3 
'A Dataset 
'The table name of a table in the dataset 
'The response object of the web page 
'The methods will be shared methods so that we don’t have to instaniate the class in order to use the method. 

'What makes this task so straight forward is the elegance of the .NET Framework design. It turns out that most web controls have a RenderControl method which will write an html text stream. All we need to do is set up the response object, call the RenderControl method of a datagrid and tell the response object to output the "rendering". Pretty simple! 

'Here’s the code for the class (DataSetToExcel.vb): 

'Class to convert a dataset to an html stream which can be used to display the dataset
'in MS Excel
'The Convert method is overloaded three times as follows
' 1) Default to first table in dataset
' 2) Pass an index to tell us which table in the dataset to use
' 3) Pass a table name to tell us which table in the dataset to use

Public Class DataSetToExcel

    Public Shared Sub Convert(ByVal ds As Data.DataSet, ByVal response As HttpResponse)
        'first let's clean up the response.object
        response.Clear()
        response.Charset = ""
        'set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        'instantiate a datagrid
        Dim dg As New DataGrid
        'set the datagrid datasource to the dataset passed in
        dg.DataSource = ds.Tables(0)
        'bind the datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'all that's left is to output the html
        response.Write(stringWrite.ToString)
        response.End()
    End Sub

    Public Shared Sub Convert(ByVal ds As Data.DataSet, ByVal TableIndex As Integer, ByVal response As HttpResponse)
        'lets make sure a table actually exists at the passed in value
        'if it is not call the base method
        If TableIndex > ds.Tables.Count - 1 Then
            Convert(ds, response)
        End If
        'we've got a good table so
        'let's clean up the response.object
        response.Clear()
        response.Charset = ""
        'set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        'instantiate a datagrid
        Dim dg As New DataGrid
        'set the datagrid datasource to the dataset passed in
        dg.DataSource = ds.Tables(TableIndex)
        'bind the datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'all that's left is to output the html
        response.Write(stringWrite.ToString)
        response.End()
    End Sub

    Public Shared Sub Convert(ByVal ds As Data.DataSet, ByVal TableName As String, ByVal response As HttpResponse)
        'let's make sure the table name exists
        'if it does not then call the default method
        If ds.Tables(TableName) Is Nothing Then
            Convert(ds, response)
        End If
        'we've got a good table so
        'let's clean up the response.object
        response.Clear()
        response.Charset = ""
        'set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        'instantiate a datagrid
        Dim dg As New DataGrid
        'set the datagrid datasource to the dataset passed in
        dg.DataSource = ds.Tables(TableName)
        'bind the datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'all that's left is to output the html
        response.Write(stringWrite.ToString)
        response.End()
    End Sub

End Class
