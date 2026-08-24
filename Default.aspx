<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html dir="rtl" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="Templates/Styles/Linkware1.css" type="text/css" />
    <style type="text/css">
        .hiddencol
        {
            display:none;
        }
        .viscol
        {
            display:block;
        }
    </style>

    <!-- FillDDLCAtalog -->
     <script type="text/javascript" language="javascript">
    

    
    </script>
    <!--function ShowHideUpdateDate-->
    <script type="text/javascript" language="javascript">
    //Global variable : flag show/hidde UpdateDate
    var glShowUpdateDateFlag = 0;
    
    function ShowHideUpdateDate(obj){
        var oAllCUpdateDateHead = document.getElementById("AllCUpdateDateHead");
        var parent = document.getElementById("offTblBdy");
        var strDisplayStatus ;
        if(glShowUpdateDateFlag == 0){
            glShowUpdateDateFlag = 1;
            strDisplayStatus = "";
        }else{
            glShowUpdateDateFlag = 0;
            strDisplayStatus = "none";
        }
        items = parent.getElementsByTagName("TR");
        N = items.length;
        oAllCUpdateDateHead.style.display = strDisplayStatus;
        for(i=0;i<N;i++){
                  parent.rows[i].childNodes[4].style.display=strDisplayStatus;
        }
    }
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	function ActiveTabChanged(sender, e) {
            var CurrentTab = $get('<%#TabContainer1.ClientID%>');
            CurrentTab.innerHTML = sender.get_activeTab().get_headerText();
            //add a custom postback
             __doPostBack('TabContainer1', sender.get_activeTab().get_headerText());
            Highlight(CurrentTab);
        }
    function ShowHideUpdateDateNew(TableId,ColumnIndex){
        //var oAllCUpdateDateHead = document.getElementById("AllCUpdateDateHead");
        var parent = document.getElementById(TableId);
        var strDisplayStatus ;
        
        if(glShowUpdateDateFlag == 0){
            glShowUpdateDateFlag = 1;
            strDisplayStatus = "viscol";
        }else{
            glShowUpdateDateFlag = 0;
            strDisplayStatus = "hiddencol";
        }
        items = parent.getElementsByTagName("TR");
        N = items.length;
        
        //oAllCUpdateDateHead.style.display = strDisplayStatus;
        for(i=0;i<N;i++){
                  parent.rows[i].childNodes[ColumnIndex].className=strDisplayStatus;
        }
    }
    
    </script>
   <!--function SearchValidator-->  
    <script type="text/javascript" language="javascript">
		function SearchValidator(){
		//debugger;
		
		    var objrdbAllCatalogs = document.getElementById("TabContainer1_TabPanel2_rdbAllCatalogs");
		    var resTable = document.getElementById("TabContainer1_TabPanel2_PartSearchDataGrid");
		    var olblStatus = document.getElementById("TabContainer1_TabPanel2_lblLoadWait");
		    var objCatalogDDL = document.getElementById ("TabContainer1_TabPanel2_ddlCatalogList");
		    var objFamylyDDL = document.getElementById ("TabContainer1_TabPanel2_ddlFamily");
		    
		    if(olblStatus!=null)
		    {
		        olblStatus.style.display='none';
		    }
		    if(resTable!=null)
		    {
		        resTable.style.display='none';
		    }
		    
		    
		    
		    if(objrdbAllCatalogs.checked)return true;
		  //  alert("objrdbAllCatalogs.checked")
		    if(objFamylyDDL.value != "-1")return true
		    //ddlCatalogList
		    if(objCatalogDDL.value != "-1")return true
		    alert("�� ����� ����� ����� �� �� ��������");
		    return false;
		}
	</script>
	<!--function ClearResults-->  
    <script type="text/javascript" language="javascript">
		function ClearResults(){
		    var resTable = document.getElementById("TabContainer1_TabPanel2_PartSearchDataGrid");
		    var olblStatus = document.getElementById("TabContainer1_TabPanel2_lblLoadWait");
		    var objFamylyDDL = document.getElementById ("TabContainer1_TabPanel2_ddlFamily");
		    var objCatalogDDL = document.getElementById ("TabContainer1_TabPanel2_ddlCatalogList");
		    olblStatus.innerText="";
		    olblStatus.style.display='none';
		    //resTable.innerHTML='';
		    //alert(resTable.innerText);
		    resTable.innerText='';
		    //alert(resTable.innerText);
		    
		    resTable.style.display='none';
		    
		    var oproduce = document.getElementById("TabContainer1_TabPanel2_txbProduce");
		    var oname = document.getElementById("TabContainer1_TabPanel2_txbName");
		    var olblStatus = document.getElementById("TabContainer1_TabPanel2_txbMakat");
		    oproduce.value='';
		    oname.value='';
		    olblStatus.value='';
		    
		    objFamylyDDL.selectedIndex=0;
		    objCatalogDDL.selectedIndex=0;
            return false;
		}
	</script>
	<!--functions Open pages-->  
	<script type="text/javascript" language="javascript">	
  	function openNews()	{
		window.open("News.aspx","news");
	}
	
	function openLatestCatalogs()	{
		window.open("LatestCatalogs.aspx","LatestCatalogs", "toolbar,scrollbars,width=360,height=340");
	}

	function openNewWin(url,name,width,height)	{
		window.open(url,name, "toolbar,scrollbars,width=" + width + ",height=" + height);
	}
    function OpenSearchFunc(){
    //debugger;
        var tabBehavior = document.getElementById("TabContainer1").control;
        tabBehavior.set_activeTabIndex(1);
    }
    function OpenTab(tabIndex){

        var tabBehavior = document.getElementById("TabContainer1").control;
        tabBehavior.set_activeTabIndex(tabIndex);

    }
    function OpenFamilyTV(){
        var tabBehavior = document.getElementById("TabContainer1").control;
        tabBehavior.set_activeTabIndex(0);
    }

    function OpenDoc(sFileUrl)
    {
        var w = window.open ('/PrintedCatalog/'+sFileUrl,'myDoc');
    }

//AllCatalogInput
function ShowAllCatalogTable(){
       //debugger;
       var oDG = document.getElementById ("TabContainer1_TabPanel1_CatalogsDataGrid");
       var strOutPut = "<table cellspacing='0' cellpadding='2' border='0' id='AllCatalogsDataGrid' class='lwHeader' style='color:Black;;border-width:1px;border-style:solid;font-family:Arial;font-size:10pt;width:100%;border-collapse:collapse;'>";
          strOutPut = strOutPut + "<thead ><tr class='lwHeader'  style='font-weight:bold;color:white;'>";
          strOutPut = strOutPut + "<th align='right' style='width:150px;'><a style='color:white;' href='javascript:sortTable(0);'>�����</a></th>";
          strOutPut = strOutPut + "<th align='right' style='width:200px;'><a style='color:white;' href='javascript:sortTable(1);'>�� �����</a></th>";
          strOutPut = strOutPut + "<th align='right' style='width:100px;'><a style='color:white;' href='javascript:sortTable(2);'>�����</a></th>";
          strOutPut = strOutPut + "<th align='right' style='width:200px;'><a style='color:white;' href='javascript:sortTable(3);'>�����</a></th>";
          strOutPut = strOutPut + "<th align='right' style='width:100px; display: none' id='AllCUpdateDateHead'><a style='color:white;' href='javascript:sortTable(4);'>����� ������</a></th>";
          strOutPut = strOutPut + "</tr>";
          strOutPut = strOutPut + "</thead>";
          strOutPut = strOutPut + "<tbody id='offTblBdy'>";
          
       /*<tr align="right" style="background-color:#6C7444;font-weight:bold;">
						<td>
                                                         <a  href="javascript:sortTable('TabContainer1_TabPanel5_AllCatalogsDataGrid',0);">�����</a>
                                                </td><td>
                                                         <a  href="javascript:sortTable('TabContainer1_TabPanel5_AllCatalogsDataGrid',1);">�� �����</a>
                                                </td><td>
                                                         <a  href="javascript:sortTable('TabContainer1_TabPanel5_AllCatalogsDataGrid',2);">�����</a>
                                                </td><td>
                                                         <a  href="javascript:sortTable('TabContainer1_TabPanel5_AllCatalogsDataGrid',3);">�����</a>
                                                </td><td>
                                                         <a  href="javascript:sortTable('TabContainer1_TabPanel5_AllCatalogsDataGrid',4);">����� ������</a>
                                                </td>
					</tr>*/
					//alert(1);
					//return(0);
					var items = oDG.getElementsByTagName("TR");
					
					
                    var N = items.length;
                    var alternateFlag = 1;
		            var dir='rtl';
								for(i=1;i<N;i++){
								    
								    if (oDG.rows[i].getAttribute("CATDIR").length>0)
							        {
							            dir='ltr';
							            //alert(oDG.rows[i].getAttribute("CATDIR")+ ' ' + oDG.rows[i].cells[1].innerHTML);
							        }
							        else
							        {
							            dir='rtl';
				        }
					    if(alternateFlag){
					          strOutPut = strOutPut + "<tr class='lwAlternate'>";
                              strOutPut = strOutPut + "<td align='right' style='width:150px;'>" + oDG.rows[i].cells[0].outerText + "</td>";
                              strOutPut = strOutPut + "<td style='direction:" + dir +";text-align:right;'>" + oDG.rows[i].cells[1].innerHTML + "</td>";
                              strOutPut = strOutPut + "<td>" + oDG.rows[i].cells[2].outerText + "</td>";
                              strOutPut = strOutPut + "<td>" + oDG.rows[i].getAttribute("FAMILY_NAME") + "</td>";
                              strOutPut = strOutPut + "<td style='display: none'>" + oDG.rows[i].getAttribute("UPDATE_DATE") + "</td>";
                              strOutPut = strOutPut + "</tr>";
                              alternateFlag = 0;
                         }else{
                              strOutPut = strOutPut + "<tr class='lwBackground'>";
                              strOutPut = strOutPut + "<td align='right' style='width:150px;'>" + oDG.rows[i].cells[0].outerText + "</td>";
                              strOutPut = strOutPut + "<td style='direction:" + dir +";text-align:right;'>" + oDG.rows[i].cells[1].innerHTML + "</td>";
                              strOutPut = strOutPut + "<td>" + oDG.rows[i].cells[2].outerText + "</td>";
                              strOutPut = strOutPut + "<td>" + oDG.rows[i].getAttribute("FAMILY_NAME") + "</td>";
                              strOutPut = strOutPut + "<td  style='display: none'>" + oDG.rows[i].getAttribute("UPDATE_DATE") + "</td>";
                              strOutPut = strOutPut + "</tr>";
                              alternateFlag = 1;
 
                         }
					    
					}
					strOutPut = strOutPut + "</tbody>";
                    strOutPut = strOutPut + "</table>";
                    var oAllCatalogInput = document.getElementById("AllCatalogDIV");
                    oAllCatalogInput.innerHTML = strOutPut;
}

var strOldFamilyName = "-1";

function OnNodeClick (i,strFamilyName){
//debugger;
    var oDG = document.getElementById ("TabContainer1_TabPanel1_CatalogsDataGrid");
    var oFN = document.getElementById ("TabContainer1_TabPanel1_lblFamilyName");
    var oOpenMsg = document.getElementById ("TabContainer1_TabPanel1_lblOpenMsg");
    var oTreeViewt = document.getElementById ("TabContainer1_TabPanel1_FamilyTreeViewt" + i);
    var oTreeViewn = document.getElementById ("TabContainer1_TabPanel1_FamilyTreeViewn" + i);
    var oFamilyTreeViewnNodes = document.getElementById ("TabContainer1_TabPanel1_FamilyTreeViewn" + i + "Nodes");
    //document.writeln("<script>" + oTreeViewn.href + "<//script>")
    //var strParameters = oTreeViewn.href;
    try{
        TreeView_ToggleNode(TabContainer1_TabPanel1_FamilyTreeView_Data,i,oTreeViewn,' ',oFamilyTreeViewnNodes);
    }catch(err){}
    //TreeView_ToggleNode(TabContainer1_TabPanel1_FamilyTreeView_Data,5,TabContainer1_TabPanel1_FamilyTreeViewn5,' ',TabContainer1_TabPanel1_FamilyTreeViewn5Nodes)"
    //oTreeViewn.onclick();
    //TabContainer1_TabPanel1_FamilyTreeViewn1Nodes
    /*if(oFamilyTreeViewnNodes.style.display=="none"){
        oFamilyTreeViewnNodes.style.display="";
    }else{
        oFamilyTreeViewnNodes.style.display="none";
    }
    var oImg = oTreeViewt.parentElement.parentElement.getElementbyTag("img");
    */
    
    oFN.innerText=oTreeViewt.innerText;
    oFN.style.display="";
    oDG.style.display="";
    oOpenMsg.style.display="none";
     //var i = document.getElementById ("lblCount").outerText ;
     //var i ;
     var span_textnode = document.getElementById ("lblCount").firstChild;
     var span_text = span_textnode.data;
     var i=(span_text);
     i--;
     
    for(;i>=0;){
        var oTR = document.getElementById("TRCatalogsDataGrid_"+i);
        if(oTR.getAttribute("FAMILY_PKEY") != ""){
            var aKeys=oTR.getAttribute("CHILD_FAMILY").split(',');
            var k=false;
            for(var j=0;j<aKeys.length;j++)
            {
                
                if(strFamilyName==aKeys[j])
                    k=true;
            }
            /*if((oTR.getAttribute("FAMILY_PKEY") !=  strFamilyName)&& (oTR.getAttribute("FAMILY_PKEY") !=  strOldFamilyName)){
                //oTR.style.display="none";
                
                 var oTRPrev = document.getElementById ("TRCatalogsDataGrid_"+(i+1));
                 try{
                     if(oTRPrev.getAttribute("EndInd") != null){
                        i = i - oTRPrev.getAttribute("EndInd")
                     }else{
                        i--;
                     }
                     }catch(err){
                        i--;
                     }
                
              }
              else{*/
                //if(1)
                //{  
                //if(oTR.getAttribute("FAMILY_PKEY") ==  strFamilyName)
                    if(k)
                    {
                        //alert(oTR.getAttribute("CHILD_FAMILY"));
                        oTR.style.display="";
                        i--;
                    }
                    else
                    {
                        //alert(oTR.getAttribute("CHILD_FAMILY"));
                        oTR.style.display="none";
                        i--;
                    }
                 
                //}
                if (oTR.getAttribute("CATDIR").length>0)
	            {
	                dir='ltr';
	                //alert(oDG.rows[i].getAttribute("CATDIR")+ ' ' + oDG.rows[i].cells[1].innerHTML);
	            }
	            else
	            {
	                dir='rtl';
	            }
	            oTR.style.direction=dir;
        }
        else
        {
            oTR.style.display="none";
            i--;
        }
    }
    
    strOldFamilyName = strFamilyName;
    //alert(strOldFamilyName);
}


  // global variables
  var oldCol=-1;
  var col = 0;
  var parent = null;
  var items = new Array();
  var N = 0;

  function get(i)
  {
    var node = items[i].getElementsByTagName("TD")[col];   
    if(node.childNodes.length == 0) return "";
    var retval = node.outerText;//firstChild.nodeValue;
    if(parseInt(retval) == retval) return parseInt(retval);
    return retval;
  }

  function compare(val1, val2, desc)
  {
   var f1, f2,d1,d2;
 
  
  

   
  // If the values are numeric, convert them to floats.

  f1 = parseFloat(val1);
  f2 = parseFloat(val2);
  
  //d1 = new Date(val1); 
  //d2 = new Date(val2); 

  if (!isNaN(f1) && !isNaN(f2)) {
    val1 = f1;
    val2 = f2;
  }


    return (desc) ? val1 > val2 : val1 < val2;
  }

  function exchange(i, j)
  {
    if(i == j+1) {
      parent.insertBefore(items[i], items[j]);
    } else if(j == i+1) {
      parent.insertBefore(items[j], items[i]);
    } else {
      var tmpNode = parent.replaceChild(items[i], items[j]);
      if(typeof(items[i]) == "undefined") {
        parent.appendChild(tmpNode);
      } else {
        parent.insertBefore(tmpNode, items[i]);
      }
    }
  }

  function quicksort(m, n, desc)
  {
  
    if(n <= m+1) return;

    if((n - m) == 2) {
      if(compare(get(n-1), get(m), desc)) exchange(n-1, m);
      return;
    }

    i = m + 1;
    j = n - 1;

    if(compare(get(m), get(i), desc)) exchange(i, m);
    if(compare(get(j), get(m), desc)) exchange(m, j);
    if(compare(get(m), get(i), desc)) exchange(i, m);

    pivot = get(m);
    //var oCount=document.getElementById ("Text1")
    //oCount.value = j;

    while(true) {
      j--;
      while(compare(pivot, get(j), desc)) j--;
      i++;
      while(compare(get(i), pivot, desc)) i++;
      if(j <= i) break;
      exchange(i, j);
    }

    exchange(m, j);

    if((j-m) < (n-j)) {
      quicksort(m, j, desc);
      quicksort(j+1, n, desc);
    } else {
      quicksort(j+1, n, desc);
      quicksort(m, j, desc);
    }
  }

  function sortTable(n)
  
  {
        document.getElementById('TabContainer1_TabPanel5_UpdateProgress2').style.display='block';
        document.body.style.cursor = 'wait';

      //debugger;
      //, desc
      //var bar1= createBar(300,15,'white',1,'black','blue',85,7,3,"");
      //check order (ASC or DESC)
        if(oldCol==n)
        {
            desc=1;
        }else
        {
            desc=0;
    
        }
        //remember old column
        oldCol=n;
        parent = document.getElementById("offTblBdy");
        col = n;

        /*  if(parent.nodeName != "TBODY")
          parent = parent.getElementsByTagName("TBODY")[0];
        if(parent.nodeName != "TBODY")
          return false;
        */
        items = parent.getElementsByTagName("TR");
        N = items.length;
        //setInterval("quicksort(0," + N + "," + desc + ")",1000);
        // quick sort
        quicksort(0, N, desc);
        var alternateFlag = 1;
		for(i=0;i<N;i++)
		{
		    if(alternateFlag)
		    {
                  parent.rows[i].className="lwAlternate";
                  alternateFlag = 0;
             }
             else
             {
                  parent.rows[i].className="lwBackground";
                  alternateFlag = 1;

             }
        }
        document.getElementById('TabContainer1_TabPanel5_UpdateProgress2').style.display='none';
                       
        document.body.style.cursor = 'default';
                      
    }


    </script>
    <!-- setSubject function -->
   <script type="text/javascript" language="javascript">
    
    function setSubject(familyDD)
    {
   
        var subjectDD = null;
        var catalogdd = document.getElementById('TabContainer1_TabPanel2_ddlCatalogList');
        while(catalogdd.options.length > 0)
        {
            catalogdd.options[0] = null;
        }
        
        catalogdd.options[catalogdd.length] = new Option("�� ��������","*");
        for (var x=0;x<areaVSsubject.length;x++)
        {
            
            if (familyDD.options[familyDD.selectedIndex].value == areaVSsubject[x][2])
            {
                catalogdd.options[catalogdd.length] = new Option( areaVSsubject[x][0], areaVSsubject[x][1]);
               }
            else if(familyDD.selectedIndex==0)
            {
                catalogdd.options[catalogdd.length] = new Option( areaVSsubject[x][0], areaVSsubject[x][1]);
            }
            
        }  
//        var rdb = document.getElementById('TabContainer1_TabPanel2_rdbAllCatalogs');
//        
//        rdb.checked=false;
        
        ClearAllCheckbox();
    }
    
    function setCombos()
    {
        var catalogdd = document.getElementById('TabContainer1_TabPanel2_ddlCatalogList');
        var familydd = document.getElementById('TabContainer1_TabPanel2_ddlFamily');
        catalogdd.selectedIndex=0; 
        familydd.selectedIndex=0;
        setSubject(familydd);
    }
    
    function ClearAllCheckbox()
    {
        var rdb = document.getElementById('TabContainer1_TabPanel2_rdbAllCatalogs');
        var catalogdd = document.getElementById('TabContainer1_TabPanel2_ddlCatalogList');
        var familydd = document.getElementById('TabContainer1_TabPanel2_ddlFamily');
        if (catalogdd.selectedIndex>0 || familydd.selectedIndex>0)
        {
            rdb.checked=false;
            //alert();
        }
    }
    
//    function ResetSearchCombo()
//    {
//         var rdb = document.getElementById('TabContainer1_TabPanel2_rdbAllCatalogs');
//         var catalogdd = document.getElementById('TabContainer1_TabPanel2_ddlCatalogList');
//         var familydd = document.getElementById('TabContainer1_TabPanel2_ddlFamily');
//         if (rdb.checked==true)
//         {
//            catalogdd.selectedIndex=0;
//            familydd.selectedIndex=0;
//         }
//    }
    function checkEnter()	
    {
		if (window.event.keyCode==13)	
		{
		    var i=0;
		    for(i=0 ; i<document.getElementById('TabContainer1').lastChild.children.length ; i++)
		    {
		        if(document.getElementById('TabContainer1').lastChild.children[i].style.visibility=="visible")
		        {
		            document.getElementById(document.getElementById('TabContainer1').lastChild.children[i].id +'_btnSearchCatalog').click();
		        }
		    }    
			
		}
	}
	function isNumeric(input)
	{   
	    return ((input - 0) == input && input.length > 0);
	}
	function ResetView()
	{
	    
	    var displayState='block';
	    var treeViewId='TabContainer1_TabPanel1_FamilyTreeView';
	    var treeView = document.getElementById(treeViewId);
        var oDG = document.getElementById ("TabContainer1_TabPanel1_CatalogsDataGrid");
        var oFN = document.getElementById ("TabContainer1_TabPanel1_lblFamilyName");
        var oOpenMsg = document.getElementById ("TabContainer1_TabPanel1_lblOpenMsg");
        
        var v;
        if(treeView)
        {
            var treeLinks = treeView.getElementsByTagName('a');
            var nodeCount = treeLinks.length;
            var flag = true;
            for(i=0;i<nodeCount;i++)
            {
                if(treeLinks[i].firstChild.tagName)
                {
                    if(treeLinks[i].firstChild.tagName.toLowerCase() == 'img')
                    {
                        var node = treeLinks[i];
                        
                        for(v=node.id.length-1;v>1;v--)
                        {
                            if (isNumeric(node.id.substr(v,1))==false)
                            {
                                break;
                            }
                        }
                        v++;
                        //alert(node.id.substr(v,node.id.length-v));
                        var level = parseInt(node.id.substr(v,node.id.length-v));
                        //var level = parseInt(node.id.substr(treeLinks[i].id.length-1,1));
                        var childContainer = GetParentByTagName('table', node).nextSibling;
                        //var childContainer = document.getElementById("TabContainer1_TabPanel1_FamilyTreeViewn" + i + "Nodes");
                        if (level>=1)
                        {
                            if(flag)
                            {
                                if(childContainer.style.display == displayState)
                                {
                                    TreeView_ToggleNode(eval(treeViewId +'_Data'),level,node,' ',childContainer);
                                }
                                flag = false;
                            }
                            else
                            {
                                if(childContainer.style.display == displayState)
                                    TreeView_ToggleNode(eval(treeViewId +'_Data'),level,node,'',childContainer);
                            }
                        }      
                    }
                }
            }//for loop ends
            
        }
	    oFN.style.display="none";
        oDG.style.display="none";
        oOpenMsg.style.display="block";
	}
	////////
	
    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj)
    {
        var parent = childElementObj.parentNode;
        while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
        {
            parent = parent.parentNode;
        }
        return parent;
    }
	/////////onload="ShowAllCatalogTable()"
    </script>
        
    <title>������ ����� ���� ��������</title>
</head>

<body style="padding-left: 0; padding-top: 0; padding-right: 0;">
    <form id="form1" runat="server">
        <table width="100%" style="vertical-align: top" dir="rtl">
            <tr>
                <td style="width: 100%; vertical-align: top">
                    <table width="100%" cellpadding="0" cellspacing="0" bgcolor="#c3ca8c">
                        <tr>
                            <td align="right">
                                <img alt="" src="Images/sitelogo.jpg" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top" align="center">
                <td style="height: 15px;" align="left">
                    <nobr>
                    <asp:Label ID="lblCount" style="display:none" runat="server"  Width="1%">0</asp:Label>
                    <asp:Label ID="lblError" runat="server"  Font-Size="11px" Font-Names="Arial"  Width="59%"></asp:Label>
                    </nobr>
                </td>
            </tr>
        </table>
        
        <table width="100%">
            <tr>
                <td valign="top" width="100%">
                <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeOut="0"></ajaxToolkit:ToolkitScriptManager>
                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="ActiveTabChanged">
                    
                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" OnClientClick="ResetView"  HeaderText="<img src='images/home.gif' width='20'/><span style='font-size:14px;'>�� ����</span>"
                            Height="30px">
                            <ContentTemplate>
                                <!-- Tree view begin -->
                                <table width="100%">
                                    <tr class="lwBackground">
                                        <td colspan="2" valign="top" align="right">
                                        </td>
                                        <!--================= Start News ====================================-->
                                        <td rowspan="3" valign="top">
                                            <table width="180">
                                                <tr>
                                                    <td style="width: 100%; border: solid 1px #6C7444; border-bottom-width: 0px;">
                                                        <table border="0" width="100%" style="height: 250px" align="right" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr valign="top">
                                                                <td style="width: 100%" align="right">
                                                                    <table width="100%" border="0" align="center" cellspacing="0" cellpadding="2">
                                                                        <tr class="lwHeader">
                                                                            <td align="center">
                                                                                <a href="JavaScript:openNews()"><span style="font-size: 16px; color: white"><b>������</b></font></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <marquee id="marq2" direction="up" height="130" scrolldelay="80" scrollamount="1"
                                                                        onmouseover="marq2.stop();" onmouseout="marq2.start();">
					                                                                         <asp:Label id="lblNews" runat="server"  />
					                                                            </marquee>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" style="border: solid 1px #6C7444;">
                                                        <table border="0" width="100%" style="height: 300" align="right" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr valign="top">
                                                                <td width="100%" align="center">
                                                                    <table width="100%" border="0" align="center" cellspacing="0" cellpadding="2">
                                                                        <tr class="lwHeader">
                                                                            <td align="center">
                                                                                <a href="JavaScript:openLatestCatalogs()"><span style="font-size: 16px; color: white">
                                                                                    <b>������� ����� ���� </b></font></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <marquee id="marq1" direction="up" height="130" scrolldelay="80" scrollamount="1"
                                                                        onmouseover="marq1.stop();" onmouseout="marq1.start();">
					                                                                    <asp:Label id="lblNewCatalogs" runat="server"   />
					                                                            </marquee>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <!--================= END News ### Start Family Treeview ====================================-->
                                    </tr>
                                    <tr>
                                        <td width="230px" valign="top">
                                            <asp:TreeView Font-Bold="true" ID="FamilyTreeView" runat="server" Width="100%" ImageSet="Simple"
                                                AutoGenerateDataBindings="False" autoPostBack="False">
                                                <ParentNodeStyle Font-Bold="true" />
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"
                                                    ForeColor="#5555DD" />
                                                <NodeStyle Font-Names="verdana" Font-Size="20px" ForeColor="Black" HorizontalPadding="0px"
                                                    NodeSpacing="0px" VerticalPadding="0px" />
                                            </asp:TreeView><br><a href="mrts/index.htm" target="_blank">������� ����� ����</a>
                                        </td>
                                        <td width="70%" valign="top">
                                            <asp:Label ID="lblOpenMsg" runat="server" Width="100%"></asp:Label>
                                            <div class="lwBackground" style="width: 100%; text-align: center;">
                                                <asp:Label ID="lblFamilyName" Style="display: none" Font-Size="14pt" Width="100%"
                                                    runat="server"></asp:Label>
                                            </div>
                                            <asp:DataGrid ID="CatalogsDataGrid" runat="server" AllowPaging="False" AllowSorting="False"
                                                AutoGenerateColumns="False" Font-Size="10pt" PageSize="20" Width="100%" CssClass="lwAlternate lwBorder"
                                                BorderWidth="1px" CellPadding="2" GridLines="None" Style="display: none">
                                                <SelectedItemStyle />
                                                <AlternatingItemStyle CssClass="lwBackground" />
                                                <HeaderStyle CssClass="lwHeader" Font-Bold="True" HorizontalAlign="Right" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="�����">
                                                        <ItemStyle HorizontalAlign="right" Width="110px" />
                                                        <ItemTemplate>
                                                            <%# Container.DataItem("PCATALOGMAKAT").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="�� �����">
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <ItemTemplate>
                                                            <a target="_blank" href="Templates/ShowTemplate.aspx?Template=<%# Container.DataItem("PINDEXTEMPLATE")%>&amp;Pkey=<%# Container.DataItem("PKEY") %>&amp;ParentKey=r&amp;Type=1&amp;PkeyCatalog=<%# Container.DataItem("PKEY") %>">
                                                                <%# Container.DataItem("PHEBDESC") %>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="�����">
                                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                        <ItemTemplate>
                                                            <%# GetClassification(Container.DataItem("PCATALOGSIVUG"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="����� ������">
                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                        <ItemTemplate>
                                                            <%# Container.DataItem("PLASTUPDATEDATEFORMAT")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="�����">
                                                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                        <ItemTemplate>
                                                            <%#Container.DataItem("PFAMILY")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle BackColor="PaleGoldenrod" HorizontalAlign="Center" ForeColor="DarkSlateBlue"
                                                    Mode="NumericPages" />
                                                <FooterStyle CssClass="lwHeader" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" style="height: 21px">
                                        </td>
                                        <td width="80%" valign="top" align="center" style="height: 21px">
                                            <asp:LinkButton ID="lnbShowUpdateDate" runat="server" OnClick="lnbShowUpdateDate_Click"
                                                Visible="false">��� ������ �����</asp:LinkButton></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <!--======================= Search Part TAB========================================================-->
                        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="<img src='images/search.ico' width='14'/><span style='font-weight:bold;font-size:14px;'>����� ����</span>">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel1" runat="server" CssClass="lwBackground lwBorder" Height="100%"
                                            Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="100%" align="right">
                                                        <table width="40%" style="font-family: Arial; font-weight: bold; font-size: 9pt;">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;<asp:RadioButton ID="rdbDetail" runat="server" Checked="True" GroupName="GrRdbPart"
                                                                                    Text="����" OnCheckedChanged="rdbDetail_CheckedChanged" AutoPostBack="True" /></td>
                                                                            <td>
                                                                                <asp:RadioButton ID="rdbPicture" runat="server" GroupName="GrRdbPart" Text="�����"  Checked="False"
                                                                                    OnCheckedChanged="rdbPicture_CheckedChanged" AutoPostBack="True" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 77px" align="left">
                                                                                �� :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txbName" runat="server"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 77px" align="left">
                                                                                ��"� :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txbMakat" runat="server"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 77px" nowrap="nowrap" align="left">
                                                                                ���� ���� :</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txbProduce" runat="server"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    ��� �<img align="absmiddle" src="Templates/arrow_previous.gif" /></td>
                                                                <td valign="top" style="width: 462px">
                                                                    <table style="width: 224px">
                                                                        <tr>
                                                                            <td style="width: 40px">
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="rdbAllCatalogs" Text="�� ��������"  GroupName="grAllCatalogs"
                                                                                     runat="server"  Onclick="javascript:setCombos();" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 40px">
                                                                            </td>
                                                                            <td>
                                                                                �����<br />
                                                                                <asp:DropDownList ID="ddlFamily" runat="server" Width="350px" onchange="javascript:setSubject(this);" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 40px">
                                                                            </td>
                                                                            <td>
                                                                                �����<br />
                                                                                <asp:DropDownList ID="ddlCatalogList" runat="server"  Width="350px" onchange="javascript:ClearAllCheckbox();" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:Button ID="btnSearch" OnClientClick="return SearchValidator();" runat="server"
                                                                        Text="���" Width="80px" />
                                                                    <asp:Button ID="btnClear" runat="server" Text="���" Width="80px" OnClientClick="return ClearResults();"  />
                                                                    <asp:Label ID="lblLoadWait" runat="server" Visible="False" Width="100"  />
																	</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td> <td>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                        <ProgressTemplate>
                                                                            <asp:Label ID="lblLoad" runat="server"> </asp:Label>
                                                                            <img alt="" src="images/ajax-loader.gif" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" dir="rtl">
                                                        <asp:DataGrid OnItemDataBound="boundPartItems" ID="PartSearchDataGrid" runat="server"
                                                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="lwAlternate lwBorder"
                                                            Font-Size="10pt" Width="100%" Visible="False" BorderWidth="1px" CellPadding="2"
                                                            Font-Names="Arial" GridLines="None" PageSize="100" PagerStyle-Font-Overline="true">
                                                            <SelectedItemStyle />
                                                            <AlternatingItemStyle CssClass="lwBackground " />
                                                            <HeaderStyle ForeColor="white" CssClass="lwHeader" Font-Bold="True" HorizontalAlign="Right" />
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="�� ���" SortExpression="PHEBDESC">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <a target="_blank" href="onlinesearchredirect.aspx?ppart=<%# Container.DataItem("PPARTKEY") %>&Pkey=<%# Container.DataItem("PPARENTKEY") %>&amp;ParentKey=<%# Container.DataItem("PPARENTKEY") %>&amp;PkeyCatalog=<%# Container.DataItem("CATALOGKEY") %>&amp;Type=<%# Container.DataItem("POBJECTTYPE") %>">
                                                                            <%#Container.DataItem("PHEBDESC")%>
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="�����" SortExpression="PMAKAT">
                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PartMakatLbl" runat="server" Text='<%#Container.DataItem("PMAKAT").ToString() %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="���� ����" SortExpression="PVENDOR_NUMBER">
                                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVendor" runat="server" Text='<%# Container.DataItem("PVENDOR_NUMBER")%>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="�� �����" SortExpression="PAGEDESC">
                                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblClassification" runat="server" Text='<%# Container.DataItem("PAGEDESC")%>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="���� �����" SortExpression="PPAGENUMBER">
                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPageNumber" runat="server" Text='<%# Container.DataItem("PPAGENUMBER")%>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="�����" SortExpression="CATALOGDESC">
                                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# Container.DataItem("CATALOGDESC")%>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                            <PagerStyle ForeColor="White" CssClass="lwHeader textLTR" HorizontalAlign="Center" Mode="NumericPages" />
                                                            <FooterStyle CssClass="lwHeader"   />
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <!--======================= Search CATALOG TAB========================================================-->
                        <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="<img src='images/search.ico' width='14'/><span  style='font-weight:bold;font-size:14px;'>����� �����</span>">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table style="font-weight: bold; font-size: 9pt; font-family: Arial" width="100%">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:Panel ID="Panel2" runat="server" CssClass="lwBackground lwBorder" Height="100%"
                                                        Width="100%">
                                                        <table id="TABLE1" width="100%">
                                                            <tr>
                                                                <td align="right" colspan="2" style="width: 20%; height: 20px">
                                                                </td>
                                                                <td align="right" style="width: 80%; height: 20px">
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" width="10%">
                                                                </td>
                                                                <td align="left" style="height: 22px" width="10%">
                                                                    �� ����� :
                                                                </td>
                                                                <td align="right" width="80%">
                                                                    <asp:TextBox ID="txbCatalogName" runat="server" onKeyPress="javascript:return checkEnter(event);"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%">
                                                                </td>
                                                                <td align="left" style="height: 22px" width="10%">
                                                                    ��"� :
                                                                </td>
                                                                <td align="right" width="80%">
                                                                    <asp:TextBox ID="txbCatalgogMakat" runat="server"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="top" width="10%">
                                                                    &nbsp;</td>
                                                                <td align="left" valign="top" width="10%">
                                                                    ����� :
                                                                </td>
                                                                <td align="right" width="80%">
                                                                    <asp:DropDownList ID="ddlFamilySelect" Width="155" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <asp:UpdateProgress ID="UpdateProgressCatalog" runat="server">
                                                                        <ProgressTemplate>
                                                                            &nbsp;<img alt="" src="images/ajax-loader.gif" />
                                                                            <asp:Label ID="lblLoad" runat="server"></asp:Label>&nbsp;
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btnSearchCatalog" runat="server" Text="��� �����"
                                                                        Width="111px" OnClick="btnSearchCatalog_Click" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblLoadWaitCat" runat="server" Visible="False"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="3">
                                                                    <asp:DataGrid ID="CatalogSearchDataGrid" runat="server" AllowPaging="False" AllowSorting="True"
                                                                        AutoGenerateColumns="False" CssClass="lwAlternate lwBorder" BorderWidth="1px"
                                                                        CellPadding="2" GridLines="None" PageSize="20" Width="100%" OnItemDataBound="dgBoundItems">
                                                                        <SelectedItemStyle BackColor="PaleTurquoise" ForeColor="DarkSlateGray" />
                                                                        <AlternatingItemStyle CssClass="lwBackground" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <HeaderStyle CssClass="lwHeader" ForeColor="white" Font-Bold="True" HorizontalAlign="Right" />
                                                                        <Columns>
                                                                            <asp:TemplateColumn HeaderText="�����" SortExpression="PCATALOGMAKAT">
                                                                                <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="MakatLbl" runat="server" Text='<%# Container.DataItem("PCATALOGMAKAT")%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="�� �����" SortExpression="PHEBDESC">
                                                                                <ItemTemplate>
                                                                                    <a href='Templates/ShowTemplate.aspx?Template=<%# Container.DataItem("PINDEXTEMPLATE")%>&amp;Pkey=<%# Container.DataItem("PKEY") %>&amp;ParentKey=r&amp;Type=1&amp;PkeyCatalog=<%# Container.DataItem("PKEY") %>'
                                                                                        target="_blank">
                                                                                        <%# Container.DataItem("PHEBDESC") %>
                                                                                    </a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="�����" SortExpression="PCATALOGSIVUG">
                                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClassification" runat="server" Text='<%# GetClassification(Container.DataItem("PCATALOGSIVUG"))%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="�����" SortExpression="PFAMILY">
                                                                                <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFamilyClassification" runat="server" Text='<%# Container.DataItem("PFAMILY")%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="����� ������" SortExpression="PLASTUPDATEDATEFORMAT">
                                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUpdateDate" runat="server" Text='<%# Container.DataItem("PLASTUPDATEDATEFORMAT")%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center"
                                                                            Mode="NumericPages" />
                                                                        <FooterStyle CssClass="lwHeader" />
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        
                        <!--========================Search Text TAB =========================================================-->
                         <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="<img src='images/search.ico' width='14'/><span  style='font-weight:bold;font-size:14px;'>����� �����</span>">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                    <table style="font-weight: bold; font-size: 9pt; font-family: Arial" width="100%">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:Panel ID="Panel3" runat="server" CssClass="lwBackground lwBorder" Height="100%"
                                                        Width="100%">
                                                        <table id="TABLE2" width="100%">
                                                            <tr>
                                                                <td align="right" colspan="2" style="width: 20%; height: 20px">
                                                                </td>
                                                                <td align="right" style="width: 80%; height: 20px">
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" width="10%">
                                                                </td>
                                                                <td align="left" style="height: 22px" width="10%">
                                                                   ���� ������ :
                                                                </td>
                                                                <td align="right" width="80%">
                                                                    <asp:TextBox ID="txtSearchKeyword" runat="server" onKeyPress="javascript:return checkEnter(event);"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server">
                                                                        <ProgressTemplate>
                                                                            &nbsp;<img alt="" src="images/ajax-loader.gif" />
                                                                            <asp:Label ID="lblLoad" runat="server"></asp:Label>&nbsp;
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btnSearchText" runat="server" Text="���"
                                                                        Width="111px" OnClick="btnSearchText_Click" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblLoadWaitText" runat="server" Visible="False"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="3">
                                                                    <!--=====================================================-->
                                                                    <asp:DataGrid ID="textSearchDataGrid" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AutoGenerateColumns="False" CssClass="lwAlternate lwBorder" BorderWidth="1px"
                                                                        CellPadding="2" GridLines="None" PageSize="50" Width="100%" OnItemDataBound="dgBoundItems">
                                                                        <SelectedItemStyle BackColor="PaleTurquoise" ForeColor="DarkSlateGray" />
                                                                        <AlternatingItemStyle CssClass="lwBackground" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <HeaderStyle CssClass="lwHeader" ForeColor="white" Font-Bold="True" HorizontalAlign="Right" />
                                                                        <Columns>
                                                                            
                                                                            <asp:TemplateColumn HeaderText="�� ���" SortExpression="PPAGENAME">
                                                                                <ItemTemplate>
                                                                                    <a href='Templates/ShowTemplate.aspx?Template=<%# Container.DataItem("PPAGETEMPLATE")%>&amp;Pkey=<%# Container.DataItem("pkeypage") %>&amp;ParentKey=<%# Container.DataItem("pkeychapter") %>&amp;Type=2&amp;PkeyCatalog=<%# Container.DataItem("PKEY") %>'
                                                                                        target="_blank">
                                                                                        <%# Container.DataItem("PPAGENAME") %>
                                                                                    </a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="�� �����" SortExpression="PCHAPTERNAME">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItem("PCHAPTERNAME") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            
                                                                            <asp:TemplateColumn HeaderText="�� ������" SortExpression="PPAGENAME">
                                                                                <ItemTemplate>
                                                                                    <a href='Templates/ShowTemplate.aspx?Template=<%# Container.DataItem("PINDEXTEMPLATE")%>&amp;Pkey=<%# Container.DataItem("PKEY") %>&amp;ParentKey=r&amp;Type=1&amp;PkeyCatalog=<%# Container.DataItem("PKEY") %>'
                                                                                        target="_blank">
                                                                                        <%# Container.DataItem("PCATALOGNAME") %>
                                                                                    </a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center"
                                                                            Mode="NumericPages" />
                                                                        <FooterStyle CssClass="lwHeader" />
                                                                    </asp:DataGrid>
                                                                    
                                                                    <!--=======================================================-->
                                                                </td>
                                                            
                                                            </tr>
                                                            </table>
                                                            </asp:Panel>
                                                            </td>
                                                            </tr>
                                                            </table>
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            </ContentTemplate>
                             <HeaderTemplate>
                                 <img src="images/search.ico" width="14" /><span style="font-weight: bold; font-size: 14px">�����
                                     ����</span>
                             </HeaderTemplate>
                                                            </ajaxToolkit:TabPanel>
                        
                        <!--======================= All CATALOGS TAB ========================================================-->
                        <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="<span style='font-size:14px;'>��� ��������</span>">
                            
                            <ContentTemplate>
                                <span dir="rtl" id="UpdateProgress2" name="UpdateProgress2" runat="server" style="display: none">
                                    &nbsp;<img alt="" src="images/ajax-loader.gif" />
                                    ����� ...&nbsp; </span><span>����� ��� �� ����� ������</span>
                                
                                <asp:DataGrid  ID="AllCatalogsDataGrid" AllowSorting="true" Runat="server" 
                                AutoGenerateColumns="False" CssClass="lwBorder lwAlternate" BorderWidth="1px" 
                                CellPadding="2" ForeColor="Black" GridLines="None" Width="100%" OnItemDataBound="dgBoundItems" >
                                    <SelectedItemStyle BackColor="PaleTurquoise" ForeColor="DarkSlateGray" />
                                    <AlternatingItemStyle CssClass="lwBackground" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="lwHeader" ForeColor="white" Font-Bold="True" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="�����" SortExpression="PCATALOGMAKAT">
                                            <ItemStyle HorizontalAlign="right" Width="120px" />
                                            <ItemTemplate>
                                                <asp:Label ID="MakatLbl" runat="server" Text='<%# Container.DataItem("PCATALOGMAKAT")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn  HeaderText="�� �����" SortExpression="PHEBDESC">
                                            <ItemTemplate>
                                                <a href='Templates/ShowTemplate.aspx?Template=<%# Container.DataItem("PINDEXTEMPLATE")%>&amp;Pkey=<%# Container.DataItem("PKEY") %>&amp;ParentKey=r&amp;Type=1&amp;PkeyCatalog=<%# Container.DataItem("PKEY") %>'
                                                    target="_blank">
                                                    <%# Container.DataItem("PHEBDESC") %>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="�����" SortExpression="PCATALOGSIVUG">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblClassification" runat="server" Text='<%# GetClassification(Container.DataItem("PCATALOGSIVUG"))%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="�����" SortExpression="PFAMILY">
                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFamilyClassification" runat="server" Text='<%# Container.DataItem("PFAMILY")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="����� ������" SortExpression="PCATDATE" visible="false" >
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpdateDate" runat="server" Text='<%# Container.DataItem("PLASTUPDATEDATEFORMAT")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                
                                </asp:DataGrid>
                                <asp:LinkButton ID="lblShowHide" Text="��� ������ �����" runat="server" onClick="ShowHideDates"  ></asp:LinkButton>
                                
                                <!--<a id="ShowUpdateDate" href="javascript:ShowHideUpdateDateNew('TabContainer1_TabPanel5_AllCatalogsDataGrid',4);">��� ������ �����</a>-->
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <!--======================= CATALOG PRINTING TAB========================================================-->
                        <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="<span  style='font-size:14px;'>������� ������</span>">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="100%"></asp:Label>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                            <ProgressTemplate>
                                                &nbsp;<img alt="" src="images/ajax-loader.gif" />
                                                <asp:Label ID="lblLoad" runat="server"></asp:Label>&nbsp;
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:DataGrid ID="PrintCatalogDataGrid" runat="server" AllowPaging="False" AllowSorting="True"
                                            AutoGenerateColumns="False" PageSize="100" Width="100%" CssClass="lwBorder lwAlternate"
                                            BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                                            <SelectedItemStyle />
                                            <AlternatingItemStyle CssClass="lwBackground" />
                                            <HeaderStyle CssClass="lwHeader" ForeColor="white" Font-Bold="True" HorizontalAlign="Right" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="�����" SortExpression="CATALOGMAKAT">
                                                    <ItemStyle HorizontalAlign="right" Width="120px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="MakatLbl" runat="server" Text='<%# Container.DataItem("CATALOGMAKAT")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="">
                                                    <ItemStyle HorizontalAlign="center" Width="10px" />
                                                    <ItemTemplate>
                                                        <img alt="" src="<%# GetImage(Container.DataItem("PFILEEXTENSION"))%>" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="�� ������" SortExpression="CATALOGNAME">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <ItemTemplate>
                                                        <a href="javascript:OpenDoc('<%# Container.DataItem("FILENAME") %>')">
                                                            <%# Container.DataItem("CATALOGNAME") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="����� �����" SortExpression="PFILEDATE">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# Container.DataItem("PFILECREATEDDATE")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle CssClass="lwHeader" HorizontalAlign="Center" ForeColor="DarkSlateBlue"
                                                Mode="NumericPages" />
                                            <FooterStyle CssClass="lwHeader" />
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <!--======================= USER GUIDE TAB========================================================-->
                        <ajaxToolkit:TabPanel ID="TabPanel10" runat="server" HeaderText="<span  style='font-size:14px;'>����� ������</span>">
                            <ContentTemplate>
                                <iframe src="help/mainsitehelp.htm" frameborder="0" width="100%" height="400"></iframe>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <!--======================= ADMIN TAB========================================================-->
                        <ajaxToolkit:TabPanel ID="TabPanelAdmin" runat="server" HeaderText="<span style='font-size:14px;'>���� �����</span>">
                            <ContentTemplate>
                                <asp:Button ID="hlnkAdminEdit" Text="����� �����" runat="server" Font-Underline="True"
                                    Font-Names="Arial" ForeColor="Blue" OnClientClick="window.open('Admin/AdminNews.aspx')"
                                    ToolTip="����� ����� ������ �����" Visible="False" Font-Bold="True" />
                                <asp:Button ID="hlnkPrintedCatalogs" Text="������� ������" runat="server" Font-Underline="True"
                                    Font-Names="Arial" ForeColor="Blue" OnClientClick="window.open('Admin/PrintedCatalogs.aspx')"
                                    ToolTip="����� ������� ������  " Visible="False" Font-Bold="True" />
								<asp:Button ID="hlnkStatistics" Text="����������" runat="server" Font-Underline="True"
                                    Font-Names="Arial" ForeColor="Blue" OnClientClick="window.open('Admin/Statistics.aspx')"
                                    ToolTip="����������" Visible="False" Font-Bold="True" />  
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </td>
            </tr>
        </table>
    </form>
    
</body>
</html>


