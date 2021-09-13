using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab8.Models;

namespace Lab7
{
    public partial class AddStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

         if (!IsPostBack)
            {
                showCell();
            }
        }
        public void nullRec()
        {
            genCell(0, "<div class=\"error\">No student yet</div>","");
        }
        public bool chkName(string x) =>x=="";  
        static List<Student> stuLst = new List<Student>();
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            StudentName.BackColor = Color.White;
            if (chkName(StudentName.Text.Trim())) {
                errMsg.Text = "Student name cannot be empty!";
                StudentName.BackColor = Color.Yellow;
                StudentName.Focus();
            }
            else {
                errMsg.Text = "";
            int sID = GenRandomID(); 
            string sName = StudentName.Text.Trim();
            string sType = StudentType.SelectedValue;
            int? maxWeeklyHours = null;
            int? maxNumOfCourses = null;
                switch (sType)
                {
                    case "ft":
                        FullTimeStudent fStu = new FullTimeStudent(sID, sName, maxWeeklyHours);
                        stuLst.Add(fStu);
                        
                        break;
                    case "pt":
                        PartTimeStudent pStu = new PartTimeStudent(sID, sName, maxNumOfCourses);
                        stuLst.Add(pStu);
                        break;
                    case "co":
                        CoopStudent cStu = new CoopStudent(sID, sName, maxWeeklyHours, maxNumOfCourses);
                        stuLst.Add(cStu);
                      
                        break;
                    default:
                        errMsg.Text = "Please select a type!";
                        showCell();
                        StudentType.BackColor = Color.Yellow;
                        return;
                }

            
            Session.Add("studentList", stuLst);
            }
            if (Session["studentList"] == null)
            {
                nullRec();
                return;
            }

            showCell();
            StudentName.Text = "";
            StudentType.SelectedValue = "";
            StudentName.BackColor = StudentType.BackColor = Color.White;
            StudentName.Focus();
         }
        
        public void nameChanged(object sender, EventArgs e)
        {
            if (chkName(StudentName.Text.Trim()))
            {
                errMsg.Text = "Student name cannot be empty!";
                StudentName.BackColor = Color.Yellow;
                StudentName.Focus();
            }
            else
            {
                errMsg.Text = "";
            }
            showCell();

        }
        public void showCell()
        {
            if (Session["studentList"] == null)
            {
                genCell(0, "<div class=\"error\">No student yet</div>","");
                return;
            }
    
            foreach (Student stud in (List<Student>)Session["studentList"])
            {

                string type="";
                if (stud.GetType().ToString().Contains("FullTimeStudent"))
                {
                    type = "Full Time";
                }
                if (stud.GetType().ToString().Contains("PartTimeStudent"))
                {
                    type = "Part Time";
                }
                if (stud.GetType().ToString().Contains("CoopStudent"))
                {
                    type = "Coop";
                }

                genCell(stud.Id,stud.Name,type);
             }
         }
        public void genCell(int id, string name,string type)
        {
            TableRow rowItem = new TableRow();
            tblStudents.Rows.Add(rowItem);
            rowItem.Style.Add("text-align", "center");
            TableCell cellItem = new TableCell();
            rowItem.Cells.Add(cellItem);
            cellItem.Text = id == 0 ? "" : id.ToString();
            TableCell cellItemtwo = new TableCell();
            rowItem.Cells.Add(cellItemtwo);
            cellItemtwo.Text = name;
            TableCell cellItemThree = new TableCell();
            rowItem.Cells.Add(cellItemThree);
            cellItemThree.Text = type;
        }

        public int GenRandomID()
        {
            Random rd = new Random();
            int randomNum = rd.Next(100000,1000000);
            return randomNum;
        }
    }
}