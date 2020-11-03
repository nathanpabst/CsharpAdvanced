﻿using System;
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
    }
}