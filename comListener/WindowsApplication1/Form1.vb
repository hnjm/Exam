
Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.Windows.Forms
Imports System.Media


Public Class Form1
   
    Delegate Sub SetTextCallback(ByVal [text] As String)
    Dim databaseFile As String
    Dim conString

    Dim smpSelected As Integer
    Dim odrSelected As Integer

    Dim balState As String
    Dim balWeight As Single

    Dim detOperation As Integer
    Dim balOperation As Integer
    Dim balCOperation As Integer
    Dim cabOperation As Integer

    Dim decimalChar As String

    Const CAM_X_ASTIME = &H20000015
    Const CAM_X_PLIVE = &H2000000C
    Const CAM_X_ELIVE = &H20000017
    Const CAM_X_EREAL = &H20000016


    Private Sub BarcodeCOM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarcodeCOM.TextChanged
        If InStr(Me.BarcodeCOM.Text, "COM") <> 0 Then
            If IsNumeric(Replace(Me.BarcodeCOM.Text, "COM", "")) Then
                Me.SerialPortBarcode.PortName = Me.BarcodeCOM.Text
            End If
        End If
    End Sub
    Private Sub BalanceCOM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalanceCOM.TextChanged
        If InStr(Me.BalanceCOM.Text, "COM") <> 0 Then
            If IsNumeric(Replace(Me.BalanceCOM.Text, "COM", "")) Then
                Me.SerialPortBalance.PortName = Me.BalanceCOM.Text
            End If
        End If
    End Sub

    Private Sub BarcodeOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarcodeOpen.Click
        Call openBarcode()
    End Sub
    Private Sub BarcodeClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarcodeClose.Click
        Call closeBarcode()
    End Sub
    Private Sub BalanceOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalanceOpen.Click
        Call openBalance()
    End Sub
    Private Sub BalanceClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalanceClose.Click
        Call closeBalance()
    End Sub

    Sub closeBarcode()
        On Error GoTo ErrorCloseBarcode
        Me.SerialPortBarcode.Close()
        Me.BarcodeStatusTextBox.Text = "port Closed"

        Exit Sub
ErrorCloseBarcode:
        fillBigDisplay("Error while closing com port")
        playERROR()
    End Sub
    Sub closeBalance()
        On Error GoTo ErrorCloseBalance
        Me.SerialPortBalance.Close()
        Me.BalanceStatusTextbox.Text = "port Closed"

        Exit Sub
ErrorCloseBalance:
        fillBigDisplay("error while closing com port")
        playERROR()
    End Sub

    Sub openBarcode()
        On Error GoTo ErrorOpenBarcode
        With Me.SerialPortBarcode
            .PortName = Me.BarcodeCOM.Text
            .Handshake = IO.Ports.Handshake.None
            .ReceivedBytesThreshold = 1
            .RtsEnable = True
            .BaudRate = 9600
            .Parity = IO.Ports.Parity.None
            .DataBits = 8
            .StopBits = 1
            .Open()
            Me.BarcodeStatusTextBox.Text = "port " & Me.SerialPortBarcode.PortName & " OPEN"
            Me.BarcodeStatusTextBox.BackColor = Color.LightGreen
        End With

        Exit Sub
ErrorOpenBarcode:
        Me.BarcodeStatusTextBox.Text = "Opening com port error on " & Me.SerialPortBarcode.PortName
        Me.BarcodeStatusTextBox.BackColor = Color.LightPink
    End Sub
    Sub openBalance()
        Dim x As String

        x = Me.balState
        On Error GoTo ErrorOpenBalance
        With Me.SerialPortBalance
            .PortName = Me.BalanceCOM.Text
            .Handshake = IO.Ports.Handshake.None
            .ReceivedBytesThreshold = 1
            .RtsEnable = True

            .BaudRate = 9600
            .Parity = IO.Ports.Parity.Odd
            .DataBits = 7
            .StopBits = 1
            .Open()
            Me.BalanceStatusTextbox.Text = "port " & Me.SerialPortBalance.PortName & " OPEN"
            Me.BalanceStatusTextbox.BackColor = Color.LightGreen
        End With

        Exit Sub
ErrorOpenBalance:
        Me.BalanceStatusTextbox.Text = "Opening com port error on " & Me.SerialPortBalance.PortName
        Me.BalanceStatusTextbox.BackColor = Color.LightPink
    End Sub

    Private Sub SerialPortBalance_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPortBalance.DataReceived
        Dim inbuff As String
        inbuff = SerialPortBalance.ReadLine
        Call HandleBalanceData(inbuff)
    End Sub
    Private Sub SerialPortBarcode_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPortBarcode.DataReceived
        Dim InBuff As String
        InBuff = SerialPortBarcode.ReadLine
        Call HandleBarcodeData(InBuff)
    End Sub

    Sub HandleBarcodeData(ByVal data As String)
        Dim myStr1 As String
        Dim myStr2 As String
        Call fillDataRead(data)
        Select Case AscW(data)
            Case 77 'FirstLetter is a M achine
                myStr1 = Val(Mid(data, 7, 6))
                myStr2 = Mid(data, 4, 3)
                If myStr2 = "BAL" Then
                    Call balance(myStr1)
                ElseIf myStr2 = "BAX" Then
                    Call balanceCarbon(myStr1)
                ElseIf myStr2 = "DET" Then
                    Call detector(myStr1, smpSelected)
                ElseIf myStr2 = "DES" Then
                    Call detectorStop(myStr1)
                ElseIf myStr2 = "CAB" Then
                    Call cabinet(myStr1)
                End If
            Case 79 'FirstLetter is a O order
                myStr1 = Val(Mid(data, 8, 7))
                Call order(myStr1)
            Case 83 'FirstLetter is a S ample
                myStr1 = Val(Mid(data, 2, 7))
                myStr2 = Val(Mid(data, 11, 7))
                SerialPortBarcode.DiscardInBuffer()
                Call sample(myStr1)
        End Select
    End Sub
    Sub HandleBalanceData(ByVal data As String)
        'BalanceDataTextbox.Text = data
        On Error Resume Next
        If data.Contains("g") Then
            Me.balState = "stable"
        Else
            Me.balState = "unstable"
        End If

        If Me.BalanceDataTextbox.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf HandleBalanceData)
            Me.Invoke(d, New Object() {[data]})
        Else
            Me.BalanceDataTextbox.Text = [data]
        End If
        Dim test As String
        test = Mid(data, 2, 9)
        test = Replace(test, ".", Me.decimalChar)
        Me.balWeight = CSng(test)
    End Sub

    Private Sub BrowseBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseBtn.Click
        OpenFileDialog1.ShowDialog()
    End Sub
    Private Sub OpenFileDialog1_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Me.databaseFile = Me.OpenFileDialog1.FileName
        Call fillDBpathTextBox(Me.databaseFile)
        Me.conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Me.databaseFile
    End Sub

    Sub fillDBpathTextBox(ByVal file As String)
        If Me.DBpathTextBox.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf fillDBpathTextBox)
            Me.Invoke(d, New Object() {[file]})
        Else
            Me.DBpathTextBox.Text = [file]
        End If
    End Sub
    Sub getWeight(ByVal sample)

    End Sub
    Sub disposalMode(ByVal cabinet)

    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Call closeBalance()
        Call closeBarcode()
    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'set connextion string to the database
        Me.conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\Lims\spectro_lab_be.mdb"

        'seach for the system deciaml char
        If CStr(1 / 2).Contains(",") Then
            decimalChar = ","
        Else
            decimalChar = "."
        End If
        decimalLbl.Text = "Actual decimal separator is : " & decimalChar

        'try to open the com ports
        Call openBalance()
        Call openBarcode()

        'initialize variables
        Me.smpSelected = -1
        Me.odrSelected = -1

        Me.detOperation = 0
        Me.balOperation = 0
        Me.cabOperation = 0
    End Sub

    Sub sample(ByVal sample As Integer)
        'we test if this sample exist in the database
        Dim objConn, rs, strSQL
        objConn = CreateObject("ADODB.connection")
        objConn.open(conString)
        rs = CreateObject("ADODB.Recordset")
        strSQL = "SELECT * FROM Sample WHERE ID=" & sample
        rs.Open(strSQL, objConn)                            'see http://www.w3schools.com/ADO/met_rs_open.asp for configuration
        If (rs.EOF) Then
            fillBigDisplay("Sample ID does not correspond to a sample in the database")
            playERROR()
            Me.smpSelected = -1
            Exit Sub
        Else
            Me.smpSelected = sample
            If Me.cabOperation = 0 Then
                fillBigDisplay("You've selected the sample : " & sample)
                playOK()
                Me.balOperation = 1
                Me.balCOperation = 1
                Me.detOperation = 1
                Me.cabOperation = 0
            ElseIf Me.cabOperation = 1 Then
                Me.balOperation = 0
                Me.balCOperation = 0
                Me.detOperation = 0
                If IsDate(rs("Sample_disposal_date").value) = False Then
                    fillBigDisplay("Cabinet Mode : No disposal date for this sample")
                ElseIf Date.Now.CompareTo(rs("Sample_disposal_date").value) < 0 Then
                    fillBigDisplay("Cabinet Mode : Keep it")
                Else
                    fillBigDisplay("Cabinet Mode : Trow it")
                End If
            Else
                fillBigDisplay("Impossible value for cabOperation " & cabOperation)
                Exit Sub
            End If
        End If
        rs.close()
        rs = Nothing
        objConn.close()
        objConn = Nothing
    End Sub
    Sub detector(ByVal det As Integer, ByVal sample As Integer)
        Me.cabOperation = 0
        Me.balOperation = 0
        Me.balCOperation = 0

        If Me.detOperation = 0 Then
            fillBigDisplay("Nothing selected to associate with this detector")
            playERROR()
            Exit Sub
        ElseIf Me.detOperation = 1 Then
            fillBigDisplay("Did you want to start the detector " & det & " for the sample " & sample & "?" & vbCrLf & "Scan a second time to start")
            Me.detOperation = 2
        ElseIf Me.detOperation = 2 Then
            Call startDetector(det, sample)
        End If

    End Sub
    Sub startDetector(ByVal det As Integer, ByVal sample As Integer)
        Dim detName, detPath, toExec, detServer
        Dim Objconn, rs, stsql

        'default value
        'startDetector = -1

        'we seach the detector
        Objconn = CreateObject("ADODB.connection")
        Objconn.open(conString)
        rs = CreateObject("ADODB.Recordset")
        stsql = "SELECT * FROM Detectors WHERE detector_ID=" & det
        rs.Open(stsql, Objconn, 1)
        'rs = Objconn.execute(stsql)

        If (rs.EOF) Then
            fillBigDisplay("startDetector Error : There is no detector with this ID in the database !")
            playERROR()
            'startDetector = 1
            Exit Sub
        ElseIf rs("detector_inUse").value = True Then
            fillBigDisplay("startDetector error : detector already in use !")
            playERROR()
            'startDetector = 1
            Exit Sub
        Else
            detName = rs("detector_name").value
            detPath = rs("detector_path").value
            detServer = rs("detector_server").value
        End If
        rs.Close()

        'we create a new Spectra for this sample

        'definition of the fileName
        Dim fyear As String 'file year
        Dim fmonth As String 'file month
        Dim myOrder As Integer

        'we search the order to get the reception date and create the right subdisrectory
        stsql = "SELECT Sample_Order_ID FROM Sample WHERE ID=" & sample
        rs.Open(stsql, conString, 1)
        If (rs.EOF) Then
            fillBigDisplay("startDetector Error : This sample has no associated order !")
            playERROR()
            'startDetector = 2
            Exit Sub
        Else
            myOrder = rs("Sample_Order_ID").value
        End If
        rs.Close()
        stsql = "SELECT * FROM Orders WHERE ID_SCK=" & myOrder
        rs.Open(stsql, conString, 1)
        If (rs.EOF) Then
            fyear = "-1"
            fmonth = "-1"
            fillBigDisplay("startDetector Error : this order does not exists !")
            playERROR()
            'startDetector = 3
            Exit Sub
        Else
            fyear = Microsoft.VisualBasic.Left(rs("Order_reception_date").value, 4)
            fmonth = Mid(rs("Order_reception_date").value, 6, 2)
        End If
        rs.Close()

        Select Case fmonth
            Case "01"
                fmonth = "January"
            Case "02"
                fmonth = "February"
            Case "03"
                fmonth = "March"
            Case "04"
                fmonth = "April"
            Case "05"
                fmonth = "May"
            Case "06"
                fmonth = "June"
            Case "07"
                fmonth = "July"
            Case "08"
                fmonth = "August"
            Case "09"
                fmonth = "September"
            Case "10"
                fmonth = "October"
            Case "11"
                fmonth = "November"
            Case "12"
                fmonth = "December"
            Case Else
                fmonth = "-1"
                fillBigDisplay("startDetector Big fault on the date this text box should never appear !")
                playERROR()
                'startDetector = 4
                Exit Sub
        End Select

        Dim spPath As String
        Dim spName As String

        'Searching of the saving directory for the configuration table
        stsql = "SELECT * FROM Configuration WHERE is_selected=TRUE"
        rs.Open(stsql, conString, 1)
        If (rs.EOF) Then
            fillBigDisplay("Error no saving directory defined in the configuration")
            playERROR()
            'startDetector = 5
            Exit Sub
        Else
            spPath = rs("CAM_files_saving_path").value
        End If
        rs.Close()

        If Microsoft.VisualBasic.Right(spPath, 1) <> "\" Then
            spPath = spPath & "\"
        End If

        spPath = spPath & fyear

        If ExistPath(spPath) = False Then
            MkDir(spPath)
        End If

        spPath = spPath & "\" & fmonth

        If ExistPath(spPath) = False Then
            MkDir(spPath)
        End If


        'the directory is find, we now looking for the filename of the camfile
        Dim fnum    'file mesurement number

        stsql = "SELECT * FROM Spectra WHERE Meas_sample_ID=" & sample & " ORDER BY Meas_number DESC"
        rs.Open(stsql, conString, 1)
        If (rs.EOF) Then
            fnum = 0
            'MsgBox "startDetector Error : no spectra for this ID"
        Else
            fnum = rs("Meas_number").value + 1
        End If
        rs.Close()


        spName = Format(sample, "0000000") & "m"
        spName = spName & Format(fnum, "0000") & ".CNF"
        'spName = String(7 - Len(LTrim(sample)), "0") & LTrim(sample) & "m"
        'spName = spName & String(2 - Len(LTrim(fnum)), "0") & LTrim(fnum) & ".CNF"

        Dim spPreset, spPresetID
        stsql = "SELECT Sample_timePreset FROM Sample WHERE ID=" & sample
        rs.Open(stsql, conString, 1)
        If (rs.EOF) Then
            fillBigDisplay("Start Detector ERROR : no valid preset to start")
            playERROR()
            'startDetector = 5
            Exit Sub
        Else
            spPreset = rs("Sample_timePreset").value
        End If
        rs.Close()

        Dim spTypeId
        stsql = "SELECT Sample_meas_type_ID FROM Sample WHERE ID=" & sample
        rs.Open(stsql, conString, 1)
        If (rs.EOF) Then
            spTypeId = -1
            fillBigDisplay("Switchboard warning : No default measurement type for this sample")
            playERROR()
        Else
            spTypeId = rs("Sample_meas_type_ID").value
        End If
        rs.Close()

        'the vbscipt path is in the environment variables path=...

        'stSql = "SELECT VBS_scripts_path FROM Configuration WHERE is_selected=TRUE"
        'rs.Open(stsql, conString, 1)
        'If rs.EOF Then
        'fillBigDisplay("startDetector Error : no path specified in the configuration for VBScripts")
        'startDetector = 6
        'Exit Sub
        'Else
        'toExec = rs("VBS_scripts_path").value
        'End If

        'If Microsoft.VisualBasic.Right(toExec, 1) <> "\" Then
        'toExec = toExec & "\"
        'End If

        'If ExistPath(toExec) = False Then
        'fillBigDisplay("the path VBS_scripts_path defined in the configuration does not exists" & toExec)
        'startDetector = 7
        'Exit Sub
        'End If

        'rs.Close()
        rs = Nothing

        If detIsAviable(detName, detServer) = False Then
            fillBigDisplay("Detector not aviable")
            playERROR()
            Exit Sub
        End If

        If Dir(spPath & "\" & spName, vbNormal) <> "" Then
            fillBigDisplay("Destination file already exists")
            playERROR()
            Exit Sub
        End If

        'we create it
        rs = CreateObject("ADODB.recordset")
        If IsNumeric(spPreset) = False Then
            fillBigDisplay("No time preset for this sample, we cannot start")
            playERROR()
            Exit Sub
        End If
        If IsNumeric(spTypeId) = False Then
            fillBigDisplay("No measurment type id for this sample, we cannot start")
            playERROR()
            Exit Sub
        End If
        stsql = "INSERT INTO Spectra (Meas_sample_ID,Meas_number,Meas_detector_ID, Meas_spectrum_path, Meas_spectrum_name,Meas_PRESET,Meas_type_ID)"
        stsql = stsql & "VALUES ('" & sample & "','" & fnum & "', '" & det & "','" & spPath & "','" & spName & "','" & spPreset & "','" & spTypeId & "')"
        'MsgBox(stsql)
        Objconn.Execute(stsql)


        stsql = "SELECT ID FROM Spectra WHERE Meas_spectrum_name='" & spName & "'"
        rs.Open(stsql, conString, 1)

        rs.Close()
        rs = Nothing
        Objconn.Close()
        Objconn = Nothing

        'we start the aquisition
        Dim ret

        Dim myShell
        myShell = CreateObject("WScript.Shell")

        toExec = "genie_startSave.vbs " & detPath & " " & spPreset & " " & spPath & "\" & spName
        'toExec = toExec & " " & detPath & " " & spPreset & " " & spPath & "\" & spName
        On Error Resume Next

        'MsgBox(toExec)
        ret = myShell.Run(toExec, 1, False)

        smpSelected = -1
        'detSelected = -1
        odrSelected = -1

        If ret <> 0 Then
            fillBigDisplay("Error n°" & ret & " for the command : " & toExec)
            playERROR()
        Else
            'startDetector = 0
            fillBigDisplay("detector start_script started " & toExec)
            playOK()
        End If

    End Sub
    Sub detectorStop(ByVal det As Integer)

        Dim con, rs, toExec

        Dim stSql
        'detectorStop = -1 'defalut value
        'Dim response

        con = CreateObject("ADODB.connection")
        con.open(conString)
        'con = Application.CurrentProject.Connection
        rs = CreateObject("ADODB.recordset")

        stSql = "SELECT * FROM Detectors WHERE detector_ID=" & det
        rs.Open(stSql, con, 1)
        If (rs.EOF) Then
            fillBigDisplay("detectorStop error : detector not found")
            playERROR()
            'detectorStop = 1
            Exit Sub
            'ElseIf rs("detector_inUse").value = False Then
            'response = MsgBox("Warning : detector seem not to be in use, proceed anyway ?", vbYesNo, "Stop Detector")
            'If response = vbYes Then
            'Else
            'detecotrStop = 0
            'Exit Sub
            'End If
        End If
        rs.Close()

        stSql = "SELECT genieEXE_path FROM Configuration WHERE is_selected=TRUE"
        rs.Open(stSql, con, 1)
        If (rs.EOF) Then
            fillBigDisplay("detectorStop error : Error for the genieEXE_path" & stSql)
            playERROR()
            'detectorStop = 1
            Exit Sub
        Else
            toExec = rs("genieEXE_path").value
        End If
        rs.Close()

        If Microsoft.VisualBasic.Right(toExec, 1) <> "\" Then
            toExec = toExec & "\"
        End If

        If ExistPath(toExec) = False Then
            fillBigDisplay("detectorStop error : the path genieEXE_path defined in the configuration does not exists")
            playERROR()
            'detectorStop = 1
            Exit Sub
        End If

        stSql = "SELECT * FROM Detectors WHERE detector_ID=" & det
        rs.Open(stSql, con, 1)
        If (rs.EOF) Then
            fillBigDisplay("detectorStop Error : no detector for this ID")
            playERROR()
            'detectorStop = 1
            Exit Sub
        End If

        rs.Close()

        'response = MsgBox(alertString, vbYesNo, "Stop Detector ?")
        If True Then 'reponse = vbYes Then ' They Clicked YES!

            toExec = toExec & "stopmca.exe "

            stSql = "SELECT * FROM Detectors WHERE detector_ID=" & det
            rs.Open(stSql, con, 1)
            If (rs.EOF) Then
                fillBigDisplay("detectorStop Error : no detector for this ID")
                playERROR()
                'detectorStop = 1
                Exit Sub
            Else
                toExec = toExec & rs("detector_path").value
            End If
            rs = Nothing
            con.Close()
            con = Nothing

            Dim ret
            Dim myShell
            myShell = CreateObject("WScript.Shell")
            ret = myShell.Run(toExec, 0, True)

            If ret <> 0 Then
                fillBigDisplay("execution error for the command " & toExec & " in detectorStop")
                playERROR()
            Else
                'detectorStop = 0
                fillBigDisplay("The request to stop is sended to the detector")
                playOK()
            End If
            myShell = Nothing

        Else    'NO clicked
        End If
    End Sub
    Sub order(ByVal order As Integer)
        fillBigDisplay("order : " & order)

    End Sub
    Sub cabinet(ByVal cabinet As Integer)
        Me.detOperation = 0
        Me.balOperation = 0
        Me.balCOperation = 0
        If Me.cabOperation = 0 Then
            fillBigDisplay("Entering in cabinet mode")
            cabOperation = 1
        Else
            fillBigDisplay("Outgoing of cabinet mode")
            cabOperation = 0
        End If
    End Sub
    Sub balance(ByVal whatIsWeighted As Integer)
        Dim con As Object
        Dim rs As Object
        Dim stSql As String
        Dim Sample_weighted As Boolean

        Me.detOperation = 0
        Me.cabOperation = 0
        Me.balCOperation = 0

        If Me.balOperation = 1 Then
            con = CreateObject("ADODB.connection")
            con.open(conString)
            rs = CreateObject("ADODB.recordset")

            stSql = "SELECT sample_weighted FROM Sample WHERE ID=" & smpSelected
            rs.Open(stSql, con, 1)
            If (rs.EOF) Then
                fillBigDisplay("Switchboard balance error : There are no items for this sample value in the database !")
                playERROR()
                Me.balOperation = 0
                Exit Sub
            End If
            Sample_weighted = rs("sample_weighted").value
            rs.Close()
            rs = Nothing
            con.Close()
            con = Nothing


            If Sample_weighted Then
                fillBigDisplay("The sample " & smpSelected & " is already weighted! Scan a second time to save a new value")
                playERROR()
            Else
                fillBigDisplay("Do you want to weight the sample " & smpSelected & "? Scan a second time to weight")
            End If
            Me.balOperation = 2
        ElseIf Me.balOperation = 2 Then
            fillBigDisplay("Start weighting")
            If Me.balState <> "stable" Then
                fillBigDisplay("the weight is not stable ! try again")
                playERROR()
                Exit Sub
            Else
                con = CreateObject("ADODB.connection")
                con.open(conString)
                Dim weight As String
                weight = Replace(Me.balWeight, Me.decimalChar, ".")


                stSql = "UPDATE sample SET Sample_weight=" & weight & ", sample_weighted=TRUE WHERE ID=" & smpSelected
                con.Execute(stSql)
                stSql = "UPDATE sample SET Sample_weight_error=0.05 , sample_weighted=TRUE WHERE ID=" & smpSelected
                con.Execute(stSql)
                con.Close()
                con = Nothing
            End If

            Me.balOperation = 0

            fillBigDisplay("sample weighted and measure saved in database")
            playOK()

            smpSelected = -1
            odrSelected = -1

        ElseIf Me.balOperation = 0 Then
            fillBigDisplay("you selected the balance without selecting a sample before")
            playERROR()
        Else
            fillBigDisplay("Impossoble value in Sub balance")
            playERROR()
        End If

    End Sub
    Sub balanceCarbon(ByVal bal As Integer)
        Dim con As Object
        Dim rs As Object
        Dim stSql As String
        Static carbonWeight As Single
        Static sampleWeight As Single

        detOperation = 0
        cabOperation = 0

        If Me.balCOperation = 1 Then
            con = CreateObject("ADODB.connection")
            con.open(conString)
            rs = CreateObject("ADODB.recordset")

            stSql = "SELECT sample_weighted FROM Sample WHERE ID=" & smpSelected
            rs.Open(stSql, con, 1)
            If (rs.EOF) Then
                fillBigDisplay("Switchboard balance error : There are no items for this sample value in the database !")
                playERROR()
                Me.balCOperation = 0
                Exit Sub
            End If
            rs.Close()
            rs = Nothing
            con.Close()
            con = Nothing

            fillBigDisplay("tare the empty balance and weight the carbon before the mixing operation")
            Me.balCOperation = 2
        ElseIf Me.balCOperation = 2 Then
            fillBigDisplay("Start weighting")
            If Me.balState <> "stable" Then
                fillBigDisplay("the weight is not stable ! try again")
                playERROR()
                Exit Sub
            Else
                carbonWeight = Me.balWeight

            End If
            fillBigDisplay("carbon weight ok, tare with carbon plus empty box and weight the carbon and the mix")
            Me.balCOperation = 3
        ElseIf Me.balCOperation = 3 Then
            fillBigDisplay("Start weighting")
            If Me.balState <> "stable" Then
                fillBigDisplay("the weight is not stable ! try again")
                playERROR()
                Exit Sub
            Else
                sampleWeight = Me.balWeight
            End If
            fillBigDisplay("sample weight ok, tare and place only the carbon used")
            Me.balCOperation = 4
        ElseIf Me.balCOperation = 4 Then
            fillBigDisplay("Start weighting")
            If Me.balState <> "stable" Then
                fillBigDisplay("the weight is not stable ! try again")
                playERROR()
                Exit Sub
            Else
                con = CreateObject("ADODB.connection")
                con.open(conString)
                Dim sampleWeightStr As String
                Dim carbonWeightStr As String
                sampleWeightStr = Replace(sampleWeight, Me.decimalChar, ".")
                carbonWeight = carbonWeight - Me.balWeight
                carbonWeightStr = Replace(carbonWeight, Me.decimalChar, ".")


                stSql = "UPDATE sample SET Sample_weight=" & sampleWeightStr & ", sample_weighted=TRUE WHERE ID=" & smpSelected
                con.Execute(stSql)
                stSql = "UPDATE sample SET Sample_additive_weight=" & carbonWeightStr & " WHERE ID=" & smpSelected
                con.Execute(stSql)
                stSql = "UPDATE sample SET Sample_weight_error=0.05 , sample_weighted=TRUE WHERE ID=" & smpSelected
                con.Execute(stSql)
                stSql = "UPDATE sample SET Sample_additive_weight_error=0.05 , sample_weighted=TRUE WHERE ID=" & smpSelected
                con.Execute(stSql)
                con.Close()
                con = Nothing
                fillBigDisplay("Operation weighting finished")
                playOK()

            End If

            Me.balCOperation = 0
            smpSelected = -1
            odrSelected = -1

        ElseIf Me.balCOperation = 0 Then
            fillBigDisplay("you selected the balance without selecting a sample before")
            playERROR()
        Else
            fillBigDisplay("Impossoble value in Sub balanceCarbon")
            playERROR()
        End If

    End Sub

    Sub fillBigDisplay(ByVal mystring As String)
        If Me.bigDisplay.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf fillBigDisplay)
            Me.Invoke(d, New Object() {[mystring]})
        Else
            Me.bigDisplay.Text = [mystring]
        End If
    End Sub
    Sub fillDataRead(ByVal mystring As String)
        If Me.BarcodeDataTextbox.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf fillDataRead)
            Me.Invoke(d, New Object() {[mystring]})
        Else
            Me.BarcodeDataTextbox.Text = [mystring]
        End If
    End Sub
    Public Function ExistPath(ByVal path) As Boolean
        Dim fso
        fso = CreateObject("Scripting.FileSystemObject")
        ExistPath = fso.FolderExists(path)
    End Function
    Public Function detIsAviable(ByVal detname As String, ByVal detserver As String) As Boolean
        Dim mydet
        ' mydet = New CAMSRCLib.CamDatasource
        Dim ret
        Try
            '  ret = mydet.OpenEx(detname, CAMSRCLib.OpenOptions.camReadWrite, CAMSRCLib.SourceTypes.camSpectroscopyDetector, detserver)
            '  mydet.close()
            detIsAviable = True
        Catch ex As Exception
            detIsAviable = False
        End Try
    End Function
    Public Function getDetParams2(ByVal detPath As String, ByVal detName As String) As String
    

        Dim mydet
        '  mydet = New CAMSRCLib.CamDatasource
        Dim myString As String
        Dim ret, camReadWrite, camSpectroscopyDetector
        camReadWrite = 512
        camSpectroscopyDetector = 4
        myString = ""

        On Error GoTo errHandler

        ret = mydet.OpenEx(detName, camReadWrite, camSpectroscopyDetector, detPath)

        If IsDate(mydet.Parameter(CAM_X_ASTIME)) Then
            myString = myString & "Starting date : " & mydet.Parameter(CAM_X_ASTIME) & vbCrLf
        Else
            myString = myString & "Starting date : Undefined" & vbCrLf
        End If
        myString = myString & "LIVE PRESET : " & mydet.Parameter(CAM_X_PLIVE) & vbCrLf
        myString = myString & "LIVE : " & mydet.Parameter(CAM_X_ELIVE) & vbCrLf
        myString = myString & "REAL : " & mydet.Parameter(CAM_X_EREAL) & vbCrLf
        mydet.Close()

        getDetParams2 = myString
        Exit Function
errHandler:
        fillBigDisplay("Common Functions Error, please check that detector is not open in Genie-2000")
        playERROR()
        If ret = 0 Then
            mydet.Close()
        End If

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Call getDetParams2("PC1074", "DET-21")

    End Sub
    Private Sub playERROR()
        Dim sPlay As New System.Media.SoundPlayer
        sPlay.SoundLocation = "c:\WINNT\Media\error.wav"
        sPlay.Play()
    End Sub
    Private Sub playOK()
        Dim sPlay As New System.Media.SoundPlayer
        sPlay.SoundLocation = "c:\WINNT\Media\OK.wav"
        sPlay.Play()
    End Sub
End Class