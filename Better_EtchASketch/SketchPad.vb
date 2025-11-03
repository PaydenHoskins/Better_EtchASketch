'Payden Hoskins
'RCET3371
'FALL 2025
'Better_EtchASketch
'https://github.com/PaydenHoskins/Better_EtchASketch.git

Option Explicit On
Option Strict On
Imports System.IO.Ports
Imports System.Threading.Thread
Imports System.Windows.Forms.VisualStyles.VisualStyleElement


Public Class SketchPad
    Function ForeGround(Optional newColor As Color = Nothing) As Color
        Static _foreColor As Color = Color.Black
        If newColor <> Nothing Then
            _foreColor = newColor
        End If
        Return _foreColor
    End Function
    Sub DrawTanWave()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim penThird As New Pen(Color.Aquamarine, 5)
        Dim ymax As Integer = CInt(DrawingPictureBox.Height / 32)
        Dim oldX, oldY, newY As Integer
        Dim yOffset As Integer = CInt(DrawingPictureBox.Height / 2)
        oldY = yOffset
        Dim degreesPerPoint As Double = 360 / DrawingPictureBox.Width
        Try
            For x = 0 To DrawingPictureBox.Width
                newY = CInt(ymax * Math.Tan((Math.PI / 180) * (x * degreesPerPoint))) + yOffset
                g.DrawLine(penThird, oldX, oldY, x, newY)
                oldX = x
                oldY = newY
            Next
        Catch ex As Exception
            MsgBox("TAN Overflow, Try again.")
        End Try
        g.Dispose()
    End Sub
    Sub DrawCosWave()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim penSecond As New Pen(Color.Black, 5)
        Dim ymax As Integer = CInt(DrawingPictureBox.Height / 2)
        Dim oldX, oldY, newY As Integer
        Dim yOffset As Integer = CInt(DrawingPictureBox.Height / 2)
        oldY = DrawingPictureBox.Height
        Dim degreesPerPoint As Double = 360 / DrawingPictureBox.Width
        For x = 0 To DrawingPictureBox.Width
            newY = CInt(ymax * Math.Cos((Math.PI / 180) * (x * degreesPerPoint))) + DrawingPictureBox.Height \ 2
            g.DrawLine(penSecond, oldX, oldY, x, newY)
            oldX = x
            oldY = newY
        Next
    End Sub
    Sub DrawSinWave()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Red, 5)
        Dim ymax As Integer = CInt(DrawingPictureBox.Height / 2)
        Dim oldX, oldY, newY As Integer
        Dim yOffset As Integer = CInt(DrawingPictureBox.Height / 2)
        oldY = yOffset
        Dim degreesPerPoint As Double = 360 / DrawingPictureBox.Width
        For x = 0 To DrawingPictureBox.Width
            newY = CInt(ymax * Math.Sin((Math.PI / 180) * (x * degreesPerPoint))) + yOffset
            g.DrawLine(pen, oldX, oldY, x, newY)
            oldX = x
            oldY = newY
        Next
        g.Dispose()
    End Sub
    Sub DrawWithMouse(oldX As Integer, oldY As Integer, newX As Integer, newY As Integer)
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(ForeGround)
        pen.Width = 5
        g.DrawLine(pen, oldX, oldY, newX, newY)
        g.Dispose()
    End Sub
    Sub DrawGridLine()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Green, 5)
        Dim height As Integer = DrawingPictureBox.Bottom
        Dim width As Integer = DrawingPictureBox.Right
        Dim scaleY As Integer = CInt(DrawingPictureBox.Height / 10)
        Dim scaleX As Integer = CInt(DrawingPictureBox.Width / 10)
        Dim y As Integer = 0
        Dim x As Integer = 0
        'pen.Color = Color.Bisque
        Do Until y > DrawingPictureBox.Height
            y += (DrawingPictureBox.Height \ 10)
            g.DrawLine(pen, 0, scaleY, width, scaleY)
            scaleY += CInt(DrawingPictureBox.Height / 10)
        Loop
        Do Until x > DrawingPictureBox.Width
            x += (DrawingPictureBox.Width \ 10)
            g.DrawLine(pen, scaleX, 0, scaleX, height)
            scaleX += CInt(DrawingPictureBox.Width / 10)
        Loop
        g.DrawLine(pen, 0, DrawingPictureBox.Bottom, 0, 0)
        g.DrawLine(pen, 0, 0, DrawingPictureBox.Right, 0)
        g.Dispose()
    End Sub
    'Event Handlers
    Private Sub GraphicExamplesForm_MouseMove(sender As Object, e As MouseEventArgs) Handles DrawingPictureBox.MouseMove, DrawingPictureBox.MouseDown
        If MouseRadioButton.Checked = True Then
            PotTimer.Stop()
            Static oldX, oldY As Integer
            Me.Text = $"({e.X},{e.Y}) {e.Button.ToString}"
            Select Case e.Button.ToString
                Case "Left"
                    DrawWithMouse(oldX, oldY, e.X, e.Y)
                Case "Right"
                    RightClickContextMenuStrip.Show()
                Case "Middle"
                    DrawWithMouse(oldX, oldY, e.X, e.Y)
            End Select
            oldX = e.X
            oldY = e.Y
        Else
            PotTimer.Start()
        End If
    End Sub
    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click, ClearButton.Click, ClearToolStripMenuItem1.Click
        Dim shake As Integer = 175
        Try
            My.Computer.Audio.Play(My.Resources.Shaker, AudioPlayMode.Background)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        For i = 1 To 10
            Me.Top += shake
            Me.Left += shake
            Sleep(50)
            shake *= -1
        Next
        DrawingPictureBox.Refresh()
    End Sub
    Private Sub ForeGroundColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorSelectButton.Click, SelectColorToolStripMenuItem.Click, SelectColorToolStripMenuItem1.Click
        ColorDialog.ShowDialog()
        ForeGround(ColorDialog.Color)
    End Sub
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click, ExitToolStripMenuItem.Click, ExitToolStripMenuItem1.Click
        Me.Close()
        End
    End Sub
    Private Sub DrawWaveFormButton_Click(sender As Object, e As EventArgs) Handles DrawWaveFormButton.Click, DrawWaveformToolStripMenuItem.Click, DrawWaveFormToolStripMenuItem1.Click
        Dim shake As Integer = 175
        Try
            My.Computer.Audio.Play(My.Resources.Shaker, AudioPlayMode.Background)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        For i = 1 To 10
            Me.Top += shake
            Me.Left += shake
            Sleep(50)
            shake *= -1
        Next
        DrawingPictureBox.Refresh()
        DrawGridLine()
        DrawSinWave()
        DrawCosWave()
        DrawTanWave()
    End Sub

    Private Sub SketchPad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MouseRadioButton.Checked = True
        For port = 0 To SerialPort.GetPortNames.Length - 1
            ToolStripComboBox1.Items.Add(SerialPort.GetPortNames()(port))
        Next
        ToolStripComboBox1.SelectedIndex = 0
        Connect()
    End Sub
    Sub Connect()
        SerialPort1.Close()
        SerialPort1.BaudRate = 9600
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.DataBits = 8
        Try
            If ToolStripComboBox1.SelectedItem.ToString Is Nothing Then
                MsgBox("Select a valid COM port", MsgBoxStyle.Critical)
            ElseIf ToolStripComboBox1.SelectedItem.ToString <> Nothing Then
                SerialPort1.PortName = ToolStripComboBox1.SelectedItem.ToString
            End If
        Catch ex As Exception
            MsgBox("Select a valid COM port", MsgBoxStyle.Critical)
        End Try

        SerialPort1.Open()


    End Sub
    Private Sub PotTimer_Tick(sender As Object, e As EventArgs) Handles PotTimer.Tick
        Dim data(0) As Byte 'put bytes into array
        Dim writeData(0) As Byte 'put bytes into array
        Dim wait%
        Dim aWhile1$ = "0"
        Dim aWhile$ = "0"
        Dim Value(3) As Integer
        Dim newX%
        Dim newY%
        Static oldX, oldY As Integer

        'y position
        data(0) = &H51 'actual data as byte
        SerialPort1.Write(data, 0, 1)

        Do Until aWhile IsNot "0" Or aWhile1 IsNot "0"
            Dim ADC(SerialPort1.BytesToRead) As Byte
            Try
                SerialPort1.Read(ADC, 0, SerialPort1.BytesToRead)
                aWhile = CStr(ADC(1))
                aWhile1 = CStr(ADC(0))
                Value(0) = ADC(0)
                Value(1) = ADC(1)
            Catch ex As Exception

            End Try
            wait += 1
            If wait = 1000 Then
                newY = CInt(0)
                Exit Do
            End If
        Loop

        Try
            newY = CInt((Value(0) * 4) + (Value(1) / 64))
        Catch ex As Exception
            MsgBox("Bad Data.")
        End Try
        aWhile = "0"
        aWhile1 = "0"
        'x position
        data(0) = &H52 'actual data as byte
        SerialPort1.Write(data, 0, 1)

        Do Until aWhile IsNot "0" Or aWhile1 IsNot "0"
            Dim ADC2(SerialPort1.BytesToRead) As Byte
            Try
                SerialPort1.Read(ADC2, 0, SerialPort1.BytesToRead)
                aWhile = CStr(ADC2(1))
                aWhile1 = CStr(ADC2(0))
                Value(2) = ADC2(0)
                Value(3) = ADC2(1)
            Catch ex As Exception

            End Try
            wait += 1
            If wait = 1000 Then
                newX = CInt(0)
                Exit Do
            End If
        Loop

        Try
            newX = CInt((Value(2) * 4) + (Value(3) / 64))
            Me.Text = CStr($"{newX},{newY}")

            DrawWithPot(oldX, oldY, newX, newY)
            oldX = newX
            oldY = newY
        Catch ex As Exception
            MsgBox("Bad Data.")
        End Try




    End Sub
    Sub DrawWithPot(oldX As Integer, oldY As Integer, newX As Integer, newY As Integer)
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(ForeGround)
        pen.Width = 5
        g.DrawLine(pen, CInt(oldX * (DrawingPictureBox.Width / 1014)), CInt(oldY * (DrawingPictureBox.Height / 1014)), CInt(newX * (DrawingPictureBox.Width / 1014)), CInt(newY * (DrawingPictureBox.Height / 1014)))
        g.Dispose()
    End Sub


    Private Sub MouseRadioButton_Click(sender As Object, e As EventArgs) Handles MouseRadioButton.Click
        If MouseRadioButton.Checked = True Then
            PotTimer.Stop()
        Else
            PotTimer.Start()
        End If
    End Sub

    Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButton2.Click
        If MouseRadioButton.Checked = True Then
            PotTimer.Stop()
        Else
            PotTimer.Start()
        End If
    End Sub
End Class
