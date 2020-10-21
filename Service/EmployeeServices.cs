using System;
using System.Collections.Generic;
using System.Configuration;
using CRUD_MVC.Models;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

/* 
CREATE PROCEDURE Employee // Stored Procedure to interact with DB
@mode nvarchar(max),
@Name nvarchar(max)=null,
@Email nvarchar(max)=null,
@Designation nvarchar(max)=null,
@Id int =null
AS
BEGIN
	
	SET NOCOUNT ON;

    if(@mode='GetEmployees')
	BEGIN
		SELECT * from Employees
	END

	if(@mode='AddEmployee')
	BEGIN
		Insert into Employees(
		Name,
		Email,
		Designation
		)
		values(
		 @Name,
		 @Email,
		 @Designation
		)
	END

	if(@mode='EmployeeById')
	BEGIN
		SELECT * FROM Employees where Id=@Id

	END

	if(@mode='Update')
	Begin
		UPDATE Employees set Name=@Name,Designation=@Designation,Email=@Email
		WHERE Id=@Id
	END

	if(@mode='DelEmp')
	BEGIN
		DELETE Employees WHERE Id=@Id
	END
	
END
*/
namespace CRUD_MVC.Service
{
    public class EmployeeServices
    {
        public  string ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();
        private SqlDataAdapter _adap;
        private DataSet _ds;

        public List<Employee> GetEmployeeData()
        {
            _ds = new DataSet();
            int i = 0;
            List<Employee> list = new List<Employee>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmployees");
                _adap = new SqlDataAdapter(cmd);
                _adap.Fill(_ds);
                if(_ds.Tables.Count > 0)
                {
                    while (i < _ds.Tables[0].Rows.Count)
                    {
                        Employee ob = new Employee();
                        ob.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        ob.Name = Convert.ToString(_ds.Tables[0].Rows[i]["Name"]);
                        ob.Designation = Convert.ToString(_ds.Tables[0].Rows[i]["Designation"]);
                        ob.Email = Convert.ToString(_ds.Tables[0].Rows[i]["Email"]);

                        list.Add(ob);
                        i++;

                    }
                }
                con.Close();
            }
            
                return list;

        }

        public List<DesignationModel> GetDesignationData(){
            _ds = new DataSet();
            int i = 0;
            List<DesignationModel> list = new List<DesignationModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetDesignationData");
                _adap = new SqlDataAdapter(cmd);
                _adap.Fill(_ds);
                if(_ds.Tables.Count > 0)
                {
                    while (i < _ds.Tables[0].Rows.Count)
                    {
                        DesignationModel ob = new DesignationModel();
                        ob.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        ob.Designation_Emp = Convert.ToString(_ds.Tables[0].Rows[i]["Designation_Emp"]);
                        

                        list.Add(ob);
                        i++;

                    }
                }
                con.Close();
            }
            
            return list;
        }
        public void AddEmployeeData(Employee ob)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "AddEmployee");
                cmd.Parameters.AddWithValue("@Name", ob.Name);
                cmd.Parameters.AddWithValue("@Email", ob.Email);
                cmd.Parameters.AddWithValue("@Designation", ob.Designation);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(Employee ob)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                Console.WriteLine(ob.Name);
                SqlCommand cmd = new SqlCommand("Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "Update");
                cmd.Parameters.AddWithValue("@Id", ob.Id);
                cmd.Parameters.AddWithValue("@Name", ob.Name);
                cmd.Parameters.AddWithValue("@Email", ob.Email);
                cmd.Parameters.AddWithValue("@Designation", ob.Designation);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Employee GetEmployeeDataById(int id)
        {
            Employee model = new Employee();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                int i = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "EmployeeById");
                cmd.Parameters.AddWithValue("@Id", id);

                _ds = new DataSet();
                _adap = new SqlDataAdapter(cmd);
                _adap.Fill(_ds);
                

                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count>0)
                {                   
                        model.Id= Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        model.Name = Convert.ToString(_ds.Tables[0].Rows[i]["Name"]);
                        model.Designation = Convert.ToString(_ds.Tables[0].Rows[i]["Designation"]);
                        model.Email = Convert.ToString(_ds.Tables[0].Rows[i]["Email"]);
                    
                }
                con.Close();
            }

                return model;

        }

       

        public void DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DelEmp");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}