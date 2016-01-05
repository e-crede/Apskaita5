Imports AccDataAccessLayer
Imports ApskaitaObjects.HelperLists
Friend Module CachedListsLoaders

#Region "*** CACHE BINDINGS MANAGER METHODS ***"

    Private BindingSourceList As List(Of BindingSourceItem)

    Public Function GetBindingSourceForCachedList(ByVal cachedItemBaseType As Type, _
        ByVal ParamArray filterCriteria As Object()) As BindingSource

        Dim result As New BindingSourceItem(cachedItemBaseType, filterCriteria)

        If BindingSourceList Is Nothing Then
            BindingSourceList = New List(Of BindingSourceItem)
        End If
        BindingSourceList.Add(result)

        AddHandler result.BindingSourceInstance.Disposed, AddressOf BindingSource_Disposed

        Return result.BindingSourceInstance

    End Function

    Public Sub CachedListChanged(ByVal e As CacheChangedEventArgs)
        If BindingSourceList Is Nothing Then Exit Sub
        For Each bs As BindingSourceItem In BindingSourceList
            If bs.BaseType Is e.Type Then bs.UpdateDataSource()
        Next
    End Sub

    Private Sub BindingSource_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)

        If BindingSourceList Is Nothing Then Exit Sub

        For i As Integer = BindingSourceList.Count To 1 Step -1
            If BindingSourceList(i - 1).BindingSourceInstance Is DirectCast(sender, BindingSource) Then
                BindingSourceList.RemoveAt(i - 1)
                RemoveHandler DirectCast(sender, BindingSource).Disposed, AddressOf BindingSource_Disposed
                Exit For
            End If
        Next

    End Sub

#End Region


    Public Function PrepareCache(ByVal callingForm As Form, ByVal ParamArray nBaseType As Type()) As Boolean

        Try
            Using busy As New StatusBusy
                CacheObjectList.GetList(nBaseType)
            End Using
        Catch ex As Exception
            ShowError(ex)
            If Not callingForm Is Nothing Then DisableAllControls(CType(callingForm, Control))
            Return False
        End Try

        Return True

    End Function



    Public Sub LoadNameInfoListToCombo(ByRef comboObject As ComboBox, _
        ByVal ofType As ApskaitaObjects.Settings.NameType, ByVal addEmptyItem As Boolean)

        If Not comboObject.DataSource Is Nothing Then Exit Sub

        comboObject.DataSource = GetBindingSourceForCachedList( _
            GetType(NameInfoList), ofType, addEmptyItem)

        AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadNameInfoListToCombo(ByRef comboObject As DataGridViewComboBoxColumn, _
        ByVal ofType As ApskaitaObjects.Settings.NameType, ByVal addEmptyItem As Boolean)

        If Not comboObject.DataSource Is Nothing Then Exit Sub

        comboObject.DataSource = GetBindingSourceForCachedList( _
            GetType(NameInfoList), ofType, addEmptyItem)

        AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadPersonGroupInfoListToCombo(ByRef comboObject As ComboBox)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.ValueMember = "GetMe"
        ComboObject.DisplayMember = "Name"
        comboObject.DataSource = GetBindingSourceForCachedList(GetType(PersonGroupInfoList), True)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadPersonGroupInfoListToCombo(ByRef comboObject As DataGridViewComboBoxColumn)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.ValueMember = "GetMe"
        ComboObject.DisplayMember = "Name"
        ComboObject.DataSource = GetBindingSourceForCachedList(GetType(PersonGroupInfoList), True)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Function LoadTaxRateListToCombo(ByRef comboObject As ComboBox, _
        ByVal taxType As TaxRateType) As Boolean

        If Not comboObject.DataSource Is Nothing Then Exit Function

        comboObject.DataSource = GetBindingSourceForCachedList(GetType(TaxRateInfoList), taxType, True)

        AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        Return True

    End Function

    Public Function LoadTaxRateListToCombo(ByRef comboObject As DataGridViewComboBoxColumn, _
        ByVal taxType As TaxRateType) As Boolean

        If Not ComboObject.DataSource Is Nothing Then Exit Function

        comboObject.DataSource = GetBindingSourceForCachedList(GetType(TaxRateInfoList), taxType, True)

        AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        Return True

    End Function

    Public Sub LoadCurrencyCodeListToComboBox(ByRef comboControl As ComboBox, ByVal addEmptyItem As Boolean)
        comboControl.DataSource = CurrencyCodes
    End Sub

    Public Sub LoadCurrencyCodeListToComboBox(ByRef comboControl As DataGridViewComboBoxColumn, _
       ByVal addEmptyItem As Boolean)
        ComboControl.DataSource = CurrencyCodes
    End Sub

    Public Sub LoadEnumHumanReadableListToComboBox(ByRef ComboControl As ComboBox, _
        ByVal EnumType As Type, ByVal AddEmptyItem As Boolean)
        ComboControl.DataSource = GetEnumValuesHumanReadableList(EnumType, AddEmptyItem)
    End Sub

    Public Sub LoadEnumLocalizedListToComboBox(ByRef comboControl As ComboBox, _
        ByVal enumType As Type, ByVal addEmptyItem As Boolean)
        Dim datasource As List(Of String) = EnumValueAttribute.GetLocalizedNameList(enumType)
        If addEmptyItem Then datasource.Insert(0, "")
        comboControl.DataSource = datasource
    End Sub

    Public Sub LoadEnumHumanReadableListToComboBox(ByRef ComboControl As DataGridViewComboBoxColumn, _
        ByVal EnumType As Type, ByVal AddEmptyItem As Boolean)
        ComboControl.DataSource = GetEnumValuesHumanReadableList(EnumType, AddEmptyItem)
    End Sub

    Public Sub LoadEnumLocalizedListToComboBox(ByRef comboControl As DataGridViewComboBoxColumn, _
        ByVal enumType As Type, ByVal addEmptyItem As Boolean)
        Dim datasource As List(Of String) = EnumValueAttribute.GetLocalizedNameList(enumType)
        If addEmptyItem Then datasource.Insert(0, "")
        comboControl.DataSource = datasource
    End Sub

    Public Sub LoadDocumentSerialInfoListToCombo(ByRef comboObject As ComboBox, _
        ByVal docType As ApskaitaObjects.Settings.DocumentSerialType, _
        ByVal addEmptyItem As Boolean, ByVal reload As Boolean)

        If Not Reload Then
            comboObject.ValueMember = "Serial"
            comboObject.DisplayMember = "Serial"
            comboObject.DataSource = GetBindingSourceForCachedList( _
                GetType(DocumentSerialInfoList), addEmptyItem, docType)
        End If

    End Sub

    Public Sub LoadDocumentSerialInfoListToCombo(ByRef comboObject As DataGridViewComboBoxColumn, _
        ByVal docType As ApskaitaObjects.Settings.DocumentSerialType, _
        ByVal addEmptyItem As Boolean, ByVal reload As Boolean)

        If Not reload Then
            comboObject.ValueMember = "Serial"
            comboObject.DisplayMember = "Serial"
            comboObject.ValueType = GetType(DocumentSerialInfo)
            comboObject.DataSource = GetBindingSourceForCachedList( _
                GetType(DocumentSerialInfoList), addEmptyItem, docType)
        End If

    End Sub

    Public Sub LoadLanguageListToComboBox(ByRef comboControl As ComboBox, ByVal addEmptyItem As Boolean)

        If Not comboControl.DataSource Is Nothing Then Exit Sub

        comboControl.DataSource = GetBindingSourceForCachedList( _
            GetType(CompanyRegionalInfoList), addEmptyItem)

        AddHandler comboControl.Disposed, AddressOf LanguageListComboBox_Disposed

    End Sub

    Public Sub LoadLanguageListToComboBox(ByRef comboControl As DataGridViewComboBoxColumn, ByVal addEmptyItem As Boolean)

        If Not ComboControl.DataSource Is Nothing Then Exit Sub

        comboControl.DataSource = GetBindingSourceForCachedList( _
            GetType(CompanyRegionalInfoList), addEmptyItem)

        AddHandler ComboControl.Disposed, AddressOf LanguageListComboBox_Disposed

    End Sub



    Public Sub LoadCashAccountInfoListToGridCombo(Of T As IGridComboBox)(ByRef comboObject As T, ByVal AddEmptyItem As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim dataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim dataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim dataGridViewTextBoxColumn4 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim listBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(CashAccountInfoList), True, AddEmptyItem)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = listBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        dataGridViewTextBoxColumn2.DataPropertyName = "Account"
        dataGridViewTextBoxColumn2.HeaderText = "Sąskaita"
        dataGridViewTextBoxColumn2.Name = ""
        dataGridViewTextBoxColumn2.ReadOnly = True

        dataGridViewTextBoxColumn3.DataPropertyName = "CurrencyCode"
        dataGridViewTextBoxColumn3.HeaderText = "Valiuta"
        dataGridViewTextBoxColumn3.Name = ""
        dataGridViewTextBoxColumn3.ReadOnly = True
        dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        dataGridViewTextBoxColumn4.DataPropertyName = "Name"
        dataGridViewTextBoxColumn4.HeaderText = "Pavadinimas"
        dataGridViewTextBoxColumn4.Name = ""
        dataGridViewTextBoxColumn4.ReadOnly = True
        dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Account")

    End Sub

    Public Sub LoadPersonInfoListToGridCombo(Of T As IGridComboBox)(ByRef comboObject As T, _
        ByVal addEmptyItem As Boolean, ByVal showClients As Boolean, _
        ByVal showSuppliers As Boolean, ByVal showWorkers As Boolean)

        If comboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim dataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim dataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim listBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(PersonInfoList), addEmptyItem, showClients, showSuppliers, showWorkers)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = listBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        dataGridViewTextBoxColumn2.DataPropertyName = "Name"
        dataGridViewTextBoxColumn2.HeaderText = "Name"
        dataGridViewTextBoxColumn2.Name = ""
        dataGridViewTextBoxColumn2.ReadOnly = True

        dataGridViewTextBoxColumn3.DataPropertyName = "Code"
        dataGridViewTextBoxColumn3.HeaderText = "Code"
        dataGridViewTextBoxColumn3.Name = ""
        dataGridViewTextBoxColumn3.ReadOnly = True
        dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet

        result.BindingContext = comboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        comboObject.SetNestedDataGridView(result)
        comboObject.SetCloseOnSingleClick(True)
        comboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadAccountInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal ParamArray ClassFilter() As Integer)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(AccountInfoList), AddEmptyItem, ClassFilter)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.DataPropertyName = "ID"
        DataGridViewTextBoxColumn2.HeaderText = "Nr."
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet

        DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn3.DataPropertyName = "Name"
        DataGridViewTextBoxColumn3.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetValueMember("ID")
        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("ID")
        ComboObject.SetEmptyValueString("0")

    End Sub

    Public Sub LoadServiceInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal ShowSales As Boolean, _
        ByVal ShowPurchases As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(ServiceInfoList), AddEmptyItem, ShowSales, ShowPurchases, True)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn2})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.DataPropertyName = "NameShort"
        DataGridViewTextBoxColumn2.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("NameShort")

    End Sub

    Public Sub LoadLongTermAssetInfoListToGridCombo(Of T As IGridComboBox)(ByRef comboObject As T)

        If comboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn1 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = GetType(ActiveReports.LongTermAssetOperationInfoList)
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn1.DataPropertyName = "InventoryNumber"
        DataGridViewTextBoxColumn1.HeaderText = "Inv. Nr."
        DataGridViewTextBoxColumn1.Name = ""
        DataGridViewTextBoxColumn1.ReadOnly = True
        DataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        DataGridViewTextBoxColumn2.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = comboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        comboObject.SetNestedDataGridView(result)
        comboObject.SetCloseOnSingleClick(True)
        comboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadWorkTimeClassInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal ShowWithoutHours As Boolean, _
        ByVal ShowWithHours As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(WorkTimeClassInfoList), AddEmptyItem, ShowWithoutHours, ShowWithHours)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.DataPropertyName = "Code"
        DataGridViewTextBoxColumn2.HeaderText = "Kodas"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn3.DataPropertyName = "Name"
        DataGridViewTextBoxColumn3.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Code")

    End Sub

    Public Sub LoadGoodsInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal TradedType As Documents.TradedItemType)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn4 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(GoodsInfoList), True, AddEmptyItem, TradedType)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        DataGridViewTextBoxColumn2.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DataGridViewTextBoxColumn3.DataPropertyName = "GoodsCode"
        DataGridViewTextBoxColumn3.HeaderText = "Kodas"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn4.DataPropertyName = "GoodsBarcode"
        DataGridViewTextBoxColumn4.HeaderText = "BAR Kodas"
        DataGridViewTextBoxColumn4.Name = ""
        DataGridViewTextBoxColumn4.ReadOnly = True
        DataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadGoodsGroupInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(GoodsGroupInfoList), AddEmptyItem)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {DataGridViewTextBoxColumn2})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        DataGridViewTextBoxColumn2.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadWarehouseInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn4 As New System.Windows.Forms.DataGridViewCheckBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(WarehouseInfoList), AddEmptyItem, True)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        DataGridViewTextBoxColumn2.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DataGridViewTextBoxColumn3.DataPropertyName = "WarehouseAccount"
        DataGridViewTextBoxColumn3.HeaderText = "Sąskaita"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn4.DataPropertyName = "IsProduction"
        DataGridViewTextBoxColumn4.HeaderText = "Gamyba"
        DataGridViewTextBoxColumn4.Name = ""
        DataGridViewTextBoxColumn4.ReadOnly = True
        DataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadProductionCalculationInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal ShowObsolete As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn4 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(ProductionCalculationInfoList), AddEmptyItem, ShowObsolete)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewTextBoxColumn2.DataPropertyName = "Date"
        DataGridViewTextBoxColumn2.HeaderText = "Data"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn3.DataPropertyName = "GoodsName"
        DataGridViewTextBoxColumn3.HeaderText = "Gaminamos Prekės"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn4.DataPropertyName = "Description"
        DataGridViewTextBoxColumn4.HeaderText = "Aprašymas"
        DataGridViewTextBoxColumn4.Name = ""
        DataGridViewTextBoxColumn4.ReadOnly = True
        DataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("GoodsName")

    End Sub

    Public Sub LoadCodeInfoListToGridCombo(Of T As IGridComboBox) _
        (ByRef comboObject As T, ByVal ofType As ApskaitaObjects.Settings.CodeType, _
         ByVal includeEmpty As Boolean, ByVal includeObsolete As Boolean)

        If comboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim dataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim dataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim listBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.CodeInfoList), ofType, includeEmpty, includeObsolete)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = listBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        dataGridViewTextBoxColumn2.DataPropertyName = "Code"
        dataGridViewTextBoxColumn2.HeaderText = "Kodas"
        dataGridViewTextBoxColumn2.Name = ""
        dataGridViewTextBoxColumn2.ReadOnly = True
        dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dataGridViewTextBoxColumn2.DefaultCellStyle.Format = "00"

        dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        dataGridViewTextBoxColumn3.DataPropertyName = "Name"
        dataGridViewTextBoxColumn3.HeaderText = "Pavadinimas"
        dataGridViewTextBoxColumn3.Name = ""
        dataGridViewTextBoxColumn3.ReadOnly = True

        result.BindingContext = comboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        comboObject.SetValueMember("Code")
        comboObject.SetNestedDataGridView(result)
        comboObject.SetCloseOnSingleClick(True)
        comboObject.SetFilterPropertyName("Code")

    End Sub

    Public Sub LoadAssignableCRItemListToGridCombo(Of T As IGridComboBox) _
        (ByRef comboObject As T, ByVal includeEmpty As Boolean)

        If comboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim dataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim listBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(AssignableCRItemList), includeEmpty)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = listBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {dataGridViewTextBoxColumn2})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        dataGridViewTextBoxColumn2.DataPropertyName = "Name"
        dataGridViewTextBoxColumn2.HeaderText = "Eil. Pavadinimas"
        dataGridViewTextBoxColumn2.Name = ""
        dataGridViewTextBoxColumn2.ReadOnly = True
        dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = comboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        comboObject.SetNestedDataGridView(result)
        comboObject.SetCloseOnSingleClick(True)
        comboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadLongTermAssetCustomGroupInfoToGridCombo(Of T As IGridComboBox) _
        (ByRef comboObject As T, ByVal addEmptyItem As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim dataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim listBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(LongTermAssetCustomGroupInfoList), addEmptyItem)

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = listBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {dataGridViewTextBoxColumn2})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        dataGridViewTextBoxColumn2.DataPropertyName = "Name"
        dataGridViewTextBoxColumn2.HeaderText = "Pavadinimas"
        dataGridViewTextBoxColumn2.Name = ""
        dataGridViewTextBoxColumn2.ReadOnly = True
        dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        comboObject.SetFilterPropertyName("Name")

    End Sub


    Public Sub LoadLocalUserListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal list As AccDataAccessLayer.Security.LocalUserList)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = New BindingSource
        ListBindingSource.DataSource = list

        CType(result, System.ComponentModel.ISupportInitialize).BeginInit()

        result.AllowUserToAddRows = False
        result.AllowUserToDeleteRows = False
        result.AutoGenerateColumns = False
        result.AllowUserToResizeRows = False
        result.ColumnHeadersVisible = False
        result.RowHeadersVisible = False
        result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        result.DataSource = ListBindingSource
        result.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
            {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3})
        result.ReadOnly = True
        result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        result.Size = New System.Drawing.Size(300, 220)
        result.AutoSize = False

        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        DataGridViewTextBoxColumn2.HeaderText = "Name"
        DataGridViewTextBoxColumn2.Name = "NameColumn"
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn3.DataPropertyName = "ServerAddress"
        DataGridViewTextBoxColumn3.HeaderText = "Server Address"
        DataGridViewTextBoxColumn3.Name = "ServerAddressColumn"
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf LocalUserListToGridCombo_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Name")

    End Sub

    Private Sub LocalUserListToGridCombo_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)

        If TypeOf sender Is DataGridView Then
            RemoveHandler DirectCast(sender, DataGridView).Disposed, _
                AddressOf LocalUserListToGridCombo_Disposed
            DirectCast(DirectCast(sender, DataGridView).DataSource, BindingSource).Dispose()
        End If

    End Sub



    Private Sub ComboControl_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)
        If TypeOf sender Is ComboBox Then
            RemoveHandler DirectCast(sender, ComboBox).Disposed, AddressOf ComboControl_Disposed
        ElseIf TypeOf sender Is DataGridViewComboBoxColumn Then
            RemoveHandler DirectCast(sender, DataGridViewComboBoxColumn).Disposed, _
                AddressOf ComboControl_Disposed
        ElseIf TypeOf sender Is DataGridView Then
            RemoveHandler DirectCast(sender, DataGridView).Disposed, AddressOf ComboControl_Disposed
        End If
        If TypeOf sender Is ComboBox Then
            Try
                DirectCast(DirectCast(sender, ComboBox).DataSource, BindingSource).Dispose()
            Catch ex As Exception
            End Try
        ElseIf TypeOf sender Is DataGridViewComboBoxColumn Then
            Try
                DirectCast(DirectCast(sender, DataGridViewComboBoxColumn).DataSource, BindingSource).Dispose()
            Catch ex As Exception
            End Try
        ElseIf TypeOf sender Is DataGridView Then
            Try
                DirectCast(DirectCast(sender, DataGridView).DataSource, BindingSource).Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub LanguageListComboBox_Disposed(ByVal sender As Object, _
        ByVal e As System.EventArgs)
        If TypeOf sender Is ComboBox Then
            Try
                DirectCast(DirectCast(sender, ComboBox).DataSource, BindingSource).Dispose()
            Catch ex As Exception
            End Try
            RemoveHandler DirectCast(sender, ComboBox).Disposed, _
                AddressOf LanguageListComboBox_Disposed
        Else
            Try
                DirectCast(DirectCast(sender, DataGridViewComboBoxColumn).DataSource, BindingSource).Dispose()
            Catch ex As Exception
            End Try
            RemoveHandler DirectCast(sender, DataGridViewComboBoxColumn).Disposed, _
                AddressOf LanguageListComboBox_Disposed
        End If
    End Sub

End Module