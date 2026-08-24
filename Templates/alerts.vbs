<!-- Alerts Functions of Task -->
function ShowAlert_OLD(currentDate,DueDate,AlertDays,TDName)
TDName = "alertActivity" & TDName

					If isDate(currentDate) And isDate(DueDate) and isnumeric(AlertDays) Then
						currentDate = cdate(currentDate)
						DueDate = cdate(DueDate)

						if DateDiff("d",currentDate,DueDate) <= 0 then
							document.all(TDName).style.backgroundcolor="red"
						elseif DateDiff("d",currentDate+AlertDays,DueDate)<=0 then
							document.all(TDName).style.backgroundcolor="yellow"
						else
							document.all(TDName).style.backgroundcolor="green"
'						'alert(DateDiff("d",currentDate,DueDate))
'						'alert(AlertDays)
					end if
					End If
ShowAlert_OLD = TDName					
End function

<!-- Alerts Functions of Task -->
function ShowAlert(currentDate,DueDate,AlertDays,TDName,CurrentStatus)
TDName = "alertActivity" & TDName
					If isDate(currentDate) And isDate(DueDate) and isnumeric(AlertDays) Then
						currentDate = cdate(currentDate)
						DueDate = cdate(DueDate)

						if DateDiff("d",currentDate,DueDate) <= 0 then
							if CurrentStatus="Continual" or CurrentStatus="101937a1" then
								document.all(TDName).style.backgroundcolor="thistle"
							else
								document.all(TDName).style.backgroundcolor="red"
							end if
						elseif DateDiff("d",currentDate+AlertDays,DueDate)<=0 then
							document.all(TDName).style.backgroundcolor="yellow"
						else
							document.all(TDName).style.backgroundcolor="green"
'						'alert(DateDiff("d",currentDate,DueDate))
'						'alert(AlertDays)
					end if
					End If
ShowAlert = TDName					
End function

function CheckDate(currenvalue)
	If isDate(currenvalue) Then
		CheckDate = "1"
	else
		CheckDate = "0"
	End If
End function


function validateDate(Prop)
	if Prop.value <> ""	Then
		if isDate(Prop.value)	Then
			validateDate = True
		else
			alert("please fill valid Date format to the field.")
			Prop.focus()
			validateDate =  false
		End if
	End if
End function

function validateNum(Prop)
	if isnumeric(Prop.value)	Then
		validateNum = True
	else
		alert("please fill valid Numric value to the field.")
		Prop.focus()
		validateNum =  false
	End if
End function	

function FieldLegth(Prop,Len1)
	if len(Prop.value) <= Len1	Then
		FieldLegth = True
	else
		alert("Field is too long")
		Prop.focus()
		FieldLegth =  false
	End if
End function	




<!-- hide fields  of Task -->
function hidefields(TRName,Iid)
			
TRName = "hidef" & TRName

	document.all(TRName).style.display="none"
//	moredetailsbuttontitle.innerText="More details"
	Iid.src="WEImages/Arrow_d.gif"
	hidefields = TRName	
End function




<!-- Expand fields  of Task -->
function expandfields(TRName,Iid)
			
	TRName = "hidef" & TRName

	document.all(TRName).style.display=""
//	moredetailsbuttontitle.innerText="Hide details"
	Iid.src="WEImages/Arrow_u.gif"
	expandfields = TRName	
		
End function


<!-- hide fields -->
function hidefieldsmulty(TDName1)
	Dim i		
	For i=0 to document.all(TDName1).length-1
		document.all(TDName1)(i).style.display="NONE"
	Next
End function









<!-- Alerts Functions of Failure -->
function ShowAlertFailure(currentDate,FailureDate,AlertDays,TDName)
TDName = "alertFailure" & TDName

					If isDate(currentDate) And isDate(FailureDate) Then
						currentDate = cdate(currentDate)
						FailureDate = cdate(FailureDate)

						if DateDiff("d",currentDate-30,FailureDate) <= 0 then
							document.all(TDName).style.backgroundcolor="red"
					
'						'alert(DateDiff("d",currentDate,DueDate))
'						'alert(AlertDays)
						end if
					else
						document.all(TDName).style.backgroundcolor="red"
					End If
					
					
ShowAlertFailure = TDName					
End function




Function showClosedStatuses()
	
	if not document.all("StatusClosed") is nothing then
		'check if there is more then one record....
		Dim sShowClose
		Dim sHideClose
		Dim i		
		sShowClose = "+Show Closed"
		sHideClose = "-Hide Closed"
		if instr(1,typename(document.all("StatusClosed")),"Collection") then
			For i=0 to document.all("StatusClosed").length-1
				If (document.all("StatusClosed")(i).style.display="") then
					document.all("StatusClosed")(i).style.display="NONE"
					txtShow.innerText = sShowClose
				Else
					document.all("StatusClosed")(i).style.display=""
					txtShow.innerText = sHideClose
				End If
			Next
		Else
			If (document.all("StatusClosed").style.display="") then
				document.all("StatusClosed").style.display="NONE"
				txtShow.innerText = sShowClose
			Else
				document.all("StatusClosed").style.display=""
				txtShow.innerText = sHideClose
			End If
		end if
	end if

End Function


Function showHideParam(sShow,sHide,ObjectName)
	if not document.all("StatusClosed") is nothing then
		'check if there is more then one record....
		Dim i		
		if instr(1,typename(document.all("StatusClosed")),"Collection") then
			For i=0 to document.all("StatusClosed").length-1
				If (document.all("StatusClosed")(i).style.display="") then
					document.all("StatusClosed")(i).style.display="NONE"
				' txtShow.innerText = sShow
				document.all(ObjectName).Value = sShow				
				Else
					document.all("StatusClosed")(i).style.display=""
				' txtShow.innerText = sHide
				 document.all(ObjectName).Value = sHide
				End If
			Next
		Else
			If (document.all("StatusClosed").style.display="") then
				document.all("StatusClosed").style.display="NONE"
				' txtShow.innerText = sShow
				document.all(ObjectName).Value = sShow				
			Else
				document.all("StatusClosed").style.display=""
				' txtShow.innerText = sHide
				 document.all(ObjectName).Value = sHide
			End If
		end if
	end if

End Function