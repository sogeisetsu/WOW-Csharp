**注意，本文所讲的API，为基于对代码注释而自动生成的开发文档，API内容为代码中内类及成员的解释，并非Web API或者REST API。**

# DOCFX

在团队开发过程中，一个漂亮的开发文档是至关重要的，它有助于帮助人们快速地理解项目。DOCFX是一个搭建开发文档网站和根据注释生成api文档的工具。DOCFX极其强大，自定义程度极高，缺点是自动化程度不高，使用起来略显麻烦，比如已经2021年了，官方竟然还不支持自动生成目录，好在其有很多与其相关的开源项目，可以一定程度上弥补它的缺憾。如果将DOCFX用好了，它就不仅仅是API文档生成器，还是一个简单的博客网站构建器，DOCFX由微软旗下的dotnet开源，微软的MSDN的构建就用到了DOCFX。遗憾的是DOCFX目前官方只支持.Net和JavaScript，但是它提供了`Generate Metadata`的步骤，理论上它可以支持任何语言（DocFX is designed to support any language），GitHub上基本上常见的语言都有针对DOCFX的开源项目。本文对DOCFX的讲解不及其功能的十分之一，但是基本上可以应对日常的需要，如果想要进一步了解DOCFX，请前往[DocFX - static documentation generator | DocFX website (dotnet.github.io)](https://dotnet.github.io/docfx/index.html)。本文所使用由DOCFX生成的API文档项目可以前往[sogeisetsu/WOW-Csharp at docfx_example (github.com)](https://github.com/sogeisetsu/WOW-Csharp/tree/docfx_example)查看源码。

## 第一步 生成简单的文档网站

前往[Releases · dotnet/docfx (github.com)](https://github.com/dotnet/docfx/releases)下载最新版的DOCFX。将其加入环境变量，这样为了方便起见，docfx 命令可以直接从任何地方调用。(例如，对于 Windows，设置 `PATH =%PATH%;d:docfx`)。

1. 运行`docfx init-q`。这个命令生成一个 `docfx_project` 文件夹，其下面有默认的 `docfx.json` 文件。**Json 是 docfx 用来生成文档的配置文件。**`-q` 选项意味着使用默认值悄悄地生成项目，您也可以尝试 `docfx init`，并按照说明提供自己的设置。

2. 运行命令 `docfx docfx_project/docfx.json`。请注意，**在该文件夹下生成了一个新的子文件夹 `_site`。这是生成静态网站的地方。**

3. 运行`docfx serve docfx_project/_site`就可以从http://localhost:8080查看生成的网页。如果未使用端口 8080，docfx 将在 http://localhost:8080 下托管 `_site`。如果8080正在使用，可以**使用`docfx serve _site -p <port>`更改docfx使用的端口。**

   ![walkthrough_simple_homepage.png (1026×640) (dotnet.github.io)](https://dotnet.github.io/docfx/tutorial/walkthrough/images/walkthrough_simple_homepage.png)

### 向网站添加文章

在进行初始化和创建网站之后，当前的`docfx_project`文件结构应该是这样的：

```bash
.
├── _site
├── api
├── apidoc
├── articles
├── docfx.json
├── images
├── index.md
├── obj
├── src
└── toc.yml
```

各个文件和文件夹的作用如下：

**/** - 这个网站的根目录，包含:

- **docfx.json** - docfx 依赖的配置文件。**所有的命令及其涉及到的文件都会用`docfx.json`来配置。**
- **index.md** - 用来创建网站的首页。
- **toc.yml** – 呈现为导航菜单栏，显示在网站每个页面的顶部。

**/articles** - 里面放着一些markdown文件。这些markdown文件的图片放在`/images`下。 这些 Markdown 文件发布在菜单栏的**Ariticles**部分下。

**/src** - 包含可选的 .NET 语言项目文件 (*.csproj)，其中包含用于生成托管 API 文档的类型信息。

**/apidoc** - 包含用于覆盖根据注释中自动生成的文本的 Markdown 文件。

运行 DocFx 后，将创建其他文件夹： 

**/_site** - 包含由 DocFx 生成的所有站点文件，包括网站所需的生成的 HTML/JSON/JS/Images 文件。

#### 修改首页

可以修改根目录下的index.md来修改网站的首页

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211205144015.png)

#### 将更多的文章放到网站

1. 将更多`.md`文件放入`articles`，例如`快速开始.md`，`演练.md`，`进阶.md`。如果文件引用了任何资源，请将这些资源放入`images`文件夹中。

2. 为了组织这些文章，我们将这些文件添加到`/articles/toc.yml`（也可以使用工具[Release 用于自动生成docfx文档 · whuanle/CZGL.DocfxBuild.Yml (github.com)](https://github.com/whuanle/CZGL.DocfxBuild.Yml/releases/tag/1.0)自动生成目录）。内容`toc.yml`如下：

   ```yaml
   - name: 快速开始
     href: intro.md
   - name: 演练
     href: 演练.md
   - name: 进阶
     href: 进阶.md
   ```

   现在`articles`文件夹的布局是：

   ```bash
   .
   ├── intro.md
   ├── toc.yml
   ├── 演练.md
   └── 进阶.md
   ```

3. 在`docfx_project`文件夹下运行`docfx`和`docfx serve _site`，然后就可以看到已经有文章加入:

   ![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211205145707.png)

#### 修改导航菜单栏

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211205145855.png)

导航菜单栏默认有两个选项，分别是`Articles`和`API Documentation`，可以通过修改根目录下的`toc.yml`来修改导航菜单栏的名称：

```yml
- name: 开始
  href: articles/
- name: Api 文档
  href: api/
  # homepage来定义api的首页
  homepage: api/index.md
```

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211205150715.png)

也可以向导航菜单栏增加新的选项，比如说，在根目录新建一个文件夹，命名为`blog`，该文件夹结构如下：

```bash
.
├── GIT 的merge、rebase和cherry-pick.md
├── Google Fonts注意事项.md
├── Linux笔记.md
├── issue trans Problem Description.md
├── python 包（package）和模块（module）的创建和引入（import）.md
├── toc.yml
├── unix bsd linux shell bash GNU之间的联系，歪讲Linux(一).md
├── vscode git 无需命令行.md
├── 一.md
├── 关于若干问题的解释说明.md
├── 商品上架格式.md
├── 如何在印刷品中使用遵循SIL Open Font License协议的字体.md
├── 对微信支付文档的自我理解.md
├── 我的python加密方案.md
├── 找人.md
├── 日语 时间的量.md
├── 日语学习资源的分享.md
├── 说明.md
├── 贝多芬小传.md
├── 还没有学会告别，就已经后会无期。.md
├── 雷电接口.md
└── 青岛科技大学新生报考参考.md
```

在该文件夹内添加`toc.yml`，内容如下：

```yaml
### D:\blog
- name: GIT 的merge、rebase和cherry-pick
  href: GIT 的merge、rebase和cherry-pick.md
- name: Google Fonts注意事项
  href: Google Fonts注意事项.md
- name: issue trans Problem Description
  href: issue trans Problem Description.md
- name: Linux笔记
  href: Linux笔记.md
- name: python 包（package）和模块（module）的创建和引入（import）
  href: python 包（package）和模块（module）的创建和引入（import）.md
- name: unix bsd linux shell bash GNU之间的联系，歪讲Linux(一)
  href: unix bsd linux shell bash GNU之间的联系，歪讲Linux(一).md
- name: vscode git 无需命令行
  href: vscode git 无需命令行.md
- name: 一
  href: 一.md
- name: 关于若干问题的解释说明
  href: 关于若干问题的解释说明.md
- name: 商品上架格式
  href: 商品上架格式.md
- name: 如何在印刷品中使用遵循SIL Open Font License协议的字体
  href: 如何在印刷品中使用遵循SIL Open Font License协议的字体.md
- name: 对微信支付文档的自我理解
  href: 对微信支付文档的自我理解.md
- name: 我的python加密方案
  href: 我的python加密方案.md
- name: 找人
  href: 找人.md
- name: 日语 时间的量
  href: 日语 时间的量.md
- name: 日语学习资源的分享
  href: 日语学习资源的分享.md
- name: 说明
  href: 说明.md
- name: 贝多芬小传
  href: 贝多芬小传.md
- name: 还没有学会告别，就已经后会无期。
  href: 还没有学会告别，就已经后会无期。.md
- name: 雷电接口
  href: 雷电接口.md
- name: 青岛科技大学新生报考参考
  href: 青岛科技大学新生报考参考.md
```

修改根目录的`toc.yml`，将blog文件夹加进去：

```yaml
- name: 开始
  href: articles/
- name: Api 文档
  href: api/
  homepage: api/index.md
- name: 博客
  href: blog/
  homepage: blog/GIT 的merge、rebase和cherry-pick.md
```

然后修改根目录下的`docfx.json`，这是依赖的配置文件。关于它的用法后面再讲，在`docfx.json`修改build的content下添加:

```json
{
    "files": [
        "blog/**.md",
        "blog/**/toc.yml",
        "toc.yml",
        "*.md"
    ]
}
```

在`docfx_project`文件夹下运行`docfx`和`docfx serve _site`，然后就可以看到已经有文章加入:

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211205154207.png)

## 第二步 向网站添加 API 文档

向/src文件夹下添加一个c#项目，要包含csproj文件。

```c#
├── src
   ├── ConsoleApp1.csproj
   ├── Program.cs
   ├── bin
   ├── newLei.cs
   └── obj
```

在`docfx_project`文件夹下运行`docfx`和`docfx serve _site`，然后就可以看到已经有根据注释自动生成的API文章加入:

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211205185532.png)

## 在左侧导航加入其他文件夹的内容

当前的根目录下的toc.yml是这样的：

```yaml
- name: 开始
  href: articles/
- name: Api 文档
  href: api/
  homepage: api/index.md
- name: 博客
  href: blog/
  homepage: blog/GIT 的merge、rebase和cherry-pick.md
```

这表示`开始`这一菜单下只会包含`articles`文件夹下面的内容。

现在在根目录新建一个文件夹，命名为`anycombine`，在该文件夹下面放置一个toc.yml，内容如下：

```yaml
- name: Articles
  href: ../articles/toc.yml
- name: Blog
  href: ../blog/toc.yml
```

然后修改根目录下的toc.yml：

```yaml
- name: 开始
  href: anycombine/
- name: Api 文档
  href: api/
  homepage: api/index.md
- name: 博客
  href: blog/
  homepage: blog/GIT 的merge、rebase和cherry-pick.md
```

最后在`docfx.json`里面添加`anycombine`文件夹，让其在创建网页时能够包含这个文件夹：

```json
  "build": {
    "content": [
      {
        "files": "anycombine/**"
      },
```

然后创建网站，网站上导航菜单栏中的`开始`选项包含`articles`和`blog`两个文件夹的内容：

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211205222709.png)

## 导出为PDF文档

之前已经讲解了如何生成网站，现在讲解如何将网站上面的内容转为PDF，先下载一个开源工具 [wkhtmltopdf](https://wkhtmltopdf.org/)，可以前往[wkhtmltopdf](https://wkhtmltopdf.org/downloads.html)根据自己电脑的规格来选择版本。安装或解压之后，将其放置在环境变量，方便以后调用，可以在power shell使用`setx PATH "%PATH%;D:\wkhtmltopdf\bin"`来将其放入环境变量。

然后在之前docfx生成的文件夹的根目录下创建一个文件夹，名为`pdf`，在里面创建一个`toc.yml`来包含需要生成PDF的目录：

```yaml
- name: Articles
  href: ../articles/toc.yml
- name: Blog
  href: ../blog/toc.yml
- name: API 文档
  href: ../api/toc.yml
```

接下来，需要将*pdf*部分添加到`docfx.json`，只有这样，才能在执行docfx的时候来将其转换为pdf，增加一个pdf属性，**在`docfx.json`排除了 TOC 文件，因为每个 TOC 文件都会生成一个 PDF 文件**，内容如下：

```json
"pdf": {
    "content": [
        {
            "files": [
                "api/**.yml"
            ],
            "exclude": [
                "**/toc.yml",
                "**/toc.md"
            ]
        },
        {
            "files": [
                "articles/**.md",
                "articles/**/toc.yml",
                "toc.yml",
                "*.md"
            ],
            "exclude": [
                "**/toc.yml",
                "**/toc.md"
            ]
        },
        {
            "files": [
                "blog/**.md",
                "blog/**/toc.yml",
                "toc.yml",
                "*.md",
                "pdf/*"
            ],
            "exclude": [
                "**/toc.yml",
                "**/toc.md"
            ]
        },
        {
            "files": "pdf/toc.yml"
        }
    ],
    "resource": [
        {
            "files": [
                "images/**"
            ]
        }
    ],
    "overwrite": [
        {
            "files": [
                "apidoc/**.md"
            ],
            "exclude": [
                "obj/**",
                "_site/**"
            ]
        }
    ],
    "wkhtmltopdf": {
        "additionalArguments": "--enable-local-file-access"
    },
    "dest": "_site_pdf"
}
```

然后在docfx项目的根目录运行docfx，就可以在`_site_pdf`文件夹看到pdf文件了。**提示：这一过程可能会花费比较长的时间，建议喝杯咖啡等待。**

![](https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211206000157.png)

其实，说实话，html的表现效果一定要比PDF文件强，这个是毋庸置疑的。

### DOCFX 常用命令

现在根据`docfx.json`中的设置，有下面几个命令（**之所以放到这里说是因为易懂性方面的考虑**）：

- `docfx metadata` 根据src目录下的项目来修改api文件夹的内容，即生成API文档。
- `docfx build` 生成网站源码，文件放在`_site`文件夹下。
- `docfx serve <_site文件夹位置>`  创建本地服务器，可以本地访问API网站内容
- `docfx pdf` 导出PDF。
- `docfx` 执行`docfx metadata`、`docfx build`和`docfx pdf`。

应该可以看到，DOCFX的命令的内容甚至是作用都和`docfx.json`中的配置是息息相关的，这个配置文件其实很好理解，主要记住`content`属性会规定命令会波及到哪些文件和会放过那些文件其实就可以了。遇到别的需求可以查阅[docfx.exe User Manual | DocFX website (dotnet.github.io)](https://dotnet.github.io/docfx/tutorial/docfx.exe_user_manual.html#3-docfxjson-format)，里面详细地记载了`docfx.json`中每个属性的作用。

## 自定义

### template

运行命令`docfx template list`，可看到内置的模板列表：

```bash
Existing embedded templates are:
        common
        default(zh-cn)
        default
        pdf.default
        statictoc
```

可以在`docfx.json`中的build属性的template属性中设置模板：

```json
"template": [
    "default(zh-cn)"
],
```

也可以在命令行中进行类似`-t statictoc`的操作，比如使用`docfx build -t statictoc`之后的网站是一个静态网站，可以通过本地文件系统预览而非服务器环境。

<img src="https://suyuesheng-biaozhun-blog-tupian.oss-cn-qingdao.aliyuncs.com/blogimg/20211209160149.png" style="zoom:70%;" />

### 导出template

使用`docfx template export default`可已导出默认的HTML模板，将有名为 `_exported_templates` 的文件夹添加到根目录下，其中包含名为`default`的文件夹，即`default`模板的HTML。

### 创建template

创建一个文件夹，比如`templates`，然后将导出的`_exported_templates` 中的`default`文件夹复制到`templates`中。

- `templates`中的`default`现在就是创建的一个新的模板文件夹，可以修改其中的内容来自定义。
  
  - 修改`partials/footer.tmpl.partial`来修改页脚版权，建议只修改`<span>Generated by <strong>DocFX</strong></span>`部分来声明自己的版权，不要修改微软对DOCFX的版权。
  
    ```html
    {{!Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See LICENSE file in the project root for full license information.}}
    
    <footer>
      <div class="grad-bottom"></div>
      <div class="footer">
        <div class="container">
          <span class="pull-right">
            <a href="#top">{{__global.backToTop}}</a>
          </span>
          {{{_appFooter}}}
          {{^_appFooter}}<span>Copyright (c) 2021 苏月晟，版权所有。<br>通过<strong>DocFX</strong>生成</span>{{/_appFooter}}
        </div>
      </div>
    </footer>
    
    ```
  
  - 有一个默认模板叫做`default(zh-cn)`，可以将文档改为中文，在`docfx.json`将其放到里面来实现中文，并将自定义模板放到`default(zh-cn)`后面，因为在`docfx.json`的`build`的`template`中是后面的覆盖前面的，这样就不用再一个个修改自己自定义的模板了。如果想自定义中文可以修改模板文件中的`partials/title.tmpl.partial`和`token.json`两个文件来自定义中文名称。
  
    ```json
    "template": [
        "templates/default",
        "default(zh-cn)"
    ],
    ```
  
  - 对于修改图标和logo有两个方式，一个是参考[docfx.exe User Manual | DocFX website (dotnet.github.io)](https://dotnet.github.io/docfx/tutorial/docfx.exe_user_manual.html)在`docfx.json`中进行修改，另一个是直接替换模板文件中的logo.svg和favicon.ico，可以修改`partials\logo.tmpl.partial`来取消`class=“svg”`从而取消logo的动画，并可以调整大小，这里附上我自己对`partials\logo.tmpl.partial`的修改，仅供参考。
  
    ```html
    <a class="navbar-brand" href="{{_rel}}index.html">
      <img id="logo" src="{{_rel}}{{{_appLogoPath}}}{{^_appLogoPath}}logo.svg{{/_appLogoPath}}" height="46" width="46" alt="{{_appName}}" >
    </a>
    ```
  
  - 

# LICENSE

copyright © 2021 苏月晟，版权所有。

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />本<span xmlns:dct="http://purl.org/dc/terms/" href="http://purl.org/dc/dcmitype/Text" rel="dct:type">作品</span>由<span xmlns:cc="http://creativecommons.org/ns#" property="cc:attributionName">苏月晟</span>采用<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">知识共享署名-非商业性使用-相同方式共享 4.0 国际许可协议</a>进行许可。

