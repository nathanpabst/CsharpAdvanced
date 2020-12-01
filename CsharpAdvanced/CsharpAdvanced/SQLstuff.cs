using System;
using System.Collections.Generic;
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

        //https://www.quackit.com/sql/tutorial/sql_top.cfm
        // https://www.quackit.com/sql_server/tutorial/
    }
}