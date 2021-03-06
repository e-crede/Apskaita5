Namespace DatabaseAccess

    ''' <summary>
    ''' Provides an abstract SQL command and a list of parameters for a SQL command.
    ''' </summary>
    <Serializable()> _
    Public Class SQLCommand
        Private _Key As String = ""
        Private _Params As New List(Of SQLParam)
        Private _LastInsertID As Long = -1
        Private _Sentence As String = ""

#Region "*** Properties ***"

        ''' <summary>
        ''' Gets or sets SQL command's sentence (statement) key in the depository.
        ''' </summary>
        Public Property Key() As String
            Get
                Return _Key
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) Then _
                    Throw New Exception("Klaida. Nenurodytas SQL sakinio (statement) kodas.")
                If value = "RawSQL" Then _
                    Throw New Exception("Klaida. Raw SQL sakinys gali būti įvedamas " _
                    & "tik sukuriant naują SQLCommand objektą.")
                If Not SQLDepositoryKeyExists(value) Then _
                    Throw New Exception("Klaida. Nežinomas SQL sakinys (statement), kurio kodas '" & Key & "'.")
                _Key = value
                _Sentence = ""
            End Set
        End Property

        ''' <summary>
        ''' Gets SQL command sentence (statement) from depository by it's key.
        ''' </summary>
        Public ReadOnly Property Sentence() As String
            Get
                If _Key = "RawSQL" Then Return _Sentence
                If Not String.IsNullOrEmpty(_Sentence) Then Return _Sentence
                Dim result As String = GetSQLStatement(_Key)
                For i As Integer = 1 To _Params.Count
                    If _Params.Item(i - 1).ToBeReplaced Then _
                        result = result.Replace(_Params.Item(i - 1).Name, _
                            _Params.Item(i - 1).Value.ToString)
                Next
                Return result
            End Get
        End Property

        ''' <summary>
        ''' Gets the ID of a row inserted by SQL command sentence (statement).
        ''' Used in the SQL utilities module to retrieve objects ID's after insertion.
        ''' </summary>
        Public ReadOnly Property LastInsertID() As Long
            Get
                Return _LastInsertID
            End Get
        End Property

        ''' <summary>
        ''' Gets an item from the list of SQLParam objects associated with the command.
        ''' </summary>
        ''' <param name="Index">Index of SQLParam object in the associated collection.</param>
        Public ReadOnly Property Params(ByVal Index As Integer) As SQLParam
            Get
                Return _Params(Index)
            End Get
        End Property

        ''' <summary>
        ''' Gets a number (count) of SQLParam objects associated with the command.
        ''' </summary>
        Public ReadOnly Property ParamsCount() As Integer
            Get
                Return _Params.Count
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the command the command. Returns command for empty sentence.
        ''' </summary>
        Public ReadOnly Property CommandType() As SQLStatementType
            Get
                If String.IsNullOrEmpty(Sentence) OrElse String.IsNullOrEmpty(Sentence.Trim) _
                    Then Return SQLStatementType.Command
                If Sentence.Trim.ToUpper.StartsWith("SELECT") OrElse _
                    Sentence.Trim.ToUpper.StartsWith("SHOW") Then Return SQLStatementType.Selection
                If Sentence.Trim.ToUpper.StartsWith("CALL") Then Return SQLStatementType.Procedure
                Return SQLStatementType.Command
            End Get
        End Property

#End Region

        ''' <summary>
        ''' Adds a new SQLParam object to Params list. If the value is null an assumed value type is string.
        ''' </summary>
        ''' <param name="ParName">The name of the parameter.</param>
        ''' <param name="ParValue">The value (object) of the parameter.</param>
        Public Sub AddParam(ByVal ParName As String, ByVal ParValue As Object)
            _Params.Add(New SQLParam(ParName, ParValue))
        End Sub

        ''' <summary>
        ''' Adds a new SQLParam object to Params list.
        ''' </summary>
        ''' <param name="ParName">The name of the parameter.</param>
        ''' <param name="ParValue">The value (object) of the parameter.</param>
        ''' <param name="ParValueType">The type of the value (object) of the parameter.</param>
        Public Sub AddParam(ByVal ParName As String, ByVal ParValue As Object, ByVal ParValueType As Type)
            _Params.Add(New SQLParam(ParName, ParValue, ParValueType))
        End Sub

        ''' <summary>
        ''' Do not allow creation of a new empty SQLCommand object without key.
        ''' </summary>
        Private Sub New()

        End Sub

        ''' <summary>
        ''' Creates a new SQLCommand object by its key in the SQL depository.
        ''' </summary>
        ''' <param name="ComKey">The SQL sentence (statement) key in the sql depository. 
        ''' Use RawSQL for non depository statements</param>
        ''' <param name="RawSQLStatement"> The SQL sentence (statement). 
        ''' Only considered if key is RawSQL. </param>
        Public Sub New(ByVal ComKey As String)
            If String.IsNullOrEmpty(ComKey) Then Throw New Exception( _
                "Klaida. Nenurodytas SQL sakinio (statement) kodas.")
            If Not SQLDepositoryKeyExists(ComKey) Then Throw New Exception( _
                "Klaida. Nežinomas SQL sakinys (statement), kurio kodas '" & ComKey & "'.")
            _Key = ComKey
            _Sentence = ""
        End Sub

        ''' <summary>
        ''' Creates a new SQLCommand object by providing raw sql query.
        ''' </summary>
        ''' <param name="RawSQL">Use "RawSQL" for non depository statements</param>
        ''' <param name="RawSQLStatement"> The SQL sentence (statement). 
        ''' Only considered if key is RawSQL. </param>
        Public Sub New(ByVal RawSQL As String, ByVal RawSQLStatement As String)
            If RawSQL Is Nothing OrElse String.IsNullOrEmpty(RawSQL.Trim) _
                OrElse RawSQL.Trim.ToLower <> "rawsql" Then Throw New Exception( _
                "Klaida. Nenurodytas SQL sakinio (statement) kodas. Turi būti 'RawSQL'.")
            If RawSQLStatement Is Nothing OrElse String.IsNullOrEmpty(RawSQLStatement.Trim) Then _
                Throw New Exception("Klaida. Nenurodytas SQL sakinys (statement/query).")
            _Key = RawSQL.Trim
            _Sentence = RawSQLStatement
        End Sub

        Public Overrides Function ToString() As String
            Dim tmp As String = "[" & Sentence & "], Params: "
            For i As Integer = 1 To _Params.Count
                tmp = tmp & _Params(i - 1).ToString
            Next
            Return tmp
        End Function

        ''' <summary>
        ''' Fetches datatable by executing the SQL command of SELECT type.
        ''' </summary>
        Public Function Fetch() As DataTable
            Return FetchCommand(Me)
        End Function

        ''' <summary>
        ''' Executes the SQL command of non SELECT type.
        ''' </summary>
        Public Function Execute() As Integer
            Dim result As Integer = 0
            _LastInsertID = ExecuteCommand(Me, result)
            Return result
        End Function

    End Class

End Namespace
