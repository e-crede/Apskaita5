﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.5485
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UseThreadingForDataTransfer() As Boolean
            Get
                Return CType(Me("UseThreadingForDataTransfer"),Boolean)
            End Get
            Set
                Me("UseThreadingForDataTransfer") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AutoReloadData() As Boolean
            Get
                Return CType(Me("AutoReloadData"),Boolean)
            End Get
            Set
                Me("AutoReloadData") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property UserName() As String
            Get
                Return CType(Me("UserName"),String)
            End Get
            Set
                Me("UserName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property UserEmailAccount() As String
            Get
                Return CType(Me("UserEmailAccount"),String)
            End Get
            Set
                Me("UserEmailAccount") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SmtpServer() As String
            Get
                Return CType(Me("SmtpServer"),String)
            End Get
            Set
                Me("SmtpServer") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property UserEmail() As String
            Get
                Return CType(Me("UserEmail"),String)
            End Get
            Set
                Me("UserEmail") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property UserEmailPassword() As String
            Get
                Return CType(Me("UserEmailPassword"),String)
            End Get
            Set
                Me("UserEmailPassword") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UseSslForEmail() As Boolean
            Get
                Return CType(Me("UseSslForEmail"),Boolean)
            End Get
            Set
                Me("UseSslForEmail") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UseAuthForEmail() As Boolean
            Get
                Return CType(Me("UseAuthForEmail"),Boolean)
            End Get
            Set
                Me("UseAuthForEmail") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SmtpPort() As String
            Get
                Return CType(Me("SmtpPort"),String)
            End Get
            Set
                Me("SmtpPort") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShowToolStrip() As Boolean
            Get
                Return CType(Me("ShowToolStrip"),Boolean)
            End Get
            Set
                Me("ShowToolStrip") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AutoUpdate() As Boolean
            Get
                Return CType(Me("AutoUpdate"),Boolean)
            End Get
            Set
                Me("AutoUpdate") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property EmailMessageText() As String
            Get
                Return CType(Me("EmailMessageText"),String)
            End Get
            Set
                Me("EmailMessageText") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property CacheTimeout() As UInteger
            Get
                Return CType(Me("CacheTimeout"),UInteger)
            End Get
            Set
                Me("CacheTimeout") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SignInvoicesWithCompanySignature() As Boolean
            Get
                Return CType(Me("SignInvoicesWithCompanySignature"),Boolean)
            End Get
            Set
                Me("SignInvoicesWithCompanySignature") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SignInvoicesWithLocalUserSignature() As Boolean
            Get
                Return CType(Me("SignInvoicesWithLocalUserSignature"),Boolean)
            End Get
            Set
                Me("SignInvoicesWithLocalUserSignature") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SignInvoicesWithRemoteUserSignature() As Boolean
            Get
                Return CType(Me("SignInvoicesWithRemoteUserSignature"),Boolean)
            End Get
            Set
                Me("SignInvoicesWithRemoteUserSignature") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AllwaysLoginAsLocalUser() As Boolean
            Get
                Return CType(Me("AllwaysLoginAsLocalUser"),Boolean)
            End Get
            Set
                Me("AllwaysLoginAsLocalUser") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShowDefaultMailClientWindow() As Boolean
            Get
                Return CType(Me("ShowDefaultMailClientWindow"),Boolean)
            End Get
            Set
                Me("ShowDefaultMailClientWindow") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UseDefaultEmailClient() As Boolean
            Get
                Return CType(Me("UseDefaultEmailClient"),Boolean)
            End Get
            Set
                Me("UseDefaultEmailClient") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property AutoSizeForm() As Boolean
            Get
                Return CType(Me("AutoSizeForm"),Boolean)
            End Get
            Set
                Me("AutoSizeForm") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property AutoSizeDataGridViewColumns() As Boolean
            Get
                Return CType(Me("AutoSizeDataGridViewColumns"),Boolean)
            End Get
            Set
                Me("AutoSizeDataGridViewColumns") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property FormPropertiesList() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("FormPropertiesList"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("FormPropertiesList") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AllwaysShowWageSettings() As Boolean
            Get
                Return CType(Me("AllwaysShowWageSettings"),Boolean)
            End Get
            Set
                Me("AllwaysShowWageSettings") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property SQLQueryTimeOut() As Integer
            Get
                Return CType(Me("SQLQueryTimeOut"),Integer)
            End Get
            Set
                Me("SQLQueryTimeOut") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property LocalUsers() As String
            Get
                Return CType(Me("LocalUsers"),String)
            End Get
            Set
                Me("LocalUsers") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property UserSignature() As String
            Get
                Return CType(Me("UserSignature"),String)
            End Get
            Set
                Me("UserSignature") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property CommonSettings() As String
            Get
                Return CType(Me("CommonSettings"),String)
            End Get
            Set
                Me("CommonSettings") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property BankDocumentPrefix() As String
            Get
                Return CType(Me("BankDocumentPrefix"),String)
            End Get
            Set
                Me("BankDocumentPrefix") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property IgnoreWrongIBAN() As Boolean
            Get
                Return CType(Me("IgnoreWrongIBAN"),Boolean)
            End Get
            Set
                Me("IgnoreWrongIBAN") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://79.142.116.110:8088/files/")>  _
        Public Property UpdateUrl() As String
            Get
                Return CType(Me("UpdateUrl"),String)
            End Get
            Set
                Me("UpdateUrl") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AlwaysUseExternalIdInvoicesMade() As Boolean
            Get
                Return CType(Me("AlwaysUseExternalIdInvoicesMade"),Boolean)
            End Get
            Set
                Me("AlwaysUseExternalIdInvoicesMade") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AlwaysUseExternalIdInvoicesReceived() As Boolean
            Get
                Return CType(Me("AlwaysUseExternalIdInvoicesReceived"),Boolean)
            End Get
            Set
                Me("AlwaysUseExternalIdInvoicesReceived") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property DefaultInvoiceReceivedItemIsGoods() As Boolean
            Get
                Return CType(Me("DefaultInvoiceReceivedItemIsGoods"),Boolean)
            End Get
            Set
                Me("DefaultInvoiceReceivedItemIsGoods") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property DefaultInvoiceMadeItemIsGoods() As Boolean
            Get
                Return CType(Me("DefaultInvoiceMadeItemIsGoods"),Boolean)
            End Get
            Set
                Me("DefaultInvoiceMadeItemIsGoods") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property CheckInvoiceReceivedNumber() As Boolean
            Get
                Return CType(Me("CheckInvoiceReceivedNumber"),Boolean)
            End Get
            Set
                Me("CheckInvoiceReceivedNumber") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property CheckInvoiceReceivedNumberWithDate() As Boolean
            Get
                Return CType(Me("CheckInvoiceReceivedNumberWithDate"),Boolean)
            End Get
            Set
                Me("CheckInvoiceReceivedNumberWithDate") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property CheckInvoiceReceivedNumberWithSupplier() As Boolean
            Get
                Return CType(Me("CheckInvoiceReceivedNumberWithSupplier"),Boolean)
            End Get
            Set
                Me("CheckInvoiceReceivedNumberWithSupplier") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property ObjectListViewColumnPropertiesList() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("ObjectListViewColumnPropertiesList"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("ObjectListViewColumnPropertiesList") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EditListViewWithDoubleClick() As Boolean
            Get
                Return CType(Me("EditListViewWithDoubleClick"),Boolean)
            End Get
            Set
                Me("EditListViewWithDoubleClick") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UseHotTracking() As Boolean
            Get
                Return CType(Me("UseHotTracking"),Boolean)
            End Get
            Set
                Me("UseHotTracking") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShowEmptyListMessage() As Boolean
            Get
                Return CType(Me("ShowEmptyListMessage"),Boolean)
            End Get
            Set
                Me("ShowEmptyListMessage") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ShowGridLines() As Boolean
            Get
                Return CType(Me("ShowGridLines"),Boolean)
            End Get
            Set
                Me("ShowGridLines") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property DefaultActionByDoubleClick() As Boolean
            Get
                Return CType(Me("DefaultActionByDoubleClick"),Boolean)
            End Get
            Set
                Me("DefaultActionByDoubleClick") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.ApskaitaWUI.My.MySettings
            Get
                Return Global.ApskaitaWUI.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
