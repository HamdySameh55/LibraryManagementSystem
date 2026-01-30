using System;

namespace LibraryManagementSystem.Utils
{
    public static class DateHelper
    {
        public static int DaysBetween(DateTime from, DateTime to)
        {
            return (to.Date - from.Date).Days;
        }

        public static bool IsOverdue(DateTime dueDate)
        {
            return DateTime.Now.Date > dueDate.Date;
        }

        public static int CalculateOverdueDays(DateTime dueDate, DateTime? returnDate)
        {
            DateTime endDate = returnDate ?? DateTime.Now;

            int days = (endDate.Date - dueDate.Date).Days;

            return days > 0 ? days : 0;
        }
    }
}
