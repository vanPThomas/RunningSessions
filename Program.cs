namespace RunningSessions{
    internal class Program{
        static void Main(string[] args){
            MenuScreen();
        }
        static void MenuScreen()
        {
            bool validInput = false;
            while(validInput == false)
            {
                Console.WriteLine("Welcome, please make a choice:");
                Console.WriteLine("1. Get the sessions of a specific date");
                Console.WriteLine("2. Get the sessions of a specific client");
                Console.Write("\nMake a choice: ");
                int choice;
                validInput = int.TryParse(Console.ReadLine(), out choice);

                if(choice == 1)
                    GetDateInfo();
                else if(choice == 2)
                    GetClientInfo();
                else
                    Console.WriteLine("Not a valid input!");

                if(validInput == false)
                    Console.Clear();
            }
        }
        static void GetDateInfo()
        {
            Console.Clear();
            List<string[]> datalist = CreateList();
            Console.WriteLine("DATE");
        }
        static void GetClientInfo()
        {
            Console.Clear();
            List<string[]> dataList = CreateList();
            //List<string[]> clientSessions = new List<string[]>();
            Dictionary<int , List<string[]>> sessionsToIntervals  = new Dictionary<int, List<string[]>>();

            Console.Write("Please enter a client number: ");
            int clientNumber = int.Parse(Console.ReadLine());

            foreach (string[] session in dataList){
                if (int.Parse(session[2]) == clientNumber){
                    if (sessionsToIntervals.ContainsKey(int.Parse(session[0]))){
                        sessionsToIntervals[int.Parse(session[0])].Add(session);
                    }
                    else
                    {
                        List<string[]> tempList = new List<string[]>(){session};
                        sessionsToIntervals.Add(int.Parse(session[0]), tempList);
                    }
                    //clientSessions.Add(session);
                }
            }
            foreach (KeyValuePair<int, List<string[]>> entry in sessionsToIntervals)
            {
                for(int i = 0; i < entry.Value.Count; i++)
                {
                    Console.WriteLine($"Session: {entry.Value[i][0]}");
                    for(int j = 0; j < entry.Value[i].Length; j++)
                    {
                        Console.Write($"{entry.Value[i][j]}");
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }

            /*foreach(string[] session in clientSessions)
            {
                Console.WriteLine("Date: " + session[1].Trim('\''));
                Console.WriteLine($"Session number: {session[0]}");
                Console.WriteLine($"Training time in minutes: {session[3]}");
                Console.WriteLine($"Average speed: {session[4]}");
                Console.WriteLine($"Sequence number of running interval: {session[5]}");
                Console.WriteLine($"Running interval duration in seconds: {session[6]}");
                Console.WriteLine($"Running speed of training interval: {session[7]}");
                Console.WriteLine();
            }*/
        }
        static List<string[]> CreateList()
        {
            List<string[]> dataList = new List<string[]>();
            using (StreamReader sr = File.OpenText(@"data\insertRunningTest.txt"))
            {
                string input = null;
                while ((input = sr.ReadLine()) != null)
                {
                    int start = input.IndexOf('(') + 1;
                    int end = input.IndexOf(')');
                    string data = input.Substring(start, end - start);
                    string[] session = data.Split(',');
                    dataList.Add(session);
                }
            }
            return dataList;
        }
    }
}