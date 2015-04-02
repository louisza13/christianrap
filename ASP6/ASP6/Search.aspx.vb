'Author: Louis Andres
'Assignment: ASP6
'Date: 3/12/15
'Program Description: Search database

Public Class Search


    Inherits System.Web.UI.Page

    Dim CustDB As New ClassCustomerDB
    Dim valid As New ClassValidate
    Dim format As New ClassFormat

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' get all customers from database
        CustDB.GetAllCustomersUsingSP()

        SearchAndBind()


    End Sub

    Private Sub SearchAndBind()
        'Purpose: search and bind
        'Arguments: none
        'Returns: customer view and record count
        'Author: Louis Andres
        'Date: 3/22/15

        'sort from rad button
        CustDB.DoSort(radSort.SelectedItem.ToString)

        ' set up grid view
        gvCustomers.DataSource = CustDB.CustMyView
        gvCustomers.DataBind()

        'label
        lblRecords.Text = CustDB.CustMyView.Count.ToString
    End Sub

    Protected Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        lblMessage.Text = ""
        txtSearch.Text = ""
        SearchAndBind()
    End Sub

    Protected Sub btnState_Click(sender As Object, e As EventArgs) Handles btnState.Click
        lblMessage.Text = ""

        If valid.CheckState(txtSearch.Text) = False Then
            lblMessage.Text = "Please enter a 2 letter abbreviation"
            Exit Sub
        End If


        CustDB.GetAllCustomersUsingSP()

        CustDB.SearchByState(txtSearch.Text)

        SearchAndBind()

    End Sub

  

    Protected Sub btnLastname_Click(sender As Object, e As EventArgs) Handles btnLastname.Click
        lblMessage.Text = ""

        CustDB.GetAllCustomersUsingSP()

        CustDB.SearchByLastName(txtSearch.Text)

        SearchAndBind()
    End Sub

    Protected Sub btnCity_Click(sender As Object, e As EventArgs) Handles btnCity.Click
        lblMessage.Text = ""

        CustDB.GetAllCustomersUsingSP()

        CustDB.SearchByCity(txtSearch.Text)

        SearchAndBind()
    End Sub

    Protected Sub btnUsername_Click(sender As Object, e As EventArgs) Handles btnUsername.Click
        lblMessage.Text = ""

        CustDB.GetAllCustomersUsingSP()

        CustDB.SearchByUsername(txtSearch.Text)

        SearchAndBind()
    End Sub
End Class