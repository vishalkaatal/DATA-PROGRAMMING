using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Group5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ADMINpasswords = new List<string>() { "PASSWORD" };
            int Studentexists = 0;
            int CorrectADMINpassword = 0;

            while (true)
            {
                Console.WriteLine("\nWELCOME TO THE STUDENT INFORMATION DATABASE");
                Console.WriteLine("PLEASE SELECT A NUMBER FROM BELOW");
                Console.WriteLine("1: ADMIN - MAY VIEW ALL STUDENT INFORMATION, ADD STUDENT INFORMATION, AND DELETE STUDENT INFORMATION");
                Console.WriteLine("2: STUDENT - MAY ONLY VIEW STUDENT'S PERSONAL INFORMATION ");
                Console.WriteLine("3: EXIT - TO EXIT THE PROGRAM\n");
                Console.Write("CHOSEN NUMBER: ");
                int Choice = int.Parse(Console.ReadLine());
                switch (Choice)
                {
                    case 1:
                        Console.WriteLine("\nUSERNAME: ADMIN");
                        Console.WriteLine("PASSWORD(all upper case): ");
                        var pass = Console.ReadLine();
                        var PASS = pass.ToUpper();
                        foreach (var ADMINpassword in ADMINpasswords)
                        {
                            if (PASS == ADMINpassword)
                            {
                                ADMIN();
                                CorrectADMINpassword = 1;
                            }
                            if (CorrectADMINpassword == 0 || PASS != ADMINpassword)
                            {
                                Console.WriteLine("INCORRECT PASSWORD");
                            }
                        }
                        break;

                    case 2:
                        int incorrectpassword = 0;
                        Studentexists = 0;
                        Console.WriteLine("\nUSERNAME(STUDENT ID#): ");
                        var studentID = Console.ReadLine();
                        string filePath1 = @"E:\BDAT - Georgian\BDAT1004 - Data Programming\Group Assignment 2\Project\Group5\StudentDatabase.json";
                        if (File.Exists(filePath1))
                        {
                            string content1 = File.ReadAllText(filePath1);
                            List<Student> students1 = JsonSerializer.Deserialize<List<Student>>(content1);
                            foreach (var student1 in students1)
                            {
                                if (student1.ID == studentID)
                                {
                                    Console.WriteLine("PASSWORD: ");
                                    var password = Console.ReadLine();
                                    if (student1.Password == password)
                                    {
                                        Console.WriteLine($"\nStudent Full Name: {student1.FullName}\nStudent ID#: {student1.ID}\nDate of Birth(dd/mm/yy): {student1.DOB}\nGender: {student1.Gender}\nPhone Number: {student1.Phone}\nAddress : {student1.Address}\nStatus: {student1.Status}\nPassword: {student1.Password}");
                                        Studentexists = 1;
                                    }
                                    else
                                    {
                                        incorrectpassword = 1;
                                        Console.WriteLine("INCORRECT PASSWORD");
                                    }
                                }
                            }
                            if (Studentexists == 0 && incorrectpassword == 0)
                            {
                                Console.WriteLine("STUDENT DOES NOT EXIST");
                            }
                        }
                        else
                        {
                            Console.WriteLine("THERE ARE NO STUDENTS IN THE DATABASE, CONTACT ADMINISTRATOR");
                        }
                        break;

                    case 3:
                        return;
                }
            }
        }

        public static void ADMIN()
        {
            Console.WriteLine("\nWELCOME ADMINISTRATOR");
            Console.WriteLine("PLEASE SELECT A NUMBER FROM BELOW");
            Console.WriteLine("1: VIEW STUDENT INFORMATION     - SELECT A STUDENT FROM LIST OF ALL STUDENTS TO VIEW THEIR INFORMATION");
            Console.WriteLine("2: ADD STUDENT INFORMATION      - ADD STUDENT INFORMATION TO THE DATABASE ");
            Console.WriteLine("3: DELETE STUDENT FROM DATABASE - DELETE STUDENT FROM THE DATABASE");
            Console.WriteLine("4: EXIT                         - RETURN TO THE PREVIOUS SCREEN\n");
            Console.Write("CHOSEN NUMBER: ");
            int choice = int.Parse(Console.ReadLine());
            int studentexists = 0;
            string filePath2 = @"E:\BDAT - Georgian\BDAT1004 - Data Programming\Group Assignment 2\Project\Group5\StudentDatabase.json";
            switch (choice)
            {
                case 1:
                    if (File.Exists(filePath2))
                    {
                        string content2 = File.ReadAllText(filePath2);
                        List<Student> students2 = JsonSerializer.Deserialize<List<Student>>(content2);
                        if (students2.Count == 0)
                        {
                            Console.WriteLine("THERE ARE NO STUDENTS, PLEASE ADD STUDENT INFORMATION FIRST");
                        }
                        else
                        {
                            foreach (var student2 in students2)
                            {
                                Console.WriteLine($"Student Full Name & ID#: {student2.FullName} & {student2.ID}");
                            }
                            Console.WriteLine("PLEASE CHOOSE A STUDENT FROM THE LIST AND TYPE IN THEIR ID #: ");
                            var chosenstudent = Console.ReadLine();
                            foreach (var student2 in students2)
                            {
                                if (chosenstudent == student2.ID)
                                {
                                    Console.WriteLine($"\nStudent Full Name: {student2.FullName}\nStudent ID#: {student2.ID}\nDate of Birth(DD/MM/YY): {student2.DOB}\nGender: {student2.Gender}\nPhone Number: {student2.Phone}\nAddress : {student2.Address}\nStatus: {student2.Status}\nPassword: {student2.Password}");
                                    studentexists = 1;
                                }
                            }
                            if (studentexists == 0)
                            {
                                Console.WriteLine("NO SUCH STUDENT EXISTS OR YOU ENTERED THE INCORRECT ID NUMBER");
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("THERE ARE NO STUDENTS, PLEASE ADD STUDENT INFORMATION FIRST");
                    }
                    break;

                case 2:
                    Console.WriteLine("PLEASE ENTER THE FOLLOWING STUDENT INFORMATION:");
                    Console.Write("FULLNAME: ");
                    var fullname = Console.ReadLine();
                    var FULLNAME = fullname.ToUpper();
                    Console.Write("STUDENT ID: ");
                    var ID = Console.ReadLine();
                    Console.Write("DATE OF BIRTH(DD/MM/YY): ");
                    var DOB = Console.ReadLine();
                    Console.Write("GENDER(F/M/X): ");
                    var GENDER = Console.ReadLine();
                    Console.Write("PHONE NUMBER(XXXXXXXXXX): ");
                    var PHONE = Console.ReadLine();
                    Console.Write("ADDRESS(# STREET, CITY, PORVINCE, POSTAL CODE): ");
                    var ADDRESS = Console.ReadLine();
                    Console.Write("STATUS(ENROLLED/REGISTERED): ");
                    var STATUS = Console.ReadLine();
                    Console.Write("STUDENT PASSWORD: ");
                    var PASSWORD = Console.ReadLine();
                    if (File.Exists(filePath2))
                    {
                        string content2 = File.ReadAllText(filePath2);
                        List<Student> students3 = JsonSerializer.Deserialize<List<Student>>(content2);
                        File.Delete(filePath2);
                        students3.Add(new Student { FullName = FULLNAME, ID = ID, DOB = DOB, Gender = GENDER, Phone = PHONE, Address = ADDRESS, Status = STATUS, Password = PASSWORD });
                        string jsonString = JsonSerializer.Serialize(students3);
                        using (StreamWriter sw = new StreamWriter(filePath2, true))
                        {
                            sw.WriteLine(jsonString);
                        }
                    }
                    else
                    {
                        List<Student> students3 = new List<Student>();
                        students3.Add(new Student { FullName = FULLNAME, ID = ID, DOB = DOB, Gender = GENDER, Phone = PHONE, Address = ADDRESS, Status = STATUS, Password = PASSWORD });
                        string jsonString = JsonSerializer.Serialize(students3);
                        using (StreamWriter sw = new StreamWriter(filePath2, true))
                        {
                            sw.WriteLine(jsonString);
                        }
                    }
                    break;

                case 3:
                    int STUDENTEXISTS = 0;
                    int studentindex = 0;
                    int index = 0;
                    Console.WriteLine("PLEASE ENTER THE ID NUMBER OF THE STUDENT YOU WISH TO DELETE:");
                    Console.Write("ID #: ");
                    var ID1 = Console.ReadLine();
                    if (File.Exists(filePath2))
                    {
                        string content2 = File.ReadAllText(filePath2);
                        List<Student> students3 = JsonSerializer.Deserialize<List<Student>>(content2);
                        File.Delete(filePath2);
                        foreach (var student3 in students3)
                        {
                            if (ID1 == student3.ID)
                            {
                                studentindex = index;
                                STUDENTEXISTS = 1;
                            }
                            index += 1;
                        }
                        if (STUDENTEXISTS == 1)
                        {
                            students3.RemoveAt(studentindex);
                            Console.WriteLine("THE STUDENT HAS BEEN REMOVED FROM THE DATABASE");
                        }
                        else if (STUDENTEXISTS == 0)
                        {
                            Console.WriteLine("THERE IS NO SUCH STUDENT IN THE DATABASE");
                        }
                        string jsonString = JsonSerializer.Serialize(students3);
                        using (StreamWriter sw = new StreamWriter(filePath2, true))
                        {
                            sw.WriteLine(jsonString);
                        }
                    }
                    else
                    {
                        Console.WriteLine("THERE ARE NO STUDENTS IN THE DATABASE");
                    }
                    break;

                case 4:
                    return;
            }
        }
    }
}