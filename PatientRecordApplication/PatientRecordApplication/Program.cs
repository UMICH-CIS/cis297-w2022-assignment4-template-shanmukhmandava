/// NAME: SHANMUKH GOPINATH MANDAVA
/// CIS 297 - C#
/// UMID: 13136658

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PatientRecordApplication
{
    /// <summary>
    /// Patient Class takes the input from the user and stores the patient details in  the patient text file
    /// </summary>
    class PatientClass
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public double patientBalance { get; set; }

        public PatientClass(int pId, string pName, double pBalance )
        {
            patientId = pId;
            patientName = pName;
            patientBalance = pBalance;

        } // end of Patient Class Constructor

        public PatientClass()
        {
        } // empty cosntructor

        public void WritePatientRecords()
        {

            using (StreamWriter w = File.AppendText("Patients.txt"))
            {
                w.WriteLine("ID : " + patientId + ", Name : " + patientName + ", Balance " + patientBalance, w);
            }

            try
            {
                using (StreamReader sr = File.OpenText("Patients.txt"))
                {
                    Console.WriteLine($"The first line of this file is {sr.ReadLine()}");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }

        } // end of Writing Patient Records


    } // end of PatientClass

    /// <summary>
    /// Here class dispaly records displays all the records in the patient.txt file
    /// </summary>
    class DisplayRecords
    {
        public int counter = 0;
        public int flag = 0;
        public string line;
        public void readFile()
        {
            System.IO.StreamReader doc = new System.IO.StreamReader(@"Patients.txt");
            while ((line = doc.ReadLine()) != null)//loop for fetching line
            {
                string[] st = line.Split(',');//split line for get patient_id,Patientname,balance
                Console.WriteLine("Patient ID :{0}\nPatient Name :{1}\nBalance:{2}", st[0], st[1], st[2]);
                counter++;
            }
            doc.Close();
        } // end of readFile method

    }// end of display records

    /// <summary>
    /// Search records takes the patient id input from user and searches for associated data in patient.txt
    /// </summary>
    class SearchRecords
    {
        public int counter = 0;
        public int flag = 0;
        public string line;
        public void searchFile(string patId)
        {
            System.IO.StreamReader doc = new System.IO.StreamReader(@"Patients.txt");
            while ((line = doc.ReadLine()) != null)//loop for fetching line
            {
                string[] st = line.Split(',');//split line for get patient_id,Patientname,balance
                if (st[0].Equals(patId) == true) //if it is found
                {
                    Console.WriteLine("Patient ID :{0}\nPatient Name :{1}\nBalance:{2}", patId, st[1], st[2]);
                    flag = 1;
                    break;
                } // end of if
                counter++;

                if (flag == 0)
                { // if patient is not found
                    Console.WriteLine("Record not Found");
                }
            }
            doc.Close();
        } // end of readFile method


    } // end of Search Records

    /// <summary>
    /// here the class traverse through the patient records data to find all the relevent data for the balance
    /// </summary>
    class recordsByBalance
    {

        public void searchBalFile(double roundedBal)
        {

            FileStream balDocfile = new FileStream("/Patients.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(balDocfile);
            string records;
            string[] fields;
            Double rBal = roundedBal;
            PatientClass p = new PatientClass();

            while (roundedBal!=999) // 999 is the keyword to end so basically end it when user wants to end
            {
                Console.WriteLine("{0,-5}{1,12}{2,8}\n", "Patient Id: ", "Patient Name: ", "Balance: ");
                balDocfile.Seek(0, SeekOrigin.Begin);
                records = read.ReadLine();
                while (records != null)
                {
                    fields = records.Split(',');

                    p.patientId = Convert.ToInt32(fields[0]);
                    p.patientName = fields[1];
                    p.patientBalance = Convert.ToDouble(fields[2]);

                    if (Convert.ToDouble(fields[2]) >= rBal )
                    {
                        Console.WriteLine("{0,-5}{1,12}{2,8}\n", p.patientId, p.patientName.ToString()) ;
                        p.patientBalance.ToString("C");
                        records = read.ReadLine();
                        Console.ReadLine();

                    }


                } // end of inner while

                read.Close();
                balDocfile.Close();


            } // end of outer while

        }


    } // end of records by balance
    /// <summary>
    /// Main function to crate objects and to call classes
    /// </summary>
    class Patient
    {
        public static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            int id;
            string name;
            double balance;
            string op;
            string patId;
            double bal;
            Console.WriteLine("Welcome to The Pateint Register");
            Console.WriteLine("Please Choose a Below Function");
            Console.WriteLine("A: Register New Patient ");
            Console.WriteLine("B: Display All Patient Records");
            Console.WriteLine("C: Search # Patient Reacord");
            Console.WriteLine("D: Display Patients Balance Records");
            Console.WriteLine("E: Exit");

            ///*
            try
            {
                op = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Error occurred.");
            }
            finally
            {
                Console.WriteLine("Re-try with a valid input.");
                op = Console.ReadLine();
            }
            // */
            //   op = Console.ReadLine();
            switch (op)
            {
                case "A":
                    Console.Write("Please Enter Patient Id : ");
                    id = int.Parse(Console.ReadLine());
                    Console.Write("Please Enter Patient Name : ");
                    name = Console.ReadLine();
                    Console.Write("Please Enter Patient Balance : ");
                    balance = double.Parse(Console.ReadLine());
                    PatientClass p = new PatientClass(id, name, balance);
                    p.WritePatientRecords();
                    Console.WriteLine("Record has been added to file.");
                    Console.ReadKey();
                    break;
                case "B":
                    DisplayRecords file = new DisplayRecords();
                    file.readFile();
                    break;
                case "C":
                    Console.Write("Please Enter Patient Id : ");
                    patId = Console.ReadLine();
                    SearchRecords docS = new SearchRecords();
                    docS.searchFile(patId);
                    break;
                case "D":
                    Console.Write("Please Enter Patient Balance : ");
                    bal = Convert.ToDouble(Console.ReadLine());
                    recordsByBalance balS = new recordsByBalance();
                    balS.searchBalFile(bal);
                    break;
                case "E":
                    Console.WriteLine("Not a valid Entry");
                    // Environment.Exit(0);
                    break;
                //default:
                    //Console.WriteLine("Not a valid Entry");
                   // break;


            } // end of switch

        } // end of Main

    }  // end of class Patient

} // end of namespace
