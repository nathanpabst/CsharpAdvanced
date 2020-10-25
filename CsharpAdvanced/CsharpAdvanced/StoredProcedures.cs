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
    }
}