**本文主要讲解利用C#和`.NET 5`所提供的方法来解析json这种资料交换格式和xml与html这种标记语言。**一般从服务器上获取这些数据的时候，这些格式的数据会以字符串的形式传输，故而本文名为“c# 解析字符串”，事实上，本篇讲解的是解析各种常见的数据格式。

# json、xml和html概述

json在前后端数据传输上用的很多，**前后端交互基本上都是用json当作标准的数据传输格式。json也被广泛的用在数据库储存上。**XML是一种标记语言，很类似HTML，XML的设计宗旨是传输数据，而非显示数据。JSON相对于XML来讲，数据的体积小，传递的速度更快些。XML的解析得考虑子节点父节点所以解析难度大于Json，**但是XML已经被业界广泛的使用，依然有网络API采用XML传输数据，它也是很多开发框架所指定数据格式**，比如maven就使用XML来作为包管理的格式。一般情况下，越是复杂的数据用XML来表示会更加易于阅读，而简单的数据用JSON会更加直观。**HTML主要是用来在浏览器上显示数据，它的功能最为强大也最为复杂，一般不用它来作为储存数据格式的首选**，解析HTML通常是用于爬虫。

# 解析JSON字符串

注意**本文重点讲解的是解析**，而非序列化，关于序列化请查看[WOW-Csharp/数组和集合.md at master · sogeisetsu/WOW-Csharp (github.com)](https://github.com/sogeisetsu/WOW-Csharp/blob/master/数组和集合.md#json序列化和反序列化)。

c#解析json字符串在.NET 5的首选是使用`System.Text.Json`的`JsonDocument`类。

## 格式化输出

想要格式化输出，需要先把字符串转变成一个`JsonDocument`实例化对象，然后在序列化这个对象的时候指定`JsonSerializerOptions`为整齐打印。

```c#
// 先定义一个json字符串
string jsonText = "{\"ClassName\":\"Science\",\"Final\":true,\"Semester\":\"2019-01-01\",\"Students\":[{\"Name\":\"John\",\"Grade\":94.3},{\"Name\":\"James\",\"Grade\":81.0},{\"Name\":\"Julia\",\"Grade\":91.9},{\"Name\":\"Jessica\",\"Grade\":72.4},{\"Name\":\"Johnathan\"}],\"Teacher'sName\":\"Jane\"}";
Console.WriteLine(jsonText);
// 将表示单个 JSON 字符串值的文本分析为 JsonDocument
JsonDocument jsonDocument = JsonDocument.Parse(jsonText);
// 序列化
string formatJson = JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions()
                                             {
                                                 // 整齐打印
                                                 WriteIndented = true,
                                                 Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                                             });
// 格式化输出
Console.WriteLine(formatJson);
```

这个比较麻烦，我们可以将其制作成拓展方法。

```c#
internal static class JsonDocumentExtensions
{
    internal static string JDFormatToString(this JsonDocument jsonDocument)
    {
        return JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions()
                                        {
                                            WriteIndented = true,
                                            Encoder=JavaScriptEncoder.Create(UnicodeRanges.All)
                                        });
    }

    internal static string TOJsonString(this string str)
    {
        JsonDocument jsonDocument = JsonDocument.Parse(str);
        return JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions()
                                        {
                                            WriteIndented = true,
                                            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                                        });
    }
}

```

这样就可以用类似于下面的方法直接调用了：

```c#
// jsondocument 格式化输出为json字符串
string a = jsonDocument.JDFormatToString();
// 格式化字符串
string b = jsonText.TOJsonString();
```

## JSON DOM

**对JSON进行DOM操作.NET提供了两种官方方法，分别是JsonDocumenth和JSonNode**，其中JsonNode提供了**创建可变 DOM** 的能力，它更加强大和简单，但是JsonNode是.NET 6的内容，鉴于.NET 6的稳定版刚刚发布，所以本文还是讲解JsonDocumenth。.NET 6是一个LTS版本，它于2021年11月8日正式发布，会支持到2024年11月8日，详情可以查看[.NET and .NET Core official support policy (microsoft.com)](https://dotnet.microsoft.com/platform/support/policy/dotnet-core#cadence)，**笔者会在.NET 5结束支持之前（2022 年 5 月 8 日）写一篇关于JSonNode的文章作为本文的Patch。**

JsonDocument 提供了使用 Utf8JsonReader 构建只读 DOM 的能力。可以通过 JsonElement 类型访问组成有效负载的 JSON 元素。 JsonElement 类型提供数组和对象枚举器以及用于将 JSON 文本转换为常见 .NET 类型的 API。 JsonDocument 公开一个 RootElement 属性。

关于JsonDocument，MSDN上有一篇非常好的讲解文章[How to use a JSON document, Utf8JsonReader, and Utf8JsonWriter in System.Text.Json | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-5-0#use-jsondocument)，这一部分笔者更多的是采用MSDN上面的例子，此部分在某种意义上可以看作对[How to use a JSON document, Utf8JsonReader, and Utf8JsonWriter in System.Text.Json](https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-5-0#use-jsondocument)的翻译。

用作例子的Json数据如下：

```json
{
    "Class Name": "Science",
    "Teacher's Name": "Jane",
    "Semester": "2019-01-01",
    "Students": [
        {
            "Name": "John",
            "Grade": 94.3
        },
        {
            "Name": "James",
            "Grade": 81
        },
        {
            "Name": "Julia",
            "Grade": 91.9
        },
        {
            "Name": "Jessica",
            "Grade": 72.4
        },
        {
            "Name": "Johnathan"
        }
    ],
    "Final": true
}
```

### 方法概述

先将其反序列化成`JsonDocument`对象：

```c#
Console.WriteLine("对json字符串进行dom操作");
string jsonText = "{\"ClassName\":\"Science\",\"Final\":true,\"Semester\":\"2019-01-01\",\"Students\":[{\"Name\":\"John\",\"Grade\":94.3},{\"Name\":\"James\",\"Grade\":81.0},{\"Name\":\"Julia\",\"Grade\":91.9},{\"Name\":\"Jessica\",\"Grade\":72.4},{\"Name\":\"Johnathan\"}],\"Teacher'sName\":\"Jane\"}";
JsonDocument jsonDocument = JsonDocument.Parse(jsonText);
```

获取当前`JsonDocument`的根元素（`JsonElement`类型）：

```c#
JsonElement root = jsonDocument.RootElement;
```

`RootElement`是json数据的根，后续所有的操作都与其息息相关。

`GetProperty`根据键名，获取根元素下的元素（`JsonElement`类型）：

```c#
JsonElement students = root.GetProperty("Students");
```

`GetArrayLength`获取**数组**属性的长度（如果将此方法用于非数组类型的json值会报错）：

```c#
// 获取数组长度
Console.WriteLine(students.GetArrayLength());
```

可以对值类型为数组的`JsonElement`使用`EnumerateArray ()`方法来获取枚举器（IEnumerator），从而进行**循环**操作：

```c#
// EnumerateArray 一个枚举器，它用于枚举由该 JsonElement 表示的 JSON 数组中的值。
foreach (JsonElement student in students.EnumerateArray())
{
    Console.WriteLine(student);
    Console.WriteLine(student.ValueKind);// object
    // 获取属性Name的string值
    Console.WriteLine(student.GetProperty("Name").GetString());
}
```

#### 获取值

对于`JsonElement`获取元素值的方式比较复杂，首先需要知道值的类型，然后根据值的类型来选择方法，方法列表可以从[JsonElement 结构 (System.Text.Json) | Microsoft Docs](https://docs.microsoft.com/zh-cn/dotnet/api/system.text.json.jsonelement?view=net-6.0#methods)查看，比如值的类型是double，就使用[GetDouble()](https://docs.microsoft.com/zh-cn/dotnet/api/system.text.json.jsonelement.getdouble?view=net-6.0#System_Text_Json_JsonElement_GetDouble)来获取json数字（double类型）:

```c#
Console.WriteLine(student.GetProperty("Grade").GetDouble());
```

如果当前的值是string类型，就使用`GetString()`来获取json字符串：

```c#
Console.WriteLine(semester.GetString());
```

总之，为了获取准确的json值，必须提前知道json值的类型。这样才会最大限度保证不会出错。

#### 获取和判读Json值的类型

可以使用`JsonElement`的`ValueKind`属性来获取值类型：

```c#
Console.WriteLine(students.ValueKind);
```

`ValueKind`属性的类型是名为`JsonValueKind`的枚举类型。`JsonValueKind`的字段如下：

| Array     | 2    | JSON 数组。                                                  |
| --------- | ---- | ------------------------------------------------------------ |
| False     | 6    | JSON 值 **false**。                                          |
| Null      | 7    | JSON 值 **null**。                                           |
| Number    | 4    | JSON 数字。                                                  |
| Object    | 1    | JSON 对象。                                                  |
| String    | 3    | JSON 字符串。                                                |
| True      | 5    | JSON 值 **true**。                                           |
| Undefined | 0    | 没有值（不同于 [Null](https://docs.microsoft.com/zh-cn/dotnet/api/system.text.json.jsonvaluekind?view=net-6.0#System_Text_Json_JsonValueKind_Null)）。 |

故可以使用像下面这种判断相等的方式来检测数据类型：

```c#
Console.WriteLine(students.ValueKind == JsonValueKind.Array); // true
```

#### 检查属性是否存在

可以使用`TryGetProperty`方法来根据键来判断元素是否存在，demo如下：

```c#
root.TryGetProperty("Name", out JsonElement value)
```

存在就返回true，不存在就返回false，`TryGetProperty`的第一个参数是键名，第二个参数是用out关键字修饰的JsonElement类型，如果此属性存在，会将其值分配给 `value` 参数。

借助`TryGetProperty`既可以判断属性是否存在，也能在属性存在的情况下获取该属性对应的`JsonElement`，demo：

```c#
// 检查存在
Console.WriteLine(root.TryGetProperty("Semester", out JsonElement value));
// 使用被分配的JsonElement
Console.WriteLine(value.GetString());
```

MSDN的demo更具备实用性：

```c#
if (student.TryGetProperty("Grade", out JsonElement gradeElement))
{
    sum += gradeElement.GetDouble();
}
else
{
    sum += 70;
}
```

### 如何在 `JsonDocument` 和 `JsonElement` 中搜索子元素

对 JsonElement 的搜索需要对属性进行顺序搜索，因此速度相对较慢（例如在使用 TryGetProperty 时）。 System.Text.Json 旨在最小化初始解析时间而不是查找时间。因此，在搜索 JsonDocument 对象时使用以下方法来优化性能：

- 使用内置的枚举器（EnumerateArray 和 EnumerateObject）而不是自己做索引或循环。不要对**数组形式的`JsonElement`**进行诸如`students[1]`的操作。
- 不要使用 RootElement 通过每个属性对整个 JsonDocument 进行顺序搜索。相反，**根据 JSON 数据的已知结构搜索嵌套的 JSON 对象。**也就是说不要进行不必要的搜索，要根据自己对所操作的JSON的最大了解程度来进行搜索，比如明知道某个json数组里面没有自己想要的数据，就别去对它进行一遍又一遍的搜索。

### JsonDocument 是非托管资源

因为是非托管资源，其不会在CLR中被托管。JsonDocument 类型实现了 IDisposable ，为了内存的整洁应该在using块中使用，使其在使用完之后立即被释放资源，就像下面这样：

```c#
using (JsonDocument jsonDocument = JsonDocument.Parse(jsonText))
{
    // 对jsonDocument的各种操作
}
```

前面笔者没有将其放在using块里面纯粹是为了演示方便，请大家注意在using块中使用JsonDocument 才是推荐的用法。

#### 函数调用JsonDocument

如果因为某种原因需要将JsonDocument转给方法调用者，则仅从您的 API 返回 JsonDocument，在大多数情况下，这不是必需的。返回返回 RootElement 的 Clone就好，它是一个 JsonElement。demo如下：

```c#
public JsonElement LookAndLoad(JsonElement source)
{
    string json = File.ReadAllText(source.GetProperty("fileName").GetString());

    using (JsonDocument doc = JsonDocument.Parse(json))
    {
        return doc.RootElement.Clone();
    }
}

```

如果你所编写的方法收到一个 JsonElement 并返回一个子元素，则没有必要返回子元素的克隆。调用者负责使传入的 JsonElement 所属的 JsonDocument 保持活动状态即可。demo如下：

```csharp
public JsonElement ReturnFileName(JsonElement source)
{
   return source.GetProperty("fileName");
}
```



# LICENSE

已将所有引用其他文章之内容清楚明白地标注，其他部分皆为作者劳动成果。对作者劳动成果做以下声明：

copyright © 2021 苏月晟，版权所有。

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />本<span xmlns:dct="http://purl.org/dc/terms/" href="http://purl.org/dc/dcmitype/Text" rel="dct:type">作品</span>由<span xmlns:cc="http://creativecommons.org/ns#" property="cc:attributionName">苏月晟</span>采用<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">知识共享署名-非商业性使用-相同方式共享 4.0 国际许可协议</a>进行许可。

