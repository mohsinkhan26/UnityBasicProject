using System;
using NUnit.Framework;
using MK.Common.Utilities;

namespace Game.Common
{
    public class DateTimeSelectionTest
    {
        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void FixDateTimeTest(bool _isDateSelected, bool _isTimeSelected)
        {
            DateTime selectedDateTime = DateTime.Now;
            DateTime selectedDate = DateTime.Now;
            DateTime selectedTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;

            Utility.FixDateTime(_isDateSelected, _isTimeSelected, ref selectedDateTime,
                ref selectedDate, ref selectedTime);

            Assert.LessOrEqual(selectedDateTime.Year, currentTime.Year, "Year is greater");
            Assert.LessOrEqual(selectedDateTime.Month, currentTime.Month, "Month is greater");
            Assert.LessOrEqual(selectedDateTime.Day, currentTime.Day, "Day is greater");

            Assert.LessOrEqual(selectedDateTime.Hour, currentTime.Hour, "Hour is greater");
            Assert.LessOrEqual(selectedDateTime.Minute, currentTime.Minute, "Minute is greater");
            Assert.LessOrEqual(selectedDateTime.Second, currentTime.Second, "Second is greater");
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void SetDateTimeTest(bool _isDateSelected, bool _isTimeSelected)
        {
            DateTime showTime = DateTime.Now.AddMinutes(-5);
            DateTime selectedDateTime = showTime;
            DateTime selectedDate = showTime;
            DateTime selectedTime = showTime;

            Utility.FixDateTime(_isDateSelected, _isTimeSelected, ref selectedDateTime,
                ref selectedDate, ref selectedTime);
            Utility.SetDateTime(showTime, _isDateSelected, _isTimeSelected, ref selectedDateTime,
                null, null, null, null);

            Assert.LessOrEqual(Utility.GetUnixTimestampMilliseconds(selectedDateTime),
                Utility.GetUnixTimestampMilliseconds(DateTime.Now),
                "Selected time is greater than current time. Time is in future");
        }

        [TestCase(true, true, 2021, 04, 16, 04, 31, 14,
            2021, 04, 16, 04, 31, 14,
            2021, 04, 16, 04, 31, 14,
            2021, 04, 16, 04, 31, 14)]
        public void SetDateTimeTest(bool _isDateSelected, bool _isTimeSelected,
            int _dateTime1Year, int _dateTime1Month, int _dateTime1Day, int _dateTime1Hour, int _dateTime1Min,
            int _dateTime1Sec,
            int _dateTime2Year, int _dateTime2Month, int _dateTime2Day, int _dateTime2Hour, int _dateTime2Min,
            int _dateTime2Sec,
            int _dateTime3Year, int _dateTime3Month, int _dateTime3Day, int _dateTime3Hour, int _dateTime3Min,
            int _dateTime3Sec,
            int _dateTime4Year, int _dateTime4Month, int _dateTime4Day, int _dateTime4Hour, int _dateTime4Min,
            int _dateTime4Sec)
        {
            DateTime showTime = new DateTime(_dateTime1Year, _dateTime1Month, _dateTime1Day,
                _dateTime1Hour, _dateTime1Min, _dateTime1Sec);
            DateTime selectedDateTime = new DateTime(_dateTime2Year, _dateTime2Month, _dateTime2Day,
                _dateTime2Hour, _dateTime2Min, _dateTime2Sec);
            DateTime selectedDate = new DateTime(_dateTime3Year, _dateTime3Month, _dateTime3Day,
                _dateTime3Hour, _dateTime3Min, _dateTime3Sec);
            DateTime selectedTime = new DateTime(_dateTime4Year, _dateTime4Month, _dateTime4Day,
                _dateTime4Hour, _dateTime4Min, _dateTime4Sec);

            Utility.FixDateTime(_isDateSelected, _isTimeSelected, ref selectedDateTime,
                ref selectedDate, ref selectedTime);
            Utility.SetDateTime(showTime, _isDateSelected, _isTimeSelected, ref selectedDateTime,
                null, null, null, null);

            Assert.LessOrEqual(Utility.GetUnixTimestampMilliseconds(selectedDateTime),
                Utility.GetUnixTimestampMilliseconds(DateTime.Now),
                "Selected time is greater than current time. Time is in future");
        }
    }
}
