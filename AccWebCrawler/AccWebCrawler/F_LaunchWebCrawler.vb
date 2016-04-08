Public Class F_LaunchWebCrawler

    Private client As System.Net.WebClient = Nothing
    Private _WebTarget As WebTarget
    Private _PersonCode As String = ""
    Private _BaseCurrency As String = "LTL"
    Private _CurrencyList As List(Of CurrencyRate) = Nothing
    Private _result As Object = Nothing
    Private _CourtRequest As CourtRequest = Nothing
    Private _FileUrl As String = ""
    Private _FilePath As String = ""
    Private _Message As String = ""
    Private _DateConversionFunction As ConvertStringToDateDelegate = Nothing
    Private _FileEncoding As System.Text.Encoding
    Private _RemoteFileEncoding As System.Text.Encoding
    Private _UseRekvizitaiDatabase As Boolean = False


    Private Enum WebTarget
        PersonInfo
        CurencyInfo
        CourtInfo
        AvailableUpdateInfo
        DownloadFile
    End Enum


    Public ReadOnly Property result() As Object
        Get
            Return _result
        End Get
    End Property


    Public Sub New(ByVal PersonCode As String, ByVal useRekvizitaiDatabase As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _WebTarget = WebTarget.PersonInfo
        _PersonCode = PersonCode
        _UseRekvizitaiDatabase = useRekvizitaiDatabase

    End Sub

    Public Sub New(ByVal CurrencyList As List(Of CurrencyRate), ByVal BaseCurrency As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _WebTarget = WebTarget.CurencyInfo
        _CurrencyList = CurrencyList
        _BaseCurrency = BaseCurrency

    End Sub

    Public Sub New(ByVal nCourtType As CourtType, ByVal FolderPath As String, _
        ByVal DateFrom As Date, ByVal DateTo As Date)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _WebTarget = WebTarget.CourtInfo
        _CourtRequest = New CourtRequest(nCourtType, FolderPath, DateFrom, DateTo)

    End Sub

    Public Sub New(ByVal FileUrl As String, ByVal FilePath As String, ByVal Message As String)

        If FileUrl Is Nothing OrElse String.IsNullOrEmpty(FileUrl.Trim) Then _
            Throw New ArgumentNullException("Klaida. Nenurodytas failo url.")
        If FilePath Is Nothing OrElse String.IsNullOrEmpty(FilePath.Trim) Then _
            Throw New ArgumentNullException("Klaida. Nenurodytas failo pavadinimas.")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _WebTarget = WebTarget.DownloadFile
        _FileUrl = FileUrl
        _FilePath = FilePath
        _Message = Message

    End Sub

    Public Sub New(ByVal FileUrl As String, ByVal ComparisionFilePath As String, _
        ByVal Message As String, ByVal FileEncoding As System.Text.Encoding, _
        ByVal RemoteFileEncoding As System.Text.Encoding, _
        ByVal DateConversionFunction As ConvertStringToDateDelegate)

        If FileUrl Is Nothing OrElse String.IsNullOrEmpty(FileUrl.Trim) Then _
            Throw New ArgumentNullException("Klaida. Nenurodytas datos failo url.")
        If ComparisionFilePath Is Nothing OrElse String.IsNullOrEmpty(ComparisionFilePath.Trim) Then _
            Throw New ArgumentNullException("Klaida. Nenurodytas datos failo pavadinimas.")
        If Not IO.File.Exists(ComparisionFilePath.Trim) Then Throw New IO.FileNotFoundException( _
            "Klaida. Nerastas datos failas.")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _WebTarget = WebTarget.AvailableUpdateInfo
        _FileUrl = FileUrl
        _FilePath = ComparisionFilePath
        _Message = Message
        _FileEncoding = FileEncoding
        _RemoteFileEncoding = RemoteFileEncoding
        _DateConversionFunction = DateConversionFunction

    End Sub


    Private Sub F_LaunchWebCrawler_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _WebTarget = WebTarget.PersonInfo Then

            If _UseRekvizitaiDatabase Then
                ProgressLabel.Text = "Gaunami duomenys iš Rekvizitai.lt..."
            Else
                ProgressLabel.Text = "Gaunami duomenys iš juridinių asmenų registro..."
            End If
            ProgressBar1.Style = Windows.Forms.ProgressBarStyle.Marquee

            AddHandler BackgroundWorker1.DoWork, AddressOf GetPersonInfoRekvizitai

            BackgroundWorker1.RunWorkerAsync(_PersonCode)

        ElseIf _WebTarget = WebTarget.CurencyInfo Then

            If _BaseCurrency.Trim.ToUpper = "LTL" Then
                ProgressLabel.Text = "Gaunami duomenys iš Lietuvos Banko duomenų bazės..."
                AddHandler BackgroundWorker1.DoWork, AddressOf GetCurrencyRateList
            ElseIf _BaseCurrency.Trim.ToUpper = "EUR" Then
                ProgressLabel.Text = "Gaunami duomenys iš Lietuvos Banko duomenų bazės..."
                AddHandler BackgroundWorker1.DoWork, AddressOf GetCurrencyRateListEUR
            Else
                Throw New NotImplementedException("Base currency " & _BaseCurrency.Trim.ToUpper _
                    & " is not implemented in AccWebCrawler.")
            End If

            ProgressBar1.Style = Windows.Forms.ProgressBarStyle.Marquee

            BackgroundWorker1.RunWorkerAsync(_CurrencyList)

        ElseIf _WebTarget = WebTarget.CourtInfo Then

            If _CourtRequest.ForCourtType = CourtType.LatCivil Then

                ProgressLabel.Text = "Gaunami duomenys iš Lietuvos Aukščiausiojo Teismo duomenų bazės..."
                ProgressBar1.Style = Windows.Forms.ProgressBarStyle.Continuous
                ProgressBar1.Maximum = 100
                ProgressBar1.Value = 0

                AddHandler BackgroundWorker1.DoWork, AddressOf DownloadLatCivilCases

                BackgroundWorker1.RunWorkerAsync(_CourtRequest)

            Else

                ProgressLabel.Text = "Gaunami duomenys iš Liteko duomenų bazės..."
                ProgressBar1.Style = Windows.Forms.ProgressBarStyle.Continuous
                ProgressBar1.Maximum = 100
                ProgressBar1.Value = 0

                AddHandler BackgroundWorker1.DoWork, AddressOf DownloadLiteko

                BackgroundWorker1.RunWorkerAsync(_CourtRequest)

            End If

        ElseIf _WebTarget = WebTarget.DownloadFile Then

            client = New System.Net.WebClient
            AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
            AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted

            ProgressLabel.Text = _Message
            ProgressBar1.Style = Windows.Forms.ProgressBarStyle.Continuous
            ProgressBar1.Maximum = 100
            ProgressBar1.Value = 0

            client.DownloadFileAsync(New Uri(_FileUrl.Trim), _FilePath.Trim)

        ElseIf _WebTarget = WebTarget.AvailableUpdateInfo Then

            client = New System.Net.WebClient
            AddHandler client.DownloadDataCompleted, AddressOf client_DownloadDataCompleted

            ProgressLabel.Text = _Message
            ProgressBar1.Style = Windows.Forms.ProgressBarStyle.Marquee

            client.DownloadDataAsync(New Uri(_FileUrl.Trim))

        Else

            Throw New NotImplementedException("Klaida. Formai F_LaunchWebCrawler " _
                & "nebuvo implementuotas tikslo parametras " & _WebTarget.ToString & ".")

        End If

    End Sub

    Private Sub ICancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ICancelButton.Click
        If Not client Is Nothing AndAlso client.IsBusy Then
            client.CancelAsync()
        ElseIf Not BackgroundWorker1 Is Nothing AndAlso BackgroundWorker1.IsBusy _
            AndAlso Not BackgroundWorker1.CancellationPending Then
            BackgroundWorker1.CancelAsync()
        ElseIf BackgroundWorker1 Is Nothing OrElse Not BackgroundWorker1.IsBusy Then
            If Not client Is Nothing Then client.Dispose()
            If Not BackgroundWorker1 Is Nothing Then BackgroundWorker1.Dispose()
            Me.Hide()
            Me.Close()
        End If
    End Sub



    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, _
        ByVal e As System.ComponentModel.ProgressChangedEventArgs) _
        Handles BackgroundWorker1.ProgressChanged

        ProgressLabel.Text = e.UserState.ToString

        If ProgressBar1.Style <> Windows.Forms.ProgressBarStyle.Marquee Then _
            ProgressBar1.Value = e.ProgressPercentage

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, _
        ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) _
        Handles BackgroundWorker1.RunWorkerCompleted

        Me.Hide()

        If Not e.Error Is Nothing Then
            MsgBox("Klaida gaunant duomenis: " & e.Error.Message, MsgBoxStyle.Exclamation, "Klaida")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        ElseIf e.Cancelled Then
            MsgBox("Duomenų gavimas buvo atšauktas.", MsgBoxStyle.Exclamation, "")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        ElseIf e.Result Is Nothing Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko gauti duomenų.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        ElseIf TypeOf e.Result Is PersonInfo AndAlso Not String.IsNullOrEmpty( _
            DirectCast(e.Result, PersonInfo).Message.Trim) Then
            MsgBox(DirectCast(e.Result, PersonInfo).Message, MsgBoxStyle.Information, "")
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf Not _CourtRequest Is Nothing AndAlso TypeOf e.Result Is Integer Then
            MsgBox("Duomenys sėkmingai gauti. Viso parsiųsta " & e.Result.ToString _
                & " nutarčių.", MsgBoxStyle.Information, "")
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            MsgBox("Duomenys sėkmingai gauti.", MsgBoxStyle.Information, "")
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If

        If e.Error Is Nothing AndAlso Not e.Cancelled Then _result = e.Result

        Me.Close()

    End Sub

    Private Sub client_ProgressChanged(ByVal sender As Object, _
        ByVal e As System.Net.DownloadProgressChangedEventArgs)

        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100
        ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())

    End Sub

    Private Sub client_DownloadCompleted(ByVal sender As Object, _
        ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

        Me.Hide()

        If e.Cancelled Then
            MsgBox("Duomenų gavimas buvo atšauktas.", MsgBoxStyle.Exclamation, "")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        ElseIf Not e.Error Is Nothing Then
            MsgBox("Klaida gaunant duomenis: " & e.Error.Message, MsgBoxStyle.Exclamation, "Klaida")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If

        Me.Close()

    End Sub

    Private Sub client_DownloadDataCompleted(ByVal sender As Object, _
        ByVal e As System.Net.DownloadDataCompletedEventArgs)

        Dim dateResult As Date = Date.MinValue

        Me.Hide()

        Try

            dateResult = ResolveAvailableUpdatesResult(_FileUrl, _FilePath, _
                _FileEncoding, _RemoteFileEncoding, _DateConversionFunction, e, True)

            If dateResult <> Date.MinValue Then _result = dateResult

            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception

            If ex.Message.ToLower.Contains("atšauktas") Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "")
            Else
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Klaida")
            End If

            Me.DialogResult = Windows.Forms.DialogResult.Cancel

        End Try

        Me.Close()

    End Sub

    Private Sub ProgressLabel_SizeChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles ProgressLabel.SizeChanged

        If ProgressLabel.Width > ProgressBar1.Width Then _
            Me.Width += ProgressLabel.Width - ProgressBar1.Width

    End Sub

End Class