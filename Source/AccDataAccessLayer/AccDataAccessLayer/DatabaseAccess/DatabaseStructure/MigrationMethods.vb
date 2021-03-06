Imports MySql.Data.MySqlClient
Imports System.Data.SQLite
Namespace DatabaseAccess.DatabaseStructure

    Public Module MigrationMethods

        Private Const CommandTimeOut As Integer = 1000 * 60

        Public Sub ConvertMySqlToSQLite(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

            Dim worker As System.ComponentModel.BackgroundWorker = Nothing
            If Not sender Is Nothing AndAlso TypeOf sender Is System.ComponentModel.BackgroundWorker Then _
                worker = DirectCast(sender, System.ComponentModel.BackgroundWorker)
            Dim credentials As MySqlToSQLiteCredentials = DirectCast(e.Argument, MySqlToSQLiteCredentials)

            Dim mySqlConn As MySqlConnection = Nothing
            Dim sqliteConn As SQLiteConnection = Nothing
            Dim myTrans As SQLiteTransaction = Nothing

            Dim mysqlGenerator As New SqlServerSpecificMethods.MySqlGenerator
            Dim sqliteGenerator As New SqlServerSpecificMethods.SQLiteGenerator

            Dim CurrentSqlCommand As String = ""

            Try

                mySqlConn = AccDataAccessLayer.SqlServerSpecificMethods.MySqlCommandManager. _
                    OpenConnection(credentials.GetMySqlConnectionString(False))

                sqliteConn = AccDataAccessLayer.SqlServerSpecificMethods.SQLiteCommandManager. _
                    OpenConnection(credentials.GetSQLiteConnectionString)

                If Not worker Is Nothing Then worker.ReportProgress(0, "Creating database...")

                Using myComm As New SQLiteCommand(sqliteConn)

                    myComm.CommandTimeout = CommandTimeOut

                    For Each tbl As DatabaseTable In credentials.Structure.TableList

                        CurrentSqlCommand = tbl.GetAddTableStatement("", sqliteGenerator)
                        myComm.CommandText = CurrentSqlCommand

                        myComm.ExecuteNonQuery()

                        For Each f As DatabaseField In tbl.FieldList
                            If Not String.IsNullOrEmpty(f.IndexName.Trim) AndAlso Not f.PrimaryKey Then

                                CurrentSqlCommand = f.GetCreateIndexStatement("", tbl.Name, sqliteGenerator)
                                myComm.CommandText = CurrentSqlCommand

                                myComm.ExecuteNonQuery()

                            End If
                        Next

                    Next

                    Dim totalRows As Integer = 0

                    Using fetchComm As New MySqlCommand()

                        fetchComm.Connection = mySqlConn
                        fetchComm.CommandTimeout = CommandTimeOut

                        Dim tablesWithRows As New List(Of DatabaseTable)

                        For Each tbl As DatabaseTable In credentials.Structure.TableList

                            fetchComm.CommandText = String.Format("SELECT COUNT(*) FROM {0};", tbl.Name)

                            Using myAdapter As New MySqlDataAdapter

                                myAdapter.SelectCommand = fetchComm

                                Using data As New DataTable

                                    myAdapter.Fill(data)

                                    If data.Rows.Count > 0 Then
                                        Dim rowsInTable As Integer = CIntSafe(data.Rows(0).Item(0), 0)
                                        totalRows += rowsInTable
                                        If rowsInTable > 0 Then tablesWithRows.Add(tbl)
                                    End If

                                End Using

                            End Using

                        Next

                        myTrans = sqliteConn.BeginTransaction()
                        myComm.Transaction = myTrans

                        Dim rowsPerPercent As Double = totalRows / 100
                        Dim currentPercents As Integer = 0
                        Dim currentRows As Integer = 0

                        For Each tbl As DatabaseTable In tablesWithRows

                            If Not worker Is Nothing AndAlso worker.CancellationPending Then Throw New Exception(
                                "Data migration was interrupted by user. Database is incomplete.")

                            CurrentSqlCommand = GetSelectStatement(tbl)
                            fetchComm.CommandText = CurrentSqlCommand

                            Using myAdapter As New MySqlDataAdapter

                                myAdapter.SelectCommand = fetchComm

                                Using data As New DataTable

                                    myAdapter.Fill(data)

                                    CurrentSqlCommand = GetInsertStatement(tbl, "$A")
                                    myComm.CommandText = CurrentSqlCommand

                                    For Each dr As DataRow In data.Rows

                                        AddWithParams(myComm, dr, "$A")
                                        myComm.ExecuteNonQuery()

                                        currentRows += 1
                                        If Convert.ToInt32(Math.Floor(currentRows / rowsPerPercent)) > currentPercents Then
                                            currentPercents = Convert.ToInt32(Math.Floor(currentRows / rowsPerPercent))
                                            If Not worker Is Nothing Then worker.ReportProgress(Math.Min(currentPercents, 100),
                                                String.Format("Transfered {0} rows out of total {1} rows, current table {2}...",
                                                currentRows.ToString, totalRows.ToString, tbl.Name))
                                        End If

                                    Next

                                End Using

                            End Using

                        Next

                    End Using

                End Using

                myTrans.Commit()

                If Not worker Is Nothing Then worker.ReportProgress(100, _
                    "Migration has completed successfuly.")

            Catch ex As Exception

                If Not myTrans Is Nothing Then
                    Try
                        myTrans.Rollback()
                    Catch ex2 As Exception
                    End Try
                    Try
                        myTrans.Dispose()
                    Catch ex3 As Exception
                    End Try
                End If

                If Not mySqlConn Is Nothing AndAlso mySqlConn.State <> ConnectionState.Closed Then mySqlConn.Close()
                If Not mySqlConn Is Nothing Then mySqlConn.Dispose()
                If Not sqliteConn Is Nothing AndAlso sqliteConn.State <> ConnectionState.Closed Then sqliteConn.Close()
                If Not sqliteConn Is Nothing Then sqliteConn.Dispose()

                If TypeOf ex Is MySql.Data.MySqlClient.MySqlException AndAlso _
                    (CType(ex, MySql.Data.MySqlClient.MySqlException).ErrorCode = _
                    MySql.Data.MySqlClient.MySqlErrorCode.AccessDenied OrElse _
                    CType(ex, MySql.Data.MySqlClient.MySqlException).ErrorCode = _
                    MySql.Data.MySqlClient.MySqlErrorCode.PasswordNoMatch) Then

                    Throw New Exception("Klaida. Jūs neturite teisės prisijungti " _
                        & "prie serverio arba nurodytas klaidingas slaptažodis.")

                ElseIf TypeOf ex Is MySql.Data.MySqlClient.MySqlException AndAlso _
                    CType(ex, MySql.Data.MySqlClient.MySqlException).ErrorCode = _
                    MySql.Data.MySqlClient.MySqlErrorCode.UnableToConnectToHost Then

                    Throw New Exception("Klaida. Išjungtas SQL serverio servisas " _
                        & "arba klaidingai nurodytas SQL serverio adresas ar portas.")

                ElseIf Not String.IsNullOrEmpty(CurrentSqlCommand.Trim) Then

                    Throw New Exception("Klaida vykdant sakinį (statement): " & vbCrLf _
                        & CurrentSqlCommand & vbCrLf & "Klaidos turinys: '" & ex.Message & "'.", ex)

                Else

                    Throw New Exception("Nenustatyta klaida: " & ex.Message, ex)

                End If

            End Try

            If Not mySqlConn Is Nothing AndAlso mySqlConn.State <> ConnectionState.Closed Then mySqlConn.Close()
            If Not mySqlConn Is Nothing Then mySqlConn.Dispose()
            If Not sqliteConn Is Nothing AndAlso sqliteConn.State <> ConnectionState.Closed Then sqliteConn.Close()
            If Not sqliteConn Is Nothing Then sqliteConn.Dispose()

        End Sub

        Public Sub ConvertSQLiteToMySql(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

            Dim worker As System.ComponentModel.BackgroundWorker = Nothing
            If Not sender Is Nothing AndAlso TypeOf sender Is System.ComponentModel.BackgroundWorker Then _
                worker = DirectCast(sender, System.ComponentModel.BackgroundWorker)
            Dim credentials As MySqlToSQLiteCredentials = DirectCast(e.Argument, MySqlToSQLiteCredentials)

            Dim mySqlConn As MySqlConnection = Nothing
            Dim sqliteConn As SQLiteConnection = Nothing
            Dim myTrans As MySqlTransaction = Nothing

            Dim mysqlGenerator As New SqlServerSpecificMethods.MySqlGenerator
            Dim sqliteGenerator As New SqlServerSpecificMethods.SQLiteGenerator

            Dim CurrentSqlCommand As String = ""

            Try

                mySqlConn = AccDataAccessLayer.SqlServerSpecificMethods.MySqlCommandManager. _
                    OpenConnection(credentials.GetMySqlConnectionString(True))

                sqliteConn = AccDataAccessLayer.SqlServerSpecificMethods.SQLiteCommandManager. _
                    OpenConnection(credentials.GetSQLiteConnectionString)

                If Not worker Is Nothing Then worker.ReportProgress(0, "Creating database...")

                Using myComm As New MySqlCommand()
                    myComm.Connection = mySqlConn
                    myComm.CommandTimeout = CommandTimeOut
                    If credentials.Structure.CharsetName Is Nothing OrElse _
                        String.IsNullOrEmpty(credentials.Structure.CharsetName.Trim) Then
                        CurrentSqlCommand = "CREATE DATABASE " & credentials.Database.Trim & ";"
                    Else
                        CurrentSqlCommand = "CREATE DATABASE " & credentials.Database.Trim & _
                            " CHARACTER SET " & credentials.Structure.CharsetName.Trim & ";"
                    End If
                    myComm.CommandText = CurrentSqlCommand
                    myComm.ExecuteNonQuery()
                End Using

                mySqlConn.Close()

                mySqlConn = AccDataAccessLayer.SqlServerSpecificMethods.MySqlCommandManager. _
                    OpenConnection(credentials.GetMySqlConnectionString(False))

                Using myComm As New MySqlCommand()

                    myComm.Connection = mySqlConn
                    myComm.CommandTimeout = CommandTimeOut

                    For Each tbl As DatabaseTable In credentials.Structure.TableList

                        CurrentSqlCommand = tbl.GetAddTableStatement(credentials.Database, mysqlGenerator)
                        myComm.CommandText = CurrentSqlCommand

                        myComm.ExecuteNonQuery()

                    Next

                    For Each proc As DatabaseStoredProcedure In credentials.Structure.StoredProcedureList

                        CurrentSqlCommand = proc.GetCreateProcedureStatement(credentials.Database, mysqlGenerator)
                        myComm.CommandText = CurrentSqlCommand

                        myComm.ExecuteNonQuery()

                    Next

                    Dim totalRows As Integer = 0

                    Using fetchComm As New SQLiteCommand(sqliteConn)

                        fetchComm.CommandTimeout = CommandTimeOut
                        Dim tablesWithRows As New List(Of DatabaseTable)

                        For Each tbl As DatabaseTable In credentials.Structure.TableList

                            fetchComm.CommandText = String.Format("SELECT COUNT(*) FROM {0};", tbl.Name)

                            Using myAdapter As New SQLiteDataAdapter

                                myAdapter.SelectCommand = fetchComm

                                Using data As New DataTable

                                    myAdapter.Fill(data)

                                    If data.Rows.Count > 0 Then
                                        Dim rowsInTable As Integer = CIntSafe(data.Rows(0).Item(0), 0)
                                        totalRows += rowsInTable
                                        If rowsInTable > 0 Then tablesWithRows.Add(tbl)
                                    End If

                                End Using

                            End Using

                        Next

                        Dim rowsPerPercent As Double = totalRows / 100
                        Dim currentPercents As Integer = 0
                        Dim currentRows As Integer = 0

                        myTrans = mySqlConn.BeginTransaction()
                        myComm.Transaction = myTrans

                        For Each tbl As DatabaseTable In tablesWithRows

                            If Not worker Is Nothing AndAlso e.Cancel Then Throw New Exception(
                                "Data migration was interrupted by user. Database is incomplete.")

                            CurrentSqlCommand = GetSelectStatement(tbl)
                            fetchComm.CommandText = CurrentSqlCommand

                            Using myAdapter As New SQLiteDataAdapter

                                myAdapter.SelectCommand = fetchComm

                                Using data As New DataTable

                                    myAdapter.Fill(data)

                                    CurrentSqlCommand = GetInsertStatement(tbl, "?A")
                                    myComm.CommandText = CurrentSqlCommand

                                    For Each dr As DataRow In data.Rows

                                        AddWithParams(myComm, dr, "?A")
                                        myComm.ExecuteNonQuery()

                                        currentRows += 1
                                        If Convert.ToInt32(Math.Floor(currentRows / rowsPerPercent)) > currentPercents Then
                                            currentPercents = Convert.ToInt32(Math.Floor(currentRows / rowsPerPercent))
                                            If Not worker Is Nothing Then worker.ReportProgress(Math.Min(currentPercents, 100),
                                                String.Format("Transfered {0} rows out of total {1} rows, current table {2}...",
                                                currentRows.ToString, totalRows.ToString, tbl.Name))
                                        End If

                                    Next

                                End Using

                            End Using

                        Next

                    End Using

                End Using

                myTrans.Commit()

                If Not worker Is Nothing Then worker.ReportProgress(100, _
                    "Migration has completed successfuly.")

            Catch ex As Exception

                If Not myTrans Is Nothing Then
                    Try
                        myTrans.Rollback()
                    Catch ex2 As Exception
                    End Try
                    Try
                        myTrans.Dispose()
                    Catch ex3 As Exception
                    End Try
                End If

                If Not mySqlConn Is Nothing AndAlso mySqlConn.State <> ConnectionState.Closed Then mySqlConn.Close()
                If Not mySqlConn Is Nothing Then mySqlConn.Dispose()
                If Not sqliteConn Is Nothing AndAlso sqliteConn.State <> ConnectionState.Closed Then sqliteConn.Close()
                If Not sqliteConn Is Nothing Then sqliteConn.Dispose()

                If TypeOf ex Is MySql.Data.MySqlClient.MySqlException AndAlso _
                    (CType(ex, MySql.Data.MySqlClient.MySqlException).ErrorCode = _
                    MySql.Data.MySqlClient.MySqlErrorCode.AccessDenied OrElse _
                    CType(ex, MySql.Data.MySqlClient.MySqlException).ErrorCode = _
                    MySql.Data.MySqlClient.MySqlErrorCode.PasswordNoMatch) Then

                    Throw New Exception("Klaida. Jūs neturite teisės prisijungti " _
                        & "prie serverio arba nurodytas klaidingas slaptažodis.")

                ElseIf TypeOf ex Is MySql.Data.MySqlClient.MySqlException AndAlso _
                    CType(ex, MySql.Data.MySqlClient.MySqlException).ErrorCode = _
                    MySql.Data.MySqlClient.MySqlErrorCode.UnableToConnectToHost Then

                    Throw New Exception("Klaida. Išjungtas SQL serverio servisas " _
                        & "arba klaidingai nurodytas SQL serverio adresas ar portas.")

                ElseIf Not String.IsNullOrEmpty(CurrentSqlCommand.Trim) Then

                    Throw New Exception("Klaida vykdant sakinį (statement): " & vbCrLf _
                        & CurrentSqlCommand & vbCrLf & "Klaidos turinys: '" & ex.Message & "'.", ex)

                Else

                    Throw New Exception("Nenustatyta klaida: " & ex.Message, ex)

                End If

            End Try

            If Not mySqlConn Is Nothing AndAlso mySqlConn.State <> ConnectionState.Closed Then mySqlConn.Close()
            If Not mySqlConn Is Nothing Then mySqlConn.Dispose()
            If Not sqliteConn Is Nothing AndAlso sqliteConn.State <> ConnectionState.Closed Then sqliteConn.Close()
            If Not sqliteConn Is Nothing Then sqliteConn.Dispose()

        End Sub


        Private Function GetSelectStatement(ByVal tbl As DatabaseTable) As String

            Dim fields As New List(Of String)
            For Each f As DatabaseField In tbl.FieldList
                fields.Add(f.Name)
            Next

            Return "SELECT " & String.Join(", ", fields.ToArray) & " FROM " & tbl.Name & ";"

        End Function

        Private Function GetInsertStatement(ByVal tbl As DatabaseTable, ByVal paramPrefix As String) As String

            Dim fields As New List(Of String)
            Dim params As New List(Of String)
            Dim i As Integer = 1
            For Each f As DatabaseField In tbl.FieldList
                fields.Add(f.Name)
                params.Add(paramPrefix & i.ToString)
                i += 1
            Next

            Return "INSERT INTO " & tbl.Name & "(" & String.Join(", ", fields.ToArray) _
                & ") VALUES(" & String.Join(", ", params.ToArray) & ");"

        End Function

        Private Sub AddWithParams(ByRef myComm As MySqlCommand, ByVal dr As DataRow, ByVal paramPrefix As String)
            myComm.Parameters.Clear()
            For i As Integer = 1 To dr.Table.Columns.Count
                myComm.Parameters.AddWithValue(paramPrefix & i.ToString, dr.Item(i - 1))
            Next
        End Sub

        Private Sub AddWithParams(ByRef myComm As SQLiteCommand, ByVal dr As DataRow, ByVal paramPrefix As String)
            myComm.Parameters.Clear()
            For i As Integer = 1 To dr.Table.Columns.Count
                myComm.Parameters.AddWithValue(paramPrefix & i.ToString, dr.Item(i - 1))
            Next
        End Sub

        Public Class MySqlToSQLiteCredentials
            Public Host As String
            Public Port As Integer
            Public User As String
            Public MySqlPassword As String
            Public Database As String
            Public File As String
            Public SQLitePassword As String
            Public [Structure] As DatabaseStructure
            Public Sub New(ByVal nHost As String, ByVal nPort As Integer, ByVal nUser As String, _
                ByVal nMySqlPassword As String, ByVal nDatabase As String, ByVal nFile As String, _
                ByVal nSQLitePassword As String, ByVal nStructure As DatabaseStructure)
                Host = nHost
                Port = nPort
                User = nUser
                MySqlPassword = nMySqlPassword
                Database = nDatabase
                File = nFile
                SQLitePassword = nSQLitePassword
                If nStructure Is Nothing Then nStructure = DatabaseStructure.GetDatabaseStructureServerSide
                [Structure] = nStructure
            End Sub
            Public Function GetMySqlConnectionString(ByVal withoutDatabase As Boolean) As String
                If withoutDatabase OrElse Database Is Nothing OrElse String.IsNullOrEmpty(Database.Trim) Then
                    Return "Server=" & Host.Trim & ";" & "Port=" & Port.ToString & ";" & "user id=" _
                        & User.Trim & ";" & "password=" & MySqlPassword.Trim
                Else
                    Return "Server=" & Host.Trim & ";" & "Port=" & Port.ToString & ";" & "user id=" _
                        & User.Trim & ";" & "password=" & MySqlPassword.Trim & ";" & "database=" _
                        & Database.Trim
                End If
            End Function
            Public Function GetSQLiteConnectionString() As String
                If SQLitePassword Is Nothing OrElse String.IsNullOrEmpty(SQLitePassword.Trim) Then
                    Return "Data Source=" & File.Trim & ";Version=3;UseUTF8Encoding=True;"
                Else
                    Return "Data Source=" & File.Trim & ";Version=3;UseUTF8Encoding=True;Password=" _
                        & SQLitePassword.Trim & ";"
                End If
            End Function
        End Class

    End Module

End Namespace