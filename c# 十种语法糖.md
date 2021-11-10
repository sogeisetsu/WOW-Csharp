[TOC]

# c# 部分语法糖讲解

语法糖指计算机语言中添加的某种语法，这种语法对语言的功能并没有影响，但是更方便程序员使用。通常来说使用语法糖能够增加程序的可读性，从而减少程序代码出错的机会。
需要声明的是“语法糖”这个词绝非贬义词，它可以给我们带来方便，是一种便捷的写法，编译器会帮我们做转换，而且可以提高开发编码的效率，在性能上也不会带来损失。

## 一、自动属性

以前：手写私有变量+公有属性
现在：声明空属性，编译器自动生成对应私有成员字段。

写法：输入prop ,连续按两次tab键，自动生成属性。

```c#
		/// <summary>
        /// 自动属性
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 传统属性写法
        /// </summary>
        private string _LoginName;

        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }
```

##  二、隐式类型（`var`）

var定义变量有以下四个特点：
程序员在声明变量时可以不指定类型，由编译器根据值来指定类型
1、必须在定义时初始化
2、一旦初始化完成，就不能再给变量赋与初始值不同类型的值了
3、var要求是局部变量
4、使用var定义变量和object不同，它在效率上和使用强类型方式定义变量完全一样

注意：
a.隐式类型在定义时必须初始化
例如：var name; 错 var name="Tom";正确
b.可以用同类型的其他隐式类型变量来初始化新的隐式类型变量
例如：var v=1;
   var v2=v;
c.也可以用同类型的字面量来初始化隐式类型变量
例如: var v3="hello";
   v3="world";
d.隐式类型局部变量还可以初始化数组而不指定类型
例如: var array=new int[]{1,2,3,4,5}; (注意：赋值运算符左边没有方括号)
e.编译器可以根据变量的初始值“推断”变量的类型
例如： var number=0; 编译后就变成了 int number =0;

## 三、参数默认值和命名参数

  C#方法的可选参数是.net 4.0最新提出的新的功能，对应简单的重载可以使用可选参数和命名参数混合的形式来定义方法，这样就可以很高效的提高代码的运行效率
  设计一个方法的参数时，可以部分或全部参数分配默认值。调用其方法时，可以重新指定分配了默认值的参数，也可以使用默认值。重新指定分配默认值的参数时，可以显式地为指定参数名称赋值；隐式指定的时候，是根据方法参数的顺序，靠C#编译器的推断。

使用的指导原则：
1、可以为方法和有参属性指定默认值
2、有默认值的参数，必须定义在没有默认值的参数之后
3、默认参数必须是常量
4、ref和out参数不能指定默认值

```c#
public class User
    {
        /// <summary>
        /// 自动属性
        /// </summary>
        public string Name { get; set; }

        public string LoginName { get; set; }

        public int Age { get; set; }

        public string  Address { get; set; }

        public string  Password { get; set; }

        //构造函数重载
        public User(string name)
        {
            this.Name = name;
        }

        public User(string name,string loginName)
        {
            this.Name = name;
            this.LoginName = loginName;
        }

        /// <summary>
        /// 默认参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loginName"></param>
        /// <param name="age"></param>
        /// <param name="address"></param>
        /// <param name="password"></param>
        public User(string name,string loginName,int age,string address="上海",string password="1234")
        {
            this.Name = name;
            this.LoginName = loginName;
            this.Age = age;
            this.Address = address;
            this.Password = password;
        }
    }

使用默认值参数和命名参数
 class Program
    {
        static void Main(string[] args)
        {
            //参数默认值:可以给参数赋值也可以使用参数默认值
            //1、使用默认值
            User user = new User("小明","xiaoming",27);
            Console.WriteLine(user.Address);//输出默认值：上海

            //2、给参数赋值
            User user2 = new User("小红", "xiaohong", 28,"北京");
            Console.WriteLine(user2.Address);//输出：北京

            //命名参数：使用默认值参数的时候，指定参数的名称，命名参数要写在所有固定参数的后面
            //好处：适用于有多个默认值参数的情况,根据命名参数，只修改需要修改的默认值
            //使用命名参数只修改密码
            User user3 = new User("小红", "xiaohong", 28,password:"5678");
            Console.WriteLine(user3.Password);//输出：5678

            Console.ReadKey();
        }
    }
```

输出结果：

![img](https://images2015.cnblogs.com/blog/1033738/201611/1033738-20161111162347530-1739288861.png)

## 四、对象初始化器和集合初始化器

```c#
class Program
    {
        static void Main(string[] args)
        {

            //传统的初始化对象的方式
            User zjl = new User();
            zjl.Name = "周杰伦";
            zjl.phone = "12345678956";

            //使用对象初始化器:{},使用对象初始化器，必须提供一个无参的构造函数,可以只给部分属性初始化
            User xiaohong = new User()
            {
                Name = "小红",
                phone = "1232154566",
                Address = "上海"
            };

            Console.WriteLine("姓名:"+xiaohong.Name);//输出：小红

            //集合初始化器
            List<User> listUser = new List<User>()
            {
                xiaohong,
                new User(){Name="张三",Password="1234",Age=12,DeptId="0001"},
                new User(){Name="张四",Password="1234",Age=16,DeptId="0002"},
                new User(){Name="张五",Password="1234",Age=29,DeptId="0003"},
                new User(){Name="张六",Password="1234",Age=18,DeptId="0001"},
                new User(){Name="张七",Password="1234",Age=12,DeptId="0001"}
            };
            Console.ReadKey();
        }
    }
```

##  五、匿名类和匿名方法

匿名类型
有时候你定义的类只是用来封装一些相关的数据，但并不需要相关联的方法、事件和其他自定义的功能。同时，这个类仅仅在当前的应用程序中使用，而不需要在项目间重用。你所需要的只是一个“临时的”类型，现在来看看这个类的定义
internal class oneClass
{
  //定义若干私有数据成员
  //通过属性来封装每个数据成员
}

构建上面的类虽然说不上有多难，但是如果这个类有很多数据成员的话，那么还是要消耗相当时间的。C#3.0提供了匿名类型来轻松完成这个工作。
现在定义一个匿名对象来表示一个人
var aPeople=new {pName="张三",pAge=26,pSex="男"};
现在我们就可以使用属性语法获取和设置对象的各个变量
aPeople.pAge=29;
Console.WriteLine("{0} is {1}years old",aPeople.pName,aPeople.pAge);
匿名类型的嵌套
  刚刚我们定义了表示一个人的匿名类型，现在我们定义一个表示雇员的嵌套匿名类型：
  var Aemployee=new {
    JoinDate="2012-09-23",
    aPeople=new {pName="张三",pAge=26,pSex="男"},
    title=Manager
  };

匿名类型的限制：
1、匿名类型不支持事件、自定义方法和自定义重写
2、匿名类型是隐式封闭的
3、匿名类型的实例创建只使用默认构造函数
4、匿名类型没有提供可供控制的类名称（使用var定义的）

匿名方法
普通方法定义方式，因为方法的存在是为了复用一段代码，所以一般会给方法取个名字，这个方法的引用就可以通过“方法名”调用
匿名方法：
但是有的方法，不需要复用，仅仅是使用一次就够了，所以不需要方法名，这种方法就叫做匿名方法。
匿名方法必须结合委托使用。（潜在的意思就是：尽管没有方法名了，但方法的指针还是存放了某个委托对象中）
注意：
1、在编译后，会为每个匿名方法创建一个私有的静态方法，然后将此静态方法传给委托对象使用
2、匿名方法编译后会生成委托对象，生成方法，然后把方法装入委托对象，最后赋值给声明的委托变量
3、匿名方法可以省略参数，编译的时候会自动为这个方法按照委托签名的参数添加参数

```c#
public class Test
    {
        public static void TestFive()
        {
            //匿名类型:只能使用一次,仅能在当前的项目中使用
            var aPeople = new { pName = "张三", pAge = 26, pAddress = "美国" };
            //嵌套匿名类型
            var aEmployee = new
            {
                JionDate = DateTime.Now,
                Salary = 8000,
                aPeople = new { pName = "张三", pAge = 26, pAddress = "美国" }
            };

            Console.WriteLine(aEmployee.aPeople.pName);//输出：张三

            Console.ReadKey();
        }

        public static void Test()
        {
            //不能使用匿名类型aPeople，aPeople是局部
            Console.WriteLine(aPeople.pName);//错误
        }
    }
```

### 匿名方法

```c#
class Program
    {
        /// <summary>
        /// 声明委托
        /// </summary>
        /// <param name="s"></param>
        delegate void Printer(string s);
        public delegate void PrintEmployee(User u);

        static void Main(string[] args)
        {
            //匿名方法:必须结合委托使用
            Printer p = delegate(string s)
            {
                Console.WriteLine(s);
            };
            //使用匿名方法
            p("Hello World");

            List<User> listUser = new List<User>()
            {
                new User(){Name="张三",Password="1234",Age=12,DeptId="0001"},
                new User(){Name="张四",Password="1234",Age=16,DeptId="0002"},
                new User(){Name="张五",Password="1234",Age=29,DeptId="0003"},
                new User(){Name="张六",Password="1234",Age=18,DeptId="0001"},
                new User(){Name="张七",Password="1234",Age=12,DeptId="0001"}
            };

            //匿名方法只使用一次
            ChangeUserPwd(listUser, delegate(User u) {

                Console.WriteLine(u.Name+"的新密码是:"+u.Password);
            });
            //使用Lambda表达式
            ChangeUserPwd(listUser, u=>
            {
                Console.WriteLine(u.Name + "的新密码是:" + u.Password);
            });
            Console.ReadKey();
        }

        /// <summary>
        /// 批量修改用户的密码并输出修改以后的密码
        /// </summary>
        /// <param name="list"></param>
        /// <param name="callback"></param>
        public static void ChangeUserPwd(List<User> list, PrintEmployee callback)
        {
            int i = 0;
            foreach (User u in list)
            {
                u.Password = u.Password + i.ToString();
                i += 2;
                callback(u);
            }
        }
    }
```

##  六、扩展方法

为什么要有扩展方法，就是为了在不修改源码的情况下，为某个类增加新的方法。
语法：
定义静态类，并添加public的静态方法，第一个参数代表扩展方法的扩展类。它必须放在一个非嵌套、非泛型的静态类中（的静态方法）；它至少有一个参数；第一个参数必须附加this关键字；第一个参数不能有任何其他修饰符（out/ref）.第一个参数不能是指针类型。
注意：
1、C#只支持扩展方法，不支持扩展属性、扩展事件等；
2、方法名无限制，第一个参数必须带this，表示要扩展的类型；
3、扩展方法的命名空间可以使用namespace System,但不推荐；
4、定义扩展方法的类必须是静态类；
5、扩展方法虽然是public的静态方法，但是生成以后是实例方法，使用时需要先实例化对象，通过对象.方法名进行调用扩展方法；

```c#
/// <summary>
    /// 静态类：对Convert进行扩展，增加一个将string转换成int的方法
    /// </summary>
    public static class ConvertExtension
    {
        /// <summary>
        /// 静态方法：this 表示针对this后面的类型进行扩展
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ConvertToInt(this Convert convert,string s)
        {
            int i;
            if (int.TryParse(s, out i))
            {
                return i;
            }
            else
            {
                return 0;
            }
        }
    }

class Program
    {
        static void Main(string[] args)
        {
            //使用扩展方法:扩展方法虽然是public的静态方法，但是生成以后是实例方法，使用时需要先实例化对象，通过对象.方法名进行调用扩展方法
            //扩展方法所在命名空间和使用代码的命名空间必须相同 扩展方法必须是静态类
            Convert convert = new Convert();
            int i= convert.ConvertToInt("abc");

            Console.WriteLine(i);//输出：0

            //方法2
            int j= ConvertExtension.ConvertToInt(convert, "2");
            Console.WriteLine(j);//输出：2

            Console.ReadKey();
        }
    }
```

## 七、内置泛型委托

Action<T>
可以使用Action<T>委托以参数形式传递方法，而不用显示声明自定义的委托。封装的方法必须与此委托定义的方法签名相对应，也就是说，封装的方法必须具有一个通过值传递给它的参数，并且不能有返回值。
通常，这种方法用于执行某个操作。

```c#
	/// <summary>
    /// List扩展方法
    /// </summary>
    public static class ListExtend
    {
        //声明自定义泛型委托
        public delegate void PrintT<T>(T t);

        public static void TEach<T>(this List<T> list, PrintT<T> pt)
        {
            foreach (T t in list)
            {
                pt(t);
            }
        }
    }


class Program
    {

        static void Main(string[] args)
        {
            List<User> listUser = new List<User>()
            {
                new User(){Name="张三",Password="1234",Age=12,DeptId="0001"},
                new User(){Name="张四",Password="1234",Age=16,DeptId="0002"},
                new User(){Name="张五",Password="1234",Age=29,DeptId="0003"},
                new User(){Name="张六",Password="1234",Age=18,DeptId="0001"},
                new User(){Name="张七",Password="1234",Age=12,DeptId="0001"}
            };

            List<Dept> listDept = new List<Dept>()
            {
                new Dept(){DeptId="0001",DeptName="人事部",PepNum=10},
                 new Dept(){DeptId="0002",DeptName="财务部",PepNum=7},
                  new Dept(){DeptId="0003",DeptName="行政部",PepNum=15}
            };


            #region 使用自定义委托

            //打印所有用户信息
            listUser.TEach(PrintUser);

            listDept.TEach(PrintDept);

            #endregion


            #region 使用内置泛型委托

            listUser.ForEach(PrintUser);
            //使用匿名方法
            listUser.ForEach(delegate(User u) { Console.WriteLine(u.Name + " " + u.Password + " " + u.phone); });

            //使用Lambda表达式
            listUser.ForEach(p=>Console.WriteLine(p.Name+" "+p.Password+" "+p.phone));

            listDept.ForEach(new Action<Dept> (delegate(Dept d)
            {
                Console.WriteLine(d.DeptId + " " + d.DeptName + " " + d.PepNum);
            }));

            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// 打印一个用户信息
        /// </summary>
        /// <param name="u"></param>
        public static void PrintUser(User u)
        {
            Console.WriteLine(u.Name+" "+u.Password+" "+u.phone);
        }

        /// <summary>
        /// 打印一个部门信息
        /// </summary>
        /// <param name="d"></param>
        public static void PrintDept(Dept d)
        {
            Console.WriteLine(d.DeptId+" "+d.DeptName+" "+d.PepNum);
        }
    }
```

### delegate

Predicate<T>
表示定义一组条件并确定指定对象是否符合这些条件的方法。
此委托由Array和List<T>类的几种方法使用，用于在集合中搜索元素。返回值为Bool类型

```c#
public static class Extend
    {
        //自定义泛型委托
        public delegate bool CheckDelegate<T>(T t);
        public static List<T> MyFind<T>(this List<T> list, CheckDelegate<T> match)
        {
            List<T> newList = new List<T>();
            foreach (T item in list)
            {
                if (match(item))
                {
                    newList.Add(item);
                }
            }
            return newList;
        }
    }


class Program
    {
        static void Main(string[] args)
        {
            List<User> listUser = new List<User>()
            {
                new User(){Name="张三",Password="1234",Age=12,DeptId="0001"},
                new User(){Name="张四",Password="1234",Age=16,DeptId="0002"},
                new User(){Name="张五",Password="1234",Age=29,DeptId="0003"},
                new User(){Name="张六",Password="1234",Age=18,DeptId="0001"},
                new User(){Name="张七",Password="1234",Age=12,DeptId="0001"}
            };

            List<Dept> listDept = new List<Dept>()
            {
                new Dept(){DeptId="0001",DeptName="人事部",PepNum=10},
                 new Dept(){DeptId="0002",DeptName="财务部",PepNum=7},
                  new Dept(){DeptId="0003",DeptName="行政部",PepNum=15}
            };

            //使用内置Predicate委托
           List<User> newListUser=  listUser.FindAll(new Predicate<User>(delegate(User u){return u.Age>40;}));

            //使用自定义泛型委托
           List<User> list = listUser.MyFind(Match);
           foreach (User u in list)
           {
               Console.WriteLine(u.Name + " " + u.Password + " " + u.phone);
           }
           Console.ReadKey();
        }

        static bool Match(User u)
        {
            if (u.Age > 15)
            {

                return true;
            }
            return false;
        }
    }
```

### Func

Func 返回值类型可以自定义

```c#
class Program
    {
        static void Main(string[] args)
        {
            List<User> listUser = new List<User>()
            {
                new User(){Name="张三",Password="1234",Age=12,DeptId="0001"},
                new User(){Name="张四",Password="1234",Age=16,DeptId="0002"},
                new User(){Name="张五",Password="1234",Age=29,DeptId="0003"},
                new User(){Name="张六",Password="1234",Age=18,DeptId="0001"},
                new User(){Name="张七",Password="1234",Age=12,DeptId="0001"}
            };

            List<Dept> listDept = new List<Dept>()
            {
                new Dept(){DeptId="0001",DeptName="人事部",PepNum=10},
                 new Dept(){DeptId="0002",DeptName="财务部",PepNum=7},
                  new Dept(){DeptId="0003",DeptName="行政部",PepNum=15}
            };

            //可以自定义Func返回值类型
            List<int> newList= listUser.Select(new Func<User, int>(delegate(User u) { return u.Age; })).ToList();
            List<int> list = listUser.Select(p => { return p.Age; }).ToList();
            newList.ForEach(p => Console.WriteLine(p));

            //根据用户信息得到员工信息
            List<Employee> listEmploy= listUser.Select(new Func<User, Employee>(delegate(User u)
                {
                    Employee e = new Employee();
                    if (u.Age > 15)
                    {
                        e.Name = u.Name;
                        e.Phone = u.phone;
                        e.Salary = 5000;
                        return e;
                    }
                    else
                    {
                        return null;
                    }


                })).ToList();
            listEmploy.ForEach(p =>
            {
                if (p != null)
                {
                    Console.WriteLine(p.Name+" "+p.Salary);
                }

            });

            Console.ReadKey();
        }
    }
```

## 八、`Lambda`表达式

Lambda表达式是比匿名方法更简洁的一种匿名方法语法

最基本的Lambda表达式语法:
{参数列表}=>{方法体}
例如：
(int x)=>{returm x+1}
说明：
1、参数列表中的参数类型可以是明确类型或推断类型
2、如果是推断类型，则参数的数据类型将由编译器根据上下文自动推断出来

如果参数列表只包含一个推断类型参数时：
参数列表=>{方法体}
前提：x的数据类型可以根据上下文推断出来
 x =>{returm x+1}

如果方法体只包含一条语句时：
{参数列表}=>表达式
{int x} => x+1;

Lambda表达式示例：
1、多参数，推断类型参数列表，表达式方法体
(x,y) => x*y
2、无参数，表达式方法体
() => Console.WriteLine()
3、多参数，推断类型参数列表，多语句方法体，需要使用{}
(x,y) => {Console.WriteLine(x);Console.WriteLine(y)}

Lambda表达式缩写推演
new Func<string,int>(delegate(string str){return str.Length;});//内置委托
delegate(string str){return str.Length;}//匿名方法
(string str)=>{return str.Length};//Lambda表达式
(str)=>str.Length;//让编译器推断类型
str=>str>Length;//去掉不必要的括弧

```c#
#region Lambda表达式

            //标准语法
            MyDelegate my1 = (string name) => { return "Lambda表达式:hello" + name; };
            Console.WriteLine(my1("tom"));

            //或者(仅有一个参数) 参数列表只包含一个推断类型参数
            MyDelegate my2 = name => { return "Lambda表达式:hello" + name; };
            Console.WriteLine(my2("tom"));

            //或者(方法体只有一条语句)
            MyDelegate my3 = name => "Lambda表达式:hello" + name;
            Console.WriteLine(my3("tom"));

            #endregion
```

## 九、标准查询运算符

标准查询运算符：定义在System.Linq.Enumerable类中的50多个为IEnumerable<T>准备的扩展方法，这些方法用来对它操作的集合进行查询筛选。
筛选集合where:需要提供一个带bool返回值的“筛选器”，从而标明集合中某个元素是否应该被返回。
查询投射：返回新对象集合IEnumerable<TSource> Select()
统计数量int Count()
多条件排序 OrderBy().ThenBy().ThenBy()
集合连接 Join()

```c#
class Program
    {
        static void Main(string[] args)
        {
            List<User> listUser = new List<User>()
            {
                new User(){Name="张三",Password="1234",Age=12,DeptId="0001"},
                new User(){Name="张四",Password="1234",Age=16,DeptId="0002"},
                new User(){Name="张五",Password="1234",Age=29,DeptId="0003"},
                new User(){Name="张六",Password="1234",Age=18,DeptId="0001"},
                new User(){Name="张七",Password="1234",Age=12,DeptId="0001"}
            };

            List<Dept> listDept = new List<Dept>()
            {
                new Dept(){DeptId="0001",DeptName="人事部",PageNum=10},
                new Dept(){DeptId="0002",DeptName="财务部",PageNum=10},
                new Dept(){DeptId="0003",DeptName="行政部",PageNum=10}
            };

            //1.where
            List<User> newListUser = listUser.Where(p => p.Age>12).ToList();
            Console.WriteLine("所有用户姓名");
            listUser.ForEach(p=>Console.WriteLine(p.Name));
            Console.WriteLine("年龄大于12的用户姓名");
            newListUser.ForEach(p=>Console.WriteLine(p.Name));

            //2.order by 排序  多条件排序:先按照年龄降序排序，在安排phone降序排序，最后按照LoginName升序排序
            List<User> list1 = listUser.OrderByDescending(p => p.Age).ThenByDescending(p => p.phone).ThenBy(p => p.LoginName).ToList();

            //3.join:连接查询
            //返回值是非匿名类:返回值是UserDept类型
            List<UserDept> uds = listUser.Join(listDept, u => u.DeptId, d => d.DeptId,
                 (u, d) => new UserDept() { UserName = u.Name, LoginName = u.LoginName, DeptName = d.DeptName }).ToList();
            //循环输出
            uds.ForEach(p=>Console.WriteLine(p.UserName));

            //返回值是匿名类:用var推断类型接收返回值
            var udVar = listUser.Join(listDept, u => u.DeptId, d => d.DeptId,
                 (u, d) => new { UserName = u.Name, LoginName = u.LoginName, DeptName = d.DeptName }).ToList();
            //循环输出
            udVar.ForEach(p => Console.WriteLine(p.UserName));

            #region 4.0 group by 分组

            //4.1 按照集合中的用户的部门编号进行分组
            IEnumerable<IGrouping<string, User>> userGroup = listUser.GroupBy(p => p.DeptId);

            foreach (IGrouping<string, User> group in userGroup)
            {
                Console.WriteLine("部门编号:" + group.Key);
                foreach (User user in group)
                {
                    Console.WriteLine(user.Name + "-" + user.phone + "-" + user.LoginName);
                }

                Console.WriteLine("--------------------------------");
            }
            #endregion

            #region 5.0 分页：Skip+Take  Skip:跳过 Take:取

            //分页前提：数据源按照一定的列进行排序
            List<User> listSource = listUser.OrderBy(p => p.Name).ToList();
            foreach (User user in listSource)
            {
                Console.WriteLine(user.Name);
            }

            Console.WriteLine("--------------");
            //取第一页数据，每页显示2条数据
            List<User> list = GetPageListByIndex(listUser, 1, 2);
            //输出分页结果
            foreach (User user in list)
            {
                Console.WriteLine(user.Name);
            }

            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// 根据页码提取当页数据
        /// </summary>
        /// <param name="listSource">要分页的数据</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页要显示的条数</param>
        /// <returns></returns>
        static List<User> GetPageListByIndex(List<User> listSource, int pageIndex, int pageSize)
        {
            return listSource.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
```

## 十、`LINQ`

Linq:语言集成查询
Linq是一组语言特性和API，使得你可以使用统一的方式编写各种查询。查询的对象包括XML、对象集合、SQL Server数据库等等。
Linq to Objects：主要负责对象的查询
Linq to XML:主要负责XML的查询
Linq to ADO.NET：主要负责数据库的查询
 Linq to SQL
 Linq to DataSet
 Linq to Entities

Linq查询的两种方式：
1、查询方法方式：主要利用System.Linq.Enumerable类中定义的扩展方法和Lambda表达式进行查询
2、查询语句方式：一种更接近SQL语法的查询方式

查询语句VS查询方法
1、CLR本身并不理解查询语句，它只理解查询方法
2、编译器负责在编译时将查询语句翻译成查询方法
3、大部分查询方法都有对应的查询语句形式：Select（）对应select、OrderBy（）对应orderby
4、部分查询方法目前在C#中还没有对应的查询语句，如：Count()和Max()这时只能采用以下替代方案:
  查询方法
  查询语句+查询方法的混合方式

```c#
class Program
    {
        //声明委托
        public delegate string MyDelegate(string name);

        static void Main(string[] args)
        {
            //创建int类型数组,查找其中的偶数并降序排序输出
            int[] arrays = { 5, 2, 0, 66, 4, 32, 7, 1 };

            #region  使用循环实现

            List<int> list = new List<int>();
            foreach (int i in arrays)
            {
                if (i % 2 == 0)
                {
                    list.Add(i);
                }
            }
            //排序
            list.Sort();
            //反转
            list.Reverse();

            Console.WriteLine(string.Join(",", list));


            #endregion

            #region 使用Linq实现
            //查询方法方式
            var intNew = arrays.Where(p => p % 2 == 0)
                .Select(p => p)
                .OrderByDescending(p => p).ToList();

            Console.WriteLine(string.Join(",", intNew));

            //查询语句方式
            var even = from number in arrays
                       where number % 2 == 0
                       orderby number descending
                       select number;
            #endregion

            //Linq新特性

            #region 类型推断
            //类型推断  注意：不要乱用,仅用在Linq中
            var b = true;
            if (b)
            {
                Console.WriteLine("True");
            }
            #endregion

            #region 扩展方法
            //扩展方法 扩展方法所在命名空间和使用代码的命名空间必须相同 扩展方法必须是静态类
            string s = "abc";
            int a = s.ToInt();//方法图标有一个向下的箭头，表示是扩展方法
            Console.WriteLine(a);

            object obj = "sdsf";
           double d=  obj.ToDouble();

           Console.WriteLine(d);
            #endregion


            #region 对象初始化器
            //集合初始化器
            Contact con = new Contact()
           {FirstName="Tom",LastName="Jerry",Email="tom@163.com" };

            Console.WriteLine(con.Email);
            #endregion

            #region 集合初始化器

            List<Contact> listContact = new List<Contact>()
            {
              new Contact(){FirstName="Tom",LastName="Tack",Email="aaa"},
              new Contact(){FirstName="Tom",LastName="jerry",Email="bbb"}
            };

            Console.WriteLine(listContact[0].Email);
            #endregion

            #region 匿名类型

            var item = new {ProductName="Iphone",Price=4000 };
            string info = item.ProductName + "..." + item.Price;
            Console.WriteLine(info);

            #endregion

            //定义委托类型的变量
            MyDelegate my = new MyDelegate(Hello);//MyDelegate my=Hello
            //调用委托
            string strName = my("tom");
            Console.WriteLine(strName);


            #region 匿名方法(只用一次)
            MyDelegate my2 = delegate(string str)
            {
                return "匿名方法:hello" + str;
            };

            //调用
            string name= my2("tom");
            Console.WriteLine(name);
            #endregion

            #region Lambda表达式

            //标准语法
            MyDelegate my1 = (string str) => { return "Lambda表达式:hello" + str; };
            Console.WriteLine(my1("tom"));

            //或者(仅有一个参数) 参数列表只包含一个推断类型参数
            MyDelegate myDel = p => { return "Lambda表达式:hello" + p; };
            Console.WriteLine(my2("tom"));

            //或者(方法体只有一条语句)
            MyDelegate my3 = p => "Lambda表达式:hello" + p;
            Console.WriteLine(my3("tom"));

            #endregion

            //Linq
            //两种查询方式
            //Select
            int[] intArray = { 1,2,3,6,4,90,65,44,9};

            //1、查询方法方式 p:指的是intArray数组中的每一个元素
            var var1 = intArray.Select(p => p + 1);
            Console.WriteLine(string.Join(",", var1));

            //2、查询语句方式
            var var2 = from p in intArray select p + 1;
            Console.WriteLine(string.Join(",", var1));


            //where
            //查询方法
            var var3 = intArray.Where(p => p % 2 == 0);//选择数组中的偶数
            //查询语句(一般以select结尾)
            var3 = from number in intArray
                   where number % 2 == 0
                   select number;

            //多个条件（刷选出数组中大于10的偶数）
            //查询方法1
            var3 = intArray.Where(p => p % 2 == 0 && p > 10);
            //查询方法2:使用自定义谓语条件查询
            var3 = intArray.Where(p => GetCondition(p));

            //查询语句1
            var3 = from number in intArray
                   where number % 2 == 0 && number > 10
                   select number;
            //查询语句2
            var3 = from number in intArray
                   where GetCondition(number)
                   select number;
            Console.WriteLine(string.Join(",", var3));


            //建立一个Contact类型的集合，查找FirstName="tom" && email=""的联系人的LastName,使用4种方式查询
            List<Contact> MyContact = new List<Contact>()
            {
              new Contact(){FirstName="tom",LastName="jorry",Email="Tom@163.com"},
              new Contact(){FirstName="tom",LastName="jack",Email="jack@163.com"},
              new Contact(){FirstName="tom",LastName="jerry",Email="jerry@163.com"}
            };

            //查询方法
            var var4 = MyContact.Where(p => p.FirstName == "tom" && p.Email == "Tom@163.com");
            var4 = MyContact.Where(p => GetContact(p));

            //查询语句
            var4 = from p in MyContact where p.FirstName == "tom" && p.Email == "Tom@163.com" select p;
            var4 = from p in MyContact where GetContact(p) select p;
            foreach(Contact contact in var4)
            {
                Console.WriteLine(contact.LastName);
            }

            //删除重复元素,没有对应的查询语句
            int[] ints = { 1,1,2,3,4,0};
            var var5 = ints.Distinct();
            Console.WriteLine(string.Join(",",var5));

            //排序
            int[] intArrays = { 1,2,4,3,7,8,0};
            var var6 = intArrays.OrderBy(i => i);
            var6 = intArrays.OrderByDescending(i => i);
            //级联调用
            var6 = intArrays.Where(i => i % 2 == 0).OrderBy(i => i);
            var6 = from i in intArrays where i % 2 == 0
                   orderby i select i;//正序排序
            var6 = from i in intArrays where i % 2 == 0
                   orderby i descending select i;//倒序排序
            //查询方法+查询语句，混合使用
            var6 = (from i in intArrays where i % 2 == 0 select i).OrderBy(i => i);
            Console.WriteLine(string.Join(",", var6));

            //复杂查询
            List<Employee> listEmployee = new List<Employee>()
            {
               new Employee(){
                   FirstName="唐僧",
                   LastName="玄奘",
                   Sex="男",
                   Age=30,
                   Country="大唐"
               },
                new Employee(){
                   FirstName="白骨精",
                   LastName="晶晶",
                   Sex="女",
                   Age=200,
                   Country="古墓"
               },
                new Employee(){
                   FirstName="孙悟空",
                   LastName="行者",
                   Sex="男",
                   Age=500,
                   Country="傲来国"
               },
                new Employee(){
                   FirstName="紫霞",
                   LastName="仙子",
                   Sex="女",
                   Age=100,
                   Country="天界"
               }
            };

            //分组,按照性别分组
            var var7 = from p in listEmployee group p by p.Sex;
            var7 = listEmployee.GroupBy(p => p.Sex);
            foreach (var group in var7)
            {
                Console.WriteLine("分组:"+group.Key);
                foreach (var v in group)
                {
                    Console.WriteLine(v.FirstName);
                }
            }
            Console.ReadKey();
        }

        static bool GetContact(Contact item)
        {
            if (item.FirstName == "tom" && item.Email == "Tom@163.com")
                return true;
            else
                return false;
        }

        static bool GetCondition(int i)
        {
            if (i % 2 == 0 && i > 10)
                return true;
            else
                return false;
        }

        static string Hello(string name)
        {
            return "hello" + name;
        }
    }
```

### 查询语句

```c#
class Program
    {
        static void Main(string[] args)
        {
            List<User> listUser = new List<User>()
            {
                new User(){Name="张三",Password="1234",Age=12,DeptId="0001"},
                new User(){Name="张四",Password="1234",Age=16,DeptId="0002"},
                new User(){Name="张五",Password="1234",Age=29,DeptId="0003"},
                new User(){Name="张六",Password="1234",Age=18,DeptId="0001"},
                new User(){Name="张七",Password="1234",Age=12,DeptId="0001"}
            };

              List<Dept> listDept = new List<Dept>()
            {
                new Dept(){DeptId="0001",DeptName="人事部",PageNum=10},
                new Dept(){DeptId="0002",DeptName="财务部",PageNum=10},
                new Dept(){DeptId="0003",DeptName="行政部",PageNum=10}
            };

            //1、从老集合中查询每一个元素存放到新集合
            var newList = from p in listUser select p;

            //2、带where条件
            var list = from p in listUser where p.Age > 12 && p.Address == "上海" select p;

            //3、OrderBy排序:按照姓名、年龄升序排序
            var newListUser = from p in listUser orderby p.Name orderby p.Age ascending  select p;

            //4、Join
            var joinResult = from u in listUser
                             join d in listDept
                             on u.DeptId equals d.DeptId
                             select new {UserName = u.Name, LoginName = u.LoginName, DeptName = d.DeptName  };
            //遍历
            foreach (var item in joinResult)
            {
                Console.WriteLine(item.DeptName);
            }

            //5、 group by 分组查询
            var groupList = from u in listUser group u by u.DeptId;
            //遍历
            foreach (var group in groupList)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item.ToString());
                }
            }

            Console.ReadKey();
        }
    }
```

 

# LICENSE

部分内容转载自[C#十种语法糖 - .NET开发菜鸟 - 博客园 (cnblogs.com)](https://www.cnblogs.com/dotnet261010/p/6055092.html)，对其进行了用markdown语法的装饰，使其更易于阅读。转载内容版权属于原作者。

除声明转载的部分之外，版权声明如下：

copyright © 2021 苏月晟，版权所有。

<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="知识共享许可协议" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png" /></a><br />由<span xmlns:cc="http://creativecommons.org/ns#" property="cc:attributionName">苏月晟</span>采用<a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">知识共享署名-非商业性使用-相同方式共享 4.0 国际许可协议</a>进行许可。

