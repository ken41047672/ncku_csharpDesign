namespace P76144176_OOP_3
{
    internal class Program
    {
        public enum MachineType{ CNC, RoboticArm,  Conveyor}
        public enum MachineStatus{ Running, Idle, Error}

        public class Machine
        {
            public Machine(string id, MachineType type, MachineStatus status, List<int> errorCodes)
            {
                Id = id;
                Type = type;
                Status = status;
                ErrorCodes = errorCodes;
            }

            public string Id { get; set; }
            public MachineType Type { get; set; }
            public MachineStatus Status { get; set; }
            public List<int> ErrorCodes { get; set; } = new List<int>();
        }

        public class FactoryFloor
        {
            public string FloorName { get; set; }
            private List<Machine> machines = new List<Machine>();

            public FactoryFloor()
            {
            }

            public void AddMachine(Machine machine)
            {
                machines.Add(machine);
            }

            public List<Machine> GetMachinesByStatus(MachineStatus status)
            {
                List<Machine> result = new List<Machine>();
                foreach (Machine machine in machines) {
                    if (machine.Status == status)
                    {
                        result.Add(machine);
                    }
                }
                return result;
            }

            public Machine FindMachineWithMostErrors()
            {
                return machines.OrderByDescending(m => m.ErrorCodes.Count).FirstOrDefault();
            }

            
        }
        static void Main(string[] args)
        {
            FactoryFloor f = new FactoryFloor();
            f.AddMachine(new Machine("A01", MachineType.CNC, MachineStatus.Running, new List<int>()));
            f.AddMachine(new Machine("B02", MachineType.RoboticArm, MachineStatus.Running, new List<int>()));
            f.AddMachine(new Machine("A03", MachineType.RoboticArm, MachineStatus.Error, new List<int> { 1, 2, 3, 4, 5 }));
            while (true)
            {
                Console.WriteLine("--- 機台監控儀表板 ---");
                Console.WriteLine("[1] 依狀態篩選機台");
                Console.WriteLine("[2] 尋找錯誤最多的機台");
                Console.WriteLine("[3] 離開");
                Console.Write("> ");
                int op = int.Parse(Console.ReadLine());
                if (op == 1)
                {
                    Console.Write("請輸入要查詢的狀態 (Running, Idle, Error): ");
                    string type = Console.ReadLine();
                    Console.WriteLine();
                    List<Machine> result = null;
                    if(type == "Running")
                    {
                        Console.WriteLine("--- 狀態為 [Running] 的機台: ---");
                        result = f.GetMachinesByStatus(MachineStatus.Running);
       
                    }
                    else if(type == "Idle")
                    {

                        Console.WriteLine("--- 狀態為 [Idle] 的機台: ---");
                        result = f.GetMachinesByStatus(MachineStatus.Idle);
                    }
                    else if(type == "Error")
                    {
                        Console.WriteLine("--- 狀態為 [Error] 的機台: ---");

                        result = f.GetMachinesByStatus(MachineStatus.Error);
                    }

                    foreach(Machine m in result)
                    {
                        Console.WriteLine($"- 機台ID: {m.Id}, 類型: {m.Type}, 錯誤數: {m.ErrorCodes.Count}");
                    }
                    Console.WriteLine();
                }
                else if (op == 2)
                {
                    Console.WriteLine();
                    Machine m = f.FindMachineWithMostErrors();
                    Console.WriteLine($"錯誤最多的機台是: 機台ID: {m.Id} ({m.Type})， 共有 {m.ErrorCodes.Count} 個錯誤紀錄");
                    Console.WriteLine();
                }
                else if (op == 3)
                {
                    break;
                }
            }
        }
    }
}
