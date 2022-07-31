from demo1 import SA
from demo2 import CA
import tkinter as tk
import tkinter.ttk as ttk


root = tk.Tk()
# 设置全局变量 scale_rate转换比例
scale_rate = tk.DoubleVar(root, value=1)
nb = ttk.Notebook(root)
tab1 = ttk.Frame(nb)
nb.add(tab1, text='光学测量')
sa = SA(tab1, scale_rate)
tab2 = ttk.Frame(nb)
nb.add(tab2, text='测量校准')
ca = CA(tab2, scale_rate)
nb.pack()
root.mainloop()
