'解析VB.Net界面生成Tkinter的Python代码
Imports System.Text.RegularExpressions
Module GUI2Py
    Dim code As String = ""
    Dim pattern As String = "System\.Windows\.Forms\.(.+?),"

    Dim ctrlName() As String = {"Button", "Label", "TextBox", "Radio", "CheckBox", "PictureBox", "ListBox", "ComboBox", "ProcessBar", "TrackBar",
            "ListView", "TreeView", "Canvas", "GroupBox", "Frame", "TabControl"}

    Public Sub write(Optional str As String = "", Optional space As Int16 = 0, Optional end_ As String = vbCrLf)
        If space > 0 Then
            For i = 1 To space
                code += " "
            Next
        End If
        code += (str + end_)
    End Sub
    Public Function parse2root(form As Form, Optional outputFile As String = "") As String
        write("import tkinter as tk")
        write("import tkinter.ttk as ttk")
        write("import tkinter.font as tkFont")
        write()
        write("root = tk.Tk()")
        write()
        write("### 界面设计部分 ###")
        write()
        write(String.Format("root.geometry('{0}x{1}+{2}+{3}')", form.Width, form.Height, form.Location.X, form.Location.Y))
        write(String.Format("root.title('{0}')", form.Text))

        For i = 0 To form.Controls.Count - 1
            Dim ctrl As Control = form.Controls.Item(i)
            Dim theCtrl As String = Regex.Match(form.Controls.Item(i).ToString(), pattern).Groups(1).Value.ToString
            Dim id As String = ctrl.Name
            Dim x As Int32 = ctrl.Left
            Dim y As Int32 = ctrl.Top
            Dim w As Int32 = ctrl.Width
            Dim h As Int32 = ctrl.Height
            Dim text As String = ctrl.Text
            Dim hide As Boolean = Not ctrl.Visible
            Dim disabled As Boolean = Not ctrl.Enabled
            Dim bgcolor As Color = ctrl.BackColor
            Dim br As String = Hex(bgcolor.R)
            Dim bg As String = Hex(bgcolor.G)
            Dim bb As String = Hex(bgcolor.B)
            Dim frcolor As Color = ctrl.ForeColor
            Dim fr As String = Hex(frcolor.R)
            Dim fg As String = Hex(frcolor.G)
            Dim fb As String = Hex(frcolor.B)
            If Len(br) < 2 Then
                br = "0" & br
            End If
            If Len(bg) < 2 Then
                bg = "0" & bg
            End If
            If Len(bb) < 2 Then
                bb = "0" & bb
            End If
            If Len(fr) < 2 Then
                fr = "0" & fr
            End If
            If Len(fg) < 2 Then
                fg = "0" & fg
            End If
            If Len(fb) < 2 Then
                fb = "0" & fb
            End If
            Dim font As Font = ctrl.Font
            Dim fontFamily As String = "family='" & font.FontFamily.Name & "'"
            Dim fontSize As String = "size=" & CStr(CInt(font.Size))
            Dim fontUnderline As String
            If font.Underline Then
                fontUnderline = "underline=True"
            Else
                fontUnderline = "underline=False"
            End If
            Dim fontItalic As String
            If font.Italic Then
                fontItalic = "slant='italic'"
            Else
                fontItalic = "slant='roman'"
            End If
            Dim fontBold As String
            If font.Bold Then
                fontBold = "weight='bold'"
            Else
                fontBold = "weight='normal'"
            End If
            Dim theFont As String = String.Join(", ", {fontFamily, fontSize, fontBold, fontItalic, fontUnderline})

            Select Case theCtrl
                Case "Button"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write(String.Format("{0} = ttk.Button({1}_frame, text='{2}')", id, id, text))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "Label"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write(String.Format("{0} = ttk.Label({1}_frame, text='{2}')", id, id, text))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    write(String.Format("{0}_font = tkFont.Font({1})", id, theFont))
                    write(String.Format("{0}.configure(font={1}_font)", id, id))
                    write(String.Format("{0}.config(background='#{1}{2}{3}')", id, br, bg, bb))
                    write(String.Format("{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb))
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "TextBox"
                    write(String.Format("{0}Var = tk.StringVar(value='{1}')", id, text))
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write("#单行文本")
                    write(String.Format("{0} = ttk.Entry({1}_frame, textvariable={2}Var)", id, id, id))
                    write("#多行文本")
                    write(String.Format("#{0} = tk.Text({1}_frame)", id, id))
                    write(String.Format("#{0}.insert('end', '{1}')", id, text))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    write("#显示密码*")
                    write(String.Format("#{0}.configure(show='*')", id))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write("#垂直滚动条")
                    write(String.Format("#{0}_vscroll = tk.Scrollbar({1}, orient='vertical', command={2}.yview)", id, id, id))
                    write(String.Format("#{0}.configure(yscrollcommand={1}_vscroll.set)", id, id))
                    write(String.Format("#{0}_vscroll.pack(side='right', fill='y')", id))
                    write("#水平滚动条")
                    write(String.Format("#{0}_hscroll = tk.Scrollbar({1}, orient='horizontal', command={2}.xview)", id, id, id))
                    write(String.Format("#{0}.configure(xscrollcommand={1}_hscroll.set)", id, id))
                    write(String.Format("#{0}_hscroll.pack(side='bottom', fill='x')", id))
                    write(String.Format("{0}_font = tkFont.Font({1})", id, theFont))
                    write(String.Format("{0}.configure(font={1}_font)", id, id))
                    write(String.Format("{0}.config(background='#{1}{2}{3}')", id, br, bg, bb))
                    write(String.Format("{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb))
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))


            End Select

        Next
        write()
        write()
        write("### 功能逻辑部分 ###")
        write()
        write()
        write("root.mainloop()")

        If Len(outputFile) < 1 Then
            Return code
        Else
            Try
                Dim filewriter = System.IO.File.CreateText(outputFile)
                filewriter.Write(code)
                filewriter.Flush()
                filewriter.Close()
                Return vbTrue
            Catch e As Exception
                Return vbFalse
            End Try
        End If
    End Function
End Module
