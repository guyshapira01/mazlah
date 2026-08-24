function checkpkey(pkey)
	dim temparr
	Dim MyCon
	Dim ADC

	
on error resume next

	Set MyCon = CreateObject("ADODB.Connection")

	mdbFileName="C:\Program Files\LinkWare\notes.mdb"
	MyConnectString = "DRIVER={Microsoft Access Driver (*.mdb)};DBQ=" & mdbFileName & ";"

	MyCon.Open MyConnectString

if err=0 then


	Set ADC = CREATEOBJECT("ADODB.RECORDSET")

	ADC.Open "select note_content from tbl_notes where note_pkey='" & pkey & "' and Note_Content<>''", MyCon, , , adOpenKeyset


	if not adc.eof then
		document.images("imgnote").src="EditNote.gif"
		document.images("imgnote").alt="Edit Remarks"
	else
		document.images("imgnote").src="AddNote.gif"
		document.images("imgnote").alt="Add Remarks"

	end if

	adc.close
	set adc=nothing
	MyCon.close
	set MyCon=nothing
else
	document.images("imgnote").style.display="none"
end if
End function
