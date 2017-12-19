using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PickFlowGen
{
    class Program
    {
        public static char[] sequence = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        static void Main(string[] args)
        {
            menu();
        }
        public static void menu()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.Clear();
                Console.WriteLine("Please select a generation method Below:");
                Console.WriteLine("1\tStart pick Flow --> End Pick Flow");
                Console.WriteLine("2\tStart pick Flow --> n positions");
                Console.WriteLine("3\tEnd pick Flow --> back n positions");
                Console.WriteLine("4\tEnd pick Flow --> back n positions(Reversed)");
                Console.WriteLine("5\tConvert Sequence Number --> Pick Flow Id");
                Console.WriteLine("6\tConvert Pick Flow ID --> Sequence Number");
                Console.WriteLine("0\tExit");

                string request = Console.ReadLine();
                if (Int32.TryParse(request, out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            break;
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Entering Selection {0}", choice);
                            PickFlowGenerator(GetSequence(Getindex("start")), GetSequence(Getindex("end")));
                            Console.ReadLine();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Entering Selection {0}", choice);
                            StartToNth();
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("Entering Selection {0}", choice);
                            StartPreviousNth();
                            Console.ReadLine();
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Entering Selection {0}", choice);
                            RevStartPreviousNth();
                            Console.ReadLine();
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("Entering Selection {0}", choice);
                            string tempstr = ConvertSeqToPFIndex(GetNth("Enter a pick sequence to return its Pick Flow ID"));
                            Console.WriteLine("Pick Flow ID: {0}",tempstr);
                            Console.ReadLine();
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("Entering Selection {0}", choice);
                            int temp = GetSequence(Getindex("Pick Flow"));
                            Console.WriteLine("Pick Flow Sequence: {0}", temp.ToString());
                            Console.ReadLine();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Issue with your choice, please enter the number of your selection.");
                    choice = -1;
                }
            }
        }
        public static void StartToNth()
        {
            int startRequest = GetSequence(Getindex("Start"));
            int endRequest = startRequest + GetNth("Enter number of items to process through: ");

            PickFlowGenerator(startRequest, endRequest);
        }
        public static void RevStartPreviousNth()
        {
            char first = '0';
            char second = '0';
            char third = '0';
            int total = 0;
            Console.WriteLine("Please enter start Flow Number: ");
            string startloc = Console.ReadLine();
            char startfirst = ' ';
            char startSecond = ' ';
            char startThird = ' ';

            char[] startchars = startloc.ToCharArray();

            startfirst = startchars[0];
            startSecond = startchars[1];
            startThird = startchars[2];


            int firstposition = Array.IndexOf(sequence, startfirst);
            int secondposition = Array.IndexOf(sequence, startSecond);
            int thirdposition = Array.IndexOf(sequence, startThird);

            int startFlag = 0;

            int startRequest = 0;
            int endRequest = 0;

            startRequest = Math.Abs((62 * 62 * 62) - ((firstposition * 62 * 62) + (secondposition * 62) + thirdposition));
            Console.WriteLine("Enter how many elements to cycle back through:");
            string strgnumbItems = Console.ReadLine();

            int numberOfItems = Int32.Parse(strgnumbItems);
            endRequest = startRequest + numberOfItems;



            using (StreamWriter output = new StreamWriter(path + GetFileName()))
            {
                for (int i = sequence.Length; i-- > 0;)
                {
                    first = sequence[i];
                    for (int y = sequence.Length; y-- > 0;)
                    {
                        second = sequence[y];
                        for (int x = sequence.Length; x-- > 0;)
                        {
                            third = sequence[x];
                            startFlag++;
                            if (startFlag >= startRequest && startFlag <= endRequest)
                            {
                                Console.WriteLine(first.ToString() + second.ToString() + third.ToString());
                                output.WriteLine(first.ToString() + second.ToString() + third.ToString());
                                total++;
                            }
                        }
                    }
                }
                Console.WriteLine("Total results: {0}", total);
                Console.WriteLine("Start Index: {0} End Index: {1}", startRequest, endRequest);
                Console.WriteLine("Total elements between start and end: {0}", endRequest - startRequest);
                Console.Read();
            }

        }
        public static void StartPreviousNth()
        {
            int startRequest = GetSequence(Getindex("Start"));
            int endRequest = startRequest - GetNth("Enter number of items to process through: ");
            PickFlowGenerator(endRequest, startRequest);
        }
        public static void PickFlowGenerator(int start, int end)
        {
            char first = ' ';
            char second = ' ';
            char third = ' ';

            int startFlag = 0;
            int total = 0;

            

            using (StreamWriter output = new StreamWriter(path + GetFileName()))
            {
                for (int i = 0; i < 62; i++)
                {
                    first = sequence[i];
                    for (int y = 0; y < 62; y++)
                    {
                        second = sequence[y];
                        for (int x = 0; x < 62; x++)
                        {
                            third = sequence[x];
                            startFlag++;
                            if (startFlag >= start && startFlag <= end)
                            {
                                Console.WriteLine(first.ToString() + second.ToString() + third.ToString());
                                output.WriteLine(first.ToString() + second.ToString() + third.ToString());
                                total++;
                            }
                        }
                    }
                }
                Console.WriteLine("Total results: {0}", total);
                Console.WriteLine("Start Index: {0} End Index: {1}", start, end);
                Console.WriteLine("Total elements between start and end: {0}", Math.Abs(end - start)+1);
                Console.Read();
            }
        }
        public static char[] Getindex(string CustomStrg)
        {
            Console.WriteLine("Please enter {0} index: ", CustomStrg);
            char[] indexarray = Console.ReadLine().ToCharArray();
            while(indexarray.Length != 3)
            {
                Console.WriteLine("Issue with string Entered please reenter:");
                indexarray = Console.ReadLine().ToCharArray();
            }

            return indexarray;
        }
        public static int GetNth(string CustomString)
        {
            int returnvalue = 0;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(CustomString);
                if (Int32.TryParse(Console.ReadLine(), out int n))
                {
                    exit = true;
                    returnvalue = n;
                }
                else
                {
                    Console.WriteLine("Issue converting information: Please use only numbers");
                }
            }
            return returnvalue;
        }
        public static int GetSequence(char[] array)
        {
            return Convert.ToInt32((Array.IndexOf(sequence, array[0]) * Math.Pow(sequence.Length, 2.0)) + (Array.IndexOf(sequence, array[1]) * sequence.Length) + Array.IndexOf(sequence, array[2]) + 1);
        }
        public static string ConvertSeqToPFIndex(int seqNumber)
        {
            int pos1Value = Convert.ToInt32(seqNumber / Math.Pow(sequence.Length, 2));
            int pos1Remainder = Convert.ToInt32(seqNumber % Math.Pow(sequence.Length, 2));
            int pos2Value = pos1Remainder / sequence.Length;
            int pos3 = pos1Remainder % sequence.Length;
            string PFIndex = sequence[pos1Value].ToString()+sequence[pos2Value].ToString()+sequence[pos3].ToString();
            return PFIndex;
        }
        public static string GetFileName()
        {
            Console.WriteLine("Please enter the name of the file you wish to create:");
            string filename = Console.ReadLine();
            filename = "\\" + filename + ".txt";
            return filename;
        }
    }
}
