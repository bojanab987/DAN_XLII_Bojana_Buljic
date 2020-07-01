IF NOT EXISTS (SELECT * FROM sys.databases WHERE name ='EmployeeRecords')
CREATE DATABASE EmployeeRecords;
GO

USE EmployeeRecords
GO

--Drop all tables--
IF OBJECT_ID('tblEmployees') IS NOT NULL
DROP TABLE tblEmployees;

IF OBJECT_ID('tblGenders') IS NOT NULL
DROP TABLE tblGenders;

IF OBJECT_ID('tblSectors') IS NOT NULL
DROP TABLE tblSectors;

IF OBJECT_ID('tblLocations') IS NOT NULL
DROP TABLE tblLocations;

--drop view Employees
IF OBJECT_ID('vwEmployees') IS NOT NULL
DROP VIEW vwEmployees
--drop view Locations
IF OBJECT_ID('vwLocations') IS NOT NULL
DROP VIEW vwLocations
--drop view Managers
--drop view Locations
IF OBJECT_ID('vwManagers') IS NOT NULL
DROP VIEW vwManagers

--Creating tables
--first we create tblLocations since we need it first on application start
CREATE TABLE tblLocations(
LocationID int primary key identity(1,1),
Street nvarchar(50) not null,
City nvarchar(30) not null,
Country nvarchar(30) not null
);

--Sector table
CREATE TABLE tblSectors(
SectorID int primary key identity (1,1),
SectorName nvarchar(50)
);

--Genders table
CREATE TABLE tblGenders(
GenderID int primary key identity (1,1),
Gender varchar(6)
);

--Employees table
CREATE TABLE tblEmployees(
EmployeeID int primary key identity(1,1),
FullName nvarchar (50) not null,
DateOfBirth Date not null,
IdentityCard char(9) not null,
JMBG char(13) not null,
GenderID int foreign key references tblGenders(GenderID) ON DELETE SET NULL,
PhoneNo varchar(20) not null,
SectorID int foreign key references tblSectors(SectorID) ON DELETE SET NULL,
LocationID int foreign key references tblLocations(LocationID) ON DELETE SET NULL,
ManagerID int
);

GO

--Inserting values into tblGenders and tblSectors
INSERT INTO tblGenders(Gender)
VALUES('Male'),('Female'),('Other');

INSERT INTO tblSectors(SectorName)
VALUES('HR Department'),('Legal Department'),('Finance Department'), ('Research and Development'),('Production'),('Marketing');
go

CREATE VIEW vwLocations
AS
SELECT LocationID, Street+','+City+','+Country AS Location
FROM tblLocations;
go

--Creating Employees view
CREATE VIEW vwEmployees
AS
SELECT e.EmployeeID, e.FullName,e.DateOfBirth, e.IdentityCard, e.JMBG,e.GenderID, Gender, e.PhoneNo,
CONCAT(Street, ',', City, ',', Country) AS Location,e.LocationID, SectorName, e.SectorID,e.ManagerID, m.FullName AS Manager
from tblEmployees e
inner join tblLocations l
on e.LocationID=l.LocationID
inner join tblGenders g
on e.GenderID=g.GenderID
inner join tblSectors s
on e.SectorID=s.SectorID
inner join tblEmployees m
on e.ManagerID=m.EmployeeID;
go

CREATE VIEW vwManagers
AS
SELECT EmployeeID, FullName AS Manager
from tblEmployees;
go

