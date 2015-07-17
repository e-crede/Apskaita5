Imports AccDataAccessLayer
Imports ApskaitaObjects.ActiveReports

Friend Module CachedListsLoaders

#Region "*** CACHE BINDINGS MANAGER METHODS ***"

    Private BindingSourceList As List(Of BindingSourceItem)

    Public Function GetBindingSourceForCachedList(ByVal CachedItemBaseType As Type, _
        ByVal ParamArray FilterCriteria As Object()) As BindingSource

        Dim result As New BindingSourceItem(CachedItemBaseType, FilterCriteria)

        If BindingSourceList Is Nothing Then BindingSourceList = New List(Of BindingSourceItem)
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


    Public Function PrepareCache(ByRef CallingForm As Form, ByVal ParamArray nBaseType As Type()) As Boolean

        Try
            Using busy As New StatusBusy
                CacheObjectList.GetList(nBaseType)
            End Using
        Catch ex As Exception
            ShowError(ex)
            If Not CallingForm Is Nothing Then DisableAllControls(CallingForm)
            Return False
        End Try

        Return True

    End Function



    Public Sub LoadAssetCustomGroupInfoListToCombo(ByRef ComboObject As ComboBox, _
        ByVal AddEmptyItem As Boolean)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.ValueMember = "GetMe"
        ComboObject.DisplayMember = "Name"
        ComboObject.DataSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.LongTermAssetCustomGroupInfoList), AddEmptyItem)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadAssetCustomGroupInfoListToCombo(ByRef ComboObject As DataGridViewComboBoxColumn, _
        ByVal AddEmptyItem As Boolean)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.ValueMember = "GetMe"
        ComboObject.DisplayMember = "Name"
        ComboObject.DataSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.LongTermAssetCustomGroupInfoList), AddEmptyItem)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadPersonGroupInfoListToCombo(ByRef ComboObject As ComboBox)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.ValueMember = "GetMe"
        ComboObject.DisplayMember = "Name"
        ComboObject.DataSource = GetBindingSourceForCachedList(GetType(HelperLists.PersonGroupInfoList), True)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadPersonGroupInfoListToCombo(ByRef ComboObject As DataGridViewComboBoxColumn)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.ValueMember = "GetMe"
        ComboObject.DisplayMember = "Name"
        ComboObject.DataSource = GetBindingSourceForCachedList(GetType(HelperLists.PersonGroupInfoList), True)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadAssignableCRItemListToCombo(ByRef ComboObject As ComboBox)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.DataSource = GetBindingSourceForCachedList(GetType(HelperLists.AssignableCRItemList), True)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Sub LoadAssignableCRItemListToCombo(ByRef ComboObject As DataGridViewComboBoxColumn)

        If Not ComboObject.DataSource Is Nothing Then Exit Sub

        ComboObject.DataSource = GetBindingSourceForCachedList(GetType(HelperLists.AssignableCRItemList), True)

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

    End Sub

    Public Function LoadTaxRateListToCombo(ByRef ComboObject As ComboBox, _
        ByVal TaxType As TaxTarifType) As Boolean

        If Not ComboObject.DataSource Is Nothing Then Exit Function

        ComboObject.DataSource = GetBindingSourceForCachedList(GetType(Settings.CommonSettings), TaxType, True)

        ComboObject.ValueMember = "TaxRate"
        ComboObject.DisplayMember = "TaxRate"

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

        Return True

    End Function

    Public Function LoadTaxRateListToCombo(ByRef ComboObject As DataGridViewComboBoxColumn, _
        ByVal TaxType As TaxTarifType) As Boolean

        If Not ComboObject.DataSource Is Nothing Then Exit Function

        ComboObject.DataSource = GetBindingSourceForCachedList(GetType(Settings.CommonSettings), TaxType, True)

        ComboObject.ValueMember = "TaxRate"
        ComboObject.DisplayMember = "TaxRate"

        AddHandler ComboObject.Disposed, AddressOf ComboControl_Disposed

        Return True

    End Function

    Public Sub LoadCurrencyCodeListToComboBox(ByRef ComboControl As ComboBox, _
       ByVal AddEmptyItem As Boolean)
        ComboControl.DataSource = CurrencyCodes
    End Sub

    Public Sub LoadCurrencyCodeListToComboBox(ByRef ComboControl As DataGridViewComboBoxColumn, _
       ByVal AddEmptyItem As Boolean)
        ComboControl.DataSource = CurrencyCodes
    End Sub

    Public Sub LoadEnumHumanReadableListToComboBox(ByRef ComboControl As ComboBox, _
        ByVal EnumType As Type, ByVal AddEmptyItem As Boolean)
        ComboControl.DataSource = GetEnumValuesHumanReadableList(EnumType, AddEmptyItem)
    End Sub

    Public Sub LoadEnumLocalizedListToComboBox(ByRef comboControl As ComboBox, _
        ByVal enumType As Type, ByVal addEmptyItem As Boolean)
        Dim datasource As List(Of String) = EnumValueAttribute.GetLocalizedNameList(enumType)
        If AddEmptyItem Then datasource.Insert(0, "")
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

    Public Sub LoadDocumentSerialInfoListToCombo(ByRef ComboObject As ComboBox, _
        ByVal DocType As ApskaitaObjects.Settings.DocumentSerialType, _
        ByVal AddEmptyItem As Boolean, ByVal Reload As Boolean)

        If Not Reload Then
            ComboObject.ValueMember = "Serial"
            ComboObject.DisplayMember = "Serial"
            ComboObject.DataSource = GetBindingSourceForCachedList( _
                GetType(HelperLists.DocumentSerialInfoList), AddEmptyItem, DocType)
        End If

    End Sub

    Public Sub LoadDocumentSerialInfoListToCombo(ByRef ComboObject As DataGridViewComboBoxColumn, _
        ByVal DocType As ApskaitaObjects.Settings.DocumentSerialType, _
        ByVal AddEmptyItem As Boolean, ByVal Reload As Boolean)

        If Not Reload Then
            ComboObject.ValueMember = "Serial"
            ComboObject.DisplayMember = "Serial"
            ComboObject.ValueType = GetType(HelperLists.DocumentSerialInfo)
            ComboObject.DataSource = GetBindingSourceForCachedList( _
                GetType(HelperLists.DocumentSerialInfoList), AddEmptyItem, DocType)
        End If

    End Sub

    Public Sub LoadLanguageListToComboBox(ByRef ComboControl As ComboBox, ByVal AddEmptyItem As Boolean)

        If Not ComboControl.DataSource Is Nothing Then Exit Sub

        ComboControl.DataSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.CompanyRegionalInfoList), AddEmptyItem)

        AddHandler ComboControl.Disposed, AddressOf LanguageListComboBox_Disposed

    End Sub

    Public Sub LoadLanguageListToComboBox(ByRef ComboControl As DataGridViewComboBoxColumn, ByVal AddEmptyItem As Boolean)

        If Not ComboControl.DataSource Is Nothing Then Exit Sub

        ComboControl.DataSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.CompanyRegionalInfoList), AddEmptyItem)

        AddHandler ComboControl.Disposed, AddressOf LanguageListComboBox_Disposed

    End Sub



    Public Sub LoadCashAccountInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, ByVal AddEmptyItem As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn4 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.CashAccountInfoList), True, AddEmptyItem)

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
        DataGridViewTextBoxColumn2.DataPropertyName = "Account"
        DataGridViewTextBoxColumn2.HeaderText = "Sąskaita"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True

        DataGridViewTextBoxColumn3.DataPropertyName = "CurrencyCode"
        DataGridViewTextBoxColumn3.HeaderText = "Valiuta"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn4.DataPropertyName = "Name"
        DataGridViewTextBoxColumn4.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn4.Name = ""
        DataGridViewTextBoxColumn4.ReadOnly = True
        DataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Account")

    End Sub

    Public Sub LoadPersonInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal ShowClients As Boolean, _
        ByVal ShowSuppliers As Boolean, ByVal ShowWorkers As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.PersonInfoList), AddEmptyItem, ShowClients, ShowSuppliers, ShowWorkers)

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

        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        DataGridViewTextBoxColumn2.HeaderText = "Name"
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True

        DataGridViewTextBoxColumn3.DataPropertyName = "Code"
        DataGridViewTextBoxColumn3.HeaderText = "Code"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadAccountInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal ParamArray ClassFilter() As Integer)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.AccountInfoList), AddEmptyItem, ClassFilter)

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
            GetType(HelperLists.ServiceInfoList), AddEmptyItem, ShowSales, ShowPurchases, True)

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

    Public Sub LoadLongTermAssetInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T)

        If ComboObject.HasAtachedGrid Then Exit Sub

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
        result.DataSource = GetType(LongTermAssetOperationInfoList)
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

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Name")

    End Sub

    Public Sub LoadWorkTimeClassInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T, _
        ByVal AddEmptyItem As Boolean, ByVal ShowWithoutHours As Boolean, _
        ByVal ShowWithHours As Boolean)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ListBindingSource As BindingSource = GetBindingSourceForCachedList( _
            GetType(HelperLists.WorkTimeClassInfoList), AddEmptyItem, ShowWithoutHours, ShowWithHours)

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
            GetType(HelperLists.GoodsInfoList), True, AddEmptyItem, TradedType)

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
            GetType(HelperLists.GoodsGroupInfoList), AddEmptyItem)

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
            GetType(HelperLists.WarehouseInfoList), AddEmptyItem, True)

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
            GetType(HelperLists.ProductionCalculationInfoList), AddEmptyItem, ShowObsolete)

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

    Public Sub LoadMunicipalityCodeInfoListToGridCombo(Of T As IGridComboBox)(ByRef ComboObject As T)

        If ComboObject.HasAtachedGrid Then Exit Sub

        Dim result As New DataGridView
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn

        Dim ML As HelperLists.NameValueItemList
        Try
            ML = HelperLists.NameValueItemList.GetNameValueItemList( _
                HelperLists.SettingListType.MunicipalityCodeList)
        Catch ex As Exception
            Throw New Exception("Nepavyko įkrauti savivaldybių kodų: " & ex.Message, ex)
        End Try

        Dim ListBindingSource As New BindingSource
        ListBindingSource.DataSource = ML

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

        DataGridViewTextBoxColumn2.DataPropertyName = "Value"
        DataGridViewTextBoxColumn2.HeaderText = "Nr."
        DataGridViewTextBoxColumn2.Name = ""
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn3.DataPropertyName = "Name"
        DataGridViewTextBoxColumn3.HeaderText = "Pavadinimas"
        DataGridViewTextBoxColumn3.Name = ""
        DataGridViewTextBoxColumn3.ReadOnly = True

        result.BindingContext = ComboObject.GetBindingContext

        CType(result, System.ComponentModel.ISupportInitialize).EndInit()

        AddHandler result.Disposed, AddressOf ComboControl_Disposed

        ComboObject.SetValueMember("Value")
        ComboObject.SetNestedDataGridView(result)
        ComboObject.SetCloseOnSingleClick(True)
        ComboObject.SetFilterPropertyName("Name")

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

    'Public Sub LoadClientInfoListToSearchLookUpEdit(ByRef ComboControl As Object, _
    '    ByVal NullValuePrompt As String, ByVal AddEmptyItem As Boolean, _
    '    ByVal LoadClients As Boolean, ByVal LoadSuppliers As Boolean)

    '    Dim ComboObject As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    '    If TypeOf ComboControl Is XtraEditors.SearchLookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.SearchLookUpEdit).Properties
    '    ElseIf TypeOf ComboControl Is XtraEditors.Repository.RepositoryItemSearchLookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.Repository.RepositoryItemSearchLookUpEdit)
    '    Else
    '        Throw New NotSupportedException("Klaida. Metodas LoadClientInfoListToSearchLookUpEdit " _
    '            & "nepalaiko control tipo '" & ComboControl.GetType.FullName & "'.")
    '    End If

    '    If Not DirectCast(ComboObject.DataSource, Windows.Forms.BindingSource) Is Nothing Then Exit Sub

    '    ComboObject.BeginUpdate()

    '    ComboObject.View.OptionsBehavior.AutoPopulateColumns = False

    '    ComboObject.DataSource = GetBindingSourceForCachedList(GetType(ClientInfoList), _
    '        LoadClients, LoadSuppliers, True, AddEmptyItem)

    '    Dim colID As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("ID")
    '    Dim colName As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("Name")
    '    Dim colCode As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("Code")
    '    Dim colCodeVAT As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("CodeVAT")
    '    Dim colAddress As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("Address")
    '    Dim colCurrencyCode As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("CurrencyCode")
    '    Dim colLanguageName As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("LanguageName")
    '    Dim colVatExemption As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("VatExemption")
    '    Dim colVatExemptionAltLng As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("VatExemptionAltLng")
    '    Dim colIsObsolete As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("IsObsolete")
    '    Dim colEmail As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("Email")
    '    Dim colContacts As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("Contacts")
    '    Dim colBalanceAtBegining As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("BalanceAtBegining")
    '    Dim colIsClient As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("IsClient")
    '    Dim colIsSupplier As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("IsSupplier")
    '    Dim colIsNaturalPerson As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("IsNaturalPerson")
    '    Dim colIsCodeLocal As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("IsCodeLocal")
    '    Dim colBreedCode As DevExpress.XtraGrid.Columns.GridColumn = _
    '        ComboObject.View.Columns.AddField("BreedCode")
    '    '
    '    'ClientSearchLookUpEdit
    '    '
    '    ComboObject.AllowDropDownWhenReadOnly = Utils.DefaultBoolean.False
    '    ComboObject.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
    '    ComboObject.LookAndFeel.UseDefaultLookAndFeel = False
    '    ComboObject.LookAndFeel.UseWindowsXPTheme = True
    '    ComboObject.NullText = NullValuePrompt
    '    ComboObject.NullValuePrompt = NullValuePrompt
    '    ComboObject.NullValuePromptShowForEmptyValue = True
    '    'ComboObject.PopupFindMode = DevExpress.XtraEditors.FindMode.Always
    '    '
    '    'ClientSearchLookUpEditView
    '    '
    '    ComboObject.View.Appearance.HeaderPanel.Font = _
    '        New System.Drawing.Font(ComboObject.View.Appearance.HeaderPanel.Font, FontStyle.Bold)
    '    ComboObject.View.Appearance.HeaderPanel.Options.UseFont = True
    '    ComboObject.View.Appearance.HeaderPanel.Options.UseTextOptions = True
    '    ComboObject.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    ComboObject.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom
    '    ComboObject.View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    ComboObject.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
    '    ComboObject.View.GroupPanelText = "Įmeskite čia stulpelio (-ių), pagal kurį (-iuos) norite grupuoti, antraštę"
    '    ComboObject.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsBehavior.AllowIncrementalSearch = True
    '    ComboObject.View.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled
    '    ComboObject.View.OptionsBehavior.Editable = False
    '    ComboObject.View.OptionsBehavior.ReadOnly = True
    '    ComboObject.View.OptionsBehavior.SummariesIgnoreNullValues = True
    '    ComboObject.View.OptionsFind.AlwaysVisible = True
    '    ComboObject.View.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[True]
    '    ComboObject.View.OptionsView.ColumnAutoWidth = False
    '    ComboObject.View.OptionsView.RowAutoHeight = True
    '    ComboObject.View.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() _
    '        {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(colName, _
    '        DevExpress.Data.ColumnSortOrder.Descending)})
    '    '
    '    'colID
    '    '
    '    colID.Caption = "ID"
    '    colID.OptionsColumn.ReadOnly = True
    '    colID.OptionsFilter.AllowAutoFilter = False
    '    colID.OptionsFilter.AllowFilter = False
    '    colID.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
    '    colID.Width = 22
    '    colID.AppearanceCell.Options.UseTextOptions = True
    '    colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    '
    '    'colName
    '    '
    '    colName.Caption = "Pavadinimas"
    '    colName.OptionsColumn.ReadOnly = True
    '    colName.OptionsFilter.AllowAutoFilter = False
    '    colName.OptionsFilter.AllowFilter = False
    '    colName.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
    '    colName.Visible = True
    '    colName.VisibleIndex = 0
    '    colName.Width = 341
    '    '
    '    'colCode
    '    '
    '    colCode.Caption = "Kodas"
    '    colCode.OptionsColumn.ReadOnly = True
    '    colCode.OptionsFilter.AllowAutoFilter = False
    '    colCode.OptionsFilter.AllowFilter = False
    '    colCode.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
    '    colCode.Visible = True
    '    colCode.VisibleIndex = 1
    '    colCode.Width = 99
    '    colCode.AppearanceCell.Options.UseTextOptions = True
    '    colCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    '
    '    'colCodeVAT
    '    '
    '    colCodeVAT.Caption = "PVM mokėtojo kodas"
    '    colCodeVAT.OptionsColumn.ReadOnly = True
    '    colCodeVAT.OptionsFilter.AllowAutoFilter = False
    '    colCodeVAT.OptionsFilter.AllowFilter = False
    '    colCodeVAT.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
    '    colCodeVAT.Visible = True
    '    colCodeVAT.VisibleIndex = 2
    '    colCodeVAT.Width = 110
    '    colCodeVAT.AppearanceCell.Options.UseTextOptions = True
    '    colCodeVAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    '
    '    'colAddress
    '    '
    '    colAddress.Caption = "Adresas"
    '    colAddress.OptionsColumn.ReadOnly = True
    '    colAddress.OptionsFilter.AllowAutoFilter = False
    '    colAddress.OptionsFilter.AllowFilter = False
    '    colAddress.Width = 60
    '    '
    '    'colCurrencyCode
    '    '
    '    colCurrencyCode.Caption = "Valiuta"
    '    colCurrencyCode.OptionsColumn.ReadOnly = True
    '    colCurrencyCode.OptionsFilter.AllowAutoFilter = False
    '    colCurrencyCode.Width = 48
    '    colCurrencyCode.AppearanceCell.Options.UseTextOptions = True
    '    colCurrencyCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    '
    '    'colLanguageName
    '    '
    '    colLanguageName.Caption = "Kalba"
    '    colLanguageName.OptionsColumn.ReadOnly = True
    '    colLanguageName.OptionsFilter.AllowAutoFilter = False
    '    colLanguageName.Width = 40
    '    '
    '    'colVatExemption
    '    '
    '    colVatExemption.Caption = "PVM išimtis"
    '    colVatExemption.OptionsColumn.ReadOnly = True
    '    colVatExemption.OptionsFilter.AllowAutoFilter = False
    '    colVatExemption.OptionsFilter.AllowFilter = False
    '    colVatExemption.Width = 73
    '    '
    '    'colVatExemptionAltLng
    '    '
    '    colVatExemptionAltLng.Caption = "PVM išimtis alt. klb."
    '    colVatExemptionAltLng.OptionsColumn.ReadOnly = True
    '    colVatExemptionAltLng.OptionsFilter.AllowAutoFilter = False
    '    colVatExemptionAltLng.OptionsFilter.AllowFilter = False
    '    colVatExemptionAltLng.Width = 117
    '    '
    '    'colIsObsolete
    '    '
    '    colIsObsolete.Caption = "Istorinis"
    '    colIsObsolete.OptionsColumn.ReadOnly = True
    '    colIsObsolete.OptionsFilter.AllowAutoFilter = False
    '    colIsObsolete.OptionsFilter.AllowFilter = False
    '    colIsObsolete.Visible = True
    '    colIsObsolete.VisibleIndex = 3
    '    colIsObsolete.Width = 56
    '    '
    '    'colEmail
    '    '
    '    colEmail.Caption = "E-mail'as"
    '    colEmail.OptionsColumn.ReadOnly = True
    '    colEmail.OptionsFilter.AllowAutoFilter = False
    '    colEmail.OptionsFilter.AllowFilter = False
    '    colEmail.Width = 60
    '    '
    '    'colContacts
    '    '
    '    colContacts.Caption = "Kontaktai"
    '    colContacts.OptionsColumn.ReadOnly = True
    '    colContacts.OptionsFilter.AllowAutoFilter = False
    '    colContacts.OptionsFilter.AllowFilter = False
    '    colContacts.Width = 64
    '    '
    '    'colBalanceAtBegining
    '    '
    '    colBalanceAtBegining.Caption = "Balansas pr."
    '    colBalanceAtBegining.DisplayFormat.FormatString = "#,##.00"
    '    colBalanceAtBegining.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '    colBalanceAtBegining.OptionsColumn.ReadOnly = True
    '    colBalanceAtBegining.OptionsFilter.AllowAutoFilter = False
    '    colBalanceAtBegining.OptionsFilter.AllowFilter = False
    '    colBalanceAtBegining.Width = 77
    '    colBalanceAtBegining.AppearanceCell.Options.UseTextOptions = True
    '    colBalanceAtBegining.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    '
    '    'colIsClient
    '    '
    '    colIsClient.Caption = "Klientas"
    '    colIsClient.OptionsColumn.ReadOnly = True
    '    colIsClient.OptionsFilter.AllowAutoFilter = False
    '    colIsClient.OptionsFilter.AllowFilter = False
    '    colIsClient.Visible = True
    '    colIsClient.VisibleIndex = 4
    '    colIsClient.Width = 54
    '    '
    '    'colIsSupplier
    '    '
    '    colIsSupplier.Caption = "Tiekėjas"
    '    colIsSupplier.OptionsColumn.ReadOnly = True
    '    colIsSupplier.OptionsFilter.AllowAutoFilter = False
    '    colIsSupplier.OptionsFilter.AllowFilter = False
    '    colIsSupplier.Visible = True
    '    colIsSupplier.VisibleIndex = 5
    '    colIsSupplier.Width = 57
    '    '
    '    'colIsNaturalPerson
    '    '
    '    colIsNaturalPerson.Caption = "Fizinis asmuo"
    '    colIsNaturalPerson.OptionsColumn.ReadOnly = True
    '    colIsNaturalPerson.OptionsFilter.AllowAutoFilter = False
    '    colIsNaturalPerson.OptionsFilter.AllowFilter = False
    '    colIsNaturalPerson.Width = 84
    '    '
    '    'colIsCodeLocal
    '    '
    '    colIsCodeLocal.Caption = "Kodas tik vidinis"
    '    colIsCodeLocal.OptionsColumn.ReadOnly = True
    '    colIsCodeLocal.OptionsFilter.AllowAutoFilter = False
    '    colIsCodeLocal.OptionsFilter.AllowFilter = False
    '    colIsCodeLocal.Width = 100
    '    '
    '    'colBreedCode
    '    '
    '    colBreedCode.Caption = "Veislė"
    '    colBreedCode.OptionsColumn.ReadOnly = True
    '    colBreedCode.OptionsFilter.AllowAutoFilter = False
    '    colBreedCode.Width = 42
    '    colBalanceAtBegining.AppearanceCell.Options.UseTextOptions = True
    '    colBalanceAtBegining.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

    '    AddHandler ComboObject.Popup, AddressOf ClientInfoListComboObject_Popup
    '    AddHandler ComboObject.Disposed, AddressOf ClientInfoListComboObject_Disposed

    '    ComboObject.EndUpdate()

    'End Sub

    'Private Sub ClientInfoListComboObject_Disposed(ByVal sender As Object, _
    '    ByVal e As System.EventArgs)
    '    RemoveHandler DirectCast(sender, XtraEditors.Repository. _
    '        RepositoryItemSearchLookUpEdit).Popup, AddressOf ClientInfoListComboObject_Popup
    '    RemoveHandler DirectCast(sender, XtraEditors.Repository. _
    '        RepositoryItemSearchLookUpEdit).Disposed, AddressOf ClientInfoListComboObject_Disposed
    '    Try
    '        DirectCast(DirectCast(sender, XtraEditors.Repository. _
    '            RepositoryItemSearchLookUpEdit).DataSource, BindingSource).Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub ClientInfoListComboObject_Popup(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim ComboObject As XtraEditors.Repository.RepositoryItemSearchLookUpEdit

    '    If TypeOf sender Is XtraEditors.SearchLookUpEdit Then
    '        ComboObject = DirectCast(sender, XtraEditors.SearchLookUpEdit).Properties
    '    Else
    '        ComboObject = DirectCast(sender, XtraEditors.Repository.RepositoryItemSearchLookUpEdit)
    '    End If

    '    If ComboObject Is Nothing OrElse ComboObject.OwnerEdit Is Nothing _
    '        OrElse Not TypeOf ComboObject.OwnerEdit.EditValue Is ClientInfo _
    '        OrElse Not DirectCast(ComboObject.OwnerEdit.EditValue, ClientInfo).ID > 0 _
    '        OrElse Not DirectCast(ComboObject.OwnerEdit.EditValue, ClientInfo).IsObsolete Then _
    '        ComboObject.View.Columns("IsObsolete").FilterInfo = _
    '            New XtraGrid.Columns.ColumnFilterInfo("[IsObsolete] != True")

    '    ComboObject.View.ApplyFindFilter("")

    'End Sub


    'Public Sub LoadSerialInfoListToGridLookUpEdit(ByRef ComboControl As Object, _
    '    ByVal NullValuePrompt As String, ByVal DocumentType As SerialDocumentType, _
    '    ByVal AddEmptyItem As Boolean)

    '    Dim ComboObject As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    '    If TypeOf ComboControl Is XtraEditors.GridLookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.GridLookUpEdit).Properties
    '    ElseIf TypeOf ComboControl Is XtraEditors.Repository.RepositoryItemGridLookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.Repository.RepositoryItemGridLookUpEdit)
    '    Else
    '        Throw New NotSupportedException("Klaida. Metodas LoadSerialInfoListToSearchLookUpEdit " _
    '            & "nepalaiko control tipo '" & ComboControl.GetType.FullName & "'.")
    '    End If

    '    If Not DirectCast(ComboObject.DataSource, Windows.Forms.BindingSource) Is Nothing Then Exit Sub

    '    ComboObject.BeginUpdate()

    '    ComboObject.View.OptionsBehavior.AutoPopulateColumns = False

    '    ComboObject.DataSource = GetBindingSourceForCachedList(GetType(SerialInfoList), _
    '        DocumentType, True, AddEmptyItem)

    '    Dim colID1 As New DevExpress.XtraGrid.Columns.GridColumn
    '    Dim colSerial As New DevExpress.XtraGrid.Columns.GridColumn
    '    Dim colIsObsolete1 As New DevExpress.XtraGrid.Columns.GridColumn

    '    ComboObject.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    ComboObject.Appearance.Options.UseFont = True
    '    ComboObject.Appearance.Options.UseTextOptions = True
    '    ComboObject.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    ComboObject.DisplayMember = "Serial"
    '    ComboObject.ImmediatePopup = True
    '    ComboObject.LookAndFeel.UseDefaultLookAndFeel = False
    '    ComboObject.LookAndFeel.UseWindowsXPTheme = True
    '    ComboObject.NullText = ""
    '    ComboObject.ValueMember = "Serial"
    '    ComboObject.NullText = NullValuePrompt
    '    ComboObject.NullValuePrompt = NullValuePrompt
    '    ComboObject.NullValuePromptShowForEmptyValue = True
    '    ComboObject.PopupFormSize = New System.Drawing.Size(30, 80)

    '    '
    '    'ClientSearchLookUpEditView
    '    '
    '    ComboObject.View.Appearance.Row.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    ComboObject.View.Appearance.Row.Options.UseFont = True
    '    ComboObject.View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() _
    '        {colID1, colSerial, colIsObsolete1})
    '    ComboObject.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
    '    ComboObject.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsBehavior.AllowIncrementalSearch = True
    '    ComboObject.View.OptionsBehavior.AutoPopulateColumns = False
    '    ComboObject.View.OptionsBehavior.Editable = False
    '    ComboObject.View.OptionsBehavior.ReadOnly = True
    '    ComboObject.View.OptionsCustomization.AllowColumnMoving = False
    '    ComboObject.View.OptionsCustomization.AllowFilter = False
    '    ComboObject.View.OptionsCustomization.AllowGroup = False
    '    ComboObject.View.OptionsCustomization.AllowQuickHideColumns = False
    '    ComboObject.View.OptionsCustomization.AllowRowSizing = True
    '    ComboObject.View.OptionsFilter.AllowColumnMRUFilterList = False
    '    ComboObject.View.OptionsFilter.AllowFilterEditor = False
    '    ComboObject.View.OptionsFilter.AllowMRUFilterList = False
    '    ComboObject.View.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = False
    '    ComboObject.View.OptionsFind.AllowFindPanel = False
    '    ComboObject.View.OptionsMenu.EnableColumnMenu = False
    '    ComboObject.View.OptionsMenu.EnableFooterMenu = False
    '    ComboObject.View.OptionsMenu.EnableGroupPanelMenu = False
    '    ComboObject.View.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsMenu.ShowAutoFilterRowItem = False
    '    ComboObject.View.OptionsMenu.ShowDateTimeGroupIntervalItems = False
    '    ComboObject.View.OptionsMenu.ShowGroupSortSummaryItems = False
    '    ComboObject.View.OptionsMenu.ShowSplitItem = False
    '    ComboObject.View.OptionsSelection.EnableAppearanceFocusedCell = False
    '    ComboObject.View.OptionsView.RowAutoHeight = True
    '    ComboObject.View.OptionsView.ShowColumnHeaders = False
    '    ComboObject.View.OptionsView.ShowGroupPanel = False

    '    '
    '    'colID1
    '    '
    '    colID1.FieldName = "ID"
    '    colID1.Name = "colID1"
    '    colID1.OptionsColumn.ReadOnly = True
    '    '
    '    'colSerial
    '    '
    '    colSerial.AppearanceCell.Options.UseTextOptions = True
    '    colSerial.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    colSerial.FieldName = "Serial"
    '    colSerial.Name = "colSerial"
    '    colSerial.OptionsColumn.ReadOnly = True
    '    colSerial.Visible = True
    '    colSerial.VisibleIndex = 0
    '    '
    '    'colIsObsolete1
    '    '
    '    colIsObsolete1.FieldName = "IsObsolete"
    '    colIsObsolete1.Name = "colIsObsolete1"
    '    colIsObsolete1.OptionsColumn.ReadOnly = True


    '    AddHandler ComboObject.Popup, AddressOf SerialInfoListComboObject_Popup
    '    AddHandler ComboObject.Disposed, AddressOf SerialInfoListComboObject_Disposed

    '    ComboObject.EndUpdate()

    'End Sub

    'Private Sub SerialInfoListComboObject_Disposed(ByVal sender As Object, _
    '    ByVal e As System.EventArgs)
    '    RemoveHandler DirectCast(sender, XtraEditors.Repository. _
    '        RepositoryItemGridLookUpEdit).Popup, AddressOf SerialInfoListComboObject_Popup
    '    RemoveHandler DirectCast(sender, XtraEditors.Repository. _
    '        RepositoryItemGridLookUpEdit).Disposed, AddressOf SerialInfoListComboObject_Disposed
    '    Try
    '        DirectCast(DirectCast(sender, XtraEditors.Repository. _
    '            RepositoryItemGridLookUpEdit).DataSource, BindingSource).Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub SerialInfoListComboObject_Popup(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim ComboObject As XtraEditors.Repository.RepositoryItemGridLookUpEdit 

    '    If TypeOf sender Is XtraEditors.GridLookUpEdit Then
    '        ComboObject = DirectCast(sender, XtraEditors.GridLookUpEdit).Properties
    '    Else
    '        ComboObject = DirectCast(sender, XtraEditors.Repository.RepositoryItemGridLookUpEdit)
    '    End If

    '    If ComboObject Is Nothing OrElse ComboObject.OwnerEdit Is Nothing _
    '        OrElse Not TypeOf ComboObject.OwnerEdit.EditValue Is SerialInfo _
    '        OrElse Not DirectCast(ComboObject.OwnerEdit.EditValue, SerialInfo).ID > 0 _
    '        OrElse Not DirectCast(ComboObject.OwnerEdit.EditValue, SerialInfo).IsObsolete Then _
    '        ComboObject.View.Columns("IsObsolete").FilterInfo = _
    '            New XtraGrid.Columns.ColumnFilterInfo("[IsObsolete] != True")

    'End Sub


    'Public Sub LoadTaxRateListToGridLookUpEdit(ByRef ComboControl As Object, _
    '    ByVal NullValuePrompt As String, ByVal TaxType As Settings.TaxTarifType)

    '    Dim ComboObject As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    '    If TypeOf ComboControl Is XtraEditors.GridLookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.GridLookUpEdit).Properties
    '    ElseIf TypeOf ComboControl Is XtraEditors.Repository.RepositoryItemGridLookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.Repository.RepositoryItemGridLookUpEdit)
    '    Else
    '        Throw New NotSupportedException("Klaida. Metodas LoadTaxRateListToSearchLookUpEdit " _
    '            & "nepalaiko control tipo '" & ComboControl.GetType.FullName & "'.")
    '    End If

    '    If Not DirectCast(ComboObject.DataSource, Windows.Forms.BindingSource) Is Nothing Then Exit Sub

    '    ComboObject.BeginUpdate()

    '    ComboObject.View.OptionsBehavior.AutoPopulateColumns = False

    '    ComboObject.DataSource = GetBindingSourceForCachedList(GetType(Settings.CommonSettings), TaxType, True)

    '    Dim colTaxRate As New DevExpress.XtraGrid.Columns.GridColumn
    '    Dim colIsObsolete1 As New DevExpress.XtraGrid.Columns.GridColumn

    '    ComboObject.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    ComboObject.Appearance.Options.UseFont = True
    '    ComboObject.Appearance.Options.UseTextOptions = True
    '    ComboObject.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    ComboObject.ImmediatePopup = True
    '    ComboObject.LookAndFeel.UseDefaultLookAndFeel = False
    '    ComboObject.LookAndFeel.UseWindowsXPTheme = True
    '    ComboObject.DisplayFormat.FormatString = "##,0.00"
    '    ComboObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '    ComboObject.DisplayMember = "TaxRate"
    '    ComboObject.EditFormat.FormatString = "##,0.00"
    '    ComboObject.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '    ComboObject.PopupFormSize = New System.Drawing.Size(30, 80)
    '    ComboObject.ValueMember = "TaxRate"
    '    ComboObject.NullValuePromptShowForEmptyValue = False
    '    ComboObject.NullText = ""
    '    ComboObject.NullValuePrompt = ""

    '    '
    '    'ClientSearchLookUpEditView
    '    '
    '    ComboObject.View.Appearance.Row.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    ComboObject.View.Appearance.Row.Options.UseFont = True
    '    ComboObject.View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colTaxRate, colIsObsolete1})
    '    ComboObject.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
    '    ComboObject.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsBehavior.AllowIncrementalSearch = True
    '    ComboObject.View.OptionsBehavior.AutoPopulateColumns = False
    '    ComboObject.View.OptionsBehavior.Editable = False
    '    ComboObject.View.OptionsBehavior.ReadOnly = True
    '    ComboObject.View.OptionsCustomization.AllowColumnMoving = False
    '    ComboObject.View.OptionsCustomization.AllowFilter = False
    '    ComboObject.View.OptionsCustomization.AllowGroup = False
    '    ComboObject.View.OptionsCustomization.AllowQuickHideColumns = False
    '    ComboObject.View.OptionsCustomization.AllowRowSizing = True
    '    ComboObject.View.OptionsFilter.AllowColumnMRUFilterList = False
    '    ComboObject.View.OptionsFilter.AllowFilterEditor = False
    '    ComboObject.View.OptionsFilter.AllowMRUFilterList = False
    '    ComboObject.View.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = False
    '    ComboObject.View.OptionsFind.AllowFindPanel = False
    '    ComboObject.View.OptionsMenu.EnableColumnMenu = False
    '    ComboObject.View.OptionsMenu.EnableFooterMenu = False
    '    ComboObject.View.OptionsMenu.EnableGroupPanelMenu = False
    '    ComboObject.View.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.View.OptionsMenu.ShowAutoFilterRowItem = False
    '    ComboObject.View.OptionsMenu.ShowDateTimeGroupIntervalItems = False
    '    ComboObject.View.OptionsMenu.ShowGroupSortSummaryItems = False
    '    ComboObject.View.OptionsMenu.ShowSplitItem = False
    '    ComboObject.View.OptionsSelection.EnableAppearanceFocusedCell = False
    '    ComboObject.View.OptionsView.RowAutoHeight = True
    '    ComboObject.View.OptionsView.ShowColumnHeaders = False
    '    ComboObject.View.OptionsView.ShowGroupPanel = False

    '    '
    '    'colTaxRate
    '    '
    '    colTaxRate.AppearanceCell.Options.UseTextOptions = True
    '    colTaxRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    colTaxRate.DisplayFormat.FormatString = "##,0.00"
    '    colTaxRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '    colTaxRate.FieldName = "TaxRate"
    '    colTaxRate.Name = "colTaxRate"
    '    colTaxRate.OptionsColumn.AllowEdit = False
    '    colTaxRate.OptionsColumn.ReadOnly = True
    '    colTaxRate.Visible = True
    '    colTaxRate.VisibleIndex = 0
    '    '
    '    'colIsObsolete1
    '    '
    '    colIsObsolete1.FieldName = "IsObsolete"
    '    colIsObsolete1.Name = "colIsObsolete1"


    '    AddHandler ComboObject.Popup, AddressOf TaxRateListComboObject_Popup
    '    AddHandler ComboObject.Disposed, AddressOf TaxRateListComboObject_Disposed

    '    ComboObject.EndUpdate()

    'End Sub

    'Private Sub TaxRateListComboObject_Disposed(ByVal sender As Object, _
    '    ByVal e As System.EventArgs)
    '    RemoveHandler DirectCast(sender, XtraEditors.Repository. _
    '        RepositoryItemGridLookUpEdit).Popup, AddressOf TaxRateListComboObject_Popup
    '    RemoveHandler DirectCast(sender, XtraEditors.Repository. _
    '        RepositoryItemGridLookUpEdit).Disposed, AddressOf TaxRateListComboObject_Disposed
    '    Try
    '        DirectCast(DirectCast(sender, XtraEditors.Repository. _
    '            RepositoryItemGridLookUpEdit).DataSource, BindingSource).Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub TaxRateListComboObject_Popup(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim ComboObject As XtraEditors.Repository.RepositoryItemGridLookUpEdit 

    '    If TypeOf sender Is XtraEditors.GridLookUpEdit Then
    '        ComboObject = DirectCast(sender, XtraEditors.GridLookUpEdit).Properties
    '    Else
    '        ComboObject = DirectCast(sender, XtraEditors.Repository.RepositoryItemGridLookUpEdit)
    '    End If

    '    If ComboObject Is Nothing OrElse ComboObject.OwnerEdit Is Nothing _
    '        OrElse Not TypeOf ComboObject.OwnerEdit.EditValue Is Settings.TaxRate _
    '        OrElse Not DirectCast(ComboObject.OwnerEdit.EditValue, Settings.TaxRate).IsObsolete Then _
    '        ComboObject.View.Columns("IsObsolete").FilterInfo = _
    '            New XtraGrid.Columns.ColumnFilterInfo("[IsObsolete] != True")

    'End Sub


    'Public Sub LoadLanguageListToLookUpEdit(ByRef ComboControl As Object, _
    '    ByVal NullValuePrompt As String, ByVal AddEmptyItem As Boolean)

    '    Dim ComboObject As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    '    If TypeOf ComboControl Is XtraEditors.LookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.LookUpEdit).Properties
    '    ElseIf TypeOf ComboControl Is XtraEditors.Repository.RepositoryItemLookUpEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.Repository.RepositoryItemLookUpEdit)
    '    Else
    '        Throw New NotSupportedException("Klaida. Metodas LoadTaxRateListToSearchLookUpEdit " _
    '            & "nepalaiko control tipo '" & ComboControl.GetType.FullName & "'.")
    '    End If

    '    If Not DirectCast(ComboObject.DataSource, Windows.Forms.BindingSource) Is Nothing Then Exit Sub

    '    ComboObject.BeginUpdate()

    '    ComboObject.DataSource = GetBindingSourceForCachedList(GetType(CompanyRegionalInfoList), AddEmptyItem)
    '    ComboObject.PopulateColumns()

    '    ComboObject.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[False]
    '    ComboObject.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    ComboObject.Appearance.Options.UseFont = True
    '    ComboObject.Appearance.Options.UseTextOptions = True
    '    ComboObject.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '    ComboObject.ImmediatePopup = True
    '    ComboObject.LookAndFeel.UseDefaultLookAndFeel = False
    '    ComboObject.LookAndFeel.UseWindowsXPTheme = True
    '    ComboObject.PopupFormSize = New System.Drawing.Size(100, 60)
    '    ComboObject.NullValuePromptShowForEmptyValue = False
    '    ComboObject.NullText = ""
    '    ComboObject.NullValuePrompt = ""
    '    ComboObject.ShowFooter = False
    '    ComboObject.ShowHeader = False

    '    AddHandler ComboObject.Disposed, AddressOf LanguageListComboObject_Disposed

    '    ComboObject.EndUpdate()

    'End Sub

    'Private Sub LanguageListComboObject_Disposed(ByVal sender As Object, _
    '    ByVal e As System.EventArgs)
    '    RemoveHandler DirectCast(sender, XtraEditors.Repository. _
    '        RepositoryItemLookUpEdit).Disposed, AddressOf LanguageListComboObject_Disposed
    '    Try
    '        DirectCast(DirectCast(sender, XtraEditors.Repository. _
    '            RepositoryItemLookUpEdit).DataSource, BindingSource).Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Sub


    'Public Sub LoadCurrencyCodeListToComboBoxEdit(ByRef ComboControl As Object, _
    '    ByVal AddEmptyItem As Boolean)

    '    Dim ComboObject As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    '    If TypeOf ComboControl Is XtraEditors.ComboBoxEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.ComboBoxEdit).Properties
    '    ElseIf TypeOf ComboControl Is XtraEditors.Repository.RepositoryItemComboBox Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.Repository.RepositoryItemComboBox)
    '    Else
    '        Throw New NotSupportedException("Klaida. Metodas LoadCurrencyCodeListToComboBoxEdit " _
    '            & "nepalaiko control tipo '" & ComboControl.GetType.FullName & "'.")
    '    End If

    '    ComboObject.BeginUpdate()

    '    ComboObject.Items.Clear()

    '    If AddEmptyItem Then ComboObject.Items.Add("")

    '    For Each s As String In CurrencyCodes
    '        ComboObject.Items.Add(s)
    '    Next

    '    ComboObject.NullText = ""
    '    ComboObject.NullValuePrompt = ""
    '    ComboObject.NullValuePromptShowForEmptyValue = False
    '    ComboObject.AllowDropDownWhenReadOnly = Utils.DefaultBoolean.False
    '    ComboObject.LookAndFeel.UseDefaultLookAndFeel = False
    '    ComboObject.LookAndFeel.UseWindowsXPTheme = True

    '    ComboObject.EndUpdate()

    'End Sub

    'Public Sub LoadEnumHumanReadableListToComboBoxEdit(ByRef ComboControl As Object, _
    '    ByVal EnumType As Type, ByVal AddEmptyItem As Boolean)

    '    Dim ComboObject As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    '    If TypeOf ComboControl Is XtraEditors.ComboBoxEdit Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.ComboBoxEdit).Properties
    '    ElseIf TypeOf ComboControl Is XtraEditors.Repository.RepositoryItemComboBox Then
    '        ComboObject = DirectCast(ComboControl, XtraEditors.Repository.RepositoryItemComboBox)
    '    Else
    '        Throw New NotSupportedException("Klaida. Metodas LoadEnumHumanReadableListToComboBoxEdit " _
    '            & "nepalaiko control tipo '" & ComboControl.GetType.FullName & "'.")
    '    End If

    '    ComboObject.BeginUpdate()

    '    ComboObject.Items.Clear()

    '    For Each s As String In GetEnumValuesHumanReadableList(EnumType, AddEmptyItem)
    '        ComboObject.Items.Add(s)
    '    Next

    '    ComboObject.NullText = ""
    '    ComboObject.NullValuePrompt = ""
    '    ComboObject.NullValuePromptShowForEmptyValue = False
    '    ComboObject.AllowDropDownWhenReadOnly = Utils.DefaultBoolean.False
    '    ComboObject.LookAndFeel.UseDefaultLookAndFeel = False
    '    ComboObject.LookAndFeel.UseWindowsXPTheme = True

    '    ComboObject.EndUpdate()

    'End Sub

End Module