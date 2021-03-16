Public NotInheritable Class AboutBox1

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        'Me.TextBoxDescription.Text = My.Application.Info.Description
        Me.TextBoxDescription.Text = "This software has been developed for R&D use only.  It is not supported by the IT department and may not be suitable for production use. Support provided by the Author: Donald Duck"


        ''Application title
        'If My.Application.Info.Title <> "" Then
        '    ApplicationTitle.Text = My.Application.Info.Title
        'Else
        '    'If the application title is missing, use the application name, without the extension
        '    ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        'End If

        '' Get the operating system version.
        'Dim OS As OperatingSystem = Environment.OSVersion
        'Dim OS_Ver As Version = OS.Version
        'lblOperatingSystemVer.Text = "OS Ver = " & OS_Ver.ToString & vbCrLf

        '' Get the common language runtime version.
        'Dim Env_Ver As Version = Environment.Version
        'lblCLRver.Text = "CLR Ver = " & Env_Ver.ToString & vbCrLf

        '' Get the version of the executing assembly (that is, this assembly).
        ''  Dim CurrAssem As System.Reflection.Assembly = System.Reflection.Assembly.GetEntryAssembly()
        'Dim CurrAssem As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
        'Dim CurrAssemName As System.Reflection.AssemblyName = CurrAssem.GetName()
        'Dim CurrAss_Ver As Version = CurrAssemName.Version


        ''Copyright info
        'lblCopyright.Text = My.Application.Info.Copyright

    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
