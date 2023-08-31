# Bookstore
My practice Project following the tutorial of <a href="https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=Blazor&DB=EF"> ABP documentation <a>

UI Choice: Blazor WebAssembly
Database Choice: Entity Framework Core

The project practices the following:
<ul>
  <li>Domain Driven Design Pattern</li>
  <li>Designing Aggregate Roots</li>
  <li>Permissions and Authorization</li>
  <li>Data Seeding and Unit Testing</li>
  <li>Relation between Entities</li>
  <li>Code First migrations</li>
  <li>Error Handling</li>
  <li>Asynchronous Programming</li>
</ul>


The following is my personal High level steps to follow:
<ol>
  <li>Decide on your root aggregates and your design </li>
  <li>Do the following for all of your aggregates:</li>
  <ol>
    <li>Create Entity in Domain project</li>
    <li>Create Domain Service: EntityManager</li>
    <li>Create Business Expections in Domain (when needed)</li>
    <li>Create Entity Repository in Domain (usually not needed)</li>
    <li>Create Entity Consts, Localization, Error Codes in Domain shared</li>
    <li>Create your db sets in dbcontext file in efcore project</li>
    <li>Create EfCoreEntityRepo in efcore project (usually not needed)</li>
    <li>Migrate to db</li>
    <li>Create your permissions (refer to point 19 in doc) in Contract</li>
    <li>Create your permissions (refer to point 19 in doc) in Contract</li>
    <li>Create Entity Service in Application Layer</li>
    <li>Create DTOs (Create, Update, Entity, GetList) in Contract</li>
    <li>Create Entity Service Interface in Contract</li>
    <li>Create Data Seeder in domain for testing purposes</li>
    <li>Create UI for this entity, the razor pages and use blazorize</li>
    <li>Update Auto-Mapper Profile in Application</li>
  </ol>
  <li>Create relations if any by doing the following:</li>
  <ol>
    <li>Add Guid property in Entity Definition in Domain</li>
    <li>Update the onModalcreatin method in projectdbcontext</li>
    <li>Update your data in domain in data seeder and test again</li>
    <li>Add Guid and Name in EntityDto in Application Contract</li>
    <li>Also add Guid in CreateUpdateDto</li>
    <li>Now you need new lookupdto, add in the search criteria (ex: name), also add its mapping to entity in application in autopmapper profile</li>
    <li>Add method getlockupasync in AppService interface</li>
    <li>Now, u need to override some methods(GetAsync,GetListAsync) to get the related antity when getting a record or list and add GetEntityLookupAsync method</li>
    <li>Update your data in application in data seeder and test again</li>
    <li>Update your UI, your list, add entity modal and edit, You need to fetch the list of options in code part of the page.</li>
  </ol>
  <li>Always update your localization page on the way</li>
  <li>Run dbMigrator to seed data</li>
  <li>Set entity framework project as startup project in visual studio and in Package Manager Console then Run Add-Migration NAME then Update-Database</li>
</ol>

