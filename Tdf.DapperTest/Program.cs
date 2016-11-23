using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using Tdf.Dapper;
using Tdf.Dapper.Repositories;

namespace Tdf.DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));

            Console.Read();
        }

        static async void MainAsync(string[] args)
        {
            //InsertAsyncTest();
            //DeleteAsyncTest();
            //UpdateAsyncTest();
            //GetAllAsyncTest();
            //GetAsyncTest();
            GetPageAsyncTest();
        }

        #region InsertAsyncTest
        static async void InsertAsyncTest()
        {
            var action = new Action();
            action.ActionName = "编辑";
            action.ActionValue = 2;

            var result = await new DapperRepositoryBase<Action>().InsertAsync(action);
            if (result == 0)
            {
                Console.WriteLine("添加成功");
            }
        }
        #endregion

        #region GetAllAsyncTest
        static async void GetAllAsyncTest()
        {
            var entity = await new DapperRepositoryBase<Action>().GetAllAsync();
            foreach (var item in entity)
            {
                Console.WriteLine(item.ActionId + " " + item.ActionName + " " + item.ActionValue);
            }
        }
        #endregion

        #region GetAsyncTest
        static async void GetAsyncTest()
        {
            var actionId = new Guid("C017765C-DE4B-45D1-83AC-C3373072BE3E");
            var entity = await new DapperRepositoryBase<Action>().GetAsync(actionId);
            if (entity != null)
            {
                Console.WriteLine(entity.ActionId + " " + entity.ActionName + " " + entity.ActionValue);
            }
        }
        #endregion

        #region GetPageAsyncTest
        static async void GetPageAsyncTest()
        {
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
        }
        #endregion

        #region UpdateAsyncTest
        static async void UpdateAsyncTest()
        {
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
        }
        #endregion

        #region DeleteAsyncTest
        static async void DeleteAsyncTest()
        {
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
        }
        #endregion

    }
}
