using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CsharpAdvanced
{
    internal class StoredProcedures
    {
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
        //-- SET NOCOUNT ON added to prevent extra result sets from
        //-- interfering with SELECT statements.
        //    SET NOCOUNT ON;

        //    -- Insert statements for procedure here

        //SELECT @a + @b;
        //END
        // Executing the SP...
        // 1. in SSMS, click create new query
        // 2. EXEC dbo.uspAddNumber 5.0, 15.0
        // 3. F5 to run the query
        //__________PARTS OF A SP section 3_____________
        // Parameters...
        // SPs can accept parameter values as inputs
        // modified values can be passed back to the calling program
        // Execution...
        // SPs can execute SQL statement, utilize conditional logic such as IF THEN or CASE statements
        // ...and looping constructs to perform tasks like WHILE loops
        // a SP is able to call another SP
        // use Cursors
        // Outputs...
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
        // 2. create query to get SalesOrderID / get the latest sales order header...
        //SELECT TOP 1 SOH.SalesOrderID
        //    FROM Sales.SalesOrderHeader SOH
        //INNER JOIN Sales.Customer C ON SOH.CustomerID = C.CustomerID
        //    INNER JOIN Person.Person P ON P.BusinessEntityID = C.CustomerID
        //    WHERE P.FirstName LIKE 'Kristina' AND P.LastName LIKE 'Garcia'
        //ORDER BY SOH.OrderDate DESC
        //--result = 73823
        // 3. Get credit card number/create query to get Credit Card
        //SELECT CC.CreditCardId, CC.CardType, CC.CardNumber, CC.ExpMonth, CC.ExpYear
        //    FROM Sales.CreditCard CC
        //INNER JOIN Sales.SalesOrderHeader SOH ON CC.CreditCardID = SOH.CreditCardID
        //    AND SOH.SalesOrderID = 73823
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
        //    FROM Sales.SalesOrderHeader SOH
        //INNER JOIN Sales.Customer C ON SOH.CustomerID = C.CustomerID
        //    INNER JOIN Person.Person P ON P.BusinessEntityID = C.CustomerID
        //    WHERE P.FirstName LIKE @first AND P.LastName LIKE @last
        //    ORDER BY SOH.OrderDate DESC;
        //--result = 73823

        //--Get credit card number
        //IF @@ROWCOUNT > 0
        //BEGIN
        //    SELECT @CreditInfo = 'Card: ' + CC.CardType + ' - ' +
        //Replicate('*', LEN(CC.CardNumber) - 4) + RIGHT(CC.CardNumber, 4) +
        //    ' Exp: ' + CAST(CC.ExpMonth as varchar(2))
        //FROM Sales.CreditCard CC
        //    INNER JOIN Sales.SalesOrderHeader SOH ON CC.CreditCardID = SOH.CreditCardID
        //    END
        //ELSE
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
        //  INNER JOIN Sales.Customer C ON SOH.CustomerID = C.CustomerID
        //    INNER JOIN Person.Person P ON P.BusinessEntityID = C.CustomerID
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
        //____WORKING WITH VARIABLES__________
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
        //________________PRINT COMMAND_________________
        // EX. 1: PRINT 'oh hey, hi' --> returns: oh hey, hi
        // EX. 2...
        //DECLARE @myVar VARCHAR(40) = 'oh hey, hi';
        //PRINT @myVar --> returns: oh hey, hi
        // EX. 3 as an expression...
        //DECLARE @row INT = 42;
        //PRINT 'The value of row is ' + CAST(@row as VARCHAR); --> returns: The value of row is 42
        //___________________IF/THEN LOGIC____________________
        // EX. 1 --weight is set to zero, so the If statement block will run
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
        //    PRINT 'Ok, Maaaaary...the price per pund is ' + CAST(@pricePerPound as NVARCHAR)
        // EX. 2 --the weight is set to 2, so the IF block will be skipped and the ELSE block will execute
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
        //    PRINT 'Ok, Maaaaary...the price per pund is ' + CAST(@pricePerPound as NVARCHAR)
        // EX. 3 --using nested IF statements
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
        //__________________WHILE LOOPS__________________
        // EX. 1...
        //DECLARE @i int = 1;
        //WHILE @i <= 10
        //BEGIN
        //    PRINT 'Current Count: ' + CAST(@i AS VARCHAR);
        //SET @i = @i + 1
        //END
        //_____________________
        // EX. 2...prints the first day of each week for 2018
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
        // using BREAK & CONTINUE to control the behavior of the while loop...
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
        // EX. 1...
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
        //, ERROR_STATE() AS ErrorState --returns 1
        //    , ERROR_PROCEDURE() AS ErrorProcedure --returns null
        //, ERROR_LINE() AS ErrorLine --returns 3
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
        //PRINT N'Eror Line = ' + CAST(ERROR_LINE() AS VARCHAR)
        //END CATCH;
        //_________________
        //--Ex. 2...FORCING AN ERROR...
        //DECLARE @TotalHours FLOAT,
        //@TotalEmployees FLOAT,
        //    @AVGHours FLOAT

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
        //PRINT N'Eror Line = ' + CAST(ERROR_LINE() AS VARCHAR) --RETURNS 14
        //END CATCH;
        //______________ASSIGNMENT 2 UNSTRUCTURED ERROR HANDLING__________________________
        //--Write a script to detect a divide by zero error.
        //--1. Declare three variables, @a, @b, @c as float.
        //--2. initialize @a and @b to any number.
        //    DECLARE @a FLOAT = 42,
        //@b FLOAT = 0,
        //    @c FLOAT
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
        //DECLARE @b FLOAT = 0;
        //DECLARE @c FLOAT;
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
        // EXECUTION: Commands--for example. SET NOCOUNT ON, Select Statement, From Statement, Parameter used as criteria
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
        //    WHERE Person.BusinessEntityID = @businessEntityID--BusinessEntityID is the primary key of the Person table & will return one record upon match
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
        //    WHERE CountryID = @CountryID--BusinessEntityID is the primary key of the Person table & will return one record upon match

        //END --F5 to run the SP

        //--Run the SP and see results in the results window
        //    EXECUTE Application.uspFindCountry 45 --returns CountryName = Chile / LatestRecordedPopulation = 16601707
    }
}