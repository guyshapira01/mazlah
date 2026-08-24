Function makeMsgBox(title,message,icon,buttons,defButton,mode)
   butVal = icon + buttons + defButton + mode
   makeMsgBox = MsgBox(message,butVal,title)
End Function

Function PutComboFromLOV(sLOV,sComboName,sDefaultValue) 
	
	Dim temparr
	Dim j
	Dim strReturnString
	
	 temparr = Split(sLOV, "|")
	 strReturnString = "<SELECT NAME=""" & sComboName & """>"
	 strReturnString = strReturnString & "<OPTION VALUE=''></OPTION>"
	 
	 For j = 0 To UBound(temparr)
		If temparr(j)=sDefaultValue Then
			strReturnString = strReturnString & "<OPTION SELECTED VALUE='" & temparr(j) & "'>" & temparr(j) & "</OPTION>"
		Else
			strReturnString = strReturnString & "<OPTION VALUE='" & temparr(j) & "'>" & temparr(j) & "</OPTION>"
		End If
	 Next

	strReturnString = strReturnString & "</SELECT>"

	PutComboFromLOV = strReturnString
	
End Function

Function PutComboFromLOVWithExtraProperties(sLOV,sComboName,sDefaultValue, sSelectExtraProperties) 
	
	Dim temparr
	Dim j
	Dim strReturnString
	
	 temparr = Split(sLOV, "|")
	 strReturnString = "<SELECT NAME=""" & sComboName & """ " & sSelectExtraProperties & ">"
	 strReturnString = strReturnString & "<OPTION VALUE=''></OPTION>"
	 
	 For j = 0 To UBound(temparr)
		If temparr(j)=sDefaultValue Then
			strReturnString = strReturnString & "<OPTION SELECTED VALUE='" & temparr(j) & "'>" & temparr(j) & "</OPTION>"
		Else
			strReturnString = strReturnString & "<OPTION VALUE='" & temparr(j) & "'>" & temparr(j) & "</OPTION>"
		End If
	 Next

	strReturnString = strReturnString & "</SELECT>"

	PutComboFromLOVWithExtraProperties = strReturnString
	
End Function



Function PutComboFromLOVWithOnchange(sLOV,sComboName,sDefaultValue,Onchange) 
	
	Dim temparr
	Dim j
	Dim strReturnString
	
	 temparr = Split(sLOV, "|")
	 strReturnString = "<SELECT NAME=""" & sComboName & """ " & Onchange & ">"
	 strReturnString = strReturnString & " " & Onchange

	 strReturnString = strReturnString & "<OPTION VALUE=''></OPTION>"
	 
	 For j = 0 To UBound(temparr)
		If temparr(j)=sDefaultValue Then
			strReturnString = strReturnString & "<OPTION SELECTED VALUE='" & temparr(j) & "'>" & temparr(j) & "</OPTION>"
		Else
			strReturnString = strReturnString & "<OPTION VALUE='" & temparr(j) & "'>" & temparr(j) & "</OPTION>"
		End If
	 Next

	strReturnString = strReturnString & "</SELECT>"

	PutComboFromLOVWithOnchange = strReturnString
End Function
