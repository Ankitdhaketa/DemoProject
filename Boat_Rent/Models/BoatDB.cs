using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Boat_Rent.Models;

namespace CRUDinMVC.Models
{
    public class BoatDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["BoatConn"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW BOAT *********************
        public bool AddBoat(Boat smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewBoat", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Image", smodel.Image);
            cmd.Parameters.AddWithValue("@Rate", smodel.Rate);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW BOat DETAILS ********************
        public List<Boat> GetBoat()
        {
            connection();
            List<Boat> Boatlist = new List<Boat>();

            SqlCommand cmd = new SqlCommand("GetBoatDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Boatlist.Add(
                    new Boat
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Image = Convert.ToString(dr["Image"]),
                        Rate = Convert.ToInt16(dr["HOURLYRate"]),
                        IsRent = Convert.ToInt16(dr["ISRent"])
                    });
            }
            return Boatlist;
        }

        // ***************** Rent Boat *********************
        public bool RentedBoat(Customer smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("RentedBoat", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", smodel.Id);
            cmd.Parameters.AddWithValue("@Number", smodel.BoatNumber);
            cmd.Parameters.AddWithValue("@CustomerName", smodel.CustomerName);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ***************** Return Boat *********************
        public List<Customer> ReturnBoat(Customer smodel)
        {
            connection();
            List<Customer> CustList = new List<Customer>();
            SqlCommand cmd = new SqlCommand("SP_ReturnBoat", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", smodel.Id);
            cmd.Parameters.AddWithValue("@Number", smodel.BoatNumber);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                CustList.Add(
                    new Customer
                    {
                        Price = Convert.ToInt32(dr["Price"]),
                        Time = Convert.ToInt32(dr["Rent_Time"]),                      
                    });
            }
            return CustList;
        }




        // ********************** DELETE Boat DETAILS *******************
        public bool DeleteBoat(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteBoat", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}