Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="General.Person">person data</see> value objects.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.</remarks>
    <Serializable()> _
Public NotInheritable Class PersonInfoList
        Inherits ReadOnlyListBase(Of PersonInfoList, PersonInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a person info by a <see cref="General.Person.Code">person code</see>. 
        ''' Returns null if no such code in the database.
        ''' </summary>
        ''' <param name="personCode">A <see cref="General.Person.Code">person code</see> to find.</param>
        ''' <remarks></remarks>
        Public Function GetPersonInfo(ByVal personCode As String) As PersonInfo
            If StringIsNullOrEmpty(personCode) Then Return Nothing
            For Each p As PersonInfo In Me
                If p.Code.Trim.ToLower = personCode.Trim.ToLower Then Return p
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets a person info by a <see cref="General.Person.ID">person ID</see>. 
        ''' Returns null if no such ID in the database.
        ''' </summary>
        ''' <param name="personID">A <see cref="General.Person.ID">person ID</see> to find.</param>
        ''' <remarks></remarks>
        Public Function GetPersonInfo(ByVal personID As Integer) As PersonInfo
            If Not personID > 0 Then Return Nothing
            For Each p As PersonInfo In Me
                If p.ID = personID Then Return p
            Next
            Return Nothing
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.PersonInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current person info value object list from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As PersonInfoList

            Dim result As PersonInfoList = CacheManager.GetItemFromCache(Of PersonInfoList)( _
                GetType(PersonInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of PersonInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(PersonInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current person info value object list.
        ''' </summary>
        ''' <param name="showEmpty">Wheather to include a placeholder object.</param>
        ''' <param name="showClients">Wheather to include clients.</param>
        ''' <param name="showSuppliers">Wheather to include suppliers.</param>
        ''' <param name="showWorkers">Wheather to include workers.</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal showEmpty As Boolean, _
            ByVal showClients As Boolean, ByVal showSuppliers As Boolean, _
            ByVal showWorkers As Boolean) As Csla.FilteredBindingList(Of PersonInfo)

            Dim filterToApply(3) As Object
            filterToApply(0) = ConvertDbBoolean(showEmpty)
            filterToApply(1) = ConvertDbBoolean(showClients)
            filterToApply(2) = ConvertDbBoolean(showSuppliers)
            filterToApply(3) = ConvertDbBoolean(showWorkers)

            Dim result As Csla.FilteredBindingList(Of PersonInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of PersonInfo)) _
                (GetType(PersonInfoList), filterToApply)

            If result Is Nothing Then

                Dim baseList As PersonInfoList = PersonInfoList.GetList
                result = New Csla.FilteredBindingList(Of PersonInfo)(baseList, AddressOf PersonInfoFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(PersonInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current person info value object list cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(PersonInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current person info value object list.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(PersonInfoList))
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
        ''' Gets a current person info value object list from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As PersonInfoList
            Dim result As New PersonInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current person info value object list from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be invoked server side.</remarks>
        Friend Shared Function GetListChild() As PersonInfoList
            Dim result As New PersonInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Shared Function PersonInfoFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse DirectCast(filterValue, Object()).Length < 4 Then Return True

            Dim ShowEmpty As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(0), Integer))
            Dim ShowClients As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(1), Integer))
            Dim ShowSuppliers As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(2), Integer))
            Dim ShowWorkers As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(3), Integer))

            ' no criteria to apply
            If ShowEmpty AndAlso ShowClients AndAlso ShowSuppliers AndAlso ShowWorkers Then Return True

            Dim CI As PersonInfo = DirectCast(item, PersonInfo)

            If ShowClients AndAlso ShowSuppliers AndAlso ShowWorkers AndAlso ShowEmpty Then Return True

            If CI.ID > 0 AndAlso ShowWorkers AndAlso ShowSuppliers AndAlso ShowWorkers Then
                Return True
            Else
                If CI.ID > 0 AndAlso ShowClients AndAlso CI.IsClient Then Return True
                If CI.ID > 0 AndAlso ShowSuppliers AndAlso CI.IsSupplier Then Return True
                If CI.ID > 0 AndAlso ShowWorkers AndAlso CI.IsWorker Then Return True
            End If
            If ShowEmpty AndAlso Not CI.ID > 0 Then Return True

            Return False

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

            Dim myComm As New SQLCommand("FetchPersonInfoList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                Add(PersonInfo.Empty)
                For Each dr As DataRow In myData.Rows
                    Add(PersonInfo.GetPersonInfo(dr, 0))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace