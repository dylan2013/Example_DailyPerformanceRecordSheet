using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.Customization.Data;
namespace 德行成績試算表
{
    class SelectStudentData
    {
        public int maxStudents = 0;
        public int totalStudent = 0;
        public int currentStudent = 1;

        public List<ClassRecord> allClasses = new List<ClassRecord>();
        public List<StudentRecord> allStudents = new List<StudentRecord>();
        public Dictionary<string, List<StudentRecord>> classStudents = new Dictionary<string, List<StudentRecord>>();
        public List<string> StudentIDList = new List<string>();

        /// <summary>
        /// 學生ID , 學生德行成績
        /// </summary>
        public Dictionary<string, SHSchool.Data.SHMoralScoreRecord> MoralScoerDic = new Dictionary<string, SHSchool.Data.SHMoralScoreRecord>();

        /// <summary>
        /// 學生ID , AutoSummary
        /// </summary>
        //public Dictionary<string, K12.BusinessLogic.Behavior> AutoSummaryDic = new Dictionary<string, SHSchool.Data.SHMoralScoreRecord>();

        public SelectStudentData(int SchoolYear, int Semester)
        {
            //取得班級資料
            AccessHelper dataSeed = new AccessHelper();
            allClasses = dataSeed.ClassHelper.GetSelectedClass();

            foreach (ClassRecord aClass in allClasses)
            {
                List<StudentRecord> studnetList = aClass.Students; //取得班級學生

                if (studnetList.Count > maxStudents)
                    maxStudents = studnetList.Count;
                allStudents.AddRange(studnetList);

                classStudents.Add(aClass.ClassID, studnetList);
                totalStudent += studnetList.Count;
            }

            //學生ID , 學生德行成績
            foreach (SHSchool.Data.SHMoralScoreRecord each in SHSchool.Data.SHMoralScore.Select(null, StudentIDList, SchoolYear, Semester))
            {
                if (!MoralScoerDic.ContainsKey(each.RefStudentID))
                {
                    MoralScoerDic.Add(each.RefStudentID, each);
                }
            }

        }
    }
}
