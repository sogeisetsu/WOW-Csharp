**本文主要讲解利用C#和`.NET 5`所提供的方法来解析json这种资料交换格式和xml与html这种标记语言。**

# json、xml和html概述

json在前后端数据传输上用的很多，前后端交互基本上都是用json当作标准的数据传输格式。json也被广泛的用在数据库储存上。XML是一种标记语言，很类似HTML，XML的设计宗旨是传输数据，而非显示数据。JSON相对于XML来讲，数据的体积小，传递的速度更快些。XML的解析得考虑子节点父节点所以解析难度大于Json，但是XML已经被业界广泛的使用，依然有网络API采用XML传输数据，它也是很多开发框架所指定数据格式，比如maven就使用XML来作为包管理的格式。一般情况下，越是复杂的数据用XML来表示会更加易于阅读，而简单的数据用JSON会更加直观。HTML主要是用来在浏览器上显示数据，它的功能最为强大也最为复杂，一般不用它来作为储存数据格式的首选。·

## LICENSE

已将所有引用其他文章之内容清楚明白地标注，其他部分皆为作者劳动成果。对作者劳动成果做以下声明：

copyright © 2021 苏月晟，版权所有。

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />本<span xmlns:dct="http://purl.org/dc/terms/" href="http://purl.org/dc/dcmitype/Text" rel="dct:type">作品</span>由<span xmlns:cc="http://creativecommons.org/ns#" property="cc:attributionName">苏月晟</span>采用<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">知识共享署名-非商业性使用-相同方式共享 4.0 国际许可协议</a>进行许可。

