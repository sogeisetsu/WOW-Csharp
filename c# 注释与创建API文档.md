**注意，本文所讲的API，为基于对代码注释而自动生成的开发文档，API内容为代码中内类及成员的解释，并非Web API或者REST API。**

# DOCFX

在团队开发过程中，一个漂亮的开发文档是至关重要的，它有助于帮助人们快速地理解项目。DOCFX是一个搭建开发文档网站和根据注释生成api文档的工具。

## 第一步 生成简单的文档网站

前往[Releases · dotnet/docfx (github.com)](https://github.com/dotnet/docfx/releases)下载最新版的DOCFX。将其加入环境变量，这样为了方便起见，docfx 命令可以直接从任何地方调用。(例如，对于 Windows，设置 `PATH =%PATH%;d:docfx`)。

1. 运行`docfx init-q`。这个命令生成一个 docfx _ project 文件夹，其下面有默认的 docfx.json 文件。Json 是 docfx 用来生成文档的配置文件。- q 选项意味着使用默认值悄悄地生成项目，您也可以尝试 docfx init，并按照说明提供自己的设置。

2. 运行命令 `docfx docfx_project/docfx.json`。请注意，在该文件夹下生成了一个新的子文件夹 _site。这是生成静态网站的地方。

3. 运行`docfx serve docfx_project/_site`就可以从http://localhost:8080查看生成的网页

   ![walkthrough_simple_homepage.png (1026×640) (dotnet.github.io)](https://dotnet.github.io/docfx/tutorial/walkthrough/images/walkthrough_simple_homepage.png)

