
Imports System.Data
Imports System.Data.SqlClient

Public Class ClassVehicleDB
    'Author: Louis Andres
    'Assignment: ASP4
    'Date: 3/2/15
    'Program Description: Vehicle DB class


    ' these module variables are internal to object
    Dim mDatasetVehicle As New DataSet
    Dim mMyView As New DataView
    Dim mstrRowFilter As String 'for session filter

    Dim mstrQuery As String
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdbConn As SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=missql.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_msbcl459;user id=msbcl459;password=Cooper13"

   


    ' define a public read only property for the outside world to access the dataset filled by this class
    Public ReadOnly Property VehicleDataset() As DataSet
        Get
            Return mDatasetVehicle
        End Get
    End Property
    Public ReadOnly Property VehicleMyView() As DataView
        Get
            ' return dataset to user
            Return mMyView
        End Get
    End Property

    'For session filter
    Public ReadOnly Property RowFilter() As String
        Get
            Return mstrRowFilter
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
            mDatasetVehicle.Clear()

            ' fill the dataset
            mdbDataAdapter.Fill(mDatasetVehicle, "tblVehicles")

            ' close the connection
            mdbConn.Close()
        Catch ex As Exception
            Throw New Exception("query is " & strQuery.ToString & "error is " & ex.Message)
        End Try

    End Sub



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
            Me.mDatasetVehicle.Clear()
            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mDatasetVehicle, "tblVehicles")
            ' copy dataset to dataview
            mMyView.Table = mDatasetVehicle.Tables("tblVehicles")
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
            Me.mDatasetVehicle.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mDatasetVehicle, "tblVehicles")

            ' copy dataset to dataview
            mMyView.Table = mDatasetVehicle.Tables("tblVehicles")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub



    Public Sub GetAllVehiclesUsingSP() 'important to fill dataset!!
        'Purpose: get all Vehicles
        'Arguments: none
        'Returns: all Vehicles
        'Author: Louis Andres
        'Date: 3/22/15
        RunProcedure("usp_Vehicles_get_all")
    End Sub

    Public Sub DoSort(strSortValue As String) 'order by
        'Purpose: sort by either name or username
        'Arguments: rad button
        'Returns: sorted view
        'Author: Louis Andres
        'Date: 3/22/15

        mMyView.Sort = strSortValue

        'If strSortValue = "Make" Then
        '    mMyView.Sort = "make"
        'ElseIf strSortValue = "StockNumber" Then
        '    mMyView.Sort = "stocknumber"
        'Else
        '    mMyView.Sort = "color"
        ''needs to be literal - capitalizing matters!!
        'End If
    End Sub


    Public Sub SearchIndividual(strSearchRadValue As String, strSearchText As String, strRadType As String)
        'Purpose: sort by either name or username
        'Arguments: rad button
        'Returns: sorted view
        'Author: Louis Andres
        'Date: 3/22/15

        'one search - seaching for and search text
        If strRadType = "Exact" Then
            mMyView.RowFilter = strSearchRadValue & " = '" & strSearchText & "'"
        ElseIf strRadType = "Partial" Then
            mMyView.RowFilter = strSearchRadValue & " like '" & strSearchText & "%'"
        ElseIf strRadType = "Keyword" Then
            mMyView.RowFilter = strSearchRadValue & " like '%" & strSearchText & "%'"
        End If


        'store for session later
        mstrRowFilter = mMyView.RowFilter

    End Sub

    Public Sub GetVehiclesByPrice(intIn As String, intIn1 As String)
        'Price range
        mMyView.RowFilter = "Price >= '" & intIn & "' and price <= '" & intIn1 & "'"
        mstrRowFilter = mMyView.RowFilter
    End Sub

    Public Sub SearchBoth(strRad As String, strIn1 As String, strIn2 As String, strRadSearch1 As String, strRadSearch2 As String, strRadType As String)

        'two different searches combined with either and/or
        If strRadType = "Exact" Then
            mMyView.RowFilter = strRadSearch1 & " = '" & strIn1 & "' " & strRad & " " & strRadSearch2 & " = '" & strIn2 & "'"
        ElseIf strRadType = "Partial" Then
            mMyView.RowFilter = strRadSearch1 & " like '" & strIn1 & "%' " & strRad & " " & strRadSearch2 & " like '" & strIn2 & "%'"
        Else
            mMyView.RowFilter = strRadSearch1 & " like '%" & strIn1 & "%' " & strRad & " " & strRadSearch2 & " like '%" & strIn2 & "%'"
        End If

        'store for session later
        mstrRowFilter = mMyView.RowFilter

    End Sub


End Class

