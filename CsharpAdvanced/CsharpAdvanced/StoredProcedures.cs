using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CsharpAdvanced
{
    internal class StoredProcedures
    {
        //_________________STORED PROCEDURES___________________
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
        // CREATE PROCEDURE spGetEmployeeCountByGender
        // @Gender NVARCHAR(20),
        // @EmployeeCount INT OUTPUT
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

        // ____What is a SP & why do we need them?_______
        // Elements of a SP...
        // inputs - name and parameters
        // execution - select statement and parameter used as criteria
        // output - the result set returned
        // Why SPs? ...
        // 1. Maintainability
        // 2. Encapsulate Biz Logic
        // 3. Stronger Security
        // 4. Efficiency
        //_____Stored Procedure: Section 3___
        // Objective...Calculate the time to travel a distance
        // Steps...
        // 1. accept distance and velocity as parameters
        // 2. calculate time as distance / velocity
        // 3. output the result
        // Sample SP...
        //CREATE PROCEDURE [dbo].upsCalculateTime
        //// add the parameters here...
        //@distance float,
        //@velocity float
        //AS
        //BEGIN
        //// set nocount on to prevent extra result sets from interfering with the select statements
        //  SET NOCOUNT ON;
        //// insert statements for procedure...
        //      IF (@velocity <> 0.00)
        //          SELECT @distance / @velocity;
        //      ELSE
        //          SELECT 0.00;
        //END
        // command to execute the SP...
        // EXEC dbo.upsCalculateTime 5.0, 15.0
        // ______Challenge 1__________
        //CREATE PROCEDURE dbo.uspAddNumber
        //-- Add the parameters for the stored procedure here

        //@a float,
        //@b float
        //    AS
        //BEGIN
        //    SET NOCOUNT ON; -- this will prevent extra result sets from interfering with SELECT statements.

        //-- Insert statements for procedure here
        //SELECT @a + @b;
        //END

        //__________STEPS TO EXECUTE A SP_______________
        // 1. in SSMS, click create new query
        // 2. EXEC dbo.uspAddNumber 5.0, 15.0
        // 3. F5 to run the query
        //__________PARTS OF A SP section 3_____________
        // PARAMETERS...
        // SPs can accept parameter values as inputs
        // modified values can be passed back to the calling program
        // EXECUTION...
        // SPs can execute SQL statement, utilize conditional logic such as IF THEN or CASE statements
        // ...and looping constructs to perform tasks like WHILE loops
        // a SP is able to call another SP
        // use Cursors
        // OUTPUTS...
        // a SP can return:
        // 1. a single value such as number or text value
        // 2. a set or rows as a result set
        // 3. OUTPUT Parameters
        //____________Defining a SP coding example: provide the biz with a way of verifying CCs_____________
        // 1. define the SP...
        // CREATE PROCEDURE uspGetCreditInfoForCustomer
        //    AS
        //BEGIN
        //  PRINT 'Display CC Information Here'
        //END
        // 2. create a query to get SalesOrderID / get the latest sales order header...
        //SELECT TOP 1 SOH.SalesOrderID
        //    FROM Sales.SalesOrderHeader SOH
        //      INNER JOIN Sales.Customer C ON SOH.CustomerID = C.CustomerID
        //      INNER JOIN Person.Person P ON P.BusinessEntityID = C.CustomerID
        //    WHERE P.FirstName LIKE 'Kristina' AND P.LastName LIKE 'Garcia'
        //      ORDER BY SOH.OrderDate DESC;
        //--result = 73823
        // 3. Get the credit card number/create a query to get the Credit Card
        //SELECT CC.CreditCardId, CC.CardType, CC.CardNumber, CC.ExpMonth, CC.ExpYear
        //FROM Sales.CreditCard CC
        //  INNER JOIN Sales.SalesOrderHeader SOH ON CC.CreditCardID = SOH.CreditCardID
        //  AND SOH.SalesOrderID = 73823
        //--result = ccId: 11978, CardType: Distinguish, CardNumber, ExpMonth, ExpYear
        //END
        // 4. Refactor to include variables...
        //ALTER PROCEDURE uspGetCreditInfoForCustomer
        //--ADD PARAMETERS
        //@first Varchar(40),
        //@last Varchar(40),
        //@CreditInfo Varchar(200) OUTPUT
        //    AS
        //BEGIN
        //--Declare variables
        //DECLARE @salesOrderID Int;

        //--Get the latest sales order header
        //SELECT TOP 1 @salesOrderID = SOH.SalesOrderID
        //FROM Sales.SalesOrderHeader SOH
        //  INNER JOIN Sales.Customer C ON SOH.CustomerID = C.CustomerID
        //  INNER JOIN Person.Person P ON P.BusinessEntityID = C.CustomerID
        //WHERE P.FirstName LIKE @first AND P.LastName LIKE @last
        //ORDER BY SOH.OrderDate DESC;
        //--result = 73823

        //--Get credit card number
        //IF @@ROWCOUNT > 0
        //BEGIN
        //    SELECT @CreditInfo = 'Card: ' + CC.CardType + ' - ' +
        //Replicate('*', LEN(CC.CardNumber) - 4) + RIGHT(CC.CardNumber, 4) +
        //    ' Exp: ' + CAST(CC.ExpMonth as varchar(2))
        //FROM Sales.CreditCard CC
        //    INNER JOIN Sales.SalesOrderHeader SOH ON CC.CreditCardID = SOH.CreditCardID
        //  END
        //  ELSE
        //    BEGIN
        //SET @CreditInfo = 'Customer not found.';
        //    --result = ccId: 11978, CardType: Distinguish, CardNumber, ExpMonth, ExpYear
        //    END
        //END
        //____________EXECUTING SP________________
        //--Test 1
        //SET @first = 'Kristina'
        //SET @last = 'Garcia'
        //exec[dbo].[uspGetCreditInfoForCustomer] @first, @last, @CreditInfo OUTPUT
        //SELECT 'Test1: ' + @CreditInfo
        //________OUTPUT: Card: Distinguish - **********5324 Exp: 12
        //--Test 2
        //SET @first = 'Kristina'
        //SET @last = 'Garcia Lopez'
        //exec[dbo].[uspGetCreditInfoForCustomer] @first, @last, @CreditInfo OUTPUT
        //SELECT 'Test2: ' +@CreditInfo
        //________OUTPUT: Customer Not Found
        //_____________________SECTION 4: DEBUGGING__________________
        // 1. Create or Alter the SP
        // 2. add 'PRINT' statements at each level of execution
        //EXAMPLE SP....
        //CREATE PROCEDURE uspGetOrderTrackingForCustomer
        //--ADD PARAMETERS
        //@first Varchar(40),
        //@last Varchar(40),
        //@cardNumber Varchar(200) OUTPUT
        //AS
        //BEGIN
        //  DECLARE @debug bit = 1;
        //    IF @debug PRINT 'Begin Execution' --?? vid cut off
        //--Declare variables
        //DECLARE @creditCardID Int;

        //--Get the latest sales order header using customer name
        //SELECT TOP 1 @creditCardID = SOH.CreditCardID
        //    FROM Sales.SalesOrderHeader SOH
        //      INNER JOIN Sales.Customer C ON SOH.CustomerID = C.CustomerID
        //      INNER JOIN Person.Person P ON P.BusinessEntityID = C.CustomerID
        //    WHERE P.FirstName LIKE @first AND P.LastName LIKE @last
        //    ORDER BY SOH.OrderDate DESC;

        //DECLARE @rowCount INT = @@ROWCOUNT
        //    PRINT '@rowCount: ' + CAST(@rowCount as varchar(10))
        //IF @@ROWCOUNT< 0
        //BEGIN

        //    PRINT 'Get Credit Card Number'
        //SELECT @cardNumber= 'CardNumber: ' + CardNumber

        //FROM Sales.CreditCard CC

        //    WHERE CreditCardId = @creditCardID
        //    END
        //ELSE
        //    BEGIN

        //PRINT 'Card not found'

        //SET @cardNumber = 'CardNumber: *** Not Found ***';
        //    --result = ccId: 11978, CardType: Distinguish, CardNumber, ExpMonth, ExpYear
        //    END
        //END
        // 3. Open a new window and execute the SP
        // EX. execute statement...
        //DECLARE @creditCard VARCHAR(200)
        //EXEC[dbo].[uspGetOrderTrackingForCustomer] 'Kristina', 'Garcia', @creditCard OUTPUT
        //SELECT @creditCard
        // 4. open the messages tab (next to the results tab) in SSMS
        // 5. look through each print statements to determine which section did not print
        // 6. Solution... 'get credit card number' statement did not execute
        //...the query broke on the 'If' statement. one row was found, but the if statement was checking for a row count of less than zero
        // 7. alter the procedure to 'IF @@ROWCOUNT > 0'
        // 8. Re-run the execute query

        //__________WORKING WITH VARIABLES__________
        // Variables are defined using the 'declare' statement & are prefixed with '@' --> DECLARE @count int; or DECLARE @city varchar(40) = 'KC';
        // Assigning Values: use 'set' --> SET @count = 42; OR SET @weight = @baseWeight * 1.05
        // Example 1...
        //DECLARE @velocity float;
        //DECLARE @time float;
        //DECLARE @distance float;

        //SET @velocity = 80;
        //SET @time = 2.5;
        //SET @distance = @velocity * @time;

        //PRINT 'Calculated Distance is ' + CAST(@distance as NVARCHAR); --> returns Calculated Distance is 200
        //note: bc the distance variable is a float, you must cast it as NVARCHAR so it can be included in the expression
        //...if you don't cast distance in this way you will get the following error: 'error converting data type varchar to float'
        // Example 2...
        //DECLARE @name VARCHAR(40) = 'unknown';
        //PRINT @name;
        //SET @name = 'Kansas City';
        //PRINT 'The uppercase value of ' + @name + ' is ' + UPPER(@name); --> returns: The uppercase value of Kansas City is KANSAS CITY
        // Example 3...
        //DECLARE @firstName VARCHAR(40);
        //DECLARE @personID int = 6990;

        //SELECT @firstName = FirstName
        //FROM Person.Person
        //WHERE BusinessEntityID = @personID
        //PRINT @firstName; --> returns: Julio

        //________________USING THE PRINT COMMAND_________________
        // EX. 1: PRINT 'oh hey, hi' --> returns: oh hey, hi
        // EX. 2...
        //DECLARE @myVar VARCHAR(40) = 'oh hey, hi';
        //PRINT @myVar --> returns: oh hey, hi
        // EX. 3 as an expression...
        //DECLARE @row INT = 42;
        //PRINT 'The value of row is ' + CAST(@row as VARCHAR); --> returns: The value of row is 42

        //________________USING IF/THEN LOGIC____________________
        //___EX. 1 --weight is set to zero, so the If statement block will run
        //DECLARE @pricePerPound FLOAT;
        //DECLARE @weight FLOAT;
        //DECLARE @totalPrice FLOAT;
        //SET @weight = 0;
        //SET @totalPrice = 10.98;
        //IF @weight = 0
        //BEGIN
        //    PRINT 'Welllllll shiiiiit Maaaary...the weight is zero. You cannot divide by zero. Let us set the weight at 1, shall we, dummyhead?';
        //SET @weight = 1;
        //END
        //    SET @pricePerPound = @totalPrice / @weight
        //    PRINT 'Ok, Maaaaary...the price per pound is ' + CAST(@pricePerPound as NVARCHAR)
        //___EX. 2 --the weight is set to 2, so the IF block will be skipped and the ELSE block will execute
        //DECLARE @pricePerPound FLOAT;
        //DECLARE @weight FLOAT;
        //DECLARE @totalPrice FLOAT;

        //SET @weight = 2;
        //SET @totalPrice = 10.98;

        //IF @weight = 0
        //BEGIN
        //    PRINT 'Welllllll shiiiiit Maaaary...the weight is zero. You cannot divide by zero. Let us set the weight at 1, shall we, dummyhead?';
        //SET @weight = 1;
        //END
        //    ELSE
        //BEGIN --this statement is not necessary because the PRINT statement is the only line being executed within the ELSE block
        //    PRINT 'This is a valid weight, Maaaaaary. I will do all dah sumz now...';
        //END --this statement is also unnecessary and could be removed...
        //    SET @pricePerPound = @totalPrice / @weight
        //    PRINT 'Ok, Maaaaary...the price per pound is ' + CAST(@pricePerPound as NVARCHAR)
        //___EX. 3 --using nested IF statements
        //DECLARE @pricePerPound FLOAT;
        //DECLARE @weight FLOAT;
        //DECLARE @totalPrice FLOAT;

        //SET @weight = 0;
        //SET @totalPrice = 100.98;

        //IF @weight = 0
        //BEGIN
        //    PRINT 'Welllllll shiiiiit Maaaary...the weight is zero. You cannot divide by zero. Let us set the weight at 1, shall we, dummyhead?';
        //SET @weight = 1;
        //IF @totalPrice > 100

        //BEGIN
        //    PRINT 'The total price is too large, Maaaaary...setting to 100.';
        //SET @totalPrice = 100;
        //END
        //    END
        //ELSE
        //    BEGIN

        //PRINT 'This is a valid weight, Maaaaaary. I will do all dah sumz now...';
        //IF @totalPrice > 100

        //BEGIN
        //    PRINT 'The total price is too large, Maaaaary...setting to 100.';
        //SET @totalPrice = 100;
        //END
        //    END

        //SET @pricePerPound = @totalPrice / @weight
        //PRINT 'Ok, Maaaaary...the price per pound is ' + CAST(@pricePerPound as NVARCHAR)
        //__________________USING WHILE LOOPS__________________
        //___EX. 1...
        //DECLARE @i int = 1;
        //WHILE @i <= 10
        //BEGIN
        //    PRINT 'Current Count: ' + CAST(@i AS VARCHAR);
        //SET @i = @i + 1
        //END
        //_____________________
        //___EX. 2...prints the first day of each week for 2018
        //--Setup Variables
        //DECLARE @myTable TABLE(WeekNumber int,
        //DateStarting smalldatetime)

        //DECLARE @n int = 0
        //DECLARE @firstWeek smalldatetime = '12/31/2017'

        //--loop through weeks
        //    WHILE @n <= 52
        //BEGIN
        //    INSERT INTO @myTable VALUES(@n, DATEADD(wk, @n, @firstWeek));
        //SELECT @n = @n + 1
        //END

        //--show results
        //SELECT WeekNumber, DateStarting
        //FROM @myTable
        //________________
        // DATEADD syntax --> SELECT DATEADD(wk, 1, '12/18/82'); --> returns 12/25/82...aka one week beyond 12/18/1982
        //_____USING BREAK & CONTINUE to control the behavior of the while loop...
        //--Setup Variables
        //SET NOCOUNT ON --removes the '(1 row affected)' messsage
        //    DECLARE @myTable TABLE(WeekNumber int,
        //DateStarting smalldatetime)

        //DECLARE @n int = 0
        //DECLARE @firstWeek smalldatetime = '12/31/2017'

        //--loop through weeks
        //    WHILE 1 = 1
        //BEGIN
        //    INSERT INTO @myTable VALUES(@n, DATEADD(wk, @n, @firstWeek));
        //SELECT @n = @n + 1

        //IF @n > 52 BREAK; --BREAK ends execution
        //    ELSE CONTINUE --goes to the top of the while loop and starts again

        //PRINT 'THIS WILL NEVER GET PRINTED.' --unless you remove the ELSE CONTINUE statement

        //    END

        //--show results
        //SELECT WeekNumber, DateStarting
        //FROM @myTable
        //___________WAITFOR uses_________________
        // 1. 'waitfor delay'
        // 2. 'waitfor time'
        // ...execution is blocked until the waitfor command completes
        //___EX. 1...
        //PRINT CONVERT(VARCHAR, GETDATE(), 109); --prints the current date and time
        //WAITFOR DELAY '00:00:05' --HH:MM:SS
        //PRINT CONVERT(VARCHAR, GETDATE(), 109); --prints the current date and time after a 5 second delay
        //--EX. 2. monitoring DB log growth...the following statement will loop over the database log every 5 seconds and display the row count
        //DECLARE @row int = 1;
        //DECLARE @i INT = 1;
        //WHILE @i <= 10
        //BEGIN
        //    Select @row = COUNT(*) FROM DatabaseLog;
        //WAITFOR DELAY '00:00:05'; --5 second delay

        //PRINT 'Row Count: ' + CAST(@ROW AS VARCHAR);
        //SET @i = @i + 1
        //END
        //________________GOTO & Labels___________________
        // the GOTO command 'jumps' program execution to the specified label
        // WARNING: the following may not be suitable for developers...Rated S (for spaghetti code)
        // ...using GOTO can make maintaining code more difficult. Far mo'bettah to use: IF, THEN, AND WHILE loops,
        //--EX. 1. using GOTO --prints current count up to 10
        //DECLARE @i INT = 1;
        //START:
        //PRINT 'Current Count: ' + CAST(@i AS VARCHAR);
        //SET @i = @i + 1

        //IF @i< 11 GOTO START
        //--EX. 2: mo'bettah way to write the above statement. --prints current count up to 10
        //DECLARE @i INT = 1;
        //WHILE @i <= 10
        //BEGIN
        //    PRINT 'Current Count: ' + CAST(@i AS VARCHAR);
        //SET @i = @i + 1
        //END
        //______________RETURN Command_________________________
        // the 'return' command unconditionally exits from the SP. similar to 'break', which exits from inner most 'while' loops
        // EX. 1. --calculating @velocity = @distance / @time  --> returns velocity: 2.66666666666667
        //DECLARE @velocity FLOAT;
        //DECLARE @distance FLOAT = 120.0;
        //DECLARE @time FLOAT = 45;

        //IF @time = 0.0 RETURN -- don't divide by zero dummy
        //SET @velocity = @distance / @time
        //SELECT @velocity;
        //__________
        // EX. 2. --calculating @velocity = @distance / @time  --> executes command without printing velocity
        //DECLARE @velocity FLOAT;
        //DECLARE @distance FLOAT = 120.0;
        //DECLARE @time FLOAT = 0.0;

        //IF @time = 0.0 RETURN -- don't divide by zero dummy
        //SET @velocity = @distance / @time
        //SELECT @velocity;
        //_________SECTION 5 PRACTICE ASSIGNMENT___________
        //--write T-SQL script that prints even numbers 2-20.
        //DECLARE @i INT = 2;
        //WHILE @i <= 20
        //BEGIN
        //--Within each loop delay execution 1 second.
        //    WAITFOR DELAY '00:00:01';
        //PRINT 'Current Count: ' + CAST(@i as VARCHAR);
        //SET @i = @i + 2
        //IF @i = 10
        //    --If the number is 10, also print halfway there!
        //PRINT 'halfway there!'
        //END
        //_______REFACTORED_____
        //--write T-SQL script that prints even numbers 2-20.
        //DECLARE @i INT = 2;
        //WHILE @i <= 20
        //BEGIN
        //    PRINT @i;
        //--If the number is 10, also print halfway there!

        //IF @i = 10 PRINT 'halfway there!'
        //--Within each loop delay execution 1 second.
        //    WAITFOR DELAY '00:00:01';
        //SET @i = @i + 2
        //END
        //_________________________________
        //________ERROR SYSTEM FUNCTION [@@ERROR]____________
        // returns the error number for the last statement executed
        // zero is returned if there is no error
        // the value is reset each time a statement is executed
        // because it is reset, check immediately after executing the statement or save to a variable to check later
        // to view a list of error messages...
        // SELECT* FROM sys.messages
        // EX. 1...
        //--unstructured error handling using @@ERROR
        //    DECLARE @a FLOAT = 10.0;
        //DECLARE @b FLOAT = 2.0;
        //DECLARE @c FLOAT;

        //SET @c = @a / @b;
        //IF @@ERROR<> 0 PRINT 'Result: Division Error';
        //ELSE PRINT 'Result: ' + CAST(@c as VARCHAR); --returns Result: 5

        //SET @b = 0;
        //SET @c = @a / @b;
        //IF @@ERROR<> 0 PRINT 'Result: Division Error'; --returns 'Divide by zero error encountered.' && Result: Division Error
        //ELSE PRINT 'Result: ' + CAST(@c as VARCHAR);
        //_________________________________
        //____________TRY/CATCH____________
        // try catch blocks are a way to run code and trap errors
        //..when an error occurs, execution jumps to the catch block
        //..i.e. when an error is thrown, it is caught by the catch block
        // EX.1 ...
        //--Try/Catch Block: generate divide-by-zero error.
        //BEGIN TRY
        //SELECT 1/0;
        //END TRY
        //BEGIN CATCH
        //SELECT
        //    ERROR_NUMBER() AS ErrorNumber --returns 8134
        //    , ERROR_SEVERITY() AS ErrorSeverity --returns 16
        //    , ERROR_STATE() AS ErrorState --returns 1
        //    , ERROR_PROCEDURE() AS ErrorProcedure --returns null
        //    , ERROR_LINE() AS ErrorLine --returns 3
        //    , ERROR_MESSAGE() AS ErrorMessage; --returns 'divide by zero error encountered
        //END CATCH
        //____________
        // EX. 2
        //DECLARE @TotalHours FLOAT,
        //        @TotalEmployees FLOAT,
        //        @AVGHours FLOAT
        //BEGIN TRY
        //    SELECT @TotalHours = SUM(VacationHours)
        //    FROM HumanResources.Employee;

        //SELECT @TOTALEmployees = COUNT(*)
        //FROM HumanResources.Employee

        //SET @AVGHours = @TotalHours / @TotalEmployees;
        //PRINT 'Average Vacation Hours for Employees: ' + CAST(@AVGHours as VARCHAR); --returns 50.6138
        //END TRY
        //BEGIN CATCH
        //PRINT N'Error Procedure = ' + ERROR_PROCEDURE()
        //PRINT N'Error State = ' + CAST(ERROR_STATE() AS VARCHAR)
        //PRINT N'Error Severity = ' + CAST(ERROR_SEVERITY() AS VARCHAR)
        //PRINT N'Error Message = ' + ERROR_MESSAGE()
        //PRINT N'Error Number = ' + CAST(ERROR_NUMBER() AS VARCHAR)
        //PRINT N'Error Line = ' + CAST(ERROR_LINE() AS VARCHAR)
        //END CATCH;
        //_________________
        //--Ex. 2...FORCING AN ERROR...
        //DECLARE @TotalHours FLOAT,
        //        @TotalEmployees FLOAT,
        //        @AVGHours FLOAT

        //    BEGIN TRY
        //    SELECT @TotalHours = SUM(VacationHours)

        //FROM HumanResources.Employee;

        //SELECT @TOTALEmployees = COUNT(*)

        //FROM HumanResources.Employee

        //WHERE 1 = 0; --FORCING AN ERROR

        //    SET @AVGHours = @TotalHours / @TotalEmployees;
        //PRINT 'Average Vacation Hours for Employees: ' + CAST(@AVGHours as VARCHAR); --returns 50.6138
        //END TRY
        //BEGIN CATCH

        //PRINT N'Error Procedure = ' + ERROR_PROCEDURE()

        //PRINT N'Error State = ' + CAST(ERROR_STATE() AS VARCHAR) --RETURNS 1
        //PRINT N'Error Severity = ' + CAST(ERROR_SEVERITY() AS VARCHAR) --RETURNS 16
        //PRINT N'Error Message = ' + ERROR_MESSAGE() --RETURNS DIVIDE BY ZERO ERROR ENCOUNTERED

        //PRINT N'Error Number = ' + CAST(ERROR_NUMBER() AS VARCHAR) --RETURNS 8134
        //PRINT N'Error Line = ' + CAST(ERROR_LINE() AS VARCHAR) --RETURNS 14
        //END CATCH;
        //______________ASSIGNMENT 2 UNSTRUCTURED ERROR HANDLING__________________________
        //--Write a script to detect a divide by zero error.
        //--1. Declare three variables, @a, @b, @c as float.
        //--2. initialize @a and @b to any number.
        //    DECLARE @a FLOAT = 42,
        //            @b FLOAT = 0,
        //            @c FLOAT
        //--3. Set @c to @a divided by @b.
        //    IF @b = 0
        //SET @b = 2;
        //SET @c = @a / @b;
        //    --4. If an error occurs, such as a divide by zero because @b = 0, then set @b to 2 and recalculate @c.
        //    IF @@ERROR<> 0 PRINT 'Result: Division Error';
        //ELSE PRINT 'Result: ' + CAST(@c as VARCHAR);
        //___________________________________________
        //______________ASSIGNMENT 2 UNSTRUCTURED ERROR HANDLING REFACTORED__________________________
        //--Write a script to detect a divide by zero error.
        //--1. Declare three variables, @a, @b, @c as float.
        //--2. initialize @a and @b to any number.
        //    DECLARE @a FLOAT = 42;
        //    DECLARE @b FLOAT = 0;
        //    DECLARE @c FLOAT;
        //--3. Set @c to @a divided by @b.
        //    SET @c = @a / @b;
        //IF @@ERROR = 0 GOTO PrintResults
        //SET @b = 2
        //SET @c = @a / @b
        //PrintResults:
        //PRINT '@a = ' + CAST(@a as VARCHAR);
        //PRINT '@b = ' + CAST(@b as VARCHAR);
        //PRINT '@a / @b = ' + CAST(@c as VARCHAR);
        //___________________________________________
        //______________ASSIGNMENT 2 STRUCTURED ERROR HANDLING__________________________
        //--Write a script to detect a divide by zero error.

        //--1. Declare three variables, @a, @b, @c as float.
        //--2. initialize @a and @b to any number.
        //    DECLARE @a FLOAT = 42,
        //@b FLOAT = 0,
        //    @c FLOAT
        //--3. Set @c to @a divided by @b.
        //    BEGIN TRY
        //IF @b = 0
        //    --SET @b = 2;
        //SET @c = @a / @b;
        //    --4. If an error occurs, such as a divide by zero because @b = 0, then set @b to 2 and recalculate @c.
        //    PRINT 'C = ' + CAST(@c as VARCHAR);
        //END TRY
        //BEGIN CATCH
        //PRINT N'Error Procedure = ' + ERROR_PROCEDURE()
        //PRINT N'Error State = ' + CAST(ERROR_STATE() AS VARCHAR)
        //PRINT N'Error Severity = ' + CAST(ERROR_SEVERITY() AS VARCHAR)
        //PRINT N'Error Message = ' + ERROR_MESSAGE()
        //PRINT N'Error Number = ' + CAST(ERROR_NUMBER() AS VARCHAR)
        //PRINT N'Eror Line = ' + CAST(ERROR_LINE() AS VARCHAR)
        //END CATCH;
        //___________________________________________
        //______________ASSIGNMENT 2 STRUCTURED ERROR HANDLING REFACTORED__________________________
        //--Write a script to detect a divide by zero error.
        //--1. Declare three variables, @a, @b, @c as float.
        //--2. initialize @a and @b to any number.
        //    DECLARE @a FLOAT = 42,
        //@b FLOAT = 0,
        //    @c FLOAT
        //--3. Set @c to @a divided by @b.
        //    BEGIN TRY
        //SET @c = @a / @b;
        //END TRY
        //BEGIN CATCH
        //--4. If an error occurs, such as a divide by zero because @b = 0, then set @b to 2 and recalculate @c.
        //    SET @b = 2
        //SET @c = @a / @b;
        //END CATCH
        //PRINT '@a = ' + CAST(@a as VARCHAR);
        //PRINT '@b = ' + CAST(@b as VARCHAR);
        //PRINT '@c = ' + CAST(@c as VARCHAR);
        //____________SECTION 7____________
        // Parts of a SP...
        // INPUTS: SP Name & Parameters
        // EXECUTION: Commands--for example. SET NOCOUNT ON, SELECT Statement, FROM Statement, Parameter used as criteria
        // OUTPUT: Where Clause & result set returned
        // Notes: use CREATE PROCEDURE to create, ALTER PROCEDURE to modify, preface SP names with 'usp' -user stored procedure
        //...as opposed to those SPs that have been defined by the system.
        // PARAMETERS: used to provide values to the procedure. naming convention for parameters... '@parameterName'
        //_________DEFINING A SP WITH PARAMETERS________________________
        //--find a person and display their first & last name using a parameter
        //CREATE PROCEDURE dbo.uspGetPerson --sp name
        //@businessEntityID INT --parameter
        //    AS
        //BEGIN
        //--variables...accepts one parameter & prints full name
        //    DECLARE @first VARCHAR(40)
        //DECLARE @last VARCHAR(40)
        //SELECT @first = FirstName
        //    , @last = LastName
        //FROM Person.Person --table
        //    WHERE Person.BusinessEntityID = @businessEntityID --BusinessEntityID is the primary key of the Person table & will return one record upon match
        //PRINT 'me llama ' + @first + ' ' + @last
        //    END --F5 to execute/create the SP

        //EXECUTE uspGetPerson 42 --returns 'me llama James Kramer
        //__________RETURN RESULTS WITH SELECT_____________________
        //NOTES: results of SELECT statements are sent directly to the client. You can have more than one SELECT statement
        //--EX. return a value using select
        //    USE WideWorldImporters;
        //GO

        //    CREATE PROCEDURE Application.uspFindCountry --sp name

        //@CountryID[INT] --parameter
        //    AS
        //BEGIN
        //    SET NOCOUNT ON;
        //SELECT CountryName,
        //    LatestRecordedPopulation

        //FROM Application.Countries --table
        //    WHERE CountryID = @CountryID

        //END --F5 to run the SP

        //--Run the SP and see results in the results window
        //    EXECUTE Application.uspFindCountry 45 --returns CountryName = Chile / LatestRecordedPopulation = 16601707
        //___________FACT CHECKING THE QUERY ABOVE______________
        //--setting up a temp table
        //    DECLARE @Country TABLE(
        //    CountryName NVARCHAR(60),
        //LatestRecordedPopulation BIGINT
        //)
        //--Add results to the temp table
        //INSERT @Country(CountryName, LatestRecordedPopulation)
        //EXECUTE Application.uspFindCountry 45
        //--Prove results are in the table
        //SELECT COUNT(1) as Proof FROM @Country
        //    SELECT * FROM @Country --returns CountryName = Chile / LatestRecordedPopulation = 16601707
        //_____________USING OUTPUT IN PARAMETERS________________
        // 1. Define parameter with the 'OUTPUT' keyword
        // 2. The calling program must also use the 'OUTPUT' keyword
        // EX. EXECUTE uspMyProcedure @a, @b, @c OUTPUT
        //// EX. 1______________
        //--Calculate Area
        //CREATE PROCEDURE uspCalcArea
        //    @height FLOAT,
        //    @width FLOAT,
        //    @area FLOAT OUTPUT
        //AS
        //    BEGIN
        //--Set NOCOUNT to ON to no longer display the count message
        //    SET NOCOUNT ON;
        //SET @area = @height * @width;
        //END
        // EX 2. EXECUTE STATEMENT_______________
        //--Execute statement
        //DECLARE @result FLOAT;
        //EXECUTE uspCalcArea 11.0, 20.0, @result OUTPUT;
        //PRINT 'The area is ' + CAST(@result AS VARCHAR);
        // EX. 3_________________________________
        //--Create Full Name
        //CREATE PROCEDURE uspFullName
        //@first NVARCHAR(40),
        //@last NVARCHAR(40),
        //@full NVARCHAR(80) OUTPUT,
        //@initials NVARCHAR(4) OUTPUT
        //AS
        //BEGIN
        //--Set NOCOUNT to ON to no longer display the count message
        //    SET NOCOUNT ON;
        //IF LEN(@first) > 0
        //BEGIN
        //    SET @full = @first + ' ' + @last;
        //SET @initials = LEFT(@first, 1) + LEFT(@last, 1);
        //END;
        //ELSE
        //    BEGIN
        //SET @full = @last;
        //SET @initials = LEFT(@last, 1);
        //END;
        //END --F5 to run the SP
        //_______________
        //--execute statement for uspFullName____________
        //DECLARE @name NVARCHAR(80)
        //DECLARE @full NVARCHAR(80)
        //DECLARE @initials NVARCHAR(10)
        //EXECUTE uspFullName 'Slider', 'theFish', @full OUTPUT, @initials OUTPUT
        //    PRINT 'Full Name: ' + @full; --returns Slider theFish
        //    PRINT 'Initials: ' + @initials --returns St
        //________________RETURN CODES____________________
        // Return Codes:
        // should be used to indicate the execution status of a procedure
        // Can be saved in a variable
        // are a form of legacy error handling
        // use TRY/CATCH/THROW error handling
        // should not be used to return application data
        //// EX.
        //--Calculate speed...
        //CREATE PROCEDURE uspCalcSpeed
        //    @distance float,
        //    @time float,
        //    @speed float OUTPUT

        //AS
        //    BEGIN TRY
        //    SET @speed = @distance / @time;
        //RETURN 0
        //END TRY
        //BEGIN CATCH

        //RETURN 1 --represents a failure
        //    END CATCH;

        //--Execute the Calculate speed SP...
        //DECLARE @speed float;
        //DECLARE @returnValue int;
        //EXECUTE @returnValue = uspCalcSpeed 120, 60, @speed OUTPUT
        //SELECT @speed as Speed, @returnValue AS ReturnValue --returns Speed = 2, ReturnValue = 0
        //    --Execute the Calculate speed SP to produce an error...
        //DECLARE @speed float;
        //DECLARE @returnValue int;
        //EXECUTE @returnValue = uspCalcSpeed 120, 0, @speed OUTPUT
        //SELECT @speed as Speed, @returnValue AS ReturnValue --returns Speed = NULL, ReturnValue = 1
        //    --refactored to print an error message...
        //DECLARE @speed float;
        //DECLARE @returnValue int;
        //EXECUTE @returnValue = uspCalcSpeed 120, 4, @speed OUTPUT
        //IF @returnValue<> 0 PRINT 'Unable to calculate speed'
        //ELSE PRINT 'Speed = ' + CAST(@speed AS VARCHAR);

        //SELECT @speed as Speed, @returnValue AS ReturnValue --returns Speed = NULL, ReturnValue = 1
        //____________SECTION QUIZ________________
        //--Write a stored procedure to query the Person.Person table, and return the person's fullname as an output parameter.
        //--The stored procedure should accept the BusinessEntityID as a parameter, so it can be use to match against the Person.Person's primary key.
        //--If the stored procedure runs successfully return a value of 0; otherwise return a value of -1.
        //ALTER PROCEDURE uspGetFullName
        //    @businessEntityId INT,
        //@fullName VARCHAR(100) OUTPUT
        //    AS
        //BEGIN
        //    DECLARE @returnCode int = 0;
        //BEGIN TRY
        //DECLARE @firstName VARCHAR(40);
        //DECLARE @lastName VARCHAR(40);
        //SET NOCOUNT ON;
        //SELECT @firstName = FirstName,
        //    @lastName = LastName
        //FROM Person.Person P
        //    WHERE P.BusinessEntityID = @businessEntityId;
        //SET @fullName = @lastName;
        //IF LEN(@firstName) > 0
        //SET @fullName = @firstName + ' ' + @lastName;
        //END TRY
        //BEGIN CATCH
        //SET @returnCode = -1;
        //END CATCH
        //RETURN @returnCode
        //END
        ////_______EXECUTE STATEMENT________
        //DECLARE @name NVARCHAR(80)
        //DECLARE @full NVARCHAR(80)
        //DECLARE @retCode int
        //    EXECUTE @retCode = uspGetFullName 1, @full OUTPUT
        //IF @retCode = 0

        //PRINT 'Full Name: ' + @full;
        //ELSE
        //    PRINT 'Nobody here by that name...go fish!'; --returns Ken Sanchez
        //_____________TRANSACTIONS__________________
        // A transaction is a single unit of work.
        // completed transaction changes become a permanent part of the db. the changes are committed.
        // cancelled transaction changes are erased. the changes are rolled back
        // transactions are used to logically group db operations (for example: money transfers)
        // ___3 key commands and one function to implement a transaction...
        // BEGIN TRANSACTION / COMMIT TRANSACTION / ROLLBACK / @@TRANCOUNT
        // key points...
        // transactions can be nested
        // transactions can span stored procedure calls
        // you can name transactions...helpful when a transaction is nested
        //______________CURSORS________________________
        // A db cursor can be thought of as a pointer to a specific row within a query result
        // the pointer can be moved from one row to the next
        // depending on the type of cursor you may be able to move it to the previous row.
        //____WHY USE CURSORS____________________
        // sql is a set-based languagge, meaning operations are completed on all or rows of the result.
        // there are times when you want to do operations on a row by row basis--enter cursors..
        //____STEPS TO DEFINE A CURSOR_____________
        // Declare variables-->declare cursor-->fetch values into variables--> test status and loop--> close cursor--> deallocate cursor
        //____TYPES OF CURSOR SYNTAX_________________________
        // ISO
        // Extended T-SQL
        //____EXAMPLE
        //SELECT BusinessEntityID,
        //    FirstName,
        //    LastName
        //FROM Person.Person

        //--Setup Cursor

        //--1. Declare Variables
        //DECLARE @businessEntityID as INT;
        //DECLARE @firstName as NVARCHAR(50),
        //@lastName as NVARCHAR(50);
        //    --2. Declare Cursor
        //DECLARE @personCursor as CURSOR;
        //SET @personCursor = CURSOR FOR
        //    SELECT BusinessEntityID,
        //FirstName,
        //LastName
        //    FROM Person.Person
        //    OPEN @personCursor
        //--3. Fetch Values into Cursor
        //FETCH NEXT FROM @personCursor INTO @businessEntityID,
        //    @firstName,
        //    @lastName
        //        --4. Test Fetch Status & Loop
        //    WHILE @@FETCH_STATUS = 0
        //BEGIN
        //    PRINT CAST(@BusinessEntityID as VARCHAR(50))
        //    + ' - ' + @firstName
        //+ ' ' +	  @lastName;
        //-- Try another fetch
        //    FETCH NEXT FROM @personCursor INTO @businessEntityID,
        //@firstName,
        //@lastName
        //    END
        //--5. Close Cursor
        //CLOSE @personCursor;
        //    --6. De-allocate Cursor
        //DEALLOCATE @personCursor; --results will be seen in the Messages section and include the biz id, first name and last name of every employee in the db
        //______________FETCH______________________
        // Keywords: NEXT, PRIOR, LAST , FIRST, ABSOLUTE n, RELATIVE n
        // EX. 1: FETCH RELATIVE 2 FROM @contact_cursor INTO @LastName, @FirstName; --skips two records
        // EX. 2: FETCH ABSOLUTE 2 FROM @contact_cursor INTO @LastName, @FirstName; --returns the second row
        // Statuses...
        // 0 = FETCH statement was successful
        // -1 = statement failed or the row was beyond the result set
        // -2 = the row fetched is missing
        // -9 = the cursor is not performing a fetch operation
        // ____________CASE STUDY___________________
        // randomly select every five employees and print the employee number and name for a survey
        //CREATE PROCEDURE dbo.uspEmployeeSurvey
        //    AS
        //BEGIN
        //    SET NOCOUNT ON;

        //DECLARE @recordsToSkip int = 1;
        //--1. Declare Variables
        //DECLARE @businessEntityID INT,
        //@firstName as NVARCHAR(50),
        //@lastName as NVARCHAR(50);
        //    --2. Declare Cursor
        //DECLARE employee_cursor SCROLL CURSOR FOR
        //    SELECT P.BusinessEntityID,
        //P.FirstName,
        //P.LastName
        //    FROM HumanResources.Employee E

        //INNER JOIN Person.Person P on E.BusinessEntityID = P.BusinessEntityID
        //    WHERE E.CurrentFlag = 1

        //OPEN employee_cursor

        //SET @recordsToSkip = ROUND(((RAND() * 4) + 1), 0)

        //    --3. Fetch Values into Cursor
        //FETCH RELATIVE @recordsToSkip FROM employee_cursor
        //    INTO @BusinessEntityID, @FirstName, @LastName
        //    WHILE @@FETCH_STATUS = 0
        //BEGIN
        //    PRINT ' skipped ' + CAST(@recordsToSkip as NVARCHAR) + ' - ' +@LastName + ', ' + @FirstName + ' '
        //+ ' - ' + @firstName
        //+ ' ' +	  @lastName;
        //SET @recordsToSkip = ROUND(((RAND() * 4) + 1), 0)

        //FETCH RELATIVE @recordsToSkip FROM employee_cursor
        //    INTO @BusinessEntityID, @FirstName, @LastName

        //    END

        //CLOSE employee_cursor
        //--6. De-allocate Cursor
        //DEALLOCATE employee_cursor; --results will be seen in the Messages section and include the biz id, first name and last name of every employee in the db
        //END

        //--EXECUTE SP
        //EXECUTE dbo.uspEmployeeSurvey;
    }
}