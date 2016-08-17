Namespace HelperLists

    ''' <summary>
    ''' A value object list that provides serials, numbers and dates of existing labour contracts.
    ''' </summary>
    ''' <remarks>Should be used as a datasource for a labour contract identification.</remarks>
    <Serializable()> _
Public NotInheritable Class ShortLabourContractList
        Inherits ReadOnlyListBase(Of ShortLabourContractList, ShortLabourContract)

#Region " Business Methods "

        ''' <summary>
        ''' Finds a contract within the list. Returns nothing if no such contract within the list.
        ''' </summary>
        ''' <param name="nSerial">Serial to find.</param>
        ''' <param name="nNumber">Number to find.</param>
        ''' <remarks></remarks>
        Public Function GetShortLabourContract(ByVal nSerial As String, _
            ByVal nNumber As Integer) As ShortLabourContract

            For Each s As ShortLabourContract In Me
                If nSerial.Trim.ToLower = s.Serial.Trim.ToLower _
                    AndAlso nNumber = s.Number Then Return s
            Next

            Return Nothing

        End Function

        ''' <summary>
        ''' Returnes whether a labour contract with the required serial and number exists within the list.
        ''' </summary>
        ''' <param name="nSerial">Serial to find.</param>
        ''' <param name="nNumber">Number to find.</param>
        ''' <remarks></remarks>
        Public Function Exists(ByVal nSerial As String, ByVal nNumber As Integer) As Boolean
            For Each s As ShortLabourContract In Me
                If nSerial.Trim.ToLower = s.Serial.Trim.ToLower _
                    AndAlso nNumber = s.Number Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.ShortLabourContractList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a list of all existing labour contracts.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function GetList() As ShortLabourContractList
            Return DataPortal.Fetch(Of ShortLabourContractList)(New Criteria())
        End Function

        ''' <summary>
        ''' Gets a list of all existing labour contracts that are entered into with a particular person.
        ''' </summary>
        ''' <param name="nPersonID">ID of a person.</param>
        ''' <remarks></remarks>
        Public Shared Function GetList(ByVal nPersonID As Integer) As ShortLabourContractList
            Return DataPortal.Fetch(Of ShortLabourContractList)(New Criteria(nPersonID))
        End Function

        ''' <summary>
        ''' Gets a list of all existing labour contracts that are entered into 
        ''' with a particular person before the date specified.
        ''' </summary>
        ''' <param name="nPersonID">ID of a person.</param>
        ''' <param name="nDate">Maximum allowed date of a contract.</param>
        ''' <remarks></remarks>
        Public Shared Function GetList(ByVal nPersonID As Integer, ByVal nDate As Date) As ShortLabourContractList
            Return DataPortal.Fetch(Of ShortLabourContractList)(New Criteria(nPersonID, nDate))
        End Function

        ''' <summary>
        ''' Gets as a child a list of all existing labour contracts.
        ''' </summary>
        ''' <remarks>Should only be called server side.</remarks>
        Friend Shared Function GetListChild() As ShortLabourContractList
            Return New ShortLabourContractList(New Criteria())
        End Function

        ''' <summary>
        ''' Gets as a child a list of all existing labour contracts that are entered into 
        ''' with a particular person before the date specified.
        ''' </summary>
        ''' <param name="nPersonID">ID of a person.</param>
        ''' <param name="nDate">Maximum allowed date of a contract.</param>
        ''' <remarks>Should only be called server side.</remarks>
        Friend Shared Function GetListChild(ByVal nPersonID As Integer, ByVal nDate As Date) As ShortLabourContractList
            Return New ShortLabourContractList(New Criteria(nPersonID, nDate))
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nCriteria As Criteria)
            DataPortal_Fetch(nCriteria)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _PersonID As Integer = 0
            Private _IsForPerson As Boolean = False
            Private _Date As Date = Today.AddYears(50)
            Public ReadOnly Property PersonID() As Integer
                Get
                    Return _PersonID
                End Get
            End Property
            Public ReadOnly Property IsForPerson() As Boolean
                Get
                    Return _IsForPerson
                End Get
            End Property
            Public ReadOnly Property [Date]() As Date
                Get
                    Return _Date
                End Get
            End Property
            Public Sub New(ByVal nPersonID As Integer)
                _PersonID = nPersonID
                _IsForPerson = True
            End Sub
            Public Sub New(ByVal nPersonID As Integer, ByVal nDate As Date)
                _PersonID = nPersonID
                _IsForPerson = True
                _Date = nDate.Date
            End Sub
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As SQLCommand
            If criteria.IsForPerson Then
                myComm = New SQLCommand("FetchShortLabourContractListForPerson")
                myComm.AddParam("?PD", criteria.PersonID)
            Else
                myComm = New SQLCommand("FetchShortLabourContractList")
            End If
            myComm.AddParam("?DT", criteria.Date.Date)

            Using myData As DataTable = myComm.Fetch
                RaiseListChangedEvents = False
                IsReadOnly = False
                For Each dr As DataRow In myData.Rows
                    Add(New ShortLabourContract(CIntSafe(dr.Item(0), 0), _
                        CStrSafe(dr.Item(1)), CDateSafe(dr.Item(2), Date.MinValue)))
                Next
                IsReadOnly = True
                RaiseListChangedEvents = True
            End Using

        End Sub

#End Region

    End Class

End Namespace