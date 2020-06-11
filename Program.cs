using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Data.SqlClient;


namespace Lab6_Vlasenko_Viktoria_IS81_Var4
{
    public class Employee
    {
        public int id;
        public string name, position, specialty, department, equiptment, acc;
        public Employee()
        {
            CreateEmployee();
        }
        protected void CreateEmployee()
        {
            Console.Write("Enter employee id: "); this.id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter employee name: "); this.name = Console.ReadLine();
            Console.Write("Enter employee position: "); this.position = Console.ReadLine();
            Console.Write("Enter employee specialty: "); this.specialty = Console.ReadLine();
            Console.Write("Enter employee department: "); this.department = Console.ReadLine();
            Console.Write("Enter employee equiptment: "); this.equiptment = Console.ReadLine();
            Console.Write("Enter employee bank acc: "); this.acc = Console.ReadLine();
        }
    }
    public class Product
    {
        public int id, amount, term;
        public string name;
        public float unit_price;
        public Product()
        {
            CreateProduct();
        }
        protected void CreateProduct()
        {
            Console.Write("Enter product id: "); this.id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter product name: "); this.name = Console.ReadLine();
            Console.Write("Enter product unit price: "); this.unit_price = float.Parse(Console.ReadLine());
            Console.Write("Enter product min amount: "); this.amount = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter number of days to create product: "); this.term = Convert.ToInt32(Console.ReadLine());
        }
    }
    public class Order
    {
        public int id, product_id, employee_id;
        public string employee_acc, customer_acc;
        public Order()
        {
            CreateOrder();
        }
        protected void CreateOrder()
        {
            Console.Write("Enter order id: "); this.id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter product id: "); this.product_id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter employee id: "); this.employee_id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter employee bank acc: "); this.employee_acc = Console.ReadLine();
            Console.Write("Enter customer bank acc: "); this.customer_acc = Console.ReadLine();
        }
    }

    class Program
    {
        public static string connectionString = @"Data Source=DESKTOP-IO4C5UQ;Initial Catalog=lab6_vlasenko_is81;Integrated Security=True";
        public static string[] names = { "EMPLOYEE", "PRICE_LIST", "ORDER_" };

        public static void ReadAllTables()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("Подключение открыто");
                foreach (string table_name in names)
                {
                    Console.WriteLine(table_name + ":\n");
                    ReadTable(table_name);
                    Console.WriteLine("\n\n");
                }
            }
            //Console.WriteLine("Подключение закрыто...");
        }
        public static void ReadTable(string table_name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("Подключение открыто");
                string sqlExpression = "SELECT * FROM " + table_name;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i) + ": " + reader[i] + " | ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
                connection.Close();
            }

            //Console.WriteLine("Подключение закрыто...");
        }
        public static void AddInfo(string table_name)
        {
            string sqlExpression = "";
            switch (table_name)
            {
                case "EMPLOYEE": 
                    {
                        Employee employee = new Employee();
                        sqlExpression = $"insert into {table_name}(ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount) " +
                        $"values ({employee.id}, '{employee.name}', '{employee.position}', '{employee.specialty}', '{employee.department}', '{employee.equiptment}', '{employee.acc}');";
                            break; 
                    }
                case "PRICE_LIST": 
                    {
                        Product product = new Product();
                        sqlExpression = $"insert into {table_name}(ID, ProductName, UnitPrice, Amount, Term)" +
                        $"values ({product.id}, '{product.name}', {product.unit_price}, {product.amount}, {product.term});";
                        break;
                    }
                case "ORDER_":
                    {
                        Order order = new Order();
                        sqlExpression = $"insert into {table_name}(ID, ProductID, EmpID, EmpAccount, CustomerAccount) " +
                        $"values ({order.id}', {order.product_id}', {order.employee_id}', '{order.employee_acc}', '{order.customer_acc}');";
                        break;
                    }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("Подключение открыто");
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //Console.WriteLine("Подключение закрыто...");
            ReadTable(table_name);
        }
        public static void UpdateInfo(string table_name)
        {
            Console.WriteLine("Enter info to update:\n");
            string sqlExpression = "";
            switch (table_name)
            {
                case "EMPLOYEE":
                    {
                        Employee employee = new Employee();
                        sqlExpression = $"update {table_name} set EmpName='{employee.name}', Position='{employee.position}', Specialty='{employee.specialty}', " +
                            $"Department='{employee.department}', Equiptment='{employee.equiptment}', BankAccount='{employee.acc}' " +
                            $"where ID={employee.id};";
                        break;
                    }
                case "PRICE_LIST":
                    {
                        Product product = new Product();
                        sqlExpression = $"update {table_name} set ProductName='{product.name}', UnitPrice={product.unit_price}, " +
                            $"Amount={product.amount}, Term={product.term} " +
                            $"where ID={product.id};";
                        break;
                    }
                case "ORDER_":
                    {
                        Order order = new Order();
                        sqlExpression = $"update {table_name} set ProductID={order.product_id}, EmpID={order.employee_id}, EmpAccount='{order.employee_acc}', " +
                            $"CustomerAccount='{order.customer_acc}' " +
                            $"where ID={order.id};"; 
                        break;
                    }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("Подключение открыто");
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //Console.WriteLine("Подключение закрыто...");
            ReadTable(table_name);
        }
        public static void DeleteInfo(string table_name)
        {
            Console.Write($"Enter id to delete row from {table_name}: "); int id = Convert.ToInt32(Console.ReadLine());
            string sqlExpression = $"delete from {table_name} where ID={id};";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("Подключение открыто");
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //Console.WriteLine("Подключение закрыто...");
            ReadTable(table_name);
        }
        
        public static void Requests()
        {
            string[] requests_names = {
                "1 - Сума за послугу вища за 150 грн",
                "2 - Вивести виконавців, які виконали більше одного замовлення",
                "3 - Вивести покупців, які робили замовлення у видавництві більше одного разу",
                "4 - Вивести усю інформацію з плайс-листа про 'Листівки'",
                "5 - Вивести усі послуги, які виконуються більше двох днів",
                "6 - Знайти співробітників, які працюють з однаковим технічними засобами",
                "7 - Вивести список департаментів співробітників, що не повторюються",
                "8 - Вивести імена виконавців та відповідну інформацію про замовлення, що було виконане ними",
                "9 - Вивести найдешевшу послугу у видавництві",
                "10 - Вивести співробітників та кількість виконаних ними замовлень"
            };
            string[] requests =
            {
                "select * from PRICE_LIST where UnitPrice>=150;",
                "select count(EMPLOYEE.ID) as Number_Of_Orders, EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount " +
                "from EMPLOYEE inner join ORDER_ on EMPLOYEE.ID=ORDER_.EmpID " +
                "group by EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount " +
                "having count(EMPLOYEE.ID)>1;",
                "select count(ID) as Number_Of_Orders, CustomerAccount from ORDER_ group by CustomerAccount having count(ID)>1; ",
                "select * from PRICE_LIST where ProductName like 'Листівка'; ",
                "select * from PRICE_LIST where Term>2; ",
                "select EmpName, Equiptment from EMPLOYEE group by EmpName, Equiptment having count(ID)>1",
                "select Department from EMPLOYEE where Department in(select Department from( select Department, count(Department) as uniq from EMPLOYEE group by Department) t " +
                "where uniq=1)",
                "select * from EMPLOYEE inner join ORDER_ on EMPLOYEE.ID=ORDER_.EmpID",
                "select top 1 * from PRICE_LIST where UnitPrice>0 order by UnitPrice asc",
                "select count(EMPLOYEE.ID) as Number_Of_Orders, EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount " +
                "from EMPLOYEE inner join ORDER_ on EMPLOYEE.ID=ORDER_.EmpID " +
                "group by EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount"
            };
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\t=========={requests_names[i]}==========");
                ReadResult(requests[i]);
                Console.WriteLine("\n");
            }
        }
        public static void ReadResult(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("Подключение открыто");
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i) + ": " + reader[i] + " | ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
            }
            //Console.WriteLine("Подключение закрыто...");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            char ch = ' ';
            int check = 0;
            do
            {
                Console.WriteLine("\t===========MENU===========\n" +
                "1 - Read all tables\n" +
                "2 - Add some new info to the table\n" +
                "3 - Update info in table\n" +
                "4 - Delete info from table\n" +
                "5 - Requests\n" +
                "0 - Exit\n" +
                "Enter menu item: ");
                int key = Convert.ToInt32(Console.ReadLine());
                switch(key){
                    case 1: { ReadAllTables(); Console.ReadKey(); Console.Clear(); break; }
                    case 2:
                        {
                            Console.Write($"Write name of table you want to add information (possible answers: {names[0]}, {names[1]}, {names[2]}): ");
                            string table_name = Console.ReadLine();
                            AddInfo(table_name);
                            Console.ReadKey(); Console.Clear(); break;
                        }
                    case 3:
                        {
                            Console.Write($"Write name of table you want to update information (possible answers: {names[0]}, {names[1]}, {names[2]}): ");
                            string table_name = Console.ReadLine();
                            UpdateInfo(table_name);
                            Console.ReadKey(); Console.Clear(); break;
                        }
                    case 4:
                        {
                            Console.Write($"Write name of table you want to delete information from (possible answers: {names[0]}, {names[1]}, {names[2]}): ");
                            string table_name = Console.ReadLine();
                            DeleteInfo(table_name);
                            Console.ReadKey(); Console.Clear(); break;
                        }
                    case 5:
                        {
                            Requests();
                            Console.ReadKey(); Console.Clear(); break;
                        }
                    default: { Console.WriteLine("Try to choose another option."); break; }
                }
                Console.WriteLine("Would you like to try again? [y/n]");
                ch = Convert.ToChar(Console.ReadLine());
                Console.Clear();
            } while (ch != 'N' && ch != 'n');
        }
    }
}
