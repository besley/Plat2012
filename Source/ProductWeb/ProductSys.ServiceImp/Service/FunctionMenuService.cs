using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using Plat.WebUtility;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.DAL;
using ProductSys.BizEntity;
using ProductSys.Interface;


namespace ProductSys.ServiceImp
{
    public partial class FunctionMenuService : ServiceBase, IFunctionMenuService
    {
        /// <summary>
        /// 生成JsTreeView格式数据
        /// </summary>
        /// <returns></returns>
        public JsTreeModel[] GetJsTreeView()
        {
            //ToList()是事先加载数据进来，避免再次出现datareader未未关闭的情况
            IEnumerable<FunctionMenu> functionList = this.GetAll<FunctionMenu, EPFunction>().ToList<FunctionMenu>();

            var rootItems = from a in functionList
                            where a.ParentFuncId == 0
                            select a;
            int index = 0;
            JsTreeModel[] jsTreeTop = new JsTreeModel[rootItems.Count()];

            foreach (var item in rootItems)
            {
                JsTreeModel[] childrenItems = GetChildren(item.ID, functionList);
                var model = CreateJsTreeModelInstance(item, childrenItems, true);
                jsTreeTop[index++] = model;
            }
            return jsTreeTop;
        }

        /// <summary>
        /// 获取子节点递归方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private JsTreeModel[] GetChildren(int parentId, IEnumerable<FunctionMenu> functionList)
        {
            IEnumerable<FunctionMenu> filterList = from a in functionList
                                                   where a.ParentFuncId == parentId
                                                   select a;
            int index = 0;
            JsTreeModel[] jstreeInner = new JsTreeModel[filterList.Count()];
            foreach (var item in filterList)
            {
                JsTreeModel[] childrenItems = GetChildren(item.ID, functionList);
                var model = CreateJsTreeModelInstance(item, childrenItems, false);
                jstreeInner[index++] = model;
            }
            return jstreeInner;
        }

        /// <summary>
        /// 构造TreeNode 实例
        /// </summary>
        /// <param name="item"></param>
        /// <param name="childrenItems"></param>
        /// <param name="isSelected"></param>
        /// <returns></returns>
        private JsTreeModel CreateJsTreeModelInstance(FunctionMenu item, 
            JsTreeModel[] childrenItems, bool isSelected)
        {
            JsTreeModel model = null;
            if (childrenItems.Count() > 0)
            {
                model = new JsTreeModel
                {
                    data = item.FuncName,
                    attr = new JsTreeAttribute
                    {
                        id = item.ID.ToString(),
                        selected = isSelected
                    },
                    children = childrenItems
                };
            }
            else
            {
                model = new JsTreeModel
                {
                    data = item.FuncName,
                    attr = new JsTreeAttribute
                    {
                        id = item.ID.ToString(),
                        selected = isSelected
                    }
                };
            }
            return model;
        }
    }
}