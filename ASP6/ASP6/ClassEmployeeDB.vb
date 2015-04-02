'Author: Louis Andres
'Assignment: ASP4
'Date: 3/2/15
'Program Description: Employee DB class

Imports System.Data
Imports System.Data.SqlClient


Public Class ClassEmployeeDB

    ' these module variables are internal to object
    Dim mDatasetEmployee As New DataSet
    Dim mMyView As New DataView

    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=missql.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_msbcl459;user id=msbcl459;password=Cooper13"



    ' define a public read only property for the outside world to access the dataset filled by this class
    Public ReadOnly Property EmpDataset() As DataSet
        Get
            ' return dataset to user
            Return mDatasetEmployee
        End Get
    End Property
    Public ReadOnly Property EmpMyView() As DataView
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
            mDatasetEmployee.Clear()

            ' fill the dataset
            mdbDataAdapter.Fill(mDatasetEmployee, "tblEmployees")

            ' close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & "error is " & ex.Message)
        End Try

    End Sub

    Public Sub GetAllEmployees()
        ' purpose: to return all Employee records
        ' inputs: none
        ' outputs: none directly, but it opens and fills the dataset
        '          with all the records in tblEmployees

        ' to Get all Employees use Select
        mstrQuery = "select * from tblEmployees order by LastName, FirstName"
        ' run the query
        SelectQuery(mstrQuery)
    End Sub






    Public Function CheckUsernameAndPassword(strUsername As String, strPassword As String) As Integer
        'Purpose: check password and username
        'Arguments: username and password 
        'Returns: -1 if good, -2 if bad user, -3 if bas pass
        'Author: Louis Andres
        'Date: 3/4/15
        mstrQuery = "select * from tblEmployees where EmpID = '" & strUsername & "'"
        SelectQuery(mstrQuery)

        'check number of rows
        If mDatasetEmployee.Tables("tblEmployees").Rows.Count = 0 Then
            Return -2
        End If

        If strPassword <> mDatasetEmployee.Tables("tblEmployees").Rows(0).Item("password").ToString Then
            Return -3
        End If

        Return -1

    End Function


    Public Sub RunProcedure(ByVal strName As String)
        ' CREATES INSTANCES OF THE CONNECTION AND COMMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        ' Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strName, objConnection)
        Try
            ' SETS THE COMMAND TYPE TO "STORED PROCEDURE"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            ' clear dataset
            Me.mDatasetEmployee.Clear()
            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mDatasetEmployee, "tblEmployees")
            ' copy dataset to dataview
            mMyView.Table = mDatasetEmployee.Tables("tblEmployees")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    'stored procedures
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
            Me.mDatasetEmployee.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mDatasetEmployee, "tblEmployees")

            ' copy dataset to dataview
            mMyView.Table = mDatasetEmployee.Tables("tblEmployees")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Function CheckEmpIDandPasswordUsingSP(strEmpID As String, strPassword As String) As Integer
        'Purpose: check emp id and password
        'Arguments: empid and password
        'Returns: customer view and record count
        'Author: Louis Andres
        'Date: 3/22/15
        RunSPwithOneParam("usp_Employees_get_by_EmpID", "@EmpID", strEmpID)
        If mMyView.Table.Rows.Count = 0 Then
            Return -2
        End If

        If strPassword <> mMyView.Table.Rows(0).Item("password").ToString Then
            Return -3
        End If

        Return -1

    End Function



End Class

