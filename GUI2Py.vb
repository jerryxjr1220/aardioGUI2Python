'解析VB.Net界面生成Tkinter的Python代码
Imports System.Text.RegularExpressions
Imports System.IO
Module GUI2Py
    Dim code As String = ""
    Dim pattern As String = "System\.Windows\.Forms\.(.+?),"

    Dim ctrlName() As String = {"Button", "Label", "TextBox", "RadioButton", "CheckBox", "PictureBox", "ListBox", "ComboBox", "ProcessBar", "TrackBar",
            "ListView", "TreeView", "Canvas", "GroupBox", "Panel", "TabControl"}

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

                Case "RadioButton"
                    write("#同一组RadioButton使用同一个RadioSelect")
                    write(String.Format("#RadioSelect1 = tk.IntVar()"))
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write("#根据选项自行修改value值")
                    write(String.Format("{0} = ttk.Radiobutton({1}_frame, text='{2}', value=0, variable=RadioSelect1)", id, id, text))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "CheckBox"
                    write(String.Format("{0}_CheckSelect = tk.IntVar()", id))
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write(String.Format("{0} = ttk.Checkbutton({1}_frame, text='{2}', variable={3}_CheckSelect)", id, id, text, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "PictureBox"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write("#根据需要自行修改图片路径")
                    write(String.Format("#{0}_img = tk.PhotoImage(file=r'## IMAGE PATH ##')", id))
                    write(String.Format("#{0} = ttk.Label({1}_frame, image={2}_img)", id, id, id))
                    write(String.Format("{0} = ttk.Label({1}_frame)", id, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}.config(background='#{1}{2}{3}')", id, br, bg, bb))
                    write(String.Format("{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb))
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "Canvas"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write(String.Format("{0} = ttk.Label({1}_frame)", id, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
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
                    write(String.Format("{0}.config(background='#{1}{2}{3}')", id, br, bg, bb))
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "ListBox"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write("#根据需要自行添加Item")
                    write(String.Format("#{0}_items = tk.Variable(value=[## ADD ITEMS ##])", id))
                    write(String.Format("#{0} = tk.Listbox({1}_frame, listvariable={2}_items)", id, id, id))
                    write(String.Format("{0} = tk.Listbox({1}_frame)", id, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
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

                Case "ComboBox"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write("#根据需要自行添加Item")
                    write(String.Format("#{0} = ttk.Combobox({1}_frame, value=[## ADD ITEMS ##])", id, id))
                    write(String.Format("{0} = ttk.Combobox({1}_frame)", id, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_font = tkFont.Font({1})", id, theFont))
                    write(String.Format("{0}.configure(font={1}_font)", id, id))
                    write(String.Format("{0}.config(background='#{1}{2}{3}')", id, br, bg, bb))
                    write(String.Format("{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb))
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "ListView"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write(String.Format("{0} = ttk.Treeview({1}_frame)", id, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "ProgressBar"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write("#根据需要自行修改进度和最大值")
                    write(String.Format("{0} = ttk.Progressbar({1}_frame, value=0, maximum=100)", id, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "TrackBar"
                    write(String.Format("{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h))
                    write("#根据需要自行修改进度和最大最小值")
                    write(String.Format("{0} = ttk.Scale({1}_frame, from_=0, to=100, value=0)", id, id))
                    write(String.Format("{0}.place(x=0, y=0, width={1}, height={2})", id, w, h))
                    If disabled Then
                        write(String.Format("{0}.configure(state='disabled')", id))
                    End If
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_frame.place(x={1}, y={2})", id, x, y))

                Case "GroupBox"
                    write(String.Format("{0} = ttk.Labelframe(text={1},width={2}, height={3})", id, text, w, h))
                    write(String.Format("{0}.place(x={1}, y={2}, width={3}, height={4})", id, x, y, w, h))
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_font = tkFont.Font({1})", id, theFont))
                    write(String.Format("{0}.configure(font={1}_font)", id, id))

                Case "Panel"
                    write(String.Format("{0} = ttk.Frame(width={1}, height={2})", id, w, h))
                    write(String.Format("{0}.place(x={1}, y={2}, width={3}, height={4})", id, x, y, w, h))
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If

                Case "TabControl"
                    write(String.Format("{0} = ttk.Notebook(width={1}, height={2})", id, w, h))
                    write(String.Format("{0}.place(x={1}, y={2}, width={3}, height={4})", id, x, y, w, h))
                    If hide Then
                        write(String.Format("{0}.place_forget()", id))
                    End If
                    write(String.Format("{0}_font = tkFont.Font({1})", id, theFont))
                    write(String.Format("{0}.configure(font={1}_font)", id, id))

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

    Public Function parse2place(form As Form, Optional name As String = "SubAssembly", Optional outputFile As String = "") As String
        write("import tkinter as tk")
        write("import tkinter.ttk as ttk")
        write("import tkinter.font as tkFont")
        write()
        write(String.Format("class {0}():", name))
        write()
        write("### 界面设计部分 ###", 4)
        write()
        write("def __init__(self, master):", 4)
        write(String.Format("self.mainframe = ttk.Frame(master, width={0}, height={1})", form.Width, form.Height), 8)

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
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write(String.Format("self.{0} = ttk.Button(self.{1}_frame, text='{2}')", id, id, text), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "Label"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write(String.Format("self.{0} = ttk.Label(self.{1}_frame, text='{2}')", id, id, text), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    write(String.Format("self.{0}_font = tkFont.Font({1})", id, theFont), 8)
                    write(String.Format("self.{0}.configure(font=self.{1}_font)", id, id), 8)
                    write(String.Format("self.{0}.config(background='#{1}{2}{3}')", id, br, bg, bb), 8)
                    write(String.Format("self.{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb), 8)
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "TextBox"
                    write(String.Format("self.{0}Var = tk.StringVar(value='{1}')", id, text), 8)
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write("#单行文本", 8)
                    write(String.Format("self.{0} = ttk.Entry(self.{1}_frame, textvariable={2}Var)", id, id, id), 8)
                    write("#多行文本", 8)
                    write(String.Format("#self.{0} = tk.Text(self.{1}_frame)", id, id), 8)
                    write(String.Format("#self.{0}.insert('end', '{1}')", id, text), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    write("#显示密码*", 8)
                    write(String.Format("#self.{0}.configure(show='*')", id), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write("#垂直滚动条", 8)
                    write(String.Format("#self.{0}_vscroll = tk.Scrollbar(self.{1}, orient='vertical', command=self.{2}.yview)", id, id, id), 8)
                    write(String.Format("#self.{0}.configure(yscrollcommand=self.{1}_vscroll.set)", id, id), 8)
                    write(String.Format("#self.{0}_vscroll.pack(side='right', fill='y')", id), 8)
                    write("#水平滚动条", 8)
                    write(String.Format("#self.{0}_hscroll = tk.Scrollbar(self.{1}, orient='horizontal', command=self.{2}.xview)", id, id, id), 8)
                    write(String.Format("#self.{0}.configure(xscrollcommand=self.{1}_hscroll.set)", id, id), 8)
                    write(String.Format("#self.{0}_hscroll.pack(side='bottom', fill='x')", id), 8)
                    write(String.Format("self.{0}_font = tkFont.Font(self.{1})", id, theFont), 8)
                    write(String.Format("self.{0}.configure(font=self.{1}_font)", id, id), 8)
                    write(String.Format("self.{0}.config(background='#{1}{2}{3}')", id, br, bg, bb), 8)
                    write(String.Format("self.{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb), 8)
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "RadioButton"
                    write("#同一组RadioButton使用同一个RadioSelect", 8)
                    write(String.Format("#self.RadioSelect1 = tk.IntVar()"), 8)
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write("#根据选项自行修改value值", 8)
                    write(String.Format("self.{0} = ttk.Radiobutton(self.{1}_frame, text='{2}', value=0, variable=self.RadioSelect1)", id, id, text), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "CheckBox"
                    write(String.Format("self.{0}_CheckSelect = tk.IntVar()", id), 8)
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write(String.Format("self.{0} = ttk.Checkbutton(self.{1}_frame, text='{2}', variable=self.{3}_CheckSelect)", id, id, text, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "PictureBox"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write("#根据需要自行修改图片路径", 8)
                    write(String.Format("#self.{0}_img = tk.PhotoImage(file=r'## IMAGE PATH ##')", id), 8)
                    write(String.Format("#self.{0} = ttk.Label(self.{1}_frame, image={2}_img)", id, id, id), 8)
                    write(String.Format("self.{0} = ttk.Label(self.{1}_frame)", id, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}.config(background='#{1}{2}{3}')", id, br, bg, bb), 8)
                    write(String.Format("self.{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb), 8)
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "Canvas"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write(String.Format("self.{0} = ttk.Label(self.{1}_frame)", id, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id))
                    End If
                    write("#垂直滚动条", 8)
                    write(String.Format("#self.{0}_vscroll = tk.Scrollbar({1}, orient='vertical', command=self.{2}.yview)", id, id, id), 8)
                    write(String.Format("#self.{0}.configure(yscrollcommand=self.{1}_vscroll.set)", id, id), 8)
                    write(String.Format("#self.{0}_vscroll.pack(side='right', fill='y')", id), 8)
                    write("#水平滚动条", 8)
                    write(String.Format("#self.{0}_hscroll = tk.Scrollbar({1}, orient='horizontal', command=self.{2}.xview)", id, id, id), 8)
                    write(String.Format("#self.{0}.configure(xscrollcommand=self.{1}_hscroll.set)", id, id), 8)
                    write(String.Format("#self.{0}_hscroll.pack(side='bottom', fill='x')", id), 8)
                    write(String.Format("self.{0}.config(background='#{1}{2}{3}')", id, br, bg, bb), 8)
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "ListBox"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write("#根据需要自行添加Item", 8)
                    write(String.Format("#self.{0}_items = tk.Variable(value=[## ADD ITEMS ##])", id), 8)
                    write(String.Format("#self.{0} = tk.Listbox(self.{1}_frame, listvariable={2}_items)", id, id, id), 8)
                    write(String.Format("self.{0} = tk.Listbox(self.{1}_frame)", id, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write("#垂直滚动条", 8)
                    write(String.Format("#self.{0}_vscroll = tk.Scrollbar({1}, orient='vertical', command=self.{2}.yview)", id, id, id), 8)
                    write(String.Format("#self.{0}.configure(yscrollcommand=self.{1}_vscroll.set)", id, id), 8)
                    write(String.Format("#self.{0}_vscroll.pack(side='right', fill='y')", id), 8)
                    write("#水平滚动条", 8)
                    write(String.Format("#self.{0}_hscroll = tk.Scrollbar({1}, orient='horizontal', command=self.{2}.xview)", id, id, id), 8)
                    write(String.Format("#self.{0}.configure(xscrollcommand=self.{1}_hscroll.set)", id, id), 8)
                    write(String.Format("#self.{0}_hscroll.pack(side='bottom', fill='x')", id), 8)
                    write(String.Format("self.{0}_font = tkFont.Font({1})", id, theFont), 8)
                    write(String.Format("self.{0}.configure(font=self.{1}_font)", id, id), 8)
                    write(String.Format("self.{0}.config(background='#{1}{2}{3}')", id, br, bg, bb), 8)
                    write(String.Format("self.{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb), 8)
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "ComboBox"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write("#根据需要自行添加Item", 8)
                    write(String.Format("#self.{0} = ttk.Combobox(self.{1}_frame, value=[## ADD ITEMS ##])", id, id), 8)
                    write(String.Format("self.{0} = ttk.Combobox(self.{1}_frame)", id, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_font = tkFont.Font({1})", id, theFont), 8)
                    write(String.Format("self.{0}.configure(font=self.{1}_font)", id, id), 8)
                    write(String.Format("self.{0}.config(background='#{1}{2}{3}')", id, br, bg, bb), 8)
                    write(String.Format("self.{0}.config(foreground='#{1}{2}{3}')", id, fr, fg, fb), 8)
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "ListView"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write(String.Format("self.{0} = ttk.Treeview(self.{1}_frame)", id, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "ProgressBar"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write("#根据需要自行修改进度和最大值", 8)
                    write(String.Format("self.{0} = ttk.Progressbar(self.{1}_frame, value=0, maximum=100)", id, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "TrackBar"
                    write(String.Format("self.{0}_frame = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write("#根据需要自行修改进度和最大最小值", 8)
                    write(String.Format("self.{0} = ttk.Scale(self.{1}_frame, from_=0, to=100, value=0)", id, id), 8)
                    write(String.Format("self.{0}.place(x=0, y=0, width={1}, height={2})", id, w, h), 8)
                    If disabled Then
                        write(String.Format("self.{0}.configure(state='disabled')", id), 8)
                    End If
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_frame.place(x={1}, y={2})", id, x, y), 8)

                Case "GroupBox"
                    write(String.Format("self.{0} = ttk.Labelframe(text={1},width={2}, height={3})", id, text, w, h), 8)
                    write(String.Format("self.{0}.place(x={1}, y={2}, width={3}, height={4})", id, x, y, w, h), 8)
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_font = tkFont.Font({1})", id, theFont), 8)
                    write(String.Format("self.{0}.configure(font=self.{1}_font)", id, id), 8)

                Case "Panel"
                    write(String.Format("self.{0} = ttk.Frame(width={1}, height={2})", id, w, h), 8)
                    write(String.Format("self.{0}.place(x={1}, y={2}, width={3}, height={4})", id, x, y, w, h), 8)
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If

                Case "TabControl"
                    write(String.Format("self.{0} = ttk.Notebook(width={1}, height={2})", id, w, h), 8)
                    write(String.Format("self.{0}.place(x={1}, y={2}, width={3}, height={4})", id, x, y, w, h), 8)
                    If hide Then
                        write(String.Format("self.{0}.place_forget()", id), 8)
                    End If
                    write(String.Format("self.{0}_font = tkFont.Font({1})", id, theFont), 8)
                    write(String.Format("self.{0}.configure(font=self.{1}_font)", id, id), 8)

            End Select

        Next
        write("self.mainframe.pack()", 8)
        write()
        write("### 功能逻辑部分 ###", 4)
        write()
        write()
        write()
        write("root = tk.Tk()")
        write(String.Format("sa = {0}(root)", name))
        write("root.mainloop()")


        If Len(outputFile) < 1 Then
            Return code
        Else
            Try
                Dim filewriter = File.CreateText(outputFile)
                filewriter.Write(code)
                filewriter.Close()
                Return CStr(vbTrue)
            Catch
                Return CStr(vbFalse)
            End Try
        End If
    End Function

End Module
