# Bookstore
My practice Project following the tutorial of <a href="https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=Blazor&DB=EF"> ABP documentation <a>

UI Choice: Blazor WebAssembly
Database Choice: Entity Framework Core

The project practices the following:
1. Domain Driven Design Pattern
2. Designing Aggregate Roots
3. Permissions and Authorization
4. Data Seeding and Unit Testing
5. Relation between Entities
6. Code First migrations
7. Error Handling
8. Asynchronous Programming


The following is my personal High level steps to follow:
1.	Decide on your root aggregates and your design 
2.	Do the following for all of your aggregates:
  2.1.	Create Entity in Domain project
  2.2.	Create Domain Service: EntityManager
  2.3.	Create Business Expections in Domain (when needed)
  2.4.	Create Entity Repository in Domain (usually not needed)
  2.5.	Create Entity Consts, Localization, Error Codes in Domain shared
  2.6.	Create your db sets in dbcontext file in efcore project
  2.7.	Create EfCoreEntityRepo in efcore project (usually not needed)
  2.8.	Migrate to db
  2.9.	Create your permissions (refer to point 19 in doc) in Contract
  2.10.	Create Entity Service in Application Layer
  2.11.	Create DTOs (Create, Update, Entity, GetList) in Contract
  2.12.	Create Entity Service Interface in Contract
  2.13.	Create Data Seeder in domain for testing purposes
  2.14.	Create UI for this entity, the razor pages and use blazorize
  2.15.	Update Auto-Mapper Profile in Application
3.	Create relations if any by doing the following:
  3.1.	Add Guid property in Entity Definition in Domain
  3.2.	Update the onModalcreatin method in projectdbcontext
  3.3.	Update your data in domain in data seeder and test again
  3.4.	Add Guid and Name in EntityDto in Application Contract
  3.5.	Also add Guid in CreateUpdateDto
  3.6.	Now you need new lookupdto, add in the search criteria (ex: name), also add its mapping to entity in application in autopmapper profile
  3.7.	Add method getlockupasync in AppService interface
  3.8.	Now, u need to override some methods(GetAsync,GetListAsync) to get the related antity when getting a record or list and add GetEntityLookupAsync method
  3.9.	Update your data in application in data seeder and test again
  3.10.	Update your UI, your list, add entity modal and edit, You need to fetch the list of options in code part of the page.
4.	Always update your localization page on the way
5.	Run dbMigrator to seed data
6.	Set entity framework project as startup project in visual studio and in Package Manager Console then Run Add-Migration NAME then Update-Database
