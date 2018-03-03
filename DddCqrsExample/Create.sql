create database DddCqrsExample_ReadStore
go

use DddCqrsEsExample_ReadStore
go

create table SalesOrder (
	Id varchar(50) not null,
	OrderValue float not null,
	Currency smallint not null
)

create table SalesOrderLine (
	SalesOrderId varchar(50) not null,
	Sku varchar(50) not null,
	Quantity int not null,
	UnitPrice money not null,
	Currency smallint not null
)

create table MonthlySalesFigure (
	[Year] int not null,
	[Month] int not null,
	Amount money not null,
	Currency smallint not null
)

create table Product (
    Id varchar(50) not null,
    Description varchar(255) not null,
	Amount money not null,
	Currency smallint not null
)

insert into Product (Id, Description, Amount, Currency) values (newid(), '100g packet BonBons', 1.25, 51)
insert into Product (Id, Description, Amount, Currency) values (newid(), '100g packet Humbugs', 1.25, 51)
insert into Product (Id, Description, Amount, Currency) values (newid(), '100g packet Cherries', 1.25, 51)
insert into Product (Id, Description, Amount, Currency) values (newid(), 'Gobstopper', 0.25, 51)
insert into Product (Id, Description, Amount, Currency) values (newid(), 'Lollipop', 0.25, 51)
insert into Product (Id, Description, Amount, Currency) values (newid(), '100g packet Refreshers', 1.25, 51)
insert into Product (Id, Description, Amount, Currency) values (newid(), '100g packet Pear Drops', 1.25, 51)
insert into Product (Id, Description, Amount, Currency) values (newid(), '100g packet Rhubarb and Custard', 1.25, 51)

select 'curl -XPUT "http://localhost:9200/products/productsearchresult/' + Id  + '" -H "Content-Type: application/json" -d''{ "Id": "' + Id + '", "Description": "' + Description + '", "Price": ' + cast(Amount as varchar(32)) + ' }''' from Product;

select * from SalesOrder
select * from SalesOrderLine
select * from MonthlySalesFigure
select * from Product


--truncate table salesorder
--truncate table salesorderline
--truncate table MonthlySalesFigure
