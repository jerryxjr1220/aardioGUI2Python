import tkinter as tk
from tkinter.messagebox import showinfo
import tkinter.ttk as ttk
import tkinter.font as tkFont
from random import randint

TM = {
    1 : {
        'ques' : "1 + 1 = ?",
        'ra' : "1",
        'rb' : "2",
        'rc' : "3",
        'rd' : "4",
        'ans' : "2"

    },

    2 : {
        'ques' : "2 + 2 = ?",
        'ra' : "4",
        'rb' : "3",
        'rc' : "2",
        'rd' : "1",
        'ans' : "4"

    },
}

class Tiku():
    ### 界面设计部分 ###
    
    def __init__(self, master):
        # global Var
        self.radioSelect = tk.IntVar()
        self.num = 0
        self.total = 0
        self.correct = 0

        self.mainframe = ttk.Frame(master, width=896, height=581)
        # Z=3
        self.rA_frame = ttk.Frame(width=176, height=27)
        self.rA = ttk.Radiobutton(self.rA_frame, text="A", value=1, variable=self.radioSelect)
        self.rA.place(x=0, y=0, width=176, height=27)
        self.rA_frame.place(x=21, y=441)
        # Z=1
        self.label1_frame = ttk.Frame(width=176, height=31)
        self.label1 = ttk.Label(self.label1_frame, text="Python习题集", justify="left")
        self.label1.place(x=0, y=0, width=176, height=31)
        self.label1_font = tkFont.Font(family="微软雅黑", size=16, weight="bold", slant="roman", underline=False)
        self.label1.configure(font=self.label1_font)
        self.label1.config(foreground="#0000FF")
        self.label1_frame.place(x=22, y=15)
        # Z=2
        self.editVar1 = tk.StringVar(value="")
        self.edit1_frame = ttk.Frame(width=831, height=346)
        self.edit1 = tk.Text(self.edit1_frame)
        self.edit1.insert("end", "")
        self.edit1.place(x=0, y=0, width=831, height=346)
        self.edit1_vscroll = tk.Scrollbar(self.edit1, orient="vertical", command=self.edit1.yview)
        self.edit1.configure(yscrollcommand=self.edit1_vscroll.set)
        self.edit1_vscroll.pack(side="right", fill="y")
        self.edit1_font = tkFont.Font(family="Consolas", size=14, weight="normal", slant="roman", underline=False)
        self.edit1.configure(font=self.edit1_font)
        self.edit1_frame.place(x=26, y=76)
        # Z=5
        self.rC_frame = ttk.Frame(width=176, height=27)
        self.rC = ttk.Radiobutton(self.rC_frame, text="C", value=3, variable=self.radioSelect)
        self.rC.place(x=0, y=0, width=176, height=27)
        self.rC_frame.place(x=467, y=441)
        # Z=4
        self.rB_frame = ttk.Frame(width=176, height=27)
        self.rB = ttk.Radiobutton(self.rB_frame, text="B", value=2, variable=self.radioSelect)
        self.rB.place(x=0, y=0, width=176, height=27)
        self.rB_frame.place(x=244, y=441)
        # Z=8
        self.btnSelect_frame = ttk.Frame(width=109, height=27)
        self.btnSelect = ttk.Button(self.btnSelect_frame, text="指定选题", command=self.selectquestion)
        self.btnSelect.place(x=0, y=0, width=109, height=27)
        self.btnSelect_frame.place(x=624, y=44)
        # Z=7
        self.btnRandom_frame = ttk.Frame(width=109, height=27)
        self.btnRandom = ttk.Button(self.btnRandom_frame, text="随机选题", command=self.randquestion)
        self.btnRandom.place(x=0, y=0, width=109, height=27)
        self.btnRandom_frame.place(x=745, y=44)
        # Z=10
        self.btnFinish_frame = ttk.Frame(width=182, height=31)
        self.btnFinish = ttk.Button(self.btnFinish_frame, text="完    成", command=self.finish)
        self.btnFinish.place(x=0, y=0, width=182, height=31)
        self.btnFinish_frame.place(x=529, y=490)
        # Z=6
        self.rD_frame = ttk.Frame(width=176, height=27)
        self.rD = ttk.Radiobutton(self.rD_frame, text="D", value=4, variable=self.radioSelect)
        self.rD.place(x=0, y=0, width=176, height=27)
        self.rD_frame.place(x=690, y=441)
        # Z=9
        self.btnSubmit_frame = ttk.Frame(width=182, height=31)
        self.btnSubmit = ttk.Button(self.btnSubmit_frame, text="提    交", command=self.answer)
        self.btnSubmit.place(x=0, y=0, width=182, height=31)
        self.btnSubmit_frame.place(x=135, y=490)

        self.editVar2 = tk.StringVar(value="")
        self.edit2_frame = ttk.Frame(width=50, height=27)
        self.edit2 = tk.Entry(self.edit2_frame, textvariable=self.editVar2)
        self.edit2.place(x=0, y=0)
        self.edit2_frame.place(x=550, y=44)
        self.mainframe.pack()

    ### 功能逻辑部分 ###

    def selectquestion(self):
        n = self.editVar2.get()
        n = int(n)
        self.num = n
        self.edit1.delete(0.0, 'end')
        self.edit1.insert("end", TM[n]['ques'])
        self.rA.configure(text=TM[n]['ra'])
        self.rB.configure(text=TM[n]['rb'])
        self.rC.configure(text=TM[n]['rc'])
        self.rD.configure(text=TM[n]['rd'])

    def randquestion(self):
        n = randint(1, len(TM))
        self.num = n
        self.edit1.delete(0.0, 'end')
        self.edit1.insert("end", TM[n]['ques'])
        self.rA.configure(text=TM[n]['ra'])
        self.rB.configure(text=TM[n]['rb'])
        self.rC.configure(text=TM[n]['rc'])
        self.rD.configure(text=TM[n]['rd'])
        
    def answer(self):
        ans = self.radioSelect.get()
        if ans == 1: ans = 'ra'
        if ans == 2: ans = 'rb'
        if ans == 3: ans = 'rc'
        if ans == 4: ans = 'rd'
        if TM[self.num]['ans'] == TM[self.num][ans]:
            self.correct += 1
            self.total += 1
            self.randquestion()
            m = tk.Tk()
            m.geometry('300x50+800+400')
            msg = tk.Message(m, text="Correct!", width=300)
            msg.pack()
            m.resizable(width=False, height=False)
            m.mainloop()
        else:
            self.total += 1
            m = tk.Tk()
            m.geometry('300x50+800+400')
            msg = tk.Message(m, text="Wrong!", width=300)
            msg.pack()
            m.resizable(width=False, height=False)
            m.mainloop()

    def finish(self):
        m = tk.Tk()
        m.geometry('300x50+800+400')
        msg = tk.Message(m, text=f'Total: {self.total}\nCorrect: {self.correct}\nScore: {int((self.correct/self.total)*100)}', width=300)
        self.total = 0
        self.correct = 0
        self.randquestion()
        msg.pack()
        m.resizable(width=False, height=False)
        m.mainloop()


root = tk.Tk()
tiku = Tiku(root)
root.title("Python习题集  v0.0.1")
root.resizable(width=False, height=False)
root.mainloop()
