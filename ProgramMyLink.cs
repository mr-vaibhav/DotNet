using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{
    class Program
    {
        public static BookDataContext ctx = null;
        public static int MenuDriven()
        {
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Add Data");
            Console.WriteLine("2.Update Data");
            Console.WriteLine("3.Delete Data");
            Console.WriteLine("4.Display Data");
            Console.WriteLine("Enter Choice");
            return int.Parse(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            int choice;

            while ((choice = MenuDriven()) != 0)
            {
                switch (choice)
                {
                    case 0:
                        break;
                    case 1:
                        addData();
                        break;
                    case 2:
                        Console.WriteLine("Enter Id to Update");
                        int id = int.Parse(Console.ReadLine());
                        update(id);
                        break;
                    case 3:
                        Console.WriteLine("Enter Id to Update");
                        int id1 = int.Parse(Console.ReadLine());
                        deleteByID(id1);
                        break;
                    case 4:
                        listAll();
                        break;
                }
            }

        }
        private static void listAll()
        {
            ctx = new BookDataContext();
            var query = from Book in ctx.Books select Book;
            foreach (var b in query)
            {
                Console.WriteLine("Bid:{0} BName:{1} BPrice:{2}",b.bid,b.bname,b.bprice);
            }
        }
        private static void addData()
        {
            ctx = new BookDataContext();

            Console.WriteLine("Enter Bid");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("ENter BName");
            string Name = Console.ReadLine();

            Console.WriteLine("Enter BPrice");
            int price = int.Parse(Console.ReadLine());

            Book b = new Book();
            b.bid = id;
            b.bname = Name;
            b.bprice = price;
            ctx.Books.InsertOnSubmit(b);
            ctx.SubmitChanges();
            Console.WriteLine("Data Has Been Inserted");
        }

        public static void deleteByID(int id)
        {
            ctx = new BookDataContext();
            var query = from Book in ctx.Books where Book.bid == id select Book;
            foreach (var d in query)
            {
                ctx.Books.DeleteOnSubmit(d);
                ctx.SubmitChanges();
                Console.WriteLine("Record Deleted");
            }
        }

        public static void update(int id)
        {
            ctx = new BookDataContext();

            Console.WriteLine("ENter BName");
            string Name = Console.ReadLine();

            Console.WriteLine("Enter BPrice");
            int price = int.Parse(Console.ReadLine());

            var query = from Book in ctx.Books where Book.bid == id select Book;
            foreach(var d in query)
            {
                d.bname = Name;
                d.bprice = price;
            }
            ctx.SubmitChanges();
            Console.WriteLine("Data Has Been Updated");
        }
    }
}