﻿Public Class Login_new
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            Exit Sub
        End If
        If Session("prv") Is Nothing Then
            Session.Abandon()
        End If
        'REVISAR SI EXISTE EL COOKIE QUE GUARDA EL USUARIO Y CONTRASEÑA
        If Not (Request.Cookies("UNA") Is Nothing) Then
            TXTUSR.Text = Request.Cookies("UNA").Value
            TXTPWD.Focus()
        Else
            TXTUSR.Focus()
        End If
    End Sub

    Private Sub BTNLOGIN_Click(sender As Object, e As EventArgs) Handles BTNLOGIN.Click
        TXTUSR.Text = LCase(TXTUSR.Text)
        AbrirRecordsetData("EXEC SEGURIDAD 4,'ADMINISTRADOR','" & TXTUSR.Text & "','" & TXTPWD.Text & "',''")
        If nA(0) = 0 Then
            'ERRMSG.Text = nA(1)
            mensaje(nA(1).ToString)
            Exit Sub
        End If
        USR = "ADMINISTRADOR"
        STA = "CONECTADO"
        NOM = nA(2)
        UCO = nA(3)
        CODEMPSAP = nA(4)
        TRM = ""
        Session.Add("BD_NOMINA", "1")
        Session.Add("USR", USR)
        Session.Add("NOM", NOM)
        Session.Add("TRM", TRM)
        Session.Add("UCO", UCO)
        Session.Add("CODEMPSAP", CODEMPSAP)
        Session.Add("EML", TXTUSR.Text)
        'COOKIE
        If Request.Cookies("UNA") Is Nothing Then
            Dim nCookie As New HttpCookie("UNA")
            nCookie.Value = TXTUSR.Text
            nCookie.Expires = Date.Now.AddDays(7)
            Response.Cookies.Add(nCookie)
        Else
            Response.Cookies("UNA").Value = TXTUSR.Text
            Response.Cookies("UNA").Expires = Date.Now.AddDays(7)
        End If
        'Response.Redirect("Dashboard.aspx")
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub TXTPWD_TextChanged(sender As Object, e As EventArgs) Handles TXTPWD.TextChanged
        BTNLOGIN_Click(BTNLOGIN, e)
    End Sub

    Public Sub mensaje(ByRef mensaje As String)
        Dim script As String = "alert('" + mensaje + "');"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, True)
    End Sub

End Class