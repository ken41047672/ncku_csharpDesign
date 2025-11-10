namespace P76144176_OOP_2
{
    internal class Program
    {
        class Workpiece
        {
            public Workpiece(int _id)
            {
                id = _id;
                isPassed = false;
            }
            int id;
            bool isPassed;
            public int Id { get { return id;  } }
            public bool IsPassed { get { return isPassed;  } set { isPassed = value; } }
        }


        class ProductionLine
        {
            public ProductionLine()
            {
                workpieces = new List<Workpiece>();
            }
            List<Workpiece> workpieces;
            public void AddWorkpiece(Workpiece toAdd)
            {
                workpieces.Add(toAdd);
            }
            public void RunQualityControl(IQualityControlStrategy qcStrategy)
            {
                int passed = 0, failed = 0;
                Console.WriteLine("--- 開始執行品質檢測 ---");
                foreach(Workpiece piece in workpieces)
                {
                    Console.Write($"對工件 [{ piece.Id}] ");
                    bool result = qcStrategy.PerformCheck(piece);   

                    if (result)
                    {
                        Console.WriteLine(" 通過!");
                        piece.IsPassed = true;
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine(" 未通過。");
                        piece.IsPassed = false;
                        failed++;
                    }
                }
                Console.WriteLine("--- 檢測完成 ---");
                Console.WriteLine($"檢測結果: {passed}個通過，{failed}個失敗");

            }
        }
        interface IQualityControlStrategy
        {
            public bool PerformCheck(Workpiece item);
        }
        class VisualInspection : IQualityControlStrategy
        {
            public bool PerformCheck(Workpiece item)
            {
                Console.Write("執行視覺檢測...");
                return true;
            }
        }
        class ElectronicTest : IQualityControlStrategy
        {
            public bool PerformCheck(Workpiece item)
            {
                Console.Write("執行精密電子測試...");
                if (item.Id % 2 == 0) return true;
                else                  return false;
            }
        }
        static void Main(string[] args)
        {
            int n = 3;
            ProductionLine p = new ProductionLine();
            p.AddWorkpiece(new Workpiece(101));
            p.AddWorkpiece(new Workpiece(102));
            p.AddWorkpiece(new Workpiece(103));
            Console.WriteLine($"生產線上準備了 {n} 個帶檢測工件 (ID: 101, 102, 103)");
            Console.WriteLine("請選擇檢測策略: [1] 視覺檢測 [2] 精密電子測試");
            Console.Write("> ");
            int op = 0;
            try
            {
                string input = Console.ReadLine();
                op = int.Parse(input);
            }
            catch (Exception ex) { }
            if (op == 1)
            {
                VisualInspection v = new VisualInspection();
                p.RunQualityControl(v);
            }
            else if (op == 2)
            {
                ElectronicTest v = new ElectronicTest();
                p.RunQualityControl(v);
            }
        }
    }
}
