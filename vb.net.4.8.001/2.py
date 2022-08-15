import tkinter as tk
import tkinter.ttk as ttk
import tkinter.font as tkFont
from api import parseApi
import webbrowser

class SubAssembly():

    ### 全局变量 ###


    ### 界面设计部分 ###

    def __init__(self, master):
        self.mainframe = ttk.Frame(master, width=669, height=308)
        self.Label2_frame = ttk.Frame(width=88, height=26)
        self.Label2 = ttk.Label(self.Label2_frame, text='视频地址')
        self.Label2.place(x=0, y=0, width=88, height=26)
        self.Label2_font = tkFont.Font(family='微软雅黑', size=14, weight='bold', slant='roman', underline=True)
        self.Label2.configure(font=self.Label2_font)
        self.Label2.config(background='#F0F0F0')
        self.Label2.config(foreground='#00008B')
        self.Label2_frame.place(x=123, y=102)
        self.cbbApi_frame = ttk.Frame(width=291, height=33)
        #根据需要自行添加Item
        self.cbbApi = ttk.Combobox(self.cbbApi_frame, values=["主接口（推荐）", "接口二", "接口三", "接口四", "接口五", "接口六",
                                                             "接口七", "接口八", "接口九", "接口十"])
        #self.cbbApi = ttk.Combobox(self.cbbApi_frame)
        self.cbbApi.place(x=0, y=0, width=291, height=33)
        self.cbbApi_font = tkFont.Font(family='微软雅黑', size=14, weight='normal', slant='roman', underline=False)
        self.cbbApi.configure(font=self.cbbApi_font)
        self.cbbApi.config(background='#FFFFFF')
        self.cbbApi.config(foreground='#000000')
        self.cbbApi_frame.place(x=229, y=40)
        self.tbUrlVar = tk.StringVar(value='')
        self.tbUrl_frame = ttk.Frame(width=291, height=33)
        #单行文本
        self.tbUrl = ttk.Entry(self.tbUrl_frame, textvariable=self.tbUrlVar)
        #多行文本
        #self.tbUrl = tk.Text(self.tbUrl_frame)
        #self.tbUrl.insert('end', '')
        self.tbUrl.place(x=0, y=0, width=291, height=33)
        #显示密码*
        #self.tbUrl.configure(show='*')
        #垂直滚动条
        #self.tbUrl_vscroll = tk.Scrollbar(self.tbUrl, orient='vertical', command=self.tbUrl.yview)
        #self.tbUrl.configure(yscrollcommand=self.tbUrl_vscroll.set)
        #self.tbUrl_vscroll.pack(side='right', fill='y')
        #水平滚动条
        #self.tbUrl_hscroll = tk.Scrollbar(self.tbUrl, orient='horizontal', command=self.tbUrl.xview)
        #self.tbUrl.configure(xscrollcommand=self.tbUrl_hscroll.set)
        #self.tbUrl_hscroll.pack(side='bottom', fill='x')
        self.tbUrl_font = tkFont.Font(family='微软雅黑', size=14, weight='normal', slant='roman', underline=False)
        self.tbUrl.configure(font=self.tbUrl_font)
        self.tbUrl.config(background='#FFFFFF')
        self.tbUrl.config(foreground='#000000')
        self.tbUrl_frame.place(x=229, y=99)
        self.Label1_frame = ttk.Frame(width=126, height=26)
        self.Label1 = ttk.Label(self.Label1_frame, text='选择解析端口')
        self.Label1.place(x=0, y=0, width=126, height=26)
        self.Label1_font = tkFont.Font(family='微软雅黑', size=14, weight='bold', slant='roman', underline=True)
        self.Label1.configure(font=self.Label1_font)
        self.Label1.config(background='#F0F0F0')
        self.Label1.config(foreground='#00008B')
        self.Label1_frame.place(x=85, y=40)
        self.btnParse_frame = ttk.Frame(width=145, height=36)
        self.btnParse = ttk.Button(self.btnParse_frame, text='解  析', command=self.parse)
        self.btnParse.place(x=0, y=0, width=145, height=36)
        self.btnParse_frame.place(x=247, y=181)
        self.mainframe.pack()

    ### 功能逻辑部分 ###
    def parse(self):
        url = self.tbUrlVar.get()
        api = parseApi[self.cbbApi.get()]
        webbrowser.open(api+url)


root = tk.Tk()
sa = SubAssembly(root)
root.mainloop()
