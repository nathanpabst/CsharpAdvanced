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
        //__________Select from multiple tables______________
        //separate each table with a comma, qualify any references to columns by placing the table name in front and separated by a dot
        //EX..
        // select * from Individual, Occupation
        // where Individual.FirstName = 'Homer'
        // and Individual.IndividualId = Occupation.IndividualId;
        //____________
        //return only the data you need...the more columns your program has to return the more it will impact performance
        //EX...
        // select IndividualId, LastName, UserName
        // from Individual
        // where FirstName = 'Homer'
        //______________WHERE Clause______________
        // SQL WHERE SYNTAX: select * from table_name where column_name = 'criteria';
        // EX...select * from Individual where FirstName = 'homer';
        //______________Multiple Conditions (AND and OR)____________
        // EX... select * from Individual
        // where FirstName = 'Homer'
        // and LastName = 'Simpson';
        // EX...select * from Individual
        // where FirstName = 'Homer'
        // or LastName = 'Simpson';
        //_____________Order by________________
        // EX... select * from Individual
        // order by LastName desc; --using the desc keyword will return the highest values first
        //_____________Sorting by Multiple Columns__________
        // you can sort by multiple columns by stating each column in the order by clause
        //...and separating each column name with a comma. SQL will first order the results by the first column, then second, third, etc.
        // EX..select * from Individual
        // order by LastName, FirstName;
        //____________TOP_________________________
        // the 'top' clause is Transact-SQL and not part of the ANSI SQL...
        // ...depending on your db system, this clause may not be available
        // EX.. select top 3 * Individual;
        //____________PERCENT_________________________
        // EX.. select top 42 percent * Individual;
        // EX 2.. select top 42 percent * Individual order by LastName desc;
        //____________DISTINCT________________________
        // the distinct keyword allows you to find unique values in a column
        // ..once a table starts getting a lot of data in it, some columns will contain duplicate values.
        //..many indiduals share first names and surnames. to find out how many unique values there are in a column, you can use the distinct keyword
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
        // EX...
        // select UserName from Individual
        // where IndividualId in
        // (select IndividualId
        // from Publisher
        // where AccessLevel = 'contributor);
        //______________SQL ALIAS___________________
        // an alias is a name that you give a table
        // use cases: when you need to reference the same table name over and over again
        // ie when working with multiple tables and column
        // or when working with multiple instances of the same table
        // EX.. select o.JobTitle from Individual as i, Occupation as o
        // where i.FirstName = 'Natz'
        // order by o.JobTitle;
        //________________JOIN________________________
        // use 'join' to query data from two or more tables.
        // INNER JOIN : returns rows when there is at least one row in each table that match the join condition.
        // LEFT OUTER JOIN or LEFT JOIN : returns rows that have data in the left table, even if there's no matching rows in the table on the right
        // RIGHT OUTER JOIN or RIGHT JOIN : returns rows that have data in the right table, even if there's no matching rows in the left table.
        // FULL OUTER JOIN or FULL JOIN : returns all rows, as long as there's matching data in one of the tables.
        // ______INNER JOIN SYNTAX...
        // SELECT * FROM table_name1
        // INNER JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        // ______LEFT JOIN SYNTAX...
        // SELECT * FROM table_name1
        // LEFT JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        // ______RIGHT JOIN SYNTAX...
        // SELECT * FROM table_name1
        // RIGHT JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        // ______FULL JOIN SYNTAX...
        // SELECT * FROM table_name1
        // FULL JOIN table_name2
        // ON table_name1.column_name = table_name2.column_name;
        //________________INNER JOIN STATEMENT EX...
        // SELECT * FROM Individual AS i
        // INNER JOIN Publisher AS p
        // ON i.IndividualId = p.IndividualId;
        //https://www.quackit.com/sql/tutorial/sql_top.cfm
        // https://www.quackit.com/sql_server/tutorial/
    }
}