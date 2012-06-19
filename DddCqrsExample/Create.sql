create database DddCqrsExample_ReadStore
go

use DddCqrsExample_ReadStore
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

select * from SalesOrder
select * from SalesOrderLine
select * from MonthlySalesFigure