using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni
{
    [Serializable]
    public struct Student
    {
        public string StudentNumber;
        public string ID;
        public string Name;
        public string EName;
        public string Sex;
        public string Class;
        public string ClassWeb; //班級網頁
        public string SchoolID; //校部(2:小學, 3:國中, 4:高中)
        public string SchoolIDName;
        public string setLanguage;
        //public string Graduate;
    }
}