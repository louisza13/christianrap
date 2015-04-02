Public Class Practice
    Inherits System.Web.UI.Page

    Dim VehicleDB As New ClassVehicleDB
    Dim Format As New ClassFormat
    Dim valid As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            'first time load data set
            VehicleDB.GetAllVehiclesUsingSP()

            'fill grid
            SortAndBind()
        End If

    End Sub



    Private Sub SortAndBind()
        'Purpose: search and bind
        'Arguments: none
        'Returns: Vehicle view and record count
        'Author: Louis Andres
        'Date: 3/22/15


        'session!!!
        Session("RowFilter") = VehicleDB.RowFilter

        'sort from rad button - order by
        VehicleDB.DoSort(radSort.SelectedItem.ToString)

        ' set up grid view
        gvVehicles.DataSource = VehicleDB.VehicleMyView
        gvVehicles.DataBind()

        'label count
        txtCount.Text = VehicleDB.VehicleMyView.Count.ToString


        

    End Sub


    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        'clear
        lblMessage1.Text = ""
        lblMessage2.Text = ""

        'number validators!!

        'validate
        If txtSearch1.Text = "" Then
            lblMessage1.Text = "Please search for something"
            Exit Sub
        End If

        'price?
        If radSearch1.SelectedValue.ToString = "Price" Then
            If valid.CheckForDigits(txtSearch1.Text) = False Then
                lblMessage1.Text = "Only search numbers with prices"
                Exit Sub
            End If
        End If

        'get all
        VehicleDB.GetAllVehiclesUsingSP()

        'search for textbox 1
        VehicleDB.SearchIndividual(radSearch1.Text, txtSearch1.Text, radTypeOfSearch.SelectedValue)

        'if no results- leave as is
        If VehicleDB.VehicleMyView.Count = 0 Then
            lblMessage1.Text = "No results found"
            Exit Sub
        End If

        'fill grid
        SortAndBind()

    End Sub


 
    Protected Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        'clear
        lblMessage1.Text = ""
        lblMessage2.Text = ""
        'load data set
        VehicleDB.GetAllVehiclesUsingSP()

        'fill grid
        SortAndBind()
    End Sub

    Protected Sub btnSearch2_Click(sender As Object, e As EventArgs) Handles btnSearch2.Click
        'clear
        lblMessage1.Text = ""
        lblMessage2.Text = ""
        'validate
        If txtSearch2.Text = "" Then
            lblMessage2.Text = "Please search for something"
            Exit Sub
        End If

        'price?
        If radSearch2.SelectedValue.ToString = "Price" Then
            If valid.CheckForDigits(txtSearch2.Text) = False Then
                lblMessage2.Text = "Only search numbers with prices"
                Exit Sub
            End If
        End If

        'get all
        VehicleDB.GetAllVehiclesUsingSP()

        'search for textbox 2
        VehicleDB.SearchIndividual(radSearch2.Text, txtSearch2.Text, radTypeOfSearch.SelectedValue.ToString)

        'if no results- leave as is
        If VehicleDB.VehicleMyView.Count = 0 Then
            lblMessage2.Text = "No results found"
            Exit Sub
        End If

        'fill grid
        SortAndBind()

    End Sub



    Protected Sub btnBoth_Click(sender As Object, e As EventArgs) Handles btnBoth.Click
        'clear
        lblMessage1.Text = ""
        lblMessage2.Text = ""

        'validate both boxes
        If txtSearch1.Text = "" Then
            lblMessage1.Text = "Please search for something"
            Exit Sub
        End If
        'check price
        If radSearch1.SelectedValue.ToString = "Price" Then
            If valid.CheckForDigits(txtSearch1.Text) = False Then
                lblMessage1.Text = "Only search numbers with prices"
                Exit Sub
            End If
        End If
        'check characters
        If radSearch1.SelectedValue.ToString <> "Price" Then
            If valid.CheckForDigits(txtSearch1.Text) = True Then
                lblMessage1.Text = "Only search letters"
                Exit Sub
            End If
        End If


        If txtSearch2.Text = "" Then
            lblMessage2.Text = "Please search for something"
            Exit Sub
        End If
        If radSearch2.SelectedValue.ToString = "Price" Then
            If valid.CheckForDigits(txtSearch2.Text) = False Then
                lblMessage2.Text = "Only search numbers with prices"
                Exit Sub
            End If
        End If
        'check characters
        If radSearch2.SelectedValue.ToString <> "Price" Then
            If valid.CheckForDigits(txtSearch2.Text) = True Then
                lblMessage2.Text = "Only search letters"
                Exit Sub
            End If
        End If

        'get data set
        VehicleDB.GetAllVehiclesUsingSP()

        'for price ranges
        If radSearch1.SelectedValue.ToString = "Price" And radSearch2.SelectedValue.ToString = "Price" Then 'Capitalize!

            VehicleDB.GetVehiclesByPrice(txtSearch1.Text, txtSearch2.Text)

            'no results
            If VehicleDB.VehicleMyView.Count = 0 Then
                lblMessage1.Text = "No results found"
                Exit Sub
            End If

            'fill
            SortAndBind()
            Exit Sub
        End If

        'search combined!!!
        VehicleDB.SearchBoth(radAndOr.SelectedValue, txtSearch1.Text, txtSearch2.Text, radSearch1.SelectedValue, radSearch2.SelectedValue, radTypeOfSearch.SelectedValue)
        'strRad As String, strIn1 As String, strIn2 As String, strRadSearch1 As String, strRadSearch2 As String, type of search

        'no results
        If VehicleDB.VehicleMyView.Count = 0 Then
            lblMessage1.Text = "No results found"
            Exit Sub
        End If

        'fill
        SortAndBind()
    End Sub


    'quick sort!!
    Protected Sub radSort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radSort.SelectedIndexChanged
        'change order by
        'get all
        VehicleDB.GetAllVehiclesUsingSP()

        'filter by same way as in session - previous search
        VehicleDB.VehicleMyView.RowFilter = Session("RowFilter")

        'fill grid
        SortAndBind()

        Session("RowFilter") = VehicleDB.VehicleMyView.RowFilter
    End Sub
End Class