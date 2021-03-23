# Introduction

The project in `rfp-dates` will create a series of ics/iCalendar files
which are count down reminders for an RFP Due date.

Reference:  
https://en.wikipedia.org/wiki/ICalendar  
https://tools.ietf.org/html/rfc5545  

# Usage

This is a simple command line program written in C# using the .Net
Framework v5.0. 

To use copy the `CurrentRFP.cs` file to `MyCurrentRFP.cs` and change the
class name and constructor name to `MyCurrentRFP.cs`. Then edit your
RFP details in the `MyCurrentRFP.cs` file. (`MyCurrentRFP.cs` is in 
the `.gitignore` file to prevent checking specific RFP details.)

The `target` is the short name for the event and is used as the event
subject. This string is also sanitized and used as the ical file name
(with the `.ical` extension) appended.

The `dueDate` is the date on which the submission is due. The event
for this date does not show the number of days left. All the other
events have the number of days to the submission date in the subject.

The `description` is added to the body of all the events.

The `ownerEmail` should be your calendar's email account.

Then run the program with something like:
```
cd rfp_dates
dotnet build
dotnet run
```
(or press the `Run` button in Visual Studio.)

# Future Direction

Have a better place to store the resulting ical files. Right now
is is the MacOS `Downloads` directory. Actually *my* `Downloads`
directory on *my* personal Mac. Not very portable.

Accepting the arguments from the command line would be good. (Maybe,
I prefer the `MyCurrentRFP.cs` file method at the moment so I can
have my sensitve changes saved in a file for later reference. And
because there would be a pile of arguments. `yaml` files are not
a good solution; the interested user is probably `C#` enabled, so 
why not have this stuff in `C#`.)

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



