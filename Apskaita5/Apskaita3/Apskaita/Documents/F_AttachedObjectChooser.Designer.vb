<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_AttachedObjectChooser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_AttachedObjectChooser))
        Me.WarehouseInfoListAccGridComboBox = New AccControls.AccGridComboBox
        Me.LongTermAssetTransferRadioButton = New System.Windows.Forms.RadioButton
        Me.LongTermAssetAcquisitionValueChangeRadioButton = New System.Windows.Forms.RadioButton
        Me.GoodsInfoListAccGridComboBox = New AccControls.AccGridComboBox
        Me.SelectGoodsInfoButton = New System.Windows.Forms.Button
        Me.BarCodeTextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.LongTermAssetAcquisitionRadioButton = New System.Windows.Forms.RadioButton
        Me.LongTermAssetAccGridComboBox = New AccControls.AccGridComboBox
        Me.ServicesRadioButton = New System.Windows.Forms.RadioButton
        Me.ServicesAccGridComboBox = New AccControls.AccGridComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GoodsAcquisitionRadioButton = New System.Windows.Forms.RadioButton
        Me.GoodsTransferRadioButton = New System.Windows.Forms.RadioButton
        Me.GoodsDiscountRadioButton = New System.Windows.Forms.RadioButton
        Me.GoodsAdditionalCostsRadioButton = New System.Windows.Forms.RadioButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.ICancelButton = New System.Windows.Forms.Button
        Me.IOkButton = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'WarehouseInfoListAccGridComboBox
        '
        Me.WarehouseInfoListAccGridComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarehouseInfoListAccGridComboBox.EmptyValueString = ""
        Me.WarehouseInfoListAccGridComboBox.FilterPropertyName = ""
        Me.WarehouseInfoListAccGridComboBox.FormattingEnabled = True
        Me.WarehouseInfoListAccGridComboBox.InstantBinding = True
        Me.WarehouseInfoListAccGridComboBox.Location = New System.Drawing.Point(72, 235)
        Me.WarehouseInfoListAccGridComboBox.Name = "WarehouseInfoListAccGridComboBox"
        Me.WarehouseInfoListAccGridComboBox.Size = New System.Drawing.Size(527, 21)
        Me.WarehouseInfoListAccGridComboBox.TabIndex = 15
        '
        'LongTermAssetTransferRadioButton
        '
        Me.LongTermAssetTransferRadioButton.AutoSize = True
        Me.LongTermAssetTransferRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LongTermAssetTransferRadioButton.Location = New System.Drawing.Point(12, 45)
        Me.LongTermAssetTransferRadioButton.Name = "LongTermAssetTransferRadioButton"
        Me.LongTermAssetTransferRadioButton.Size = New System.Drawing.Size(173, 17)
        Me.LongTermAssetTransferRadioButton.TabIndex = 1
        Me.LongTermAssetTransferRadioButton.Text = "Ilgalaikio turto perleidimas"
        Me.LongTermAssetTransferRadioButton.UseVisualStyleBackColor = True
        '
        'LongTermAssetAcquisitionValueChangeRadioButton
        '
        Me.LongTermAssetAcquisitionValueChangeRadioButton.AutoSize = True
        Me.LongTermAssetAcquisitionValueChangeRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LongTermAssetAcquisitionValueChangeRadioButton.Location = New System.Drawing.Point(206, 45)
        Me.LongTermAssetAcquisitionValueChangeRadioButton.Name = "LongTermAssetAcquisitionValueChangeRadioButton"
        Me.LongTermAssetAcquisitionValueChangeRadioButton.Size = New System.Drawing.Size(262, 17)
        Me.LongTermAssetAcquisitionValueChangeRadioButton.TabIndex = 2
        Me.LongTermAssetAcquisitionValueChangeRadioButton.Text = "Ilgalaikio turto įsigijimo savikainos pokytis"
        Me.LongTermAssetAcquisitionValueChangeRadioButton.UseVisualStyleBackColor = True
        '
        'GoodsInfoListAccGridComboBox
        '
        Me.GoodsInfoListAccGridComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GoodsInfoListAccGridComboBox.EmptyValueString = ""
        Me.GoodsInfoListAccGridComboBox.FilterPropertyName = ""
        Me.GoodsInfoListAccGridComboBox.FormattingEnabled = True
        Me.GoodsInfoListAccGridComboBox.InstantBinding = True
        Me.GoodsInfoListAccGridComboBox.Location = New System.Drawing.Point(15, 208)
        Me.GoodsInfoListAccGridComboBox.Name = "GoodsInfoListAccGridComboBox"
        Me.GoodsInfoListAccGridComboBox.Size = New System.Drawing.Size(331, 21)
        Me.GoodsInfoListAccGridComboBox.TabIndex = 12
        '
        'SelectGoodsInfoButton
        '
        Me.SelectGoodsInfoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectGoodsInfoButton.Image = Global.ApskaitaWUI.My.Resources.Resources.Button_Reload_icon_16p
        Me.SelectGoodsInfoButton.Location = New System.Drawing.Point(575, 206)
        Me.SelectGoodsInfoButton.Margin = New System.Windows.Forms.Padding(0)
        Me.SelectGoodsInfoButton.Name = "SelectGoodsInfoButton"
        Me.SelectGoodsInfoButton.Size = New System.Drawing.Size(24, 24)
        Me.SelectGoodsInfoButton.TabIndex = 14
        Me.SelectGoodsInfoButton.UseVisualStyleBackColor = True
        '
        'BarCodeTextBox
        '
        Me.BarCodeTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BarCodeTextBox.Location = New System.Drawing.Point(416, 208)
        Me.BarCodeTextBox.MaxLength = 100
        Me.BarCodeTextBox.Name = "BarCodeTextBox"
        Me.BarCodeTextBox.Size = New System.Drawing.Size(158, 20)
        Me.BarCodeTextBox.TabIndex = 13
        Me.BarCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(352, 212)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Barkodas:"
        '
        'LongTermAssetAcquisitionRadioButton
        '
        Me.LongTermAssetAcquisitionRadioButton.AutoSize = True
        Me.LongTermAssetAcquisitionRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LongTermAssetAcquisitionRadioButton.Location = New System.Drawing.Point(12, 12)
        Me.LongTermAssetAcquisitionRadioButton.Name = "LongTermAssetAcquisitionRadioButton"
        Me.LongTermAssetAcquisitionRadioButton.Size = New System.Drawing.Size(160, 17)
        Me.LongTermAssetAcquisitionRadioButton.TabIndex = 0
        Me.LongTermAssetAcquisitionRadioButton.Text = "Ilgalaikio turto įsigijimas"
        Me.LongTermAssetAcquisitionRadioButton.UseVisualStyleBackColor = True
        '
        'LongTermAssetAccGridComboBox
        '
        Me.LongTermAssetAccGridComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LongTermAssetAccGridComboBox.EmptyValueString = ""
        Me.LongTermAssetAccGridComboBox.Enabled = False
        Me.LongTermAssetAccGridComboBox.FilterPropertyName = ""
        Me.LongTermAssetAccGridComboBox.FormattingEnabled = True
        Me.LongTermAssetAccGridComboBox.InstantBinding = True
        Me.LongTermAssetAccGridComboBox.Location = New System.Drawing.Point(12, 68)
        Me.LongTermAssetAccGridComboBox.Name = "LongTermAssetAccGridComboBox"
        Me.LongTermAssetAccGridComboBox.Size = New System.Drawing.Size(587, 21)
        Me.LongTermAssetAccGridComboBox.TabIndex = 5
        '
        'ServicesRadioButton
        '
        Me.ServicesRadioButton.AutoSize = True
        Me.ServicesRadioButton.Checked = True
        Me.ServicesRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServicesRadioButton.Location = New System.Drawing.Point(12, 107)
        Me.ServicesRadioButton.Name = "ServicesRadioButton"
        Me.ServicesRadioButton.Size = New System.Drawing.Size(83, 17)
        Me.ServicesRadioButton.TabIndex = 6
        Me.ServicesRadioButton.TabStop = True
        Me.ServicesRadioButton.Text = "Paslaugos"
        Me.ServicesRadioButton.UseVisualStyleBackColor = True
        '
        'ServicesAccGridComboBox
        '
        Me.ServicesAccGridComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServicesAccGridComboBox.EmptyValueString = ""
        Me.ServicesAccGridComboBox.FilterPropertyName = ""
        Me.ServicesAccGridComboBox.FormattingEnabled = True
        Me.ServicesAccGridComboBox.InstantBinding = True
        Me.ServicesAccGridComboBox.Location = New System.Drawing.Point(12, 130)
        Me.ServicesAccGridComboBox.Name = "ServicesAccGridComboBox"
        Me.ServicesAccGridComboBox.Size = New System.Drawing.Size(587, 21)
        Me.ServicesAccGridComboBox.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 187)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Prekių:"
        '
        'GoodsAcquisitionRadioButton
        '
        Me.GoodsAcquisitionRadioButton.AutoSize = True
        Me.GoodsAcquisitionRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GoodsAcquisitionRadioButton.Location = New System.Drawing.Point(61, 185)
        Me.GoodsAcquisitionRadioButton.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.GoodsAcquisitionRadioButton.Name = "GoodsAcquisitionRadioButton"
        Me.GoodsAcquisitionRadioButton.Size = New System.Drawing.Size(76, 17)
        Me.GoodsAcquisitionRadioButton.TabIndex = 8
        Me.GoodsAcquisitionRadioButton.Text = "Įsigijimas"
        Me.GoodsAcquisitionRadioButton.UseVisualStyleBackColor = True
        '
        'GoodsTransferRadioButton
        '
        Me.GoodsTransferRadioButton.AutoSize = True
        Me.GoodsTransferRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GoodsTransferRadioButton.Location = New System.Drawing.Point(143, 185)
        Me.GoodsTransferRadioButton.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.GoodsTransferRadioButton.Name = "GoodsTransferRadioButton"
        Me.GoodsTransferRadioButton.Size = New System.Drawing.Size(89, 17)
        Me.GoodsTransferRadioButton.TabIndex = 9
        Me.GoodsTransferRadioButton.Text = "Perleidimas"
        Me.GoodsTransferRadioButton.UseVisualStyleBackColor = True
        '
        'GoodsDiscountRadioButton
        '
        Me.GoodsDiscountRadioButton.AutoSize = True
        Me.GoodsDiscountRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GoodsDiscountRadioButton.Location = New System.Drawing.Point(235, 185)
        Me.GoodsDiscountRadioButton.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.GoodsDiscountRadioButton.Name = "GoodsDiscountRadioButton"
        Me.GoodsDiscountRadioButton.Size = New System.Drawing.Size(75, 17)
        Me.GoodsDiscountRadioButton.TabIndex = 10
        Me.GoodsDiscountRadioButton.Text = "Nuolaida"
        Me.GoodsDiscountRadioButton.UseVisualStyleBackColor = True
        '
        'GoodsAdditionalCostsRadioButton
        '
        Me.GoodsAdditionalCostsRadioButton.AutoSize = True
        Me.GoodsAdditionalCostsRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GoodsAdditionalCostsRadioButton.Location = New System.Drawing.Point(314, 185)
        Me.GoodsAdditionalCostsRadioButton.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.GoodsAdditionalCostsRadioButton.Name = "GoodsAdditionalCostsRadioButton"
        Me.GoodsAdditionalCostsRadioButton.Size = New System.Drawing.Size(132, 17)
        Me.GoodsAdditionalCostsRadioButton.TabIndex = 11
        Me.GoodsAdditionalCostsRadioButton.Text = "Savik. Padidinimas"
        Me.GoodsAdditionalCostsRadioButton.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.Controls.Add(Me.ICancelButton)
        Me.Panel2.Controls.Add(Me.IOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 262)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(622, 32)
        Me.Panel2.TabIndex = 55
        '
        'ICancelButton
        '
        Me.ICancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ICancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ICancelButton.Location = New System.Drawing.Point(521, 6)
        Me.ICancelButton.Name = "ICancelButton"
        Me.ICancelButton.Size = New System.Drawing.Size(89, 23)
        Me.ICancelButton.TabIndex = 1
        Me.ICancelButton.Text = "Atšaukti"
        Me.ICancelButton.UseVisualStyleBackColor = True
        '
        'IOkButton
        '
        Me.IOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IOkButton.Location = New System.Drawing.Point(416, 6)
        Me.IOkButton.Name = "IOkButton"
        Me.IOkButton.Size = New System.Drawing.Size(89, 23)
        Me.IOkButton.TabIndex = 0
        Me.IOkButton.Text = "Ok"
        Me.IOkButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 238)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Sandėlis:"
        '
        'F_AttachedObjectChooser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 294)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.WarehouseInfoListAccGridComboBox)
        Me.Controls.Add(Me.SelectGoodsInfoButton)
        Me.Controls.Add(Me.GoodsInfoListAccGridComboBox)
        Me.Controls.Add(Me.BarCodeTextBox)
        Me.Controls.Add(Me.GoodsAdditionalCostsRadioButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GoodsDiscountRadioButton)
        Me.Controls.Add(Me.GoodsTransferRadioButton)
        Me.Controls.Add(Me.GoodsAcquisitionRadioButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ServicesAccGridComboBox)
        Me.Controls.Add(Me.LongTermAssetAccGridComboBox)
        Me.Controls.Add(Me.LongTermAssetAcquisitionValueChangeRadioButton)
        Me.Controls.Add(Me.ServicesRadioButton)
        Me.Controls.Add(Me.LongTermAssetTransferRadioButton)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LongTermAssetAcquisitionRadioButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_AttachedObjectChooser"
        Me.ShowInTaskbar = False
        Me.Text = "Sąskaitos faktūros objektas"
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LongTermAssetAccGridComboBox As AccControls.AccGridComboBox
    Friend WithEvents LongTermAssetAcquisitionRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents LongTermAssetTransferRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ServicesRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ServicesAccGridComboBox As AccControls.AccGridComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ICancelButton As System.Windows.Forms.Button
    Friend WithEvents IOkButton As System.Windows.Forms.Button
    Friend WithEvents LongTermAssetAcquisitionValueChangeRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents WarehouseInfoListAccGridComboBox As AccControls.AccGridComboBox
    Friend WithEvents GoodsInfoListAccGridComboBox As AccControls.AccGridComboBox
    Friend WithEvents GoodsAcquisitionRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents GoodsTransferRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents GoodsDiscountRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents GoodsAdditionalCostsRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents SelectGoodsInfoButton As System.Windows.Forms.Button
    Friend WithEvents BarCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
