# python 包（`package`）和模块（`module`）的创建和引入（`import`）

[![img](https://i.creativecommons.org/l/by-nc-sa/3.0/us/80x15.png)](https://creativecommons.org/licenses/by-nc-sa/3.0/us/)

## 名词解释

> 实际上，Python中的函数(Function)、类(Class)、模块(Module)、包库(Package)，都是为了实现模块化引用，让程序的组织更清晰有条理。
>
> 👉通常，函数、变量、类存储在被称为模块(Module)的.py文件中，一组模块文件又组成了包(Package)。
>
> 👉将函数、变量、类存储在存储在独立的.py文件中，可隐藏代码实现的细节，将不同代码块重新组织，与主程序分离，简化主程序的逻辑，提高主程序的可读性。
>
> 👉 有了包和模块文件，可以在其他不同程序中进行复用，还可以使用其他人开发的第三方依赖库。
>
>
> ------------------------------------------------
> 本引用为CSDN博主「虾米小馄饨」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
> 原文链接：https://blog.csdn.net/Bit_Coders/article/details/119318000

package实际上就是就是一个文件夹，里面包含诸多module和一个`__init__.py`，package是module的一种，这点在python报错的时候也能看出来。

## 引入方式

1. `import moduleName`
2. `import packageName`
3. `from packageName import moduleName\packageName`
4. `from moudleName import Function\Class`

### 引入父级目录模块

> sys.path 是 sys 模块中的内置变量。它包含一个目录列表，编译器将搜索所需的模块。

如果要引入父级模块，需要在引入之前需要在python的编译器的环境变量中添加当前文件父目录，然后再import，有两个添加方法

1. `sys.path.append(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))`
2. `sys.path.append("..")`

建议使用第一个方法，第二个方法会在除pycharm以外的地方运行的时候造成错误，原因是`sys.path.append("..")`添加的是当前使用者所在目录的父目录，而不是当前这个文件的父目录。

`util.hi`是父级目录中的模块，引入方式如下：

```python
import sys

sys.path.append(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
import util.hi
```

### 注意事项

- ✔️在没有`from`的情况下，`moduleName`的形式可以是通过附属关系按照`packageName.moduleName`使用。
- ❌在有`from`的时候，`import`后面必须是包名称或者是函数名或者类名或者模块名。但是不能用`.`来表示层级关系，也就是说不能用向`packageName.moduleName`之类的用法，但是可以用`,`来区分不同的模块。
- ✔️只有在有`from`的情况下，`import`后面才能跟函数名或者类名

### 示范

这是<a id="当前环境">当前的环境</a>👇

```powershell
.
├── main.py
├── test
├── testproject
│   ├── __init__.py
│   ├── pa1
│   │   ├── __init__.py
│   │   └── hello.py
│   └── testproject.py
└── util

```

这是pa1目录下的`hello.py`👇，有一个函数`hello`和一个类`HelloT`。

```python
import sys

acb: str = "1232"


def hello():
    print("hello world")


class HelloT:
    def __init__(self) -> None:
        pass

    def hello(self):
        print("call", sys._getframe().f_code.co_name)

```

#### 正确示范

下面示范在根目录下，main.py的**正确import示范**：

```python
# 引入pa1包的hello.py模块
from testproject.pa1 import hello
# 引入pa1包
import testproject.pa1
# 引入hello.py模块
import testproject.pa1.hello
# 引入在testproject包的pa1包
from testproject import pa1
# 引入hello.py模块下的hello函数和HelloT类
from testproject.pa1.hello import hello,HelloT
```

#### 错误示范

下面示范在根目录下，main.py的**错误import示范**👇，错误原因请对照[注意事项](#注意事项)

```python
from testproject import pa1.hello
import testproject.pa1.hello.HelloT
```

## 使用方式

先看一下在引入默认模块（比如`os`、`math`、`random`）的时候，使用被引入的模块的方式：

```python
>>> import os
>>> os.path.abspath(".")
'C:\\Users\\苏月晟\\Desktop\\pythonProject1'
>>> import math
>>> math.pi
3.141592653589793
>>> import random
>>> random.random()
0.11531493534041015
```

使用引入基本上只有两个要求，一个是别重名，一个是使用引入的时候所使用的被引入模块名字必须是和`import`后面的一模一样，比如说使用了`import testproject.pa1.hello`，那么想使用hello模块的时候必须用`testproject.pa1.hello`而不是`hello`。如果是使用了`from testproject.pa1 import hello`来引入hello模块，则在使用hello模块的时候直接用`hello`。

可以看一下下面的示例来具体了解其中的差异：

```python
>>> import os.path
>>> path.abspath(".")
Traceback (most recent call last):   
  File "<stdin>", line 1, in <module>
NameError: name 'path' is not defined
>>> os.path.abspath(".")
'C:\\Users\\苏月晟\\Desktop\\pythonProject1'
>>> from os import path
>>> path.abspath(".")
'C:\\Users\\苏月晟\\Desktop\\pythonProject1'
```

### 使用模块中的常量

只需要在模块中定义一个常量，然后在使用的时候用`模块名.常量名`就可以了，就像hello模块里面有一个常量`acb`，在引入hello模块之后，用`hello.acb`就可以调用常量`acb`了

## 创建方式

创建包的时候，包目录里面必须有`__init__.py`，这个文件一般情况下可以是空的，具体这个文件怎么使用可以看[Python __init__.py 作用详解 - Data&Truth - 博客园 (cnblogs.com)](https://www.cnblogs.com/Lands-ljk/p/5880483.html)

看一下下面的目录结构

```bash
.
├── __init__.py
├── pa1
│   ├── __init__.py
│   └── hello.py
└── testproject.py
```

pa1是一个包，pa1目录下面有一个`__init__.py`，pa1下面还有一个`hello.py`，这个文件是一个模块。

hello.py

```python
import sys

acb: str = "1232"


def hello():
    print("hello world")


class HelloT:
    def __init__(self) -> None:
        pass

    def hello(self):
        print("call", sys._getframe().f_code.co_name)

```

这样就创建了一个模块。

# LICENSE

[![img](https://i.creativecommons.org/l/by-nc-sa/3.0/us/80x15.png)](https://creativecommons.org/licenses/by-nc-sa/3.0/us/)[署名-非商业性使用-相同方式共享 3.0 美国 (CC BY-NC-SA 3.0 US)](https://creativecommons.org/licenses/by-nc-sa/3.0/us/deed.zh) © 2021 苏月晟。

