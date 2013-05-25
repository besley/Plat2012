Plat2012
========

plat framework with vs2012

项目描述

基于MVC, WebApi, Dapper的3层分布式架构开发框架，其特点是：

1. 采用Dapper微ORMapping框架，性能接近原生SQL；
2. 采用Repository模式；
3. 采用面向Interface接口编程规范；
4. 采用WebApi实现服务总线;
5. 前端SlickGrid数据控件展现，Web富交互功能实现；
6. 前端RequireJS框架，解决脚本加载和命名冲突。


Plat framework is a basic library for an enterprise information system or website project. Some new features have been 
implemenmted in the solution. The solution is designed for 3-tier distributed system, SOA based system, Repository pattern, POCO
entity pattern, plugin architecture and asp.net mvc/web api architecture. There are some details  described here:

1. The 3-tier distributed layer include: data access layer, buisiness logic layer and web presentation layer.
2. Using micro-ORMapping framework Dapper/DapperExtension for database operation.
3. Using Generic repository pattern to convert data entity and business entity.
4. Using SimpleInjection and MEF to support Plugin architecture.
5. Using Asp.net MVC WebAPI to implement service layer/business layer, webapi is a restful style service, we make it
   to replacte wcf, the reason is that wcf would make you spend much time to maintain xml config in both server and 
   client sides.
6. All business logic are implemented from Interface, it makes concept not depended on the concrete class instance.

 
The require mvc project would give you a full tutorial how to use the plat library and webapi to create a rich mvc web
application. Similarily, there sare serveral key points to describe here:

1. The javascript block included in asp.net mvc view can call service/webapi by ajax get, post method;
2. RequireJS is a modular framework to avoid javascript global namespace problem, javascript load issue;
3. Slickgrid is a javascript control for displaying large records with high performance;
4. JsTree is a treeview control, and cutetabmenu is a tab control both used in the main layout to show function node 
   for you business product.
  
