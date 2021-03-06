Imports ApskaitaObjects.Attributes

Namespace Assets

    ''' <summary>
    ''' Represents a user defined long term asset group.
    ''' </summary>
    ''' <remarks>Values are stored in the database table longtermassetcustomgroups.</remarks>
    <Serializable()> _
Public NotInheritable Class LongTermAssetCustomGroup
        Inherits BusinessBase(Of LongTermAssetCustomGroup)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = -1
        Private _Name As String = ""


        ''' <summary>
        ''' Gets an ID of the custom group that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field longtermassetcustomgroups.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a name of the custom group.
        ''' </summary>
        ''' <remarks>Value is stored in the database field longtermassetcustomgroups.Name.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property


        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Name"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringValueUniqueInCollectionValidation, _
                New Csla.Validation.RuleArgs("Name"))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function GetLongTermAssetCustomGroup(ByVal dr As DataRow) As LongTermAssetCustomGroup
            Return New LongTermAssetCustomGroup(dr)
        End Function

        Friend Shared Function NewLongTermAssetCustomGroup() As LongTermAssetCustomGroup
            Return New LongTermAssetCustomGroup()
        End Function


        Private Sub New()
            MarkAsChild()
            ValidationRules.CheckRules()
        End Sub

        Private Sub New(ByVal dr As DataRow)
            MarkAsChild()
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Friend Sub Insert(ByVal parent As LongTermAssetCustomGroupList)

            Dim myComm As New SQLCommand("InsertLongTermAssetCustomGroup")
            myComm.AddParam("?NM", _Name.Trim)
            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As LongTermAssetCustomGroupList)

            Dim myComm As New SQLCommand("UpdateLongTermAssetCustomGroup")
            myComm.AddParam("?CD", _ID)
            myComm.AddParam("?NM", _Name.Trim)
            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            If IsNew Then Exit Sub

            Dim myComm As New SQLCommand("DeleteLongTermAssetCustomGroup")
            myComm.AddParam("?CD", _ID)
            myComm.Execute()

            MarkNew()

        End Sub

        Friend Sub CheckIfItemCanBeDeleted()

            If IsNew Then Exit Sub

            Dim myComm As New SQLCommand("CheckIfLongTermAssetCustomGroupCanBeDeleted")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count > 0 Then
                    Throw New Exception(String.Format(My.Resources.Assets_LongTermAssetCustomGroup_InvalidDelete, _
                        _Name, _ID.ToString()))
                End If
            End Using

        End Sub

#End Region

    End Class

End Namespace
