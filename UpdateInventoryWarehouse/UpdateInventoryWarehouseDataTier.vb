'Title:         Update Inventory Warehouse Data Tier
'Date:          1-15-15
'Author:        Terry Holmes

'Description:   This data tier is for updating the warehouse information

Option Strict On

Public Class UpdateInventoryWarehouseDataTier

    Private aEmployeesDataSetTableAdapter As EmployeesDataSetTableAdapters.employeesTableAdapter
    Private aEmployeesDataSet As EmployeesDataSet

    Private aInventoryDataSetTableAdapter As InventoryDataSetTableAdapters.InventoryTableAdapter
    Private aInventoryDataSet As InventoryDataSet

    Private aWarehouseInventoryDataSetTableAdapter As WarehouseInventoryDataSetTableAdapters.WarehouseInventoryTableAdapter
    Private aWarehouseInventoryDataSet As WarehouseInventoryDataSet

    Public Function GetWarehouseInventoryInformation() As WarehouseInventoryDataSet

        'Setting up the Datatier
        Try
            aWarehouseInventoryDataSet = New WarehouseInventoryDataSet
            aWarehouseInventoryDataSetTableAdapter = New WarehouseInventoryDataSetTableAdapters.WarehouseInventoryTableAdapter
            aWarehouseInventoryDataSetTableAdapter.Fill(aWarehouseInventoryDataSet.WarehouseInventory)
            Return aWarehouseInventoryDataSet

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Check", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return aWarehouseInventoryDataSet
        End Try
    End Function

    Public Sub UpdateWarehouseInventoryDB(ByVal aWarehouseInventoryDataSet As WarehouseInventoryDataSet)

        'This will update the database
        Try
            aWarehouseInventoryDataSetTableAdapter.Update(aWarehouseInventoryDataSet.WarehouseInventory)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetInventoryInformation() As InventoryDataSet

        'Setting up the Datatier
        Try
            aInventoryDataSet = New InventoryDataSet
            aInventoryDataSetTableAdapter = New InventoryDataSetTableAdapters.InventoryTableAdapter
            aInventoryDataSetTableAdapter.Fill(aInventoryDataSet.Inventory)
            Return aInventoryDataSet

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Check", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return aInventoryDataSet
        End Try
    End Function

    Public Sub UpdateInventoryDB(ByVal aInventoryDataSet As InventoryDataSet)

        'This will update the database
        Try
            aInventoryDataSetTableAdapter.Update(aInventoryDataSet.Inventory)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetEmployeeInformation() As EmployeesDataSet

        'Setting up the Datatier
        Try
            aEmployeesDataSet = New EmployeesDataSet
            aEmployeesDataSetTableAdapter = New EmployeesDataSetTableAdapters.employeesTableAdapter
            aEmployeesDataSetTableAdapter.Fill(aEmployeesDataSet.employees)
            Return aEmployeesDataSet

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Check", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return aEmployeesDataSet
        End Try
    End Function

    Public Sub UpdateEmployeeDB(ByVal aEmployeesDataSet As EmployeesDataSet)

        'This will update the database
        Try
            aEmployeesDataSetTableAdapter.Update(aEmployeesDataSet.employees)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
