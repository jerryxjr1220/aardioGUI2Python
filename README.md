# GUI2Python for VB.Net


GUI2Python Lib - use VB.Net to create UI of Tkinter and generate Python code. - *As a Tkinter Designer*

>TKinter is a native Python Lib to easily create GUI of Python program，but there is no visual interface to use. Not friendly at all!

Not too complicated, just like a "Qt Designer".


---

GUI2Python库 - 用VB.Net画Tkinter界面并生成Python代码 - *As a Tkinter Designer*

>TKinter是Python的原生GUI库，用来创建一些简单的GUI界面还是很方便的。但是，缺点是没有提供一套图形化的设计界面，这样创建界面的时候就非常不直观。

思路是搞一个类似QtDesigner类似的工具，但是又不想搞得太复杂。


---

- 视频教程：  [https://space.bilibili.com/382613220/channel/seriesdetail?sid=2503337](https://space.bilibili.com/382613220/channel/seriesdetail?sid=2503337)


## GUI2Py  VB.Net版本发布

- VB.Net 可以自动识别控件名称，这样就不需要通过Z序去对应了，直接可以使用自定义的控件名称
- VB.Net 没有Canvas组件，如有需要可以通过其他组件改写（例如，Panel/Frame控件)
- VB.Net 的有Treeview组件，直接对应Tkinter的Treeview

## 目前可以实现的功能：

- 20220810更新：
1. 修正一处错误：radiobutton和checkbox不支持justify参数，也不支持font参数
2. radiobutton添加value参数，配合variable参数控制选项，详见Test实例
3. 添加一个新的Test实例“Python习题集”

- 20220809更新：
1. 优化代码，用格式化字符串替换字符串拼接，增强可读性。
2. 精简布局，移除transfer2pack和transfer2grid方法（有需要可以自行更改transfer2place的最后一行布局代码），仅保留transfer2root和transfer2place。
3. 详见GUI2Py_simple.aardio，下载后改名替换原有GUI2Py.aardio

- 20220807更新：
1. 优化代码增强可读性，功能不变。

- 20220731更新：
1. 增加了一个实例：光学测量仪，源码在Measurement文件夹中。demo1.py和demo2.py的界面设计都是在aardio中完成并自动生成的，在添加逻辑功能后，放到主程序界面运行。

- 20220728更新：
1. 标签、文本框等有字符布局的控件新增左对齐、居中、右对齐布局
2. 多行文本框、画板、Listbox增加水平和垂直滚动条
3. 新增translateName函数，解析窗体设计器代码，返回的Z序与对应控件名称，可用来替换python中的控件名
>由于遍历控件时each返回的控件顺序是随机的，所以控件名和序号不好对应。但由于控件的Z序是固定的，所以新增了translateName函数，用于解析窗体设计器代码，返回的Z序与对应控件名称，可用来在python中替换控件名（可选功能）

- 20220727更新：

1. 新增字体属性设置，可按照aardio中的字体自动设置到Tkinter中
2. 修改transfer2assembly名称 为 transfer2place，更好地对应place方法

- 20220725更新：

1. 所有可禁用控件，增加禁用属性
2. 文本框增加密码属性
3. 增加pack布局
4. 增加grid布局

- 20220724更新：

1. 基本控件都已经完善，包括Label, Button, Text/Entry, RadioButton, CheckBox, PictureBox, Canvas, ListBox, ComboBox, Treeview, ProgressBar, Scale, LabelFrame, Frame, Notebook等，基本满足常规使用。其余Tkinter支持的控件也可以通过手动方式添加使用。
2. 可以把界面直接转换为主界面
3. 更新组件化界面功能，这样可以像aardio一样把不同的界面拆成单独的组件，然后在Tabs高级选项卡（python中对应的就是Notebook）进行调用。也可以嵌入到其他Frame、Notebook、LabelFrame等容器中，当然也可以是主窗口Tk容器。
4. 新增了“隐藏”属性支持和背景色、前景色设置支持（仅对标签、文本框等控件有效）
5. 新增Canvas控件，对应aardio的plus

## Quick Start

1. 安装
直接把下载的GUI2Py.vb文件复制到VB.Net项目文件夹下即可。

2. 在VB.Net中画界面

3. 调用
```VB
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox(GUI2Py.parse2place(Me, "SA", "C:\Users\xujr1\Documents\PythonProjects\demo.py"))
    End Sub
End Class

```

现在生成的code就是我们所需要的python代码，并自动保存为py文件。

4. python代码
```python
import tkinter as tk
import tkinter.ttk as ttk
import tkinter.font as tkFont

class SA():

    ### 全局变量 ###


    ### 界面设计部分 ###

    def __init__(self, master):
        self.mainframe = ttk.Frame(master, width=816, height=489)
        self.GroupBox1 = ttk.Labelframe(text='GroupBox1',width=302, height=189)
        self.GroupBox1.place(x=71, y=241, width=302, height=189)
        self.TabControl1 = ttk.Notebook(width=307, height=276)
        self.TabControl1.place(x=402, y=48, width=307, height=276)
        self.Panel1 = ttk.Frame(width=286, height=152)
        self.Panel1.place(x=78, y=56, width=286, height=152)
        self.Button1_frame = ttk.Frame(width=213, height=61)
        self.Button1 = ttk.Button(self.Button1_frame, text='Button1')
        self.Button1.place(x=0, y=0, width=213, height=61)
        self.Button1_frame.place(x=575, y=377)
        self.mainframe.pack()

    ### 功能逻辑部分 ###



root = tk.Tk()
sa = SA(root)
root.mainloop()

```
GUI2Py库可以转换大部分的常用控件至Python的Tkinter中，界面可以直接运行。当然“功能逻辑部分”需要自行添加，可以实现预想的功能。

### 进阶使用
- 参考aardio分支中的实例，用法基本相同。
        
### 应用实例
- aardio创建界面，用matplotlib画图，实时动态显示在Tkinter中

![https://www.htmlayout.cn/upload/image/20220726/1658814090451434.gif](https://www.htmlayout.cn/upload/image/20220726/1658814090451434.gif)

```python
import tkinter as tk
import tkinter.ttk as ttk
import matplotlib.pyplot as plt
import numpy as np
from io import BytesIO
from PIL import Image, ImageTk
   
class SA():
    ### 界面设计部分 ###
     
    def __init__(self, master):
        self.mainframe = ttk.Frame(master, width=601, height=390)
        self.label1_frame = ttk.Frame(self.mainframe, width=209, height=27)
        self.label1 = ttk.Label(self.label1_frame, text="y = sin(x) / log(x)")
        self.label1.place(x=0, y=0)
        self.label1_frame.pack()
        self.pic1_frame = ttk.Frame(self.mainframe, width=640, height=480)
        self.pic1 = ttk.Label(self.pic1_frame)
        self.pic1.place(x=0, y=0)
        self.pic1_frame.pack()
        self.scale1_frame = ttk.Frame(self.mainframe, width=529, height=30)
        self.scale1 = ttk.Scale(self.scale1_frame, from_=21, to=100, value=21, command=self.drawImage)
        self.scale1.place(x=0, y=0)
        self.scale1_frame.pack()
        self.label2_frame = ttk.Frame(self.mainframe, width=209, height=27)
        self.label2_str = tk.StringVar(value='start point: x = 2.1')
        self.label2 = ttk.Label(self.label2_frame, textvariable=self.label2_str)
        self.label2.place(x=0, y=0)
        self.label2_frame.pack()
        self.mainframe.pack()
 
    ### 功能逻辑部分 ###
    def drawImage(self, *args):
        start = self.scale1.get()
        x = np.linspace(start, start+50, 50)
        x = x / 10
        self.label2_str.set('start point: x = {0}'.format(start/10))
        y = np.sin(x) / np.log(x)
        plt.clf()
        plt.plot(x, y, 'b')
        buff = BytesIO()
        plt.savefig(buff)
        buff.seek(0)
        im = Image.open(buff)
        image = ImageTk.PhotoImage(im)
        self.pic1.image = image
        self.pic1.configure(image=image)
 
 
 
         
root = tk.Tk()
 
sa = SA(root)
sa.drawImage()
root.mainloop()
```

