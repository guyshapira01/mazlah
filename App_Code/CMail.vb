Imports System.Web.Mail

Public Class CMail

    Protected sSmtpServer As String = "smtp.bezeqint.net"

    Public Sub SendMail(ByVal _from As String, ByVal _to As String, ByVal _cc As String, ByVal _bcc As String, ByVal _subject As String, ByVal _body As String)

        Dim mailMsg As New MailMessage

        mailMsg.From = _from
        mailMsg.To = _to
        mailMsg.BodyFormat = MailFormat.Html
        'mailMsg.Cc = "cc@ccServer.com";
        'mailMsg.Bcc = "bcc@bccServer.com";
        mailMsg.Subject = _subject
        mailMsg.Body = _body
        SmtpMail.SmtpServer = sSmtpServer
        SmtpMail.Send(mailMsg)
    End Sub

End Class
