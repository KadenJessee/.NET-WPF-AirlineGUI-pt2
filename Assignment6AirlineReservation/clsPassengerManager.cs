using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6Flight
{
    internal class clsPassengerManager
    {

        /// <summary>
        /// object to connect to the data access
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// creates object for the data access
        /// </summary>
        public clsPassengerManager()
        {
            try
            {
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// gets the passengers from SQL Queries and returnss a list of clsPassengers
        /// </summary>
        /// <returns></returns>
        public List<clsPassenger> GetPassengers(string sFlightID)
        {
            try
            {
                //create a new lislt
                List<clsPassenger> passengers = new List<clsPassenger>();
                int i = 0;

                //get the sql statement
                string sSQL = clsSQL.GetPassengers(sFlightID);

                //execute the sql statement
                DataSet dsPassengers = new DataSet();

                dsPassengers = db.ExecuteSQLStatement(sSQL, ref i);

                //loop through
                foreach (DataRow dr in dsPassengers.Tables[0].Rows)
                {
                    //create new passenger
                    clsPassenger clsMyPassenger = new clsPassenger();
                    //get the data
                    clsMyPassenger.PassengerID = int.Parse(dr["Passenger_ID"].ToString());
                    clsMyPassenger.FirstName = (string)dr["First_Name"];
                    clsMyPassenger.LastName = (string)dr["Last_Name"];
                    clsMyPassenger.SeatNum = (string)dr["Seat_Number"];
                    //add to list of flights
                    passengers.Add(clsMyPassenger);
                }
                return passengers;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //addPassenger
        /// <summary>
        /// inserts a new passenger into the database
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void InsertPassenger(string FirstName, string LastName)
        {
            try
            {
                //get the sql string
                string sSQL = clsSQL.InsertPassenger(FirstName, LastName);
                //execute the nonquery to update the database
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// inserts into the link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="sSeatNum"></param>
        /// <exception cref="Exception"></exception>
        public void InsertLinkTable(int sFlightID, int sPassengerID, string sSeatNum)
        {
            try
            {
                //get the sql string
                string sSQL = clsSQL.InsertLinkTable(sFlightID, sPassengerID, sSeatNum);
                //execute the non query
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        //updatePassengerSeat
        /// <summary>
        /// updates a passengers seat
        /// </summary>
        /// <param name="seatNum"></param>
        /// <param name="sFlightID"></param>
        /// <param name="PassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void UpdatePassengerSeat(int sFlightID, int PassengerID, string seatNum)
        {
            try
            {
                string sSQL = clsSQL.UpdateSeatNums(sFlightID, PassengerID, seatNum);

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        //deletePassenger
        /// <summary>
        /// gets the sql delete statement and deletes it
        /// </summary>
        /// <param name="PassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void DeletePassenger(int PassengerID)
        {
            try
            {
                string sSQL = clsSQL.DeletePassenger(PassengerID);

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// deletes the passenger link
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        public void DeletePassengerLink(int sFlightID, int sPassengerID)
        {
            try
            {
                string sSQL = clsSQL.DeleteLink(sFlightID, sPassengerID);

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        public int GetPassengerID(string firstName, string lastName)
        {
            try
            {
                int passengerID = 0;
                string sSQL = clsSQL.GetPassengerID(firstName, lastName);
                //passengerID = db.ExecuteNonQuery(sSQL);
                passengerID = Convert.ToInt32(db.ExecuteScalarSQL(sSQL));
                return passengerID;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
