'Title:         Update Warehouse Inventory
'Date:          1-15-15
'Author:        Terry Holmes

'Description:   This form is used to update the WarehouseID in the Time Warner Inventory Program

Option Strict On

Public Class Form1

    'Setting up the data variables
    Dim TheEmployeesDataSet As EmployeesDataSet
    Dim TheEmployeesDataTier As UpdateInventoryWarehouseDataTier
    Dim WithEvents TheEmployeesBindingSource As BindingSource

    Dim TheInventoryDataSet As InventoryDataSet
    Dim TheInventoryDataTier As UpdateInventoryWarehouseDataTier
    Dim WithEvents TheInventoryBindingSource As BindingSource

    Dim TheWarehouseInventoryDataSet As WarehouseInventoryDataSet
    Dim TheWarehouseInventoryDataTier As UpdateInventoryWarehouseDataTier
    Dim WithEvents TheWarehouseInventoryBindingSource As BindingSource

    'Setting up the array
    Dim mintCounter As Integer
    Dim mintSelectedIndex(1000) As Integer
    Dim mintUpperLimit As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'This will load when the program is launched

        'Setting local variables
        Dim intCounter As Integer
        Dim intNumberOfRecords As Integer
        Dim strLastNameForSearch As String
        Dim strLastNameFromTable As String
        Dim blnItemNotFound As Boolean = True

        Try

            TheEmployeesDataTier = New UpdateInventoryWarehouseDataTier
            TheEmployeesDataSet = TheEmployeesDataTier.GetEmployeeInformation
            TheEmployeesBindingSource = New BindingSource

            'Setting up the binding source
            With TheEmployeesBindingSource
                .DataSource = TheEmployeesDataSet
                .DataMember = "employees"
            End With

            'Setting up the combo box
            With cboEmployeeID
                .DataSource = TheEmployeesBindingSource
                .DisplayMember = "EmployeeID"
                .DataBindings.Add("text", TheEmployeesBindingSource, "EmployeeID", False, DataSourceUpdateMode.Never)
            End With

            'Setting up the rest of the controls for Employee
            txtFirstName.DataBindings.Add("text", TheEmployeesBindingSource, "FirstName")
            txtLastName.DataBindings.Add("text", TheEmployeesBindingSource, "LastName")

            'Setting up for the loop
            mintCounter = 0
            intNumberOfRecords = cboEmployeeID.Items.Count - 1
            strLastNameForSearch = "PARTS"

            'Performing loop
            For intCounter = 0 To intNumberOfRecords

                'Setting up the combo box
                cboEmployeeID.SelectedIndex = intCounter

                'Loading up the variable
                strLastNameFromTable = txtLastName.Text

                'Preforming loop
                If strLastNameForSearch = strLastNameFromTable Then
                    blnItemNotFound = False
                    mintSelectedIndex(mintCounter) = intCounter
                    mintCounter += 1
                End If

            Next

            If blnItemNotFound = True Then
                MessageBox.Show("No Items Were Found", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'Setting  up the navigation
            mintUpperLimit = mintCounter - 1
            mintCounter = 0
            cboEmployeeID.SelectedIndex = mintSelectedIndex(0)

            If mintUpperLimit > 0 Then
                btnNext.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub ClearInventoryDataBindings()

        cboPartID.DataBindings.Clear()
        txtPartNumber.DataBindings.Clear()
        txtWarehouseID.DataBindings.Clear()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        'This will close the program
        Me.Close()

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        'Setting up the combo box
        mintCounter += 1

        'Setting the selected index of the combo box
        cboEmployeeID.SelectedIndex = mintSelectedIndex(mintCounter)

        'enabling the button
        btnBack.Enabled = True

        'Checking to see if the button needs to be disabled
        If mintCounter = mintUpperLimit Then
            btnNext.Enabled = False
        End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        'Setting up the combo box
        mintCounter -= 1

        'Setting the selected index of the combo box
        cboEmployeeID.SelectedIndex = mintSelectedIndex(mintCounter)

        'enabling the button
        btnNext.Enabled = True

        'Checking to see if the button needs to be disabled
        If mintCounter = 0 Then
            btnBack.Enabled = False
        End If

    End Sub
    Private Sub BindControlsForInventory()

        Try

            'Setting the controls for inventory
            TheInventoryDataTier = New UpdateInventoryWarehouseDataTier
            TheInventoryDataSet = TheInventoryDataTier.GetInventoryInformation
            TheInventoryBindingSource = New BindingSource

            'Setting up the Binding Source
            With TheInventoryBindingSource
                .DataSource = TheInventoryDataSet
                .DataMember = "Inventory"
                .MoveFirst()
                .MoveLast()
            End With

            With cboPartID
                .DataSource = TheInventoryBindingSource
                .DisplayMember = "PartID"
                .DataBindings.Add("text", TheInventoryBindingSource, "PartID", False, DataSourceUpdateMode.Never)
            End With

            'Setting up the rest of the controls
            txtPartNumber.DataBindings.Add("Text", TheInventoryBindingSource, "PartNumber")
            txtWarehouseID.DataBindings.Add("text", TheInventoryBindingSource, "WarehouseID")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BindControlsForWarehouseInventory()

        Try

            'Setting the controls for inventory
            TheWarehouseInventoryDataTier = New UpdateInventoryWarehouseDataTier
            TheWarehouseInventoryDataSet = TheWarehouseInventoryDataTier.GetWarehouseInventoryInformation
            TheWarehouseInventoryBindingSource = New BindingSource

            'Setting up the Binding Source
            With TheWarehouseInventoryBindingSource
                .DataSource = TheWarehouseInventoryDataSet
                .DataMember = "WarehouseInventory"
                .MoveFirst()
                .MoveLast()
            End With

            With cboPartID
                .DataSource = TheWarehouseInventoryBindingSource
                .DisplayMember = "PartID"
                .DataBindings.Add("text", TheWarehouseInventoryBindingSource, "PartID", False, DataSourceUpdateMode.Never)
            End With

            'Setting up the rest of the controls
            txtPartNumber.DataBindings.Add("Text", TheWarehouseInventoryBindingSource, "PartNumber")
            txtWarehouseID.DataBindings.Add("text", TheWarehouseInventoryBindingSource, "WarehouseID")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SaveInventoryDataSet()

        Try

            'Saving the information
            TheInventoryBindingSource.EndEdit()
            TheInventoryDataTier.UpdateInventoryDB(TheInventoryDataSet)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub SaveWarehouseInventoryDataSet()

        Try

            'Saving the information
            TheWarehouseInventoryBindingSource.EndEdit()
            TheWarehouseInventoryDataTier.UpdateWarehouseInventoryDB(TheWarehouseInventoryDataSet)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Please Correct", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnSelectAndUpdate_Click(sender As Object, e As EventArgs) Handles btnSelectAndUpdate.Click

        'This will update the records

        'Setting local variables
        Dim intCounter As Integer
        Dim intNumberOfRecords As Integer

        'Removing and Setting Bindings
        ClearInventoryDataBindings()
        BindControlsForInventory()

        'Getting the count
        intNumberOfRecords = cboPartID.Items.Count - 1

        'Preforming Loop
        For intCounter = 0 To intNumberOfRecords

            'Incrementing the combo box
            cboPartID.SelectedIndex = intCounter

            'Setting the controls
            txtWarehouseID.Text = cboEmployeeID.Text

            'Saving the record
            SaveInventoryDataSet()

        Next

        'Clearing the data set
        ClearInventoryDataBindings()
        BindControlsForWarehouseInventory()

        'Getting the count
        intNumberOfRecords = cboPartID.Items.Count - 1

        'Preforming Loop
        For intCounter = 0 To intNumberOfRecords

            'Incrementing the combo box
            cboPartID.SelectedIndex = intCounter

            'Setting the controls
            txtWarehouseID.Text = cboEmployeeID.Text

            'Saving the record
            SaveWarehouseInventoryDataSet()

        Next

        MessageBox.Show("All Records are Updated", "Thank You", MessageBoxButtons.OK, MessageBoxIcon.Information)


    End Sub
End Class
