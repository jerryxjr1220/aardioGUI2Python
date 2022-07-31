import tkinter as tk
import tkinter.ttk as ttk
import tkinter.font as tkFont
import cv2
from PIL import Image, ImageTk, ImageDraw
import pyautogui as auto
from system_hotkey import SystemHotkey
   
class SA():
    ### 界面设计部分 ###
    
    def __init__(self, master, globalVar): # 增加一个全局变量接收参数globalVar = scale_rate
        self.globalVar = globalVar
        self.mainframe = ttk.Frame(master, width=716, height=569)
        # Z=1
        self.label1_frame = ttk.Frame(self.mainframe, width=158, height=24)
        self.label1 = ttk.Label(self.label1_frame, text="尺寸测量", justify="left")
        self.label1.place(x=0, y=0, width=158, height=24)
        self.label1_font = tkFont.Font(family="微软雅黑", size=12, weight="bold", slant="roman", underline=False)
        self.label1.configure(font=self.label1_font)
        self.label1.config(foreground="#000080")
        self.label1_frame.place(x=39, y=6)
        # Z=2
        self.pic1_frame = ttk.Frame(self.mainframe, width=640, height=480)
        self.pic1 = ttk.Label(self.pic1_frame, justify="left")
        self.pic1.place(x=0, y=0, width=640, height=480)
        self.pic1_frame.place(x=30, y=39)
        # Z=3
        self.button1_frame = ttk.Frame(self.mainframe, width=124, height=24)
        self.button1 = ttk.Button(self.button1_frame, text="打开摄像头", command=self.detect)
        self.button1.place(x=0, y=0, width=124, height=24)
        self.button1_frame.place(x=212, y=4)
        # Z=4
        self.button2_frame = ttk.Frame(self.mainframe, width=124, height=24)
        self.button2 = ttk.Button(self.button2_frame, text="关闭摄像头", command=self.close, state='disabled')
        self.button2.place(x=0, y=0, width=124, height=24)
        self.button2_frame.place(x=352, y=4)
        # Z=5
        self.scale1_frame = ttk.Frame(self.mainframe, width=200, height=24)
        self.scale1 = ttk.Scale(self.scale1_frame, orient='horizontal', from_=24, to=120, value=30, command=self.set_FPS)
        self.scale1.place(x=0, y=0, width=124, height=24)
        self.scale1_frame.place(x=502, y=4)
        self.mainframe.pack()
        # Global
        self.Running = False
        self.FPS = 30
        self.x = 0
        self.y = 0
        self.draw = False
        self.hotkey = SystemHotkey()
        self.hotkey.register(('f11',), callback=self.set_xy)
        
    ### 功能逻辑部分 ###
    def detect(self, *args):
        self.pic1.place(x=0, y=0, width=640, height=480)
        cap = cv2.VideoCapture(0)
        self.Running = True
        self.button2.configure(state='normal')
        self.capture(cap)


    def capture(self, cap):
        _, frame = cap.read()
        cwindow = auto.getActiveWindow()
        mx, my = auto.position()
        x = mx-cwindow.left-40
        y = my-cwindow.top-98
        d = ((self.x - x)**2 + (self.y - y)**2)**0.5
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        im = Image.fromarray(frame)
        imtext = ImageDraw.Draw(im)
        imtext.text(xy=(5, 5), text=f'FPS: { self.FPS }')
        imtext.text(xy=(5, 15), text=f'({self.x}, {self.y}) to ({x}, {y}): { d }')
        if self.draw:
            imtext.line(xy=[(self.x, self.y), (x, y)], width=2)
            imtext.text(xy=(5, 25), text=f'Distance: {d/self.globalVar.get()} mm')
        img = ImageTk.PhotoImage(im)
        self.pic1.img = img
        self.pic1.configure(image=img)
        if self.Running:
            self.pic1.after(int(1000/self.FPS), lambda:self.capture(cap))
        else:
            cap.release()

    def set_xy(self, *args):
        cwindow = auto.getActiveWindow()
        mx, my = auto.position()
        x = mx - cwindow.left - 40
        y = my - cwindow.top - 98
        self.x = x
        self.y = y
        self.draw = not self.draw

    def close(self, *args):
        self.Running = False
        self.pic1.place_forget()

    def set_FPS(self, *args):
        self.FPS = int(self.scale1.get())

