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
    }
}