Imports System.Web.Security
Imports System.Security.Cryptography
Imports System.Text


Public Class ClsCrypto
    Dim myKey As String
    Dim des As New TripleDESCryptoServiceProvider
    Dim hashmd5 As New MD5CryptoServiceProvider


    Public Sub New()
        'Inserire codice di configurazione della classe 
        myKey = "LW2006License"
    End Sub


    Public Function clsCrypto(ByVal testo As String, ByVal Operazione As Boolean) As String
        If Operazione Then
            clsCrypto = Cifra(testo)
        Else
            clsCrypto = DeCifra(testo)
        End If
    End Function


    Private Function DeCifra(ByVal testo As String) As String
        des.Key = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        des.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = des.CreateDecryptor()
        Dim buff() As Byte = Convert.FromBase64String(testo)
        DeCifra = ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    End Function


    Private Function Cifra(ByVal testo As String) As String
        des.Key = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        des.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = des.CreateEncryptor()
        Dim MyASCIIEncoding = New ASCIIEncoding
        Dim buff() As Byte = ASCIIEncoding.ASCII.GetBytes(testo)
        Cifra = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    End Function


End Class


