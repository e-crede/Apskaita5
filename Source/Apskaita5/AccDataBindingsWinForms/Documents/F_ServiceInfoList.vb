Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.Documents
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing

Friend Class F_ServiceInfoList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.ServiceInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of ServiceInfoItemList)
    Private _ListViewManager As DataListViewEditControlManager(Of ServiceInfoItem)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_ServiceInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        RefreshButton.PerformClick()

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of ServiceInfoItem) _
                (ServiceInfoListDataListView, ContextMenuStrip1, Nothing, _
                 Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Keisti", "Keisti paslaugos duomenis.", _
                AddressOf ChangeItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti paslaugos duomenis iš duomenų bazės.", _
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(ChangeItem_MenuItem, AddressOf ChangeItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)
            _ListViewManager.AddMenuItemHandler(NewItem_MenuItem, AddressOf NewItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' ServiceInfoItemList.GetServiceInfoItemList()
            _FormManager = New CslaActionExtenderReportForm(Of ServiceInfoItemList) _
                (Me, ServiceInfoListBindingSource, Nothing, _RequiredCachedLists, RefreshButton, _
                 ProgressFiller1, "GetServiceInfoItemList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(ServiceInfoListDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        NewObjectButton.Enabled = Service.CanAddObject

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return New Object() {}
    End Function

    Private Sub NewObjectButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles NewObjectButton.Click
        NewItem(Nothing)
    End Sub

    Private Sub ChangeItem(ByVal item As ServiceInfoItem)
        If item Is Nothing Then Exit Sub
        ' Service.GetService(item.ID)
        _QueryManager.InvokeQuery(Of Service)(Nothing, "GetService", True, AddressOf OpenObjectEditForm, item.ID)
    End Sub

    Private Sub DeleteItem(ByVal item As ServiceInfoItem)

        If item Is Nothing Then Exit Sub

        If CheckIfObjectEditFormOpen(Of Service)(item.ID, True, True) Then Exit Sub

        If Not YesOrNo("Ar tikrai norite pašalinti pasirinktos paslaugos " & _
            "duomenis iš duomenų bazės?") Then Exit Sub

        ' Service.DeleteService(item.ID)
        _QueryManager.InvokeQuery(Of Service)(Nothing, "DeleteService", False, AddressOf OnItemDeleted, item.ID)

    End Sub

    Private Sub OnItemDeleted(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub
        If Not YesOrNo("Paslaugos duomenys sėkmingai pašalinti iš įmonės duomenų bazės. Atnaujinti sąrašą?") Then Exit Sub
        RefreshButton.PerformClick()
    End Sub

    Private Sub NewItem(ByVal item As ServiceInfoItem)
        OpenNewForm(Of Service)()
    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetMailDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems
        Return Nothing
    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick
        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, False, 0, "PaslauguSarasas", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "PaslauguSarasas", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

End Class