Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="Documents.CashAccount">cash account</see> value objects.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.</remarks>
    <Serializable()> _
    Public NotInheritable Class CashAccountInfoList
        Inherits ReadOnlyListBase(Of CashAccountInfoList, CashAccountInfo)

#Region " Business Methods "

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.CashAccountInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current cash account value object list from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As CashAccountInfoList

            Dim result As CashAccountInfoList = CacheManager.GetItemFromCache(Of CashAccountInfoList)( _
                GetType(CashAccountInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of CashAccountInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(CashAccountInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current cash account value object list.
        ''' </summary>
        ''' <param name="showEmpty">Wheather to include a placeholder object.</param>
        ''' <param name="showObsolete">Wheather to include obsolete cash accounts (no longer in use).</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal showObsolete As Boolean, _
            ByVal showEmpty As Boolean) As Csla.FilteredBindingList(Of CashAccountInfo)

            Dim filterToApply(1) As Object
            filterToApply(0) = ConvertDbBoolean(showObsolete)
            filterToApply(1) = ConvertDbBoolean(showEmpty)

            Dim result As Csla.FilteredBindingList(Of CashAccountInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of CashAccountInfo)) _
                (GetType(CashAccountInfoList), filterToApply)


            If result Is Nothing Then

                Dim baseList As CashAccountInfoList = CashAccountInfoList.GetList
                result = New Csla.FilteredBindingList(Of CashAccountInfo)(baseList, _
                    AddressOf CashAccountInfoFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(CashAccountInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current cash account value object list cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(CashAccountInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current cash account value object list.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(CashAccountInfoList))
        End Function

        ''' <summary>
        ''' Returns true if the collection is common across all the databases.
        ''' I.e. cache is not to be cleared on changing databases.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function IsApplicationWideCache() As Boolean
            Return False
        End Function

        ''' <summary>
        ''' Gets a current cash account value object list from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As CashAccountInfoList
            Dim result As New CashAccountInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Shared Function CashAccountInfoFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing Then Return True

            Dim showObsolete As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(0), Integer))
            Dim showEmpty As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(1), Integer))

            ' no criteria to apply
            If showObsolete AndAlso showEmpty Then Return True

            Dim current As CashAccountInfo = DirectCast(item, CashAccountInfo)

            If Not showEmpty AndAlso Not current.ID > 0 Then Return False
            If Not showObsolete AndAlso current.IsObsolete AndAlso current.ID > 0 Then Return False

            Return True

        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchCashAccountInfoList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(CashAccountInfo.GetCashAccountInfo(dr, 0))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace