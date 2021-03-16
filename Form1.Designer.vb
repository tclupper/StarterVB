<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.my_menu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BrowserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.analogread_timer = New System.Windows.Forms.Timer(Me.components)
        Me.cbo_comport = New System.Windows.Forms.ComboBox()
        Me.lbl_command = New System.Windows.Forms.Label()
        Me.txt_command = New System.Windows.Forms.TextBox()
        Me.btn_send_command = New System.Windows.Forms.Button()
        Me.lbl_power_status = New System.Windows.Forms.Label()
        Me.chk_analogread = New System.Windows.Forms.CheckBox()
        Me.chk_flicker = New System.Windows.Forms.CheckBox()
        Me.rdo_no_notification = New System.Windows.Forms.RadioButton()
        Me.rdo_pushbutton_notification = New System.Windows.Forms.RadioButton()
        Me.rdo_threshold_notification = New System.Windows.Forms.RadioButton()
        Me.txt_output = New System.Windows.Forms.TextBox()
        Me.my_menu.SuspendLayout()
        Me.SuspendLayout()
        '
        'my_menu
        '
        Me.my_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.my_menu.Location = New System.Drawing.Point(0, 0)
        Me.my_menu.Name = "my_menu"
        Me.my_menu.Size = New System.Drawing.Size(784, 24)
        Me.my_menu.TabIndex = 0
        Me.my_menu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveAsToolStripMenuItem, Me.toolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &As..."
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(177, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BrowserToolStripMenuItem, Me.toolStripSeparator2, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'BrowserToolStripMenuItem
        '
        Me.BrowserToolStripMenuItem.Name = "BrowserToolStripMenuItem"
        Me.BrowserToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.BrowserToolStripMenuItem.Text = "&Browser"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(113, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        'analogread_timer
        '
        '
        'cbo_comport
        '
        Me.cbo_comport.FormattingEnabled = True
        Me.cbo_comport.Location = New System.Drawing.Point(12, 27)
        Me.cbo_comport.Name = "cbo_comport"
        Me.cbo_comport.Size = New System.Drawing.Size(275, 21)
        Me.cbo_comport.TabIndex = 1
        '
        'lbl_command
        '
        Me.lbl_command.AutoSize = True
        Me.lbl_command.Location = New System.Drawing.Point(488, 30)
        Me.lbl_command.Name = "lbl_command"
        Me.lbl_command.Size = New System.Drawing.Size(105, 13)
        Me.lbl_command.TabIndex = 2
        Me.lbl_command.Text = "Enter command here"
        '
        'txt_command
        '
        Me.txt_command.Location = New System.Drawing.Point(599, 27)
        Me.txt_command.Name = "txt_command"
        Me.txt_command.Size = New System.Drawing.Size(45, 20)
        Me.txt_command.TabIndex = 3
        '
        'btn_send_command
        '
        Me.btn_send_command.Location = New System.Drawing.Point(666, 24)
        Me.btn_send_command.Name = "btn_send_command"
        Me.btn_send_command.Size = New System.Drawing.Size(106, 24)
        Me.btn_send_command.TabIndex = 4
        Me.btn_send_command.Text = "Submit command"
        Me.btn_send_command.UseVisualStyleBackColor = True
        '
        'lbl_power_status
        '
        Me.lbl_power_status.AutoSize = True
        Me.lbl_power_status.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_power_status.ForeColor = System.Drawing.Color.Green
        Me.lbl_power_status.Location = New System.Drawing.Point(12, 59)
        Me.lbl_power_status.Name = "lbl_power_status"
        Me.lbl_power_status.Size = New System.Drawing.Size(159, 13)
        Me.lbl_power_status.TabIndex = 5
        Me.lbl_power_status.Text = "Plugged in (100% charged)"
        '
        'chk_analogread
        '
        Me.chk_analogread.AutoSize = True
        Me.chk_analogread.Location = New System.Drawing.Point(368, 55)
        Me.chk_analogread.Name = "chk_analogread"
        Me.chk_analogread.Size = New System.Drawing.Size(112, 17)
        Me.chk_analogread.TabIndex = 6
        Me.chk_analogread.Text = "Analog read mode"
        Me.chk_analogread.UseVisualStyleBackColor = True
        '
        'chk_flicker
        '
        Me.chk_flicker.AutoSize = True
        Me.chk_flicker.Location = New System.Drawing.Point(512, 55)
        Me.chk_flicker.Name = "chk_flicker"
        Me.chk_flicker.Size = New System.Drawing.Size(107, 17)
        Me.chk_flicker.TabIndex = 7
        Me.chk_flicker.Text = "LED flicker mode"
        Me.chk_flicker.UseVisualStyleBackColor = True
        '
        'rdo_no_notification
        '
        Me.rdo_no_notification.AutoSize = True
        Me.rdo_no_notification.Location = New System.Drawing.Point(12, 81)
        Me.rdo_no_notification.Name = "rdo_no_notification"
        Me.rdo_no_notification.Size = New System.Drawing.Size(93, 17)
        Me.rdo_no_notification.TabIndex = 8
        Me.rdo_no_notification.TabStop = True
        Me.rdo_no_notification.Text = "No notification"
        Me.rdo_no_notification.UseVisualStyleBackColor = True
        '
        'rdo_pushbutton_notification
        '
        Me.rdo_pushbutton_notification.AutoSize = True
        Me.rdo_pushbutton_notification.Location = New System.Drawing.Point(12, 104)
        Me.rdo_pushbutton_notification.Name = "rdo_pushbutton_notification"
        Me.rdo_pushbutton_notification.Size = New System.Drawing.Size(124, 17)
        Me.rdo_pushbutton_notification.TabIndex = 9
        Me.rdo_pushbutton_notification.TabStop = True
        Me.rdo_pushbutton_notification.Text = "Notify on Pushbutton"
        Me.rdo_pushbutton_notification.UseVisualStyleBackColor = True
        '
        'rdo_threshold_notification
        '
        Me.rdo_threshold_notification.AutoSize = True
        Me.rdo_threshold_notification.Location = New System.Drawing.Point(12, 127)
        Me.rdo_threshold_notification.Name = "rdo_threshold_notification"
        Me.rdo_threshold_notification.Size = New System.Drawing.Size(117, 17)
        Me.rdo_threshold_notification.TabIndex = 10
        Me.rdo_threshold_notification.TabStop = True
        Me.rdo_threshold_notification.Text = "Notify on Threshold"
        Me.rdo_threshold_notification.UseVisualStyleBackColor = True
        '
        'txt_output
        '
        Me.txt_output.Location = New System.Drawing.Point(12, 150)
        Me.txt_output.Multiline = True
        Me.txt_output.Name = "txt_output"
        Me.txt_output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_output.Size = New System.Drawing.Size(760, 407)
        Me.txt_output.TabIndex = 11
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.txt_output)
        Me.Controls.Add(Me.rdo_threshold_notification)
        Me.Controls.Add(Me.rdo_pushbutton_notification)
        Me.Controls.Add(Me.rdo_no_notification)
        Me.Controls.Add(Me.chk_flicker)
        Me.Controls.Add(Me.chk_analogread)
        Me.Controls.Add(Me.lbl_power_status)
        Me.Controls.Add(Me.btn_send_command)
        Me.Controls.Add(Me.txt_command)
        Me.Controls.Add(Me.lbl_command)
        Me.Controls.Add(Me.cbo_comport)
        Me.Controls.Add(Me.my_menu)
        Me.MainMenuStrip = Me.my_menu
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(800, 600)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "Form1"
        Me.Text = "Starter Application"
        Me.my_menu.ResumeLayout(False)
        Me.my_menu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents my_menu As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BrowserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents analogread_timer As Timer
    Friend WithEvents cbo_comport As ComboBox
    Friend WithEvents lbl_command As Label
    Friend WithEvents txt_command As TextBox
    Friend WithEvents btn_send_command As Button
    Friend WithEvents lbl_power_status As Label
    Friend WithEvents chk_analogread As CheckBox
    Friend WithEvents chk_flicker As CheckBox
    Friend WithEvents rdo_pushbutton_notification As RadioButton
    Friend WithEvents rdo_threshold_notification As RadioButton
    Friend WithEvents txt_output As TextBox
    Friend WithEvents rdo_no_notification As RadioButton
End Class
