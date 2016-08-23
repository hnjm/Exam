<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.DBpathTextBox = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.BrowseBtn = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BarcodeClose = New System.Windows.Forms.Button
        Me.BarcodeOpen = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.BarcodeCOM = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.BarcodeDataTextbox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.BarcodeStatusTextBox = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.BalanceDataTextbox = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.BalanceStatusTextbox = New System.Windows.Forms.TextBox
        Me.BalanceClose = New System.Windows.Forms.Button
        Me.BalanceOpen = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.BalanceCOM = New System.Windows.Forms.TextBox
        Me.SerialPortBarcode = New System.IO.Ports.SerialPort(Me.components)
        Me.SerialPortBalance = New System.IO.Ports.SerialPort(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.bigDisplay = New System.Windows.Forms.TextBox
        Me.decimalLbl = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DBpathTextBox
        '
        Me.DBpathTextBox.Location = New System.Drawing.Point(187, 22)
        Me.DBpathTextBox.Name = "DBpathTextBox"
        Me.DBpathTextBox.Size = New System.Drawing.Size(448, 20)
        Me.DBpathTextBox.TabIndex = 0
        Me.DBpathTextBox.Text = "c:\Lims\spectro_lab_be.mdb"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Default acces database file"
        '
        'BrowseBtn
        '
        Me.BrowseBtn.Location = New System.Drawing.Point(521, 48)
        Me.BrowseBtn.Name = "BrowseBtn"
        Me.BrowseBtn.Size = New System.Drawing.Size(114, 31)
        Me.BrowseBtn.TabIndex = 2
        Me.BrowseBtn.Text = "Browse..."
        Me.BrowseBtn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BarcodeClose)
        Me.GroupBox1.Controls.Add(Me.BarcodeOpen)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.BarcodeCOM)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.BarcodeDataTextbox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.BarcodeStatusTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(48, 85)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(587, 114)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Barcode Reader"
        '
        'BarcodeClose
        '
        Me.BarcodeClose.Location = New System.Drawing.Point(353, 16)
        Me.BarcodeClose.Name = "BarcodeClose"
        Me.BarcodeClose.Size = New System.Drawing.Size(116, 24)
        Me.BarcodeClose.TabIndex = 7
        Me.BarcodeClose.Text = "Close"
        Me.BarcodeClose.UseVisualStyleBackColor = True
        '
        'BarcodeOpen
        '
        Me.BarcodeOpen.Location = New System.Drawing.Point(221, 15)
        Me.BarcodeOpen.Name = "BarcodeOpen"
        Me.BarcodeOpen.Size = New System.Drawing.Size(111, 24)
        Me.BarcodeOpen.TabIndex = 6
        Me.BarcodeOpen.Text = "Open"
        Me.BarcodeOpen.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "COM Port to use"
        '
        'BarcodeCOM
        '
        Me.BarcodeCOM.Location = New System.Drawing.Point(119, 18)
        Me.BarcodeCOM.Name = "BarcodeCOM"
        Me.BarcodeCOM.Size = New System.Drawing.Size(82, 20)
        Me.BarcodeCOM.TabIndex = 4
        Me.BarcodeCOM.Text = "COM3"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Data Read :"
        '
        'BarcodeDataTextbox
        '
        Me.BarcodeDataTextbox.Location = New System.Drawing.Point(167, 79)
        Me.BarcodeDataTextbox.Name = "BarcodeDataTextbox"
        Me.BarcodeDataTextbox.Size = New System.Drawing.Size(398, 20)
        Me.BarcodeDataTextbox.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Status :"
        '
        'BarcodeStatusTextBox
        '
        Me.BarcodeStatusTextBox.Location = New System.Drawing.Point(167, 50)
        Me.BarcodeStatusTextBox.Name = "BarcodeStatusTextBox"
        Me.BarcodeStatusTextBox.Size = New System.Drawing.Size(397, 20)
        Me.BarcodeStatusTextBox.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RichTextBox1)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.BalanceDataTextbox)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.BalanceStatusTextbox)
        Me.GroupBox2.Controls.Add(Me.BalanceClose)
        Me.GroupBox2.Controls.Add(Me.BalanceOpen)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.BalanceCOM)
        Me.GroupBox2.Location = New System.Drawing.Point(48, 205)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(587, 121)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Balance"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(501, 83)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(140, 89)
        Me.RichTextBox1.TabIndex = 16
        Me.RichTextBox1.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(29, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Data Read :"
        '
        'BalanceDataTextbox
        '
        Me.BalanceDataTextbox.Location = New System.Drawing.Point(168, 79)
        Me.BalanceDataTextbox.Name = "BalanceDataTextbox"
        Me.BalanceDataTextbox.Size = New System.Drawing.Size(315, 20)
        Me.BalanceDataTextbox.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Status :"
        '
        'BalanceStatusTextbox
        '
        Me.BalanceStatusTextbox.Location = New System.Drawing.Point(168, 50)
        Me.BalanceStatusTextbox.Name = "BalanceStatusTextbox"
        Me.BalanceStatusTextbox.Size = New System.Drawing.Size(397, 20)
        Me.BalanceStatusTextbox.TabIndex = 12
        '
        'BalanceClose
        '
        Me.BalanceClose.Location = New System.Drawing.Point(353, 20)
        Me.BalanceClose.Name = "BalanceClose"
        Me.BalanceClose.Size = New System.Drawing.Size(116, 24)
        Me.BalanceClose.TabIndex = 11
        Me.BalanceClose.Text = "Close"
        Me.BalanceClose.UseVisualStyleBackColor = True
        '
        'BalanceOpen
        '
        Me.BalanceOpen.Location = New System.Drawing.Point(221, 19)
        Me.BalanceOpen.Name = "BalanceOpen"
        Me.BalanceOpen.Size = New System.Drawing.Size(111, 24)
        Me.BalanceOpen.TabIndex = 10
        Me.BalanceOpen.Text = "Open"
        Me.BalanceOpen.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "COM Port to use"
        '
        'BalanceCOM
        '
        Me.BalanceCOM.Location = New System.Drawing.Point(119, 22)
        Me.BalanceCOM.Name = "BalanceCOM"
        Me.BalanceCOM.Size = New System.Drawing.Size(82, 20)
        Me.BalanceCOM.TabIndex = 8
        Me.BalanceCOM.Text = "COM1"
        '
        'SerialPortBarcode
        '
        Me.SerialPortBarcode.PortName = "COM3"
        '
        'SerialPortBalance
        '
        Me.SerialPortBalance.DataBits = 7
        Me.SerialPortBalance.DtrEnable = True
        Me.SerialPortBalance.Parity = System.IO.Ports.Parity.Odd
        Me.SerialPortBalance.PortName = "COM2"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "spectro_lab_be"
        Me.OpenFileDialog1.InitialDirectory = "O:\PUBLIEK\spectro\Lims"
        Me.OpenFileDialog1.Title = "Select the access database backend file :"
        '
        'bigDisplay
        '
        Me.bigDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bigDisplay.Location = New System.Drawing.Point(47, 336)
        Me.bigDisplay.Multiline = True
        Me.bigDisplay.Name = "bigDisplay"
        Me.bigDisplay.Size = New System.Drawing.Size(588, 93)
        Me.bigDisplay.TabIndex = 5
        '
        'decimalLbl
        '
        Me.decimalLbl.AutoSize = True
        Me.decimalLbl.Location = New System.Drawing.Point(164, 57)
        Me.decimalLbl.Name = "decimalLbl"
        Me.decimalLbl.Size = New System.Drawing.Size(133, 13)
        Me.decimalLbl.TabIndex = 6
        Me.decimalLbl.Text = "Current decimal separator :"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(9, 233)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 15)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 445)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.decimalLbl)
        Me.Controls.Add(Me.bigDisplay)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BrowseBtn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DBpathTextBox)
        Me.Name = "Form1"
        Me.Text = "LIMS Aquisition"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DBpathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BrowseBtn As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BarcodeStatusTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BarcodeCOM As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BarcodeDataTextbox As System.Windows.Forms.TextBox
    Friend WithEvents BarcodeClose As System.Windows.Forms.Button
    Friend WithEvents BarcodeOpen As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BalanceDataTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents BalanceStatusTextbox As System.Windows.Forms.TextBox
    Friend WithEvents BalanceClose As System.Windows.Forms.Button
    Friend WithEvents BalanceOpen As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BalanceCOM As System.Windows.Forms.TextBox
    Friend WithEvents SerialPortBarcode As System.IO.Ports.SerialPort
    Friend WithEvents SerialPortBalance As System.IO.Ports.SerialPort
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents bigDisplay As System.Windows.Forms.TextBox
    Friend WithEvents decimalLbl As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox

End Class
