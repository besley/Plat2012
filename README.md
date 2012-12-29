Plat2012
========

plat framework with vs2012

Plat framework is a basic library for an enterprise information system or website project. Some new features have been 
implemenmted in the solution. The solution is designed with 3-tier distributed system, SOA based system, Repository pattern, POCO
entity pattern, plugin architecture and asp.net mvc/web api architecture. There are some details  described here:

1. The 3-tier distributed layer include: data access layer, buisiness logic layer and web presentation layer.
2. Using micro-ORMapping framework Dapper/DapperExtension for database operation.
3. Using Generic repository patten to convert data entity and business entity.
4. Using SimpleInjection and MEF to support Plugin architecture.
5. Using Asp.net MVC WebAPI to implement service layer, webapi is a restful style service, we make it to replacte wcf, the 
   reason is that wcf would make you spend much time to maintain xml config in both service and client sides.

  
