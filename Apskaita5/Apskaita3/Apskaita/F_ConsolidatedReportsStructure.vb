Imports ApskaitaObjects.General
Imports AccDataAccessLayer

Public Class F_ConsolidatedReportsStructure

    Private Obj As ConsolidatedReport = Nothing
    Private _CurrentItem As ConsolidatedReportItem = Nothing


    Private Sub F_ConsolidatedReportsStructure_Activated(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Activated
        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub F_ConsolidatedReportsStructure_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MyCustomSettings.GetFormLayout(Me)
    End Sub

    Private Sub F_ConsolidatedReportsStructure_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        ToolTip1.SetToolTip(Me.SaveAsFileButton, "Išsaugoti duomenis faile.")
        ToolTip1.SetToolTip(Me.SaveInDatabaseButton, "Išsaugoti duomenis įmonės duomenų bazėje.")
        ToolTip1.SetToolTip(Me.FetchFromDatabaseButton, "Gauti duomenis iš įmonės duomenų bazės.")
        ToolTip1.SetToolTip(Me.GetNewFormButton, "Nauja atskaitomybės struktūra.")
        ToolTip1.SetToolTip(Me.OpenFileButton, "Atidaryti failą su duomenimis.")
        ToolTip1.SetToolTip(Me.AddItemButton, "Pridėti dukterinį struktūros elementą (naują eilutę).")
        ToolTip1.SetToolTip(Me.RemoveItemButton, "Pašalinti struktūros elementą (eilutę).")
        ToolTip1.SetToolTip(Me.ItemUpButton, "Pakelti struktūros elementą (eilutę) aukštyn sąraše.")
        ToolTip1.SetToolTip(Me.ItemDownButton, "Nuleisti struktūros elementą (eilutę) žemyn sąraše.")

        SaveInDatabaseButton.Enabled = ConsolidatedReport.CanEditObject
        FetchFromDatabaseButton.Enabled = ConsolidatedReport.CanGetObject

        MyCustomSettings.SetFormLayout(Me)

    End Sub


    Private Sub FetchFromDatabaseButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles FetchFromDatabaseButton.Click

        Try
            Using busy As New StatusBusy
                Obj = ConsolidatedReport.GetConsolidatedReport()
                ConsolidatedReportToTree()
            End Using
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        ReportView.Select()

    End Sub

    Private Sub GetNewFormButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles GetNewFormButton.Click

        Obj = ConsolidatedReport.NewConsolidatedReport
        ConsolidatedReportToTree()
        ReportView.Select()

    End Sub

    Private Sub OpenFileButton_Click(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles OpenFileButton.Click

        Dim fileName As String = ""

        Using openFile As New OpenFileDialog
            openFile.InitialDirectory = AppPath()
            openFile.Filter = "Ataskaitos forma|*.str|Visi failai|*.*"
            openFile.Multiselect = False
            If openFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = openFile.FileName
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        If Not IO.File.Exists(fileName) Then
            MsgBox("Klaida. Failas '" & fileName & "' neegzistuoja.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Try
            Using busy As New StatusBusy
                Obj = ConsolidatedReport.NewConsolidatedReport(fileName, Nothing)
                ConsolidatedReportToTree()
            End Using
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        ReportView.Select()

    End Sub

    Private Sub SaveAsFileButton_Click(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles SaveAsFileButton.Click

        If Obj Is Nothing Then Exit Sub

        Obj.CheckRules()
        If Not Obj.IsValid Then
            If Not YesOrNo("Ataskaitos formoje yra klaidų: " & Obj.GetAllBrokenRules & _
                vbCrLf & "Ar tikrai norite išsaugoti ataskaitos formą su klaidomis?") Then Exit Sub
        End If

        Dim fileName As String

        Using saveFile As New SaveFileDialog
            saveFile.InitialDirectory = AppPath()
            saveFile.Filter = "Ataskaitos forma|*.str|Visi failai|*.*"
            saveFile.AddExtension = True
            saveFile.DefaultExt = ".str"
            If saveFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = saveFile.FileName
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        Try
            Using busy As New StatusBusy
                Obj.SaveToFile(fileName)
            End Using
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        MsgBox("Ataskaitos formos duomenys sėkmingai išsaugoti faile.", _
            MsgBoxStyle.Information, "Info")

        ReportView.Select()

    End Sub

    Private Sub SaveInDatabaseButton_Click(ByVal sender As System.Object, _
                ByVal e As System.EventArgs) Handles SaveInDatabaseButton.Click

        If Obj Is Nothing OrElse Not Obj.IsDirty Then Exit Sub

        If Not Obj.IsValid Then
            MsgBox("Formoje yra klaidų: " & Obj.GetAllBrokenRules, MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        If Obj.IsNew Then
            If Not YesOrNo("DĖMESIO. Jūs ketinate įkrauti visiškai naują finansinių ataskaitų " _
                & "rinkinio struktūrą. Po įkrovimo visos sąskaitos įmonės sąskaitų plane praras " _
                & "susiejimą su finansinių ataskaitų eilutėmis ir šį priskyrimą reikės iš naujo " _
                & "nustatyti sąskaitų plano formoje. Ar tikrai norite tęsti?") Then Exit Sub
        Else
            If Not YesOrNo("Ar tikrai norite išsaugoti ataskaitų formas įmonės duomenų bazėje?") Then Exit Sub
        End If

        Try
            Using busy As New StatusBusy
                Obj = Obj.Clone.Save
            End Using
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        MsgBox("Ataskaitų formos duomenys sėkmingai išsaugoti duomenų bazėje.", _
            MsgBoxStyle.Information, "Info")

        ReportView.Select()

    End Sub


    Private Sub AddItemButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddItemButton.Click

        If Obj Is Nothing OrElse ReportView.SelectedNode Is Nothing _
            OrElse ReportView.SelectedNode.Tag Is Nothing Then Exit Sub

        Dim currentGuid As Guid = GetCurrentGuid()
        If currentGuid = Guid.Empty Then Exit Sub

        Dim current As ConsolidatedReportItem = GetCurrentItem(currentGuid)
        If current Is Nothing Then Exit Sub

        If current.HasAccountsAssigned Then
            If Not YesOrNo("DĖMESIO. Šiai eilutei yra priskirtų apskaitos sąskaitų. " _
                & "Pridėjus subeilutę reikės pakeisti atitinkamų sąskaitų priskyrimą " _
                & "sąskaitų plane. Ar tikrai norite tęsti?") Then Exit Sub
        End If

        Dim newChildGuid As Guid = Guid.Empty

        Try
            newChildGuid = Obj.AddChild(currentGuid)
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        If newChildGuid = Guid.Empty Then
            MsgBox("Klaida. Dėl neaiškių priežasčių nepavyko pridėti naujos eilutės.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Dim newNode As New TreeNode("")
        newNode.Tag = newChildGuid
        ReportView.SelectedNode.Nodes.Add(newNode)
        ReportView.SelectedNode = newNode
        newNode.BeginEdit()

    End Sub

    Private Sub RemoveItemButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RemoveItemButton.Click

        If Obj Is Nothing OrElse ReportView.SelectedNode Is Nothing _
            OrElse ReportView.SelectedNode.Parent Is Nothing _
            OrElse ReportView.SelectedNode.Tag Is Nothing Then Exit Sub

        Dim currentGuid As Guid = GetCurrentGuid()
        If currentGuid = Guid.Empty Then Exit Sub

        Dim current As ConsolidatedReportItem = GetCurrentItem(currentGuid)
        If current Is Nothing Then Exit Sub

        If current.HasAccountsAssigned Then
            If Not YesOrNo("DĖMESIO. Šiai eilutei yra priskirtų apskaitos sąskaitų. " _
                & "Ištrynus šią eilutę reikės pakeisti atitinkamų sąskaitų priskyrimą " _
                & "sąskaitų plane. Ar tikrai norite tęsti?") Then Exit Sub
        Else
            If Not YesOrNo("Ar tikrai norite pašalinti pasirinktą ataskaitos elementą?") Then Exit Sub
        End If

        Try
            Obj.RemoveChild(currentGuid)
            ReportView.SelectedNode.Remove()
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Private Sub ItemUpButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ItemUpButton.Click

        If Obj Is Nothing OrElse ReportView.SelectedNode Is Nothing OrElse _
            ReportView.SelectedNode.Parent Is Nothing OrElse _
            ReportView.SelectedNode.Tag Is Nothing OrElse _
            ReportView.SelectedNode.Parent.Tag Is Nothing OrElse _
            ReportView.SelectedNode.Index = 0 Then Exit Sub

        Dim currentGuid As Guid = GetCurrentGuid()
        If currentGuid = Guid.Empty Then Exit Sub

        Try
            If Not Obj.MoveChildUp(currentGuid) Then
                MsgBox("Klaida. Šios eilutės negalima pakelti sąraše.", _
                    MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        Dim nodeToMove As TreeNode = ReportView.SelectedNode

        MoveNodeUp(nodeToMove)

        ReportView.SelectedNode = nodeToMove

    End Sub

    Private Sub ItemDownButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ItemDownButton.Click

        If Obj Is Nothing OrElse ReportView.SelectedNode Is Nothing OrElse _
            ReportView.SelectedNode.Parent Is Nothing OrElse _
            ReportView.SelectedNode.Tag Is Nothing OrElse _
            ReportView.SelectedNode.Parent.Tag Is Nothing OrElse _
            ReportView.SelectedNode.Index = ReportView.SelectedNode.Parent.Nodes.Count - 1 Then Exit Sub

        Dim currentGuid As Guid = GetCurrentGuid()
        If currentGuid = Guid.Empty Then Exit Sub

        Try
            If Not Obj.MoveChildDown(currentGuid) Then
                MsgBox("Klaida. Šios eilutės negalima nuleisti sąraše.", _
                    MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        Dim nodeToMove As TreeNode = ReportView.SelectedNode

        MoveNodeDown(nodeToMove)

        ReportView.SelectedNode = nodeToMove

    End Sub

    Private Sub ReportView_AfterSelect(ByVal sender As System.Object, _
            ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles ReportView.AfterSelect
        ' enables/disables buttons depending on the selected node position

        ' no selected node, no editing buttons
        If ReportView.SelectedNode Is Nothing Then
            AddItemButton.Enabled = False
            RemoveItemButton.Enabled = False
            ItemUpButton.Enabled = False
            ItemDownButton.Enabled = False
            Exit Sub
        End If

        ' can only remove nodes, if they are not of the top level
        RemoveItemButton.Enabled = (ReportView.SelectedNode.Level > 2)
        ' can always add new nodes to the existing one
        AddItemButton.Enabled = (ReportView.SelectedNode.Level > 1)
        ' node can only be moved up if it's not the first node.
        ItemUpButton.Enabled = (ReportView.SelectedNode.Level > 2 _
            AndAlso ReportView.SelectedNode.Index > 0)
        ' node can only be moved down if it's not the last node.
        Dim parentNodesCount As Integer
        If ReportView.SelectedNode.Parent Is Nothing Then
            parentNodesCount = ReportView.Nodes.Count
        Else
            parentNodesCount = ReportView.SelectedNode.Parent.Nodes.Count
        End If
        ItemDownButton.Enabled = (ReportView.SelectedNode.Level > 2 AndAlso _
            ReportView.SelectedNode.Index < (parentNodesCount - 1))

        Dim current As ConsolidatedReportItem = TryGetCurrentItem()

        If current Is Nothing Then Exit Sub

        ConsolidatedReportItemBindingSource.RaiseListChangedEvents = False

        If Not ConsolidatedReportItemBindingSource.DataSource Is Nothing Then

            ConsolidatedReportItemBindingSource.EndEdit()

            ConsolidatedReportItemBindingSource.DataSource = Nothing

        End If

        If Not _CurrentItem Is Nothing Then
            _CurrentItem.ApplyEdit()
        End If

        _CurrentItem = current

        _CurrentItem.BeginEdit()

        ConsolidatedReportItemBindingSource.DataSource = _CurrentItem

        ConsolidatedReportItemBindingSource.RaiseListChangedEvents = True

        ConsolidatedReportItemBindingSource.ResetBindings(False)

        Me.VisibleIndexNumericUpDown.Enabled = (ReportView.SelectedNode.Level > 1)
        Me.DisplayedNumberTextBox.ReadOnly = (ReportView.SelectedNode.Level < 2)
        Me.NameTextBox.ReadOnly = (ReportView.SelectedNode.Level < 2)
        Me.IsCreditCheckBox.Enabled = (ReportView.SelectedNode.Level > 1)

    End Sub


    ''' <summary>
    ''' Maps the ConsolidatedReport object to the TreeView control.
    ''' </summary>
    Private Sub ConsolidatedReportToTree()

        Using busy As New StatusBusy
            ReportView.Nodes.Clear()
            ConsolidatedReportItemListToNode(Obj.Children, ReportView.Nodes)
            ReportView.ExpandAll()
            ReportView_AfterSelect(Me, New TreeViewEventArgs(ReportView.SelectedNode))
        End Using

        For Each n As TreeNode In ReportView.Nodes
            n.ForeColor = Color.DarkBlue
            For Each n2 As TreeNode In n.Nodes
                n2.ForeColor = Color.DarkBlue
            Next
        Next

    End Sub

    ''' <summary>
    ''' Helper method. Recursevely maps the ConsolidatedReport object to the TreeView control.
    ''' </summary>
    Private Sub ConsolidatedReportItemListToNode(ByVal childList As ConsolidatedReportItem, _
        ByRef currentNode As TreeNodeCollection)

        Dim newNode As New TreeNode(childList.Name)
        newNode.Checked = childList.IsCredit
        newNode.Tag = childList.Guid
        For Each c As ConsolidatedReportItem In childList.Children
            ConsolidatedReportItemListToNode(c, newNode.Nodes)
        Next
        currentNode.Add(newNode)

    End Sub

    Private Function GetCurrentGuid() As Guid

        Dim result As Guid = Guid.Empty

        Try
            result = DirectCast(ReportView.SelectedNode.Tag, Guid)
        Catch ex As Exception
            ShowError(ex)
            Return Guid.Empty
        End Try

        If result = Guid.Empty Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko nustatyti pasirinktos eilutės", _
                MsgBoxStyle.Exclamation, "Klaida.")
        End If

        Return result

    End Function

    Private Function GetCurrentItem(ByVal itemGuid As Guid) As ConsolidatedReportItem

        Dim result As ConsolidatedReportItem = Nothing

        Try
            result = Obj.GetChild(itemGuid)
        Catch ex As Exception
            ShowError(ex)
            Return Nothing
        End Try

        If result Is Nothing Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko nustatyti pasirinktos eilutės", _
                MsgBoxStyle.Exclamation, "Klaida.")
        End If

        Return result

    End Function

    Private Function TryGetCurrentItem() As ConsolidatedReportItem

        Dim currentguid As Guid = Guid.Empty

        Try
            currentguid = DirectCast(ReportView.SelectedNode.Tag, Guid)
        Catch ex As Exception
        End Try

        If currentguid = Guid.Empty Then
            Return Nothing
        End If

        Dim result As ConsolidatedReportItem = Nothing

        Try
            result = Obj.GetChild(currentguid)
        Catch ex As Exception
        End Try

        Return result

    End Function

    Private Shared Sub MoveNodeUp(ByVal node As TreeNode)

        Dim parent As TreeNode = node.Parent

        If Not parent Is Nothing Then

            Dim index As Integer = parent.Nodes.IndexOf(node)
            If index > 0 Then
                parent.Nodes.RemoveAt(index)
                parent.Nodes.Insert(index - 1, node)
            End If

        End If

    End Sub

    Private Shared Sub MoveNodeDown(ByVal node As TreeNode)

        Dim parent As TreeNode = node.Parent

        If Not parent Is Nothing Then

            Dim index As Integer = parent.Nodes.IndexOf(node)
            If index < parent.Nodes.Count - 1 Then
                parent.Nodes.RemoveAt(index)
                parent.Nodes.Insert(index + 1, node)
            End If

        End If

    End Sub

    Private Sub NameTextBox_TextChanged(ByVal sender As System.Object, _
        ByVal e As EventArgs) Handles NameTextBox.TextChanged

        If _CurrentItem Is Nothing Then Exit Sub

        SetNodeLabel(ReportView.TopNode, _CurrentItem.Guid, NameTextBox.Text)

    End Sub

    Private Sub CancelItemEditButton_Click(ByVal sender As System.Object, _
        ByVal e As EventArgs) Handles CancelItemEditButton.Click

        If ConsolidatedReportItemBindingSource.DataSource Is Nothing Then Exit Sub

        ConsolidatedReportItemBindingSource.RaiseListChangedEvents = False

        ConsolidatedReportItemBindingSource.CancelEdit()

        ConsolidatedReportItemBindingSource.DataSource = Nothing

        If Not _CurrentItem Is Nothing Then
            _CurrentItem.CancelEdit()
            _CurrentItem.BeginEdit()
        End If

        ConsolidatedReportItemBindingSource.DataSource = _CurrentItem

        ConsolidatedReportItemBindingSource.RaiseListChangedEvents = True

        ConsolidatedReportItemBindingSource.ResetBindings(False)

    End Sub

    Private Function SetNodeLabel(ByVal parentNode As TreeNode, ByVal itemGuid As Guid, _
        ByVal labelText As String) As Boolean

        If Not parentNode.Tag Is Nothing AndAlso DirectCast(parentNode.Tag, Guid) = itemGuid Then
            parentNode.Text = labelText
            Return True
        End If

        For Each child As TreeNode In parentNode.Nodes
            If SetNodeLabel(child, itemGuid, labelText) Then Return True
        Next

        Return False

    End Function

End Class