Public Class DDLandGrid
    Inherits System.Web.UI.Page

    Dim DB As New ClassCustomerDB
    Dim Format As New ClassFormat
    Dim valid As New ClassValidate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Purpose: load db for the first time and fill txt boxes
        'Arguments: none
        'Returns: none
        'Author: Louis Andres
        'Date: 3/2/15

        'first time thru
        If IsPostBack = False Then

            'load DDL
            LoadDDL()
            'Get all customers
            DB.GetAllCustomersUsingSP()
            'fill
            FillGrid()
        End If
    End Sub



    Private Sub LoadDDL()  'important!! - create new table
        'Purpose: load db into ddl
        'Arguments: none
        'Returns: none
        'Author: Louis Andres
        'Date: 3/2/15

        DB.GetCities()
        ddlCity.DataSource = DB.CustDataset.Tables("tblCities")
        ddlCity.DataTextField = "City"
        ddlCity.DataBind()
    End Sub



    Public Sub FillGrid()
        'Purpose: fill txt boxes
        'Arguments: none
        'Returns: filled txt boxes
        'Author: Louis Andres
        'Date: 3/2/15

        ' set up grid view
        gvCustomers.DataSource = DB.CustMyView
        gvCustomers.DataBind()


        'show customer total

        txtCustomerCount.Text = DB.CustMyView.Count.ToString

    End Sub


  

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        'get all customers
        DB.GetAllCustomersUsingSP()
        'fill
        FillGrid()
    End Sub

    Protected Sub ddlCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCity.SelectedIndexChanged
        'get all customers
        DB.GetAllCustomersUsingSP()
        'sort by city selected
        DB.SearchByCity2(ddlCity.SelectedValue)
        'fill
        FillGrid()
    End Sub
End Class