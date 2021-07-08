using System;
using System.Text;
using System.IO;

// reference material
// https://stackoverflow.com/questions/46033843/how-to-create-ics-file-using-c
// https://stackoverflow.com/questions/1716237/single-day-all-day-appointments-in-ics-files
// https://www.iana.org/time-zones
// https://en.wikipedia.org/wiki/Tz_database

namespace rfp_dates {
    class Program {
        static string baseDirectory = "/Users/markhahn/Downloads"; 
        static void Main(string [] args) {

            Console.WriteLine (Environment.SpecialFolder.Personal);
            Console.WriteLine (Environment.GetFolderPath (Environment.SpecialFolder.UserProfile));
            RFP x = new MyCurrentRFP ();

            GenerateICSLadder (x);
        }

        private static void GenerateICSLadder(RFP currentRFP) {
                    var currentTime = DateTime.Now.AddDays (-1);
            Console.WriteLine (currentTime.ToLongDateString ());
            var currentDay = new DateTime (currentTime.Year, currentTime.Month, currentTime.Day);
            Console.WriteLine (currentDay.ToLongDateString ());

            var baseText = " days to ";
            var basename = currentRFP.Target.Replace (' ', '_').Replace ('/', '_')
                .Replace (':', '_').Replace ('\'', '_').Replace ('.', '_').Replace (',', '_');
            var dueBasename = basename + "_due";
            var dueTitle = currentRFP.Target + " due today";
            Console.WriteLine ("{0:yyyy-MM-dd} {1} {2}", currentRFP.DueDate, dueTitle, dueBasename);
            ReminderICS (currentRFP.DueDate, dueTitle, dueBasename, currentRFP.Description, currentRFP.OwnerEmail);

            int countDown = 1;
            var nextDate = currentRFP.DueDate.AddDays (-1);
            while (nextDate > currentDay) {
                bool createReminder = true;
                // no reminders on weekends
                if ((nextDate.DayOfWeek == DayOfWeek.Saturday) || (nextDate.DayOfWeek == DayOfWeek.Sunday)) {
                    createReminder = false;
                }

                if (countDown > 14) {
                    if (countDown % 2 == 1) {
                        createReminder = false;
                    }
                }

                if (createReminder) {
                    var name = countDown.ToString ("#") + baseText + currentRFP.Target;
                    var icsbasename = basename + "_" + nextDate.ToString ("yyyyMMdd");
                    Console.WriteLine ("{0:yyyy-MM-dd} {1} {2}", nextDate, name, icsbasename);
                    ReminderICS (nextDate, name, icsbasename, currentRFP.Description, currentRFP.OwnerEmail);

                } else {
                    Console.WriteLine (nextDate.DayOfWeek);
                }
                countDown += 1;
                nextDate = nextDate.AddDays (-1);
            }
        }

        /// <summary>
        /// Generate a single calendar file
        /// </summary>
        /// <param name="DateStart">Date of event</param>
        /// <param name="title">Subject of event</param>
        /// <param name="basename">file basename</param>
        /// <param name="desc">Body of event</param>
        static void ReminderICS(DateTime DateStart, String title,  String basename, string desc, string ownerEmail) {
            //create a new stringbuilder instance
            StringBuilder sb = new StringBuilder ();

            //start the calendar item
            sb.AppendLine ("BEGIN:VCALENDAR");
            sb.AppendLine ("VERSION:2.0");
            sb.AppendLine ("METHOD:PUBLISH");
            sb.AppendLine ("BEGIN:VTIMEZONE");
            sb.AppendLine ("TZID:America/Los_Angeles");

            /* This is redundant to he TZID (it it's UTC from the original sample, not am/la as above
            sb.AppendLine ("BEGIN:STANDARD");
            sb.AppendLine ("TZOFFSETFROM:+0000");
            sb.AppendLine ("TZOFFSETTO:+0000");
            sb.AppendLine ("DTSTART:16010101T000000");
            sb.AppendLine ("END:STANDARD");
            */

            sb.AppendLine ("END:VTIMEZONE");

            sb.AppendLine ("BEGIN:VEVENT");
            sb.AppendLine ("DTSTART:" + DateStart.ToString ("yyyyMMddT000000"));
            sb.AppendLine ("DTEND:" + DateStart.ToString ("yyyyMMddT000000"));
            sb.AppendLine ("SUMMARY:" + title);
            if (desc != null) {
                sb.AppendLine ("DESCRIPTION:" + desc + "\\n\\n\\n");
            }
            sb.AppendLine ("ORGANIZER:MAILTO:" + ownerEmail);
            sb.AppendLine ("SEQUENCE:0");
            sb.AppendLine ("X-MICROSOFT-CDO-BUSYSTATUS:FREE");
            sb.AppendLine ("X-MICROSOFT-CDO-ALLDAYEVENT:TRUE");

            sb.AppendLine ("BEGIN: VALARM");
            sb.AppendLine ("ACTION:DISPLAY");
            sb.AppendLine ("DESCRIPTION:REMINDER");
            // Alert is 10 hours prior
            sb.AppendLine ("TRIGGER; RELATED = START:-PT10H00M00S");
            sb.AppendLine ("END: VALARM");

            sb.AppendLine ("END:VEVENT");
            sb.AppendLine ("END:VCALENDAR");

            var di = new DirectoryInfo (baseDirectory);
            var fullname = Path.Combine (di.FullName, basename + ".ics");
            var fi = new FileInfo (fullname);
            Console.WriteLine (fi.FullName);
            var sw = new StreamWriter (fi.FullName);
            sw.Write (sb);
            sw.Flush ();
            sw.Close ();
        }

// Pretty much ignore code below the lines. They were early
// test for generation ical files.
// They are left as references for different types of event files.

        public static void UseSample2() {
            DateTime DateStart = DateTime.Now.AddDays (1);
            MySample2 (DateStart);
        }

        static void MySample2(DateTime DateStart) {
            Console.WriteLine ("My Sample Start");


            //create a new stringbuilder instance
            StringBuilder sb = new StringBuilder ();

            //start the calendar item
            sb.AppendLine ("BEGIN:VCALENDAR");
            sb.AppendLine ("VERSION:2.0");
            sb.AppendLine ("METHOD:PUBLISH");
            sb.AppendLine ("BEGIN:VTIMEZONE");
            sb.AppendLine ("TZID:America/Los_Angeles");

            /* This is redundant to he TZID (it it's UTC from the original sample, not am/la as above
            sb.AppendLine ("BEGIN:STANDARD");
            sb.AppendLine ("TZOFFSETFROM:+0000");
            sb.AppendLine ("TZOFFSETTO:+0000");
            sb.AppendLine ("DTSTART:16010101T000000");
            sb.AppendLine ("END:STANDARD");
            */

            sb.AppendLine ("END:VTIMEZONE");

            sb.AppendLine ("BEGIN:VEVENT");
            sb.AppendLine ("DTSTART:" + DateStart.ToString ("yyyyMMddT000000"));
            sb.AppendLine ("DTEND:" + DateStart.ToString ("yyyyMMddT000000"));
            sb.AppendLine ("SUMMARY:Sample for me");
            sb.AppendLine ("ORGANIZER:MAILTO:MHahn@ciber.com");
            sb.AppendLine ("SEQUENCE:0");
            sb.AppendLine ("X-MICROSOFT-CDO-BUSYSTATUS:FREE");
            sb.AppendLine ("X-MICROSOFT-CDO-ALLDAYEVENT:TRUE");

            sb.AppendLine ("BEGIN: VALARM");
            sb.AppendLine ("ACTION:DISPLAY");
            sb.AppendLine ("DESCRIPTION:REMINDER");
            // Alert is 10 hours prior
            sb.AppendLine ("TRIGGER; RELATED = START:-PT10H00M00S");
            sb.AppendLine ("END: VALARM");

            sb.AppendLine ("END:VEVENT");
            sb.AppendLine ("END:VCALENDAR");

            var fi = new FileInfo ("/Users/markhahn/Downloads/test.ics");
            Console.WriteLine (fi.FullName);
            var sw = new StreamWriter (fi.FullName);
            sw.Write (sb);
            sw.Flush ();
            sw.Close ();

            Console.WriteLine ("My Sample End");
        }

        static void MySample() {
            Console.WriteLine ("My Sample Start");

            DateTime DateStart = DateTime.Now;

            //create a new stringbuilder instance
            StringBuilder sb = new StringBuilder ();

            //start the calendar item
            sb.AppendLine ("BEGIN:VCALENDAR");
            sb.AppendLine ("VERSION:2.0");
            sb.AppendLine ("PRODID:-//Microsoft Corporation//Outlook for Mac MIMEDIR//EN");
            sb.AppendLine ("METHOD:PUBLISH");
            sb.AppendLine ("BEGIN:VTIMEZONE");
            sb.AppendLine ("TZID:Coordinated Universal Time");
            sb.AppendLine ("X-ENTOURAGE-CFTIMEZONE:UTC");
            sb.AppendLine ("X-ENTOURAGE-TZID:93");
            sb.AppendLine ("BEGIN:STANDARD");
            sb.AppendLine ("TZOFFSETFROM:+0000");
            sb.AppendLine ("TZOFFSETTO:+0000");
            sb.AppendLine ("DTSTART:16010101T000000");
            sb.AppendLine ("END:STANDARD");
            sb.AppendLine ("END:VTIMEZONE");
            sb.AppendLine ("BEGIN:VEVENT");
            /*
            sb.AppendLine ("UID:040000008200E00074C5B7101A82E00800000000D4104E55FAF5D60100000000000000");
            sb.AppendLine (" 0010000000073EB386CB90B8449A756D0103A566AC");
            sb.AppendLine ("X-ENTOURAGE_UUID:C937F423-A978-4319-A715-7E2A27872EE6");
            sb.AppendLine ("X-MICROSOFT-EXCHANGE-ID:AAMkAGNjNTg0NjY1LTgzOTYtNDViOC04OTNlLWZkOThhNWY0YT");
            sb.AppendLine (" Q3YwBGAAAAAABfgJ6b9osjS5qnuUEw3tyKBwC4yPAZHL5LQptbJy4uzuNuAAAAAAENAAC4yPAZ");
            sb.AppendLine (" HL5LQptbJy4uzuNuAAOphSSfAAA=");
            */
            sb.AppendLine ("X-MICROSOFT-EXCHANGE-CHANGEKEY:uMjwGRy+S0KbWycuLs7jbgADrr0seQ==");
            sb.AppendLine ("DTSTAMP:20210209T231218Z");
            //sb.AppendLine ("DTSTART; TZID=\"Coordinated Universal Time\":20210210T000000");
            //sb.AppendLine (" DTEND; TZID=\"Coordinated Universal Time\":20210211T000000");
            sb.AppendLine ("DTSTART:" + DateStart.ToString ("yyyyMMddT000000"));
            sb.AppendLine ("DTEND:" + DateStart.ToString ("yyyyMMddT000000"));
            sb.AppendLine ("LAST-MODIFIED:20210209T231218Z");
            sb.AppendLine ("SUMMARY:6 Days to Azure Test");
            sb.AppendLine ("ORGANIZER:MAILTO:MHahn@ciber.com");
            sb.AppendLine ("SEQUENCE:0");
            sb.AppendLine ("X-MICROSOFT-CDO-BUSYSTATUS:FREE");
            sb.AppendLine ("X-MICROSOFT-CDO-ALLDAYEVENT:TRUE");
            sb.AppendLine ("X-MICROSOFT-DISALLOW-COUNTER:FALSE");
            sb.AppendLine ("X-MICROSOFT-DONOTFORWARDMEETING:FALSE");
            sb.AppendLine ("X-MICROSOFT-CDO-INSTTYPE:0");

            sb.AppendLine ("BEGIN: VALARM");
            sb.AppendLine ("ACTION:DISPLAY");
            sb.AppendLine ("DESCRIPTION:REMINDER");
            sb.AppendLine ("TRIGGER; RELATED = START:-PT10H00M00S");
            sb.AppendLine ("END: VALARM");

            sb.AppendLine ("END:VEVENT");
            sb.AppendLine ("END:VCALENDAR");

            StreamWriter sw = new StreamWriter ("/Users/markhahn/Downloads/test.ics");
            sw.Write (sb);
            sw.Flush ();
            sw.Close ();

            Console.WriteLine ("My Sample End");
        }

        static void StackOverflowSample() { 
            Console.WriteLine ("Hello World!");

            //some variables for demo purposes
            DateTime DateStart = DateTime.Now;
            DateTime DateEnd = DateStart.AddMinutes (105);
            string Summary = "Small summary text";
            string Location = "Event location";
            string Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.";
            //string FileName = "CalendarItem";

            //create a new stringbuilder instance
            StringBuilder sb = new StringBuilder ();

            //start the calendar item
            sb.AppendLine ("BEGIN:VCALENDAR");
            sb.AppendLine ("VERSION:2.0");
            //sb.AppendLine ("PRODID:stackoverflow.com");
            sb.AppendLine ("CALSCALE:GREGORIAN");
            sb.AppendLine ("METHOD:PUBLISH");

            //create a time zone if needed, TZID to be used in the event itself
            //sb.AppendLine ("BEGIN:VTIMEZONE");
            //sb.AppendLine ("TZID:Europe/Amsterdam");
            //sb.AppendLine ("BEGIN:STANDARD");
            //sb.AppendLine ("TZOFFSETTO:+0100");
            //sb.AppendLine ("TZOFFSETFROM:+0100");
            //sb.AppendLine ("END:STANDARD");
            //sb.AppendLine ("END:VTIMEZONE");

            //add the event
            sb.AppendLine ("BEGIN:VEVENT");

            //with time zone specified
            //sb.AppendLine ("DTSTART;TZID=Europe/Amsterdam:" + DateStart.ToString ("yyyyMMddTHHmm00"));
            //sb.AppendLine ("DTEND;TZID=Europe/Amsterdam:" + DateEnd.ToString ("yyyyMMddTHHmm00"));
            //or without
            //sb.AppendLine ("DTSTART:" + DateStart.ToString ("yyyyMMddTHHmm00"));
            //sb.AppendLine ("DTEND:" + DateEnd.ToString ("yyyyMMddTHHmm00"));
            // or all day
            sb.AppendLine ("DTSTART:" + DateStart.ToString ("yyyyMMddT000000"));
            sb.AppendLine ("DTEND:" + DateStart.ToString ("yyyyMMddT000000"));


            sb.AppendLine ("SUMMARY:" + Summary + "");
            sb.AppendLine ("LOCATION:" + Location + "");
            sb.AppendLine ("DESCRIPTION:" + Description + "");
            sb.AppendLine ("PRIORITY:3");
            sb.AppendLine ("END:VEVENT");

            //end calendar item
            sb.AppendLine ("END:VCALENDAR");

            StreamWriter sw = new StreamWriter ("/Users/markhahn/Downloads/test.ics");
            sw.Write (sb);
            sw.Flush ();
            sw.Close ();
        }
    }
}
