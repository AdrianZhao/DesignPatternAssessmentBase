/*
 This assessment is designed to gauge your ability to solve a problem and explain your solution, 
 not necessarily your ability to code in any specific syntax. You will provide  
 a short demonstration of your planned approach to the problem. You may add as much code, 
 pseudocode, or notes to your project as you wish to aid in explaining your solution. 

 Be as detailed as possible in your explanation; show where and what changes you will make, 
 preferably with names and properties picked out already, and indicate why you are making 
 these changes.

 All required changes should be made in the Models that have been already provided, or in a 
 new class. No code needs to be added or changed in Program.cs.
*/

using DesignPatternAssessmentBase.Models;

Console.WriteLine("Good luck!");
TicketFactory bugReportFactory = new BugReportFactory();
Ticket bugReport = bugReportFactory.makeTicket("Error Codes");
TicketFactory requestFactory = new RequestFactory();
Ticket request = requestFactory.makeTicket(RequestType.Information);
bugReport.SetAssign(4);
bugReport.SetResolve(16);
ModifyHourDecorator bugReportWithModifiers = new TypeBugReport(bugReport);
bugReportWithModifiers = new WhiteGloveClient(bugReportWithModifiers);
bugReportWithModifiers.CalculateTime();
request.SetAssign(5);
request.SetResolve(10);
ModifyHourDecorator requestWithModifiers = new BacklogReissue(request);
requestWithModifiers.CalculateTime();