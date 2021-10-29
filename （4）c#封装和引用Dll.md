#  What is Dll

Dll就是动态链接库，可以理解为一个已经编译好的package，Dll理论上可以被所有主流的编程语言调用。在安全性上面，因为是编译好的文件，理论上如果没有反编译技术是无法看到内部的代码实现的。因为是在运行时加载，所以可以多个程序共用同一个Dll，这就减轻了储存的压力。

.NET SDK的运行时CLR就是一堆Dll的集合。

C#调用Dll分为原生调用（静态调用）和动态加载两个部分，原生调用指的是调用通过.NET框架编译并发布的Dll。动态加载指的是调用其他第三方语言（比如c语言）用自己的编译器编译生成的Dll。

**本文讲的是原生调用（静态调用）。**

# 不借助vs

静态调用在大多数教程里面讲的是借助微软官方的IDE进行封装和调用。这一节讲的是在不借助IDE的情况下实现封装和调用。

## 封装

### 第一步 编写类库并编译成Dll

通过查询`dotnet new -l`可知类库的名称为`classlib`。

先创建一个类库，名为DllTwo。

在shell中运行下列程序即可生成：

```powershell
dotnet new classlib -o DllTwo
```

进入DllTwo文件夹，内部项目结构如下：

```shell
.
├── Class1.cs
├── DllTwo.csproj
├── bin
└── obj

```

编写Class1.cs，如下：

```c#
using System;
using System.Reflection;
namespace DllTwo
{
    public class Class1
    {
        public static int a = 1;
        public Class1()
        {
            Console.WriteLine("新建类库");
        }
        public String getName(){
            return MethodBase.GetCurrentMethod().Name;
        }
    }
}
```

` dotnet publish -c Release`编译类库。

## 调用

复制`\DllTwo\bin\Release\net5.0\publish\DllTwo.dll`到另一个项目中去，这里的另一个项目，设置为`SuOne`，将DllTwo.dll复制到SuOne的根目录。在SuOne.csproj中添加以下内容：

```xml
  <ItemGroup>
    <Reference Include="DllTwo">
      <HintPath>.\DllTwo.dll</HintPath>
    </Reference>
  </ItemGroup>
```

在SuOne的class1文件中通过`using DllTwo;`来引入dll，然后创建一个方法，这个方法调用DllTwo的Class1类的函数：

```c#
/// <summary>
/// 调用引入的动态链接库
/// </summary>
public void UseDll()
{
    Class1 cc = new Class1();
    Console.WriteLine(cc.getName());
}
```

执行这个函数，控制台输出：

```shell
新建类库
getName
```

这样就说明调用成功了。

# LICENSE

copyright © 2021 苏月晟，版权所有。

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />本<span xmlns:dct="http://purl.org/dc/terms/" href="http://purl.org/dc/dcmitype/Text" rel="dct:type">作品</span>由<span xmlns:cc="http://creativecommons.org/ns#" property="cc:attributionName">苏月晟</span>采用<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">知识共享署名-非商业性使用-相同方式共享 4.0 国际许可协议</a>进行许可。

