Imports System.Net

Public Class PersonInfo

    Private _Code As String = ""
    Private _Name As String = ""
    Private _Address As String = ""
    Private _VatCode As String = ""
    Private _Message As String = ""


    Public ReadOnly Property Code() As String
        Get
            Return _Code.Trim
        End Get
    End Property

    Public Property Name() As String
        Get
            Return _Name.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _Name = value.Trim
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _Address = value.Trim
        End Set
    End Property

    Public Property VatCode() As String
        Get
            Return _VatCode.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _VatCode = value.Trim
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _Message.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _Message = value.Trim
        End Set
    End Property


    Public Sub New(ByVal nCode As String)
        If nCode Is Nothing Then nCode = ""
        _Code = nCode.Trim
    End Sub


    Public Shared Function GetPersonInfoRekvizitai(ByVal personCode As String) As PersonInfo

        If personCode Is Nothing OrElse String.IsNullOrEmpty(personCode.Trim) Then _
            Throw New ArgumentNullException("Klaida. Nenurodytas įmonės kodas.")

        Dim result As New PersonInfo(personCode)
        Dim xmlResponse As String = Nothing

        Try
            Using client As New WebClient
                Dim data As Byte() = client.DownloadData("http://www.rekvizitai.lt/api-xml/?apiKey=fa8cb6d2d671c4f71815b64e63525bff&clientId=1&method=companyDetails&code=" & personCode)
                xmlResponse = System.Text.Encoding.UTF8.GetString(data)
            End Using
        Catch ex As Exception
            result.Message = "Nepavyko prisijungti prie Rekvizitai.lt duomenų bazės: " & ex.Message
        End Try

        result = ParseResultForRekvizitai(xmlResponse, result)

        If String.IsNullOrEmpty(result.Name.Trim) AndAlso String.IsNullOrEmpty(result.VatCode.Trim) Then _
            Throw New Exception(result.Message)

        Return result

    End Function

End Class
