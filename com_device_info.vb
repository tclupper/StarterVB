Option Explicit On
Option Strict On

Public Class com_device_info
    Private mComPort As String
    Private mManufacturer As String
    Private mModel As String
    Private mSerialNumber As String
    Private mFirmware As String

#Region "Constructor procedures"
    Public Sub New()
        mComPort = ""
        mManufacturer = ""
        mModel = ""
        mSerialNumber = ""
        mFirmware = ""
    End Sub

    Public Sub New(ComPort As String, Manufacturer As String, Model As String, SerialNumber As String, Firmware As String)
        '  Add some code to validate with ComPort is between COM1 to COM99
        mComPort = ComPort.Trim
        mManufacturer = Manufacturer.Trim
        mModel = Model.Trim
        mSerialNumber = SerialNumber.Trim
        mFirmware = Firmware.Trim
    End Sub

#End Region

#Region "Property procedures"
    Public Property ComPort As String
        Get
            Return mComPort
        End Get
        Set(value As String)
            mComPort = value
        End Set
    End Property

    Public Property Manufacturer As String
        Get
            Return mManufacturer
        End Get
        Set(value As String)
            mManufacturer = value
        End Set
    End Property

    Public Property Model As String
        Get
            Return mModel
        End Get
        Set(value As String)
            mModel = value
        End Set
    End Property

    Public Property SerialNumber As String
        Get
            Return mSerialNumber
        End Get
        Set(value As String)
            mSerialNumber = value
        End Set
    End Property

    Public Property Firmware As String
        Get
            Return mFirmware
        End Get
        Set(value As String)
            mFirmware = value
        End Set
    End Property

#End Region

#Region "Method procedures"
    Public Overrides Function ToString() As String
        Return "ComPort=" & mComPort & ", Manufacturer=" & mManufacturer & ", Model=" & mModel & ", SerialNumber=" & mSerialNumber & ", Firmware=" & mFirmware
    End Function

    Public Function ToComboItem() As String
        Return mComPort & "  (" & mModel & "," & mSerialNumber & ")"
    End Function

#End Region

End Class
