<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_ConsolidatedReportsStructure
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim VisibleIndexLabel As System.Windows.Forms.Label
        Dim DisplayedNumberLabel As System.Windows.Forms.Label
        Dim NameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_ConsolidatedReportsStructure))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.RemoveItemButton = New System.Windows.Forms.Button
        Me.ItemDownButton = New System.Windows.Forms.Button
        Me.ItemUpButton = New System.Windows.Forms.Button
        Me.AddItemButton = New System.Windows.Forms.Button
        Me.GetNewFormButton = New System.Windows.Forms.Button
        Me.SaveInDatabaseButton = New System.Windows.Forms.Button
        Me.SaveAsFileButton = New System.Windows.Forms.Button
        Me.OpenFileButton = New System.Windows.Forms.Button
        Me.FetchFromDatabaseButton = New System.Windows.Forms.Button
        Me.ReportView = New System.Windows.Forms.TreeView
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.NameTextBox = New System.Windows.Forms.TextBox
        Me.VisibleIndexNumericUpDown = New System.Windows.Forms.NumericUpDown
        Me.IsCreditCheckBox = New System.Windows.Forms.CheckBox
        Me.DisplayedNumberTextBox = New System.Windows.Forms.TextBox
        Me.CancelItemEditButton = New System.Windows.Forms.Button
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        Me.ConsolidatedReportItemBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        VisibleIndexLabel = New System.Windows.Forms.Label
        DisplayedNumberLabel = New System.Windows.Forms.Label
        NameLabel = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.VisibleIndexNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConsolidatedReportItemBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VisibleIndexLabel
        '
        VisibleIndexLabel.AutoSize = True
        VisibleIndexLabel.Dock = System.Windows.Forms.DockStyle.Fill
        VisibleIndexLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        VisibleIndexLabel.Location = New System.Drawing.Point(3, 0)
        VisibleIndexLabel.Name = "VisibleIndexLabel"
        VisibleIndexLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        VisibleIndexLabel.Size = New System.Drawing.Size(82, 26)
        VisibleIndexLabel.TabIndex = 4
        VisibleIndexLabel.Text = "Eilės Nr.:"
        VisibleIndexLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DisplayedNumberLabel
        '
        DisplayedNumberLabel.AutoSize = True
        DisplayedNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill
        DisplayedNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DisplayedNumberLabel.Location = New System.Drawing.Point(279, 0)
        DisplayedNumberLabel.Name = "DisplayedNumberLabel"
        DisplayedNumberLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        DisplayedNumberLabel.Size = New System.Drawing.Size(84, 26)
        DisplayedNumberLabel.TabIndex = 5
        DisplayedNumberLabel.Text = "Rodomas Nr.:"
        DisplayedNumberLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'NameLabel
        '
        NameLabel.AutoSize = True
        NameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        NameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        NameLabel.Location = New System.Drawing.Point(3, 26)
        NameLabel.Name = "NameLabel"
        NameLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        NameLabel.Size = New System.Drawing.Size(82, 26)
        NameLabel.TabIndex = 8
        NameLabel.Text = "Pavadinimas:"
        NameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.RemoveItemButton)
        Me.Panel1.Controls.Add(Me.ItemDownButton)
        Me.Panel1.Controls.Add(Me.ItemUpButton)
        Me.Panel1.Controls.Add(Me.AddItemButton)
        Me.Panel1.Controls.Add(Me.GetNewFormButton)
        Me.Panel1.Controls.Add(Me.SaveInDatabaseButton)
        Me.Panel1.Controls.Add(Me.SaveAsFileButton)
        Me.Panel1.Controls.Add(Me.OpenFileButton)
        Me.Panel1.Controls.Add(Me.FetchFromDatabaseButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(713, 74)
        Me.Panel1.TabIndex = 0
        '
        'RemoveItemButton
        '
        Me.RemoveItemButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveItemButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RemoveItemButton.Location = New System.Drawing.Point(629, 38)
        Me.RemoveItemButton.Name = "RemoveItemButton"
        Me.RemoveItemButton.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.RemoveItemButton.Size = New System.Drawing.Size(33, 33)
        Me.RemoveItemButton.TabIndex = 9
        Me.RemoveItemButton.Text = "-"
        Me.RemoveItemButton.UseVisualStyleBackColor = True
        '
        'ItemDownButton
        '
        Me.ItemDownButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ItemDownButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ItemDownButton.Location = New System.Drawing.Point(668, 38)
        Me.ItemDownButton.Name = "ItemDownButton"
        Me.ItemDownButton.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.ItemDownButton.Size = New System.Drawing.Size(33, 33)
        Me.ItemDownButton.TabIndex = 8
        Me.ItemDownButton.Text = "v"
        Me.ItemDownButton.UseVisualStyleBackColor = True
        '
        'ItemUpButton
        '
        Me.ItemUpButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ItemUpButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ItemUpButton.Location = New System.Drawing.Point(668, 5)
        Me.ItemUpButton.Name = "ItemUpButton"
        Me.ItemUpButton.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.ItemUpButton.Size = New System.Drawing.Size(33, 33)
        Me.ItemUpButton.TabIndex = 7
        Me.ItemUpButton.Text = "^"
        Me.ItemUpButton.UseVisualStyleBackColor = True
        '
        'AddItemButton
        '
        Me.AddItemButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddItemButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddItemButton.Location = New System.Drawing.Point(629, 5)
        Me.AddItemButton.Name = "AddItemButton"
        Me.AddItemButton.Size = New System.Drawing.Size(33, 33)
        Me.AddItemButton.TabIndex = 6
        Me.AddItemButton.Text = "+"
        Me.AddItemButton.UseVisualStyleBackColor = True
        '
        'GetNewFormButton
        '
        Me.GetNewFormButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GetNewFormButton.Image = Global.ApskaitaWUI.My.Resources.Resources.Action_file_new_icon_24p
        Me.GetNewFormButton.Location = New System.Drawing.Point(128, 12)
        Me.GetNewFormButton.Name = "GetNewFormButton"
        Me.GetNewFormButton.Size = New System.Drawing.Size(43, 43)
        Me.GetNewFormButton.TabIndex = 5
        Me.GetNewFormButton.UseVisualStyleBackColor = True
        '
        'SaveInDatabaseButton
        '
        Me.SaveInDatabaseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveInDatabaseButton.Image = Global.ApskaitaWUI.My.Resources.Resources.database_save
        Me.SaveInDatabaseButton.Location = New System.Drawing.Point(68, 12)
        Me.SaveInDatabaseButton.Name = "SaveInDatabaseButton"
        Me.SaveInDatabaseButton.Size = New System.Drawing.Size(43, 43)
        Me.SaveInDatabaseButton.TabIndex = 4
        Me.SaveInDatabaseButton.UseVisualStyleBackColor = True
        '
        'SaveAsFileButton
        '
        Me.SaveAsFileButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveAsFileButton.Image = Global.ApskaitaWUI.My.Resources.Resources.Actions_document_save_icon_24p
        Me.SaveAsFileButton.Location = New System.Drawing.Point(241, 12)
        Me.SaveAsFileButton.Name = "SaveAsFileButton"
        Me.SaveAsFileButton.Size = New System.Drawing.Size(35, 33)
        Me.SaveAsFileButton.TabIndex = 3
        Me.SaveAsFileButton.UseVisualStyleBackColor = True
        '
        'OpenFileButton
        '
        Me.OpenFileButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenFileButton.Image = Global.ApskaitaWUI.My.Resources.Resources.folder_open_icon_24p
        Me.OpenFileButton.Location = New System.Drawing.Point(190, 12)
        Me.OpenFileButton.Name = "OpenFileButton"
        Me.OpenFileButton.Size = New System.Drawing.Size(35, 33)
        Me.OpenFileButton.TabIndex = 2
        Me.OpenFileButton.UseVisualStyleBackColor = True
        '
        'FetchFromDatabaseButton
        '
        Me.FetchFromDatabaseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FetchFromDatabaseButton.Image = Global.ApskaitaWUI.My.Resources.Resources.database_refresh
        Me.FetchFromDatabaseButton.Location = New System.Drawing.Point(10, 12)
        Me.FetchFromDatabaseButton.Name = "FetchFromDatabaseButton"
        Me.FetchFromDatabaseButton.Size = New System.Drawing.Size(43, 43)
        Me.FetchFromDatabaseButton.TabIndex = 1
        Me.FetchFromDatabaseButton.UseVisualStyleBackColor = True
        '
        'ReportView
        '
        Me.ReportView.CausesValidation = False
        Me.ReportView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportView.HideSelection = False
        Me.ReportView.Location = New System.Drawing.Point(0, 74)
        Me.ReportView.Name = "ReportView"
        Me.ReportView.Size = New System.Drawing.Size(713, 433)
        Me.ReportView.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(NameLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(VisibleIndexLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VisibleIndexNumericUpDown, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.IsCreditCheckBox, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(DisplayedNumberLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DisplayedNumberTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CancelItemEditButton, 6, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 507)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(713, 81)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'NameTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.NameTextBox, 6)
        Me.NameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsolidatedReportItemBindingSource, "Name", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NameTextBox.Location = New System.Drawing.Point(91, 29)
        Me.NameTextBox.MaxLength = 255
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.Size = New System.Drawing.Size(599, 20)
        Me.NameTextBox.TabIndex = 9
        '
        'VisibleIndexNumericUpDown
        '
        Me.VisibleIndexNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.ConsolidatedReportItemBindingSource, "VisibleIndex", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.VisibleIndexNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VisibleIndexNumericUpDown.Location = New System.Drawing.Point(91, 3)
        Me.VisibleIndexNumericUpDown.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.VisibleIndexNumericUpDown.Name = "VisibleIndexNumericUpDown"
        Me.VisibleIndexNumericUpDown.Size = New System.Drawing.Size(162, 20)
        Me.VisibleIndexNumericUpDown.TabIndex = 5
        Me.VisibleIndexNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IsCreditCheckBox
        '
        Me.IsCreditCheckBox.AutoSize = True
        Me.IsCreditCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ConsolidatedReportItemBindingSource, "IsCredit", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.IsCreditCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsCreditCheckBox.Location = New System.Drawing.Point(557, 3)
        Me.IsCreditCheckBox.Name = "IsCreditCheckBox"
        Me.IsCreditCheckBox.Size = New System.Drawing.Size(133, 17)
        Me.IsCreditCheckBox.TabIndex = 8
        Me.IsCreditCheckBox.Text = "Kreditinis Balansas"
        Me.IsCreditCheckBox.UseVisualStyleBackColor = True
        '
        'DisplayedNumberTextBox
        '
        Me.DisplayedNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsolidatedReportItemBindingSource, "DisplayedNumber", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DisplayedNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DisplayedNumberTextBox.Location = New System.Drawing.Point(369, 3)
        Me.DisplayedNumberTextBox.MaxLength = 50
        Me.DisplayedNumberTextBox.Name = "DisplayedNumberTextBox"
        Me.DisplayedNumberTextBox.Size = New System.Drawing.Size(162, 20)
        Me.DisplayedNumberTextBox.TabIndex = 6
        Me.DisplayedNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CancelItemEditButton
        '
        Me.CancelItemEditButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelItemEditButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CancelItemEditButton.Location = New System.Drawing.Point(615, 55)
        Me.CancelItemEditButton.Name = "CancelItemEditButton"
        Me.CancelItemEditButton.Size = New System.Drawing.Size(75, 23)
        Me.CancelItemEditButton.TabIndex = 10
        Me.CancelItemEditButton.Text = "Atšaukti"
        Me.CancelItemEditButton.UseVisualStyleBackColor = True
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleInformation = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleWarning = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.ConsolidatedReportItemBindingSource
        '
        'ConsolidatedReportItemBindingSource
        '
        Me.ConsolidatedReportItemBindingSource.DataSource = GetType(ApskaitaObjects.General.ConsolidatedReportItem)
        '
        'F_ConsolidatedReportsStructure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(713, 588)
        Me.Controls.Add(Me.ReportView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_ConsolidatedReportsStructure"
        Me.Text = "Finansinės atskaitomybės dokumentų struktūra"
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.VisibleIndexNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConsolidatedReportItemBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FetchFromDatabaseButton As System.Windows.Forms.Button
    Friend WithEvents GetNewFormButton As System.Windows.Forms.Button
    Friend WithEvents SaveInDatabaseButton As System.Windows.Forms.Button
    Friend WithEvents SaveAsFileButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileButton As System.Windows.Forms.Button
    Friend WithEvents ItemDownButton As System.Windows.Forms.Button
    Friend WithEvents ItemUpButton As System.Windows.Forms.Button
    Friend WithEvents AddItemButton As System.Windows.Forms.Button
    Friend WithEvents ReportView As System.Windows.Forms.TreeView
    Friend WithEvents RemoveItemButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ConsolidatedReportItemBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents VisibleIndexNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents DisplayedNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IsCreditCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CancelItemEditButton As System.Windows.Forms.Button
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
End Class
