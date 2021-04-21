using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CsharpAdvanced
{
    internal class SQLstuff
    {
        //__________Data Manipulation Language (DML) Commands________
        //SELECT, INSERT, UPDATE, DELETE
        //__________Data Definition Language (DDL) Commands__________
        //CREATE DATABASE, ALTER DATABASE, DROP DATABASE, CREATE TABLE, ALTER TABLE, DROP TABLE
        //__________SELECTING FROM MULTIPLE TABLES______________
        //separate each table with a comma, qualify any references to columns by placing the table name in front and separated by a dot
        //EX..
        // select * from Individual, Occupation
        // where Individual.FirstName = 'Homer'
        // and Individual.IndividualId = Occupation.IndividualId;
        //____________
        // EX 2..
        //select * from [dbo].[User] u, Role r
        //where u.FirstName = 'Nathan' and r.Name = 'Admin';
        //--returns all columns from the User and Role tables where the user's first name is 'Nathan' and the role is 'Admin' for the first name, last name, and role name
        //__________________________
        // NOTE: return only the data you need...the more columns your program has to return the more it will impact performance
        //EX...
        // select IndividualId, LastName, UserName
        // from Individual
        // where FirstName = 'Homer'
        //______________WHERE Clause______________
        // SQL WHERE SYNTAX: select * from table_name where column_name = 'criteria';
        // EX...select * from[dbo].[User] u WHERE u.Active = 0 order by u.LastName desc;
        // EX...select * from Individual where FirstName = 'homer';
        //______________Multiple Conditions (AND and OR)____________
        // EX... select * from Individual
        // where FirstName = 'Homer'
        // and LastName = 'Simpson';
        // EX...select * from Individual
        // where FirstName = 'Homer'
        // or LastName = 'Simpson';
        //_____________ORDER BY________________
        // EX...select * from[dbo].[User] u WHERE u.Active = 0 order by u.LastName desc;
        // EX... select * from Individual order by LastName desc; --using the desc keyword will return the highest values first
        //_____________Sorting by Multiple Columns__________
        // you can sort by multiple columns by stating each column in the order by clause
        //...and separating each column name with a comma. SQL will first order the results by the first column, then second, third, etc.
        // EX..select * from Individual order by LastName, FirstName;
        // EX..select u.FirstName, u.LastName, u.RoleId from [dbo].[User] u
        //...order by u.RoleId desc, u.LastName; --returns first name, last name and role id.primary sort = role id listed in descending order(4,3,2,1)
        //____________TOP_________________________
        // the 'top' clause is Transact-SQL and not part of the ANSI SQL...
        // ...depending on your db system, this clause may not be available
        // EX.. select top 3 * Individual;
        // EX..select top 5 * from[dbo].[User] --returns the first five results (chronologically by Id)
        //____________PERCENT_________________________
        // EX.. select top 42 percent * Individual;
        // EX 2.. select top 42 percent * Individual order by LastName desc;
        // EX..select top 10 percent u.UserName, u.LastName, u.Active, u.RoleId from [dbo].[User] u
        //____________DISTINCT________________________
        // the distinct keyword allows you to find unique values in a column
        // ..once a table starts getting a lot of data in it, some columns will contain duplicate values.
        //..many individuals share first names and surnames. to find out how many unique values there are in a column, you can use the distinct keyword
        // EX..select distinct(FirstName) from Individual; NOTE: in this example, all customers with the same first name
        //..will be treated as one. this results in only one entry per first name
        //____________IN________________________
        //the IN operator assists in providing multiple values in the 'where' clause
        // i.e. when you need to compare your value to a list of values. this list could be the result of a query from another table
        // EX.. select * from Individual where LastName IN ('Andrews', 'Pabst', 'Flintstone');
        // the IN operator is similar, but more concise than using the 'or' operator
        // EX.. select * from Individual
        // ..where LastName = 'Andrews',
        // or LastName = 'Pabst',
        // or LastName = 'Flintstone');
        // NOTES: the 'in' operator is especially useful when you need to compare a value against the result of another query.
        //..example, lets say we have two tables (Individual and Publisher--which contains read/write permissions)...All users in the publisher table are also in the Individual table,
        //..but not all users in the Individual table are also in the Publisher table
        // EX...select UserName from Individual where IndividualId in (select IndividualId from Publisher where AccessLevel = 'contributor);
        // EX...select u.UserName from[dbo].[User] u where u.RoleId IN (select Id from Role where Name = 'viewer'); --returns the username of any user assigned the viewer role

        //______________GROUP BY____________________
        // https://www.youtube.com/watch?v=FKSSOpQe5Jc&list=PL08903FB7ACA1C2FB&index=12
        // syntax:
        // SELECT COUNT(CustomerID) AS customer_count, Country FROM Customers GROUP BY Country ORDER BY COUNT(CustomerID) DESC;
        // --returns two columns: customer_count and country. countries are grouped together and ordered (high to low) by the number of customers it has
        // The GROUP BY statement groups rows that have the same value into summary rows. EX. 'find the number of customers in each country.'
        // The GROUP BY clause is used to group a selected set of rows into a set of summary rows y the values of one or more columns or expressions.
        // GROUP BY is always used in conjunction with one or more aggregate functions (COUNT, MAX, MIN, SUM, AVG)
        // you can only apply aggregate functions on columns containing numerical values (not text, etc.)--
        // ___EX. 1. SELECT City, SUM(Salary) AS TotalSalary FROM tblEmployee GROUP BY City;
        // NOTE: if you omit the GROUP BY clause and try to execute the query, you will receive the following error...
        // "Column 'tblEmployee.City' is invalid in the select list because it is not contained in either an aggregate function or the GROUP BY clause."
        // ___EX. 2 Grouping by multiple columns
        // SELECT City, Gender, SUM(Salary) AS TotalSalary FROM tblEmployee GROUP BY City, Gender ORDER BY City;
        // __EX. 3 Using multiple aggregate functions
        // SELECT City, Gender, SUM(Salary) AS TotalSalary, COUNT(Id) AS [Total Employees] FROM tblEmployee GROUP BY City, Gender ORDER BY City;
        // ________Filtering Groups__________________
        // The following queries produce the same result...
        // SELECT City, SUM(Salary) AS TotalSalary FROM tblEmployee WHERE City = 'London' GROUP BY City;
        // SELECT City, SUM(Salary) AS TotalSalary FROM tblEmployee GROUP BY City HAVING City = 'London';
        // NOTE: performance is about the same on both queries. SQL Server optimizer analyzes each statement and selects an efficient way of executing it.
        // As a best practice, use the syntax that clearly describes the desired result. and try to eliminate the rows that you do not need as early as possible.
        // _________Difference between WHERE & HAVING____________
        // The WHERE clause is used to filter rows before aggregation (grouping). NOTE: the WHERE clause can be used with SELECT, INSERT, & UPDATE statements
        // The HAVING clause is used to filter groups after aggregations are performed. The HAVING clause must come after the GROUP BY clause
        // Aggregate functions cannot be used in the WHERE clause, unless it is in a sub query contained in a HAVING clause,
        // whereas, aggregate functions can be used in the HAVING clause.
        // __EX.
        // SELECT City, Gender, SUM(Salary) AS TotalSalary, COUNT(ID) AS TotalEmployees FROM tblEmployee GROUP BY City, Gender HAVING SUM(Salary) > 5000;

        //

        //______________SQL ALIAS___________________
        // an alias is a name that you give a table
        // use cases: when you need to reference the same table name over and over again
        // ie when working with multiple tables and column
        // or when working with multiple instances of the same table
        // EX.. select o.JobTitle from Individual as i, Occupation as o
        // where i.FirstName = 'Natz'
        // order by o.JobTitle;
        //___________________________JOIN________________________
        // use 'join' to query data from two or more tables.

        //_________________________________________________________________
        // INNER JOIN : returns rows when there is at least one row in each table that match the join condition.
        // __________________________INNER JOIN SYNTAX...
        // EX. 1...
        // SELECT * FROM table_name1
        // INNER JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        // EX. 2...
        // select u.UserName, r.Name as 'roleName' from[dbo].[User] u
        //    inner join[dbo].[Role] r
        //    on u.RoleId = r.Id
        //    where u.RoleId = 4; --returns a list of userNames and the roleName where the role id is 4 (Viewer)
        //____________________________________________________________________

        // LEFT OUTER JOIN or LEFT JOIN : returns rows that have data in the left table, even if there's no matching rows in the table on the right
        // ___________________________LEFT JOIN SYNTAX...
        // EX. 1...
        // SELECT * FROM table_name1
        // LEFT JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        // EX. 2...
        // select * from [dbo].[User] u
        //    left join[dbo].[Role] r
        //    on u.RoleId = r.Id
        //    where u.RoleId = 4; --returns all data from the user and role tables where the role id is 4 (Viewer)
        // EX. 3...
        //select u.UserName, r.Name as 'roleName' from[dbo].[User] u
        //    left join[dbo].[Role] r
        //    on u.RoleId = r.Id
        //    order by u.UserName desc; --returns a list of userNames in desc order and each users roleName
        //____________________________________________________________________

        // RIGHT OUTER JOIN or RIGHT JOIN : returns rows that have data in the right table, even if there's no matching rows in the left table.
        // ___________________________RIGHT JOIN SYNTAX...
        // SELECT * FROM table_name1
        // RIGHT JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        //____________________________________________________________________

        // FULL OUTER JOIN or FULL JOIN : returns all rows, as long as there's matching data in one of the tables.
        // ___________________________FULL JOIN SYNTAX...
        // SELECT * FROM table_name1
        // FULL JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        // EX. 2...

        //________________INNER JOIN STATEMENT EX...
        // SELECT * FROM Individual AS i
        // INNER JOIN Publisher AS p
        // ON i.IndividualId = p.IndividualId;
        //____________
        // EX..
        //--INNER JOIN
        //select* from[dbo].[User] as u
        //    inner join[dbo].[Role] as r
        //    on u.RoleId = r.Id
        //    where u.RoleId = 1; --returns data for all users with the role of admin.data = all columns from the user & role tables
        //_________________
        // EX..
        //--INNER JOIN & TOP
        //select top 1 * from[dbo].[User] as u
        //    inner join[dbo].[Role] as r
        //    on u.RoleId = r.Id
        //    where u.RoleId = 1; --returns data for the first user with the role of admin.data = all columns from the user & role tables
        //_______________
        // EX..
        //--INNER JOIN & TOP 1 w/ custom column names
        //select top 1 u.FirstName as First_Name, u.LastName as Last_Name from[dbo].[User] as u
        //    inner join[dbo].[Role] as r
        //    on u.RoleId = r.Id
        //    where u.RoleId = 1; --returns columns for first name, last name, and role.
        //_______________
        // EX..
        //--INNER JOIN & TOP 10% w/ custom column names
        //select top 10 percent u.FirstName as First_Name, u.LastName as Last_Name, r.Name as Role from [dbo].[User] as u
        //    inner join[dbo].[Role] as r
        //    on u.RoleId = r.Id
        //    where u.RoleId = 1; --returns a column for the first name, last name, and role name with data for the top 10 percent of users
        //______________________________________________________________

        // _________________UPDATE COMMAND
        // NOTE: The update command uses a where clause. if you don't use a where clause, all rows will be updated.
        // EX. 1...
        //UPDATE[dbo].[User]
        //SET UserName = 'bob.loblaw@lawblogs.com'
        //where Id = 1;

        // EX. 2... TO UPDATE MULTIPLE FIELDS, SEPARATE EACH FIELD ASSIGNMENT WITH A COMMA
        //UPDATE[dbo].[User]
        //SET FirstName = 'bob', LastName = 'loblaw'
        //where Id = 1;

        //____________________DELETE COMMAND
        // NOTE: The DELETE command uses a WHERE clause. if you don't use the where clause, all rows will be deleted.
        // SYNTAX EX. 1...
        // DELETE FROM[dbo].[User] WHERE Id = 99;

        // __________________SQL FUNCTIONS___________________________
        // Functions are a self contained script/program built for a specific purpose. Generally, the value returned by a function will depend
        // on the context in which it is being used. Often, a SQL function will be used within a query and this is what provides it with context.
        // Transact-SQL Functions
        // 1. Rowset Functions: return an object that can be used in place of a table reference in a sql statement.
        // 2. Aggregate Functions: perform a calc on a set of values and return a single value.
        // Can be used in the following: Select, Compute, Compute By, Having
        // Ranking Functions: returns a value for each row in a partition
        // Scaler Functions: Returns a single value from a single value. Types: Configuration, cursor, DateTime, Mathematical, etc.
        // NOTE: DB vendors have their own built-in functions and allow programmers to write their own defined functions. see the DB vendor's documentation
        //
        // COUNT: returns the number of rows that match the given criteria
        // EX: select COUNT(*) from [dbo].[User] --returns the number of records in a table including null values and duplicates
        // COUNT(column name): --returns the number of non-null values in a given column
        // EX: select COUNT(FirstName) from [dbo].[User]
        // COUNT & DISTINCT: returns the number of unique values/names in the table
        // EX: select COUNT(Distinct(RoleId)) from [dbo].[User] --returns 4 (number of unique values...1,2,3,4)
        //
        // CREATE COMMANDS: https://www.quackit.com/sql/tutorial/sql_create.cfm
        // Keywords...
        // USE master: specifies the location of the db's data file and transaction log
        // SIZE & MAXSIZE: specifies the initial size of the files & the max size it can grow to
        // FILEGROWTH: specifies the growth increment of each file
        // GO: indicates the end of a batch (specific to MSSM)

        // Create Table Syntax...
        // CREATE TABLE table_name
        // (column_name_1 datatype,
        // column_name_2 datatype
        // ...
        // );
        // EX:
        // CREATE TABLE Individual
        // (IndividualId int,
        //  FirstName Varchar(255),
        //  LastName Varchar(255),
        //  UserName Char(10)
        // );
        // DB Data Types...
        // char: fixed length strings
        // varchar: variable length strings
        // other types: datetime, bigint, int, smallint, tinyint, numeric
        //
        // CREATE INDEX
        // Indexes can be created against a table to make searches more efficient. A database index is similar to an index of a book
        // — a book index allows you to find information without having to read through the whole book.
        // A database index enables the database application to find data quickly without having to scan the whole table.
        // Indexes can have a slight impact on performance so you should only create indexes against tables and columns that will be frequently searched against.
        // For example, if users of your application often search against the LastName field then that field is a great candidate for an index.
        // SYNTAX: CREATE INDEX index_name ON table_name (column_name);
        // EX: CREATE INDEX UserIndex ON User (UserName);
        //
        // CREATING & WORKING WITH TABLES
        // https://www.youtube.com/watch?v=JLeaM8pK8dE&list=PL08903FB7ACA1C2FB&index=4
        // The purpose of a Primary Key is to identify, uniquely, each record within the table
        // Creating a table graphically using SSMS...
        // 1. Select the appropriate database, mofo!!
        // 2. Right click on the Tables folder and select 'New Table' option ...
        // 3. Populate each field within the Table Designer Window (i.e.: Column Name, Data Type, Allow Nulls, right click on the arrow, to the left of the column name, to establish the column as the primary key)
        // 4. Right click on the tab that you are working in, Select 'Save Table', enter a name for the table
        // 5. Open the table, via Object Explorer, expand the Columns folder, check to ensure all fields (i.e. column names, data types, etc.) are present.
        //
        // Creating a Foreign Key relationship graphically using SSMS...
        // 1. Right click on the table, select Design
        // 2. Right click the column that will be a FK and select 'Relationships'
        // 3. Click 'Add' in the pop-up box
        // 4. Click the ellipsis icon in the 'Tables And Columns Specifications'
        // 5. Specify the relationship (primary key table and foreign key table)
        // 6. Close and Save
        // ________________________________________
        // Creating a table by writing a query...
        // **IMPORTANT** make sure you are creating the table in the correct DB OR preface the query with the USE statement
        // USE [DatabaseName]
        // GO--
        // Create Table tblGender
        // (
        //      ID int NOT NULL Primary Key,
        //      Gender nvarchar(50) NOT NULL
        // )
        // Then...Execute (F5), Refresh the Tables folder, ensure that the table was created
        //
        // Foreign keys are used to enforce database integrity. in layman's terms, a FK in one table points to a primary key in another table
        // the FK constraint prevents invalid data from being inserted into the FK column. The values that you enter into the FK column, must
        // be one of the values contained within the table it points to.
        // ________________ADDING A DEFAULT CONSTRAINT_______________________
        // https://www.youtube.com/watch?v=dwSqHhMl32Y&list=PL08903FB7ACA1C2FB&index=5
        // The DEFAULT constraint is used to insert a default value into a column. The default value will be added to all new records,
        // if no other value is specified, including NULL.
        // Inserting a record via query...
        // Insert into tblPerson (Id, Name, Email) Values (7, 'Nate', n@n.com)
        // ___F5 to execute
        // Adding a default constraint to an existing record...
        // ALTER TABLE tblPerson
        // ADD CONSTRAINT DF_tblPerson_GenderId
        // DEFAULT 3 FOR GENDERID
        // F5 to execute, refresh the Constraints folder, and double check the work
        // NOTE: if you supply a value, for example NULL, to the record, that value will override the default constraint value.
        // ...Rather, the value supplied will be inserted into the table.
        // __________________DROPPING A CONSTRAINT SYNTAX______________________
        // ALTER TABLE tblPerson
        // DROP CONSTRAINT DF_tblPerson_GenderId
        // __F5 to execute, refresh the Constraint folder, check to ensure the constraint was removed
        //
        //_________________CASCADING REFERENTIAL INTEGRITY CONSTRAINT___________
        // https://www.youtube.com/watch?v=ETepOVi7Xk8&list=PL08903FB7ACA1C2FB&index=6--
        // The cascading referential integrity constraint allows us to define the actions MS SQL Server
        // should take when a user attempts to delete or update a key to which an existing FK points.
        // For example, if you delete row with Id = 1 from the tblGender table, then row with an Id of 3
        // from the tblPerson table becomes an orphan record. (You won't be able to tell the Gender for this row)
        // Cascading referential integrity constraint can be used to define actions SQL Server should take
        // when this happens. By default, we will get an error the DELETE or UPDATE statement is rolled back.
        // Options when setting up cascading referential integrity constraints...
        // NOTE: these options can be set by navigating to SSMS, opening the appropriate table, opening the 'Keys' folder,
        //      right-clicking on the FK, selecting the Modify option. Within the FK Relationships pop-up box,
        //      click the dropdown arrow to the left of 'INSERT And UPDATE Specifications',
        //      click in the box next to the rule that needs to be updated or modified.
        // 1. No Action: default behavior. No action specifies that if an attempt is made to delete or update a row with a
        //      key referenced by foreign keys in existing rows in other tables, an error is raised and the DELETE or UPDATE
        //      will be rolled back.
        // 2. Cascade: specifies that if an attempt is made to delete or update a row with a key referenced by foreign keys
        //      in existing rows in other tables, all rows containing those FK are also deleted or updated.
        // 3. Set NULL: specifies that if an attempt is made to delete or update a row with a key referenced by foreign keys
        //      in existing rows in other tables, all rows containing those FK are set to NULL.
        // 4. Set Default: specifies that if an attempt is made to delete or update a row with a key referenced by foreign keys
        //      in existing rows in other tables, all rows containing those FK are set to default values.
        //
        // ________________ALTERING TABLES_______________________
        // Creating a Foreign Key relationship by writing a query...
        // Alter Table tblPerson add constraint tblPerson_GenderId_FK
        // Foreign Key (GenderId) references tblGender (Id)
        // --F5 to execute, refresh the Person table and check for the FK constraint within the Keys folder
        // --
        // Adding a Column to a table...
        // SYNTAX: ALTER TABLE table_name ADD column_name datatype;
        // EX: ..ALTER TABLE [dbo].[User] ADD MiddleName varchar;

        // Changing Data type
        // SYNTAX...
        // ALTER TABLE table_name
        // ALTER COLUMN column_name datatype;
        // EX:
        // ALTER TABLE Individual
        // ALTER COLUMN age numeric;

        // Dropping a Column--removing or deleting a column
        // SYNTAX...
        // ALTER TABLE table_name
        // DROP COLUMN column_name;
        // EX:
        // ALTER TABLE Individual
        // DROP COLUMN age;
        //_________________________________________________
        // https://www.quackit.com/sql/tutorial/sql_top.cfm
        // https://www.quackit.com/sql_server/tutorial/
    }
}