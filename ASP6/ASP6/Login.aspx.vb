'Author: Louis Andres
'Assignment: ASP6
'Date: 3/12/15
'Program Description: Search database

Public Class Login
    Inherits System.Web.UI.Page

    Dim EmployeeDB As New ClassEmployeeDB
    Dim Format As New ClassFormat
    Dim valid As New ClassValidate

    Dim mintCount As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'session count = 0
        If IsPostBack = False Then
            Session("Count") = 0
        End If


    End Sub



    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click


        If valid.CheckForDigits(txtEmpID.Text) = False Then
            lblMessage.Text = "Bad Username"
            'add session count
            SessionCounter()
            Exit Sub
        End If

        If EmployeeDB.CheckEmpIDandPasswordUsingSP(txtEmpID.Text, txtPassword.Text) = -2 Then
            lblMessage.Text = "Invalid Username"
            'add session count
            SessionCounter()
            Exit Sub
        End If


        If EmployeeDB.CheckEmpIDandPasswordUsingSP(txtEmpID.Text, txtPassword.Text) = -3 Then
            lblMessage.Text = "Bad Password"
            'add session count
            SessionCounter()
            Exit Sub
        End If

        'redirect page
        Response.Redirect("Search.aspx")
    End Sub

    Private Sub SessionCounter()
        'Purpose: session counter
        'Arguments: none
        'Returns: adds 1 to session
        'Author: Louis Andres
        'Date: 3/22/15

        mintCount = CInt(Session("count"))
        mintCount += 1
        Session("count") = mintCount
        If Session("Count") = 3 Then
            lblMessage.Text = "Too many failed attempts. Please try again later"
            btnLogin.Enabled = False
        End If
    End Sub


End Class