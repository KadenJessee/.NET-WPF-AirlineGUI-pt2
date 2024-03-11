using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6Flight
{
    internal class clsSQL
    {
        /// <summary>
        /// SQL query to get the flights
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// sql query to get the passengers
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengers(string sFlightID)
        {
            try
            {
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                                "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                                "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                                "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                                "FLIGHT.FLIGHT_ID = " + sFlightID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// sql statement to  update the seat numbers
        /// </summary>
        /// <param name="seatNum"></param>
        /// <param name="sFlightID"></param>
        /// <param name="PassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateSeatNums(int sFlightID, int PassengerID, string seatNum)
        {
            try
            {
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                                "SET Seat_Number = '" + seatNum + "' " +
                                "WHERE FLIGHT_ID = " + sFlightID + " AND PASSENGER_ID = " + PassengerID;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// sql statement to insert a passenger into the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertPassenger(string First_Name, string Last_Name)
        {
            try
            {
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) " +
                            "VALUES('" + First_Name + "','" + Last_Name + "')";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// sql statement to insert values into the link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="sSeatNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertLinkTable(int sFlightID, int sPassengerID, string sSeatNum)
        {
            try
            {
                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                    "VALUES( " + sFlightID + " , " + sPassengerID + " , " + sSeatNum + ")";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// sql statement to delete the link from the link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteLink(int  sFlightID, int sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                                "WHERE FLIGHT_ID = " + sFlightID + " AND " +
                                "PASSENGER_ID = " + sPassengerID;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// sql statement to delete a passenger
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassenger(int sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM PASSENGER " +
                                "WHERE PASSENGER_ID = " + sPassengerID;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string GetPassengerID(string firstName, string lastName)
        {
            try
            {
                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '" + firstName
                    + "' AND Last_Name = '" + lastName + "'";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}
