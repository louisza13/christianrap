'Author: Louis Andres
'Assignment: ASP4
'Date: 3/2/15
'Program Description: Customer DB class

Imports System.Data
Imports System.Data.SqlClient


Public Class ClassCustomerDB

    ' these module variables are internal to object
    Dim mDatasetCustomer As New DataSet
    Dim mMyView As New DataView

    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=missql.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_msbcl459;user id=msbcl459;password=Cooper13"



    ' define a public read only property for the outside world to access the dataset filled by this class
    Public ReadOnly Property CustDataset() As DataSet
        Get
            ' return dataset to user
            Return mDatasetCustomer
        End Get
    End Property
    Public ReadOnly Property CustMyView() As DataView
        Get
            ' return dataset to user
            Return mMyView
        End Get
    End Property



    ' define a public sub that will handle running any select query
    ' in class I had this as private, but said you might want to make it public, so you 
    ' could just send it any query and have it run it.  This avoids having more subs for 
    ' each type of query you want to run.


    Public Sub SelectQuery(ByVal strQuery As String)

        ' purpose: run any select query and fill dataset

        Try
            ' define data connection and data adapter
            mdbConn = New SqlConnection(mstrConnection)
            mdbDataAdapter = New SqlDataAdapter(strQuery, mdbConn)

            ' open the connection and dataset 
            mdbConn.Open()

            ' clear the dataset before filling
            mDatasetCustomer.Clear()

            ' fill the dataset
            mdbDataAdapter.Fill(mDatasetCustomer, "tblCustomers")

            ' close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & "error is " & ex.Message)
        End Try

    End Sub

    Public Sub GetAllCustomers()
        ' purpose: to return all customer records
        ' inputs: none
        ' outputs: none directly, but it opens and fills the dataset
        '          with all the records in tblCustomers

        ' to Get all Customers use Select
        mstrQuery = "select * from tblCustomers order by LastName, FirstName"
        ' run the query
        SelectQuery(mstrQuery)
    End Sub

    Public Sub GetCustomersByState(strIN As String)
        'Purpose: show data from certatin state
        'Arguments: state input
        'Returns: data about customers from certain state
        'Author: Louis Andres
        'Date: 2/11/15
        mstrQuery = "select * from tblCustomers where state = '" & strIN & "'"
        SelectQuery(mstrQuery)

    End Sub


    Public Function CheckUsername(strIN As String) As Boolean
        'Purpose: check username in database
        'Arguments: username
        'Returns: true/false
        'Author: Louis Andres
        'Date: 2/11/15
        mstrQuery = "select * from tblCustomers where username = '" & strIN & "'"
        SelectQuery(mstrQuery)

        'check number of rows
        If mDatasetCustomer.Tables("tblCustomers").Rows.Count = 0 Then
            Return False
        Else
            Return True

        End If
    End Function

    Public Function CheckPassword(strIN As String) As Boolean
        'Purpose: check password with username
        'Arguments: password
        'Returns: true/false
        'Author: Louis Andres
        'Date: 2/11/15
        If strIN = mDatasetCustomer.Tables("tblCustomers").Rows(0).Item("password").ToString Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub AddCustomer(strUsername As String, strPassword As String, strLastName As String, strFirstName As String, strInitial As String, strAddress As String, strCity As String, strState As String, strZip As String, strPhone As String, strEmail As String)
        'Purpose: add customer to db
        'Arguments: all customer info
        'Returns: none
        'Author: Louis Andres
        'Date: 2/16/15

        mstrQuery = "INSERT INTO tblCustomers (UserName, Password, LastName, FirstName, MI, Address, City, State, ZipCode, Phone, EmailAddr) VALUES (" & _
      "'" & strUsername & "', " & _
      "'" & strPassword & "', " & _
      "'" & strLastName & "', " & _
      "'" & strFirstName & "', " & _
      "'" & strInitial & "', " & _
      "'" & strAddress & "', " & _
      "'" & strCity & "', " & _
      "'" & strState & "', " & _
      "'" & strZip & "', " & _
      "'" & strPhone & "', " & _
      "'" & strEmail & "')"

        UpdateDB(mstrQuery)

    End Sub


    Public Sub UpdateDB(strQuery As String)
        'purpose: run given query to update database
        'arguement: one string
        'return: nothing
        'Author: Louis Andres
        'Date: 2/16/15

        Try
            'make connection
            mdbConn = New SqlConnection(mstrConnection)
            Dim dbCommand As New SqlCommand(strQuery, mdbConn)

            'open the connection
            mdbConn.Open()

            'run the query
            dbCommand.ExecuteNonQuery()

            'close the connection
            mdbConn.Close()

        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & " error is " & ex.Message)

        End Try


    End Sub

    Public Sub GetAllCustomersWithFullName()
        'Purpose: get customers with full name together
        'Arguments: first name and last name
        'Returns: first and last together in sql
        'Author: Louis Andres
        'Date: 3/2/15

        Dim strQuery As String
        strQuery = "Select *, LastName + ', ' + FirstName as FullName from tblCustomers Order by lastname, firstname"
        SelectQuery(strQuery)

    End Sub

    Public Sub ModifyCustomer(strUsername As String, strPassword As String, strLastName As String, strFirstName As String, strInitial As String, strAddress As String, strCity As String, strState As String, strZip As String, strPhone As String, strEmail As String, RecordID As String)
        'Purpose: abort delete mode
        'Arguments: all inputs from txt boxes
        'Returns: modified record
        'Author: Louis Andres
        'Date: 3/2/15
        mstrQuery = "UPDATE tblCustomers SET " & _
   "LastName = '" & strUsername & "', " & _
   "FirstName = '" & strPassword & "', " & _
   "MI = '" & strLastName & "', " & _
   "Username = '" & strFirstName & "', " & _
   "Password = '" & strInitial & "', " & _
   "Address = '" & strAddress & "', " & _
   "City = '" & strCity & "', " & _
   "State = '" & strState & "', " & _
   "Zipcode = '" & strZip & "', " & _
   "Phone = '" & strPhone & "', " & _
   "EmailAddr = '" & strEmail & "' " & _
   "WHERE RecordID = " & RecordID

        UpdateDB(mstrQuery)
    End Sub

    Public Sub DeleteRecord(intID As Integer)
        'Purpose: delete record
        'Arguments: record id
        'Returns: none
        'Author: Louis Andres
        'Date: 3/2/15
        mstrQuery = "Delete from tblcustomers where recordID = " & intID
        UpdateDB(mstrQuery)

    End Sub

    Public Function CheckRecordID(strIN As String) As Boolean
        'Purpose: check username in database
        'Arguments: username
        'Returns: true/false
        'Author: Louis Andres
        'Date: 2/11/15
        mstrQuery = "select * from tblCustomers where RecordID = '" & strIN & "'"
        SelectQuery(mstrQuery)

        'check number of rows
        If mDatasetCustomer.Tables("tblCustomers").Rows.Count = 0 Then
            Return False
        Else
            Return True

        End If
    End Function

    'stored procedures
    Public Sub RunProcedure(ByVal strName As String)
        ' CREATES INSTANCES OF THE CONNECTION AND COMMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        ' Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strName, objConnection)
        Try
            ' SETS THE COMMAND TYPE TO "STORED PROCEDURE"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            ' clear dataset
            Me.mDatasetCustomer.Clear()
            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mDatasetCustomer, "tblCustomers")
            ' copy dataset to dataview
            mMyView.Table = mDatasetCustomer.Tables("tblCustomers")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub


    Public Sub RunSPwithOneParam(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
        ' purpose to run a stored procedure with one parameter
        ' inputs:  stored procedure name, parameter name, parameter value
        ' returns: dataset filled with correct records

        ' CREATES INSTANCES OF THE CONNECTION AND COMMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        ' Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strSPName, objConnection)
        Try
            ' SETS THE COMMAND TYPE TO "STORED PROCEDURE"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            ' ADD PARAMETER(S) TO SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParamName, strParamValue))
            ' clear dataset
            Me.mDatasetCustomer.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mDatasetCustomer, "tblCustomers")

            ' copy dataset to dataview
            mMyView.Table = mDatasetCustomer.Tables("tblCustomers")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub



    Public Sub GetAllCustomersUsingSP()
        'Purpose: get all customers
        'Arguments: none
        'Returns: all customers
        'Author: Louis Andres
        'Date: 3/22/15
        RunProcedure("usp_Customers_get_all")
    End Sub

    Public Sub DoSort(strSortValue As String)
        'Purpose: sort by either name or username
        'Arguments: rad button
        'Returns: sorted view
        'Author: Louis Andres
        'Date: 3/22/15
        If strSortValue = "Sort By Name" Then
            mMyView.Sort = "lastname, firstname"
        Else
            mMyView.Sort = "username"
        End If
    End Sub

    Public Sub SearchByState(strIn As String)
        'Purpose: search by state
        'Arguments: state
        'Returns: customers from one state
        'Author: Louis Andres
        'Date: 3/22/15
        mMyView.RowFilter = "state = '" & strIn & "'"
    End Sub

    Public Sub SearchByLastName(strIn As String)
        'Purpose: search by last name
        'Arguments: last name (partial)
        'Returns: customers with last name
        'Author: Louis Andres
        'Date: 3/22/15
        mMyView.RowFilter = "lastname like '" & strIn & "%'"
    End Sub

    Public Sub SearchByCity(strIn As String)
        'Purpose: search by city
        'Arguments: city (partial)
        'Returns: customers from one city
        'Author: Louis Andres
        'Date: 3/22/15
        mMyView.RowFilter = "city like '" & strIn & "%'"
    End Sub

    Public Sub SearchByUsername(strIn As String)
        'Purpose: search by username
        'Arguments: username (partial)
        'Returns: customers with certain username
        'Author: Louis Andres
        'Date: 3/22/15
        mMyView.RowFilter = "username like '" & strIn & "%'"
    End Sub


End Class

