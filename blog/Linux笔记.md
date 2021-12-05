[TOC]

# 笔者序言

## 2021年9月25日 

**2021年9月24日**，对过去写的一些关于Linux的博客尝试做了一个整合的工作，我发现这个工作比我想象的要复杂，因为对之前写的文章多有不满，于是我尽力让文章变得通俗易懂且将所有引用的地方都标注出来，在整理的过程中，我有了一个大胆的想法，我想让这篇文章最终变成一个实用的Linux入门文章（或者说是书籍），这个工作不是一天就能完成的……，但是，这个工作毕竟已经开始了。

我主要参考的书籍是《鸟哥的Linux私房菜》，这是一本很优秀的书籍，我个人感觉这本书有一个缺点就是全书读下来，缺乏连贯性，这本书感觉像一本“特别幽默的字典”，我很喜欢这本书，这本书在官网是可以免费阅读的，我其实是有用这篇文章致敬“鸟哥”的想法，但我的文章的语言一定不会是幽默的，我的语言可能会偏口语一些。

编写这篇文章将会是一个不太漫长的过程（一个月左右），我会及时在我的博客[sogeisetsu - 博客园 (cnblogs.com)](https://www.cnblogs.com/sogeisetsu/)上更新文章，预计两天一更吧，每次更新我会在笔者序言里面进行简要说明更新的内容，**每次更新都会在笔者序言里拥有一个二级标题，二级标题题目是我上传更新的日期**，按从后到前排序，也就是说最新的更新说明会排在笔者序言最前面，**然后我应该不会按既有的目录进行更新。**

**这是第一次更新**，算是这个工作的开头，更新内容如下👇

1. 编写目录。
2. 增加[历史与常用关键词](#menuOneHistoryAndUniversWord)一节。
3. 在[服务管理](#serviceControl)一节，增加了三个表格。
4. 其他某几个小节，零零散散写了一些东西。

*更新于2021年9月25日02点53分 凌晨*

# <a id="menuOneHistoryAndUniversWord">历史与常用关键词</a>

## 名词解释

下面的东西，看了就会晕，我也是因为晕，才写了这篇文章🤣

- **unix** ：Unix是20世纪70年代初出现的一个操作系统，闭源，需要付费使用。
  - **BSD** ：又被称作是伯克利软件发行版，**是unix的一个重要的分支**，它创造性地加入了vi（一个编辑软件）和csh（一款shell）。
  - **POSIX标准**：  一个标准，因为unix的分支越来越多，**POSIX标准的目的是实现UNIX的标准化**，**这个标准的一个比较大的贡献就是shell脚本在大多数情况下，既可以在linux上运行，也可以在mac os上运行。**
- **GNU** ：全拼是**G**NU is **N**ot **U**nix 是一个[自由软件](https://zh.wikipedia.org/wiki/自由軟體)[集体协作](https://zh.wikipedia.org/wiki/集體協作)计划，1983年9月27日由[理查德·斯托曼](https://zh.wikipedia.org/wiki/理查德·斯托曼)在[麻省理工学院](https://zh.wikipedia.org/wiki/麻省理工學院)公开发起。它的目标是创建一套完全[自由](https://zh.wikipedia.org/wiki/自由软件)的[操作系统](https://zh.wikipedia.org/wiki/操作系统)，称为[GNU](https://zh.wikipedia.org/wiki/GNU)。理查德·斯托曼最早在net.unix-wizards[新闻组](https://zh.wikipedia.org/wiki/新闻组)上公布该消息，并附带一份《[GNU宣言](https://zh.wikipedia.org/wiki/GNU宣言)》等解释为何发起该计划的文章，其中一个理由就是要“重现当年软件界合作互助的团结精神”。
- **Linux**：有两个说法下面👇
  - **操作系统内核**：Linus发布的遵循GNU旗下“通用公共许可证”的操作系统内核。但是，**Linux内核并非GNU计划的一部分。**[^7]
  - **操作系统**：[GNU官网]([GNU操作系统和自由软件运动](https://www.gnu.org/))更喜欢称之为GNU/Linux，因为**GNU提供了bash和GCC**等操作系统所必须的东西，[Linux](https://baike.baidu.com/item/Linux)操作系统包涵了[Linux内核](https://baike.baidu.com/item/Linux内核)与其他自由软件项目中的GNU组件和软件，**甚至“linux操作系统”之中GNU计划的代码所占的比重要远远高于Linux内核**，现在所谓的各种Linux发行版其实应该叫做**一个带有Linux的GNU系统**。现在**越来越多的Linux发行版也认为自己是“GNU/Linux”**。
- **Linux发行版**：有两种说法，**一种是使用了Linux内核就叫Linux发行版，一种是只有GNU/linux才可以叫Linux发行版**，Linux发行版之间的区别就是预装软件、软件包管理、驱动、内核补丁、桌面环境的不同。
  - Linux发行版有社区版和商业版之分，前者是社区维护，后者嘛，就是公司维护咯。
  - 之所以会有**发行版的区别**，主要还是**侧重点**不一样，**目标用户**不一样，**风格**不一样，**背后的哲学思考**不一样，
    - **Ubuntu**注重的是**功能齐全，桌面华丽，预装驱动全**，基本上安装了就能用👍。
    - **debain**比较**注重开源精神，系统里面就很少有预装闭源的软件或者驱动**为了追求稳定性，软件包也不是很新
    - **Redhat**就是注重服务器系统的稳定性了，各种对kernel的补丁保证了稳定性，付费使用。
    - **centos**就是Redhat的每半年复制版（剔除了些许Redhat专有代码），继承了Redhat稳定性的特点，又由社区维护，免费用（但是**2021年之后centos8就停止维护了**，就挺震惊的🤦‍♀️）。
    - **Fedora**由红帽赞助，红帽各种新功能会先扔到Fedora上测试，算是红帽的先期测试版。
    - **Deepin**就比较牛逼了，基于debain，**桌面环境用起来比较舒服，为国人的使用习惯做了优化，预装的软件很良心，自带软件商店且软件商店丰富度尚可，自带图形化包安装器，最新版甚至可以直接安装安卓软件**，极大限度的照顾了小白用户，但就笔者使用下来的使用体验来看，照windows这种专业的图形界面操作系统还有相当长的差距。
    - **arch**系统就是两个字简洁，三个字，个性化，当然了，滚动更新还更新的特快🛴，**稳定性就差点儿事儿了**😓。
    - **还有一些很轻量化的系统**，比如安装在树莓派上的树莓派专属debain就可以装在一个8g的内存卡上使用🤷‍♂️，关键是还带桌面。**TinyCore Linux**就不到20MB，竟然还有图形化桌面环境，且kernel放在内存中运行，惊人不😉？
    - **Android**，这个比较有争议，**争议的核心是Linux发行版的定义问题**，Android使用了Linux的内核，但是Android并没有使用GNU套件，**至少Android绝对不是GNU/Linux**。
  - **不同的包管理系统** ：**采用不同的包管理系统，各发行版的维护者对系统的追求不一样**，比如准求稳定的发行版的的包仓库的软件通常是稳定版，追求新技术的发行版的包仓库的软件通常是比较新的版本，不同的包管理系统有不同的使用方法和社区用户者。
    - **什么是包管理系统**：包管理系统的作用是**方便安装、升级和自行解决依赖**，「包仓库」有助于确保代码已经在你使用的系统上进行了审核，并由软件开发者或包维护者进行管理。
    - **包管理系统的区别**：有的包管理系统是图形化界面（GUI），有的包管理系统是命令行界面（CLI），命令行界面虽然使用方式不同，**但笔者在使用体验上没有感觉到明显的不同🤷‍♂️，apt和yum都能很好的解决依赖问题，也都能方便的去安装、升级和卸载包。**据说不同的包管理系统在处理依赖的能力上是有区别的。
    - **为什么包管理系统没有统一**：**主要是历史原因。大约在同一时间建立了多个软件包管理系统，特别是.rpm和.deb。每个都有自己的拥护者，每个都足够优秀**👍，以至于没有任何一个软件包管理者具有明显的优势。发行商肯定不会完全重建他们的系统以实施其他程序包管理器。比如笔者我就比较喜欢yum，因为我单纯觉得yum的命令比较好记和简洁，apt虽然使用起来比较贴合直觉，但早些年的dpkg在安装本地的包的时候竟然不会解决依赖问题，需要用gdebi来解决🤬（当然，新版的debain可以用apt-get来安装本地包并且解决依赖了👌）
    - **主要的包管理系统使用方式的区别**：这个随便一搜就出来了，贴个随便搜的链接[Linux 包管理基础：apt、yum、dnf 和 pkg - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/28562152)
    - **遇到包仓库没有的软件怎么办**：一是自己编译源代码，比如用make命令编译，二是添加源，添加源，添加的源里面就收入了这个软件。贴一个常用的清华源（**注意：这是镜像源，理论上来说，原版源没有的软件，清华的镜像源里面也没有**）[centos | 镜像站使用帮助 | 清华大学开源软件镜像站 | Tsinghua Open Source Mirror](https://mirrors.tuna.tsinghua.edu.cn/help/centos/)
- **Android**：
  - **名词解释（绝大多数人撕逼的地方就在这里）**：安卓有很多含义
    - **AOSP** ：Android Open Source Project Android 开放源代码项目，遵循阿帕奇协议，谷歌是该项目的维护者。
    - **商标**：谷歌公司注册了，属于谷歌公司。
    - **Android**：这个安卓指的是谷歌公司发布的包含GMS服务和各种谷歌软件和新特性的操作系统，Android=AOSP+GMS+新特性。
  - **开源还是闭源**：
    - Android是运行于[Linux kernel](https://zh.wikipedia.org/wiki/Linux_kernel)之上，但并不是[GNU/Linux](https://zh.wikipedia.org/wiki/GNU/Linux)。
    - Android默认情况下并没有本机[X窗口系统](https://zh.wikipedia.org/wiki/X_Window系統)，也不支持整套标准[GNU](https://zh.wikipedia.org/wiki/GNU)库。Android为了达到商业应用，必须移除被[GNU GPL](https://zh.wikipedia.org/wiki/GNU_GPL)授权证所约束的部分。
    - **只有基础的Android操作系统（包括一些应用程序）才是开源软件**，任何厂商都不须经过Google和开放手持设备联盟的授权随意使用Android操作系统；大多数Android设备都附带着大量的专有软件，例如是[Google移动服务](https://zh.wikipedia.org/wiki/Google流動服務)，当中包括[Google Play](https://zh.wikipedia.org/wiki/Google_Play)商店、Google搜索，以及[Google Play服务](https://zh.wikipedia.org/wiki/Google_Play服務) — 那是一个提供与Google提供的服务[应用程序接口](https://zh.wikipedia.org/wiki/應用程式介面)集成的软件层。这些应用程序必须由设备制造商从Google得到许可
    - Android的硬件抽像层是能以封闭源码形式提供硬件驱动模块。HAL的目的是为了把Android framework与Linux kernel隔开，让Android不至过度依赖Linux kernel，以达成“内核独立”（kernel independent）的概念。这样安卓剥离了Linux内核以外的东西，**只需要对Android修改过的Linux内核采用GPL协议，硬件驱动可以闭源，Android其余的可以采用阿帕奇协议，保护了硬件厂商的权益和安卓GMS和安卓其他新特性**，这也就是为什么**华为可以使用ASOP却不能使用GMS和Android12。**
- <a id="whatshell">**shell**</a>： 壳程序，通俗的讲是命令解释器，**他的作用就是将用户的指令告诉操作系统的核心（kernel），然后呢kernel调用电脑的各种软硬件来达到你的指令的要求。**Windows的cmd是shell，Bash是Shell，powershell是shell，甚至windows的文件资源管理器也是shell（图形界面shell），sh也是shell（地球上第一个比较流行的shell简称sh，是一个外国人开发出来的，简称Bourne shell😶）
  - 分为**图形界面shell和文本shell**，当然，**这个区分是我瞎编的**，如有雷同，不胜荣幸哦😁
  - **bash** ：地球上所有能叫出名字来的Linux发行版默认使用的shell，可以说bash是sh的宇宙无敌增强版，知乎上竟然有一个问题linux初学者是学csh还是tcsh还是sh还是zsh的，**我晕，初学者当然是学bash啊，虽然是大同小异，可实在是没有必要让一个初学者第一个学习的shell竟然不是bash。**其实吧，*问这个问题的同学，你甚至可以学习在linux上用poweshell，powershell是完全有能力向kernel解释你的指令的😂，当然啦，在linux上使用powershell是一件可以但没有必要的事情。*
  - **不同shell的区别？** 首先是功能上的区别，有的shell的自定义程度很高，可以自定义彩色显示，有的shell可以自动建议和自动补全命令，有的shell会匹配历史命令，这是功能上的区别。然后就是标准的区别，也就是**是否支持POSIX标准**，比如说fish的早期版本不支持这个标准，fish无法执行含有`&&`的命令，这对使用者造成了很大的困扰。

## 历史

### （一）先从UNIX到GNU讲起

这部分很简单，20世纪六十年代，贝尔实验室，开发出了unix。**Unix不是开源软件，Unix源码可以与它的拥有者AT&T通过协议获得许可证**。第一个已知的软件许可证在1975年卖给了伊利诺伊大学。

Unix在学术界发展迅速，随着伯克利成为重要的活动中心，在70年代，Ken Thompson（开发unix的领导者）有一个学术休假。通过在伯克利的Unix的所有活动，一个新的Unix软件之父诞生了：伯克利软件发行版，或者叫**BSD**。

有好多公司和团体吧，就在unix的基础上二次开发，整出来了好多unix的分支，并且吧，这些个分支所使用的shell还不一样，**shell是啥呢？他的官方名称就是内容解释器或者叫壳程序，他的功能就是把用户输入的命令告诉操作系统的核心，然后这个核心再指挥硬件。**

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20210831003302.png)

**shell不一样，进行相同的操作接收的命令也就不一样**，也就是说，相当于这些shell所能听懂的人话不一样，好比shell是不同国家的人，有的shell只能听懂英文，有的shell只能听懂日文，这可咋办，这地球上unix分支这么多，甚至不同的计算机安装的分支都不一样，总不能让用户换个电脑还得重新学门语言吧，于是乎**POSIX标准**就出来了，不管你是谁，都应该能听懂同一种语言，全世界都得说中国话🤬（当然这只是戏虐）！于是乎，**同样的命令，可以在所有支持POSIX标准的shell上运行**。

然后，[理查德·斯托曼](https://zh.wikipedia.org/wiki/理查·斯托曼)，我们就暂且叫他老李头吧，老李头对unix很不爽，于是乎呢，他有了一个想法，那就是**开发一个完整的开放源代码的类unix操作系统**，来取代unix。他给自己的这个想法起了个名字叫GNU，名字的意思呢是“**GNU's Not Unix!**”，翻译成中文就是“老子不是unix，老子叫GNU！！！🙄”，他这个想法一提出来就得到了广大人民群众自发的支持，毕竟大家早就看unix不爽了，unix，啥都不是！想学习学习你的源代码竟然要花钱，老子不用你了！于是大家怀着这个要将unix干翻的想法开始工作起来了。

老李头在接下来的几年，**成立了自由软件基金会，开始雇佣人来写代码，并且发表了操作系统界的《独立宣言》——GNU宣言。还整了个GPL协议，这个协议挺niubility👍啊，遵循这个协议的软件必须开源，并且使用遵循GPL协议代码的软件也必须开源，哪怕是你商用，也得开源。**

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20210831002803.png)

这个计划吧，一开始还算顺利，向命令解释器（bash）啊，编辑器（Emacs）啊，编译器（GCC）啊啥的都进展的挺顺，可是啊，就是核心开发不出来，核心是啥呢？上面提过一嘴，核心就是能够控制硬件来执行接收到的命令的东西。这个东西可把老李头愁怀了，他是吃也吃不下，睡也睡不着，头发大把掉，胡子蹭蹭长（只是在调侃理查德的大胡子🤦‍♀️）……

### （二）天降正义，Linus！

时间来到了九十年代，一个芬兰赫尔辛基大学的大学生林纳斯，我们管他叫小林吧，这个小林不声不响，开发出来了操作系统内核linux，**并且这个Linux还采用了GPL协议**，小林按当时的话来说，就是比较帅呆了，还特谦虚幽默，你看他当年放出Linux时说的那话

> 我在做个（自由的）操作系统（就是个兴趣爱好，我不会搞得像GNU那么大那么专业），打算让它工作在386 AT平台上。它从四月就开始酝酿了，马上就快好了。我想要那些喜欢或不喜欢minix的人的意见，因为我的系统和它有点类似（同样的文件系统的物理布局——由于实际原因——还有些其他的东西）。
>
> 我现在已经移植了[bash](https://zh.wikipedia.org/wiki/Bash)(1.08)和[gcc](https://zh.wikipedia.org/wiki/GNU_Compiler_Collection)(1.40)， 而且看起来奏效了。这意味着我会在几个月内得到一些实用的东西。“……”是的——它没有任何minix代码，并且它有一个多线程的fs。它**不**可移植（使用386任务切换等），而且它可能永远不会支持除AT硬盘之外的其他东西，因为我只有这些:-(。
>
> “……”它基本上是用C语言写的，但是大多数人可能不会把我写的东西叫做C语言。它使用我能找到的386的每个可以想象的特性，因为它也是一个教我关于386的功能的项目。我前面提到过，它使用内存管理单元来进行分页（还没实现到对硬盘的功能）和分段。这个分段功能使得它真正的依赖于386（每个任务都有64Mb的代码和数据段——4Gb中最多64个任务。如果有人需要超过每个任务64Mb的限制，那将是个麻烦事）。“……”我的一些C语言文件（特别是mm.c）几乎用了和C一样多的汇编。“……”不像minix，我也碰巧喜欢中断，所以中断将在不试图隐藏背后的原因的情形下被处理。

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20210831011345.png)

老李头看这个小林是越看越喜欢，小林呢觉得GNU也不错，于是呢，$GNU+linux kernel（内核）=GNU/Linux！$

### （三）越来越受欢迎

从GNU/Linux第一个发行版Boot-root问世以来，GNU/Linux越来越受到欢迎，Marc Ewing成立Red Hat软件公司，成为最著名的Linux经销商之一，也证明了GNU/Linux在商业上被认可（GNU/Linux虽然开源，但不代表不可以商用，Red Hat是开源的，但是并不免费，Red Hat会为Red Hat的用户有偿提供技术服务）。

现在，整个GNU/Linux操作系统家族基于Linux内核部署在传统计算机平台（如个人计算机和服务器，以Linux发行版的形式）和各种嵌入式平台，如[路由器](https://zh.wikipedia.org/wiki/路由器)、[无线接入点](https://zh.wikipedia.org/wiki/無線接入點)、[专用小交换机](https://zh.wikipedia.org/wiki/专用小交换机)、[机顶盒](https://zh.wikipedia.org/wiki/机顶盒)、[FTA接收器](https://zh.wikipedia.org/w/index.php?title=FTA接收器&action=edit&redlink=1)、[智能电视](https://zh.wikipedia.org/wiki/智能电视机)、[数字视频录像机](https://zh.wikipedia.org/wiki/数字视频录像机)、[网络附加存储](https://zh.wikipedia.org/wiki/网络附加存储)（NAS）等。工作于[平板电脑](https://zh.wikipedia.org/wiki/平板電腦)、[智能手机](https://zh.wikipedia.org/wiki/智能手机)及[智能手表](https://zh.wikipedia.org/wiki/智能手表)的[Android](https://zh.wikipedia.org/wiki/Android)操作系统同样通过Linux内核提供的服务完成自身功能。尽管于[桌面电脑](https://zh.wikipedia.org/wiki/桌面電腦)的占用率较低，基于Linux的操作系统统治了几乎从移动设备到主机的其他全部领域。截至2017年11月，世界前500台最强的[超级计算机](https://zh.wikipedia.org/wiki/超级计算机)全部使用Linux。

GNU/Linux被打包成供个人计算机和服务器使用的Linux发行版，一些流行的主流Linux发行版，包括[Debian](https://zh.wikipedia.org/wiki/Debian)（及其派生版本[Ubuntu](https://zh.wikipedia.org/wiki/Ubuntu)、[Linux Mint](https://zh.wikipedia.org/wiki/Linux_Mint)）、[Fedora](https://zh.wikipedia.org/wiki/Fedora_(作業系統))（及其相关版本[Red Hat Enterprise Linux](https://zh.wikipedia.org/wiki/Red_Hat_Enterprise_Linux)、[CentOS](https://zh.wikipedia.org/wiki/CentOS)）和[openSUSE](https://zh.wikipedia.org/wiki/OpenSUSE)等。Linux发行版包含Linux内核和支撑内核的实用[程序](https://zh.wikipedia.org/wiki/计算机程序)和库，通常还带有大量可以满足各类需求的应用程序。

### （四）命名之争，linux还是GNU/linux？

有越来越多的人管GNU/linux叫linux操作系统，老李头团队的某些人一看不对啊，这怎么行？虽然使用了Linux内核，可也不能抹杀GNU的贡献吧？GNU起初可是要打造GNU系统的啊。

支持GNU的人认为**GNU提供了bash和GCC**等操作系统所必须的东西，[Linux](https://baike.baidu.com/item/Linux)操作系统包涵了[Linux内核](https://baike.baidu.com/item/Linux内核)与其他自由软件项目中的GNU组件和软件，**甚至“linux操作系统”之中GNU计划的代码所占的比重要远远高于Linux内核**，现在所谓的各种Linux发行版其实应该叫做**一个带有Linux的GNU系统**。

还有些人呢，就发话了，一个操作系统最重要的是啥？不就是内核吗？我们就叫Linux怎么了？（不代表笔者看法🤷‍♂️）

> “GNU/Linux”此名称是[GNU](https://zh.wikipedia.org/wiki/GNU)计划的支持者与开发者，特别是其创立者[理查德·斯托曼](https://zh.wikipedia.org/wiki/理查德·斯托曼)对于Linux操作系统的主张。由于此类操作系统使用了众多GNU程序，包含[Bash](https://zh.wikipedia.org/wiki/Bash)（[Shell程序](https://zh.wikipedia.org/wiki/Unix_shell)）、[库](https://zh.wikipedia.org/wiki/函式庫)、[编译器](https://zh.wikipedia.org/wiki/編譯器)等等作为Linux内核上的系统包，理查德·斯托曼认为应该将该操作系统称为“GNU/Linux”或“GNU+Linux”较为恰当，但现今多数人仍称其为Linux。就1997年之前的Linux来看，一间CD-ROM的供应商所提供的资料显示在他们的“Linux 发行版”中，GNU 软件所占最大的比重，大约占全部源代码的28%，且还包括一些关键的部件，如果没有这些部件，系统就无法工作，而Linux 本身占大约3%。
>
> Linux社群中的一些成员，如[埃里克·雷蒙](https://zh.wikipedia.org/wiki/埃里克·雷蒙)、[林纳斯·托瓦兹](https://zh.wikipedia.org/wiki/林纳斯·托瓦兹)等人，偏好Linux的名称，认为Linux朗朗上口，短而好记，拒绝使用“GNU/Linux”作为操作系统名称。并且认为Linux并不属于[GNU计划](https://zh.wikipedia.org/wiki/GNU計劃)的一部分，斯托曼直到1990年代中期Linux开始流行后才要求更名。有部分[Linux发行版](https://zh.wikipedia.org/wiki/Linux套件)，如[Debian](https://zh.wikipedia.org/wiki/Debian)，采用了“GNU/Linux”的称呼。但大多数商业Linux发行版依然将操作系统称为Linux。而有些人则认为“操作系统”一词指的只是系统的内核(Kernel)，其他程序都只能算是[应用软件](https://zh.wikipedia.org/wiki/应用软件)，因而，该操作系统应叫Linux，但Linux系统包是在Linux内核的基础上加入各种GNU软件包集合而成的。

反正吧，这事儿现在也没个准确的说法，我觉得吧，人GNU是做了很大贡献的，这个肯定都承认，可是挡不住Linux这个叫法朗朗上口写起来还省墨水啊，总之，现在（2021年8月31日）各种官方还没有个统一的说法，叫啥名字都无伤大雅，不管什么名字，都掩盖不了GNU和linux kernel的成功，也掩盖不了Linux操作系统对这个世界（尤其是开源世界）的贡献。

以上，就是Linux从无到有的历史了，通过这些历史，我们将不再对unix，GNU，Linux，Shell和bash等概念感到一筹莫展了，这就足够了。

# 系统安装

## 虚拟机

## wsl

## U盘安装

### 双系统

## 安装之后要做的第一件事情，换源、更新、安装常用软件和更改语系支持

# 常用命令

## 软件安装

[换源和更新](##安装之后要做的第一件事情，换源、更新、安装常用软件和更改语系支持)

# 文件与目录命令

## 加密 `tar`、`zip`和`unzip`

### `tar`

`tar`是一个Linux下比较常见的压缩命令，这个命令比较简单，支持多种压缩方式，**缺点是压缩出来的文件会得不到某些windows下常用压缩工具（7-zip）很好的支持**。

```bash
应用举例
       tar -xvf foo.tar
              提取 foo.tar 文件并显示提取过程

       tar -xzf foo.tar.gz
              提取用 gzip 压缩的文件 foo.tar.gz

       tar -cjf foo.tar.bz2 bar/
              用 bzip 为目录 bar 创建一个叫做 foo.tar.bz2存档

       tar -xjf foo.tar.bz2 -C bar/
              把用 bzip 压缩的文件 foo.tar.bz2 提取到 bar 目录

       tar -xzf foo.tar.gz blah.txt
              把文件 blah.txt 从 foo.tar.gz 中提取出来

       注意: 当压缩或提取的时候， 压缩类型选项常常是不必需的， 因为tar会根据文件的后缀自动选择压缩类型。
```



# 权限管理

# shell 脚本

## bash == 一种壳程序

shell就是“壳程序”，你可以阅读第一章的名词解释中的[shell部分](#whatshell)来了解一下。

shell通俗的讲是命令解释器，**他的作用就是将用户的指令告诉操作系统的核心（kernel），然后呢kernel调用电脑的各种软硬件来达到你的指令的要求。**

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20210831003302.png)

地球上有很多shell，Linux上也预装了很多shell（根据Linux发行版的不同，预装的软件也会不同），**bash是Linux默认的shell**。

```bash
描述(DESCRIPTION)
       Bash 是一个与 sh 兼容的命令解释程序，可以执行从标准输入或者文件中读取的命令。 Bash 也整合了 Korn 和 C Shell (ksh 和 csh) 中的优秀特性。

       Bash 的目标是成为遵循 IEEE POSIX Shell and Tools specification (IEEE Working Group 1003.2，可移植操作系统规约： shell  和工具) 的实现。
```



## 变量

### 环境变量与自有变量

### 变量的读取与查看 `echo`、`env`、`set`

### 变量类型的转换 `export`、`declare`

## 基本语法

## 运行脚本

# 环境配置文件

## `login shell`和`non-login shell`

[Linux登陆的两种状态 - 孙晨c - 博客园 (cnblogs.com)](https://www.cnblogs.com/sunbr/p/13255121.html)

## 语系的配置

### 中文man手册

# 编辑器

# 程序管理

## process和program

- 程序 （program）：通常为 binary program ，放置在储存媒体中 （如硬盘、光盘、
  软盘、磁带等）， 为实体文件的型态存在；

- 程序 （process）：程序被触发后，执行者的权限与属性、程序的程序码与所需数
  据等都会被载入内存中， 操作系统并给予这个内存内的单元一个识别码 （PID），
  可以说，程序就是一个正在运行中的程序。

一般所说的程序，指的是process

### 子程序与父程序

先明确一个概念，**Linux下所有的应用都是程序**，这句话的意思是，**bash也是一个程序。我们不要将bash看作是一个终端**，其实bash是一个shell程序。也就是说，当我们打开bash的时候，就已经有一个bash程序在运行了，我们在bash中输入命令（假定输入`ps -l`），这个命令会产生一个程序，这个程序叫做bash的子程序。

每一个程序都有一个单独的pid。查看pid可以使用命令`ps -l`，这个命令可以查看与当前bash相关的所有程序和子程序

```bash
suyuesheng@DESKTOP-OKC7OBA:~$ ps -l
F S   UID   PID  PPID  C PRI  NI ADDR SZ WCHAN  TTY          TIME CMD
4 S  1000   106   105  0  80   0 -  6215 wait   pts/2    00:00:00 bash
4 S  1000   289   288  0  80   0 -  6195 wait   pts/2    00:00:00 bash
4 S  1000  6259  6258  0  80   0 -  6187 wait   pts/2    00:00:00 bash
4 S  1000  7127  7126  0  80   0 -  6194 wait   pts/2    00:00:00 bash
4 S  1000  7150  7149  0  80   0 -  6187 wait   pts/2    00:00:00 bash
4 S  1000  7266  7265  0  80   0 -  6220 wait   pts/2    00:00:00 bash
4 S  1000  7330  7329  0  80   0 -  6288 wait   pts/2    00:00:00 bash
4 S  1000  7853  7852  0  80   0 -  6286 wait   pts/2    00:00:00 bash
0 R  1000  8470  7853  0  80   0 -  7350 -      pts/2    00:00:00 ps
```

第一行有很多字段，`F`、`S`、`PID`等，**pid代表的是当前程序的id，ppid代表的是当前程序的父程序的id**。==可以看到第11行的父程序是第10行pid为7853的程序。==

## `job` 工作管理

这个工作管理 （job control） 是用在 bash 环境下的，也就是说：“==当我们登陆系统取得 bash shell 之后，在单一终端机接口下同时进行多个工作的行为管理== ”。举例来说，我们在登陆 bash 后， 想要一边复制文件、一边进行数据搜寻、一边进行编译，还可以一边进行 vim 程序撰写！

### 后台运行 `&`

### 后台暂停 `ctrl+z`

###  `jobs` 显示工作的状态

```bash
 选项：
      -l        在正常信息基础上列出进程号
      -n        仅列出上次通告之后改变了状态的进程
      -p        仅列出进程号
      -r        限制仅输出运行中的任务
      -s        限制仅输出停止的任务
```

### 后台的工作拿到前台 `fg`

### 后台暂停的工作继续运行 `bg`

### 停止 `ctrl+c`、`kill`

- `ctrl+c` ：结束前台的工作，例如`ping google.com`可以用`ctrl+c` 来结束。
- `kill`：杀死进程，`kill`有很多不同的信号，使用`kill -l`可以查看信号列表，不同的信号表示对进程不同的操作。

```bash
suyuesheng@DESKTOP-OKC7OBA:~$ kill -l
 1) SIGHUP       2) SIGINT       3) SIGQUIT      4) SIGILL       5) SIGTRAP
 6) SIGABRT      7) SIGBUS       8) SIGFPE       9) SIGKILL     10) SIGUSR1
11) SIGSEGV     12) SIGUSR2     13) SIGPIPE     14) SIGALRM     15) SIGTERM
16) SIGSTKFLT   17) SIGCHLD     18) SIGCONT     19) SIGSTOP     20) SIGTSTP
21) SIGTTIN     22) SIGTTOU     23) SIGURG      24) SIGXCPU     25) SIGXFSZ
26) SIGVTALRM   27) SIGPROF     28) SIGWINCH    29) SIGIO       30) SIGPWR
31) SIGSYS      34) SIGRTMIN    35) SIGRTMIN+1  36) SIGRTMIN+2  37) SIGRTMIN+3
38) SIGRTMIN+4  39) SIGRTMIN+5  40) SIGRTMIN+6  41) SIGRTMIN+7  42) SIGRTMIN+8
43) SIGRTMIN+9  44) SIGRTMIN+10 45) SIGRTMIN+11 46) SIGRTMIN+12 47) SIGRTMIN+13
48) SIGRTMIN+14 49) SIGRTMIN+15 50) SIGRTMAX-14 51) SIGRTMAX-13 52) SIGRTMAX-12
53) SIGRTMAX-11 54) SIGRTMAX-10 55) SIGRTMAX-9  56) SIGRTMAX-8  57) SIGRTMAX-7
58) SIGRTMAX-6  59) SIGRTMAX-5  60) SIGRTMAX-4  61) SIGRTMAX-3  62) SIGRTMAX-2
63) SIGRTMAX-1  64) SIGRTMAX
```

| `kill`的single(信号) | 代表的操作                                                   |
| -------------------- | ------------------------------------------------------------ |
| 1                    | 对于daemon（守护进程）是重新读取配置，对于普通进程来说就是杀掉进程 |
| 9                    | 强制杀掉进程                                                 |
| 15（默认）           | 使用正常的步骤杀死进程                                       |

有一个惯例，通常使用`kill -9` 是**因为某些程序你真的不知道怎么通过正常手段去终止他**，这才用到 `kill -9` 的！

使用kill默认的方式（`kill -15`）不一定能够杀死进程。

==但凡是知道正常终止这个进程的方式，就不要用`kill`！==

# <a id="serviceControl">服务管理</a>

Linux老版本使用service、chkconfig等命令来管理系统服务，在RHEL 7/8系统中是使用systemctl命令来管理服务的，现在将两个版本的区别列在下面，表格来自[第1章 动手部署一台Linux操作系统 | 《Linux就该这么学》 (linuxprobe.com)](https://www.linuxprobe.com/basic-learning-01.html)

- systemd与System V init的区别以及作用👇

| System V init运行级别 | systemd目标名称   | systemd 目标作用 |
| --------------------- | ----------------- | ---------------- |
| 0                     | poweroff.target   | 关机             |
| 1                     | rescue.target     | 单用户模式       |
| 2                     | multi-user.target | 多用户的文本界面 |
| 3                     | multi-user.target | 多用户的文本界面 |
| 4                     | multi-user.target | 多用户的文本界面 |
| 5                     | graphical.target  | 多用户的图形界面 |
| 6                     | reboot.target     | 重启             |
| emergency             | emergency.target  | 救援模式         |

- 服务的**启动、重启、停止、重载、查看状态**等常用命令👇

| 老系统命令            | 新系统命令                | 作用                           |
| --------------------- | ------------------------- | ------------------------------ |
| `service foo start`   | `systemctl start httpd`   | 启动服务                       |
| `service foo restart` | `systemctl restart httpd` | 重启服务                       |
| `service foo stop`    | `systemctl stop httpd`    | 停止服务                       |
| `service foo reload`  | `systemctl reload httpd`  | 重新加载配置文件（不终止服务） |
| `service foo status`  | `systemctl status httpd`  | 查看服务状态                   |

-  服务**开机启动、不启动、查看各级别下服务启动状态**等常用命令👇

| 老系统命令          | 新系统命令                               | 作用                               |
| ------------------- | ---------------------------------------- | ---------------------------------- |
| `chkconfig foo on`  | `systemctl enable httpd`                 | 开机自动启动                       |
| `chkconfig foo off` | `systemctl disable httpd`                | 开机不自动启动                     |
| `chkconfig foo`     | `systemctl is-enabled httpd`             | 查看特定服务是否为开机自启动       |
| `chkconfig --list`  | `systemctl list-unit-files --type=httpd` | 查看各个级别下服务的启动与禁用情况 |

# 参考文档

阮一峰的博客：[阮一峰的网络日志 (ruanyifeng.com)](http://www.ruanyifeng.com/blog/)。

《鸟哥的Linux私房菜》

《Linux就该这么学》

