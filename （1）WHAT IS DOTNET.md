# WHAT IS DOTNET ?

## DOTNET之前的岁月

> 天不生.NET，万古如长夜。

**DOTNET简称“.NET”。**

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211111143708.png)

毫无疑问，DOTNET现在是一个跨平台的开发框架，但是DOTNET的产生之时，更多的是为了解决传统的Windows开发所面临的问题。传统的Windows开发基本是使用编程语言来调用`Win32 API`或者是`COM`。这种方式工程量大、实际代码复杂、需要大量丑陋的底层代码。**于是微软推出了下一代平台服务的目标——DOTNET。**

> .NET 是一个免费的、跨平台的、开源的开发平台，用于构建许多不同类型的应用程序。你可以使用多种语言、编辑器和库来构建 web、移动、桌面、游戏和物联网。
>
> ---
>
> 来源 [What is .NET? An open-source developer platform. (microsoft.com)](https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet)

## DOTNET框架的组成

DOTNET和Java等编程语言最大的不同就是它是一个一整套的解决方案。

> 剑阁峥嵘而崔嵬，一夫当关，万夫莫开。

**DOTNET由编程工具、基类库（BCL）和公共语言运行库（CLR）组成。**

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211010145656.png)

- 编程工具包括IDE（visual studio）、各种语言的编译器和调试器等部分组成。

- 基础类库（BCL）是微软所提出的一组标准库，可提供给 .NET 所有语言使用。随着 Windows 以及 .NET 的成长，BCL 已近乎成为在 .NET 上的 Windows API。

- CLR是DOTNET的核心，它在操作系统的顶层，负责管理程序的运行。

一段代码在DOTNET上编译和执行的过程如图所示：

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211010150805.png)

不同语言的源代码经由DOTNET兼容编译器，编译成`CIL`（公共中间语言），`CIL`是`CLI`（公共语言基础结构）的一部分，因为不同编程语言的特征都不同，为了让不同语言编写的程序都能通过DOTNET运行，所以需要一个统一的标准，这就是`CLI`（公共语言基础结构）。有了CLI之后， C# 生成的 CIL 代码可以与通过 .NET 版本的 F#、Visual Basic、C++ 生成的代码进行交互（可以通过调用动态链接 DLL）。

DOTNET不是编程语言，但它能通过`CLR`（公共语言运行库）的`JIT编译器`将`CIL`（公共中间语言）编译成`本机代码`，`本机代码`包括`托管代码`和`非托管代码`。

- 托管代码：为DOTNET框架编写的代码，需要CLR。
- 非托管代码：是指直接编译成目标计算机的机器码，比如`Win32 C/C++ DLL`。不在CLR控制下以运行。

本机代码可以在操作系统上执行，在执行的过程中`CLR`（公共语言运行库）会对`托管代码`进行内存管理、垃圾收集和异常处理等管理。

### 公共语言运行库（CLR）

CLR提供以下服务：

- 自动垃圾收集
- 安全和认证
- 通过BCL获得广泛的编程能力。

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211010153501.png)

# C# & DOTNET

C#的第一个预览版由微软在2000年发布，它是一个面向对象的语言，它伴随着DOTNET产生。它可以经由DOTNET兼容编译器编译成CIL（公共中间语言）。

> 回想起来，和 Visual Studio .NET 2002 一起发布的 C# 版本 1.0 非常像 Java。 在 [ECMA 制定的设计目标](https://feeldotneteasy.blogspot.com/2011/01/c-design-goals.html)中，它旨在成为一种“简单、现代、面向对象的常规用途语言”。 当时，它和 Java 类似，说明已经实现了上述早期设计目标。
>
> ---
>
> 来源：[C# 发展历史 - C# 指南 | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/csharp/whats-new/csharp-version-history)



> C# 程序在 .NET 上运行，而 .NET 是名为公共语言运行时 (CLR) 的虚执行系统和一组类库。 CLR 是 Microsoft 对公共语言基础结构 (CLI) 国际标准的实现。 CLI 是创建执行和开发环境的基础，语言和库可以在其中无缝地协同工作。
>
> 用 C# 编写的源代码被编译成符合 CLI 规范的[中间语言 (IL)](https://docs.microsoft.com/zh-cn/dotnet/standard/managed-code)。 IL 代码和资源（如位图和字符串）存储在扩展名通常为 .dll 的程序集中。 程序集包含一个介绍程序集的类型、版本和区域性的清单。
>
> 执行 C# 程序时，程序集将加载到 CLR。 CLR 会直接执行实时 (JIT) 编译，将 IL 代码转换成本机指令。 CLR 可提供其他与自动垃圾回收、异常处理和资源管理相关的服务。 由 CLR 执行的代码有时称为“托管代码”。 “非托管代码”编译为面向特定平台的本机语言。
>
> 除了运行时服务之外，.NET 还包含大量库。 这些库支持多种不同的工作负载。 它们已整理到命名空间中，这些命名空间提供各种实用功能。 这些功能包括文件输入输出、字符串控制、XML 分析、Web 应用程序框架和 Windows 窗体控件。 典型的 C# 应用程序广泛使用 .NET 类库来处理常见的“管道”零碎工作。
>
> ---
>
> 来源：[C# 介绍 - C# 指南 | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/)



- 参考文献：[.NET 简介和概述 | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/core/introduction#tools-and-productivity)

