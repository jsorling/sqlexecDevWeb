-- install
if not exists ( select * from sys.schemas where name = N'sqlexecprojectstore' ) 
      exec('create schema [sqlexecprojectstore]');
GO

create table sqlexecprojectstore.projecttypes(
	id uniqueidentifier not null primary key
	, typename nvarchar (65) not null
	, description nvarchar (255) not null
	, helplink nvarchar (225)
)
GO

create table sqlexecprojectstore.projects(
	id uniqueidentifier not null primary key default newid() 
	, typeid uniqueidentifier not null
	, projectname nvarchar (128) not null
	, created datetime not null default getdate()
	, constraint fk_sqlexecprojectstore_projects_types foreign key (typeid)
		references sqlexecprojectstore.projecttypes(id)
)
GO

create table sqlexecprojectstore.projecttypeprops(
	id uniqueidentifier not null primary key
	, typeid uniqueidentifier not null
	, name nvarchar (65)
	, description nvarchar (255)
	, defaultvalue nvarchar (1028)
	, proptype nvarchar (32)
	, sortorder int
	, helplink nvarchar (255)
)
GO

create table sqlexecprojectstore.projectvalues(
	projectid uniqueidentifier not null
	, typepropid uniqueidentifier not null
	, value nvarchar (1028)
	, constraint pk_sqlexecprojectstore_projectvalues
		primary key (projectid, typepropid)
	, constraint fk_sqlexecprojectstore_projectvalues_projects
		foreign key (projectid) references sqlexecprojectstore.projects (id)
		on delete cascade
)
GO