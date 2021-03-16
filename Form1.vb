Option Explicit On
Option Strict On

Public Class Form1

#Region "Global declarations"
    ' Global variable used to avoid event handling when programmatically changing control properties (like adding items to combo boxes)
    Dim process_events As Boolean = False

    ' comport declarations
    Dim available_comdevices As New List(Of com_device_info)
    Dim current_device_info As New com_device_info
    Dim current_comport As IO.Ports.SerialPort = New IO.Ports.SerialPort

    ' File related declarations
    Dim filename As String = ""
    Dim current_directory As String = ""

    ' Email notification declarations
    Dim smtp_server As String = ""
    Dim smtp_port As Integer = 0
    Dim sender_email As String = ""
    Dim password As String = ""
    Dim receiver_email As String = ""

    ' Arduino I/O declarations
    Dim analogread_notification_sent As Boolean = False
    Dim pushbutton_notification_sent As Boolean = False
    Dim notification_state As NotificationEnum = NotificationEnum.NoNotification
    Dim timer_interval As Integer = 1000    ' Timer interval in msec
    Dim output_interval As Integer = 1        ' Arduino output interval in sec

    Enum ComportEnum
        LeavetheSame
        LeaveOpenRegardless
        LeaveClosedRegardless
    End Enum
    Enum OutputEnum
        OutputResponse
        DontOutput
    End Enum
    Enum NotificationEnum
        NoNotification
        NotifyOnPushbutton
        NotifyOnThreshold
    End Enum

#End Region

#Region "Form Load and Close"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim error_message As String = ""

        Me.Text = String.Format(My.Application.Info.ProductName & "   (Version {0})", My.Application.Info.Version.ToString)

        ' Read in the icon and .ini file contents
        Try
            load_icon_and_ini_file(error_message)
        Catch ex As Exception
            output_text("Problem with INI file")
        End Try

        process_events = False      ' This is needed in order to prevent event handler code from executing
        chk_analogread.Checked = False
        chk_analogread.Enabled = False
        chk_flicker.Checked = False
        chk_flicker.Enabled = False
        txt_command.Enabled = False
        btn_send_command.Enabled = False
        rdo_no_notification.Checked = True

        Try
            Dim last_cbo_added As Integer = 0
            cbo_comport.Enabled = False
            cbo_comport.Items.Clear()
            cbo_comport.Items.Add("Select Com Port")       ' This item will always be the first entry (at SelectedIndex = 0)
            For Each sp As String In My.Computer.Ports.SerialPortNames
                Dim PortName As String = sp.Trim
                Dim already_added As Boolean = False             ' I have seen the same com port come up twice in  My.Computer.Ports.SerialPortNames
                For Each TestComPort As com_device_info In available_comdevices
                    If TestComPort.ComPort = PortName Then
                        already_added = True
                    End If
                Next
                If Not already_added Then
                    Try
                        current_comport = My.Computer.Ports.OpenSerialPort(PortName)
                        current_comport.BaudRate = 115200
                        current_comport.DataBits = 8
                        current_comport.StopBits = IO.Ports.StopBits.One
                        current_comport.Parity = IO.Ports.Parity.None
                        current_comport.ParityReplace = CByte(45)
                        current_comport.Handshake = IO.Ports.Handshake.None
                        current_comport.NewLine = vbCr
                        current_comport.WriteTimeout = 500
                        current_comport.ReadTimeout = 500

                        ' Write a command to each available com port to see if an Arduino is there
                        current_comport.DiscardInBuffer()
                        current_comport.DiscardOutBuffer()
                        current_comport.WriteLine("f")
                        System.Threading.Thread.Sleep(100)

                        current_comport.DiscardInBuffer()
                        current_comport.DiscardOutBuffer()
                        current_comport.WriteLine("i")
                        System.Threading.Thread.Sleep(100)
                        Dim temp_info As String
                        temp_info = current_comport.ReadLine

                        Dim temp_string() = Split(temp_info, ",")
                        If temp_string.GetUpperBound(0) = 3 Then
                            last_cbo_added = last_cbo_added + 1
                            Dim Manufacturer As String = temp_string(0).Trim
                            Dim Model As String = temp_string(1).Trim
                            Dim SerNum As String = temp_string(2).Trim
                            Dim Firmware As String = temp_string(3).Trim
                            current_device_info = New com_device_info(PortName, Manufacturer, Model, SerNum, Firmware)
                            available_comdevices.Add(current_device_info)
                            ' So, cboComPort.SelectedIndex = 0 will always = "Select Com Port".
                            ' Therefore, you access the selected ComPort by:  AvailableComDevices.Item(cboComPort.SelectedIndex-1)
                            ' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/concepts/collections
                            cbo_comport.Items.Add(current_device_info.ToComboItem)
                            ' Get the number of power-ups for the Arduino Uno board
                            System.Threading.Thread.Sleep(100)
                            current_comport.DiscardInBuffer()
                            current_comport.DiscardOutBuffer()
                            current_comport.WriteLine("p?")
                            temp_info = current_comport.ReadLine.Trim
                            output_text(current_device_info.ToString & "  (PowerCycles=" & temp_info & ")")
                            'cbo_comport.SelectedIndex = last_cbo_added     ' This would auto select the last entered device
                            cbo_comport.SelectedIndex = 0                   ' This forces the user to select a device
                            cbo_comport.Enabled = True
                        End If

                        current_comport.Close()

                    Catch ex As Exception
                        current_comport.Close()
                    End Try
                End If
            Next
            If cbo_comport.Items.Count = 1 Then
                cbo_comport.Items.Clear()
                cbo_comport.Items.Add("No Arduino found")
                cbo_comport.SelectedIndex = 0
                cbo_comport.Enabled = False
            End If
        Catch ex As Exception
            cbo_comport.Items.Clear()
            cbo_comport.Items.Add("Problem finding Arduino")
            cbo_comport.SelectedIndex = 0
            cbo_comport.Enabled = False
            output_text(ex.ToString)
        End Try

        lbl_power_status.Text = power_status()

        process_events = True           ' Enable event handler code to be executed now

    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        analogread_timer.Stop()
        reset_arduino()

        ' Constant              Value	Description
        ' vbOKOnly              0	    Display OK button only.
        ' vbOKCancel            1	    Display OK And Cancel buttons.
        ' vbAbortRetryIgnore    2	    Display Abort, Retry, And Ignore buttons.
        ' vbYesNoCancel         3	    Display Yes, No, And Cancel buttons.
        ' vbYesNo               4	    Display Yes And No buttons.
        ' vbRetryCancel         5	    Display Retry And Cancel buttons.
        ' vbCritical            16	    Display Critical Message icon.
        ' vbQuestion            32	    Display Warning Query icon.
        ' vbExclamation         48	    Display Warning Message icon.
        Dim Style As MsgBoxStyle = MsgBoxStyle.OkCancel
        ' Constant  Value	Description
        ' vbOK      1	    OK
        ' vbCancel  2	    Cancel
        ' vbAbort   3	    Abort
        ' vbRetry   4	    Retry
        ' vbIgnore  5	    Ignore
        ' vbYes     6	    Yes
        ' vbNo      7	    No
        Dim Response As MsgBoxResult
        Dim Msg As String = "Are you sure you want to quit?"    ' Define message.
        Dim Title As String = "Quit the program"                ' Define title.

        ' Display message.
        Response = MsgBox(Msg, Style, Title)
        If Response = vbOK Then    ' User chose Yes.
            e.Cancel = False
        Else    ' User chose No.
            e.Cancel = True
        End If

    End Sub

    Public Sub load_icon_and_ini_file(ByRef ErrorMsg As String)

        ErrorMsg = ""
        Dim icon_file As String = ""
        Dim ini_file As String = ""

        current_directory = My.Computer.FileSystem.CurrentDirectory.Trim
        output_text("Current Directory=" & current_directory)

        Dim path As String = My.Application.Info.DirectoryPath
        output_text("Current path=" & path)

        ' Load the default icon
        Try
            If My.Computer.FileSystem.FileExists("..\..\starter.ico") Then
                icon_file = "..\..\starter.ico"
            ElseIf My.Computer.FileSystem.FileExists(current_directory & "\starter.ico") Then
                icon_file = current_directory & "\starter.ico"
            End If
            Me.Icon = New Icon(icon_file)
            output_text("icon_file=" & icon_file)
        Catch ex As Exception
            output_text("There was a problem setting icon: " & ex.ToString)
        End Try

        ' load the default .ini file
        Try
            If My.Computer.FileSystem.FileExists("..\..\starter.ini") Then
                ini_file = "..\..\starter.ini"
            ElseIf My.Computer.FileSystem.FileExists(current_directory & "\starter.ini") Then
                ini_file = current_directory & "\starter.ini"
            End If
        Catch ex As Exception
            output_text("There was a problem setting ini file location: " & ex.ToString)
        End Try
        output_text("ini_file=" & ini_file)

        If ini_file <> "" Then
            Dim InFile As System.IO.StreamReader
            Try
                InFile = New System.IO.StreamReader(ini_file)
                Dim TextLine As String
                Do
                    TextLine = InFile.ReadLine
                    If Not TextLine Is Nothing Then ' Avoid exception at end of file
                        If TextLine.Trim <> Nothing Then ' Skip over lines with spaces ony
                            Dim TestLine() As String = Split(TextLine, "=")
                            If TestLine.GetUpperBound(0) = 1 Then
                                Select Case TestLine(0).ToLower.Trim
                                    Case "smtpserver"
                                        smtp_server = TestLine(1).Trim
                                    Case "smtpport"
                                        smtp_port = CInt(Val(TestLine(1)))
                                    Case "senderemail"
                                        sender_email = TestLine(1).Trim
                                    Case "password"
                                        password = TestLine(1).Trim
                                    Case "notifyemail"
                                        receiver_email = TestLine(1).Trim
                                End Select
                            End If
                        End If
                    End If
                Loop Until TextLine Is Nothing
            Catch ex As Exception
                ErrorMsg = "exception in ReadString is " & ex.ToString
            Finally
                InFile.Close()
            End Try
        End If

    End Sub

#End Region

#Region "Handle control events"
    Private Sub cbo_comport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_comport.SelectedIndexChanged

        If process_events Then
            If cbo_comport.SelectedIndex = 0 Then
                analogread_timer.Stop()
                reset_arduino()
                txt_command.Enabled = False
                btn_send_command.Enabled = False
                chk_flicker.Enabled = False
                chk_analogread.Enabled = False
            Else
                txt_command.Enabled = True
                btn_send_command.Enabled = True
                chk_analogread.Enabled = True
                chk_flicker.Enabled = True
            End If
        End If

    End Sub

    Private Sub btn_send_command_Click(sender As Object, e As EventArgs) Handles btn_send_command.Click

        Dim response As String
        response = send_command(txt_command.Text.Trim, ComportEnum.LeavetheSame)
        output_text("command=" & txt_command.Text.Trim & ",  response=" & response.Trim)

    End Sub

    Private Sub BrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrowserToolStripMenuItem.Click

        Dim web_address As String = "https://nccde.org/1389/Route-9-Library-Innovation-Center"
        Process.Start(web_address)

    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click

        Dim error_message As String = ""

        get_filename(error_message)
        If error_message = "" Then
            output_text("Filename = " & filename)
        Else
            output_text("Problems selecting filename: " & error_message)
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        Me.Close()

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        AboutBox1.Show()

    End Sub

    Private Sub chkFlickerMode_CheckedChanged(sender As Object, e As EventArgs) Handles chk_flicker.CheckedChanged

        Dim response As String = ""
        If process_events Then
            If chk_flicker.Checked Then
                response = send_command("mf")
            Else
                response = send_command("mo")
            End If
        End If

    End Sub

    Private Sub chk_analogread_CheckedChanged(sender As Object, e As EventArgs) Handles chk_analogread.CheckedChanged

        Dim response As String = ""
        If process_events Then
            If chk_analogread.Checked Then
                response = send_command("o?", ComportEnum.LeaveOpenRegardless)
                Dim output_interval As Integer = CInt(Val(response))
                If output_interval < 10 Then
                    timer_interval = output_interval * 800
                Else
                    timer_interval = 1000 * (output_interval - 2)
                End If
                output_text("output_interval=" & output_interval.ToString & ", timer_interval=" & timer_interval.ToString)
                analogread_timer.Interval = timer_interval
                ' Set the output format to default
                response = send_command("rf0", ComportEnum.LeaveOpenRegardless)
                '  We assume at this point the comport is open and ready for business
                current_comport.DiscardInBuffer()
                current_comport.DiscardOutBuffer()
                current_comport.WriteLine("rb")  ' Nothing is directly returned with this command

                analogread_timer.Start()
            Else
                analogread_timer.Stop()
                If current_comport.IsOpen Then
                    response = send_command("r", ComportEnum.LeaveClosedRegardless)
                End If
            End If
        End If

    End Sub

    Private Sub rdo_no_notification_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_no_notification.CheckedChanged

        If rdo_no_notification.Checked Then
            notification_state = NotificationEnum.NoNotification
            output_text("No notification selected")
        End If

    End Sub

    Private Sub rdo_pushbutton_notification_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_pushbutton_notification.CheckedChanged

        If rdo_pushbutton_notification.Checked Then
            notification_state = NotificationEnum.NotifyOnPushbutton
            output_text("Notification on pushbutton state on Arduino")
        End If

    End Sub

    Private Sub rdo_threshold_notification_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_threshold_notification.CheckedChanged

        If rdo_threshold_notification.Checked Then
            notification_state = NotificationEnum.NotifyOnThreshold
            output_text("Notification on Analog read > 900 (reset < 100) value on Arduino")
        End If

    End Sub

#End Region

#Region "Main routines"
    Private Sub output_text(ByVal Text As String)

        If txt_output.TextLength > 5000 Then txt_output.Text = ""
        txt_output.Text = txt_output.Text & Text & vbCrLf
        txt_output.SelectionStart = txt_output.TextLength
        txt_output.ScrollToCaret()

    End Sub

    Public Sub get_filename(ByRef ErrorMsg As String)

        ErrorMsg = ""

        If filename = "" Then
            filename = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & ".txt"
        End If

        ' Get the filename
        Try
            Dim SaveFileDialog As New System.Windows.Forms.SaveFileDialog
            With SaveFileDialog
                Dim finfo As New System.IO.FileInfo(filename)
                Dim ret As DialogResult
                .AddExtension = True
                .DefaultExt = ".txt"
                .FileName = finfo.Name
                .Filter = "Comma Separted Value (*.csv)|*.csv|" &
                          "Text Files (*.txt)|*.txt|" &
                          "All files (*.*)|*.*"
                .InitialDirectory = finfo.DirectoryName
                ret = .ShowDialog()
                If ret <> System.Windows.Forms.DialogResult.OK Then
                    ErrorMsg = "User Canceled"
                Else
                    filename = .FileName
                End If
            End With
        Catch ex As Exception
            ErrorMsg = "Problems setting up the data file" & vbCrLf & ex.ToString
            Exit Sub
        End Try

    End Sub

    Private Sub writefile_text(ByVal output_string As String, ByVal append_file As Boolean)

        If output_string.Trim <> "" And filename <> "" Then
            Try
                Dim output_file As System.IO.StreamWriter
                output_file = New System.IO.StreamWriter(filename, append_file)
                output_file.WriteLine(output_string)
                output_file.Close()
            Catch ex As Exception
                output_text("Problems writing to file" & vbCrLf & ex.ToString)
            End Try
        End If

    End Sub

    Private Function send_command(ByVal Command As String, Optional ByVal CPstate As ComportEnum = ComportEnum.LeavetheSame) As String

        Dim Response As String = ""
        Dim LeaveComportOpen As Boolean = False         ' Default is to close com port

        ' Open the commport
        If current_comport.IsOpen Then                   ' You have to open comport before you can write to it.
            LeaveComportOpen = True
        Else
            If cbo_comport.SelectedIndex >= 0 Then       ' Skip this if no comport was open
                Try
                    current_comport = My.Computer.Ports.OpenSerialPort(available_comdevices.Item(cbo_comport.SelectedIndex - 1).ComPort)
                    current_comport.BaudRate = 115200
                    current_comport.DataBits = 8
                    current_comport.StopBits = IO.Ports.StopBits.One
                    current_comport.Parity = IO.Ports.Parity.None
                    current_comport.ParityReplace = CByte(45)
                    current_comport.Handshake = IO.Ports.Handshake.None
                    current_comport.NewLine = vbCr
                    current_comport.WriteTimeout = 500
                    current_comport.ReadTimeout = 5000

                    current_comport.DiscardInBuffer()        ' This fixes a problem when you first open a port to the Arduino
                    current_comport.DiscardOutBuffer()
                    current_comport.WriteLine("f")
                    System.Threading.Thread.Sleep(100)
                Catch ex As Exception
                    Try
                        current_comport.Close()
                    Catch ex1 As Exception
                        ' Do nothing
                    End Try
                    Return "Error trying to open comport:  " & ex.ToString
                    Exit Function
                End Try
            Else
                Return "You need to select a com port to open"
                Exit Function
            End If
        End If
        If CPstate = ComportEnum.LeaveOpenRegardless Then LeaveComportOpen = True    ' Override to stay open
        If CPstate = ComportEnum.LeaveClosedRegardless Then LeaveComportOpen = False   ' Override to stay closed

        Try
            current_comport.DiscardInBuffer()
            current_comport.DiscardOutBuffer()
            current_comport.WriteLine(Command.Trim)
            Response = current_comport.ReadLine.Trim
        Catch ex As Exception
            Try
                current_comport.Close()
            Catch ex1 As Exception
                ' Do nothing
            End Try
            Return "Error sending commands to comport:  " & ex.ToString
            Exit Function
        End Try

        If Not LeaveComportOpen Then
            Try
                current_comport.Close()
            Catch ex As Exception
                Return "Error trying to close comport:  " & ex.ToString
                Exit Function
            End Try
        End If

        Return Response

    End Function

    Public Function power_status() As String

        Dim Message As String
        Dim psBattery As PowerStatus = SystemInformation.PowerStatus
        If psBattery.PowerLineStatus = PowerLineStatus.Online Then
            Message = "Plugged in (" & (psBattery.BatteryLifePercent * 100.0).ToString("##0") & "% charged)"
        Else
            Message = "On battery power (" & (psBattery.BatteryLifePercent * 100.0).ToString("##0") & "%) (" & (psBattery.BatteryLifeRemaining / 60.0).ToString("###0") & " min left)"
        End If
        Return Message

    End Function

    Private Sub reset_arduino()

        Dim response As String = ""
        If current_comport.IsOpen Then
            process_events = False
            response = send_command("r", ComportEnum.LeaveOpenRegardless)        ' Stop any streaming data
            chk_analogread.Checked = False
            response = send_command("mo", ComportEnum.LeaveOpenRegardless)       ' Turn off flicker mode
            chk_flicker.Checked = False
            response = send_command("lo", ComportEnum.LeaveClosedRegardless)     ' TUrn off on-board LED
            process_events = True
        End If

    End Sub

    Private Sub analogread_timer_Tick(sender As Object, e As EventArgs) Handles analogread_timer.Tick

        Dim response As String = ""

        analogread_timer.Stop()
        ' We assume the comport is open and has sent something
        If current_comport.IsOpen Then
            response = current_comport.ReadLine.Trim
            Dim data() As String = Split(response, ",")
            If data.GetUpperBound(0) <> 1 Then
                reset_arduino()
                Exit Sub
            End If
            Dim pushbutton_state As Integer = CInt(Val(data(0)))
            Dim analog_value As Integer = CInt(Val(data(1)))
            output_text("analog_value=" & analog_value.ToString("###0") & ", pushbutton state=" & pushbutton_state.ToString("0"))

            writefile_text(DateTime.Now.ToString & ", " & current_device_info.SerialNumber & ", " & response, True)

            ' Check If pushbutton state Is 1, Or pressed (reset when not pressed)
            If (notification_state = NotificationEnum.NotifyOnPushbutton And pushbutton_state = 1 And pushbutton_notification_sent = False) Then
                Dim subject As String = "Pushbutton pressed on " & current_device_info.SerialNumber
                Dim message As String = "This is the text of the message"
                Dim error_message As String = ""
                send_email_notification(subject, message, error_message)
                If error_message <> "" Then
                    output_text(error_message)
                End If
                output_text(subject)
                pushbutton_notification_sent = True
            End If
            If (notification_state = NotificationEnum.NotifyOnPushbutton And pushbutton_state = 0 And pushbutton_notification_sent = True) Then
                pushbutton_notification_sent = False
                output_text("Button not pushed")
            End If
            ' Check If analog value is greater than 900 (reset when less than 100)
            If (notification_state = NotificationEnum.NotifyOnThreshold And analog_value > 900 And analogread_notification_sent = False) Then
                Dim subject As String = "analog_value > 900 " & current_device_info.SerialNumber
                Dim message As String = "This is the text of the message"
                Dim error_message As String = ""
                send_email_notification(subject, message, error_message)
                If error_message <> "" Then
                    output_text(error_message)
                End If
                output_text(subject)
                analogread_notification_sent = True
            End If
            If (notification_state = NotificationEnum.NotifyOnThreshold And analog_value < 100 And analogread_notification_sent = True) Then
                analogread_notification_sent = False
                output_text("analog_value < 100")
            End If
        Else
            output_text("Com port not open")
        End If
        analogread_timer.Start()

    End Sub

    Private Sub send_email_notification(ByVal subject As String, ByVal target_message As String, ByRef error_message As String)

        error_message = ""

        Dim Mail As New Net.Mail.MailMessage
        Try

            Mail.From = New Net.Mail.MailAddress(sender_email)
            Mail.To.Add(New Net.Mail.MailAddress(receiver_email))

            Mail.Subject = subject
            Mail.Body = target_message

            Dim client As New Net.Mail.SmtpClient
            client.UseDefaultCredentials = False
            client.EnableSsl = True

            client.Host = smtp_server
            client.Port = smtp_port

            client.Credentials = New System.Net.NetworkCredential(sender_email, password)

            client.Send(Mail)

            Mail.Dispose()

        Catch ex As Exception
            Mail.Dispose()
            error_message = "Error sending email: " & Now.ToString & vbCrLf & ex.ToString & vbCrLf
        End Try
    End Sub

#End Region

End Class
