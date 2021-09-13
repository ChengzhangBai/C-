using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab8.Models;

namespace Lab8
{
    public partial class RegisterCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack) { 
            foreach (Course course in Helper.GetAvailableCourses())
            {
                string title = course.Code + ' ' + course.Title + " - " + course.WeeklyHours.ToString() + " hours/week";
                chkboxCourseLst.Items.Add(new ListItem(title, course.Code));
            }

                if (Session["studentList"] != null)
                {
                    foreach (Student student in (List<Student>)Session["studentList"])
                    {
                        ListItem stu = new ListItem(student.ToString(), student.Id.ToString());
                        stuLst.Items.Add(stu);
                    }
                }
            }
        }
        protected void btnRegisterCourses(object sender, EventArgs e)
        {
            Label3.Text = "";
            string sID = stuLst.SelectedValue;
            if (sID == "")
            {  
                Label3.Text = "Must select one!";
                foreach (ListItem lstItem in chkboxCourseLst.Items)
                {
                    lstItem.Selected = false;
                }
                return;
            }

            string sType = getType(sID);
            Student stu = ((List<Student>)Session["studentList"]).First(item => item.Id == int.Parse(sID));
            List<Course> selectedCourses = new List<Course>();
            int numOfCourses = 0;
            int weeklyHours = 0;
            foreach (ListItem lstItem in chkboxCourseLst.Items)
            {
                if (lstItem.Selected == true)
                {
                    numOfCourses++;
                    Course course = Helper.GetCourseByCode(lstItem.Value);
                    weeklyHours += course.WeeklyHours;

                    if(chkErr(weeklyHours, numOfCourses, out string err)) { selectedCourses.Add(course); }
                    else
                    {
                        selectedCourses.Clear();
                        return;
                    }
                    
                  }
            }
            if (numOfCourses == 0)
            {
                {
                    return;
                }
            }


            stu.RegisterCourses(selectedCourses);

            //int totalWeeklyHours = (from course in stu.RegisteredCourses select course.WeeklyHours).Sum();//Linq
            int totalWeeklyHours = stu.TotalWeeklyHours();
            Label2.Text = $"Selected student has registered {stu.RegisteredCourses.Count()} course(s), {totalWeeklyHours} hours weekly";
        }

        protected string getType(string sID)
        {
            string type = "";
            if (Session["studentList"] != null)
            {
                foreach (Student student in (List<Student>)Session["studentList"])
                {
                    if (student.Id == int.Parse(sID))
                    {
                        type = student.GetType().ToString().Split('.')[2];
                    }
                }
            }
                return type;
        }


        public void nameChanged(object sender, EventArgs e)
        {
            errMsg.Text = "";
            Label2.Text = "";
            string sID = stuLst.SelectedValue;
            if (sID == "")
            { 
                RequiredFieldValidator1.Visible = false;
                foreach (ListItem lstItem in chkboxCourseLst.Items)
                {
                    lstItem.Selected = false;
                }
                Label3.Text = "Must select one!";
                Label2.Text = "";
                return;
            }
            Label3.Text = "";

            Student stu = ((List<Student>)Session["studentList"]).First(item => item.Id == int.Parse(sID));
            //int totalWeeklyHours = (from course in stu.RegisteredCourses select course.WeeklyHours).Sum();//Linq
            int totalWeeklyHours = stu.TotalWeeklyHours();
            Label2.Text = $"Selected student has registered {stu.RegisteredCourses.Count()} course(s), {totalWeeklyHours} hours weekly";
            foreach(ListItem item in chkboxCourseLst.Items)
            {
                var result = stu.RegisteredCourses.Find(s => s.Code.Contains(item.Value));//check if registered couse list of the stu contains the checkbox value
                item.Selected = (!(result == null));//result:false->item:true
            }
            }
        public void coursesLstChg(object sender, EventArgs e)
        {
            errMsg.Text = "";
            Label2.Text = "";
            if (listSelected(out int numOfSelectedCourses, out int weeklyHours, out string err) == false)
            {
                errMsg.Text = err;
                Label2.Text = "";
                return;
            }
            else
            {
                string sID = stuLst.SelectedValue;
                if (sID == "")
                {
                    return;
                }
                Student stu = ((List<Student>)Session["studentList"]).First(item => item.Id == int.Parse(sID));
                int totalWeeklyHours = stu.TotalWeeklyHours();
            }
        }

        public bool listSelected(out int numOfSelectedCourses, out int weeklyHours, out string err)
        {
            errMsg.Text = "";
            Label2.Text = "";
            numOfSelectedCourses = 0;
            weeklyHours = 0;
            #region generate the selected course list
            foreach (ListItem lstItem in chkboxCourseLst.Items)
            {
                if (lstItem.Selected == true)
                {
                    numOfSelectedCourses++;
                    weeklyHours += Helper.GetCourseByCode(lstItem.Value).WeeklyHours;
                }
            }
            #endregion

            if (numOfSelectedCourses == 0)
            {
                err = "You must select at least one course!";
                Label2.Text = "";
                return false;
            }

            if(chkErr(weeklyHours, numOfSelectedCourses, out err)) { //if no errors found
            errMsg.Text = err = "";
            return true;
            }
            else
            {   
                return false;
            }
        }
    
        public void showErr(string msg)
        {
            Label2.Text = msg;
        }

        public bool chkErr(int weeklyHours, int numOfSelectedCourses, out string err)
        {
            string sID = stuLst.SelectedValue;
            if (sID == "")
            {
                err = "";
                return false;
            }
            Student stu = ((List<Student>)Session["studentList"]).First(item => item.Id == int.Parse(sID));
            string sType = getType(sID);
            switch (sType)
            {
                case "FullTimeStudent":
                    if (weeklyHours > FullTimeStudent.MaxWeeklyHours)
                    {
                        err = "Your selection exceeds the maximum weekly hours: "+ FullTimeStudent.MaxWeeklyHours;
                        return false;
                    }
                    err = "";
                    return true;
                case "PartTimeStudent":
                    if (numOfSelectedCourses > PartTimeStudent.MaxNumOfCourses)
                    {
                        err = "Your selection exceeds the maximum courses: "+ PartTimeStudent.MaxNumOfCourses;
                        return false;
                    }
                    err = "";
                    return true;
                case "CoopStudent":
                    if (weeklyHours > CoopStudent.MaxWeeklyHours)
                    {
                        err = "Your selection exceeds the maximum weekly hours: "+ CoopStudent.MaxWeeklyHours;
                        return false;
                    }
                    else if (numOfSelectedCourses > CoopStudent.MaxNumOfCourses)
                    {
                        err = "Your selection exceeds the maximum courses: "+ CoopStudent.MaxNumOfCourses;
                        return false;
                    }
                    else
                    {
                        err = "";
                        return true;
                    }
                default:
                    {
                        err = "";
                        return true;
                    }
            }

        }

    }
}