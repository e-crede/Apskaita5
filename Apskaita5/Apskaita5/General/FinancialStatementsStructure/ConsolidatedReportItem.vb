Imports ApskaitaObjects.Attributes

Namespace General

    ''' <summary>
    ''' Represents a child item of a hierarchical consolidated financial statement report 
    ''' (balance sheet or income statement item).
    ''' </summary>
    ''' <remarks>Values are stored in the database table financialstatementsstructure.
    ''' Values are stored using <see href="https://en.wikipedia.org/wiki/Nested_set_model">
    ''' Nested set model</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class ConsolidatedReportItem
        Inherits BusinessBase(Of ConsolidatedReportItem)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private Const ItemNodeName As String = "ConsolidatedReportItem"
        Private Const NameNodeName As String = "Name"
        Private Const IsCreditNodeName As String = "IsCredit"
        Private Const TypeNodeName As String = "Type"
        Friend Const ChildrenNodeName As String = "Children"


        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Left As Integer = 0
        Private _Right As Integer = 0
        Private _Name As String = ""
        Private _Type As FinancialStatementItemType
        Private _IsCredit As Boolean = False
        Private _HasAccountsAssigned As Boolean = False
        Private _Children As ConsolidatedReportItemList _
            = ConsolidatedReportItemList.NewConsolidatedReportItemList


        ''' <summary>
        ''' Gets an ID of ConsolidatedReportItem that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field financialstatementsstructure.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a GUID of ConsolidatedReportItem that uniquely identifies the item 
        ''' within the parent <see cref="ConsolidatedReport">ConsolidatedReport</see>.
        ''' </summary>
        ''' <remarks>Value is used to identify items within a TreeView control 
        ''' due to the lack of data binding support.
        ''' Value is not stored in the database.
        ''' Value is created when either a new <see cref="ConsolidatedReport">
        ''' ConsolidatedReport</see> is created/fetched or a new item is added.</remarks>
        Public ReadOnly Property Guid() As Guid
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Guid
            End Get
        End Property

        ''' <summary>
        ''' Whether the item is a root item, i.e. represents a root element in
        ''' a balance sheet or an income statement.
        ''' </summary>
        ''' <remarks>Root items are readonly. They cannot be changed or moved.</remarks>
        Public ReadOnly Property IsRootItem() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type = FinancialStatementItemType.HeaderGeneral OrElse _
                    _Type = FinancialStatementItemType.HeaderStatementOfComprehensiveIncome OrElse _
                    _Type = FinancialStatementItemType.HeaderStatementOfFinancialPosition
            End Get
        End Property

        ''' <summary>
        ''' Gets the left index within the nested set model.
        ''' </summary>
        ''' <remarks>Value is stored in the database field financialstatementsstructure.Lft.</remarks>
        Public Property Left() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Left
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Private Set(ByVal value As Integer)
                If _Left <> value Then
                    _Left = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the right index within the nested set model.
        ''' </summary>
        ''' <remarks>Value is stored in the database field financialstatementsstructure.Rgt.</remarks>
        Public Property Right() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Right
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Private Set(ByVal value As Integer)
                If _Right <> value Then
                    _Right = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a name of ConsolidatedReportItem, i.e. text of the line in a Consolidated Report.
        ''' </summary>
        ''' <remarks>Value is stored in the database field financialstatementsstructure.Name.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255, False)> _
        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Private Set(ByVal value As String)
                If IsRootItem Then Exit Property
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether credit balance is displayed as positive value.
        ''' </summary>
        ''' <remarks>Value is stored in the database field financialstatementsstructure.IsCredit.</remarks>
        Public Property IsCredit() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsCredit
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Private Set(ByVal value As Boolean)
                If IsRootItem Then Exit Property
                CanWriteProperty(True)
                If _IsCredit <> value Then
                    _IsCredit = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a type of ConsolidatedReportItem
        ''' </summary>
        ''' <remarks>Value is stored in the database field financialstatementsstructure.StatementType.</remarks>
        Public ReadOnly Property [Type]() As FinancialStatementItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Whether the item currently have any accounts assigned.
        ''' </summary>
        Public ReadOnly Property HasAccountsAssigned() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HasAccountsAssigned
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of child ConsolidatedReportItem.
        ''' </summary>
        Public ReadOnly Property Children() As ConsolidatedReportItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Children
            End Get
        End Property


        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso _Children.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Children.IsDirty
            End Get
        End Property


        Friend Function AddChild(ByVal parentItemGuid As Guid) As Guid

            If Me.Guid = parentItemGuid Then

                If IsRootItem Then
                    Throw New Exception(My.Resources.General_ConsolidatedReportItem_CannotChangeBaseItem)
                End If

                Dim newChild As ConsolidatedReportItem = ConsolidatedReportItem.NewConsolidatedReportItem()
                newChild._Children = ConsolidatedReportItemList.NewConsolidatedReportItemList
                newChild._IsCredit = _IsCredit
                newChild._Type = _Type

                _Children.Add(newChild)

                Return newChild.Guid

            End If

            Dim result As Guid = Guid.Empty
            For Each c As ConsolidatedReportItem In _Children
                result = c.AddChild(parentItemGuid)
                If result <> Guid.Empty Then Return result
            Next

            Return Guid.Empty

        End Function

        Friend Function RemoveChild(ByVal itemGuidToRemove As Guid) As Boolean

            For Each c As ConsolidatedReportItem In _Children

                If c.Guid = itemGuidToRemove Then

                    If IsRootItem OrElse c.IsRootItem Then
                        Throw New Exception(My.Resources.General_ConsolidatedReportItem_CannotChangeBaseItem)
                    End If

                    _Children.Remove(c)
                    Return True

                End If

            Next

            For Each c As ConsolidatedReportItem In _Children
                If c.RemoveChild(itemGuidToRemove) Then Return True
            Next

            Return False

        End Function

        Friend Function MoveChildUp(ByVal itemGuidToMove As Guid, ByRef success As Boolean) As Boolean

            For Each c As ConsolidatedReportItem In _Children

                If c._Guid = itemGuidToMove Then

                    If c.IsRootItem Then
                        Throw New Exception(My.Resources.General_ConsolidatedReportItem_CannotChangeBaseItem)
                    End If

                    Dim result As ConsolidatedReportItem = _Children.MoveUp(c)
                    If Not result Is Nothing Then
                        result.MarkDirty()
                        success = True
                    End If

                    Return True

                End If

            Next

            For Each c As ConsolidatedReportItem In _Children
                If c.MoveChildUp(itemGuidToMove, success) Then Return True
            Next

            Return False

        End Function

        Friend Function MoveChildDown(ByVal itemGuidToMove As Guid, ByRef success As Boolean) As Boolean

            For Each c As ConsolidatedReportItem In _Children

                If c._Guid = itemGuidToMove Then

                    If c.IsRootItem Then
                        Throw New Exception(My.Resources.General_ConsolidatedReportItem_CannotChangeBaseItem)
                    End If

                    Dim result As ConsolidatedReportItem = _Children.MoveDown(c)
                    If Not result Is Nothing Then
                        result.MarkDirty()
                        success = True
                    End If

                    Return True

                End If

            Next

            For Each c As ConsolidatedReportItem In _Children
                If c.MoveChildDown(itemGuidToMove, success) Then Return True
            Next

            Return False

        End Function

        Friend Function SetName(ByVal itemGuid As Guid, ByVal newNameValue As String) As Boolean

            If Me.Guid = itemGuid Then

                If Me.IsRootItem Then
                    Throw New Exception(My.Resources.General_ConsolidatedReportItem_CannotChangeBaseItem)
                End If

                Name = newNameValue
                Return True

            End If

            For Each c As ConsolidatedReportItem In _Children
                If c.SetName(itemGuid, newNameValue) Then Return True
            Next

            Return False

        End Function

        Friend Function SetIsCredit(ByVal itemGuid As Guid, ByVal newIsCreditValue As Boolean) As Boolean

            If Me.Guid = itemGuid Then

                If Me.IsRootItem Then
                    Throw New Exception(My.Resources.General_ConsolidatedReportItem_CannotChangeBaseItem)
                End If

                IsCredit = newIsCreditValue
                Return True

            End If

            For Each c As ConsolidatedReportItem In _Children
                If c.SetIsCredit(itemGuid, newIsCreditValue) Then Return True
            Next

            Return False

        End Function

        Friend Function GetChild(ByVal itemGuid As Guid) As ConsolidatedReportItem

            If itemGuid = Guid.Empty Then Return Nothing

            If itemGuid = Me._Guid Then Return Me

            Dim result As ConsolidatedReportItem = Nothing
            For Each c As ConsolidatedReportItem In Me._Children
                result = c.GetChild(itemGuid)
                If Not result Is Nothing Then Return result
            Next

            Return Nothing

        End Function


        Friend Function ChildExists(ByVal child As ConsolidatedReportItem) As Boolean

            If child Is Nothing Then Return False

            If child._Guid = Me._Guid Then Return True

            For Each c As ConsolidatedReportItem In Me._Children
                If c.ChildExists(child) Then Return True
            Next

            Return False

        End Function

        Friend Function ChildExists(ByVal itemGuid As Guid) As Boolean

            If itemGuid = Guid.Empty Then Return False

            If itemGuid = Me._Guid Then Return True

            For Each c As ConsolidatedReportItem In Me._Children
                If c.ChildExists(itemGuid) Then Return True
            Next

            Return False

        End Function


        ''' <summary>
        ''' Gets a human readable desciption of all the validation errors.
        ''' Returns <see cref="String.Empty" /> if no errors.
        ''' </summary>
        ''' <returns>A human readable desciption of all the validation errors.
        ''' Returns <see cref="String.Empty" /> if no errors.</returns>
        ''' <remarks></remarks>
        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            Dim result As String = ""
            If Not MyBase.IsValid Then
                result = String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                    vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
            End If
            For Each c As ConsolidatedReportItem In _Children
                result = AddWithNewLine(result, c.GetErrorString(), False)
            Next
            Return result
        End Function

        ''' <summary>
        ''' Gets a human readable desciption of all the validation warnings.
        ''' Returns <see cref="String.Empty" /> if no warnings.
        ''' </summary>
        ''' <returns>A human readable desciption of all the validation warnings.
        ''' Returns <see cref="String.Empty" /> if no warnings.</returns>
        ''' <remarks></remarks>
        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            Dim result As String = ""
            If Me.BrokenRulesCollection.WarningCount > 0 Then
                result = String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                    vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
            End If
            For Each c As ConsolidatedReportItem In _Children
                result = AddWithNewLine(result, c.GetWarningString(), False)
            Next
            Return result
        End Function

        Public Function HasWarnings() As Boolean

            If Me.BrokenRulesCollection.WarningCount > 0 Then Return True

            For Each c As ConsolidatedReportItem In _Children
                If c.HasWarnings Then Return True
            Next

            Return False

        End Function

        Friend Sub CheckRules()
            Me.ValidationRules.CheckRules()
            For Each c As ConsolidatedReportItem In _Children
                c.CheckRules()
            Next
        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.General_ConsolidatedReportItem_ToString, _
                Utilities.ConvertLocalizedName(_Type), _Name, _
                IIf(_IsCredit, " (-)", ""))
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Validation.RuleArgs("Name"))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of ConsolidatedReportItem with default values set.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewConsolidatedReportItem() As ConsolidatedReportItem
            Return New ConsolidatedReportItem(False)
        End Function

        ''' <summary>
        ''' Gets a new root instance of ConsolidatedReportItem for a new
        ''' <see cref="ConsolidatedReport">ConsolidatedReport</see> instance.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewRootConsolidatedReportItem() As ConsolidatedReportItem
            Return New ConsolidatedReportItem(True)
        End Function

        ''' <summary>
        ''' Gets an existing instance of ConsolidatedReportItem from a database query.
        ''' </summary>
        ''' <param name="index"></param>
        ''' <param name="myData">Database query result data.</param>
        Friend Shared Function GetConsolidatedReportItem(ByVal myData As DataTable, _
            ByRef index As Integer) As ConsolidatedReportItem
            Return New ConsolidatedReportItem(myData, index)
        End Function

        ''' <summary>
        ''' Gets a new instance of ConsolidatedReportItem from xml data.
        ''' </summary>
        ''' <param name="node"><see cref="Xml.XmlNode">XmlNode</see> that contains information about item.</param>
        ''' <param name="level"></param>
        Friend Shared Function GetConsolidatedReportItem(ByVal node As Xml.XmlNode, _
            ByRef level As Integer) As ConsolidatedReportItem
            Return New ConsolidatedReportItem(node, level)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal createBaseStructure As Boolean)
            MarkAsChild()
            Create(createBaseStructure)
        End Sub

        Private Sub New(ByVal myData As DataTable, ByRef index As Integer)
            MarkAsChild()
            Fetch(myData, index)
        End Sub

        Private Sub New(ByVal node As Xml.XmlNode, ByRef level As Integer)
            MarkAsChild()
            Create(node, level)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal createBaseStructure As Boolean)

            _Children = ConsolidatedReportItemList.NewConsolidatedReportItemList

            If createBaseStructure Then
                AddRootStructure()
                CheckRules()
            Else
                Me.ValidationRules.CheckRules()
            End If

        End Sub

        Private Sub AddRootStructure()

            _Name = My.Resources.General_ConsolidatedReportItem_FinancialStatementsRootName
            _Type = FinancialStatementItemType.HeaderGeneral

            Dim newBalanceSheetItem As New ConsolidatedReportItem
            newBalanceSheetItem._Name = My.Resources.General_ConsolidatedReportItem_BalanceStatementRootName
            newBalanceSheetItem._Type = FinancialStatementItemType.HeaderStatementOfFinancialPosition
            newBalanceSheetItem._Children = ConsolidatedReportItemList.NewConsolidatedReportItemList

            Dim newAssetsItem As New ConsolidatedReportItem
            newAssetsItem._IsCredit = False
            newAssetsItem._Name = My.Resources.General_ConsolidatedReportItem_BalanceAssetsStatementRootName
            newAssetsItem._Type = FinancialStatementItemType.StatementOfFinancialPosition
            newAssetsItem._Children = ConsolidatedReportItemList.NewConsolidatedReportItemList

            Dim newCapitalItem As New ConsolidatedReportItem
            newCapitalItem._IsCredit = True
            newCapitalItem._Name = My.Resources.General_ConsolidatedReportItem_BalanceCapitalStatementRootName
            newCapitalItem._Type = FinancialStatementItemType.StatementOfFinancialPosition
            newBalanceSheetItem._Children = ConsolidatedReportItemList.NewConsolidatedReportItemList

            newBalanceSheetItem._Children.Add(newAssetsItem)
            newBalanceSheetItem._Children.Add(newCapitalItem)

            Dim newIncomeStatementItem As New ConsolidatedReportItem
            newIncomeStatementItem._IsCredit = True
            newIncomeStatementItem._Name = My.Resources.General_ConsolidatedReportItem_IncomeStatementRootName
            newIncomeStatementItem._Type = FinancialStatementItemType.HeaderStatementOfComprehensiveIncome
            newIncomeStatementItem._Children = ConsolidatedReportItemList.NewConsolidatedReportItemList

            Dim newIncomeStatementFirstItem As New ConsolidatedReportItem
            newIncomeStatementFirstItem._IsCredit = True
            newIncomeStatementFirstItem._Name = My.Resources.General_ConsolidatedReportItem_IncomeStatementFirstItemRootName
            newIncomeStatementFirstItem._Type = FinancialStatementItemType.StatementOfComprehensiveIncome
            newIncomeStatementFirstItem._Children = ConsolidatedReportItemList.NewConsolidatedReportItemList

            newIncomeStatementItem._Children.Add(newIncomeStatementFirstItem)

            _Children.Add(newBalanceSheetItem)
            _Children.Add(newIncomeStatementItem)

        End Sub

        Private Sub Create(ByVal node As Xml.XmlNode, ByRef level As Integer)

            _Name = node.Item(NameNodeName).InnerText.Trim
            _IsCredit = ConvertDbBoolean(CIntSafe(node.Item(IsCreditNodeName).InnerText.Trim, 0))
            _Type = Utilities.ConvertDatabaseID(Of FinancialStatementItemType) _
                (CIntSafe(node.Item(TypeNodeName).InnerText.Trim, 0))

            _Children = ConsolidatedReportItemList.NewConsolidatedReportItemList( _
                node.Item(ChildrenNodeName).ChildNodes, level)

            ValidationRules.CheckRules()

        End Sub


        Private Sub Fetch(ByVal myData As DataTable, ByRef index As Integer)

            If myData.Rows.Count < 1 Then
                Create(True)
                Exit Sub
            End If

            _ID = CIntSafe(myData.Rows(index).Item(0), 0)
            _Name = CStrSafe(myData.Rows(index).Item(1))
            _Type = Utilities.ConvertDatabaseID(Of FinancialStatementItemType) _
                (CIntSafe(myData.Rows(index).Item(3), 0))
            _IsCredit = ConvertDbBoolean(CIntSafe(myData.Rows(index).Item(4), 0))
            _HasAccountsAssigned = ConvertDbBoolean(CIntSafe(myData.Rows(index).Item(5), 0))
            _Left = CIntSafe(myData.Rows(index).Item(6), 0)
            _Right = CIntSafe(myData.Rows(index).Item(7), 0)
            _Children = ConsolidatedReportItemList.GetConsolidatedReportItemList( _
                myData, index, CIntSafe(myData.Rows(index).Item(2), 0))

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Friend Sub SaveRootItem()

            If _Type <> FinancialStatementItemType.HeaderGeneral Then
                Throw New InvalidOperationException( _
                    My.Resources.General_ConsolidatedReportItem_InvalidSaveOperation)
            End If

            UpdateNestedSetIndexes(1)

            Me.Update()

        End Sub

        Private Sub UpdateNestedSetIndexes(ByRef index As Integer)

            Me.Left = index

            For Each i As ConsolidatedReportItem In _Children
                index += 1
                i.UpdateNestedSetIndexes(index)
            Next

            index += 1

            Me.Right = index

        End Sub

        Friend Sub Update()

            Dim myComm As SQLCommand

            If IsNew OrElse MyBase.IsDirty Then

                If IsNew Then
                    myComm = New SQLCommand("InsertConsolidatedReportItem")
                Else
                    myComm = New SQLCommand("UpdateConsolidatedReportItem")
                    myComm.AddParam("?AA", _ID)
                End If
                myComm.AddParam("?AB", _Name.Trim)
                myComm.AddParam("?AC", ConvertDbBoolean(_IsCredit))
                myComm.AddParam("?AD", Utilities.ConvertDatabaseID(_Type))
                myComm.AddParam("?AE", _Left)
                myComm.AddParam("?AF", _Right)

                myComm.Execute()

                If IsNew Then _ID = Convert.ToInt32(myComm.LastInsertID)

            End If

            If Not IsNew AndAlso _Children.HasNewChildren() Then

                myComm = New SQLCommand("RemoveAccountsReferences")
                myComm.AddParam("?CD", _ID)
                myComm.Execute()

            End If

            _Children.Update()

            MarkOld()

        End Sub


        Friend Sub DeleteSelf()

            _Children.DeleteSelf()

            If IsNew Then Exit Sub

            Dim myComm As New SQLCommand("DeleteConsolidatedReportItem")
            myComm.AddParam("?AA", _ID)

            myComm.Execute()

            myComm = New SQLCommand("RemoveAccountsReferences")
            myComm.AddParam("?CD", _ID)
            myComm.Execute()

            myComm.Execute()

            MarkNew()

        End Sub


        Friend Sub WriteXmlNode(ByRef writer As System.Xml.XmlWriter)

            writer.WriteStartElement(ItemNodeName)

            writer.WriteStartElement(NameNodeName)
            writer.WriteString(_Name.Trim)
            writer.WriteEndElement()

            writer.WriteStartElement(IsCreditNodeName)
            writer.WriteString(ConvertDbBoolean(_IsCredit).ToString)
            writer.WriteEndElement()

            writer.WriteStartElement(TypeNodeName)
            writer.WriteString(Utilities.ConvertDatabaseID(_Type).ToString)
            writer.WriteEndElement()

            writer.WriteStartElement(ChildrenNodeName)
            For Each c As ConsolidatedReportItem In _Children
                c.WriteXmlNode(writer)
            Next
            writer.WriteEndElement()

            writer.WriteEndElement()

            MarkNew()

        End Sub

#End Region

    End Class

End Namespace