Create Schema sa;
Go

Create Table sa.Location (
	Id int Not Null Primary Key Identity,
	State VarChar(20) Not Null,
	City VarChar(30) Not Null
);

Create Table sa.Store (
	Id int Not Null Primary Key Identity,
	Name VarChar(30) Not Null,
	LocationId int Not Null Foreign Key References sa.Location (Id)
);

Create Table sa.Customer (
	Id int Not Null Primary Key Identity,
	FirstName VarChar(20) Not Null,
	LastName VarChar(20) Not Null,
	LocationId int Not Null Foreign Key References sa.Location (Id)
);

Create Table sa.Product (
	Id int Not Null Primary Key Identity,
	ProductName VarChar(30) Not Null,
	Price Money Not Null
);

Create Table sa.StoreProduct (
	StoreId int Not Null Foreign Key References sa.Store (Id),
	ProductId int Not Null Foreign Key References sa.Product (Id),
	Stock int
);

Create Table OrderHistory (
	Id int Not Null Primary Key Identity
);


































































