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
        //  ON DELETE NO ACTION --specifies what SQL Server should do if someone tries to delete or update a parent record that is being referenced in the child table
        //  ON UPDATE NO ACTION --...user will receive an error and the table will not be updated or deleted.
        // NOTES: this code creates a relationship between the Albums table and the Artists table, by setting the ArtistId column of the Albums
        // to reference the ArtistId column of the Artists table.
        // i.e. Albums.ArtistId becomes a foreign key of Artists.ArtistId--which itself is the primary key of that table.
        // ON DELETE CASCADE command will delete the parent and child
        // ON UPDATE CASCADE command will update the parent and child
        // NO ACTION is the default value

        // ABOUT FOREIGN KEY CONSTRAINTS...
        // A foreign key constraint defines a relationship between two tables. When you create a FK constraint, you create it against a specific column
        // in the child table, to reference a specific column in the parent table. This makes the column in the child table a foreign key. The constraint
        // ensures that any value that goes into the FK column corresponds with a value in the PK column of the parent table. If someone tries to enter
        // a value that doesn't correspond with a value in the parent table's PK column, SQL Server will throw an error. This helps enforce referential integrity
        // and prevents us from having orphaned records (child records that have no parent. For example..Albums that are not associated with any artist.)

        // ADDING A RELATIONSHIP TO AN EXISTING TABLE...
        // Code example...
        // USE Music;
        // ALTER TABLE Albums
        // ADD CONSTRAINT FK_Albums_Genres FOREIGN KEY(GenreId)
        //   REFERENCES dbo.Genres(GenreId)
        //   ON DELETE NO ACTION
        //   ON UPDATE NO ACTION
        // ;

        // ___________INSERTING DATA INTO A TABLE__________
        // Code Example...
        // INSERT INTO Artists(ArtistName, ActiveFrom)
        // VALUES
        //    ('Iron Maiden','1975-12-25');

        // ___________JOINING TABLES & RETURNING DATA_______
        // Code example...
        // SELECT AlbumId, AlbumName, ArtistName
        // FROM Albums
        //   INNER JOIN Artists
        //   ON Albums.ArtistId = Artists.ArtistId
        // WHERE ReleaseDate < '1980-01-01';

        // ____________FORMATTING THE DATE____________
        // Code example using the YEAR() function...
        // SELECT AlbumName, YEAR(ReleaseDate) AS Year --assigns an alias of 'year' to the release date column. The YEAR() function takes 'ReleaseDate' as a parameter
        //    FROM Albums; --returns the album name and year (Vulgar Display of Power, 1997)

        // ABOUT TRANSACT-SQL
        // SQL Server uses Transact-SQL (sometimes shortened to T-SQL), which is Microsoft's and Sybase's proprietary extension to SQL.
        // SQL is the standard query language used on most relational database management systems.
        // It's a standard of the American National Standards Institute (ANSI), and of the International Organization for Standardization (ISO).
        // Transact-SQL reference : https://docs.microsoft.com/en-us/sql/t-sql/language-reference?view=sql-server-ver15

        // ____________VIEWS___________________
        // in SQL Server, you can save a query as a view. Views are beneficial for
        // ...many reasons, including security, usability, and convenience.
        // A view is a virtual table whose contents are defined by a query. It is basically a pre-written query that is stored on the db.
        // Views consist of a select statement and are referred to as virtual tables because they can pull together data from multiple tables
        // ..as well as aggregate data, and present it as though it is a single table.
        // Benefits of Views...
        // Views are useful when there are multiple users with different levels of access, who all need to see portions of the data in the db (but not all the data)
        // Views can do the following:
        // 1) Restrict access to specific rows in a table
        // 2) Restrict access to specific columns in a table
        // 3) Join columns from multiple tables and present them as though they are part of a single table
        // 4) Present aggregate information (such as the results of the COUNT() function)
        //
        // ___________Creating a view Code Example:_____________
        // CREATE VIEW RockAlbums
        // AS
        // SELECT AlbumName, ArtistName
        // FROM Albums
        //    INNER JOIN Artists
        //    ON Albums.ArtistId = Artists.ArtistId
        //    INNER JOIN Genres
        //    ON Albums.GenreId = Genres.GenreId
        //    WHERE Genres.Genre = 'Rock';

        // ___________Altering a view Code Example:____________
        // ALTER VIEW RockAlbums
        // AS
        // SELECT AlbumName, ArtistName, ReleaseDate
        // FROM Albums
        //    INNER JOIN Artists ON Albums.ArtistId = Artists.ArtistId
        //    INNER JOIN Genres ON Albums.GenreId = Genres.GenreId
        //    WHERE Genres.Genre = 'Rock';

        // __________Running the View Code Example:__________
        // SELECT *
        // FROM RockAlbums
        // WHERE ReleaseDate > '1985-01-01';

        // _____________STORED PROCEDURES______________________
        // A SP is a group of SQL statements compiled into one.
        // SPs can include business logic and other programming constructs.
        // In SQL Server, a SP is a group of one or more Transact-SQL statements or
        // reference to a microsoft.NET framework common runtime language (CLR) method
        //
        // Programmability: SP's can
        // 1) Accept input parameters (and return multiple values in the form of output parameters to the calling program)
        // 2) Contain programming statements
        // 3) Return a status value to a calling program to indicate success or failure and the reason

        // SPs often contain business logic. For example, a stored procedure can accept parameters that are passed to it and
        // test against those parameters using IF statements. Eg, if the parameter is one value, do this, if it's another value, do that.
        // Stored procedures can improve performance in an application, because the stored procedure is parsed and optimized as soon as it's created,
        // and then stored in memory. Running a conditional query via stored procedure can be extremely quick - compared to an application that sends
        // a query across the network, to the SQL Server, then has all the data returned to it across the network so it can filter through it, and pick
        // out only the records it's interested in.

        // Benefits of using SPs:
        // 1) Modular programming--write a SP once then call it as many times as needed, from different parts of an application (and even from multiple applications)
        // 2) Performance--SPs provide faster code execution and reduce network traffic
        // Faster Execution: Stored procedures are parsed and optimized as soon as they are created and the stored procedure is stored in memory.
        // This means that it will execute a lot faster than sending many lines of SQL code from your application to the SQL Server.
        // Doing that requires SQL Server to compile and optimze your SQL code every time it runs.
        // Reduced network traffic: If you send many lines of SQL code over the network to your SQL Server, this will impact on network performance.
        // This is especially true if you have hundreds of lines of SQL code and/or you have lots of activity on your application.
        // Running the code on the SQL Server (as a stored procedure) eliminates the need to send this code over the network.
        // The only network traffic will be the parameters supplied and the results of any query.
        // 3) Security--Users can execute a stored procedure without needing to execute any of the statements directly.
        // Therefore, a stored procedure can provide advanced database functionality for users who wouldn't normally have access to these tasks,
        // but this functionality is made available in a tightly controlled way.

        // Practice Queries...
        //--return all albums by a given artist
        // select ar.ArtistName, al.AlbumName, al.ReleaseDate
        // from Albums al
        // inner join Artists ar on al.ArtistId = ar.ArtistId
        // where ar.ArtistId = 2;

        //--return all albums within a given genre
        // select g.Genre, ar.ArtistName, al.AlbumName
        // from Genres g
        // inner join Albums al on g.GenreId = al.GenreId
        // inner join Artists ar on al.ArtistId = ar.ArtistId
        // where g.Genre = 'Pop'
        // order by ar.ArtistName;

        //--return a count of albums by a given artist
        // select COUNT(*) as NumOfAlbums from Albums
        // where ArtistId = 5;

        // ____________CREATING A STORED PROCEDURE________________
        // Code example...
        // CREATE PROCEDURE spAlbumsFromArtist
        //     @ArtistName varchar(255)
        // AS
        //     SELECT AlbumName, ReleaseDate
        //     FROM Albums
        //     INNER JOIN Artists

        // ON Albums.ArtistId = Artists.ArtistId
        //     WHERE Artists.ArtistName = @ArtistName; --creates a SP that, when called, selects all albums from an artist
        // GO

        //___________EXECUTING A STORED PROCEDURE_________________
        // EXECUTE spAlbumsFromArtist @ArtistName = "Iron Maiden"; --returns a list of album names and release date for a given artist

        //___________ALTERING A STORED PROCEDURE__________________
        // ALTER PROCEDURE spAlbumsFromArtist
        //     @ArtistName varchar(255)
        // AS
        //     SELECT
        //          al.AlbumName,
        //          al.ReleaseDate,
        //          g.Genre
        //     FROM Albums al
        //          INNER JOIN Artists ar
        //          ON al.ArtistId = ar.ArtistId
        //          INNER JOIN Genres g
        //          ON g.GenreId = al.GenreId
        //     WHERE ar.ArtistName = @ArtistName;
        // GO

        // current location: https://www.quackit.com/sql_server/sql_server_2017/tutorial/create_a_stored_procedure_in_sql_server_2017.cfm

        //https://www.quackit.com/sql_server/sql_server_2017/tutorial/
    }
}