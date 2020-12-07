using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    internal class SQLserver
    {
        // WHAT: SQL Server is a relational database management system(RDBMS) developed by Microsoft.
        // SQL Server is extremely versatile and it can be used at all ends of the spectrum — from storing the content for an individual's blog, to storing customer data and providing analytics for small to medium enterprises, to being an integral part of the enterprise systems of some of the world's largest companies.

        // COMPETITORS: Some of SQL Server's competitors include Oracle, MySQL, PostgreSQL, etc.

        // CLIENT/SERVER DATABASE SYSTEMS
        // SQL Server is a client/server database management system(DBMS). This means that you can have many different "client" machines
        // all connecting to SQL Server at the same time(or at different times). And each one of those client machines could be connecting
        // via a different tool. SQL Server can also be managed from the server that it's installed on, but the main benefit of a client/server DBMS
        // is that multiple users can access it simultaneously, each with a specific level of access.
        // If the database administrators have done their job properly, any client that connects to SQL Server will only be able to access the
        // databases that they're allowed to access. And they can only perform the tasks that they're allowed to perform. (controlled from within SQL Server

        // SQL SERVER MANAGEMENT TOOLS: gui tools, azure data studio, ssms, dbeaver + several Command Line Tools

        // CONNECTING TO SQL SERVER INSTRUCTIONS : https://www.quackit.com/sql_server/sql_server_2017/tutorial/create_a_database_in_sql_server_2017.cfm

        //_________EXPLANATION OF THE CREATE TABLE STATEMENT
        //USE Music; --ensures that we are creating the tables within the correct db. i.e. merely precautionary
        //CREATE TABLE Artists( --artists = the name of the table that is to be created
        //    ArtistId int IDENTITY(1,1) NOT NULL PRIMARY KEY, --defining the column name (ArtistId) within the table, datatype (int), column type and how to increment the IDs (Identity(1,1)), nullable type, and defines the column as the PK for the table.
        //    ArtistName nvarchar(255) NOT NULL, --defines the column name: ArtistName, datatype: nvarchar(255), and nullable type: not null
        //    ActiveFrom date --defines the column name: ActiveFrom, datatype: date
        //); --use ')' to close the definition & ';' to end the statement (the semi-colon is a statement terminator)
        //GO --Signals the end of the batch of Transact-SQL statements
        //
        // Additional Notes:
        // IDENTITY(1,1): sets the column as an identity column and provides the unique ID for the table and the value will increment with each row added
        // (1,1) : means that the value starts at 1 and increments by 1
        // NOT NULL : means that the field cannot contain null values
        // PRIMARY KEY : sets the column as the primary key for the table. PK is a column that has been configured as the unique identifier for the table
        // NVARCHAR(255) : accepts variable-length Unicode string data, with a max length of 255 characters

        // _______________Retrieving Table information__________________ :
        // use Music;
        // select column_name, data_type, character_maximum_length, is_nullable
        // from information_schema.columns;

        // _________CREATING A RELATIONSHIP BETWEEN TWO TABLES__________
        // In relational db design, a relationship is where two or more tables are linked together because they contain related data.
        // This enables users to run queries for related data across multiple tables.
        // Code Example to create a relationship between the Albums and Artist tables...
        // CONSTRAINT FK_Albums_Artists FOREIGN KEY (ArtistId)
        //  REFERENCES[dbo].Artists(ArtistId)
        //  ON DELETE NO ACTION
        //  ON UPDATE NO ACTION
        // NOTES: this code creates a relationship between the Albums table and the Artists table, by setting the ArtistId column of the Albums
        // to reference the ArtistId column of the Artists table.
        // i.e. Albums.ArtistId becomes a foreign key of Artists.ArtistId--which itself is the primary key of that table.
        // ..
        //
        // ..
        // current location: https://www.quackit.com/sql_server/sql_server_2017/tutorial/create_a_table_in_sql_server_2017.cfm

        //https://www.quackit.com/sql_server/sql_server_2017/tutorial/
    }
}