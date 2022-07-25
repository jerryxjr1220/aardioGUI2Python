# aardioGUI2Python
GUI2Python库 - 用aardio画Tkinter界面并生成Python代码 - *As a Tkinter Designer*

>TKinter是Python的原生GUI库，用来创建一些简单的GUI界面还是很方便的。但是，缺点是没有提供一套图形化的设计界面，这样创建界面的时候就非常不直观。

思路是搞一个类似QtDesigner类似的工具，但是又不想搞得太复杂。

>aardio的图形化设计非常便捷，这样在aardio里画好界面自动生成tkinter的python代码就是再好不过的事情了。

* aardio 可以至 [https://www.aardio.com/](https://www.aardio.com/) 下载最新版，可免费下载使用。

## 目前可以实现的功能：

- 20220724更新：

1. 基本控件都已经完善，包括Label, Button, Text/Entry, RadioButton, CheckBox, PictureBox, Canvas, ListBox, ComboBox, Treeview, ProgressBar, Scale, LabelFrame, Frame, Notebook等，基本满足常规使用。其余Tkinter支持的控件也可以通过手动方式添加使用。
2. 可以把界面直接转换为主界面
3. 更新组件化界面功能，这样可以像aardio一样把不同的界面拆成单独的组件，然后在Tabs高级选项卡（python中对应的就是Notebook）进行调用。也可以嵌入到其他Frame、Notebook、LabelFrame等容器中，当然也可以是主窗口Tk容器。
4. 新增了“隐藏”属性支持和背景色、前景色设置支持（仅对标签、文本框等控件有效）
5. 新增Canvas控件，对应aardio的plus

## Quick Start

1. 在aardio中画界面
```aardio
import win.ui;
/*DSG{{*/
mainForm = win.form(text="Window Title";right=596;bottom=383)
mainForm.add(
button2={cls="button";text="Button";left=242;top=261;right=369;bottom=293;z=3};
edit2={cls="edit";text="Text";left=217;top=117;right=399;bottom=234;edge=1;multiline=1;z=2};
static2={cls="static";text="Hello, Python!";left=218;top=74;right=373;bottom=103;transparent=1;z=1}
)
/*}}*/

mainForm.show();
return win.loopMessage();
```

2. 调用
```aardio
import GUI2Py;
g2t = GUI2Py.GUI2Tk(mainForm);
code = g2t.transfer2root();
```
生成的code就是我们所需要的python代码，可以直接复制到python运行。

3. python代码
```python
import tkinter as tk
import tkinter.ttk as ttk
   
root = tk.Tk()
   
### 界面设计部分 ###
   
root.geometry("601x390")
root.title("Window Title")
button1_frame = ttk.Frame(width=127, height=32)
button1 = ttk.Button(button1_frame, text="Button")
button1.place(x=0, y=0)
button1_frame.place(x=242, y=261)
label1_frame = ttk.Frame(width=155, height=29)
label1 = ttk.Label(label1_frame, text="Hello, Python!")
label1.place(x=0, y=0)
label1_frame.place(x=218, y=74)
editVar1 = tk.StringVar(value='Text')
edit1_frame = ttk.Frame(width=182, height=117)
edit1 = tk.Text(edit1_frame)
edit1.insert("end", "Text")
edit1.place(x=0, y=0)
edit1_frame.place(x=217, y=117)


### 功能逻辑部分 ###

root.mainloop()
```
aardioGUI2Python库可以转换大部分的常用控件至Python的Tkinter中，界面可以直接运行。当然“功能逻辑部分”需要自行添加，可以实现预想的功能。

## 进阶应用

把界面组件化，这样可以像aardio一样把不同的界面拆成单独的组件，然后在Tabs高级选项卡（python中对应的就是Notebook）进行调用。也可以嵌入到其他Frame、Notebook、LabelFrame等容器中，当然也可以是主窗口Tk容器。
```aardio
import GUI2Py;
g2t = GUI2Py.GUI2Tk(mainForm);
code = g2t.transfer2assembly();
```

生成的python代码
```python
import tkinter as tk
import tkinter.ttk as ttk
  
class SubAssembly():
    ### 界面设计部分 ###
    
    def __init__(self, master):
        self.mainframe = ttk.Frame(master, width=601, height=390)
        self.label1_frame = ttk.Frame(self.mainframe, width=155, height=29)
        self.label1 = ttk.Label(self.label1_frame, text="Hello, Python!")
        self.label1.place(x=0, y=0)
        self.label1_frame.place(x=218, y=74)
        self.editVar1 = tk.StringVar(value='Text')
        self.edit1_frame = ttk.Frame(self.mainframe, width=182, height=117)
        self.edit1 = tk.Text(self.edit1_frame)
        self.edit1.insert("end", "Text")
        self.edit1.place(x=0, y=0)
        self.edit1_frame.place(x=217, y=117)
        self.button1_frame = ttk.Frame(self.mainframe, width=127, height=32)
        self.button1 = ttk.Button(self.button1_frame, text="Button")
        self.button1.place(x=0, y=0)
        self.button1_frame.place(x=242, y=261)
        self.mainframe.pack()

    ### 功能逻辑部分 ###
```

在python中调用，只需要实例化即可，当然如果需要实现额外功能，需要自行添加功能代码。
```python
root = tk.Tk()
sa = SubAssembly(root)
root.mainloop()
```
        
