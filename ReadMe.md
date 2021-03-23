# Introduction

The project in `rfp-dates` will create a series of ics/iCalendar files
which are count down reminders for an RFP Due date.

Reference:  
https://en.wikipedia.org/wiki/ICalendar  
https://tools.ietf.org/html/rfc5545  

# Usage

This is a simple command line program written in C# using the .Net
Framework v5.0. To use, first edit the few lines in the `Main` static
function to set the variables for `target`, `dueDate` and
`description`.

The `target` is the short name for the event and is used as the event
subject. This string is also sanitized and used as the ical file name
(with the `.ical` extension) appended.

The `dueDate` is the date on which the submission is due. The event
for this date does not show the number of days left. All the other
events have the number of days to the submission date in the subject.

The `description` is added to the body of all the events.

# Future Direction

Accepting the arguments from the command line would be good.

# Code

The code is in the `rfp_dates` folder. All the code is in
`Program.cs`. `Main` sets up the call to `GenerateICSLadder`.
`GenerateICSLadder` is the loop which walks back from the due date to
today's date, creating a reminder for each weekday. `ReminderICS`
generates a single ical file.

Below `ReminderICS` are samples of generating single ical files. They
are useful refernece for other types of ical events.

The documentation in the IETF RFC's are accurate but impossible to
read.  Best to search on stack overflow. `iCalendar.org` offers a
gentle overview but is click-baity.



