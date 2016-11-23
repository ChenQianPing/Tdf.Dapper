# Tdf.Dapper

Tdf.Dapper是一个开源库，他在Dapper的基础上封装了基本的CRUD操作，分页查询，使得一些简单的数据库操作可以不用自己写sql语句。使用起来更方便。

Tdf.Dapper is an open source library, he Dapper based on the package of the basic CRUD operations, paging query, so that some simple database operations can not write their own SQL statements. More convenient to use.

详见：[轻量级ORM框架—Dapper](http://www.jianshu.com/p/b11451fc538c)

- [https://www.nuget.org/packages/Tdf.Dapper/](https://www.nuget.org/packages/Tdf.Dapper/)

## Insert methods

```
var action = new Action();
action.ActionName = "编辑";
action.ActionValue = 2;

var result = await new DapperRepositoryBase<Action>().InsertAsync(action);
if (result == 0)
{
	Console.WriteLine("添加成功");
}
```

## Delete methods

```
var action = new Action();
action.ActionId = new Guid("8D48FB03-9742-4BA5-A018-54237915E6FC");

var result = await new DapperRepositoryBase<Action>().DeleteAsync(action);
if (result)
{
	Console.WriteLine("删除成功");
}
else
{
	Console.WriteLine("删除失败");
}
```

## Update methods

```
var action = new Action();
action.ActionId = new Guid("E5C72887-A6C5-4864-B994-8A44B197EE61");
action.ActionName = "导入";
action.ActionValue = 64;

var result = await new DapperRepositoryBase<Action>().UpdateAsync(action);
if (result != null)
{
	Console.WriteLine("更新成功");
}
else
{
	Console.WriteLine("更新失败");
}
```


## Get methods

**Get one specific entity based on id**

```
var actionId = new Guid("C017765C-DE4B-45D1-83AC-C3373072BE3E");
var entity = await new DapperRepositoryBase<Action>().GetAsync(actionId);
if (entity != null)
{
	Console.WriteLine(entity.ActionId + " " + entity.ActionName + " " + entity.ActionValue);
}
```

**or a list of all entities in the table.**

```
var entity = await new DapperRepositoryBase<Action>().GetAllAsync();
foreach (var item in entity)
{
	Console.WriteLine(item.ActionId + " " + item.ActionName + " " + item.ActionValue);
}
```

**GetPage**

```
var model = new PageInput();
model.OrderStr = "ActionValue";
model.PageIndex = 0;  // 当前索引，第一页
model.PageSize = 2;   // 每页多少条
model.Offset = model.PageIndex * model.PageSize;

var sqlWhere = new System.Text.StringBuilder();
dynamic pms1 = new System.Dynamic.ExpandoObject();
dynamic pms2 = new System.Dynamic.ExpandoObject();

//sqlWhere.Append(" where ActionValue=@ActionValue");
//pms1.ActionValue = 8;

//sqlWhere.Append(" and ActionName like @ActionName");
//pms1.ActionName = string.Format("%{0}%", "查询");

pms2 = pms1;

pms2.OrderStr = model.OrderStr;

var pageOutput = await new DapperRepositoryBase<Action>().GetPageAsync(model, "Act_Action", sqlWhere.ToString(), pms1, pms2) as PageOutput;

var list = new List<Action>();
foreach (dynamic item in pageOutput.Records)
{
	list.Add(new Action
	{
		ActionId = item.ActionId,
		ActionName = item.ActionName,
		ActionValue = item.ActionValue

	});
}

foreach (var item in list)
{
	Console.WriteLine(item.ActionId + " " + item.ActionName + " " + item.ActionValue);
}
```
