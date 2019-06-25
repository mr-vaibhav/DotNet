using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace NewDatabaseDemosCDAC
{
    class DbDemo
    {
        private SqlConnection sqlCon = null;
        private SqlCommand sqlCmd;
        private SqlDataReader sqlReader;
        private string str;

        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            DbDemo dbObj = new DbDemo();
           //dbObj.ListAllDepts();
            DeptBO dep = new DeptBO() { DeptCode = 400, DeptName = "EventsMgmt", Location = "Las Vegas" };
            int c = dbObj.PerformInsert(dep);
            dbObj.ListAllDepts();

            DeptBO depupdate = new DeptBO() { DeptCode = 12, DeptName = "ModifiedDept", Location = "ModifiedLoc" };
            dbObj.PerformUpdate(depupdate);
            Console.WriteLine("Enter Dept Number to be modified");
            int b = Convert.ToInt32(Console.ReadLine());
            int a = dbObj.PerformDelete(b);


           // dbObj.ListAllDeptsUsingProc();
           // dbObj.ListAllDeptsByDeptCodeUsingProc(10);
            Console.WriteLine("Done");

            Console.ReadLine();
        }

        public SqlConnection GetConnection()
        {
            str = ConfigurationManager.ConnectionStrings["CnStr"].ToString();
            sqlCon = new SqlConnection(str);
            if (sqlCon.State != ConnectionState.Open)
            {
                sqlCon.Open();
            }
            return sqlCon;
        }

        public int PerformInsert(DeptBO dep)
        {//int deptno, string dname, string location
            int rowCount = 0;
            //int deptno = 0;
            sqlCon = this.GetConnection();
            try
            {

                sqlCmd = new SqlCommand("INSERT INTO Dept VALUES (@pDeptCode, @pDName, @pLoc)", sqlCon);
                sqlCmd.Parameters.AddWithValue("@pDeptCode", dep.DeptCode);
                sqlCmd.Parameters.AddWithValue("@pDName", dep.DeptName);
                sqlCmd.Parameters.AddWithValue("@pLoc", dep.Location);
                rowCount = sqlCmd.ExecuteNonQuery();
                if (rowCount == 1)
                {
                    Console.WriteLine("New Department added successfully...");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                sqlCmd.Dispose();


            }
            return rowCount;
        }

        public int PerformUpdate(DeptBO dep)
        {
            int rowCount = 0;
            //int deptno = 0;
            sqlCon = this.GetConnection();
            try
            {
                // sqlCon.Open();
                sqlCmd = new SqlCommand("UPDATE DEPT SET DEPTNAME=@pDName,LOCATION=@pLoc WHERE DEPTCODE=@pDeptCode", sqlCon);
                sqlCmd.Parameters.AddWithValue("@pDeptCode", dep.DeptCode);
                sqlCmd.Parameters.AddWithValue("@pDName", dep.DeptName);
                sqlCmd.Parameters.AddWithValue("@pLoc", dep.Location);
                rowCount = sqlCmd.ExecuteNonQuery();
                if (rowCount > 0)
                {
                    Console.WriteLine("Existing {0} Department added successfully...", dep.DeptName);
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                sqlCmd.Dispose();


            }
            return rowCount;
        }

        public int PerformDelete(int deptno)
        {
            int rowCount = 0;
            //int deptno = 0;
            sqlCon = this.GetConnection();
            try
            {

                sqlCmd = new SqlCommand("DELETE FROM Dept WHERE DEPTCODE=@pDeptCode", sqlCon);
                sqlCmd.Parameters.AddWithValue("@pDeptCode", deptno);

                rowCount = sqlCmd.ExecuteNonQuery();
                if (rowCount > 0)
                {
                    Console.WriteLine("THE Department REMOVED successfully...");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCmd.Dispose();


            }
            return rowCount;
        }

        public void ListAllDepts()
        {
            sqlCon = this.GetConnection();
            try
            {
                //sqlCon.Open();
                sqlCmd = new SqlCommand("SELECT * FROM DEPT", sqlCon);
                //sqlCmd.Parameters.AddWithValue("@pDeptCode", deptno);
                //sqlCmd.Parameters.AddWithValue("@pDName", dname);
                //sqlCmd.Parameters.AddWithValue("@pLoc", location);
                sqlReader = sqlCmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    Console.WriteLine("Code \t\t\t Name \t\t\t Location ");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    while (sqlReader.Read())
                    {
                        Console.WriteLine("{0}\t\t\t Name={1}\t\t\t location={2}", sqlReader[0], sqlReader["DeptName"], sqlReader[2]);
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            { sqlCmd.Dispose();
                sqlCon.Close();
            }
            
        }

        public SqlDataReader ListBydeptLocation(string ploc)
        {
            sqlCon = this.GetConnection();
            try
            {
                //sqlCon.Open();
                sqlCmd = new SqlCommand("SELECT * FROM DEPT where location=@pLoc", sqlCon);

                sqlCmd.Parameters.AddWithValue("@pLoc", ploc);
                sqlReader = sqlCmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    Console.WriteLine("Code \t\t\t Name \t\t\t Location ");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    while (sqlReader.Read())
                    {
                        Console.WriteLine("{0}\t\t\t Name={1}\t\t\t location={2}", sqlReader[0], sqlReader["DeptName"], sqlReader[2]);
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            { sqlCmd.Dispose(); }
            return sqlReader;
        }

        public void ListAllDeptsUsingProc()
        {
            sqlCon = this.GetConnection();
            try
            {
                //sqlCon.Open();
                sqlCmd = new SqlCommand("GetDepts", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    Console.WriteLine("Code \t\t\t Name \t\t\t Location ");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    while (sqlReader.Read())
                    {
                        Console.WriteLine("{0}\t\t\t Name={1}\t\t\t location={2}", sqlReader[0], sqlReader["DeptName"], sqlReader[2]);
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            { sqlCmd.Dispose(); }
            
        }

        
    }



}

