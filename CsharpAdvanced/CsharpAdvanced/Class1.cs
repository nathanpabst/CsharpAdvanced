using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    internal class SQLwithVenkat
    {
        // CREATING & WORKING WITH TABLES --Part 3
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

        // ________________ADDING A DEFAULT CONSTRAINT: Part 4_______________________
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

        //_________________CASCADING REFERENTIAL INTEGRITY CONSTRAINT: Part 5___________
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

        //______________GROUP BY: Part 11____________________
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
        // ___________________END OF YouTube VIDEO ON GROUP BY_______________________

        //___________________________JOIN: Part 12________________________
        // https://www.youtube.com/watch?v=wW4xcQ3FFp4&list=PL08903FB7ACA1C2FB&index=13
        // Joins in SQL Server are used to retrieve data from 2 or more related tables. In general tables are related to each other using foreign key constraints.
        // Types of JOINS...
        // 1. INNER JOIN: returns only the matching rows between both tables. Non-matching rows are eliminated. for example, if you are joining a department and
        //      and employee table by department id and one of the employees hasn't been assigned to a department (the value in their departmentId field is NULL)...that employee record/information will not
        //      be returned as part of the query.
        // 2. OUTER JOIN: divided into left join, left outer join, right join, right outer join, full join, full outer join
        //      LEFT JOIN returns all the matching rows + the non-matching rows from the left table (From the previous example...even the records for the employees who have not
        //          been assigned to a department will be returned.)
        //      NOTE: LEFT JOIN & LEFT OUTER JOIN can be used interchangeably...the query results are the same for both statements. i.e. the OUTER keyword is optional
        //          similarly, a RIGHT JOIN will return all matching and non-matching rows from the table on the right side of the query. The 'left' table is nearly always
        //          listed first in the query.
        //      FULL JOIN returns all rows from both the left and right tables, including the non-matching rows.
        // 3. CROSS JOIN produces the Cartesian product of the two tables involved in the join. For example, in the Employees table we have 10 rows
        //          and 4 rows in the Departments table, so a cross join between the two tables will produce 40 rows.
        //          Cartesian Product Calculation = # of rows in the first table multiplied by the # of rows in the second table.
        //          i.e. Employee rows (10) X Department rows (4) = 40
        //    NOTE: a cross join should not have an ON clause
        // ____________END OF YouTube VIDEO___________________________

        //_________________STORED PROCEDURES: Part 18___________________
        // https://www.youtube.com/watch?v=Qu3E-oncF3g&list=PL08903FB7ACA1C2FB&index=19
        // A SP is a group of T-SQL (Transact SQL) statements. If you have a situation where you write the same query over and over,
        //  you can save that specific query as a SP and call it by its name.
        // 1. Use 'CREATE PROCEDURE' OR 'CREATE PROC' statement to create a SP followed by the name for the procedure
        //      NOTE: when naming user defined SPs, MS recommends not to use 'sp_' as a prefix. All system stored procedures are prefixed with 'sp_'.
        //          This will serve to avoid ambiguity between user defined and system SPs and any conflicts with future system SPs.
        // _____EX. Syntax for creating a SP...
        // CREATE PROCEDURE spGetEmployees
        // AS
        // BEGIN
        //  SELECT Name, Gender FROM tblEmployee
        // END
        // Check to ensure the SP was created...expand the Programmability older, right click and refresh the Stored Procedures folder
        // _________
        // To execute the SP...
        // 1. write 'spGetEmployees', highlight the name, click the Execute button
        // 2. use the EXEC keyword + spGetEmployees
        // 3. Execute spGetEmployees
        //      NOTE: you may also right click on the procedure name, in Object Explorer (SSMS), and select 'EXECUTE STORED PROCEDURE'
        // ____Creating a SP with Parameters_________
        // Parameters and variables have an @ prefix in their name
        // To view the definition of a SP...
        // 1. write sp_helptext followed by the name of the SP
        // 2. Right click the SP in Object Explorer, script procedure as, Create To, New Query Editor Window
        //      NOTE: use ALTER PROCEDURE to change the SP, use DROP PROC 'SPName' to delete, use WITH ENCRYPTION to encrypt the text of a SP
        //          it is not possible to view the text of an encrypted SP
        // _______EX. Syntax for creating a SP with parameters...
        // CREATE PROC spGetEmployeesByGenderAndDepartment
        // @Gender nvarchar(20)
        // @DepartmentId int
        // AS
        // BEGIN
        //  SELECT Name, Gender, DepartmentId
        // FROM tblEmployee
        //  WHERE Gender = @Gender
        //  AND DepartmentId = @DepartmentId
        // END
        //      Execute by...
        // Option 1. spGetEmployeesByGenderAndDepartment 'Male', 1
        // Option 2. spGetEmployeesByGenderAndDepartment @Gender = 'Male', @DepartmentId = 1
        //      NOTE: the order of the parameters matters if you do not specify (as seen in option 2). Both queries will retrieve all male employees from department 1
        //
        // ____________Changing/Altering the definition of a SP________________
        // ______EX. Syntax for modifying a SP
        // ALTER PROCEDURE spGetEmployees
        // AS
        // BEGIN
        //  SELECT Name, Gender
        //  FROM tblEmployee
        //      ORDER BY Name
        // END
        // ______________Dropping a SP_______________
        // ______EX. Syntax to drop a SP...
        // DROP PROC spName
        //    NOTE: highlight and execute or press F5, then refresh the SP Folder to ensure the SP has been deleted.
        //    Option 2. Navigate to the Stored Procedures folder, right click on the appropriate SP and select Delete.
        // ________________end of video___________________________
        //
        //_________________Stored Procedures - Output Parameters - Part 19___________
        // https://www.youtube.com/watch?v=bldBshxuhMk&list=PL08903FB7ACA1C2FB&index=20
        // _____CREATING A SP WITH AN OUTPUT PARAMETER______
        // note: To create a SP with an output parameter, we use the keyword OUT or OUTPUT
        // __EX.
        // CREATE PROCEDURE spGetEmployeeCountB
        // AS
        // BEGIN
        //      SELECT @EmployeeCount = COUNT(Id)
        //      FROM tblEmployee
        //      WHERE Gender = @Gender
        // END
        // ______
        // ____EXECUTING A SP WITH OUTPUT PARAMETERS_____
        //  NOTE: if you do not specify the OUTPUT or OUT keyword, when executing the SP, the @EmployeeTotal variable will be NULL.
        //      As a safeguard, use an IF/ELSE statement to ensure a message will always be printed. see example below.
        // __EX.1
        // DECLARE @EmployeeTotal INT --this line creates a variable to receive the value
        // EXECUTE spGetEmployeeCountByGender 'Male', @EmployeeTotal OUTPUT
        //  IF(@EmployeeTotal IS NULL) --the IF/ELSE statement is optional
        //      PRINT '@EmployeeTotal is null'
        //  ELSE
        //      PRINT '@EmployeeTotal is not null'
        // PRINT @EmployeeTotal
        //__EX.2
        // DECLARE @EmployeeTotal INT --this line creates a variable to hold the result
        // EXECUTE spGetEmployeeCountByGender @EmployeeCount = @EmployeeTotal OUT, @Gender = 'Male'
        // PRINT @EmployeeTotal
        //____USEFUL SYSTEM STORED PROCEDURES____________
        // sp_help procedure_name: to view the information about the SP, like parameter names, data types, etc.
        // sp_help can be used with any database object, like tables, views, SPs, triggers, etc.
        // Alternatively, you can also press ALT + F1, when the name of the object is highlighted
        // sp_helptext procedure_name: to view the text of the SP
        // sp_depends procedure_name: to view the dependencies of the SP. This is very useful esp if you want to check if there are
        //      any SPs that are referencing a table that you about to drop. sp_depends can also be used with other DB objects like tables.
        // ________________end of Part 19 video___________________________

        //_________________Stored Procedures - Output Parameters or return values - Part 20___________
        // https://www.youtube.com/watch?v=st8RnNg_LLA&list=PL08903FB7ACA1C2FB&index=21
        // Return Values: whenever you execute a SP it returns one value (an integer status). Typically 0 indicates success & non-zero indicates failure.
        //      Return values can only be used with the INT data type and are used to convey success or failure
        // Output Parameters...can be used with any data type, can contain multiple values, and are used to return values like, name, count, etc.
        // __EX.1 USING OUTPUT PARAMETER
        // CREATE PROC spGetTotalCount
        // @TotalCount INT OUTPUT
        // AS
        // BEGIN
        //      SELECT @TotalCount = COUNT(Id)
        //      FROM tblEmployee
        // END
        // ___To Execute the SP...
        // DECLARE @Total INT
        // EXEC spGetTotalCount @Total OUT
        // PRINT @Total --returns 10
        //________end of example 1_____
        // __EX.2 USING RETURN VALUE --The following use of a RETURN value will work because the value being returned is an integer.
        //      NOTE: If you try to return a value type that is not an integer, you will receive a conversion error.
        // CREATE PROC spGetTotalCount2
        // AS
        // BEGIN
        //      RETURN (SELECT @TotalCount = COUNT(Id) FROM tblEmployee)
        // END
        //  ___To Execute the SP...
        // DECLARE @Total INT
        // EXEC @Total = spGetTotalCount2
        // PRINT @Total --returns 10
        //________end of example 2_____
        // ________________end of Part 20 video___________________________

        //_________________Stored Procedures - Advantages of SPs - Part 21___________
        // https://www.youtube.com/watch?v=uDcVd4vUU3s&list=PL08903FB7ACA1C2FB&index=22
        // 1. Execution plan retention and re-usability - better performance
        // 2. Reduces network traffic - better performance
        // 3. Code re-usability and better maintainability (one location to make changes)
        // 4. Better security - similar to views...only reveal the information that needs to be known
        // 5. Avoids SQL Injection attacks --see his vid on youtube
        // ________________end of Part 21 video___________________________

        //_________________Stored Procedures - Build-In String Functions in SQL Server - Part 22___________
        // https://www.youtube.com/watch?v=qJFr-R76r9A&list=PL08903FB7ACA1C2FB&index=23
        // To view a complete list of available function... navigate to the Programmability folder, expand the Functions folder, expand
        // the System Functions folder, choose from the list of available functions.
        // Commonly Used String functions...
        // 1. ASCII (Character_Expression) - Returns the ASCII code of the given (first) character expression.
        //      SELECT ASCII('ABC') --returns 65...the ASCII value of 'A'
        // 2. CHAR (Integer_Expression)- Converts an int ASCII code to a character. The Integer_Expression should be between 0 and 255
        //      SELECT CHAR(65) --returns uppercase 'A'
        //      To print the alphabet (in uppercase letters)...
        //          DECLARE @Start INT
        //          SET @Start = 65 --char value of 'A'
        //          WHILE(@Start <= 90)
        //          BEGIN
        //              SELECT CHAR(@Start)
        //              SET @Start = @Start +1
        //          END--
        // 3. LTRIM (Character_Expression) - removes blanks on the left hand side of the given character expression
        //      SELECT LTRIM(FirstName) as FirstName, MiddleName, LastName FROM tblEmployee;

        // 4. RTRIM (Character_Expression) - removes blanks on the right hand side of the given character expression
        //      SELECT RTRIM(FirstName) as FirstName, MiddleName, LastName FROM tblEmployee;
        //      To remove excess space from both sides...
        //          SELECT RTRIM(LTRIM(FirstName)) as FirstName FROM tblEmployee;
        // 5. LOWER (Character_Expression) - converts all characters to lower case
        // 6. UPPER (Character_Expression) - converts all characters to upper case
        // 7. REVERSE ('Any_String_Expression') - reverses all the characters in the given string expression. i.e 'sam' would return as 'mas'
        // 8. LEN (String_Expression) - returns the count of total characters, in the given string expression
        //      excluding the blanks at the end of the expression. You can use LTRIM as well to get a count without any leading spaces.
        // ________________end of Part 22 video___________________________

        //_______________Views in SQL Server - Part 39___________
        // https://www.youtube.com/watch?v=VQpmOmZO2mo&list=PL08903FB7ACA1C2FB&index=40
        // A View does not store any data it is only a saved SQL query. Only the select query is saved in the DB. Views are also considered/know as virtual tables.
        // __EX 1. Creating a View...
        // CREATE VIEW vwEmployeesByDepartment
        // AS
        // SELECT Id, Name, Salary, Gender, DeptName
        // FROM tblEmployee
        // JOIN tblDepartment
        // ON tblEmployee.DepartmentId = tblDepartment.DeptId --Use F5 to execute, click on the Views folder in Object Explorer and refresh the folder
        //____________
        // NOTE: to retrieve information from the view...SELECT * FROM vwEmployeesByDepartment
        //  ...to retrieve a definition of a view... sp_helptext vwEmployeesByDepartment -> F5
        //_____ADVANTAGES OF VIEWS__________
        // Views can be used to reduce the complexity of the db schema. Ex. for non-IT users that will need to interact with the db
        // Views can be used as a mechanism to implement row and column level security
        // Views can be used to present aggregated data and hide detailed data

        // __EX. 1. only display the information needed...the IT dept manager does not need to see the salaries for employees outside her department.
        // ...create a view containing only the necessary info for IT employees
        // CREATE VIEW vwITEmployees
        // AS
        // SELECT Id, Name, Salary, Gender, DeptName
        // FROM tblEmployee
        // JOIN tblDepartment
        // ON tblEmployee.DepartmentId = tblDepartment.DeptId
        // WHERE tblDepartment.DeptName = 'IT'
        //
        // __EX.2. create a view that retrieves employee info, but hides salaries...
        // CREATE VIEW vwNonConfidentialData
        // AS
        // SELECT Id, Name, Gender, DeptName
        // FROM tblEmployee
        // JOIN tblDepartment
        // ON tblEmployee.DepartmentId = tblDepartment.DeptId
        //
        // SELECT * FROM vwNonConfidentialData
        // __
        // __EX. 3. create a view with summarized data only --aggregate queries will require the 'group by' clause
        // CREATE VIEW vwSummarizedData
        // AS
        // SELECT DeptName, COUNT(Id) as TotalEmployees
        // FROM tblEmployee
        // JOIN tblDepartment
        // ON tblEmployee.DepartmentId = tblDepartment.DeptId
        // GROUP BY DeptName;
        //
        // SELECT * FROM vwSummarizedData --returns DeptName and a count of employees in each department
        // ________________end of Part 39 video___________________________

        //________________VIEW LIMITATIONS IN SQL SERVER - Part 42________
        // https://www.youtube.com/watch?v=xRIMMZsIt2k&list=PL08903FB7ACA1C2FB&index=41

        //_________________end of Part 42 video___________________________
    }
}