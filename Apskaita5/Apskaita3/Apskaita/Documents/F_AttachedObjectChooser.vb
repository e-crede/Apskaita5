Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.HelperLists
Public Class F_AttachedObjectChooser

    Private Loading As Boolean = True
    Private _SelectedType As InvoiceAttachedObjectType = InvoiceAttachedObjectType.LongTermAssetPurchase
    Private _SelectedService As ServiceInfo = Nothing
    Private _SelectedLongTermAsset As LongTermAssetInfo = Nothing
    Private _SelectedGoods As GoodsInfo = Nothing
    Private _SelectedWarehouse As WarehouseInfo = Nothing
    Private _result As Boolean = False
    Private _ForInvoiceReceived As Boolean = False


    Public ReadOnly Property SelectedType() As InvoiceAttachedObjectType
        Get
            Return _SelectedType
        End Get
    End Property

    Public ReadOnly Property SelectedService() As ServiceInfo
        Get
            Return _SelectedService
        End Get
    End Property

    Public ReadOnly Property SelectedLongTermAsset() As LongTermAssetInfo
        Get
            Return _SelectedLongTermAsset
        End Get
    End Property

    Public ReadOnly Property SelectedGoods() As GoodsInfo
        Get
            Return _SelectedGoods
        End Get
    End Property

    Public ReadOnly Property SelectedWarehouse() As WarehouseInfo
        Get
            Return _SelectedWarehouse
        End Get
    End Property

    Public ReadOnly Property AttachedObjectParentID() As Integer
        Get
            If Not _SelectedLongTermAsset Is Nothing AndAlso _
                _SelectedLongTermAsset.ID > 0 Then Return _SelectedLongTermAsset.ID
            If Not _SelectedService Is Nothing AndAlso _SelectedService.ID > 0 Then _
                Return _SelectedService.ID
            If Not _SelectedGoods Is Nothing AndAlso _SelectedGoods.ID > 0 Then _
                Return _SelectedGoods.ID
            Return 0
        End Get
    End Property

    Public ReadOnly Property AttachedObjectWarehouseID() As Integer
        Get
            If Not _SelectedWarehouse Is Nothing AndAlso _
                _SelectedWarehouse.ID > 0 Then Return _SelectedWarehouse.ID
            Return 0
        End Get
    End Property

    Public ReadOnly Property Result() As Boolean
        Get
            Return _result
        End Get
    End Property


    Public Sub New(ByVal ForInvoiceReceived As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _ForInvoiceReceived = ForInvoiceReceived

    End Sub


    Private Sub F_AttachedObjectChooser_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If Me.WindowState = FormWindowState.Maximized Then Me.WindowState = FormWindowState.Normal

        If Loading Then
            Loading = False
            Exit Sub
        End If

        If Not PrepareCache(Me, GetType(HelperLists.ServiceInfoList), _
            GetType(HelperLists.GoodsInfoList), GetType(HelperLists.WarehouseInfoList)) Then Exit Sub

    End Sub

    Private Sub F_AttachedObjectChooser_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GetFormLayout(Me)
    End Sub

    Private Sub F_AttachedObjectChooser_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        SetFormLayout(Me)

        If _ForInvoiceReceived AndAlso MyCustomSettings.DefaultInvoiceReceivedItemIsGoods Then
            GoodsAcquisitionRadioButton.Checked = True
            BarCodeTextBox.Focus()
        ElseIf Not _ForInvoiceReceived AndAlso MyCustomSettings.DefaultInvoiceMadeItemIsGoods Then
            GoodsTransferRadioButton.Checked = True
            BarCodeTextBox.Focus()
        End If

    End Sub


    Private Sub IOkButton_Click(ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles IOkButton.Click

        If LongTermAssetAcquisitionRadioButton.Checked Then

            _SelectedType = InvoiceAttachedObjectType.LongTermAssetPurchase

        ElseIf LongTermAssetTransferRadioButton.Checked OrElse _
            LongTermAssetAcquisitionValueChangeRadioButton.Checked Then

            _SelectedLongTermAsset = Nothing

            If Not LongTermAssetAccGridComboBox.SelectedValue Is Nothing AndAlso _
                TypeOf LongTermAssetAccGridComboBox.SelectedValue Is LongTermAssetInfo AndAlso _
                DirectCast(LongTermAssetAccGridComboBox.SelectedValue, LongTermAssetInfo).ID > 0 Then _
                _SelectedLongTermAsset = DirectCast(LongTermAssetAccGridComboBox.SelectedValue, LongTermAssetInfo)

            If _SelectedLongTermAsset Is Nothing Then
                MsgBox("Klaida. Nepasirinktas ilgalaikis turtas.", MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If

            _SelectedType = InvoiceAttachedObjectType.LongTermAssetSale
            If LongTermAssetAcquisitionValueChangeRadioButton.Checked Then _
                _SelectedType = InvoiceAttachedObjectType.LongTermAssetAcquisitionValueChange

        ElseIf ServicesRadioButton.Checked Then

            _SelectedService = Nothing

            If Not ServicesAccGridComboBox.SelectedValue Is Nothing AndAlso _
                TypeOf ServicesAccGridComboBox.SelectedValue Is ServiceInfo AndAlso _
                DirectCast(ServicesAccGridComboBox.SelectedValue, ServiceInfo).ID > 0 Then _
                _SelectedService = DirectCast(ServicesAccGridComboBox.SelectedValue, ServiceInfo)

            If _SelectedService Is Nothing Then
                MsgBox("Klaida. Nepasirinkta paslauga.", MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If

            _SelectedType = InvoiceAttachedObjectType.Service

        ElseIf GoodsAcquisitionRadioButton.Checked OrElse GoodsAdditionalCostsRadioButton.Checked _
            OrElse GoodsDiscountRadioButton.Checked OrElse GoodsTransferRadioButton.Checked Then

            _SelectedGoods = Nothing

            If Not GoodsInfoListAccGridComboBox.SelectedValue Is Nothing AndAlso _
                TypeOf GoodsInfoListAccGridComboBox.SelectedValue Is GoodsInfo AndAlso _
                DirectCast(GoodsInfoListAccGridComboBox.SelectedValue, GoodsInfo).ID > 0 Then _
                _SelectedGoods = DirectCast(GoodsInfoListAccGridComboBox.SelectedValue, GoodsInfo)

            If _SelectedGoods Is Nothing Then
                MsgBox("Klaida. Nepasirinktos prekės.", MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If

            _SelectedWarehouse = Nothing

            If Not WarehouseInfoListAccGridComboBox.SelectedValue Is Nothing AndAlso _
                TypeOf WarehouseInfoListAccGridComboBox.SelectedValue Is WarehouseInfo AndAlso _
                DirectCast(WarehouseInfoListAccGridComboBox.SelectedValue, WarehouseInfo).ID > 0 Then _
                _SelectedWarehouse = DirectCast(WarehouseInfoListAccGridComboBox.SelectedValue, WarehouseInfo)

            If _SelectedWarehouse Is Nothing Then
                MsgBox("Klaida. Nepasirinktas sandėlis.", MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If

            If GoodsAcquisitionRadioButton.Checked Then
                _SelectedType = InvoiceAttachedObjectType.GoodsAcquisition
            ElseIf GoodsAdditionalCostsRadioButton.Checked Then
                _SelectedType = InvoiceAttachedObjectType.GoodsConsignmentAdditionalCosts
            ElseIf GoodsDiscountRadioButton.Checked Then
                _SelectedType = InvoiceAttachedObjectType.GoodsConsignmentDiscount
            Else
                _SelectedType = InvoiceAttachedObjectType.GoodsTransfer
            End If

        Else

            Exit Sub

        End If

        _result = True

        Me.Hide()
        Me.Close()

    End Sub

    Private Sub ICancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ICancelButton.Click

        Me.Hide()
        Me.Close()

    End Sub


    Private Sub RefreshLongTermAssetInfoListButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshLongTermAssetInfoListButton.Click

        Dim assetList As LongTermAssetInfoList = Nothing

        Try
            Using busy As New StatusBusy
                assetList = LongTermAssetInfoList.GetLongTermAssetInfoList( _
                    LongTermAssetAcquisitionDateTimePicker.Value, _
                    LongTermAssetAcquisitionDateTimePicker.Value, Nothing)
            End Using
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        LongTermAssetAccGridComboBox.AttachedGrid.DataSource = assetList

    End Sub

    Private Sub SelectGoodsInfoButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles SelectGoodsInfoButton.Click
        GoodsInfoListAccGridComboBox.SelectedValue = GoodsInfoList.GetList. _
            GetItemByBarCode(Me.BarCodeTextBox.Text.Trim)
    End Sub

    Private Sub BarCodeTextBox_TextChanged(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles BarCodeTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SelectGoodsInfoButton_Click(sender, New EventArgs)
        End If
    End Sub

    Private CheckedAreChanging As Boolean = False

    Private Sub AttachedObjectTypeRadioButton_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles LongTermAssetAcquisitionRadioButton.CheckedChanged, _
        LongTermAssetTransferRadioButton.CheckedChanged, ServicesRadioButton.CheckedChanged, _
        LongTermAssetAcquisitionValueChangeRadioButton.CheckedChanged, _
        GoodsAcquisitionRadioButton.CheckedChanged, GoodsAdditionalCostsRadioButton.CheckedChanged, _
        GoodsDiscountRadioButton.CheckedChanged, GoodsTransferRadioButton.CheckedChanged

        If CheckedAreChanging Then Exit Sub

        CheckedAreChanging = True

        If Not sender Is Nothing AndAlso sender Is LongTermAssetTransferRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            LongTermAssetAcquisitionRadioButton.Checked = False
            LongTermAssetAcquisitionValueChangeRadioButton.Checked = False
            ServicesRadioButton.Checked = False
            GoodsAcquisitionRadioButton.Checked = False
            GoodsAdditionalCostsRadioButton.Checked = False
            GoodsDiscountRadioButton.Checked = False
            GoodsTransferRadioButton.Checked = False
            LongTermAssetAcquisitionDateTimePicker.Focus()

        ElseIf Not sender Is Nothing AndAlso sender Is LongTermAssetAcquisitionRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            LongTermAssetTransferRadioButton.Checked = False
            LongTermAssetAcquisitionValueChangeRadioButton.Checked = False
            ServicesRadioButton.Checked = False
            GoodsAcquisitionRadioButton.Checked = False
            GoodsAdditionalCostsRadioButton.Checked = False
            GoodsDiscountRadioButton.Checked = False
            GoodsTransferRadioButton.Checked = False
        ElseIf Not sender Is Nothing AndAlso sender Is ServicesRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            LongTermAssetTransferRadioButton.Checked = False
            LongTermAssetAcquisitionValueChangeRadioButton.Checked = False
            LongTermAssetAcquisitionRadioButton.Checked = False
            GoodsAcquisitionRadioButton.Checked = False
            GoodsAdditionalCostsRadioButton.Checked = False
            GoodsDiscountRadioButton.Checked = False
            GoodsTransferRadioButton.Checked = False
            ServicesAccGridComboBox.Focus()
        ElseIf Not sender Is Nothing AndAlso sender Is LongTermAssetAcquisitionValueChangeRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            LongTermAssetAcquisitionRadioButton.Checked = False
            ServicesRadioButton.Checked = False
            LongTermAssetTransferRadioButton.Checked = False
            GoodsAcquisitionRadioButton.Checked = False
            GoodsAdditionalCostsRadioButton.Checked = False
            GoodsDiscountRadioButton.Checked = False
            GoodsTransferRadioButton.Checked = False
            BarCodeTextBox.Focus()
        ElseIf Not sender Is Nothing AndAlso sender Is GoodsAcquisitionRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            GoodsAdditionalCostsRadioButton.Checked = False
            GoodsDiscountRadioButton.Checked = False
            GoodsTransferRadioButton.Checked = False
            LongTermAssetAcquisitionRadioButton.Checked = False
            ServicesRadioButton.Checked = False
            LongTermAssetTransferRadioButton.Checked = False
            LongTermAssetAcquisitionValueChangeRadioButton.Checked = False
            SetWarehouse()
            BarCodeTextBox.Focus()
        ElseIf Not sender Is Nothing AndAlso sender Is GoodsAdditionalCostsRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            GoodsAcquisitionRadioButton.Checked = False
            GoodsDiscountRadioButton.Checked = False
            GoodsTransferRadioButton.Checked = False
            LongTermAssetAcquisitionRadioButton.Checked = False
            ServicesRadioButton.Checked = False
            LongTermAssetTransferRadioButton.Checked = False
            LongTermAssetAcquisitionValueChangeRadioButton.Checked = False
            SetWarehouse()
            BarCodeTextBox.Focus()
        ElseIf Not sender Is Nothing AndAlso sender Is GoodsDiscountRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            GoodsAcquisitionRadioButton.Checked = False
            GoodsAdditionalCostsRadioButton.Checked = False
            GoodsTransferRadioButton.Checked = False
            LongTermAssetAcquisitionRadioButton.Checked = False
            ServicesRadioButton.Checked = False
            LongTermAssetTransferRadioButton.Checked = False
            LongTermAssetAcquisitionValueChangeRadioButton.Checked = False
            SetWarehouse()
            BarCodeTextBox.Focus()
        ElseIf Not sender Is Nothing AndAlso sender Is GoodsTransferRadioButton _
            AndAlso DirectCast(sender, RadioButton).Checked Then
            GoodsAcquisitionRadioButton.Checked = False
            GoodsAdditionalCostsRadioButton.Checked = False
            GoodsDiscountRadioButton.Checked = False
            LongTermAssetAcquisitionRadioButton.Checked = False
            ServicesRadioButton.Checked = False
            LongTermAssetTransferRadioButton.Checked = False
            LongTermAssetAcquisitionValueChangeRadioButton.Checked = False
            SetWarehouse()
            BarCodeTextBox.Focus()
        End If

        LongTermAssetAcquisitionDateTimePicker.Enabled = LongTermAssetTransferRadioButton.Checked _
            OrElse LongTermAssetAcquisitionValueChangeRadioButton.Checked
        RefreshLongTermAssetInfoListButton.Enabled = LongTermAssetTransferRadioButton.Checked _
            OrElse LongTermAssetAcquisitionValueChangeRadioButton.Checked
        LongTermAssetAccGridComboBox.Enabled = LongTermAssetTransferRadioButton.Checked _
            OrElse LongTermAssetAcquisitionValueChangeRadioButton.Checked
        ServicesAccGridComboBox.Enabled = ServicesRadioButton.Checked
        GoodsInfoListAccGridComboBox.Enabled = GoodsAcquisitionRadioButton.Checked OrElse _
            GoodsAdditionalCostsRadioButton.Checked OrElse GoodsDiscountRadioButton.Checked _
            OrElse GoodsTransferRadioButton.Checked
        SelectGoodsInfoButton.Enabled = GoodsInfoListAccGridComboBox.Enabled
        BarCodeTextBox.Enabled = GoodsInfoListAccGridComboBox.Enabled
        WarehouseInfoListAccGridComboBox.Enabled = GoodsAcquisitionRadioButton.Checked OrElse _
            GoodsAdditionalCostsRadioButton.Checked OrElse GoodsDiscountRadioButton.Checked _
            OrElse GoodsTransferRadioButton.Checked

        CheckedAreChanging = False

    End Sub


    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.ServiceInfoList), _
            GetType(HelperLists.GoodsInfoList), GetType(HelperLists.WarehouseInfoList)) Then Return False

        Try

            LoadLongTermAssetInfoListToGridCombo(LongTermAssetAccGridComboBox)
            LoadServiceInfoListToGridCombo(ServicesAccGridComboBox, True, True, True)
            LoadGoodsInfoListToGridCombo(GoodsInfoListAccGridComboBox, True, TradedItemType.All)
            LoadWarehouseInfoListToGridCombo(WarehouseInfoListAccGridComboBox, True)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub SetWarehouse()

        Try
            If WarehouseInfoList.GetList.Count = 1 Then
                WarehouseInfoListAccGridComboBox.SelectedValue = WarehouseInfoList.GetList.Item(0)
            ElseIf WarehouseInfoList.GetList.Count = 2 Then
                WarehouseInfoListAccGridComboBox.SelectedValue = WarehouseInfoList.GetList.Item(1)
            End If
        Catch ex As Exception
        End Try

    End Sub

End Class