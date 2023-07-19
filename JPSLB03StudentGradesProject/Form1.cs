using StudentLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace JPSLB03StudentGradesProject
{
    public partial class Form1 : Form
    {
        //  Global List Variable
        private List<Student> students;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  Set up file path and file name
            string dir  = @"C:\C#\Files\";
            string path = dir + "students.txt";

            //  Read the students.txt file
            //  into a list of Student objects.
            students = ReadStudentFile(path);

            //  Display the list contents
            DisplayStudents();
        }

        private List<Student> ReadStudentFile(string fileName)
        {
            List<Student> students = new List<Student>();

            try
            {
                // Read the lines (records) from the file
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    //  Split the line by commas to
                    //  get the Student class properties
                    string[] parts = line.Split(',');

                    if (parts.Length >= 3)
                    {
                        //  Create a new Student object
                        //  and assign its properties.
                        Student student = new Student
                        {
                            Name        = parts[0],
                            LabScore    = decimal.Parse(parts[1]),
                            TestScore   = decimal.Parse(parts[2])
                        };
 
                        //  Add the student to the list of students
                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message,
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return students;
        }

        private void DisplayStudents()
        {
            decimal overallGrade = 0M;

            //  Loop through the students list
            //  and for each student, print out:
            //
            //  1.  Student name
            //  2.  Student lab  score
            //  3.  Student test score
            //  4.  Calculated overall grade
            foreach(Student student in students)
            {
                //  Calculate the overall grade
                //  for each individual student by the formula:
                //  overAllGrade = (labScore * .4) + (testScore * .6)
                overallGrade = ((student.LabScore  * 0.4M) +
                                (student.TestScore * 0.6M));
                lstStudentContents.Items.Add(
                  $"Name: {student.Name}\tLab Score: {student.LabScore}\tTest Score: {student.TestScore}\tOverall Grade: {overallGrade.ToString("n2")}\r\n");
            }
        }
    }
}
