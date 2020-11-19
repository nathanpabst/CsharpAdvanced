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
        //https://www.quackit.com/sql/tutorial/sql_top.cfm
        // https://www.quackit.com/sql_server/tutorial/
    }
}