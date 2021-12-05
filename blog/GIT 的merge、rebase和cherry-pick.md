# GIT 的merge、rebase和cherry-pick区别和使用示例

[![img](https://i.creativecommons.org/l/by-nc-sa/3.0/us/80x15.png)](https://creativecommons.org/licenses/by-nc-sa/3.0/us/)

## 名词解释

就是大体说一下git的上传和撤销的工作流程，用[图解Git (marklodato.github.io)](https://marklodato.github.io/visual-git-guide/index-zh-cn.html#basic-usage)的一张图就能说的很明白了

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211003175009.png)

或者看这张图

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211003225301.svg)

## 当前环境

有两个分支一个是`master`，另一个是`dev`，两个分支都指向同一个提交，并且两个分支的状态都是干净的。

```shell
>git status
On branch master
nothing to commit, working tree clean

>git switch dev
Switched to branch 'dev'

>git status
On branch dev
nothing to commit, working tree clean

```

```powershell
>git log
commit a1fe48e0054054c026554f5ed527e886113b5718 (HEAD -> dev, master)
Author: suyuesheng <df>
Date:   Sun Oct 3 16:29:12 2021 +0800

    add diff.txt


```

## 尝试使用merge

### ①分别修改两个分支，并提交

现在将两个分支的`hello.pyx`各增加一句不同的话，并提交修改，现在分支状况如下：

```powershell
>git log --all --graph --pretty=oneline
* 42a54922a38300eb091a714f092136c469718c82 (HEAD -> master) diff master one
| * 1e04ba0525d896e46ea6d01bbbd965c50d13d03a (dev) diff dev one
|/
* a1fe48e0054054c026554f5ed527e886113b5718 add diff.txt

```

### ②进行合并，合并失败，需处理冲突

现在尝试合并，因为两个分支各自都分别有新的提交，**所谓合并就是合并该分支基于共同祖先的更改**，因为合并dev分支之后会伤害到master分支所作的更改，所以会出现如下报错

```powershell
>git merge dev
Auto-merging cythontest/hello.pyx
CONFLICT (content): Merge conflict in cythontest/hello.pyx
Automatic merge failed; fix conflicts and then commit the result.

```

工作目录中`hello.pyx`中也自动标记出了<a id="合并的冲突">合并的冲突</a>

```python
<<<<<<< HEAD
    print("diff master one")
=======
    print("diff dev one")
>>>>>>> dev

```

### ③处理冲突

尝试使用git自带的`mergetool`来处理冲突，当然也可以在工作目录中之间编辑冲突。

键入`git mergetool`

```powershell
>git mergetool

This message is displayed because 'merge.tool' is not configured.
See 'git mergetool --tool-help' or 'git help config' for more details.
'git mergetool' will now attempt to use one of the following tools:
tortoisemerge emerge vimdiff
Merging:
cythontest/hello.pyx

Normal merge conflict for 'cythontest/hello.pyx':
  {local}: modified file
  {remote}: modified file
Hit return to start merge resolution tool (vimdiff):

```

键入`回车`

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211003171359.png)

很明显，pycharm的终端抽风了，但是，这不重要。

……

我退出了git自带的mergetool——`vimdiff`,我在工作目录中修改冲突。修改如下

```python

    print("diff master one")

    print("diff dev one")


```

还记得刚键入`git merge dev`时工作目录中文件是什么鬼样子吗？忘了的话，[回去看一下](#合并的冲突)

现在**合并工作还没有完成，因为修改冲突之后的文件还没有提交**，我们先看一下现在的状态，键入`git status`

```powershell
>git status
On branch master
All conflicts fixed but you are still merging.
  (use "git commit" to conclude merge)

Changes to be committed:
        modified:   cythontest/hello.pyx

Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  (use "git restore <file>..." to discard changes in working directory)
        modified:   cythontest/hello.pyx

```

git非常智能地告诉我们，现在还在合并中(`still merging`)，所以就按git提示地来键入`git commit`（先键入`git add .`，将处理冲突的结果加入暂存区）来结束合并（`conclude merge`）。shell出现如下回复：

```powershell
>git commit
hint: Waiting for your editor to close the file...
[main 2021-10-03T09:24:32.241Z] update#setState idle
[master f45308b] Merge branch 'dev'

```

### ④合并完成

键入`git commit`之后会自动打开一个文件，然后按git的智能提示来关闭这个文件（close the file），合并完成。

键入`git log --pretty=oneline --graph --all`看一下现在各个分支的状态

```powershell
>git log --pretty=oneline --graph --all
*   f45308b77fc578dd17bcca7d7402285dca97bac0 (HEAD -> master) Merge branch 'dev'
|\
| * 1e04ba0525d896e46ea6d01bbbd965c50d13d03a (dev) diff dev one
* | 42a54922a38300eb091a714f092136c469718c82 diff master one
|/
* a1fe48e0054054c026554f5ed527e886113b5718 add diff.txt

```

可以看到合并成功，并且合并成功的修改被自动提交，提交信息为`Merge branch 'dev'`，当然，如果在处理冲突完之后使用`git commit -am “message”`也是可以完成合并的，只是提交信息是自定义的message而不是`Merge branch 'dev'`，其实`Merge branch 'dev'`就蛮好的，清楚明了。

### 理解

合并分支，**只要两个分支各自都分别有新的提交，那么出现冲突是常态。**更关键的是，**如果两个分支对同一个文件进行大幅度的不同的修改，那么合并分支是一件非常蛋疼的事情**，所以建议，**团队开发项目的时候，尽量避免两个人同时修改同一个文件的同一部分，严格按照面向对象编程**，不同的人负责不同的功能。同时推荐使用图形化合并分支工具[Git diffmerge ](https://cloud.tencent.com/developer/article/1622950)，生活好难，使用gui工具不丢人。

## 尝试使用cherry-pick

git cherry-pick  应用一些现有提交引入的更改

给定一个或多个现有提交，应用每个提交的更改，为每个提交记录一个新提交。这要求你的工作树是干净的（没有来自 HEAD 提交的修改）。

具体可以看下面的图了解`cherry-pick`

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211003183636.png)

### ①当前环境

使用`git log --all --graph --pretty=oneline`查看两个分支的状态

```powershell
>git log --all --graph --pretty=oneline
* c183b66776c6fdf25e9fde9b6f96d84fcd0bb4c1 (HEAD -> dev)        deleted:    main.py
* de561c32bd816415a8451e3360775d2c8f97b740 diff_dev_newfile_two
| * d4c928527c7c7e16b53f918cc7a4834ec223e25f (master)   modified:   diff.txt
| * b4bcbfdc058fb74e5780755b4deebefe743c6f5a    deleted:    diff_master_newfile_two.txt
| * 1c836275343b27b85c3809f93f7c948c58c27b8d diff_master_newfile_two
|/
*   c4992fed883bc3c2fc04694e84f892972a37a050 Merge branch 'dev'
|\
| * 1e04ba0525d896e46ea6d01bbbd965c50d13d03a diff dev one
* | 42a54922a38300eb091a714f092136c469718c82 diff master one
|/
* a1fe48e0054054c026554f5ed527e886113b5718 add diff.txt

```

在进行了[尝试使用merge](#尝试使用merge)之后，两个分支各自进行了若干次提交，`master`分支在几次提交之中，修改了`diff.txt`。`dev`分支增加了`diff_dev_newfile_two.txt`，删除了`main.py`。具体如下：

```powershell
>git diff master --stat
 diff.txt                 | 14 ++------------
 diff_dev_newfile_two.txt |  0
 main.py                  | 29 -----------------------------
 3 files changed, 2 insertions(+), 41 deletions(-)

```

可以说目前两个分支差距比较大了，如果合并分支的话，必定有冲突。

### ②进行`cherry-pick`

使用`git cherry-pick master`

```powershell
>git cherry-pick master
[dev 29fc0b2]   modified:   diff.txt
 Date: Sun Oct 3 18:34:53 2021 +0800
 1 file changed, 12 insertions(+), 2 deletions(-)

```

### ③`cherry-pick`完毕

看到没有，没有出现任何的冲突，并且明明是有三个文件不一样，却只是一个文件改变了，这是为什么呢？因为`cherry-pick`是**"复制"一个提交节点并在当前分支做一次完全一样的新提交，**也就是说它只是复制了`master`分支最近的一次提交，那么我们`cherry-pick`的这次提交到底修改了那些部分呢？我们比较一下`master`最近两次提交的hash值，`git diff d4c9285 b4bcbfd --stat`结果如下：

```powershell
>git diff d4c9285 b4bcbfd --stat
 diff.txt | 14 ++------------
 1 file changed, 2 insertions(+), 12 deletions(-)

```

可以看到我们`cherry-pick`的这次提交只修改了一个文件，所以，`cherry-pick`之后，也只修改了一个文件。

### 理解

**`cherry-pick`只是复制提交，也就是说被`cherry-pick`的节点提交了什么，那么进行`cherry-pick`的节点也将会提交什么。**

甚至提交的message也是一样的，就像我们这次cherry-pick之后，两个节点的提交信息都是`modified:   diff.txt`。

```powershell
* 29fc0b2 (HEAD -> dev)         modified:   diff.txt
* c183b66       deleted:    main.py
* de561c3 diff_dev_newfile_two
| * d4c9285 (master)    modified:   diff.txt

```

具体的，可以看下面的视频，讲解的很棒，来自[Git cherry pick tutorial. How to use git cherry-pick. - YouTube](https://www.youtube.com/watch?v=wIY824wWpu4&ab_channel=Ihatetomatoes)。

<iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/wIY824wWpu4" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

## 尝试使用rebase

rebase是合并命令的另一种选择。合并把两个父分支合并进行一次提交，提交历史不是线性的。衍合在当前分支上重演另一个分支的历史，提交历史是线性的。 本质上，这是线性化的自动的 [cherry-pick](#尝试使用cherry-pick)

变基的作用就是**修整历史，将分支历史并入主线**。

这功能看起没没啥卵用，其实这功能是需要在某些场景下才会有明显作用，比如当我们向他人维护的开源项目提交修改时，肯定要先在自己的分支中进行开发，然后再提交，但如果我们变基后再提交，维护人员就不用进行整合工作了，直接快速合并即可。

![](https://marklodato.github.io/visual-git-guide/rebase.svg)

### ①当前环境

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211005014454.png)

可以看到两个分支各自都有很多新的提交。

两个分支的差别如下

```powershell
git diff dev --stat

 .gitignore               |  4 +++-
 diff_dev_newfile_two.txt |  0
 main.py                  | 29 +++++++++++++++++++++++++++++
 3 files changed, 32 insertions(+), 1 deletion(-)
 
```

可以看到修改了三个文件。

### ②尝试rebase

先切换到dev分支，执行`git rebase master`，它和`cherry-pick`一样都只是复制提交，而没有进行合并操作。

```powershell
> git rebase master
First, rewinding head to replay your work on top of it...
Applying: diff_dev_newfile_two
Applying:       deleted:    main.py

```

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211005023011.png)

可以看到，分支变直线了，然后，切换到master之后，再合并dev。提交历史变得异常整洁。

### ③理解

> 一般我们这样做的目的是为了确保在向远程分支推送时能保持提交历史的整洁——例如向某个其他人维护的项目贡献代码时。 在这种情况下，你首先在自己的分支里进行开发，当开发完成时你需要先将你的代码变基到 `origin/master` 上，然后再向主项目提交修改。 这样的话，该项目的维护者就不再需要进行整合工作，只需要快进合并便可。

## 这几种不同的所谓的操作分支的方式有啥意义？

我认为，我们学的是功能，而不是学的“意义”，“意义”的意思是使用场景，总有一个使用场景会用到特定的功能，可能这个使用场景因为习惯的原因，我们一辈子也遇不到一次，举个极端的例子，如果从始至终就是整个仓库就一条分支，那么就肯定很难遇到合并分支的操作。如果你的团队有几百个人，每个人都负责不同的一小部分，整个仓库有上百条分支，那么变基这事就显得尤为重要，要不然就太乱了，一个芝麻大小的功能更新不配让提交历史变得杂乱。就像git官网说的这样👇

> 至此，你已在实战中学习了变基和合并的用法，你一定会想问，到底哪种方式更好。 在回答这个问题之前，让我们退后一步，想讨论一下提交历史到底意味着什么。
>
> 有一种观点认为，仓库的提交历史即是 **记录实际发生过什么**。 它是针对历史的文档，本身就有价值，不能乱改。 从这个角度看来，改变提交历史是一种亵渎，你使用 *谎言* 掩盖了实际发生过的事情。 如果由合并产生的提交历史是一团糟怎么办？ 既然事实就是如此，那么这些痕迹就应该被保留下来，让后人能够查阅。
>
> 另一种观点则正好相反，他们认为提交历史是 **项目过程中发生的事**。 没人会出版一本书的第一版草稿，软件维护手册也是需要反复修订才能方便使用。 持这一观点的人会使用 `rebase` 及 `filter-branch` 等工具来编写故事，怎么方便后来的读者就怎么写。
>
> 现在，让我们回到之前的问题上来，到底合并还是变基好？希望你能明白，这并没有一个简单的答案。 Git 是一个非常强大的工具，它允许你对提交历史做许多事情，但每个团队、每个项目对此的需求并不相同。 既然你已经分别学习了两者的用法，相信你能够根据实际情况作出明智的选择。
>
> 总的原则是，只对尚未推送或分享给别人的本地修改执行变基操作清理历史， 从不对已推送至别处的提交执行变基操作，这样，你才能享受到两种方式带来的便利。

## LICENSE

本文使用的部分图片来自[图解Git (marklodato.github.io)](https://marklodato.github.io/visual-git-guide/index-zh-cn.html)，Copyright © 2010, [Mark Lodato](mailto:lodatom@gmail.com). Chinese translation © 2012, [wych](mailto:ellrywych@gmail.com). [![img](https://i.creativecommons.org/l/by-nc-sa/3.0/us/80x15.png)](https://creativecommons.org/licenses/by-nc-sa/3.0/us/) 采用[创用CC 姓名标示-非商业性-相同方式分享3.0 美国授权条款](https://creativecommons.org/licenses/by-nc-sa/3.0/us/)授权。本文嵌入的视频视频详细信息点击[YouTube](https://youtu.be/wIY824wWpu4)。除文中已经表明引用的部分之外的其他部分版权来自[![img](https://i.creativecommons.org/l/by-nc-sa/3.0/us/80x15.png)](https://creativecommons.org/licenses/by-nc-sa/3.0/us/)[署名-非商业性使用-相同方式共享 3.0 美国 (CC BY-NC-SA 3.0 US)](https://creativecommons.org/licenses/by-nc-sa/3.0/us/deed.zh) © 2021 苏月晟。

