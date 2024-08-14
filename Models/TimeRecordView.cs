using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models
{
    /// <summary>
    /// Handles Time Record data from the Time Record view
    /// </summary>
    public class TimeRecordView : TimeRecord
    {
        private int _count;
        private string _title;

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public TimeRecordView() { }

        public TimeRecordView(int count, string title)
        {
            _count = count;
            _title = title;
        }
    }
}
