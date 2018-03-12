﻿Imports System.IO
Imports System.Xml
Imports ApskaitaObjects.My.Resources

Namespace HelperLists

    ''' <summary>
    ''' Represents a user report value object (information about an *.rdl file).
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class UserReportInfo
        Inherits ReadOnlyBase(Of UserReportInfo)
        Implements IValueObject

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _FileName As String = ""
        Private _Name As String = ""
        Private _Author As String = ""
        Private _FileAdded As DateTime = Now
        Private _FileLastUpdated As DateTime = Now
        Private _Params As UserReportParamInfoList = Nothing


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real user report).
        ''' </summary>
        ''' <remarks>a placeholder for a user report is not needed and does not exist</remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets a user report file (*.rdl) name.
        ''' </summary>
        ''' <remarks>File names are generated by the program, see
        ''' <see cref="ApskaitaObjects.Settings.CommandUploadUserReport">CommandUploadUserReport</see>
        ''' for details.</remarks>
        Public ReadOnly Property FileName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FileName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a user report file (*.rdl) description (as specified in the file).
        ''' </summary>
        ''' <remarks>Corresponds to a Description element of the RDL file.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a user report file (*.rdl) author (as specified in the file).
        ''' </summary>
        ''' <remarks>Corresponds to a Author element of the RDL file.</remarks>
        Public ReadOnly Property Author() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Author.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a date and time when the user report file (*.rdl) was added.
        ''' </summary>
        ''' <remarks>File system information.</remarks>
        Public ReadOnly Property FileAdded() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FileAdded
            End Get
        End Property

        ''' <summary>
        ''' Gets a date and time when the user report file (*.rdl) was last updated.
        ''' </summary>
        ''' <remarks>File system information.</remarks>
        Public ReadOnly Property FileLastUpdated() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FileLastUpdated
            End Get
        End Property

        ''' <summary>
        ''' Gets a collection of report parameters (as specified in the file).
        ''' </summary>
        ''' <remarks>Corresponds to a ReportParameters element of the RDL file.</remarks>
        Public ReadOnly Property Params() As UserReportParamInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Params
            End Get
        End Property


        Public Shared Operator =(ByVal a As UserReportInfo, ByVal b As UserReportInfo) As Boolean

            Dim aCount, bCount As Integer
            Dim aName, bName, aAuthor, bAuthor As String
            If a Is Nothing OrElse a.IsEmpty Then
                aName = ""
                aAuthor = ""
                aCount = 0
            Else
                aName = a.Name.Trim.ToLower
                aAuthor = a.Author.Trim.ToLower
                aCount = a.Params.Count
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bName = ""
                bAuthor = ""
                bCount = 0
            Else
                bName = b.Name.Trim.ToLower
                bAuthor = b.Author.Trim.ToLower
                bCount = b.Params.Count
            End If

            Return aName = bName AndAlso aAuthor = bAuthor AndAlso aCount = bCount

        End Operator

        Public Shared Operator <>(ByVal a As UserReportInfo, ByVal b As UserReportInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As UserReportInfo, ByVal b As UserReportInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As UserReportInfo, ByVal b As UserReportInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As UserReportInfo = TryCast(obj, UserReportInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(HelperLists_UserReportInfo_ToString, _Name, _Author)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetUserReportInfo(ByVal info As FileInfo) As UserReportInfo
            Return New UserReportInfo(info)
        End Function

        Friend Shared Function GetUserReportInfo(ByVal content As String) As UserReportInfo
            Return New UserReportInfo(content)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal info As FileInfo)
            Fetch(info)
        End Sub

        Private Sub New(ByVal content As String)
            Fetch(content)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal info As FileInfo)

            _FileName = info.Name
            _FileAdded = DateTime.SpecifyKind(info.CreationTimeUtc, _
                DateTimeKind.Utc).ToLocalTime
            _FileLastUpdated = DateTime.SpecifyKind(info.LastWriteTimeUtc, _
                DateTimeKind.Utc).ToLocalTime

            Fetch(File.ReadAllText(info.FullName))

        End Sub

        Private Sub Fetch(ByVal content As String)

            Dim data As New XmlDocument()

            data.LoadXml(content)

            _Name = GetReportDescription(data)
            _Author = GetReportAuthor(data)
            _Params = GetReportParams(data)

        End Sub

        Private Shared Function GetReportDescription(ByVal data As Xml.XmlDocument) As String

            Dim node As Xml.XmlNodeList = data.GetElementsByTagName("Description")

            If node.Count < 1 Then
                Return String.Format(HelperLists_UserReportInfo_FailedToParseDesciption)
            Else
                Return node(0).InnerText
            End If

        End Function

        Private Shared Function GetReportAuthor(ByVal data As Xml.XmlDocument) As String

            Dim node As Xml.XmlNodeList = data.GetElementsByTagName("Author")

            If node.Count < 1 OrElse StringIsNullOrEmpty(node(0).InnerText) Then
                Return HelperLists_UserReportInfo_NullAuthorString
            Else
                Return node(0).InnerText
            End If

        End Function

        Private Shared Function GetReportParams(ByVal data As XmlDocument) As UserReportParamInfoList

            Dim paramNode As XmlNode = Nothing
            Dim paramNodeList As XmlNodeList = data.GetElementsByTagName("ReportParameters")

            If paramNodeList.Count > 0 Then
                paramNode = paramNodeList(0)
            End If

            Return UserReportParamInfoList.GetUserReportParamInfoList(paramNode)

        End Function

#End Region

    End Class

End Namespace