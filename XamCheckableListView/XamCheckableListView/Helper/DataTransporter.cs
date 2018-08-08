using System.Collections.Generic;
using XamCheckableListView;

namespace XamListView.Helper
{
    public class DataTransporter
    {
        private static DataTransporter instance = null;
        private static readonly object padlock = new object();

        private DataTransporter()
        {
        }

        public static DataTransporter Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataTransporter();
                    }
                    return instance;
                }
            }
        }

        
    }
}
