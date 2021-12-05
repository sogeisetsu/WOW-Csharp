# Problem Description 
## Problem recurrence 
When I use **English to Chinese translation, everything is normal**, as shown below 👇 
```bash
suyuesheng@DESKTOP-OKC7OBA:~$ trans en:zh hello
hello
/həˈlō/

你好
(Nǐ hǎo)

hello的定义
[ English -> 简体中文 ]

感叹词
    你好!
        Hello!, Hi!, Hallo!
    喂!
        Hey!, Hello!

hello
    你好, 您好
```
But **when translating Chinese into English, garbled characters will appear**, as shown below👇
```bash
suyuesheng@DESKTOP-OKC7OBA:~$ trans zh:en 好
å¥½

å¥½

å¥½ 的翻译
[ 简体中文 -> English ]

å¥½
    å¥½, å ¥ ½
```
**I am using windows terminal** 

## Locales

**My computer supports English and Chinese**. I use the `locale -a` command to clearly see the following 

```bash
C
C.UTF-8
en_US.utf8
POSIX
zh_CN.utf8
zh_HK.utf8
zh_SG.utf8
zh_TW.utf8
```
The locale of my computer is Chinese, when I use the `echo $LANG` command, the following👇
```bash
zh_CN.utf8
```
I am using a font `fira code`
# My version information 
The linux on my computer is ubuntu 
linux kernel version is`#1 SMP Wed Oct 28 23:40:43 UTC 2020`
**`trans -v ` is 👇**

```bash
Translate Shell       0.9.6.6

platform              Linux
gawk (GNU Awk)        4.1.4
fribidi (GNU FriBidi) 0.19.7
audio player          [NOT INSTALLED]
terminal pager        less
terminal type         xterm-256color
user locale           zh_CN.utf8 (Chinese Simplified)
home language         zh-CN
source language       auto
target language       zh-CN
translation engine    google
proxy                 [NONE]
user-agent            Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/602.1 (KHTML, like Gecko) Version/8.0 Safari/602.1 Epiphany/3.18.2
theme                 default
init file             [NONE]

Report bugs to:       https://github.com/soimort/translate-shell/issues
```

---

**In order to make it easier for native Chinese speakers to clearly understand the issue I provided, I will attach the Chinese information here.**

**使用wsl2**，Linux发行版是Ubuntu。

shell的语言环境是中文，`locale -a`包含中文和英文

可以正常的从英文翻译成中文，但是一旦用中文翻译成英文就会乱码。就像下面这样👇

```bash
suyuesheng@DESKTOP-OKC7OBA:~$ trans zh:en 好
å¥½

å¥½

å¥½ 的翻译
[ 简体中文 -> English ]

å¥½
    å¥½, å ¥ ½
```

