#  What is Dll

Dll就是动态链接库，可以理解为一个已经编译好的package，Dll理论上可以被所有主流的编程语言调用。在安全性上面，因为是编译好的文件，理论上如果没有反编译技术是无法看到内部的代码实现的。因为是在运行时加载，所以可以多个程序共用同一个Dll，这就减轻了储存的压力。

.NET SDK的运行时CLR就是一堆Dll的集合。

C#调用Dll分为原生调用（静态调用）和动态加载两个部分，原生调用指的是调用通过.NET框架编译并发布的Dll。动态加载指的是调用其他第三方语言（比如c语言）用自己的编译器编译生成的Dll。

**本文讲的是原生调用（静态调用）。**

# 不借助`visual studio`

静态调用在大多数教程里面讲的是借助微软官方的IDE进行封装和调用。这一节讲的是在不借助IDE的情况下实现封装和调用。

## 封装

**编写类库并编译成Dll**

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

# 借助`visual studio`

**使用`visual studio`来封装和调用dll。**

其实这个方法本质上也是和不借助vs一样的调用.NET CLI的那些命令，仅仅是从CLI变成了GUI而已。

**以下来源为visual studio的[官方教程](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/library-with-visual-studio?pivots=dotnet-5-0)：**

类库定义的是可以由应用程序调用的类型和方法。 如果库以 .NET Standard 2.0 为目标，则支持 .NET Standard 2.0 的任何 .NET 实现（包括 .NET Framework）均可调用该库。 如果库以 .NET 5 为目标，则以 .NET 5 为目标的任何应用程序均可调用该库。 本教程演示如何以 .NET 5 为目标。

创建类库后，可将其作为 NuGet 包或作为与使用该类库的应用程序捆绑在一起的组件进行分发。

## 必备条件

- 安装了具有“.NET Core 跨平台开发”工作负载的 [Visual Studio 2019 版本 16.8 或更高版本](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=inline+link&utm_content=download+vs2019)。 选择此工作负载时，将自动安装 .NET 5.0 SDK。 本教程假设已启用“在‘新建项目’中显示所有 .NET Core 模板”，如[教程：使用 Visual Studio 创建 .NET 控制台应用程序](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/with-visual-studio)中所示。

## 创建解决方案

首先，创建一个空白解决方案来放置类库项目。 Visual Studio 解决方案用作一个或多个项目的容器。 将其他相关项目添加到同一个解决方案中。

创建空白解决方案：

1. 启动 Visual Studio。

2. 在“开始”窗口上，选择“创建新项目”。

3. 在“创建新项目”页面上，在搜索框中输入“解决方案”。 选择“空白解决方案”模板，然后选择“下一步”。

   ![Visual Studio 中的空白解决方案模板](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/media/library-with-visual-studio/blank-solution.png)

4. 在“配置新项目”页面上，在“项目名称”框中输入“ClassLibraryProjects”。 然后选择“创建”。

## 创建类库项目

1. 将名为“StringLibrary”的新 .NET 类库项目添加到解决方案。

   1. 在“解决方案资源管理器”中，右键单击解决方案并选择“添加” > “新建项目” 。
   2. 在“创建新项目”页面上，在搜索框中输入“库”。 从“语言”列表中选择“C#”或“Visual Basic”，然后从“平台”列表中选择“所有平台”。 选择“类库”模板，然后选择“下一步” 。
   3. 在“配置新项目”页的“项目名称”框中，输入“StringLibrary”，然后选择“下一步” 。
   4. 在“其他信息”页上，选择“.NET 5.0 (当前)”，然后选择“创建” 。

2. 请检查以确保库以 .NET 的正确版本为目标。 右键单击“解决方案资源管理器”中的库项目，然后选择“属性”。 “目标框架”文本框显示项目以 .NET 5.0 为目标。

3. 如果使用 Visual Basic，请清除“根命名空间”文本框中的文本。

   ![类库的项目属性](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/media/library-with-visual-studio/vb/library-project-properties.png)

   对于每个项目，Visual Basic 会自动创建一个与项目名称对应的命名空间。 在本教程中，通过使用代码文件中的 [`namespace`](https://docs.microsoft.com/zh-cn/dotnet/visual-basic/language-reference/statements/namespace-statement) 关键字定义顶级命名空间。

4. 将 Class1.cs 或 Class1.vb 代码窗口中的代码替换为以下代码，并保存文件 。 如果未显示想要使用的语言，请更改页面顶部的语言选择器。

   C#复制

   ```csharp
   using System;
   
   namespace UtilityLibraries
   {
       public static class StringLibrary
       {
           public static bool StartsWithUpper(this string str)
           {
               if (string.IsNullOrWhiteSpace(str))
                   return false;
   
               char ch = str[0];
               return char.IsUpper(ch);
           }
       }
   }
   ```

   类库 `UtilityLibraries.StringLibrary`包含一个名为 `StartsWithUpper` 的方法。 此方法会返回 [Boolean](https://docs.microsoft.com/zh-cn/dotnet/api/system.boolean) 值，以指明当前字符串实例是否以大写字符开头。 Unicode 标准会区分大小写字符。 如果为大写字符，[Char.IsUpper(Char)](https://docs.microsoft.com/zh-cn/dotnet/api/system.char.isupper#System_Char_IsUpper_System_Char_) 方法返回 `true`。

   `StartsWithUpper` 以[扩展方法](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)的形式进行实现，这样就可以将其作为 [String](https://docs.microsoft.com/zh-cn/dotnet/api/system.string) 类成员进行调用。

5. 在菜单栏上，选择“生成” > “生成解决方案”或按 Ctrl+Shift+B，验证项目是否编译正确 。

## 向解决方案添加控制台应用

添加使用类库的控制台应用程序。 应用将提示用户输入字符串，并报告字符串是否以大写字符开头。

1. 将名为“ShowCase”的新 .NET 控制台应用程序添加到解决方案。

   1. 在“解决方案资源管理器”中右键单击解决方案并选择“添加” > “新建项目”。
   2. 在“创建新项目”页面，在搜索框中输入“控制台”。 从“语言”列表中选择“C#”或“Visual Basic”，然后从“平台”列表中选择“所有平台”。
   3. 选择“控制台应用程序”模板，然后选择“下一步” 。
   4. 在“配置新项目”页面，在“项目名称”框中输入“ShowCase”。 然后选择“下一步” 。
   5. 在“其他信息”页上，选择“目标框架”框中的“.NET 5.0 (当前)” 。 然后选择“创建”。

2. 在“Program.cs”或“Program.vb”文件的代码窗口中，将所有代码替换为以下代码 。

   C#复制

   ```csharp
   using System;
   using UtilityLibraries;
   
   class Program
   {
       static void Main(string[] args)
       {
           int row = 0;
   
           do
           {
               if (row == 0 || row >= 25)
                   ResetConsole();
   
               string input = Console.ReadLine();
               if (string.IsNullOrEmpty(input)) break;
               Console.WriteLine($"Input: {input} {"Begins with uppercase? ",30}: " +
                                 $"{(input.StartsWithUpper() ? "Yes" : "No")}{Environment.NewLine}");
               row += 3;
           } while (true);
           return;
   
           // Declare a ResetConsole local method
           void ResetConsole()
           {
               if (row > 0)
               {
                   Console.WriteLine("Press any key to continue...");
                   Console.ReadKey();
               }
               Console.Clear();
               Console.WriteLine($"{Environment.NewLine}Press <Enter> only to exit; otherwise, enter a string and press <Enter>:{Environment.NewLine}");
               row = 3;
           }
       }
   }
   ```

   该代码使用 `row` 变量来维护写入到控制台窗口的数据行数计数。 如果大于或等于 25，该代码将清除控制台窗口，并向用户显示一条消息。

   该程序会提示用户输入字符串。 它会指明字符串是否以大写字符开头。 如果用户没有输入字符串就按 Enter 键，那么应用程序会终止，控制台窗口会关闭。

## 添加项目引用

最初，新的控制台应用项目无权访问类库。 若要允许该项目调用类库中的方法，可以创建对类库项目的项目引用。

1. 在“解决方案资源管理器”中，右键单击 `ShowCase` 项目的“依赖项”节点，并选择“添加项目引用” 。

   ![在 Visual Studio 中添加引用上下文菜单](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/media/library-with-visual-studio/add-reference-context-menu.png)

2. 在“引用管理器”对话框中，选择“StringLibrary”项目，然后选择“确定”按钮 。

   ![选择了“StringLibrary”的“引用管理器”对话框](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/media/library-with-visual-studio/manage-project-references.png)

## 运行应用

1. 在“**解决方案资源管理器**”中，右键单击“**ShowCase**”项目，在上下文菜单中选择“**设为启动项目**”。

   ![Visual Studio 中用于设置启动项目的项目上下文菜单](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/media/library-with-visual-studio/set-startup-project-context-menu.png)

2. 按 Ctrl+F5 编译并运行程序，而不进行调试。

   ![Visual Studio 中显示“调试”按钮的项目工具栏](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/media/library-with-visual-studio/visual-studio-project-toolbar.png)

3. 输入字符串并按 Enter 以试用程序，然后按 Enter 退出。

   ![运行展示的控制台窗口](https://docs.microsoft.com/zh-cn/dotnet/core/tutorials/media/library-with-visual-studio/run-showcase.png)

# LICENSE

copyright © 2021 苏月晟，版权所有。

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />本<span xmlns:dct="http://purl.org/dc/terms/" href="http://purl.org/dc/dcmitype/Text" rel="dct:type">作品</span>由<span xmlns:cc="http://creativecommons.org/ns#" property="cc:attributionName">苏月晟</span>采用<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">知识共享署名-非商业性使用-相同方式共享 4.0 国际许可协议</a>进行许可。

